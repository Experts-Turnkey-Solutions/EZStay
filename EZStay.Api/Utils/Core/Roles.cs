using System;
using System.Collections.Generic;

namespace EZStay.Api.Utils.Core
{
    public static class Roles
    {
        public enum RoleType
        {
            Admin = 1,
            Manager = 2,
            AccountManager = 3,
            ContentManager = 4,
            Owner = 5,
            User = 6
        }

        public static string Admin => RoleType.Admin.ToString();
        public static string Manager => RoleType.Manager.ToString();
        public static string AccountManager => RoleType.AccountManager.ToString();
        public static string ContentManager => RoleType.ContentManager.ToString();
        public static string Owner => RoleType.Owner.ToString();
        public static string User => RoleType.User.ToString();

        public static readonly List<string> AllRoles = new()
        {
            Admin,
            Manager,
            AccountManager,
            ContentManager,
            Owner,
            User
        };

        // Helper: parse string to enum
        public static bool TryParse(string roleString, out RoleType role)
        {
            return Enum.TryParse(roleString, out role);
        }
    }
}
