using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface IUserManage
    {
        Task<ActionResult> DangkyUserKhachHang(DangkyModel dto);

        Task<KhachHang> GetById(int Id);

        Task<int> Update(KhachhangModel kh);

        //đổi mật khẩu khách hàng
        Task<ActionResult> ChangePassword(DoiMatKhauModel dto);

    }
}
