﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class Update_Per2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phanloai",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phanloai",
                table: "Permissions");
        }
    }
}
