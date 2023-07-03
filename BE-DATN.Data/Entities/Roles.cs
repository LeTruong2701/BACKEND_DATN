using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Entities
{
    public class Roles: IdentityRole
    {
        public string Description { get; set; }
    }
}
