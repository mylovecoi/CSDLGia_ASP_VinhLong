using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class GiasptoidaCtaddGiatoidatheoculy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GiaToiDaTheoCuLy",
                table: "GiaSpDvToiDaCt",
                newName: "GiaToiDaTheoCuLy4");

            migrationBuilder.AddColumn<double>(
                name: "GiaToiDaTheoCuLy1",
                table: "GiaSpDvToiDaCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GiaToiDaTheoCuLy2",
                table: "GiaSpDvToiDaCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "GiaToiDaTheoCuLy3",
                table: "GiaSpDvToiDaCt",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiaToiDaTheoCuLy1",
                table: "GiaSpDvToiDaCt");

            migrationBuilder.DropColumn(
                name: "GiaToiDaTheoCuLy2",
                table: "GiaSpDvToiDaCt");

            migrationBuilder.DropColumn(
                name: "GiaToiDaTheoCuLy3",
                table: "GiaSpDvToiDaCt");

            migrationBuilder.RenameColumn(
                name: "GiaToiDaTheoCuLy4",
                table: "GiaSpDvToiDaCt",
                newName: "GiaToiDaTheoCuLy");
        }
    }
}
