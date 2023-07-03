

using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Data.Entities;

namespace BE_DATN.Application.BUS.Home
{
    public class TrangChu : ITrangChu
    {
        private readonly BEDATNDbContext _context;
        public TrangChu(BEDATNDbContext context)
        {
            _context = context;
        }
        public async Task<List<SanPhamModel>> GetAoTheThao()
        {
            
            var data = from sp in _context.SanPhams
                       join dm in _context.DanhMucs on sp.IdDanhMuc equals dm.IdDanhMuc
                       select new
                       {
                           IdSanPham = sp.IdSanPham,
                           IdDanhMuc = sp.IdDanhMuc,
                           IdDanhMucCha = dm.IdDanhMucCha,
                           TenSanPham = sp.TenSanPham,
                           MoTaSanPham = sp.MoTaSanPham,
                           AnhSanPham = sp.AnhSanPham,
                           XuatXu = sp.XuatXu,
                           ChatLieu = sp.ChatLieu,
                           IdThuongHieu = sp.IdThuongHieu,
                           NgayTao = sp.NgayTao,
                           TrangThai = sp.TrangThai
                       };
            var result= await data.Where(a => a.IdDanhMuc == 10 || a.IdDanhMucCha == 10).Select(x => new DTO.SanPhamModel()
            {
                IdSanPham = x.IdSanPham,
                IdDanhMuc = x.IdDanhMuc,
                TenSanPham = x.TenSanPham,
                MoTaSanPham = x.MoTaSanPham,
                AnhSanPham = x.AnhSanPham,
                XuatXu = x.XuatXu,
                ChatLieu = x.ChatLieu,
                IdThuongHieu = x.IdThuongHieu,
                NgayTao = x.NgayTao,
                TrangThai = x.TrangThai

            }).ToListAsync();

            return GetSanPhamListWithPrice(result);
        }

        

        public async Task<List<SanPhamModel>> GetPhuKienTheThao()
        {
            var data = from sp in _context.SanPhams
                       join dm in _context.DanhMucs on sp.IdDanhMuc equals dm.IdDanhMuc
                       select new
                       {
                           IdSanPham = sp.IdSanPham,
                           IdDanhMuc = sp.IdDanhMuc,
                           IdDanhMucCha = dm.IdDanhMucCha,
                           TenSanPham = sp.TenSanPham,
                           MoTaSanPham = sp.MoTaSanPham,
                           AnhSanPham = sp.AnhSanPham,
                           XuatXu = sp.XuatXu,
                           ChatLieu = sp.ChatLieu,
                           IdThuongHieu = sp.IdThuongHieu,
                           NgayTao = sp.NgayTao,
                           TrangThai = sp.TrangThai
                       };
            var result= await data.Where(a => a.IdDanhMuc == 7 || a.IdDanhMucCha == 7).Select(x => new DTO.SanPhamModel()
            {
                IdSanPham = x.IdSanPham,
                IdDanhMuc = x.IdDanhMuc,
                TenSanPham = x.TenSanPham,
                MoTaSanPham = x.MoTaSanPham,
                AnhSanPham = x.AnhSanPham,
                XuatXu = x.XuatXu,
                ChatLieu = x.ChatLieu,
                IdThuongHieu = x.IdThuongHieu,
                NgayTao = x.NgayTao,
                TrangThai = x.TrangThai

            }).ToListAsync();

            return GetSanPhamListWithPrice(result);
        }

        public async Task<List<SanPhamModel>> GetQuanTheThao()
        {
            var data = from sp in _context.SanPhams
                       join dm in _context.DanhMucs on sp.IdDanhMuc equals dm.IdDanhMuc
                       select new
                       {
                           IdSanPham = sp.IdSanPham,
                           IdDanhMuc = sp.IdDanhMuc,
                           IdDanhMucCha = dm.IdDanhMucCha,
                           TenSanPham = sp.TenSanPham,
                           MoTaSanPham = sp.MoTaSanPham,
                           AnhSanPham = sp.AnhSanPham,
                           XuatXu = sp.XuatXu,
                           ChatLieu = sp.ChatLieu,
                           IdThuongHieu = sp.IdThuongHieu,
                           NgayTao = sp.NgayTao,
                           TrangThai = sp.TrangThai
                       };
            var result= await data.Where(a => a.IdDanhMuc == 2 || a.IdDanhMucCha == 2).Select(x => new DTO.SanPhamModel()
            {
                IdSanPham = x.IdSanPham,
                IdDanhMuc = x.IdDanhMuc,
                TenSanPham = x.TenSanPham,
                MoTaSanPham = x.MoTaSanPham,
                AnhSanPham = x.AnhSanPham,
                XuatXu = x.XuatXu,
                ChatLieu = x.ChatLieu,
                IdThuongHieu = x.IdThuongHieu,
                NgayTao = x.NgayTao,
                TrangThai = x.TrangThai

            }).ToListAsync();

            return GetSanPhamListWithPrice(result);
        }

