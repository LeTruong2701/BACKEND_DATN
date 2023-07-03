using BE_DATN.Application.BUS.Admin.DTO;
using BE_DATN.Application.Common;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Admin.Interfaces
{
    public interface IManageDonHang
    {
        Task<PagedResult<DonHangModel>> SearchDanhMucPaging([FromBody] Dictionary<string, object> formData);
        Task<PagedResult<DonHangModel>> SearchDonHangPaging([FromBody] Dictionary<string, object> formData);
        Task<PagedResult<DonHangModel>> SearchDonHangPagingTheoNgay([FromBody] Dictionary<string, object> formData);

        Task<int> CapNhatKhoSauKhiBanHang(KhoSizeModel model);


        Task<List<ChiTietDonHangModel>> GetChiTietDonHangByIdDonHang(int Id);
        Task<List<DonHang>> GetKhachHang();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        Task<DonHang> GetById(int Id);

        Task<int> Update(DonHangDTO th);

        Task<int> Delete(int Id);
    }
}
