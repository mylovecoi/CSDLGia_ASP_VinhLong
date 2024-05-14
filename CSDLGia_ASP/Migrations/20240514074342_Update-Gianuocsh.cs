using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGianuocsh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaNuocShCtDf");

            migrationBuilder.DropTable(
                name: "GiaNuocShDmVung");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaNuocShCtDf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Doituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giachuathue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Giacothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mahuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phibvmttyle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thanhtien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thuevat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShCtDf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaNuocShDmVung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CapDo = table.Column<int>(type: "int", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Doituongsd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaSoGoc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STT = table.Column<int>(type: "int", nullable: false),
                    SttHienthi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaNuocShDmVung", x => x.Id);
                });
        }
    }
}
