using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class AddPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "DsDonViTdg",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "tendv",
                table: "DsDonViTdg",
                newName: "Tendv");

            migrationBuilder.RenameColumn(
                name: "sothe",
                table: "DsDonViTdg",
                newName: "Sothe");

            migrationBuilder.RenameColumn(
                name: "nguoidaidien",
                table: "DsDonViTdg",
                newName: "Nguoidaidien");

            migrationBuilder.RenameColumn(
                name: "ngaycap",
                table: "DsDonViTdg",
                newName: "Ngaycap");

            migrationBuilder.RenameColumn(
                name: "maso",
                table: "DsDonViTdg",
                newName: "Maso");

            migrationBuilder.RenameColumn(
                name: "diachi",
                table: "DsDonViTdg",
                newName: "Diachi");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "DsDonViTdg",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "chucvu",
                table: "DsDonViTdg",
                newName: "Chucvu");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DsDonViTdg",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<bool>(type: "bit", nullable: false),
                    Create = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    Approve = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "DsDonViTdg",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "Tendv",
                table: "DsDonViTdg",
                newName: "tendv");

            migrationBuilder.RenameColumn(
                name: "Sothe",
                table: "DsDonViTdg",
                newName: "sothe");

            migrationBuilder.RenameColumn(
                name: "Nguoidaidien",
                table: "DsDonViTdg",
                newName: "nguoidaidien");

            migrationBuilder.RenameColumn(
                name: "Ngaycap",
                table: "DsDonViTdg",
                newName: "ngaycap");

            migrationBuilder.RenameColumn(
                name: "Maso",
                table: "DsDonViTdg",
                newName: "maso");

            migrationBuilder.RenameColumn(
                name: "Diachi",
                table: "DsDonViTdg",
                newName: "diachi");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "DsDonViTdg",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "Chucvu",
                table: "DsDonViTdg",
                newName: "chucvu");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DsDonViTdg",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
