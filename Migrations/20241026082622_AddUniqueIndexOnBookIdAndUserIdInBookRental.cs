using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_api.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexOnBookIdAndUserIdInBookRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookRentals_BookId",
                table: "BookRentals");

            migrationBuilder.CreateIndex(
                name: "IX_BookRentals_BookId_UserId",
                table: "BookRentals",
                columns: new[] { "BookId", "UserId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookRentals_BookId_UserId",
                table: "BookRentals");

            migrationBuilder.CreateIndex(
                name: "IX_BookRentals_BookId",
                table: "BookRentals",
                column: "BookId");
        }
    }
}
