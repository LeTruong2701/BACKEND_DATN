using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> builder)
        {
            builder.ToTable("KhachHang");
            builder.HasKey(x => x.IdKhachHang);
            builder.Property(x => x.TenKhachHang).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(x => x.GioiTinh).IsRequired(false).HasColumnType("nvarchar(50)");
            builder.Property(x => x.NgaySinh).IsRequired(false);
            builder.Property(x => x.NgayTao).IsRequired(false).HasDefaultValueSql("getdate()");
            builder.Property(x => x.DiaChi).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.SDT).IsRequired(false).HasColumnType("varchar(20)");
            builder.Property(x => x.Email).IsRequired(false).HasColumnType("varchar(50)");
            builder.Property(x => x.AnhDaiDien).IsRequired(false).HasColumnType("nvarchar(100)");
            builder.Property(x => x.DiaChiGiaoHang).IsRequired(false).HasColumnType("nvarchar(500)");


        }

    }

}
