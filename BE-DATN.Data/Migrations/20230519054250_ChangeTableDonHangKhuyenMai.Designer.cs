﻿// <auto-generated />
using System;
using BE_DATN.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BE_DATN.Data.Migrations
{
    [DbContext(typeof(BEDATNDbContext))]
    [Migration("20230519054250_ChangeTableDonHangKhuyenMai")]
    partial class ChangeTableDonHangKhuyenMai
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BE_DATN.Data.Entities.Account", b =>
                {
                    b.Property<int>("IdAccount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<int?>("IdNguoiDung")
                        .HasColumnType("int");

                    b.Property<string>("LoaiQuyen")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PassWord")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(100)");

                    b.HasKey("IdAccount");

                    b.HasIndex("IdKhachHang")
                        .IsUnique()
                        .HasFilter("[IdKhachHang] IS NOT NULL");

                    b.HasIndex("IdNguoiDung")
                        .IsUnique()
                        .HasFilter("[IdNguoiDung] IS NOT NULL");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.ChiTietDonHang", b =>
                {
                    b.Property<int>("IdChiTietDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("GiaMua")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdDonHang")
                        .HasColumnType("int");

                    b.Property<int>("IdMauSanPham")
                        .HasColumnType("int");

                    b.Property<int>("IdSanPham")
                        .HasColumnType("int");

                    b.Property<int>("IdSizeSanPham")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("IdChiTietDonHang");

                    b.HasIndex("IdDonHang");

                    b.HasIndex("IdMauSanPham");

                    b.ToTable("ChiTietDonHang");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.ChiTietHoaDonNhap", b =>
                {
                    b.Property<int>("IdChiTietHoaDonNhap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("GiaNhap")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdHoaDonNhap")
                        .HasColumnType("int");

                    b.Property<int>("IdMauSanPham")
                        .HasColumnType("int");

                    b.Property<int>("IdSanPham")
                        .HasColumnType("int");

                    b.Property<int>("IdSizeSanPham")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("IdChiTietHoaDonNhap");

                    b.HasIndex("IdHoaDonNhap");

                    b.HasIndex("IdMauSanPham");

                    b.ToTable("ChiTietHoaDonNhap");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.DanhMuc", b =>
                {
                    b.Property<int>("IdDanhMuc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdDanhMucCha")
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("TenDanhMuc")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdDanhMuc");

                    b.ToTable("DanhMuc");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.DonHang", b =>
                {
                    b.Property<int>("IdDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("DiaChiGiaoHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<string>("MaKhuyenMai")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NgayDat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TenKhachHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("TongTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TrangThaiDonHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("IdDonHang");

                    b.HasIndex("IdKhachHang");

                    b.ToTable("DonHang");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.HoaDonNhap", b =>
                {
                    b.Property<int>("IdHoaDonNhap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("IdNguoiDung")
                        .HasColumnType("int");

                    b.Property<int>("IdNhaCungCap")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayCapNhat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("NgayNhap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<decimal>("TongTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TrangThaiHoaDonNhap")
                        .HasColumnType("int");

                    b.HasKey("IdHoaDonNhap");

                    b.HasIndex("IdNguoiDung");

                    b.HasIndex("IdNhaCungCap");

                    b.ToTable("HoaDonNhap");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.KhachHang", b =>
                {
                    b.Property<int>("IdKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnhDaiDien")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("DiaChiGiaoHang")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TenKhachHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("IdKhachHang");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.Kho", b =>
                {
                    b.Property<int>("IdKho")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdMauSanPham")
                        .HasColumnType("int");

                    b.Property<int>("IdSanPham")
                        .HasColumnType("int");

                    b.Property<int>("IdSizeSanPham")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("IdKho");

                    b.ToTable("Kho");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.KhuyenMai", b =>
                {
                    b.Property<int>("IdKhuyenMai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("GiaTienGiam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MaKhuyenMai")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MoTa")
                        .HasColumnType("ntext");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("PhanTramGiam")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TenKhuyenMai")
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdKhuyenMai");

                    b.ToTable("KhuyenMai");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.MauSanPham", b =>
                {
                    b.Property<int>("IdMauSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnhSanPham")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("CheckThanhToan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal");

                    b.Property<decimal>("GiaNhap")
                        .HasColumnType("decimal");

                    b.Property<int>("IdSanPham")
                        .HasColumnType("int");

                    b.Property<string>("MaMau")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TenMau")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdMauSanPham");

                    b.HasIndex("IdSanPham");

                    b.ToTable("MauSanPham");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.News", b =>
                {
                    b.Property<int>("IdNews")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anh")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdNguoiDung")
                        .HasColumnType("int");

                    b.Property<string>("LoaiTin")
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("NgayDang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdNews");

                    b.HasIndex("IdNguoiDung");

                    b.ToTable("News");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.NguoiDung", b =>
                {
                    b.Property<int>("IdNguoiDung")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnhDaiDien")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .HasColumnType("varchar(20)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdNguoiDung");

                    b.ToTable("NguoiDung");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.NhaCungCap", b =>
                {
                    b.Property<int>("IdNhaCungCap")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SDT")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TenNhaCungCap")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("IdNhaCungCap");

                    b.ToTable("NhaCungCap");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.NhanVien", b =>
                {
                    b.Property<int>("IdNhanVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("GioiTinh")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("NgayVaoLam")
                        .HasColumnType("datetime2");

                    b.Property<string>("SDT")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("TenNhanVien")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdNhanVien");

                    b.ToTable("NhanVien");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.SanPham", b =>
                {
                    b.Property<int>("IdSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnhSanPham")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("ChatLieu")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("IdDanhMuc")
                        .HasColumnType("int");

                    b.Property<int>("IdThuongHieu")
                        .HasColumnType("int");

                    b.Property<string>("MoTaSanPham")
                        .HasColumnType("ntext");

                    b.Property<DateTime>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.Property<string>("XuatXu")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("IdSanPham");

                    b.HasIndex("IdDanhMuc");

                    b.HasIndex("IdThuongHieu");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.SizeSanPham", b =>
                {
                    b.Property<int>("IdSizeSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdMauSanPham")
                        .HasColumnType("int");

                    b.Property<int>("IdSanPham")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdSizeSanPham");

                    b.HasIndex("IdMauSanPham");

                    b.ToTable("SizeSanPham");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.ThuongHieu", b =>
                {
                    b.Property<int>("IdThuongHieu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MoTa")
                        .HasColumnType("ntext");

                    b.Property<string>("TenThuongHieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("TrangThai")
                        .HasColumnType("int");

                    b.HasKey("IdThuongHieu");

                    b.ToTable("ThuongHieu");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.Users", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("IdKhachHang")
                        .HasColumnType("int");

                    b.Property<int?>("IdNguoiDung")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.Roles", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Roles");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.Account", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.KhachHang", "KhachHang")
                        .WithOne()
                        .HasForeignKey("BE_DATN.Data.Entities.Account", "IdKhachHang");

                    b.HasOne("BE_DATN.Data.Entities.NguoiDung", "NguoiDung")
                        .WithOne()
                        .HasForeignKey("BE_DATN.Data.Entities.Account", "IdNguoiDung");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.ChiTietDonHang", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.DonHang", "DonHang")
                        .WithMany()
                        .HasForeignKey("IdDonHang")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_DATN.Data.Entities.MauSanPham", "MauSanPham")
                        .WithMany()
                        .HasForeignKey("IdMauSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.ChiTietHoaDonNhap", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.HoaDonNhap", "HoaDonNhap")
                        .WithMany()
                        .HasForeignKey("IdHoaDonNhap")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_DATN.Data.Entities.MauSanPham", "MauSanPham")
                        .WithMany()
                        .HasForeignKey("IdMauSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.DonHang", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("IdKhachHang");
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.HoaDonNhap", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("IdNguoiDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_DATN.Data.Entities.NhaCungCap", "NhaCungCap")
                        .WithMany()
                        .HasForeignKey("IdNhaCungCap")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.MauSanPham", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("IdSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.News", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.NguoiDung", "NguoiDung")
                        .WithMany()
                        .HasForeignKey("IdNguoiDung")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.SanPham", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.DanhMuc", "DanhMuc")
                        .WithMany()
                        .HasForeignKey("IdDanhMuc")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_DATN.Data.Entities.ThuongHieu", "ThuongHieu")
                        .WithMany()
                        .HasForeignKey("IdThuongHieu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BE_DATN.Data.Entities.SizeSanPham", b =>
                {
                    b.HasOne("BE_DATN.Data.Entities.MauSanPham", "MauSanPham")
                        .WithMany()
                        .HasForeignKey("IdMauSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
