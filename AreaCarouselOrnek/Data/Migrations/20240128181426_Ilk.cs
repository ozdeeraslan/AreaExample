using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AreaCarouselOrnek.Data.Migrations
{
    /// <inheritdoc />
    public partial class Ilk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slaytlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: false),
                    ResimYolu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slaytlar", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Slaytlar",
                columns: new[] { "Id", "Aciklama", "Baslik", "ResimYolu", "Sira" },
                values: new object[,]
                {
                    { 1, "Hepimiz birer yağmur damlasıyız aslında", "Yagmur", "yagmur.jpg", 1 },
                    { 2, "Eğer sen doğaya zarar vermezsen doğa sana asla zarar vermez", "Manzara", "manzara.jpg", 2 },
                    { 3, "Rengarenk gökkuşağını görmek için yağmuru doyasıya yaşamak gerekir", "Gökkusagi", "gokkusagi.jpg", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slaytlar");
        }
    }
}
