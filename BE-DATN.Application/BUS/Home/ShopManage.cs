using BE_DATN.Application.BUS.Home.DTO;
using BE_DATN.Application.BUS.Home.Interfaces;
using BE_DATN.Application.Common;
using BE_DATN.Data.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BE_DATN.Application.BUS.Home
{
    public class ShopManage : IShop
    {
        private readonly BEDATNDbContext _context;
        public ShopManage(BEDATNDbContext context)
        {
            _context = context;
        }

        public async Task<List<ThuongHieuModel>> GetListThuongHieu()
        {
            var data = from th in _context.ThuongHieus
                       select new { th.IdThuongHieu, th.TenThuongHieu, th.TrangThai };
            return await data.Where(a => a.TrangThai == 1).Select(x => new ThuongHieuModel()
            {
                IdThuongHieu = x.IdThuongHieu,
                TenThuongHieu = x.TenThuongHieu,


            }).ToListAsync();
        }
        //lấy list sản phẩm tìm kiếm theo giá tiền
        public List<SanPhamModel> GetSanPhamListTheoKhoangGia(dynamic listsp, int page, int pageSize, int giadau, int giacuoi)
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

                if (giadau==500000)
                {
                    if (min >= 500000 || max >= 500000)
                    {
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
                }
                else
                {
                    // Kiểm tra nếu giá của sản phẩm thuộc khoảng giá cần lấy thì thêm sản phẩm vào danh sách kết quả
                    if ((min >= giadau || max >= giadau) && (min <= giacuoi || max <= giacuoi))
                    {
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
                }

                
            }

            // Sắp xếp danh sách kết quả theo giá tăng dần
            ketqua = ketqua.OrderBy(x => x.minGiaSP).Skip(pageSize * (page - 1)).Take(pageSize).ToList();

            return ketqua;
        }






        public List<SanPhamModel> GetSanPhamListWithPrice(dynamic listsp,int page,int pageSize)
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
            ketqua= ketqua.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

            return ketqua;
        }

        //lấy list sản phẩm có cả giá, sắp xếp tăng dần giảm dần
        public List<SanPhamModel> GetSanPhamListWithPriceAndSortOrder(dynamic listsp, int sortOrder,int page,int pageSize)
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

            // Sắp xếp theo giá max
            if (sortOrder == 1)
            {
                ketqua = ketqua.OrderBy(sp => sp.maxGiaSP).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }
            else // Sắp xếp theo giá max giảm dần
            {
                ketqua = ketqua.OrderByDescending(sp => sp.maxGiaSP).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            }

            return ketqua;
        }



        public PagedResult<SanPhamModel> SearchSanPhamPaging([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());

                string keyword1 = "";


                if (formData.Keys.Contains("keyword1") && !string.IsNullOrEmpty(Convert.ToString(formData["keyword1"])))
                { keyword1 = formData["keyword1"].ToString(); }

               
                int? id_danh_muc = null;
                string loc = "";
               
                if (formData.Keys.Contains("loc") && !string.IsNullOrEmpty(Convert.ToString(formData["loc"])))
                { loc = formData["loc"].ToString(); }


                if (formData.Keys.Contains("id_danh_muc") && !string.IsNullOrEmpty(Convert.ToString(formData["id_danh_muc"])))
                { id_danh_muc = int.Parse(formData["id_danh_muc"].ToString()); }

                int? id_thuonghieu = null;
                if (formData.Keys.Contains("idthuonghieu") && !string.IsNullOrEmpty(Convert.ToString(formData["idthuonghieu"])))
                { id_thuonghieu = int.Parse(formData["idthuonghieu"].ToString()); }

                var data = from r in _context.SanPhams                              
                             select new {
                                IdSanPham= r.IdSanPham,
                                 IdDanhMuc=r.IdDanhMuc,
                                TenSanPham= r.TenSanPham,
                                 MoTaSanPham=r.MoTaSanPham,
                                 AnhSanPham=r.AnhSanPham,
                                 XuatXu=r.XuatXu,
                                 ChatLieu=r.ChatLieu,
                                 IdThuongHieu=r.IdThuongHieu,
                                 NgayTao=r.NgayTao,
                                 TrangThai=r.TrangThai,   
                             };

                var resultkeyword = data.Where(z => z.TenSanPham.Contains(keyword1)).Select(n => new SanPhamModel {
                            IdSanPham = n.IdSanPham,
                            IdDanhMuc = n.IdDanhMuc,
                            TenSanPham = n.TenSanPham,
                            MoTaSanPham = n.MoTaSanPham,
                            AnhSanPham = n.AnhSanPham,
                            XuatXu = n.XuatXu,
                            ChatLieu = n.ChatLieu,
                            IdThuongHieu = n.IdThuongHieu,
                            NgayTao = n.NgayTao,
                            TrangThai = n.TrangThai,
                }).ToList();

                var resultiddanhmuc = data.Where(s => s.IdDanhMuc == id_danh_muc || id_danh_muc == null).Select(x => new SanPhamModel
                {
                        IdSanPham = x.IdSanPham,
                        IdDanhMuc = x.IdDanhMuc,
                        TenSanPham = x.TenSanPham,
                        MoTaSanPham = x.MoTaSanPham,
                        AnhSanPham = x.AnhSanPham,
                        XuatXu =x.XuatXu,
                        ChatLieu = x.ChatLieu,
                        IdThuongHieu = x.IdThuongHieu,
                        NgayTao = x.NgayTao,
                        TrangThai = x.TrangThai,
                }).ToList();

                dynamic resultidthuonghieu;

                

                dynamic final_result;
                dynamic ketqua = null;
                int total = 0;
                if (id_danh_muc != null && keyword1 == "")
                {
                    total = resultiddanhmuc.Count();

                    switch (loc)
                    {
                        case "IdThuongHieu":
                            resultidthuonghieu = resultiddanhmuc.Where(s => s.IdThuongHieu == id_thuonghieu || id_thuonghieu == null).Select(x => new SanPhamModel
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
                                TrangThai = x.TrangThai,
                            }).ToList();
                            ketqua = resultidthuonghieu;
                            final_result = GetSanPhamListWithPriceAndSortOrder(ketqua, 1, page, pageSize);
                            break;
                        case "100-300":
                            ketqua = resultiddanhmuc.ToList();
                            final_result = GetSanPhamListTheoKhoangGia(ketqua, page, pageSize, 100000, 300000);
                            break;
                        case "300-500":
                            ketqua = resultiddanhmuc.ToList();
                            final_result = GetSanPhamListTheoKhoangGia(ketqua, page, pageSize, 300000, 500000);
                            break;
                        case "500":
                            ketqua = resultiddanhmuc.ToList();
                            final_result = GetSanPhamListTheoKhoangGia(ketqua, page, pageSize,500000,0);
                            break;


                        case "Tang":
                            ketqua = resultiddanhmuc.ToList();
                            final_result = GetSanPhamListWithPriceAndSortOrder(ketqua, 1,page,pageSize);
                            break;

                        case "Giam":
                            ketqua = resultiddanhmuc.ToList();
                            final_result = GetSanPhamListWithPriceAndSortOrder(ketqua, 0,page,pageSize);
                            break;

                        case "KeyWord":
                            total = resultkeyword.Count();
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListWithPrice(ketqua,page,pageSize);
                            break;

                        default:
                            ketqua = resultiddanhmuc.ToList();
                            final_result = GetSanPhamListWithPrice(ketqua,page,pageSize);
                            break;
                    }
                }
                else
                {
                  
                   total= resultkeyword.Count();
                    switch (loc)
                    {
                        case "IdThuongHieu":
                            resultidthuonghieu = resultkeyword.Where(s => s.IdThuongHieu == id_thuonghieu || id_thuonghieu == null).Select(x => new SanPhamModel
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
                                TrangThai = x.TrangThai,
                            }).ToList();
                            ketqua = resultidthuonghieu;
                            final_result = GetSanPhamListWithPriceAndSortOrder(ketqua, 1, page, pageSize);
                            break;

                        case "100-300":
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListTheoKhoangGia(ketqua, page, pageSize, 100000, 300000);
                            break;
                        case "300-500":
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListTheoKhoangGia(ketqua, page, pageSize, 300000, 500000);
                            break;
                        case "500":
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListTheoKhoangGia(ketqua, page, pageSize, 500000, 0);
                            break;


                        case "Tang":
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListWithPriceAndSortOrder(ketqua, 1,page,pageSize);
                            break;

                        case "Giam":
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListWithPriceAndSortOrder(ketqua, 0,page,pageSize);
                            break;

                        case "KeyWord":
                            total = resultkeyword.Count();
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListWithPrice(ketqua,page,pageSize);
                            break;

                        default:
                            ketqua = resultkeyword.ToList();
                            final_result = GetSanPhamListWithPrice(ketqua,page,pageSize);
                            break;
                    }
                }


                var pageResult = new PagedResult<SanPhamModel>()
                {
                    totalItem = total,
                    page = page,
                    pageSize = pageSize,
                    data = final_result

                };

                return pageResult;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
