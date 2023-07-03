using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Application.BUS.Admin.DTO
{
    public class ChangePasswordModel
    {
        public string UserName { get; set; }
        public string CurentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
