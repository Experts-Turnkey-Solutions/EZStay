using System.Collections.Generic;

namespace EZStay.Api.Utils.Core
{
    public static class Permissions
    {
        // User Management
        public const string CreateUser = "Permissions.Users.Create";
        public const string EditUser = "Permissions.Users.Edit";
        public const string DeleteUser = "Permissions.Users.Delete";
        public const string ViewUsers = "Permissions.Users.View";

        // Hotel Management
        public const string ManageHotels = "Permissions.Hotels.Manage";
        public const string ViewHotels = "Permissions.Hotels.View";

        // Booking Management
        public const string ManageBookings = "Permissions.Bookings.Manage";
        public const string ViewBookings = "Permissions.Bookings.View";

        // Account Management
        public const string ManageAccounts = "Permissions.Accounts.Manage";

        // Content Management
        public const string ManageContent = "Permissions.Content.Manage";

        // Role-based permission mapping
        public static readonly Dictionary<string, List<string>> RolePermissions = new()
        {
            { Roles.Admin, new List<string>
                {
                    CreateUser, EditUser, DeleteUser, ViewUsers,
                    ManageHotels, ViewHotels,
                    ManageBookings, ViewBookings,
                    ManageAccounts,
                    ManageContent
                }
            },

            { Roles.Manager, new List<string>
                {
                    ViewUsers,
                    ManageHotels, ViewHotels,
                    ManageBookings, ViewBookings
                }
            },

            { Roles.AccountManager, new List<string>
                {
                    ViewUsers,
                    ManageAccounts
                }
            },

            { Roles.ContentManager, new List<string>
                {
                    ManageContent
                }
            },

            { Roles.Owner, new List<string>
                {
                    ViewUsers,
                    ViewHotels,
                    ViewBookings
                }
            },

            { Roles.User, new List<string>
                {
                    ViewHotels,
                    ViewBookings,
                    ManageBookings
                }
            }
        };
    }
}
