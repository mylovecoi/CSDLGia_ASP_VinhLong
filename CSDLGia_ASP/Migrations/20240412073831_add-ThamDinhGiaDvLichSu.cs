using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class addThamDinhGiaDvLichSu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThamDinhGiaDvLichSu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDV = table.Column<int>(type: "int", nullable: false),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoQD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayQD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileQD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Theodoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThamDinhGiaDvLichSu", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThamDinhGiaDvLichSu");
        }
    }
}
