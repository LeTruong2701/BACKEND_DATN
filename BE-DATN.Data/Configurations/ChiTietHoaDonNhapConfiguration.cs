using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class ChiTietHoaDonNhapConfiguration : IEntityTypeConfiguration<ChiTietHoaDonNhap>
    {
        public void Configure(EntityTypeBuilder<ChiTietHoaDonNhap> builder)
        {
            builder.ToTable("ChiTietHoaDonNhap");
            builder.HasKey(x => x.IdChiTietHoaDonNhap);
            builder.Property(x => x.IdHoaDonNhap).IsRequired();
            builder.Property(x => x.IdSanPham).IsRequired();
            builder.Property(x => x.IdMauSanPham).IsRequired();
            builder.Property(x => x.IdSizeSanPham).IsRequired();
            builder.Property(x => x.SoLuong).IsRequired();
            builder.Property(x => x.GiaNhap).IsRequired();

            //builder.HasOne(x => x.HoaDonNhap).WithMany(x => x.ChiTietHoaDonNhaps).HasForeignKey(x => x.IdHoaDonNhap);

            //builder.HasOne(x => x.SanPham).WithMany(x => x.ChiTietHoaDonNhaps).HasForeignKey(x => x.IdSanPham);
            //builder.HasOne(x => x.MauSanPham).WithMany(x => x.ChiTietHoaDonNhaps).HasForeignKey(x => x.IdMauSanPham);
            //builder.HasOne(x => x.SizeSanPham).WithMany(x => x.ChiTietHoaDonNhaps).HasForeignKey(x => x.IdSizeSanPham);

            builder.HasOne(x => x.HoaDonNhap).WithMany().HasForeignKey(fk => fk.IdHoaDonNhap);
            //builder.HasOne(x => x.SanPham).WithMany().HasForeignKey(fk => fk.IdSanPham);
            builder.HasOne(x => x.MauSanPham).WithMany().HasForeignKey(fk => fk.IdMauSanPham);
            //builder.HasOne(x => x.SizeSanPham).WithMany().HasForeignKey(fk => fk.IdSizeSanPham);
        }

       
    }

}
