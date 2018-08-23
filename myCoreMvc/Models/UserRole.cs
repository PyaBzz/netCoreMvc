using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Models
{
    public static class UserRole
    {
        public const string AdminRoleName = "Admin";
        public const string JuniorRoleName = "Junior";
        public const string SeniorRoleName = "Senior";

        public static string[] All => new string[]
        {
            AdminRoleName,
            JuniorRoleName,
            SeniorRoleName
        };
    }
}
