using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BE_DATN.Data.Entities;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageHoaDonNhap : IManageHoaDonNhap
    {
        private readonly BEDATNDbContext _context;
        public ManageHoaDonNhap(BEDATNDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateHoaDonNhap([FromBody] DataNhapHoaDonNhap model)
        {
            HoaDonNhap hdn=new HoaDonNhap();
            hdn.IdNhaCungCap = model.hoadonnhap.IdNhaCungCap;
            hdn.IdNguoiDung = model.hoadonnhap.IdNguoiDung;
            hdn.GhiChu = model.hoadonnhap.GhiChu;
            hdn.TongTien = model.hoadonnhap.TongTien;
            hdn.NgayNhap = System.DateTime.Now;
            hdn.TrangThaiHoaDonNhap = model.hoadonnhap.TrangThaiHoaDonNhap;

            _context.HoaDonNhaps.Add(hdn);
            await _context.SaveChangesAsync();

            int IdHoaDonNhap = hdn.IdHoaDonNhap;

            


            foreach (var item in model.sizesanpham)
            {
                var sizesp = await _context.SizeSanPhams
                            .Where(s => s.IdMauSanPham == item.IdMauSanPham && s.Size == item.Size).FirstOrDefaultAsync();

                if (sizesp!=null)
                {
                    
                    sizesp.SoLuong = sizesp.SoLuong+item.SoLuong;
                    sizesp.TrangThai = item.TrangThai;
                    await _context.SaveChangesAsync();

                    var kho = await _context.Khos
                            .Where(s => s.IdSizeSanPham == sizesp.IdSizeSanPham).FirstOrDefaultAsync();
                    kho.IdSanPham = sizesp.IdSanPham;
                    kho.IdMauSanPham = sizesp.IdMauSanPham;
                    kho.IdSizeSanPham=sizesp.IdSizeSanPham;
                    kho.SoLuong = sizesp.SoLuong;
                    await _context.SaveChangesAsync();



                    //lưu chi tiết hóa đơn nhập
                    ChiTietHoaDonNhap cthdn=new ChiTietHoaDonNhap();
                    cthdn.IdHoaDonNhap = IdHoaDonNhap;
                    cthdn.IdSanPham = item.IdSanPham;
                    cthdn.IdMauSanPham = item.IdMauSanPham;
                    cthdn.IdSizeSanPham=sizesp.IdSizeSanPham;
                    cthdn.SoLuong = item.SoLuong;
                    cthdn.GiaNhap = model.GiaNhap;
                    _context.ChiTietHoaDonNhaps.Add(cthdn);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    
                    _context.SizeSanPhams.Add(item);
                    await _context.SaveChangesAsync();

                    int IdSizeSanPhamMoiThem = await _context.SizeSanPhams.MaxAsync(ssp => ssp.IdSizeSanPham);

                    // Thêm vào kho
                    var kho = new Kho
                    {
                        IdSanPham = item.IdSanPham,
                        IdMauSanPham = item.IdMauSanPham,
                        IdSizeSanPham = IdSizeSanPhamMoiThem,
                        SoLuong = item.SoLuong
                    };
                    _context.Khos.Add(kho);
                    await _context.SaveChangesAsync();




                    //lưu chi tiết hóa đơn nhập
                    ChiTietHoaDonNhap cthdn = new ChiTietHoaDonNhap();
                    cthdn.IdHoaDonNhap = IdHoaDonNhap;
                    cthdn.IdSanPham = item.IdSanPham;
                    cthdn.IdMauSanPham = item.IdMauSanPham;
                    cthdn.IdSizeSanPham = item.IdSizeSanPham;
                    cthdn.SoLuong = item.SoLuong;
                    cthdn.GiaNhap = model.GiaNhap;

                    _context.ChiTietHoaDonNhaps.Add(cthdn);
                    await _context.SaveChangesAsync();
                }


                var mausp = await _context.MauSanPhams
                      .Where(x => x.IdMauSanPham == item.IdMauSanPham).FirstOrDefaultAsync();
                mausp.CheckThanhToan = 1;
                await _context.SaveChangesAsync();
            }


            //List<MauSanPhamModel> listmsp = await GetListMauSanPhamChuaThanhToan(model.IdSanPham);

            //if (listmsp.Count > 0)
            //{
            //    foreach (var itemmsp in listmsp)
            //    {

            //        int idmausanpham = itemmsp.IdMauSanPham;
            //        List<ChiTietHoaDonNhap> listsizesp = await GetListSizeMauSanPham2(idmausanpham);

            //        foreach(var item in listsizesp)
            //        {
            //            item.IdHoaDonNhap = IdHoaDonNhap;
            //            _context.ChiTietHoaDonNhaps.Add(item);
            //            await _context.SaveChangesAsync();
            //        }


            //    }

            //}
            return 1;
        }

        public async Task<HoaDonNhapModel> GetById(int Id)
        {
            var data=from hdn in _context.HoaDonNhaps

            join ncc in _context.NhaCungCaps on hdn.IdNhaCungCap equals ncc.IdNhaCungCap
            join nd in _context.NguoiDungs on hdn.IdNguoiDung equals nd.IdNguoiDung
                     where hdn.IdHoaDonNhap == Id
            select new HoaDonNhapModel
            {

                IdHoaDonNhap = hdn.IdHoaDonNhap,

                TenNhaCungCap = ncc.TenNhaCungCap,
                TenNguoiDung = nd.HoTen,
                GhiChu = hdn.GhiChu,
                TongTien = hdn.TongTien,
                NgayNhap = hdn.NgayNhap,
                NgayCapNhat = hdn.NgayCapNhat,

                TrangThaiHoaDonNhap = hdn.TrangThaiHoaDonNhap
            };
           
            return await data.FirstOrDefaultAsync();
        }

        public async Task<List<ChiTietHoaDonNhapModel>> GetChiTietHoaDonNhap(int IdHoaDonNhap)
        {
            var data = from cthdn in _context.ChiTietHoaDonNhaps
                       join sp in _context.SanPhams on cthdn.IdSanPham equals sp.IdSanPham
                       join msp in _context.MauSanPhams on cthdn.IdMauSanPham equals msp.IdMauSanPham
                       join ssp in _context.SizeSanPhams on cthdn.IdSizeSanPham equals ssp.IdSizeSanPham

                       select new { 
                           cthdn.IdSanPham,cthdn.IdHoaDonNhap,cthdn.IdMauSanPham,
                           cthdn.IdSizeSanPham,cthdn.SoLuong,cthdn.GiaNhap,
                           sp.TenSanPham,ssp.Size,msp.TenMau
                       };
            return await data.Where(a => a.IdHoaDonNhap == IdHoaDonNhap).Select(x => new ChiTietHoaDonNhapModel()
            {
                IdHoaDonNhap = x.IdHoaDonNhap,
                IdSanPham = x.IdSanPham,
                IdMauSanPham = x.IdMauSanPham,
                IdSizeSanPham= x.IdSizeSanPham,
                SoLuong=x.SoLuong,
                GiaNhap=x.GiaNhap,
                TenMau=x.TenMau,
                TenSanPham=x.TenSanPham,
                Size=x.Size

            }).ToListAsync();
        }

        public async Task<List<MauSanPhamModel>> GetListMauSanPhamChuaThanhToan(int IdSanPham)
        {
            var data = from msp in _context.MauSanPhams
                         join sp in _context.SanPhams on msp.IdSanPham equals sp.IdSanPham
                         select new
                         {
                             IdSanPham = msp.IdSanPham,
                             
                             TenSanPham = sp.TenSanPham,
                             IdMauSanPham=msp.IdMauSanPham,
                             AnhSanPham = msp.AnhSanPham,
                             TenMau=msp.TenMau,
                             MaMau=msp.MaMau,
                             GiaNhap=msp.GiaNhap,
                             GiaBan=msp.GiaBan,
                             CheckThanhToan = msp.CheckThanhToan,
                             TrangThai = msp.TrangThai,

                         };

            return await data.Where(x => x.TrangThai == 0&&x.IdSanPham==IdSanPham).Select(a => new MauSanPhamModel()
            {
                IdSanPham = a.IdSanPham,
                TenSanPham = a.TenSanPham,
                IdMauSanPham = a.IdMauSanPham,
                AnhSanPham = a.AnhSanPham,
                TenMau = a.TenMau,
                MaMau = a.MaMau,
                GiaNhap = a.GiaNhap,
                GiaBan = a.GiaBan,
                TrangThai=a.TrangThai,
                CheckThanhToan = a.CheckThanhToan
            }).ToListAsync();
            
        }

        //public async Task<List<DanhSachSanPhamNhap>> GetListSanPhamNhap(int IdSanPham)
        //{
        //    List<MauSanPhamModel> listmsp = await GetListMauSanPhamChuaThanhToan(IdSanPham);

        //    List<DanhSachSanPhamNhap> result = new List<DanhSachSanPhamNhap>();
        //    if (listmsp.Count > 0)
        //    {
        //        foreach (var itemmsp in listmsp)
        //        {

        //            int idmausanpham = itemmsp.IdMauSanPham;
        //            List<ChiTietHoaDonNhapModel> listsizesp = await GetListSizeMauSanPham(idmausanpham);

        //            foreach (var item in listsizesp)
        //            {
        //                DanhSachSanPhamNhap listsanphamnhap = new DanhSachSanPhamNhap();
        //                listsanphamnhap.TenMau = item.TenMau;
        //                listsanphamnhap.Size = item.Size;
        //                listsanphamnhap.SoLuong = item.SoLuong;

                        
        //                result.Add(listsanphamnhap);
        //            }
                    
        //        }

        //    }

        //    return  result;

        //}

        public async Task<List<ChiTietHoaDonNhapModel>> GetListSizeMauSanPham(int IdMauSanPham)
        {
            var data = from s in _context.SizeSanPhams
                       join msp in _context.MauSanPhams on s.IdMauSanPham equals msp.IdMauSanPham
                       select new
                       {
                           IdSizeSanPham = s.IdSizeSanPham,
                           IdSanPham=s.IdSanPham,
                           IdMauSanPham=s.IdMauSanPham,
                           Size = s.Size,
                           SoLuong=s.SoLuong,
                           TrangThai = s.TrangThai,
                           GiaNhap=msp.GiaNhap,
                           TenMau=msp.TenMau

                       };

            return await data.Where(x =>x.IdMauSanPham == IdMauSanPham).Select(a => new ChiTietHoaDonNhapModel()
            {
                IdSizeSanPham = a.IdSizeSanPham,
                IdSanPham = a.IdSanPham,
                IdMauSanPham = a.IdMauSanPham,
                TenMau=a.TenMau,
                SoLuong = a.SoLuong,
                Size = a.Size,
                GiaNhap = a.GiaNhap,
            }).ToListAsync();
        }

        //public async Task<List<ChiTietHoaDonNhap>> GetListSizeMauSanPham2(int IdMauSanPham)
        //{
        //    var data = from s in _context.SizeSanPhams
        //               join msp in _context.MauSanPhams on s.IdMauSanPham equals msp.IdMauSanPham
        //               select new
        //               {
        //                   IdSizeSanPham = s.IdSizeSanPham,
        //                   IdSanPham = s.IdSanPham,
        //                   IdMauSanPham = s.IdMauSanPham,
        //                   Size = s.Size,
        //                   SoLuong = s.SoLuong,
        //                   TrangThai = s.TrangThai,
        //                   GiaNhap = msp.GiaNhap,
        //                   TenMau = msp.TenMau

        //               };

        //    return await data.Where(x => x.IdMauSanPham == IdMauSanPham).Select(a => new ChiTietHoaDonNhap()
        //    {
        //        IdSizeSanPham = a.IdSizeSanPham,
        //        IdSanPham = a.IdSanPham,
        //        IdMauSanPham = a.IdMauSanPham,
        //        SoLuong = a.SoLuong,
        //        GiaNhap = a.GiaNhap,
        //    }).ToListAsync();
        //}

        public async Task<PagedResult<HoaDonNhapModel>> SearchHoaDonNhapPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from hdn in _context.HoaDonNhaps
                         
                         join ncc in _context.NhaCungCaps on hdn.IdNhaCungCap equals ncc.IdNhaCungCap
                         join nd in _context.NguoiDungs on hdn.IdNguoiDung equals nd.IdNguoiDung

                         select new
                         {
                           
                             IdHoaDonNhap=hdn.IdHoaDonNhap,
                             
                             TenNhaCungCap = ncc.TenNhaCungCap,
                             TenNguoiDung = nd.HoTen,
                             GhiChu = hdn.GhiChu,
                             TongTien = hdn.TongTien,
                             NgayNhap = hdn.NgayNhap,
                             NgayCapNhat = hdn.NgayCapNhat,
                          
                             TrangThaiHoaDonNhap = hdn.TrangThaiHoaDonNhap
                         };

            // Lọc các sản phẩm theo tên
            result = result.Where(x => x.TenNhaCungCap.Contains(ten)||x.TenNguoiDung.Contains(ten));

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new HoaDonNhapModel()
            {
                IdHoaDonNhap=x.IdHoaDonNhap,
              
                TenNhaCungCap = x.TenNhaCungCap,
                TenNguoiDung = x.TenNguoiDung,
                GhiChu = x.GhiChu,
                TongTien = x.TongTien,
                NgayNhap = x.NgayNhap,
                NgayCapNhat = x.NgayCapNhat,

                TrangThaiHoaDonNhap = x.TrangThaiHoaDonNhap

            }).ToListAsync();

            var pageResult = new PagedResult<HoaDonNhapModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        public async Task<int> Update(HoaDonNhapModel sp)
        {
            var hoadonnhap = await _context.HoaDonNhaps.FindAsync(sp.hoadonnhap.IdHoaDonNhap);

            hoadonnhap.TrangThaiHoaDonNhap = sp.hoadonnhap.TrangThaiHoaDonNhap;
           
            await _context.SaveChangesAsync();

            return 1;
        }
    }
}
