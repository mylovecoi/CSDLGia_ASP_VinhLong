using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class edit_giamuataisan3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "GiaMuaTaiSanCt",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "GiaMuaTaiSanCt");
        }
    }
}
