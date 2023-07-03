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
    public interface IManageNhanVien
    {
        Task<PagedResult<NhanVien>> SearchNhanVienPaging([FromBody] Dictionary<string, object> formData);
        Task<List<NhanVien>> GetNhanVien();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<NhanVien> GetById(int Id);
        Task<int> Create(NhanVienModel nv);
        Task<int> Update(NhanVienModel nv);
        Task<int> Delete(int Id);
    }
}
