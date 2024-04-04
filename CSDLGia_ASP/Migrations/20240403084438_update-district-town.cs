using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class updatedistricttown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chucvuky",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Chucvukythay",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Diachi",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Diadanh",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Emailql",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Emailqt",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Nguoiky",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Songaylv",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Tendv",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Tendvcqhienthi",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Tendvhienthi",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Towns");

            migrationBuilder.DropColumn(
                name: "Diachi",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Emailql",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Emailqt",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Phanloaiql",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Tendv",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Tendvcqhienthi",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "Tendvhienthi",
                table: "Districts");

            migrationBuilder.RenameColumn(
                name: "Ttlienhe",
                table: "Towns",
                newName: "Tenxa");

            migrationBuilder.RenameColumn(
                name: "Ttlienhe",
                table: "Districts",
                newName: "Tenhuyen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tenxa",
                table: "Towns",
                newName: "Ttlienhe");

            migrationBuilder.RenameColumn(
                name: "Tenhuyen",
                table: "Districts",
                newName: "Ttlienhe");

            migrationBuilder.AddColumn<string>(
                name: "Chucvuky",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chucvukythay",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diachi",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diadanh",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emailql",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emailqt",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nguoiky",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Songaylv",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tendv",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tendvcqhienthi",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tendvhienthi",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Towns",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Diachi",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emailql",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emailqt",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phanloaiql",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tendv",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tendvcqhienthi",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tendvhienthi",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
