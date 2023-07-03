using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BE_DATN.Data.Migrations
{
    public partial class addColumnNgayCapNhattableHoaDonNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCapNhat",
                table: "HoaDonNhap",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayCapNhat",
                table: "HoaDonNhap");
        }
    }
}
