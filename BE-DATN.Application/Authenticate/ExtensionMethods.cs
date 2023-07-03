using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BE_DATN.Application.Authenticate
{
    public static class ExtensionMethods
    {
        public static IEnumerable<UserViewModel> WithoutPasswords(this IEnumerable<UserViewModel> users)
        {
            if (users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static UserViewModel WithoutPassword(this UserViewModel user)
        {
            if (user == null) return null;

            user.PassWord = null;
            return user;
        }
    }
}
