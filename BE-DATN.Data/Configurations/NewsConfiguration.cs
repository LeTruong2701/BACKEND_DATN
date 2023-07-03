using BE_DATN.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BE_DATN.Data.Configurations
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");
            builder.HasKey(x => x.IdNews);
            builder.Property(x => x.LoaiTin).IsRequired(false).HasColumnType("nvarchar(100)");
            builder.Property(x => x.Title).IsRequired().HasColumnType("nvarchar(500)");
            builder.Property(x => x.NoiDung).IsRequired().HasColumnType("ntext");
            builder.Property(x => x.Anh).IsRequired(false).HasColumnType("nvarchar(100)");
            builder.Property(x => x.IdNguoiDung).IsRequired();
            builder.Property(x => x.NgayDang).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.NguoiDung).WithMany().HasForeignKey(fk => fk.IdNguoiDung);

        }


    }

}
