using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class editgiamuataisanndm2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HienThi",
                table: "GiaMuaTaiSanDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "SapXep",
                table: "GiaMuaTaiSanDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Style",
                table: "GiaMuaTaiSanDm",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HienThi",
                table: "GiaMuaTaiSanDm");

            migrationBuilder.DropColumn(
                name: "SapXep",
                table: "GiaMuaTaiSanDm");

            migrationBuilder.DropColumn(
                name: "Style",
                table: "GiaMuaTaiSanDm");
        }
    }
}
