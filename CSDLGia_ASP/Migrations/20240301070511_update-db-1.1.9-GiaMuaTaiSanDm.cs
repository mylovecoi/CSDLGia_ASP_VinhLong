using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedb119GiaMuaTaiSanDm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiaMuaTaiSanDm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tennhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaMuaTaiSanDm", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaMuaTaiSanDm");
        }
    }
}
