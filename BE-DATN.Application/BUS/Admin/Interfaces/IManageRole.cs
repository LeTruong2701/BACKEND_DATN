using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IManageRole
    {
        Task<List<RoleModel>> GetListRole();

        Task<ActionResult> CreateRole(RoleModel dto);
        Task<ActionResult> UpdateRole(RoleModel dto);

        Task<IdentityResult> DeleteRole(string Name);
        Task<RoleModel> GetRoleById(string id);


        Task<PagedResult<RoleModel>> SearchRolePaging([FromBody] Dictionary<string, object> formData);
    }
}
