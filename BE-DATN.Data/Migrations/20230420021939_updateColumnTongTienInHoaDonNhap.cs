using Microsoft.EntityFrameworkCore.Migrations;

namespace BE_DATN.Data.Migrations
{
    public partial class updateColumnTongTienInHoaDonNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TongTien",
                table: "HoaDonNhap",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TongTien",
                table: "HoaDonNhap",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
