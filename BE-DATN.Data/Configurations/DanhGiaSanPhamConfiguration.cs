using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class DanhGiaSanPhamConfiguration: IEntityTypeConfiguration<DanhGiaSanPham>
    {
        public void Configure(EntityTypeBuilder<DanhGiaSanPham> builder)
        {
            builder.ToTable("DanhGiaSanPham");
            builder.HasKey(x => x.IdDanhGia);
            builder.Property(x => x.IdSanPham);
            builder.Property(x => x.IdKhachHang);
            builder.Property(x => x.NoiDungDanhGia).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.NgayDanhGia).HasDefaultValueSql("getdate()");

        }
    }
}
