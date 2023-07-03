using Microsoft.EntityFrameworkCore.Migrations;

namespace BE_DATN.Data.Migrations
{
    public partial class addTableKho : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kho",
                columns: table => new
                {
                    IdKho = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSanPham = table.Column<int>(nullable: false),
                    IdMauSanPham = table.Column<int>(nullable: false),
                    IdSizeSanPham = table.Column<int>(nullable: false),
                    SoLuong = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kho", x => x.IdKho);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kho");
        }
    }
}
