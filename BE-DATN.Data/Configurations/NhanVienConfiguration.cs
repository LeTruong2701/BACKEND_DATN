using BE_DATN.Data.Entities;
using BE_DATN.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class NhanVienConfiguration : IEntityTypeConfiguration<NhanVien>
    {
        public void Configure(EntityTypeBuilder<NhanVien> builder)
        {
            builder.ToTable("NhanVien");
            builder.HasKey(x => x.IdNhanVien);
            builder.Property(x => x.TenNhanVien).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(x => x.GioiTinh).IsRequired(false).HasColumnType("nvarchar(50)");
            builder.Property(x => x.NgaySinh).IsRequired(false);
            builder.Property(x => x.DiaChi).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.SDT).IsRequired(false).HasColumnType("varchar(20)");
            builder.Property(x => x.Email).IsRequired(false).HasColumnType("varchar(50)");
            builder.Property(x => x.NgayVaoLam).IsRequired(false);
            builder.Property(x => x.TrangThai);
        }

      
    }

}
