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
    public interface IManageNhaCungCap
    {
        Task<PagedResult<NhaCungCap>> SearchNhaCungCapPaging([FromBody] Dictionary<string, object> formData);
        
        Task<List<NhaCungCapModel>> GetNhaCungCap();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<NhaCungCap> GetById(int Id);
        Task<int> Create(NhaCungCapModel ncc);
        Task<int> Update(NhaCungCapModel ncc);
        Task<int> Delete(int Id);
    }
}
