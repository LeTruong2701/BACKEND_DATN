using BE_DATN.Application.Authenticate;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using BE_DATN.Authenticate;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageUser1 : IManageUser1
    {
        MD5 md = MD5.Create();
        private readonly BEDATNDbContext _context;
        private readonly AppSettings _appSettings;
        public ManageUser1(IOptions<AppSettings> appSettings, BEDATNDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }
        public UserViewModel Authenticate(string username, string password)
        {
            byte[] inputstr = System.Text.Encoding.ASCII.GetBytes(password);
            byte[] hash = md.ComputeHash(inputstr);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            password = sb.ToString();

            var result = from ac in _context.Accounts
                         join nd in _context.NguoiDungs on ac.IdNguoiDung equals nd.IdNguoiDung
                         select new UserViewModel
                         {
                              IdNguoiDung = nd.IdNguoiDung,
                              HoTen       =nd.HoTen,
                              GioiTinh    =nd.GioiTinh,
                              NgaySinh    =nd.NgaySinh,
                              DiaChi      =nd.DiaChi,
                              SDT         =nd.SDT,
                              Email       =nd.Email,
                              AnhDaiDien  =nd.AnhDaiDien,
                              TrangThai   =nd.TrangThai,
                              IdAccount   =ac.IdAccount,
                              UserName    =ac.UserName,
                              PassWord    =ac.PassWord,
                              LoaiQuyen   =ac.LoaiQuyen

                         };

            var user = result.SingleOrDefault(x => x.UserName == username && x.PassWord == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.HoTen.ToString()),
                    new Claim(ClaimTypes.MobilePhone, user.SDT.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public async Task<List<UserViewModel>> Get()
        {

            var result = from ac in _context.Accounts
                         join nd in _context.NguoiDungs on ac.IdNguoiDung equals nd.IdNguoiDung
                         select new { nd.IdNguoiDung, nd.HoTen, nd.GioiTinh, nd.NgaySinh, nd.DiaChi, nd.SDT, nd.Email, nd.AnhDaiDien, nd.TrangThai, ac.IdAccount, ac.UserName, ac.PassWord, ac.LoaiQuyen };
                        return await result.Select(x => new UserViewModel()
                         {
                            IdNguoiDung = x.IdNguoiDung,
                            HoTen = x.HoTen,
                            GioiTinh = x.GioiTinh,
                            NgaySinh = x.NgaySinh,
                            DiaChi = x.DiaChi,
                            SDT = x.SDT,
                            Email = x.Email,
                            AnhDaiDien = x.AnhDaiDien,
                            TrangThai = x.TrangThai,
                            IdAccount = x.IdAccount,
                            UserName = x.UserName,
                            PassWord = x.PassWord,
                            LoaiQuyen = x.LoaiQuyen

                        }).ToListAsync();

            //var query = from a in _context.Accounts
            //            join b in _context.Users on a.User_Id equals b.Id
            //            select new
            //            { a, b };
            //return await query.Select(x => new UserViewModel()
            //{
            //    User_Id = x.a.User_Id,
            //    UserName = x.a.UserName,
            //    PassWord = x.a.Password,
            //    StartDate = x.a.StartDate,
            //    EndDate = x.a.EndDate,
            //    Status_Account = x.a.Status,
            //    Role = x.a.Role,
            //    Name = x.b.Name,
            //    DateOfBirth = x.b.DateOfBirth,
            //    GioiTinh = x.b.GioiTinh,
            //    Image = x.b.Image,
            //    Address = x.b.Address,
            //    Phone = x.b.Phone,
            //    Email = x.b.Email,
            //    Status_User = x.b.Status,

            //}).ToListAsync();
        }

        public Task<int> Create(UserModel request)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int Id)
        {
            throw new NotImplementedException();
        }

        

        public Task<PagedResult<UserViewModel>> GetAllPaging(int pageindex, int pagesize, string UserName, string Name, string Role)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(UserModel request)
        {
            throw new NotImplementedException();
        }
    }
}
