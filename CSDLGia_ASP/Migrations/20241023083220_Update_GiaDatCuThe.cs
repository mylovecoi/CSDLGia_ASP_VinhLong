using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class Update_GiaDatCuThe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaDatCuTheDm");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheNhom");

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheDmPPDGDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenpp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheDmPPDGDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheDmPPDGDatCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mapp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SapXep = table.Column<double>(type: "float", nullable: false),
                    HienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Noidungcv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhapGia = table.Column<bool>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheDmPPDGDatCt", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaDatCuTheDmPPDGDat");

            migrationBuilder.DropTable(
                name: "GiaDatCuTheDmPPDGDatCt");

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhapGia = table.Column<bool>(type: "bit", nullable: false),
                    Noidungcv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SapXep = table.Column<double>(type: "float", nullable: false),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheDm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaDatCuTheNhom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaDatCuTheNhom", x => x.Id);
                });
        }
    }
}