        public async Task<List<SanPhamModel>> GetSanPhamMoi()
        {
            var data = from sp in _context.SanPhams
                       orderby sp.NgayTao descending //sắp xếp sản phẩm theo ngày tạo giảm dần để lấy sản phẩm mới nhất
                       select new
                       {
                           IdSanPham = sp.IdSanPham,
                           IdDanhMuc = sp.IdDanhMuc,
                           TenSanPham = sp.TenSanPham,
                           MoTaSanPham = sp.MoTaSanPham,
                           AnhSanPham = sp.AnhSanPham,
                           XuatXu = sp.XuatXu,
                           ChatLieu = sp.ChatLieu,
                           IdThuongHieu = sp.IdThuongHieu,
                           NgayTao = sp.NgayTao,
                           TrangThai = sp.TrangThai
                       };
            var result= await data.Take(10).Select(x => new SanPhamModel()
            {
                IdSanPham = x.IdSanPham,
                IdDanhMuc = x.IdDanhMuc,
                TenSanPham = x.TenSanPham,
                MoTaSanPham = x.MoTaSanPham,
                AnhSanPham = x.AnhSanPham,
                XuatXu = x.XuatXu,
                ChatLieu = x.ChatLieu,
                IdThuongHieu = x.IdThuongHieu,
                NgayTao = x.NgayTao,
                TrangThai = x.TrangThai

            }).ToListAsync();
            

            return GetSanPhamListWithPrice(result);
        }




        public List<SanPhamModel> GetSanPhamListWithPrice(dynamic listsp)
        {
            var ketqua = new List<SanPhamModel>();

            foreach (var item in listsp)
            {
                int idsp;
                idsp = item.IdSanPham;
                dynamic min = null;
                dynamic max = null;
                var datasp = from msp in _context.MauSanPhams where msp.CheckThanhToan==1 select new { msp.GiaBan, msp.IdSanPham };
                var list_giasp = datasp.Select(a => new { a.GiaBan, a.IdSanPham }).Where(x => x.IdSanPham == idsp).ToArray();
                if (list_giasp.Length == 0)
                {
                    min = max = 0;
                    continue;
                }
                else if (list_giasp.Length == 1)
                {
                    min = max = list_giasp.Min(x => x.GiaBan);
                }
                else
                {
                    min = list_giasp.Min(x => x.GiaBan);
                    max = list_giasp.Max(x => x.GiaBan);
                }
                // Tạo đối tượng mới chứa các thuộc tính cần thiết
                var newItem = new SanPhamModel
                {
                    IdSanPham = item.IdSanPham,
                    TenSanPham = item.TenSanPham,
                    AnhSanPham = item.AnhSanPham,
                    minGiaSP = min,
                    maxGiaSP = max,
                    
                    
                };
                // Thêm đối tượng mới vào danh sách result
                ketqua.Add(newItem);

            }
            return ketqua;
        }



        public List<EntityDanhMuc> GetData()
        {
            var allDanhMuc = _context.DanhMucs.Where(a => a.TrangThai == 1).Select(a => new EntityDanhMuc { IdDanhMuc = a.IdDanhMuc, IdDanhMucCha = a.IdDanhMucCha, TenDanhMuc = a.TenDanhMuc }).ToList();
            var listDanhMucCha = allDanhMuc.Where(ds => ds.IdDanhMucCha == null).ToList();
            foreach (var dm in listDanhMucCha)
            {
                dm.DanhMucCon = GetListDanhMuc(allDanhMuc, dm);
            }
            return listDanhMucCha;
        }
        private List<EntityDanhMuc> GetListDanhMuc(List<EntityDanhMuc> listDM, EntityDanhMuc node)
        {
            var listDanhMucCon = listDM.Where(ds => ds.IdDanhMucCha == node.IdDanhMuc).ToList();
            if (listDanhMucCon.Count == 0)
                return null;
            for (int i = 0; i < listDanhMucCon.Count; i++)
            {
                var danhmuccon = GetListDanhMuc(listDM, listDanhMucCon[i]);
                listDanhMucCon[i].type = (danhmuccon == null || danhmuccon.Count == 0) ? "leaf" : "";
                listDanhMucCon[i].DanhMucCon = danhmuccon;
            }
            return listDanhMucCon.ToList();
        }


    }
}
