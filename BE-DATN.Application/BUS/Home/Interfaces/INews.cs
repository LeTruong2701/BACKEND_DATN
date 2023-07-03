using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.Common;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface INews
    {
        Task<PagedResult<NewsModel>> SearchNewsPaging([FromBody] Dictionary<string, object> formData);
        Task<List<NewsModel>> GetNewsGanDay();
        Task<List<NguoiDungModel>> GetNguoiDangBai();
        Task<News> GetById(int Id);

    }
}
