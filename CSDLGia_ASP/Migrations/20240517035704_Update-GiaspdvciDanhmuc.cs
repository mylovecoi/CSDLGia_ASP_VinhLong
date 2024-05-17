using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGiaspdvciDanhmuc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "GiaSpDvCongIchNhom",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Madv",
                table: "GiaSpDvCongIchDm",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Madv",
                table: "GiaSpDvCongIchNhom");

            migrationBuilder.DropColumn(
                name: "Madv",
                table: "GiaSpDvCongIchDm");
        }
    }
}
