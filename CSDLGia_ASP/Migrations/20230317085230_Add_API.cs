using Microsoft.EntityFrameworkCore.Migrations;

namespace CSDLGia_ASP.Migrations
{
    public partial class Add_API : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KetNoiAPI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phanloai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kieudulieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dinhdang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dodai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Batbuoc = table.Column<bool>(type: "bit", nullable: false),
                    Macdinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetNoiAPI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KetNoiAPI_HoSo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentruong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kieudulieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dinhdang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dodai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Batbuoc = table.Column<bool>(type: "bit", nullable: false),
                    Macdinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetNoiAPI_HoSo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KetNoiAPI_HoSo_ChiTiet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Maso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendong_goc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tentruong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tendong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kieudulieu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dinhdang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dodai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Batbuoc = table.Column<bool>(type: "bit", nullable: false),
                    Macdinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stt = table.Column<int>(type: "int", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KetNoiAPI_HoSo_ChiTiet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KetNoiAPI");

            migrationBuilder.DropTable(
                name: "KetNoiAPI_HoSo");

            migrationBuilder.DropTable(
                name: "KetNoiAPI_HoSo_ChiTiet");
        }
    }
}
