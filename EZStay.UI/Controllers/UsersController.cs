using EZStay.UI.Models;
using EZStay.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EZStay.UI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserApiService _userApiService;
        private readonly AuthApiService _authApiService;

        public UsersController(UserApiService userApiService, AuthApiService authApiService)
        {
            _userApiService = userApiService;
            _authApiService = authApiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authApiService.LoginAsync(
                    model.EmailOrUsername,
                    model.Password,
                    model.Role);

                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    // Store token in session
                    HttpContext.Session.SetString("JWTToken", response.Token);

                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Invalid login attempt");
            }

            return View(model);
        }

        public async Task<IActionResult> Index()
        {
            // Get token from session
            var token = HttpContext.Session.GetString("JWTToken");

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login");
            }

            // Set token on the API service
            _userApiService.SetAuthToken(token);

            // Get users from API
            var users = await _userApiService.GetUsersAsync();
            return View(users);
        }
    }
}