using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IManageKho
    {

        Task<PagedResult<KhoModel>> SearchKhoPaging([FromBody] Dictionary<string, object> formData);
        
    }
}
