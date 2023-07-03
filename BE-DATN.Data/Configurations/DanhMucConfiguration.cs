using BE_DATN.Data.Entities;
using BE_DATN.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class DanhMucConfiguration : IEntityTypeConfiguration<DanhMuc>
    {
        public void Configure(EntityTypeBuilder<DanhMuc> builder)
        {
            builder.ToTable("DanhMuc");
            builder.HasKey(x => x.IdDanhMuc);
            builder.Property(x => x.IdDanhMucCha).IsRequired(false);
            builder.Property(x => x.TenDanhMuc).IsRequired().HasColumnType("nvarchar(250)");
            builder.Property(x => x.MoTa).IsRequired(false).HasColumnType("nvarchar(500)");
            builder.Property(x => x.TrangThai);

        }

        
    }

}
