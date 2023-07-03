using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageAccount : IManageAccount
    {
        private readonly BEDATNDbContext _context;

        public ManageAccount(BEDATNDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(AccountModel ac)
        {
            _context.Accounts.Add(ac.account);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(int Id)
        {
            var account = await _context.Accounts.FindAsync(Id);

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return 1;
        }

        public Task<List<Account>> GetAccount()
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetById(int Id)
        {
            var account = await _context.Accounts.FindAsync(Id);

            return account;
        }

        public async Task<PagedResult<Account>> SearchDanhMucPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from kh in _context.Accounts
                         select new { IdAccount = kh.IdAccount, IdNguoiDung = kh.IdNguoiDung, IdKhachHang = kh.IdKhachHang, UserName = kh.UserName, PassWord = kh.PassWord, LoaiQuyen = kh.LoaiQuyen, TrangThai = kh.TrangThai };
            var kq = await result.Where(x => x.UserName.Contains(ten)).Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new Account()
            {
                IdAccount = x.IdAccount,
                IdNguoiDung = x.IdNguoiDung,
                IdKhachHang = x.IdKhachHang,
                UserName = x.UserName,
                PassWord = x.PassWord,
                LoaiQuyen = x.LoaiQuyen,
                TrangThai = x.TrangThai

            }).ToListAsync();

            var pageResult = new PagedResult<Account>()
            {
                totalItem = kq.Count(),
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<int> Update(AccountModel ac)
        {
            var account = await _context.Accounts.FindAsync(ac.account.IdAccount);

            account.IdAccount = ac.account.IdAccount;
            account.IdNguoiDung = ac.account.IdNguoiDung;
            account.IdKhachHang = ac.account.IdKhachHang;
            account.UserName = ac.account.UserName;
            account.PassWord = ac.account.PassWord;
            account.LoaiQuyen = ac.account.LoaiQuyen;
            account.TrangThai = ac.account.TrangThai;

            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
