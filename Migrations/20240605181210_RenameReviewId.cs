using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ebookings.Migrations
{
    /// <inheritdoc />
    public partial class RenameReviewId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.AddColumn<int>(
                name: "ReviewId1",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewId1",
                table: "Reviews",
                column: "ReviewId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Reviews_ReviewId1",
                table: "Reviews",
                column: "ReviewId1",
                principalTable: "Reviews",
                principalColumn: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Reviews_ReviewId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewId1",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Reviews",
                newName: "Id");
        }
    }
}
