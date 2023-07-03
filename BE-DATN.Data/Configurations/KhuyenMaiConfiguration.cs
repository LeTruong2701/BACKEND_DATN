using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class KhuyenMaiConfiguration : IEntityTypeConfiguration<KhuyenMai>
    {
        public void Configure(EntityTypeBuilder<KhuyenMai> builder)
        {
            builder.ToTable("KhuyenMai");
            builder.HasKey(x => x.IdKhuyenMai);
            builder.Property(x => x.MaKhuyenMai).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.TenKhuyenMai).IsRequired(false).HasColumnType("nvarchar(250)");
            builder.Property(x => x.MoTa).IsRequired(false).HasColumnType("ntext");
            builder.Property(x => x.PhanTramGiam).IsRequired(false);
            builder.Property(x => x.GiaTienGiam).IsRequired(false);
            builder.Property(x => x.DieuKienHoaDon).IsRequired(false);
            builder.Property(x => x.NgayBatDau).IsRequired();
            builder.Property(x => x.NgayKetThuc).IsRequired(false);



        }

    }

}
