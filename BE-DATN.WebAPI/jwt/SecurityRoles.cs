using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BE_DATN.WebAPI.jwt
{
    public static class SecurityRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";

        public static readonly IList<string> Roles = new ReadOnlyCollection<string>
            (new List<string>
            {
                Admin,
                Manager,
                User
            });
    }
}
