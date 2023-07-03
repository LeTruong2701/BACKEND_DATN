using BE_DATN.Data.Configurations;
using BE_DATN.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.EF
{
    public class BEDATNDbContext : IdentityDbContext<Users>
    {
        public BEDATNDbContext(DbContextOptions<BEDATNDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SanPhamConfiguration());
            modelBuilder.ApplyConfiguration(new DanhMucConfiguration());
            modelBuilder.ApplyConfiguration(new MauSanPhamConfiguration());
            modelBuilder.ApplyConfiguration(new SizeSanPhamConfiguration());
            modelBuilder.ApplyConfiguration(new KhuyenMaiConfiguration());
            modelBuilder.ApplyConfiguration(new KhachHangConfiguration());
            modelBuilder.ApplyConfiguration(new NewsConfiguration());
            modelBuilder.ApplyConfiguration(new NhaCungCapConfiguration());
            modelBuilder.ApplyConfiguration(new NhanVienConfiguration());
            modelBuilder.ApplyConfiguration(new ThuongHieuConfiguration());
            modelBuilder.ApplyConfiguration(new DonHangConfiguration());
            modelBuilder.ApplyConfiguration(new ChiTietDonHangConfiguration());
            modelBuilder.ApplyConfiguration(new HoaDonNhapConfiguration());
            modelBuilder.ApplyConfiguration(new ChiTietHoaDonNhapConfiguration());
            modelBuilder.ApplyConfiguration(new NguoiDungConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new KhoConfiguration());
            modelBuilder.ApplyConfiguration(new DanhGiaSanPhamConfiguration());


            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(i => new { i.UserId, i.RoleId });
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(i => i.UserId);
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

            //
            modelBuilder.Entity<Roles>();
            //modelBuilder.Entity<AppUser>();
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
            });

        }

        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<MauSanPham> MauSanPhams { get; set; }
        public DbSet<SizeSanPham> SizeSanPhams { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<News> Newss { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public DbSet<HoaDonNhap> HoaDonNhaps { get; set; }
        public DbSet<ChiTietHoaDonNhap> ChiTietHoaDonNhaps { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<Kho> Khos { get; set; }
        public DbSet<DanhGiaSanPham> DanhGiaSanPhams { get; set; }

        public DbSet<Users> User1s { get; set; }
        public DbSet<Roles> Role1s { get; set; }

    }
}
