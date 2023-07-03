using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.Common;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IManageKhachHang
    {
        Task<PagedResult<KhachHang>> SearchKhachHangPaging([FromBody] Dictionary<string, object> formData);
        Task<List<KhachHang>> GetKhachHang();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<KhachHang> GetById(int Id);
        Task<int> Create(KhachHangModel kh);
        Task<int> Update(KhachHangModel kh);
        Task<int> Delete(int Id);
    }
}
