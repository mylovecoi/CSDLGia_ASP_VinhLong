using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class FixGroupPer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MoTa",
                table: "GroupPermissions",
                newName: "TenNhomQ");

            migrationBuilder.RenameColumn(
                name: "GroupName",
                table: "GroupPermissions",
                newName: "ChucNang");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "GroupPermissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "GroupPermissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "GroupPermissions");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "GroupPermissions");

            migrationBuilder.RenameColumn(
                name: "TenNhomQ",
                table: "GroupPermissions",
                newName: "MoTa");

            migrationBuilder.RenameColumn(
                name: "ChucNang",
                table: "GroupPermissions",
                newName: "GroupName");
        }
    }
}
