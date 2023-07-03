using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class DonHangConfiguration : IEntityTypeConfiguration<DonHang>
    {
        public void Configure(EntityTypeBuilder<DonHang> builder)
        {
            builder.ToTable("DonHang");
            builder.HasKey(x => x.IdDonHang);
            builder.Property(x => x.IdKhachHang).IsRequired(false);
            builder.Property(x => x.TenKhachHang).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.DiaChi).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.SDT).IsRequired().HasColumnType("varchar(20)");

            builder.Property(x => x.GhiChu).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.DiaChiGiaoHang).IsRequired().HasColumnType("nvarchar(500)");
            builder.Property(x => x.NgayDat).HasDefaultValueSql("getdate()");
            builder.Property(x => x.NgayNhanHang).IsRequired(false);
            builder.Property(x => x.TrangThaiDonHang).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(x => x.PhuongThucThanhToan).IsRequired().HasColumnType("nvarchar(50)");
            builder.Property(x => x.TrangThaiThanhToan).IsRequired();
            builder.Property(x => x.TongTien).IsRequired();
            builder.Property(x => x.MaKhuyenMai).IsRequired(false).HasColumnType("nvarchar(100)");

            //builder.HasOne(x => x.KhachHang).WithMany(x => x.DonHangs).HasForeignKey(x => x.IdKhachHang);
            builder.HasOne(x => x.KhachHang).WithMany().HasForeignKey(fk => fk.IdKhachHang);

        }

      
    }

}
