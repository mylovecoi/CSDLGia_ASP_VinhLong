using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updateadddmnuocsachshkhung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaNuocShDm");

            migrationBuilder.CreateTable(
                name: "GiaNuocShDmKhung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShDmKhung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaNuocShDmVung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShDmVung", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaNuocShDmKhung");

            migrationBuilder.DropTable(
                name: "GiaNuocShDmVung");

            migrationBuilder.CreateTable(
                name: "GiaNuocShDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShDm", x => x.Id);
                });
        }
    }
}
