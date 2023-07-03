using BE_DATN.Data.Entities;
using BE_DATN.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class MauSanPhamConfiguration : IEntityTypeConfiguration<MauSanPham>
    {
        public void Configure(EntityTypeBuilder<MauSanPham> builder)
        {
            builder.ToTable("MauSanPham");
            builder.HasKey(x => x.IdMauSanPham);
            builder.Property(x => x.IdSanPham).IsRequired();
            builder.Property(x => x.TenMau).HasColumnType("nvarchar(100)");
            builder.Property(x => x.MaMau).HasColumnType("varchar(50)");
            builder.Property(x => x.GiaNhap).HasColumnType("decimal");
            builder.Property(x => x.GiaBan).HasColumnType("decimal");
            builder.Property(x => x.AnhSanPham).HasColumnType("varchar(500)");

            builder.Property(x => x.CheckThanhToan).HasDefaultValue(Status.InActive);
            builder.Property(x => x.TrangThai);

            //builder.HasOne(x => x.SanPham).WithMany(x => x.MauSanPhams).HasForeignKey(x => x.IdMauSanPham);
            builder.HasOne(x => x.SanPham).WithMany().HasForeignKey(fk => fk.IdSanPham);

        }

        
    }

}
