using BE_DATN.Data.Entities;
using BE_DATN.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class SanPhamConfiguration : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPham");
            builder.HasKey(x => x.IdSanPham);
            builder.Property(x => x.IdDanhMuc).IsRequired();
            builder.Property(x => x.TenSanPham).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(x => x.MoTaSanPham).HasColumnType("ntext");
            builder.Property(x => x.AnhSanPham).HasColumnType("varchar(500)");
            builder.Property(x => x.XuatXu).HasColumnType("nvarchar(100)");
            builder.Property(x => x.ChatLieu).HasColumnType("nvarchar(100)");
            builder.Property(x => x.IdThuongHieu).IsRequired();
            builder.Property(x => x.NgayTao).HasDefaultValueSql("getdate()");
            builder.Property(x => x.TrangThai);
            

            builder.HasOne(x => x.DanhMuc).WithMany().HasForeignKey(fk => fk.IdDanhMuc);
            builder.HasOne(x => x.ThuongHieu).WithMany().HasForeignKey(fk => fk.IdThuongHieu);

        }

       
    }

}
