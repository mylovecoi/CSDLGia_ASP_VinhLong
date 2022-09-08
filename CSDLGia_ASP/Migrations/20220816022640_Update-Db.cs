using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaBanHd");

            migrationBuilder.DropTable(
                name: "DsDonViTdg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiaBanHd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diaban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaBanHd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DsDonViTdg",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaycap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nguoidaidien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sothe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsDonViTdg", x => x.Id);
                });
        }
    }
}
