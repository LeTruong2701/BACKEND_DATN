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
    public interface IManageAccount
    {
        Task<PagedResult<Account>> SearchDanhMucPaging([FromBody] Dictionary<string, object> formData);
        Task<List<Account>> GetAccount();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<Account> GetById(int Id);
        Task<int> Create(AccountModel ac);
        Task<int> Update(AccountModel ac);
        Task<int> Delete(int Id);
    }
}
