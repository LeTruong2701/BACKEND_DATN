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
    public interface IManageNews
    {
        Task<PagedResult<NewsModel>> SearchNewsPaging([FromBody] Dictionary<string, object> formData);
        Task<List<News>> GetNews();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<News> GetById(int Id);
        Task<int> Create(NewsModel news);
        Task<int> Update(NewsModel news);
        Task<int> Delete(int Id);
    }
}
