using Microsoft.EntityFrameworkCore.Migrations;

namespace MajorProject.Migrations
{
    public partial class AddedBuyProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyProducts",
                columns: table => new
                {
                    BuyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(maxLength: 50, nullable: false),
                    CustomerPhone = table.Column<string>(nullable: false),
                    ProcudtId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyProducts", x => x.BuyId);
                    table.ForeignKey(
                        name: "FK_BuyProducts_Products_ProcudtId",
                        column: x => x.ProcudtId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyProducts_ProcudtId",
                table: "BuyProducts",
                column: "ProcudtId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyProducts");
        }
    }
}
