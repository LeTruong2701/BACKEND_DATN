using Microsoft.EntityFrameworkCore.Migrations;

namespace BE_DATN.Data.Migrations
{
    public partial class ChangeTableDonHangKhuyenMai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_KhachHang_IdKhachHang",
                table: "DonHang");

            migrationBuilder.AlterColumn<decimal>(
                name: "PhanTramGiam",
                table: "KhuyenMai",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "MaKhuyenMai",
                table: "KhuyenMai",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "GiaTienGiam",
                table: "KhuyenMai",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdKhachHang",
                table: "DonHang",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DiaChi",
                table: "DonHang",
                type: "nvarchar(500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaKhuyenMai",
                table: "DonHang",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SDT",
                table: "DonHang",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TenKhachHang",
                table: "DonHang",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_KhachHang_IdKhachHang",
                table: "DonHang",
                column: "IdKhachHang",
                principalTable: "KhachHang",
                principalColumn: "IdKhachHang",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_KhachHang_IdKhachHang",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "GiaTienGiam",
                table: "KhuyenMai");

            migrationBuilder.DropColumn(
                name: "DiaChi",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "MaKhuyenMai",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "SDT",
                table: "DonHang");

            migrationBuilder.DropColumn(
                name: "TenKhachHang",
                table: "DonHang");

            migrationBuilder.AlterColumn<decimal>(
                name: "PhanTramGiam",
                table: "KhuyenMai",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaKhuyenMai",
                table: "KhuyenMai",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<int>(
                name: "IdKhachHang",
                table: "DonHang",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_KhachHang_IdKhachHang",
                table: "DonHang",
                column: "IdKhachHang",
                principalTable: "KhachHang",
                principalColumn: "IdKhachHang",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
