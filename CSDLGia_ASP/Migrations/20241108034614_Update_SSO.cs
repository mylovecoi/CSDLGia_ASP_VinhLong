using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class Update_SSO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SSO",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SSOCode",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSOGetToken",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SSOGetUserInfo",
                table: "tblHeThong",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSO",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SSOCode",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "SSOGetToken",
                table: "tblHeThong");

            migrationBuilder.DropColumn(
                name: "SSOGetUserInfo",
                table: "tblHeThong");
        }
    }
}
