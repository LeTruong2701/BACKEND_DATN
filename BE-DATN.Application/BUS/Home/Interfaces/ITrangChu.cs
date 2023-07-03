using BE_DATN.Application.BUS.Home.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BE_DATN.Application.BUS.Home.Interfaces
{
    public interface ITrangChu
    {
       
        Task<List<SanPhamModel>> GetAoTheThao();
        Task<List<SanPhamModel>> GetQuanTheThao();
        Task<List<SanPhamModel>> GetPhuKienTheThao();
        Task<List<SanPhamModel>> GetSanPhamMoi();


        List<EntityDanhMuc> GetData();
        //Task<ActionResult> GetAllByCategoryPaging(int? Category_Id, int pageindex, int pagesize, string filter, int? lowprice, int? highprice);
        //Task<IAsyncResult> GetAllPaging(int? Category_Id, int pageindex, int pagesize, string keyword);
        //Task<News> GetById(int Id);
        //Task<int> Create(NewsModel news);
        //Task<int> Update(NewsModel news);
        //Task<int> Delete(int Id);
    }
}
