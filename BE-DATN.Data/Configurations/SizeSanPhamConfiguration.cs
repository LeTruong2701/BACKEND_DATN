using BE_DATN.Data.Entities;
using BE_DATN.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    internal class SizeSanPhamConfiguration : IEntityTypeConfiguration<SizeSanPham>
    {
        public void Configure(EntityTypeBuilder<SizeSanPham> builder)
        {
            builder.ToTable("SizeSanPham");
            builder.HasKey(x => x.IdSizeSanPham);
            builder.Property(x => x.IdSanPham);
            builder.Property(x => x.IdMauSanPham);
            builder.Property(x => x.Size).HasColumnType("nvarchar(10)");
            builder.Property(x => x.SoLuong);

            builder.Property(x => x.TrangThai);

            //builder.HasOne(x => x.MauSanPham).WithMany(x => x.SizeSanPhams).HasForeignKey(x => x.IdMauSanPham);
            //builder.HasOne(x => x.SanPham).WithMany().HasForeignKey(fk => fk.IdSanPham);
            builder.HasOne(x => x.MauSanPham).WithMany().HasForeignKey(fk => fk.IdMauSanPham);

        }

       
    }

}
