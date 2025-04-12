using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoDeSangue.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TotalEstoque = table.Column<int>(type: "int", nullable: false),
                    TotalOPositivo = table.Column<int>(type: "int", nullable: false),
                    TotalONegativo = table.Column<int>(type: "int", nullable: false),
                    TotalAPositivo = table.Column<int>(type: "int", nullable: false),
                    TotalANegativo = table.Column<int>(type: "int", nullable: false),
                    TotalBPositivo = table.Column<int>(type: "int", nullable: false),
                    TotalBNegativo = table.Column<int>(type: "int", nullable: false),
                    TotalABPositivo = table.Column<int>(type: "int", nullable: false),
                    TotalABNegativo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estoques");
        }
    }
}
