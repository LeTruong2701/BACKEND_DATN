using BE_DATN.Application.BUS.Admin.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using BE_DATN.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BE_DATN.Application.BUS.Admin.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BE_DATN.Application.BUS.Admin
{
    public class ManageSanPham :IManageSanPham
    {
        private readonly BEDATNDbContext _context;
        public ManageSanPham(BEDATNDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(SanPhamModel sp)
        {
            _context.SanPhams.Add(sp.sanPham);
            await _context.SaveChangesAsync();

            return 1;
        }

      

        public async Task<int> Delete(int Id)
        {

            var sanpham = await _context.SanPhams.FindAsync(Id);

            var idSanPham = sanpham.IdSanPham;
            _context.SanPhams.Remove(sanpham);
            await _context.SaveChangesAsync();

            var mauSanPhamsToDelete = await _context.MauSanPhams.Where(s => s.IdSanPham == idSanPham ).ToListAsync();
            _context.MauSanPhams.RemoveRange(mauSanPhamsToDelete);
            await _context.SaveChangesAsync();

            var sizeSanPhamsToDelete = await _context.SizeSanPhams.Where(s => s.IdSanPham == idSanPham).ToListAsync();
            _context.SizeSanPhams.RemoveRange(sizeSanPhamsToDelete);
            await _context.SaveChangesAsync();


            var sizeSanPhamsKhoToDelete = await _context.Khos.Where(s => s.IdSanPham == idSanPham ).ToListAsync();
            _context.Khos.RemoveRange(sizeSanPhamsKhoToDelete);
            await _context.SaveChangesAsync();


            //var sanpham = await _context.SanPhams.FindAsync(Id);

            //_context.SanPhams.Remove(sanpham);
            //await _context.SaveChangesAsync();
            return 1;
        }

       

        public async Task<SanPhamModel> GetById(int Id)
        {
            var data = from dm in _context.DanhMucs
                       join sp in _context.SanPhams on dm.IdDanhMuc equals sp.IdDanhMuc
                       where sp.IdSanPham == Id //Thêm điều kiện lọc sản phẩm cần tìm kiếm
                       select new SanPhamModel
                       {
                           IdSanPham = sp.IdSanPham,
                           IdDanhMuc = sp.IdDanhMuc,
                           IdDanhMucCha = dm.IdDanhMucCha.GetValueOrDefault(),
                           TenSanPham = sp.TenSanPham,
                           MoTaSanPham = sp.MoTaSanPham,
                           AnhSanPham = sp.AnhSanPham,
                           XuatXu = sp.XuatXu,
                           ChatLieu = sp.ChatLieu,
                           IdThuongHieu = sp.IdThuongHieu,
                           NgayTao = sp.NgayTao,
                           TrangThai = sp.TrangThai
                       };

            return await data.FirstOrDefaultAsync();
        }

        public async Task<PagedResult<SanPhamModel>> SearchSanPhamPaging([FromBody] Dictionary<string, object> formData)
        {
            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";

            var iddanhmuc = 0;
            if (formData.Keys.Contains("iddanhmuc"))
            {
                int.TryParse(formData["iddanhmuc"].ToString(), out iddanhmuc);
            }

            var result = from nv in _context.SanPhams
                         select new
                         {
                             IdSanPham = nv.IdSanPham,
                             IdDanhMuc = nv.IdDanhMuc,
                             TenSanPham = nv.TenSanPham,
                             MoTaSanPham = nv.MoTaSanPham,
                             AnhSanPham = nv.AnhSanPham,
                             XuatXu = nv.XuatXu,
                             ChatLieu = nv.ChatLieu,
                             IdThuongHieu = nv.IdThuongHieu,
                             NgayTao = nv.NgayTao,
                             TrangThai = nv.TrangThai
                         };

            if (!string.IsNullOrEmpty(ten) && iddanhmuc != 0)
            {
                // Lọc sản phẩm theo cả tên và danh mục
                result = result.Where(x => x.TenSanPham.Contains(ten) && x.IdDanhMuc.Equals(iddanhmuc));
            }
            else if (!string.IsNullOrEmpty(ten))
            {
                // Lọc sản phẩm theo tên
                result = result.Where(x => x.TenSanPham.Contains(ten));
            }
            else if (iddanhmuc != 0)
            {
                // Lọc sản phẩm theo danh mục
                result = result.Where(x => x.IdDanhMuc == iddanhmuc);
            }

            // Lấy tổng số sản phẩm thỏa mãn
            int totalItems = await result.CountAsync();

            var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new SanPham()
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

            var pageResult = new PagedResult<SanPhamModel>()
            {
                totalItem = totalItems,
                page = page,
                pageSize = pageSize,
                data = GetSanPhamListWithPrice(kq)

            };

            return pageResult;
        }


        //public async Task<PagedResult<SanPhamModel>> SearchSanPhamPaging([FromBody] Dictionary<string, object> formData)
        //{
        //    var page = int.Parse(formData["page"].ToString());
        //    var pageSize = int.Parse(formData["pageSize"].ToString());

        //    var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";

        //    var iddanhmuc=formData.Keys.Contains("iddanhmuc") ? (formData["iddanhmuc"]).ToString().Trim() : "";
        //    var result = from nv in _context.SanPhams
        //                 select new {
        //                     IdSanPham = nv.IdSanPham,
        //                     IdDanhMuc = nv.IdDanhMuc,
        //                     TenSanPham = nv.TenSanPham,
        //                     MoTaSanPham = nv.MoTaSanPham,
        //                     AnhSanPham = nv.AnhSanPham,
        //                     XuatXu = nv.XuatXu,
        //                     ChatLieu = nv.ChatLieu,
        //                     IdThuongHieu = nv.IdThuongHieu,
        //                     NgayTao = nv.NgayTao,
        //                     TrangThai = nv.TrangThai };

        //    // Lọc các sản phẩm theo tên
        //    result = result.Where(x => x.TenSanPham.Contains(ten));

        //    // Lấy tổng số sản phẩm thỏa mãn
        //    int totalItems = await result.CountAsync();

        //    var kq = await result.Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new SanPham()
        //    {
        //        IdSanPham = x.IdSanPham,
        //        IdDanhMuc = x.IdDanhMuc,
        //        TenSanPham = x.TenSanPham,
        //        MoTaSanPham = x.MoTaSanPham,
        //        AnhSanPham = x.AnhSanPham,
        //        XuatXu = x.XuatXu,
        //        ChatLieu = x.ChatLieu,
        //        IdThuongHieu = x.IdThuongHieu,
        //        NgayTao = x.NgayTao,
        //        TrangThai = x.TrangThai

        //    }).ToListAsync();

        //    var pageResult = new PagedResult<SanPhamModel>()
        //    {
        //        totalItem = totalItems,
        //        page = page,
        //        pageSize = pageSize,
        //        data = GetSanPhamListWithPrice(kq)

        //    };

        //    return pageResult;
        //}

        public async Task<int> Update(SanPhamModel sp)
        {
            var checkmausanpham = await _context.MauSanPhams.Where(m => m.IdSanPham ==sp.sanPham.IdSanPham).ToListAsync();

            var sanpham = await _context.SanPhams.FindAsync(sp.sanPham.IdSanPham);

            sanpham.IdDanhMuc = sp.sanPham.IdDanhMuc;
            sanpham.TenSanPham = sp.sanPham.TenSanPham;
            sanpham.MoTaSanPham = sp.sanPham.MoTaSanPham;
            sanpham.AnhSanPham = sp.sanPham.AnhSanPham;
            sanpham.XuatXu = sp.sanPham.XuatXu;
            sanpham.ChatLieu = sp.sanPham.ChatLieu;
            sanpham.IdThuongHieu = sp.sanPham.IdThuongHieu;
            sanpham.NgayTao = sp.sanPham.NgayTao;
            if (checkmausanpham.Count==0)
            {
                sanpham.TrangThai = 0;
            }
            else
            {
                sanpham.TrangThai = 1;
            }
            
            await _context.SaveChangesAsync();

            return 1;
        }
        

        //Màu sản phẩm
        //lấy các sản phẩm có màu sắc khác nhau của sản phẩm
        public async Task<PagedResult<MauSanPhamModel>> GetListMauSanPhamById([FromBody] Dictionary<string, object> formData)
        {

            var idsanpham = int.Parse(formData["idSanPham"].ToString());

            var page = int.Parse(formData["page"].ToString());
            var pageSize = int.Parse(formData["pageSize"].ToString());

            var ten = formData.Keys.Contains("ten") ? (formData["ten"]).ToString().Trim() : "";



            var result = from sp in _context.SanPhams
                         join msp in _context.MauSanPhams on sp.IdSanPham equals msp.IdSanPham
                         select new
                         {
                             IdSanPham = sp.IdSanPham,
                             IdDanhMuc = sp.IdDanhMuc,
                             IdThuongHieu = sp.IdThuongHieu,
                             IdMauSanPham=msp.IdMauSanPham,
                             TenSanPham = sp.TenSanPham,
                             NgayTao = sp.NgayTao,
                             TrangThai=msp.TrangThai,
                             TenMau=msp.TenMau,
                             GiaBan=msp.GiaBan,
                             GiaNhap=msp.GiaNhap,
                             AnhSanPham=msp.AnhSanPham,
                             MaMau=msp.MaMau,
                             CheckThanhToan=msp.CheckThanhToan
                            
                         };
            var kq = await result.Where(x => x.IdSanPham == idsanpham).Skip(pageSize * (page - 1)).Take(pageSize).Select(x => new MauSanPhamModel()
            {
                IdSanPham = x.IdSanPham,
                IdDanhMuc = x.IdDanhMuc,
                IdThuongHieu = x.IdThuongHieu,
                IdMauSanPham = x.IdMauSanPham,
                TenSanPham = x.TenSanPham,
                NgayTao = x.NgayTao,
                TrangThai = x.TrangThai,
                TenMau = x.TenMau,
                GiaBan = x.GiaBan,
                GiaNhap = x.GiaNhap,
                AnhSanPham = x.AnhSanPham,
                MaMau = x.MaMau,
                CheckThanhToan = x.CheckThanhToan

            }).ToListAsync();

            var pageResult = new PagedResult<MauSanPhamModel>()
            {
                totalItem = result.Count(),
                page = page,
                pageSize = pageSize,
                data = kq

            };

            return pageResult;
        }

        //lấy màu sản phẩm theo id màu sản phẩm
        public async Task<MauSanPham> GetMauSanPhamById(int Id)
        {
            var mausanpham = await _context.MauSanPhams.FindAsync(Id);

            return mausanpham;
        }

        public Task<PagedResult<SanPham>> SearchMauSanPhamPaging([FromBody] Dictionary<string, object> formData)
        {
            throw new NotImplementedException();
        }



        public async Task<MauSanPhamModel> CreateMauSanPham(MauSanPhamModel msp)
        {

            _context.MauSanPhams.Add(msp.mauSanPham);
            await _context.SaveChangesAsync();

            int IdSanPham = msp.mauSanPham.IdSanPham;
            int IdMauSanPham = msp.mauSanPham.IdMauSanPham;

            var sanpham = await _context.SanPhams.FindAsync(IdSanPham);
            sanpham.TrangThai = 1;
            await _context.SaveChangesAsync();


            MauSanPhamModel result = new MauSanPhamModel
            {
                IdMauSanPham = msp.mauSanPham.IdMauSanPham,
                IdSanPham = msp.mauSanPham.IdSanPham,
                TenMau = msp.mauSanPham.TenMau,
                GiaNhap = msp.mauSanPham.GiaNhap,
            };

            return result;
        }

        public async Task<int> UpdateMauSanPham(MauSanPhamModel msp)
        {
            var mausanpham = await _context.MauSanPhams.FindAsync(msp.mauSanPham.IdMauSanPham);

            mausanpham.IdSanPham = msp.mauSanPham.IdSanPham;
            mausanpham.TenMau = msp.mauSanPham.TenMau;
            mausanpham.MaMau = msp.mauSanPham.MaMau;
            mausanpham.GiaNhap = msp.mauSanPham.GiaNhap;
            mausanpham.GiaBan = msp.mauSanPham.GiaBan;
            mausanpham.AnhSanPham = msp.mauSanPham.AnhSanPham;
            await _context.SaveChangesAsync();

            //if (msp.sizesanpham.count > 0)
            //{
            //    foreach (var item in msp.sizesanpham)
            //    {
            //        if (item.idsizesanpham == 0)
            //        {
            //            item.idmausanpham = msp.mausanpham.idmausanpham;
            //            _context.sizesanphams.add(item);
            //            await _context.savechangesasync();
            //        }
            //        else
            //        {
            //            var sizesp = await _context.sizesanphams.findasync(item.idsizesanpham);
            //            sizesp.size = item.size;
            //            sizesp.soluong= item.soluong;
            //            sizesp.trangthai= item.trangthai;
            //            await _context.savechangesasync();
            //        }

            //    }
            //    await _context.savechangesasync();
            //}

            return 1;
        }

        public async Task<int> DeleteMauSanPham(int Id)
        {
            var mausanpham = await _context.MauSanPhams.FindAsync(Id);

            var idSanPham = mausanpham.IdSanPham;
            _context.MauSanPhams.Remove(mausanpham);
            await _context.SaveChangesAsync();

            var sizeSanPhamsToDelete = await _context.SizeSanPhams.Where(s => s.IdSanPham == idSanPham && s.IdMauSanPham == Id).ToListAsync();
            _context.SizeSanPhams.RemoveRange(sizeSanPhamsToDelete);
            await _context.SaveChangesAsync();


            var sizeSanPhamsKhoToDelete = await _context.Khos.Where(s => s.IdSanPham == idSanPham && s.IdMauSanPham == Id).ToListAsync();
            _context.Khos.RemoveRange(sizeSanPhamsKhoToDelete);
            await _context.SaveChangesAsync();


            //sau khi xóa màu sản phẩm kiểm tra xem sản phẩm đó còn màu sản phẩm nào nữa hay không
            //nếu còn thì hiện trạng thái "đã nhập" nếu trống thì hiện trang thái "chưa nhập"
            var list_mausanpham = from msp in _context.MauSanPhams select new { msp.IdMauSanPham,msp.IdSanPham};

            var kq = await list_mausanpham.Where(x => x.IdSanPham == idSanPham).CountAsync();

            if (kq==0)
            {
                var sanpham = await _context.SanPhams.FindAsync(idSanPham);
                sanpham.TrangThai = 0;
                await _context.SaveChangesAsync();
            }
            return 1;
        }

        Task<PagedResult<MauSanPham>> IManageSanPham.SearchMauSanPhamPaging(Dictionary<string, object> formData)
        {
            throw new NotImplementedException();
        }

        //Size sản phẩm
        public async Task<Result<SizeSanPhamModel>> GetListSizeSanPhamByIdMauSanPham([FromBody] Dictionary<string, object> formData)
        {
            var idmausanpham = int.Parse(formData["idMauSanPham"].ToString());
            var result = from msp in _context.MauSanPhams
                         join ssp in _context.SizeSanPhams on msp.IdMauSanPham equals ssp.IdMauSanPham
                         select new
                         {
                             IdSanPham = msp.IdSanPham,
                             IdMauSanPham = msp.IdMauSanPham,
                             IdSizeSanPham = ssp.IdSizeSanPham,
                             Size = ssp.Size,
                             SoLuong = ssp.SoLuong,
                            
                             TrangThai = ssp.TrangThai,
                             

                         };
            var kq = await result.Where(x => x.IdMauSanPham == idmausanpham).Select(x => new SizeSanPhamModel()
            {
                IdSanPham = x.IdSanPham,
                IdMauSanPham = x.IdMauSanPham,
                IdSizeSanPham=x.IdSizeSanPham,
                Size = x.Size,
                SoLuong = x.SoLuong,

                TrangThai = x.TrangThai,

            }).ToListAsync();

            var resultdata = new Result<SizeSanPhamModel>()
            {
               
                data = kq

            };

            return resultdata;
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
                    IdDanhMuc =item.IdDanhMuc,
                    TenSanPham = item.TenSanPham,
                    MoTaSanPham = item.MoTaSanPham,
                    XuatXu = item.XuatXu,
                    ChatLieu = item.ChatLieu,
                    IdThuongHieu = item.IdThuongHieu,
                    AnhSanPham = item.AnhSanPham,
                    NgayTao = item.NgayTao,
                    TrangThai = item.TrangThai,
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
