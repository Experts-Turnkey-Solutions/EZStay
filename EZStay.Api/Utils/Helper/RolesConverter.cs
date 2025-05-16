using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EZStay.API.Utils.Helper
{
    public class RolesConverter : ValueConverter<List<string>, string>
    {
        public RolesConverter() : base(
            rolesList => string.Join(",", rolesList),
            rolesString => string.IsNullOrEmpty(rolesString)
                ? new List<string>()
                : rolesString.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
        )
        { }
    }
}
