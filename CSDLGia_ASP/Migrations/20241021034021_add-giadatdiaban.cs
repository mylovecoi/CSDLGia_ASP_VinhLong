using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class addgiadatdiaban : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Giavt6",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giavt7",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Giavtconlai",
                table: "GiaDatDiaBanCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Giavt6",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavt7",
                table: "GiaDatDiaBanCt");

            migrationBuilder.DropColumn(
                name: "Giavtconlai",
                table: "GiaDatDiaBanCt");
        }
    }
}
