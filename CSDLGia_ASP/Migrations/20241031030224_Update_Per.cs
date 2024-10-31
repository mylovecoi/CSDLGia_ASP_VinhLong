﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class Update_Per : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Magoc",
                table: "Permissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sttsx",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Magoc",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "Sttsx",
                table: "Permissions");
        }
    }
}
