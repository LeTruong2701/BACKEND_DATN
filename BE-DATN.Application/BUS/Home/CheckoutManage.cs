using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home
{
    public class CheckoutManage:ICheckout
    {
        private readonly BEDATNDbContext _context;
        public CheckoutManage(BEDATNDbContext context)
        {
            _context = context;
        }

        

        public async Task<KhachHang> GetKhachHangById(int Id)
        {
            
             var khachhang = await _context.KhachHangs.FindAsync(Id);

            return khachhang;

        }

        public async Task<KhuyenMai> GetKhuyenMaiByMaKhuyenMai(string makhuyenmai)
        {
            var makm = await _context.KhuyenMais.SingleOrDefaultAsync(k => k.MaKhuyenMai == makhuyenmai);

            return makm;
        }


        public async Task<ActionResult> CreateDonHang([FromBody] DonHangModel model)
        {
            var donhang = new DonHang()
            {
                IdKhachHang=model.IdKhachHang,
                TenKhachHang = model.TenKhachHang,
                TongTien = model.TongTien,
                MaKhuyenMai = model.MaKhuyenMai,
                DiaChiGiaoHang = model.DiaChiGiaoHang,
                SDT = model.SDT,
                TrangThaiThanhToan = model.TrangThaiThanhToan,
                TrangThaiDonHang = "Chờ xác nhận",
                PhuongThucThanhToan=model.PhuongThucThanhToan,
                NgayDat = System.DateTime.Now,
                GhiChu=model.GhiChu
            };

            _context.DonHangs.Add(donhang);
            await _context.SaveChangesAsync();


            int IdDonHang = donhang.IdDonHang;

            if (model.ctdh.Count > 0)
            {
                foreach (var item in model.ctdh)
                {
                    item.IdDonHang = IdDonHang;
                    _context.ChiTietDonHangs.Add(item);
                    
                  
                }
                _context.SaveChanges();
            }

            if (donhang.IdDonHang > 0)
            {
                return new OkObjectResult("Đơn hàng tạo thành công");
            }
            else
            {
                return new BadRequestObjectResult("Không thể tạo đơn hàng");
            }
        }

        public async Task<ActionResult> CreateDonHangVoiViMoMo([FromBody] DonHangModel model)
        {
            var donhang = new DonHang()
            {
                IdKhachHang = model.IdKhachHang,
                TenKhachHang = model.TenKhachHang,
                TongTien = model.TongTien,
                MaKhuyenMai = model.MaKhuyenMai,
                DiaChiGiaoHang = model.DiaChiGiaoHang,
                SDT = model.SDT,
                TrangThaiThanhToan = model.TrangThaiThanhToan,
                TrangThaiDonHang = "Chờ xác nhận",
                PhuongThucThanhToan = model.PhuongThucThanhToan,
                NgayDat = System.DateTime.Now,
                GhiChu = model.GhiChu
            };

            _context.DonHangs.Add(donhang);
            await _context.SaveChangesAsync();


            int IdDonHang = donhang.IdDonHang;

            if (model.ctdh.Count > 0)
            {
                foreach (var item in model.ctdh)
                {
                    item.IdDonHang = IdDonHang;
                    _context.ChiTietDonHangs.Add(item);


                }
                _context.SaveChanges();
            }

            if (donhang.IdDonHang > 0)
            {
                return new OkObjectResult("Đơn hàng tạo thành công");
            }
            else
            {
                return new BadRequestObjectResult("Không thể tạo đơn hàng");
            }
        }
    }
}
