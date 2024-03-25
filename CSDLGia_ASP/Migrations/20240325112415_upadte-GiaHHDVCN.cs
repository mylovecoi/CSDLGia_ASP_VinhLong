using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class upadteGiaHHDVCN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "GiaHhDvCnDm",
                newName: "Style");

            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "GiaHhDvCnCt",
                newName: "Tenspdv");

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaHhDvCnDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaHhDvCnDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sapxep",
                table: "GiaHhDvCnDm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Dvt",
                table: "GiaHhDvCnCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaHhDvCnCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hientrang",
                table: "GiaHhDvCnCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sapxep",
                table: "GiaHhDvCnCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaHhDvCnCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiaHhDvCnNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaHhDvCnNhom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaHhDvCnNhom");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaHhDvCnDm");

            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaHhDvCnDm");

            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaHhDvCnDm");

            migrationBuilder.DropColumn(
                name: "Dvt",
                table: "GiaHhDvCnCt");

            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaHhDvCnCt");

            migrationBuilder.DropColumn(
                name: "Hientrang",
                table: "GiaHhDvCnCt");

            migrationBuilder.DropColumn(
                name: "Sapxep",
                table: "GiaHhDvCnCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaHhDvCnCt");

            migrationBuilder.RenameColumn(
                name: "Style",
                table: "GiaHhDvCnDm",
                newName: "Mota");

            migrationBuilder.RenameColumn(
                name: "Tenspdv",
                table: "GiaHhDvCnCt",
                newName: "Mota");
        }
    }
}
