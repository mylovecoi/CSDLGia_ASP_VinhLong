using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixModelGiaRung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GiaBoiThuong1",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaBoiThuong2",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaBoiThuong3",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaBoiThuong4",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaBoiThuong5",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaBoiThuong6",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaChoThue1",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaChoThue2",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaRung1",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaRung2",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaRung3",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaRung4",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaRung5",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GiaRung6",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "STTHienThi",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "STTSapXep",
                table: "GiaRungCt",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaRungCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GiaRungDmCt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manhom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STTSapXep = table.Column<int>(type: "int", nullable: false),
                    STTHienThi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaRungDmCt", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiaRungDmCt");

            migrationBuilder.DropColumn(
                name: "GiaBoiThuong1",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaBoiThuong2",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaBoiThuong3",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaBoiThuong4",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaBoiThuong5",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaBoiThuong6",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaChoThue1",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaChoThue2",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaRung1",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaRung2",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaRung3",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaRung4",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaRung5",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "GiaRung6",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "STTHienThi",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "STTSapXep",
                table: "GiaRungCt");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaRungCt");
        }
    }
}
