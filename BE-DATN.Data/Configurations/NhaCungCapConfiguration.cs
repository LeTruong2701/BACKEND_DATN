using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class NhaCungCapConfiguration : IEntityTypeConfiguration<NhaCungCap>
    {
        public void Configure(EntityTypeBuilder<NhaCungCap> builder)
        {
            builder.ToTable("NhaCungCap");
            builder.HasKey(x => x.IdNhaCungCap);
            builder.Property(x => x.TenNhaCungCap).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(x => x.DiaChi).IsRequired().HasColumnType("nvarchar(500)");

            builder.Property(x => x.SDT).IsRequired(false).HasColumnType("varchar(20)");
            builder.Property(x => x.Email).IsRequired(false).HasColumnType("varchar(50)");
        }

       
    }

}
