using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.Authenticate
{
    public class UserModel
    {
        public NguoiDung user { get; set; }
        public Account account { get; set; }
    }
}
