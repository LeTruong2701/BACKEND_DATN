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
    public interface IManageNguoiDung
    {
        Task<PagedResult<NguoiDung>> SearchNguoiDungPaging([FromBody] Dictionary<string, object> formData);
        Task<List<NguoiDungModel>> GetListNguoiDung();
        Task<List<NguoiDung>> GetNguoiDung();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<NguoiDung> GetById(int Id);
        Task<int> Create(NguoiDungModel nd);
        Task<int> Update(NguoiDungModel nd);
        Task<int> Delete(int Id);
    }
}
