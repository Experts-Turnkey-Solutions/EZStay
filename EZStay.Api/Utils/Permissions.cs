namespace EZStay.Api.Utils
{
    public static class Permissions
    {
        public const string CreateUser = "CreateUser";
        public const string EditUser = "EditUser";
        public const string DeleteUser = "DeleteUser";
        public const string ViewUsers = "ViewUsers";

        public const string ManageHotels = "ManageHotels";
        public const string ManageBookings = "ManageBookings";

        // Ensure Roles is referenced correctly
        public static readonly Dictionary<string, List<string>> RolePermissions = new()
        {
            { Roles.Admin, new List<string> { CreateUser, EditUser, DeleteUser, ViewUsers, ManageHotels, ManageBookings } },
            { Roles.Manager, new List<string> { ViewUsers, ManageHotels, ManageBookings } },
            { Roles.Customer, new List<string> { ManageBookings } }
        };
    }
}
