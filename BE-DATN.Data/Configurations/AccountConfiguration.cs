using BE_DATN.Data.Entities;
using BE_DATN.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account");
            builder.HasKey(x => x.IdAccount);
            builder.Property(x => x.IdNguoiDung).IsRequired(false);
            builder.Property(x => x.IdKhachHang).IsRequired(false);
            builder.Property(x => x.UserName).IsRequired(false).HasColumnType("varchar(100)");
            builder.Property(x => x.PassWord).IsRequired(false).HasColumnType("varchar(100)");
            builder.Property(x => x.LoaiQuyen).IsRequired(false).HasColumnType("nvarchar(50)");
            builder.Property(x => x.TrangThai);


            builder.HasOne(x => x.NguoiDung).WithOne().HasForeignKey<Account>(x => x.IdNguoiDung);
            builder.HasOne(x => x.KhachHang).WithOne().HasForeignKey<Account>(x => x.IdKhachHang);

        }
    }
}
