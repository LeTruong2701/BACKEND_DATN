using Microsoft.EntityFrameworkCore.Migrations;

namespace BE_DATN.Data.Migrations
{
    public partial class addTrangThaiTableKMandNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "News",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrangThai",
                table: "KhuyenMai",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "KhuyenMai");
        }
    }
}
