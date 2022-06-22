using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class fixGiaDatDiaBanCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Giavt1",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt2",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt3",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt4",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt5",
                table: "GiaDatDiaBanCt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Giavt1",
                table: "GiaDatDiaBanCt",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Giavt2",
                table: "GiaDatDiaBanCt",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Giavt3",
                table: "GiaDatDiaBanCt",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Giavt4",
                table: "GiaDatDiaBanCt",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Giavt5",
                table: "GiaDatDiaBanCt",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
