using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class editgiamuataisandm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manhom",
                table: "GiaMuaTaiSanDm");

            migrationBuilder.DropColumn(
                name: "Phanloai",
                table: "GiaMuaTaiSanDm");

            migrationBuilder.DropColumn(
                name: "Stt",
                table: "GiaMuaTaiSanDm");

            migrationBuilder.RenameColumn(
                name: "Tennhom",
                table: "GiaMuaTaiSanDm",
                newName: "Mota");

            migrationBuilder.AddColumn<double>(
                name: "KhoiLuong",
                table: "GiaMuaTaiSanDm",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KhoiLuong",
                table: "GiaMuaTaiSanDm");

            migrationBuilder.RenameColumn(
                name: "Mota",
                table: "GiaMuaTaiSanDm",
                newName: "Tennhom");

            migrationBuilder.AddColumn<string>(
                name: "Manhom",
                table: "GiaMuaTaiSanDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phanloai",
                table: "GiaMuaTaiSanDm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stt",
                table: "GiaMuaTaiSanDm",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
