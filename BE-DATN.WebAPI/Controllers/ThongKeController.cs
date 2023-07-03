using BE_DATN.Application.BUS.Admin.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKe _manageThongKe;
        public ThongKeController(IThongKe manageThongKe)
        {
            _manageThongKe = manageThongKe;
        }

        [Route("get-sodonhang-theo-ngaythangnam/{date}")]
        [HttpGet]
        public async Task<IActionResult> getbyid(DateTime date)
        {
            var sodon = await _manageThongKe.GetSoDonHangTheoNgayThangNam(date);
            if (sodon == null)
            {
                return BadRequest("Không tìm thấy ngày");
            }
            return Ok(sodon);

        }


        [Route("get-sodonhang-theo-thang")]
        [HttpGet]
        public async Task<IActionResult> getSoDonTheoThang()
        {
            var result = await _manageThongKe.GetSoDonHangTheoThang();
            if (result == 0)
            {
                return BadRequest("Không tìm thấy hóa đơn");
            }
            return Ok(result);

        }

        [Route("get-loinhuan-theo-thang")]
        [HttpGet]
        public async Task<IActionResult> getLoiNhuanTheoThang()
        {
            var result = await _manageThongKe.GetLoiNhuanTheoThang();
            
            return Ok(result);

        }

        [Route("get-doanhthuloinhuan")]
        [HttpGet]
        public  IActionResult getdoanhthuloinhuan([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            var doanhthu =  _manageThongKe.GetDoanhThuLoiNhuan(fromDate,toDate);
            if (doanhthu == null)
            {
                return BadRequest("Không có doanh thu");
            }
            return Ok(doanhthu);

        }

        [Route("get-thongkesodon")]
        [HttpGet]
        public IActionResult getthongkedonhang([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            var doanhthu = _manageThongKe.GetThongKeSoDon(fromDate, toDate);
            if (doanhthu == null)
            {
                return BadRequest("Không có số đơn");
            }
            return Ok(doanhthu);

        }

        [Route("get-danhsachsanpham-voisoluongban-trongthang")]
        [HttpGet]
        public IActionResult getdanhsachsanphambantrongthang()
        {
            var doanhthu = _manageThongKe.GetSanPhamListWithSoLuongBanTrongThang();
            if (doanhthu == null)
            {
                return BadRequest("Không có ");
            }
            return Ok(doanhthu);

        }

    }
}
