using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BE_DATN.Application.BUS.Home
{
    public class SanPhamManage : ISanPham
    {
        private readonly BEDATNDbContext _context;
        public SanPhamManage(BEDATNDbContext context)
        {
            _context = context;
        }

       
        public async Task<SanPhamTheoMauModel> GetSanPhamMauByIdSanPham(int Id)
        {
            var sanpham = await (from sp in _context.SanPhams
                                 join msp in _context.MauSanPhams on sp.IdSanPham equals msp.IdSanPham
                                 where sp.IdSanPham == Id
                                 select new SanPhamTheoMauModel
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
                                     TrangThai = sp.TrangThai,
                                     IdMauSanPham=msp.IdMauSanPham,
                                     MaMau=msp.MaMau,
                                     TenMau=msp.TenMau,
                                     GiaBan=msp.GiaBan,
                                     
                                 }).FirstOrDefaultAsync();
            return sanpham;
        }

        public async Task<SanPhamTheoMauModel> GetSanPhamTheoMauByIdMauSanPham(int Id)
        {
            var sanpham = await(from sp in _context.SanPhams
                                join msp in _context.MauSanPhams on sp.IdSanPham equals msp.IdSanPham
                                where msp.IdMauSanPham == Id
                                select new SanPhamTheoMauModel
                                {
                                    IdSanPham = sp.IdSanPham,
                                    IdDanhMuc = sp.IdDanhMuc,
                                    TenSanPham = sp.TenSanPham,
                                    MoTaSanPham = sp.MoTaSanPham,
                                    AnhSanPham = msp.AnhSanPham,
                                    XuatXu = sp.XuatXu,
                                    ChatLieu = sp.ChatLieu,
                                    IdThuongHieu = sp.IdThuongHieu,
                                    NgayTao = sp.NgayTao,
                                    TrangThai = sp.TrangThai,
                                    IdMauSanPham = msp.IdMauSanPham,
                                    MaMau = msp.MaMau,
                                    TenMau = msp.TenMau,
                                    GiaBan = msp.GiaBan,

                                }).FirstOrDefaultAsync();
            return sanpham;
        }

        //lấy list màu của sản phẩm
        public async Task<List<SanPhamTheoMauModel>> GetListMauCuaSanPham(int Id)
        {
            var listmau = await(from sp in _context.SanPhams
                                join msp in _context.MauSanPhams on sp.IdSanPham equals msp.IdSanPham
                                where msp.IdSanPham == Id
                                select new SanPhamTheoMauModel
                                {
                                    IdSanPham = sp.IdSanPham,
                                    IdDanhMuc = sp.IdDanhMuc,
                                    TenSanPham = sp.TenSanPham,
                                    MoTaSanPham = sp.MoTaSanPham,
                                    AnhSanPham = msp.AnhSanPham,
                                    XuatXu = sp.XuatXu,
                                    ChatLieu = sp.ChatLieu,
                                    IdThuongHieu = sp.IdThuongHieu,
                                    NgayTao = sp.NgayTao,
                                    TrangThai = sp.TrangThai,
                                    IdMauSanPham = msp.IdMauSanPham,
                                    MaMau = msp.MaMau,
                                    TenMau = msp.TenMau,
                                    GiaBan = msp.GiaBan,

                                }).ToListAsync();
            return listmau;
        }


        //SIZE

        public async Task<List<SizeSanPhamModel>> GetListSizeByIdMauSanPham(int Id)
        {
            var listsize = await(from msp in _context.MauSanPhams
                                join ssp in _context.SizeSanPhams on msp.IdMauSanPham equals ssp.IdMauSanPham
                                where msp.IdMauSanPham == Id
                                select new SizeSanPhamModel
                                {
                                    IdSanPham = msp.IdSanPham,
                                   
                                    IdMauSanPham = msp.IdMauSanPham,
                                    MaMau = msp.MaMau,
                                    TenMau = msp.TenMau,
                                    IdSizeSanPham=ssp.IdSizeSanPham,
                                    Size=ssp.Size,
                                    SoLuong=ssp.SoLuong,

                                }).ToListAsync();
            return listsize;
        }

        public async Task<SizeSanPhamModel> GetSizeSanPhamDauTienByIdMauSanPham(int Id)
        {
            var listsize = await (from msp in _context.MauSanPhams
                                  join ssp in _context.SizeSanPhams on msp.IdMauSanPham equals ssp.IdMauSanPham
                                  where msp.IdMauSanPham == Id
                                  select new SizeSanPhamModel
                                  {
                                      IdSanPham = msp.IdSanPham,

                                      IdMauSanPham = msp.IdMauSanPham,
                                      MaMau = msp.MaMau,
                                      TenMau = msp.TenMau,
                                      IdSizeSanPham = ssp.IdSizeSanPham,
                                      Size = ssp.Size,
                                      SoLuong = ssp.SoLuong

                                  }).FirstOrDefaultAsync();
            return listsize;

        }

        public async Task<SizeSanPhamModel> GetSizeSanPhamByIdSizeSanPham(int Id)
        {
            var size = await(from ssp in _context.SizeSanPhams
                             join msp in _context.MauSanPhams on ssp.IdMauSanPham equals msp.IdMauSanPham
                                 where ssp.IdSizeSanPham == Id
                                 select new SizeSanPhamModel
                                 {
                                     IdSanPham = msp.IdSanPham,
                                     IdMauSanPham = msp.IdMauSanPham,
                                     MaMau = msp.MaMau,
                                     TenMau = msp.TenMau,
                                     IdSizeSanPham = ssp.IdSizeSanPham,
                                     Size = ssp.Size,
                                     SoLuong = ssp.SoLuong

                                 }).FirstOrDefaultAsync();
            return size;
        }

        public async Task<SanPhamModel> GetSanPhamByIdSanPham(int Id)
        {
            var datasp = from msp in _context.MauSanPhams
                         where msp.IdSanPham == Id
                         select new { msp.GiaBan };

            var minGiaSP = datasp.Min(x => x.GiaBan);
            var maxGiaSP = datasp.Max(x => x.GiaBan);

            var sanPham = _context.SanPhams.FirstOrDefault(sp => sp.IdSanPham == Id);
            var thuonghieu = _context.ThuongHieus.FirstOrDefault(th => th.IdThuongHieu == sanPham.IdThuongHieu);
            

            if (sanPham != null)
            {
                var sanPhamModel = new SanPhamModel
                {
                    IdSanPham = sanPham.IdSanPham,
                    TenSanPham = sanPham.TenSanPham,
                    AnhSanPham = sanPham.AnhSanPham,
                    minGiaSP = minGiaSP,
                    maxGiaSP = maxGiaSP,
                    XuatXu=sanPham.XuatXu,
                    ChatLieu=sanPham.ChatLieu,
                    IdDanhMuc=sanPham.IdDanhMuc,
                    TenThuongHieu=thuonghieu.TenThuongHieu
                };

                return sanPhamModel;
            }

            return null;
        }

        public SanPhamModel GetSanPhamWithPrice(int idSanPham)
        {
            var datasp = from msp in _context.MauSanPhams
                         where msp.IdSanPham == idSanPham
                         select new { msp.GiaBan };

            var minGiaSP = datasp.Min(x => x.GiaBan);
            var maxGiaSP = datasp.Max(x => x.GiaBan);

            var sanPham = _context.SanPhams.FirstOrDefault(sp => sp.IdSanPham == idSanPham);

            if (sanPham != null)
            {
                var sanPhamModel = new SanPhamModel
                {
                    IdSanPham = sanPham.IdSanPham,
                    TenSanPham = sanPham.TenSanPham,
                    AnhSanPham = sanPham.AnhSanPham,
                    minGiaSP = minGiaSP,
                    maxGiaSP = maxGiaSP
                };

                return sanPhamModel;
            }

            return null;
        }

        public async Task<List<SanPhamModel>> GetSanPhamCungLoai(int Id)
        {
            var data = from sp in _context.SanPhams
                       where sp.IdDanhMuc == Id
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
            var result = await data.OrderBy(x => Guid.NewGuid()).Take(4).Select(x => new SanPhamModel()
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
                var datasp = from msp in _context.MauSanPhams select new { msp.GiaBan, msp.IdSanPham };
                var list_giasp = datasp.Select(a => new { a.GiaBan, a.IdSanPham }).Where(x => x.IdSanPham == idsp).ToArray();
                if (list_giasp.Length == 0)
                {
                    min = max = 0;
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
                    maxGiaSP = max
                };
                // Thêm đối tượng mới vào danh sách result
                ketqua.Add(newItem);

            }
            return ketqua;
        }





    }
}
