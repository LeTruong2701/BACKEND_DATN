using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class ChiTietDonHangConfiguration : IEntityTypeConfiguration<ChiTietDonHang>
    {
        public void Configure(EntityTypeBuilder<ChiTietDonHang> builder)
        {
            builder.ToTable("ChiTietDonHang");
            builder.HasKey(x => x.IdChiTietDonHang);
            builder.Property(x => x.IdDonHang).IsRequired();
            builder.Property(x => x.IdSanPham).IsRequired();
            builder.Property(x => x.IdMauSanPham).IsRequired();
            builder.Property(x => x.IdSizeSanPham).IsRequired();
            builder.Property(x => x.SoLuong).IsRequired();
            builder.Property(x => x.GiaMua).IsRequired();


            

            builder.HasOne(x => x.DonHang).WithMany().HasForeignKey(fk => fk.IdDonHang);
            builder.HasOne(x => x.MauSanPham).WithMany().HasForeignKey(fk => fk.IdMauSanPham);
      

        }

        
    }

}
