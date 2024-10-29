using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolelist3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "STTSapXep",
                table: "RoleList",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "STTSapXep",
                table: "RoleList");
        }
    }
}
