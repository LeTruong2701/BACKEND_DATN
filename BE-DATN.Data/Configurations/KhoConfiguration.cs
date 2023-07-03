using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class KhoConfiguration : IEntityTypeConfiguration<Kho>
    {
        public void Configure(EntityTypeBuilder<Kho> builder)
        {
            builder.ToTable("Kho");
            builder.HasKey(x => x.IdKho);
            builder.Property(x => x.IdSanPham).IsRequired();
            builder.Property(x => x.IdMauSanPham).IsRequired();
            builder.Property(x => x.IdSizeSanPham).IsRequired();
            builder.Property(x => x.SoLuong).IsRequired();
            
        }
    }
}
