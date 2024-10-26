using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSDLGia_ASP.Migrations
{
    /// <inheritdoc />
    public partial class Fix_GiaDatCuThe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Manhom",
                table: "GiaDatCuTheVlCt",
                newName: "Noidungcv");

            migrationBuilder.AlterColumn<double>(
                name: "STTSapXep",
                table: "GiaDatCuTheVlCt",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Mapp",
                table: "GiaDatCuTheVlCt",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NhapGia",
                table: "GiaDatCuTheVlCt",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mapp",
                table: "GiaDatCuTheVlCt");

            migrationBuilder.DropColumn(
                name: "NhapGia",
                table: "GiaDatCuTheVlCt");

            migrationBuilder.RenameColumn(
                name: "Noidungcv",
                table: "GiaDatCuTheVlCt",
                newName: "Manhom");

            migrationBuilder.AlterColumn<int>(
                name: "STTSapXep",
                table: "GiaDatCuTheVlCt",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
