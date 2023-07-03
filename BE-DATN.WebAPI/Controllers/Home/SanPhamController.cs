using BE_DATN.Application.BUS.Home.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BE_DATN.WebAPI.Controllers.Home
{
    [Route("api/sanpham")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPham _manageSanPham;
        public SanPhamController(ISanPham manageSanPham)
        {
            _manageSanPham = manageSanPham;
        }

        [Route("get-sanpham-by-idsanpham/{id}")]
        [HttpGet]
        public async Task<IActionResult> getSanPhamByIdSanPham(int Id)
        {
            var result = await _manageSanPham.GetSanPhamByIdSanPham(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy sản phẩm theo màu với id sản phẩm");
            }
            return Ok(result);

        }


        //lấy màu sản phẩm để có idmausanpham lấy được thông tin màu sản phẩm đó để hiển thị
        [Route("get-sanpham-maudautien-by-idsanpham/{id}")]
        [HttpGet]
        public async Task<IActionResult> getMauSanPhamFirtById(int Id)
        {           
            var result = await _manageSanPham.GetSanPhamMauByIdSanPham(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy sản phẩm theo màu với id sản phẩm");
            }
            return Ok(result);

        }
        //lấy thông tin sản phẩm  với idmausanpham lấy được ở trên
        [Route("get-san-pham-theo-mau-by-idmausanpham/{id}")]
        [HttpGet]
        public async Task<IActionResult> getMauSanPhamById(int Id)
        {
            var result = await _manageSanPham.GetSanPhamTheoMauByIdMauSanPham(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy thông tin sản phẩm theo màu");
            }
            return Ok(result);

        }

        //lấy list size sản phẩm với id màu sản phẩm 
        [Route("get-list-size-san-pham-by-idmausanpham/{id}")]
        [HttpGet]
        public async Task<IActionResult> getSizeSanPhamByIdMauSanPham(int Id)
        {
            var result = await _manageSanPham.GetListSizeByIdMauSanPham(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy list size");
            }
            return Ok(result);

        }


        //lấy size sản phẩm đầu tiên của màu sản phẩm với id màu sản phẩm 
        [Route("get-size-sanpham-dautien-by-idmausanpham/{id}")]
        [HttpGet]
        public async Task<IActionResult> getSizeSanPhamFirtByIdMauSanPham(int Id)
        {
            var result = await _manageSanPham.GetSizeSanPhamDauTienByIdMauSanPham(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy size sản phẩm đầu tiên");
            }
            return Ok(result);

        }

        //lấy thông tin size sản phẩm id size sản phẩm 
        [Route("get-size-sanpham-by-id-size/{id}")]
        [HttpGet]
        public async Task<IActionResult> getSizeSanPhamByIdSizeSanPham(int Id)
        {
            var result = await _manageSanPham.GetSizeSanPhamByIdSizeSanPham(Id);
            if (result == null)
            {
                return BadRequest("Không tìm thấy size sản phẩm");
            }
            return Ok(result);

        }



        //lấy list màu của sản phẩm
        [Route("get-list-mau-sanpham-by-id/{id}")]
        [HttpGet]
        public async Task<IActionResult> getListMauCuaSanPhamFirtByIdSanPham(int Id)
        {
            var result = await _manageSanPham.GetListMauCuaSanPham(Id);
            if (result == null)
            {
                return BadRequest("Không thấy tìm thấy list màu của sản phẩm");
            }
            return Ok(result);

        }

        //lấy danh sách sản phẩm cùng loại
        [Route("get-sanphamcungloai/{id}")]
        [HttpGet]
        public async Task<IActionResult> getSanPhamCungLoai(int ID)
        {
            var result = await _manageSanPham.GetSanPhamCungLoai(ID);
            if (result == null)
            {
                return BadRequest("Get Failed");
            }

            return Ok(result);
        }

    }
}
