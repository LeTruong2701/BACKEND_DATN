using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageDonHang : IManageDonHang
    {
        private readonly BEDATNDbContext _context;
        public ManageDonHang(BEDATNDbContext context)
        {
            _context = context;
        }
      

        public async Task<List<ChiTietDonHangModel>> GetChiTietDonHangByIdDonHang(int Id)
        {
            var result = from ct in _context.ChiTietDonHangs
                         join dh in _context.DonHangs on ct.IdDonHang equals dh.IdDonHang
                         join msp in _context.MauSanPhams on ct.IdMauSanPham equals msp.IdMauSanPham
                         join ssp in _context.SizeSanPhams on ct.IdSizeSanPham equals ssp.IdSizeSanPham
                         join sp in _context.SanPhams on ct.IdSanPham equals sp.IdSanPham
                         select new { 
                             IdChiTietDonHang = ct.IdChiTietDonHang,
                             TenSanPham=sp.TenSanPham,
                             IdDonHang = ct.IdDonHang,
                             IdSanPham = ct.IdSanPham, 
                             IdMauSanPham = ct.IdMauSanPham,
                             MaMau=msp.MaMau,
                             IdSizeSanPham = ct.IdSizeSanPham,
                             Size=ssp.Size,
                             AnhSanPham = msp.AnhSanPham,
                             SoLuong=ct.SoLuong,
                             GiaMua=ct.GiaMua
                         };


            var kq = await result.Where(x => x.IdDonHang == Id).Select(x => new ChiTietDonHangModel()
            {
                IdDonHang = x.IdDonHang,
                IdChiTietDonHang = x.IdChiTietDonHang,
                IdSanPham = x.IdSanPham,
                TenSanPham=x.TenSanPham,
                IdMauSanPham = x.IdMauSanPham,
                MaMau=x.MaMau,
                IdSizeSanPham = x.IdSizeSanPham,
                Size=x.Size,
                AnhSanPham = x.AnhSanPham,
                SoLuong = x.SoLuong,
                GiaMua = x.GiaMua,
            }).ToListAsync();
            return kq;
        }

        public Task<List<DonHang>> GetKhachHang()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<DonHangModel>> SearchDanhMucPaging([FromBody] Dictionary<string, object> formData)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<DonHangModel>> SearchDonHangPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from dh in _context.DonHangs
                       
                         select new { IdDonHang = dh.IdDonHang, IdKhachHang = dh.IdKhachHang, TenKhachHang = dh.TenKhachHang,GhiChu=dh.GhiChu,
                             SDT = dh.SDT, DiaChi = dh.DiaChi ,TrangThaiDonHang=dh.TrangThaiDonHang,DiaChiGiaoHang=dh.DiaChiGiaoHang,
                             NgayDat=dh.NgayDat,TrangThaiThanhToan=dh.TrangThaiThanhToan,TongTien=dh.TongTien,MaKhuyenMai=dh.MaKhuyenMai,
                         PhuongThucThanhToan=dh.PhuongThucThanhToan};

            // Lọc các item theo tên
            result = result.Where(x => x.TenKhachHang.Contains(ten) || x.SDT.Contains(ten) || x.DiaChiGiaoHang.Contains(ten));

            // Lấy tổng số item thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new DonHangModel()
            {
                IdDonHang = x.IdDonHang,
                IdKhachHang = x.IdKhachHang,
                TenKhachHang= x.TenKhachHang,
                GhiChu = x.GhiChu,
                SDT = x.SDT,
                DiaChi = x.DiaChi,
                TrangThaiDonHang = x.TrangThaiDonHang,
                DiaChiGiaoHang = x.DiaChiGiaoHang,
                NgayDat = x.NgayDat,
                TrangThaiThanhToan=x.TrangThaiThanhToan,
                TongTien=x.TongTien,
                MaKhuyenMai=x.MaKhuyenMai,
                PhuongThucThanhToan=x.PhuongThucThanhToan
                
              

            }).ToListAsync();

            var pageResult = new PagedResult<DonHangModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }


        public async Task<int> Update(DonHangDTO th)
        {
            int check = 0;
            var donhang = await _context.DonHangs.FindAsync(th.IdDonHang);
            if(th.TrangThaiDonHang=="Đã xác nhận"|| th.TrangThaiDonHang=="Đang vận chuyển")
            {
                if (th.TrangThaiDonHang=="Đã xác nhận")
                {
                    var ctdh = await _context.ChiTietDonHangs.Where(x => x.IdDonHang == th.IdDonHang).ToListAsync();
                    // trừ đi các sản phẩm có trong kho do khách hàng đã nhận hàng
                    foreach (var item in ctdh)
                    {


                        var kho = _context.Khos.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham == item.IdMauSanPham && x.IdSizeSanPham == item.IdSizeSanPham);
                        kho.SoLuong = kho.SoLuong - item.SoLuong;

                        var size = _context.SizeSanPhams.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham == item.IdMauSanPham && x.IdSizeSanPham == item.IdSizeSanPham);
                        size.SoLuong = size.SoLuong - item.SoLuong;
                    }
                    donhang.TrangThaiDonHang = th.TrangThaiDonHang;
                    donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
                    await _context.SaveChangesAsync();

                    check = 1;
                }
                else if(donhang.TrangThaiDonHang=="Đã xác nhận" && th.TrangThaiDonHang=="Đang vận chuyển")
                {
                    donhang.TrangThaiDonHang = th.TrangThaiDonHang;
                    donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
                    await _context.SaveChangesAsync();
                    check = 1;
                }
            }else if (th.TrangThaiDonHang=="Hoàn về")
            {
                var ctdh = await _context.ChiTietDonHangs.Where(x => x.IdDonHang == th.IdDonHang).ToListAsync();
                // cộng các sản phẩm có trong ctdh vào kho và sizesp
                foreach (var item in ctdh)
                {

                    var kho = _context.Khos.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham == item.IdMauSanPham && x.IdSizeSanPham == item.IdSizeSanPham);
                    kho.SoLuong = kho.SoLuong + item.SoLuong;

                    var size = _context.SizeSanPhams.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham == item.IdMauSanPham && x.IdSizeSanPham == item.IdSizeSanPham);
                    size.SoLuong = size.SoLuong + item.SoLuong;
                }
                donhang.TrangThaiDonHang = th.TrangThaiDonHang;
                donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
                await _context.SaveChangesAsync();

                check = 1;
            }else if (th.TrangThaiDonHang=="Đã hủy")
            {
                if (donhang.TrangThaiDonHang=="Đã xác nhận"|| donhang.TrangThaiDonHang == "Đang vận chuyển")
                {
                    var ctdh = await _context.ChiTietDonHangs.Where(x => x.IdDonHang == th.IdDonHang).ToListAsync();
                    // cộng các sản phẩm có trong ctdh vào kho và sizesp
                    foreach (var item in ctdh)
                    {

                        var kho = _context.Khos.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham == item.IdMauSanPham && x.IdSizeSanPham == item.IdSizeSanPham);
                        kho.SoLuong = kho.SoLuong + item.SoLuong;

                        var size = _context.SizeSanPhams.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham == item.IdMauSanPham && x.IdSizeSanPham == item.IdSizeSanPham);
                        size.SoLuong = size.SoLuong + item.SoLuong;
                    }
                    donhang.TrangThaiDonHang = th.TrangThaiDonHang;
                    donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
                    await _context.SaveChangesAsync();

                    check = 1;
                }else if (donhang.TrangThaiDonHang=="Chờ xác nhận"||donhang.TrangThaiDonHang=="Hoàn về")
                {
                    donhang.TrangThaiDonHang = th.TrangThaiDonHang;
                    donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
                    await _context.SaveChangesAsync();
                    check = 1;
                }
            }else if (th.TrangThaiDonHang=="Chờ xác nhận")
            {
                if (donhang.TrangThaiDonHang=="Hoàn về"||donhang.TrangThaiDonHang=="Đã hủy")
                {
                    donhang.TrangThaiDonHang = th.TrangThaiDonHang;
                    donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
                    await _context.SaveChangesAsync();
                }
                check = 1;

            }
            else
            {
                donhang.NgayNhanHang = System.DateTime.Now;
                donhang.TrangThaiDonHang = th.TrangThaiDonHang;
                donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
                await _context.SaveChangesAsync();
                check = 1;
            }
            
            return check;

        }



        //public async Task<int> Update(DonHangDTO th)
        //{
        //    var donhang = await _context.DonHangs.FindAsync(th.IdDonHang);
        //    if (th.TrangThaiDonHang=="Giao hàng thành công")
        //    {
        //        donhang.NgayNhanHang = System.DateTime.Now;

        //        var ctdh = await _context.ChiTietDonHangs.Where(x => x.IdDonHang == th.IdDonHang).ToListAsync();
        //        // trừ đi các sản phẩm có trong kho do khách hàng đã nhận hàng
        //        foreach (var item in ctdh)
        //        {


        //            var kho = _context.Khos.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham==item.IdMauSanPham&& x.IdSizeSanPham==item.IdSizeSanPham);
        //            kho.SoLuong = kho.SoLuong - item.SoLuong;

        //            var size= _context.SizeSanPhams.SingleOrDefault(x => x.IdSanPham == item.IdSanPham && x.IdMauSanPham == item.IdMauSanPham && x.IdSizeSanPham == item.IdSizeSanPham);
        //            size.SoLuong = size.SoLuong - item.SoLuong;
        //        }
        //        donhang.TrangThaiDonHang = th.TrangThaiDonHang;
        //        donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        donhang.TrangThaiDonHang = th.TrangThaiDonHang;
        //        donhang.TrangThaiThanhToan = th.TrangThaiThanhToan;

        //        await _context.SaveChangesAsync();
        //    }


        //    return 1;
        //}

        public async Task<DonHang> GetById(int Id)
        {
            var donhang = await _context.DonHangs.FindAsync(Id);

            return donhang;
        }

        public async Task<int> Delete(int Id)
        {
            var chitietdonhangs = await _context.ChiTietDonHangs.Where(c => c.IdDonHang == Id).ToListAsync();

            foreach (var chitiet in chitietdonhangs)
            {
                _context.ChiTietDonHangs.Remove(chitiet);
            }

            await _context.SaveChangesAsync();

            var hoadon= await _context.DonHangs.FindAsync(Id);

            _context.DonHangs.Remove(hoadon);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> CapNhatKhoSauKhiBanHang(KhoSizeModel model)
        {
            if (model.ctdh.Count > 0)
            {
                foreach (var item in model.ctdh)
                {
                    var obj = _context.Khos.SingleOrDefault(x => x.IdSizeSanPham == item.IdSizeSanPham);
                    obj.SoLuong = obj.SoLuong - item.SoLuong;

                    var size = _context.SizeSanPhams.SingleOrDefault(y => y.IdSizeSanPham == item.IdSizeSanPham);
                    size.SoLuong = size.SoLuong - item.SoLuong;
                }
                await _context.SaveChangesAsync();
                return 1;
            }
            return await Task.FromResult(1);
        }

        public async Task<PagedResult<DonHangModel>> SearchDonHangPagingTheoNgay([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";
            var result = from dh in _context.DonHangs

                         select new
                         {
                             IdDonHang = dh.IdDonHang,
                             IdKhachHang = dh.IdKhachHang,
                             TenKhachHang = dh.TenKhachHang,
                             GhiChu = dh.GhiChu,
                             SDT = dh.SDT,
                             DiaChi = dh.DiaChi,
                             TrangThaiDonHang = dh.TrangThaiDonHang,
                             DiaChiGiaoHang = dh.DiaChiGiaoHang,
                             NgayDat = dh.NgayDat,
                             TrangThaiThanhToan = dh.TrangThaiThanhToan,
                             TongTien = dh.TongTien,
                             MaKhuyenMai = dh.MaKhuyenMai,
                             PhuongThucThanhToan = dh.PhuongThucThanhToan
                         };
            if (DateTime.TryParseExact(ten, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayDat))
            {
                // Lọc kết quả theo NgayDat
                result = result.Where(x => x.NgayDat.Date == ngayDat.Date);
            }
            // Lọc các item theo tên


            // Lấy tổng số item thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new DonHangModel()
            {
                IdDonHang = x.IdDonHang,
                IdKhachHang = x.IdKhachHang,
                TenKhachHang = x.TenKhachHang,
                GhiChu = x.GhiChu,
                SDT = x.SDT,
                DiaChi = x.DiaChi,
                TrangThaiDonHang = x.TrangThaiDonHang,
                DiaChiGiaoHang = x.DiaChiGiaoHang,
                NgayDat = x.NgayDat,
                TrangThaiThanhToan = x.TrangThaiThanhToan,
                TongTien = x.TongTien,
                MaKhuyenMai = x.MaKhuyenMai,
                PhuongThucThanhToan = x.PhuongThucThanhToan



            }).ToListAsync();

            var pageResult = new PagedResult<DonHangModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }
    }
}
