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
    public interface IManageKhuyenMai
    {
        Task<PagedResult<KhuyenMai>> SearchKhuyenMaiPaging([FromBody] Dictionary<string, object> formData);
        Task<List<KhuyenMai>> GetKhuyenMai();
        Task<List<KhuyenMai>> GetKhuyenMaiTrangNguoiDung();     
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<KhuyenMai> GetById(int Id);
        Task<int> Create(KhuyenMaiModel km);
        Task<int> Update(KhuyenMaiModel km);
        Task<int> Delete(int Id);
    }
}
