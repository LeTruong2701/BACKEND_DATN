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
    public interface IManageThuongHieu
    {
        Task<PagedResult<ThuongHieu>> SearchThuongHieuPaging([FromBody] Dictionary<string, object> formData);
        Task<List<ThuongHieuModel>> GetListThuongHieu();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<ThuongHieu> GetById(int Id);
        Task<int> Create(ThuongHieuModel th);
        Task<int> Update(ThuongHieuModel th);
        Task<int> Delete(int Id);
    }
}
