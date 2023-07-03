using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BE_DATN.Application.Common;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageRole : IManageRole
    {

        //private readonly UserManager<Users> _userManager;
        private readonly BEDATNDbContext _context;

        private readonly RoleManager<Roles> _roleManager;
        //private readonly UserManager<Users> _userManager;

        public ManageRole(RoleManager<Roles> roleManager, BEDATNDbContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }
        public async Task<List<RoleModel>> GetListRole()
        {
            var data = from th in _context.Role1s
                       select new
                       {
                           Id = th.Id,
                           Name = th.Name,
                           Description = th.Description,
                       };
            return await data.Select(x => new RoleModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description


            }).ToListAsync();
        }
        public async Task<ActionResult> CreateRole(RoleModel dto)
        {
            var existRole = await _roleManager.FindByNameAsync(dto.Name);

            if (existRole != null) return new BadRequestObjectResult($"Username {dto.Name} has already been taken");

            var appRole = new Roles
            {
                Name = dto.Name,
                Description = dto.Description

            };

            var result1 = await _roleManager.CreateAsync(appRole);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            var response = await _roleManager.FindByNameAsync(dto.Name);

            return new OkObjectResult(response);
        }

        public async Task<IdentityResult> DeleteRole(string Name)
        {
            var role = await _roleManager.FindByNameAsync(Name);

            if (role == null) return IdentityResult.Failed(new IdentityError { Description = $"Role {Name} not found" });

            // Xóa role
            var result2 = await _roleManager.DeleteAsync(role);

            if (!result2.Succeeded) return IdentityResult.Failed(result2.Errors.ToArray());

            return IdentityResult.Success;
        }

       

        public async Task<ActionResult> UpdateRole(RoleModel dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id);

            if (role == null) return new BadRequestObjectResult($"Role{dto.Name} has already been taken");

            // Cập nhật thông tin người dùng
            role.Name = dto.Name;
            role.Description = dto.Description;
            var result1 = await _roleManager.UpdateAsync(role);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);


            var response = await _roleManager.FindByNameAsync(dto.Name);

            return new OkObjectResult(response);
        }

        public async Task<PagedResult<RoleModel>> SearchRolePaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from r in _context.Role1s
                         select new
                         {
                             Id = r.Id,
                             Name = r.Name,
                             Description = r.Description
                         };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.Name.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize)
                .Select(x => new RoleModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description


                }).ToListAsync();

            var pageResult = new PagedResult<RoleModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<RoleModel> GetRoleById(string id)
        {
            var data = from r in _context.Role1s
                       select new
                       {
                           r.Id,
                           r.Name,
                           r.Description
                       };
            var role = await data.FirstOrDefaultAsync(a => a.Id == id);
            
            if (role != null)
            {
                return new RoleModel()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description
                };
            }
            return null;
        }
    }
}
