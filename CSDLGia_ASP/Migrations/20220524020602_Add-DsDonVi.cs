using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddDsDonVi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DsDonVi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDiaBan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaQhNs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaDv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TtLienHe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailQl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailQt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoNgayLv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDvHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenDvCqHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVuKy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucVuKyThay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NguoiKy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiaDanh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChucNang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DsDonVi", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DsDonVi");
        }
    }
}
