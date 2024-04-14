using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatekkdvluhanh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KkGiaLuHanhCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendvcu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoigian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dodaitgian = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gialk = table.Column<double>(type: "float", nullable: false),
                    Giakk = table.Column<double>(type: "float", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KkGiaLuHanhCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KkGiaLuHanhCt");
        }
    }
}
