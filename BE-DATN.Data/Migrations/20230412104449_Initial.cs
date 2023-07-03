using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BE_DATN.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DanhMuc",
                columns: table => new
                {
                    IdDanhMuc = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDanhMucCha = table.Column<int>(nullable: true),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMuc", x => x.IdDanhMuc);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    IdKhachHang = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhachHang = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.IdKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMai",
                columns: table => new
                {
                    IdKhuyenMai = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhuyenMai = table.Column<string>(nullable: false),
                    TenKhuyenMai = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    MoTa = table.Column<string>(type: "ntext", nullable: true),
                    PhanTramGiam = table.Column<decimal>(nullable: false),
                    NgayBatDau = table.Column<DateTime>(nullable: false),
                    NgayKetThuc = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMai", x => x.IdKhuyenMai);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    IdNguoiDung = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.IdNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCap",
                columns: table => new
                {
                    IdNhaCungCap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    SDT = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCap", x => x.IdNhaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    IdNhanVien = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhanVien = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    NgaySinh = table.Column<DateTime>(nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    NgayVaoLam = table.Column<DateTime>(nullable: true),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.IdNhanVien);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    IdThuongHieu = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MoTa = table.Column<string>(type: "ntext", nullable: true),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.IdThuongHieu);
                });

            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    IdDonHang = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKhachHang = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    DiaChiGiaoHang = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    NgayDat = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    TrangThaiDonHang = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TongTien = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.IdDonHang);
                    table.ForeignKey(
                        name: "FK_DonHang_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    IdAccount = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNguoiDung = table.Column<int>(nullable: true),
                    IdKhachHang = table.Column<int>(nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    PassWord = table.Column<string>(type: "varchar(100)", nullable: true),
                    LoaiQuyen = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.IdAccount);
                    table.ForeignKey(
                        name: "FK_Account_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "IdKhachHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Account_NguoiDung_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "IdNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    IdNews = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiTin = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    NoiDung = table.Column<string>(type: "ntext", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IdNguoiDung = table.Column<int>(nullable: false),
                    NgayDang = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.IdNews);
                    table.ForeignKey(
                        name: "FK_News_NguoiDung_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "IdNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonNhap",
                columns: table => new
                {
                    IdHoaDonNhap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdNhaCungCap = table.Column<int>(nullable: false),
                    IdNguoiDung = table.Column<int>(nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    TongTien = table.Column<string>(nullable: false),
                    NgayNhap = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    TrangThaiHoaDonNhap = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonNhap", x => x.IdHoaDonNhap);
                    table.ForeignKey(
                        name: "FK_HoaDonNhap_NguoiDung_IdNguoiDung",
                        column: x => x.IdNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "IdNguoiDung",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDonNhap_NhaCungCap_IdNhaCungCap",
                        column: x => x.IdNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "IdNhaCungCap",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    IdSanPham = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDanhMuc = table.Column<int>(nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MoTaSanPham = table.Column<string>(type: "ntext", nullable: true),
                    AnhSanPham = table.Column<string>(type: "varchar(500)", nullable: true),
                    IdThuongHieu = table.Column<int>(nullable: false),
                    NgayTao = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.IdSanPham);
                    table.ForeignKey(
                        name: "FK_SanPham_DanhMuc_IdDanhMuc",
                        column: x => x.IdDanhMuc,
                        principalTable: "DanhMuc",
                        principalColumn: "IdDanhMuc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_ThuongHieu_IdThuongHieu",
                        column: x => x.IdThuongHieu,
                        principalTable: "ThuongHieu",
                        principalColumn: "IdThuongHieu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MauSanPham",
                columns: table => new
                {
                    IdMauSanPham = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSanPham = table.Column<int>(nullable: false),
                    TenMau = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MaMau = table.Column<string>(type: "varchar(50)", nullable: true),
                    GiaNhap = table.Column<decimal>(type: "decimal", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal", nullable: false),
                    AnhSanPham = table.Column<string>(type: "varchar(500)", nullable: true),
                    CheckThanhToan = table.Column<int>(nullable: false, defaultValue: 0),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSanPham", x => x.IdMauSanPham);
                    table.ForeignKey(
                        name: "FK_MauSanPham_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "IdSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDonHang",
                columns: table => new
                {
                    IdChiTietDonHang = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDonHang = table.Column<int>(nullable: false),
                    IdSanPham = table.Column<int>(nullable: false),
                    IdMauSanPham = table.Column<int>(nullable: false),
                    IdSizeSanPham = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    GiaMua = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDonHang", x => x.IdChiTietDonHang);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_DonHang_IdDonHang",
                        column: x => x.IdDonHang,
                        principalTable: "DonHang",
                        principalColumn: "IdDonHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDonHang_MauSanPham_IdMauSanPham",
                        column: x => x.IdMauSanPham,
                        principalTable: "MauSanPham",
                        principalColumn: "IdMauSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDonNhap",
                columns: table => new
                {
                    IdChiTietHoaDonNhap = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHoaDonNhap = table.Column<int>(nullable: false),
                    IdSanPham = table.Column<int>(nullable: false),
                    IdMauSanPham = table.Column<int>(nullable: false),
                    IdSizeSanPham = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false),
                    GiaNhap = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDonNhap", x => x.IdChiTietHoaDonNhap);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDonNhap_HoaDonNhap_IdHoaDonNhap",
                        column: x => x.IdHoaDonNhap,
                        principalTable: "HoaDonNhap",
                        principalColumn: "IdHoaDonNhap",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDonNhap_MauSanPham_IdMauSanPham",
                        column: x => x.IdMauSanPham,
                        principalTable: "MauSanPham",
                        principalColumn: "IdMauSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SizeSanPham",
                columns: table => new
                {
                    IdSizeSanPham = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSanPham = table.Column<int>(nullable: false),
                    IdMauSanPham = table.Column<int>(nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    SoLuong = table.Column<int>(nullable: false),
                    TrangThai = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeSanPham", x => x.IdSizeSanPham);
                    table.ForeignKey(
                        name: "FK_SizeSanPham_MauSanPham_IdMauSanPham",
                        column: x => x.IdMauSanPham,
                        principalTable: "MauSanPham",
                        principalColumn: "IdMauSanPham",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_IdKhachHang",
                table: "Account",
                column: "IdKhachHang",
                unique: true,
                filter: "[IdKhachHang] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Account_IdNguoiDung",
                table: "Account",
                column: "IdNguoiDung",
                unique: true,
                filter: "[IdNguoiDung] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_IdDonHang",
                table: "ChiTietDonHang",
                column: "IdDonHang");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDonHang_IdMauSanPham",
                table: "ChiTietDonHang",
                column: "IdMauSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDonNhap_IdHoaDonNhap",
                table: "ChiTietHoaDonNhap",
                column: "IdHoaDonNhap");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDonNhap_IdMauSanPham",
                table: "ChiTietHoaDonNhap",
                column: "IdMauSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_IdKhachHang",
                table: "DonHang",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonNhap_IdNguoiDung",
                table: "HoaDonNhap",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonNhap_IdNhaCungCap",
                table: "HoaDonNhap",
                column: "IdNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_MauSanPham_IdSanPham",
                table: "MauSanPham",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_News_IdNguoiDung",
                table: "News",
                column: "IdNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdDanhMuc",
                table: "SanPham",
                column: "IdDanhMuc");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdThuongHieu",
                table: "SanPham",
                column: "IdThuongHieu");

            migrationBuilder.CreateIndex(
                name: "IX_SizeSanPham_IdMauSanPham",
                table: "SizeSanPham",
                column: "IdMauSanPham");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "ChiTietDonHang");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDonNhap");

            migrationBuilder.DropTable(
                name: "KhuyenMai");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "SizeSanPham");

            migrationBuilder.DropTable(
                name: "DonHang");

            migrationBuilder.DropTable(
                name: "HoaDonNhap");

            migrationBuilder.DropTable(
                name: "MauSanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "NhaCungCap");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "DanhMuc");

            migrationBuilder.DropTable(
                name: "ThuongHieu");
        }
    }
}
