using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddHoSoKeKhaiDVLT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "DoanhNghiepDVLT");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "DoanhNghiepDVLT");

            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "CoSoKinhDoanhDVLT");

            migrationBuilder.DropColumn(
                name: "Updated_at",
                table: "CoSoKinhDoanhDVLT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DoanhNghiepDVLT",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CoSoKinhDoanhDVLT",
                newName: "id");

            migrationBuilder.CreateTable(
                name: "HoSoKeKhaiGia",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    masothue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaynhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    socv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    socvlk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaycvlk = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ngayhieuluc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ttnguoinop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaynhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sohsnhan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaychuyen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lydo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trangthai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cqcq = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dvt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    plhs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tencskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loaihang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thoihan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giaycnhangcs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filedk1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filedk2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filedk3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    soqdgiaycnhangcs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    giaycnhangcstungay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    giaycnhangcsdenngaypublic = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoKeKhaiGia", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HoSoKeKhaiGia_ChiTiet",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    macskd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mahs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    maloaip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loaip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    qccl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sohieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mucgialk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mucgiakk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tendoituong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    apdungpublic = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoSoKeKhaiGia_ChiTiet", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoSoKeKhaiGia");

            migrationBuilder.DropTable(
                name: "HoSoKeKhaiGia_ChiTiet");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DoanhNghiepDVLT",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CoSoKinhDoanhDVLT",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "DoanhNghiepDVLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "DoanhNghiepDVLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "CoSoKinhDoanhDVLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "CoSoKinhDoanhDVLT",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
