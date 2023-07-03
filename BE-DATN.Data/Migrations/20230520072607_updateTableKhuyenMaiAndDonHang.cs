using Microsoft.EntityFrameworkCore.Migrations;

namespace BE_DATN.Data.Migrations
{
    public partial class updateTableKhuyenMaiAndDonHang : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DieuKienHoaDon",
                table: "KhuyenMai",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrangThaiThanhToan",
                table: "DonHang",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DieuKienHoaDon",
                table: "KhuyenMai");

            migrationBuilder.DropColumn(
                name: "TrangThaiThanhToan",
                table: "DonHang");
        }
    }
}
