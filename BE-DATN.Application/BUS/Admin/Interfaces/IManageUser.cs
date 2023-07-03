using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.Common;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IManageUser
    {
        Task<ActionResult> DangkyUser(DangkyUserModel dto);
        Task<ActionResult> UpdateUser(DangkyUserModel dto);

        Task<KhachHang> GetById(int Id);
        

        Task<IdentityResult> DeleteUser(string userName);
        Task<ActionResult> ChangePassword(ChangePasswordModel dto);
        Task<Users> GetUserByIdUser(string id);

        Task<UserModel> GetUserByUserId(string id);

        Task<PagedResult<UserModel>> SearchUserPaging([FromBody] Dictionary<string, object> formData);
    }
}
