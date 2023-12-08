using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrosBurger.Migrations
{
    /// <inheritdoc />
    public partial class bros3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BannersBannerId",
                table: "Produtos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_BannersBannerId",
                table: "Produtos",
                column: "BannersBannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Banners_BannersBannerId",
                table: "Produtos",
                column: "BannersBannerId",
                principalTable: "Banners",
                principalColumn: "BannerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Banners_BannersBannerId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_BannersBannerId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "BannersBannerId",
                table: "Produtos");
        }
    }
}
