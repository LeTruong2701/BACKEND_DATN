using BE_DATN.Application.Authenticate;
using BE_DATN.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IManageUser1
    {
        UserViewModel Authenticate(string username, string password);
        Task<List<UserViewModel>> Get();
        Task<PagedResult<UserViewModel>> GetAllPaging(int pageindex, int pagesize, string UserName, string Name, string Role);
        Task<UserViewModel> GetById(int Id);
        Task<int> Create(UserModel request);
        Task<int> Update(UserModel request);
        Task<int> Delete(int Id);
    }
}
