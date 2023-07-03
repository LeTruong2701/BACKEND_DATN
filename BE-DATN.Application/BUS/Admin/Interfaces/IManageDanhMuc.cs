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
    public interface IManageDanhMuc
    {
        Task<PagedResult<DanhMucModel>> SearchDanhMucPaging([FromBody] Dictionary<string, object> formData);
        Task<List<DanhMucModel>> GetDanhMucLon();
        Task<List<DanhMucModel>> GetListDanhMucCon();
        Task<List<DanhMucModel>> GetDanhMucNho(int IdDanhMuc);
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<DanhMuc> GetById(int Id);
        Task<int> Create(DanhMucModel dm);
        Task<int> Update(DanhMucModel dm);
        Task<int> Delete(int Id);
    }
}
