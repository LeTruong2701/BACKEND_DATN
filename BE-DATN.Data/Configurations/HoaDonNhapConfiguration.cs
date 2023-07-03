using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class HoaDonNhapConfiguration : IEntityTypeConfiguration<HoaDonNhap>
    {
        public void Configure(EntityTypeBuilder<HoaDonNhap> builder)
        {
            builder.ToTable("HoaDonNhap");
            builder.HasKey(x => x.IdHoaDonNhap);
            builder.Property(x => x.IdNhaCungCap).IsRequired();
            builder.Property(x => x.IdNguoiDung).IsRequired();
            builder.Property(x => x.NgayNhap).HasDefaultValueSql("getdate()");
            builder.Property(x => x.NgayCapNhat).HasDefaultValueSql("getdate()");

            builder.Property(x => x.GhiChu).IsRequired(false).HasColumnType("nvarchar(500)");


            builder.Property(x => x.TrangThaiHoaDonNhap).IsRequired();
            builder.Property(x => x.TongTien).IsRequired();

            //builder.HasOne(x => x.NhaCungCap).WithMany(x => x.HoaDonNhaps).HasForeignKey(x => x.IdNhaCungCap);
            builder.HasOne(x => x.NhaCungCap).WithMany().HasForeignKey(fk => fk.IdNhaCungCap);
            builder.HasOne(x => x.NguoiDung).WithMany().HasForeignKey(x => x.IdNguoiDung);


        }


    }

}
