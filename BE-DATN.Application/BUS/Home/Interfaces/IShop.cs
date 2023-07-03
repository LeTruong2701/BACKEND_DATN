using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface IShop
    {
        PagedResult<SanPhamModel> SearchSanPhamPaging([FromBody] Dictionary<string, object> formData);
        Task<List<ThuongHieuModel>> GetListThuongHieu();
        
    }
}
