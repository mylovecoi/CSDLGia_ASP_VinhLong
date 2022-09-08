using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddVbQlNn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VbQlNn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kyhieuvb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dvbanhanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Loaivb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ngaybanhanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ngayapdung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tieude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ipf5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Madv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thoidiem = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VbQlNn", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VbQlNn");
        }
    }
}
