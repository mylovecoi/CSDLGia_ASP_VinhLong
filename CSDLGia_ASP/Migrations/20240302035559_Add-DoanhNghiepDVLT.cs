using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddDoanhNghiepDVLT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoSoKinhDoanhDVLT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    masothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tencskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loaihang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diachikd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    toado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoSoKinhDoanhDVLT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoanhNghiepDVLT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tendn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    masothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diachidn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    teldn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    faxdn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    noidknopthue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giayphepkd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chucdanhky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nguoiky = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diadanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tailieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoanhNghiepDVLT", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoSoKinhDoanhDVLT");

            migrationBuilder.DropTable(
                name: "DoanhNghiepDVLT");
        }
    }
}
