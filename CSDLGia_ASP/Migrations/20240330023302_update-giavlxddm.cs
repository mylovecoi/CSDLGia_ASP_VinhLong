using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updategiavlxddm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaVatLieuXayDungNhom");

            migrationBuilder.DropColumn(
                name: "Cap1",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.DropColumn(
                name: "Cap2",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.DropColumn(
                name: "Cap3",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.DropColumn(
                name: "Cap4",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.DropColumn(
                name: "Cap5",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.DropColumn(
                name: "GhiChu",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaVatLieuXayDungDm");

            migrationBuilder.RenameColumn(
                name: "Theodoi",
                table: "GiaVatLieuXayDungDm",
                newName: "Tieuchuan");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "GiaVatLieuXayDungDm",
                newName: "Tenvlxd");

            migrationBuilder.RenameColumn(
                name: "Sapxep",
                table: "GiaVatLieuXayDungDm",
                newName: "Mavlxd");

            migrationBuilder.RenameColumn(
                name: "TieuChuan",
                table: "GiaVatLieuXayDungCt",
                newName: "Tieuchuan");

            migrationBuilder.RenameColumn(
                name: "Ten",
                table: "GiaVatLieuXayDungCt",
                newName: "Tenvlxd");

            migrationBuilder.AddColumn<string>(
                name: "Mavlxd",
                table: "GiaVatLieuXayDungCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mavlxd",
                table: "GiaVatLieuXayDungCt");

            migrationBuilder.RenameColumn(
                name: "Tieuchuan",
                table: "GiaVatLieuXayDungDm",
                newName: "Theodoi");

            migrationBuilder.RenameColumn(
                name: "Tenvlxd",
                table: "GiaVatLieuXayDungDm",
                newName: "Ten");

            migrationBuilder.RenameColumn(
                name: "Mavlxd",
                table: "GiaVatLieuXayDungDm",
                newName: "Sapxep");

            migrationBuilder.RenameColumn(
                name: "Tieuchuan",
                table: "GiaVatLieuXayDungCt",
                newName: "TieuChuan");

            migrationBuilder.RenameColumn(
                name: "Tenvlxd",
                table: "GiaVatLieuXayDungCt",
                newName: "Ten");

            migrationBuilder.AddColumn<string>(
                name: "Cap1",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap2",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap3",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap4",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cap5",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GhiChu",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaVatLieuXayDungDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiaVatLieuXayDungNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sapxep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaVatLieuXayDungNhom", x => x.Id);
                });
        }
    }
}
