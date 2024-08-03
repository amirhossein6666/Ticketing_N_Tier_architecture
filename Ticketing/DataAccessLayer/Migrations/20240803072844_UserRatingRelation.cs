using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class UserRatingRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupporterRatings_Tickets_RelatedTicketId",
                table: "SupporterRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_SupporterRatings_Users_RatedUserId",
                table: "SupporterRatings");

            migrationBuilder.DropIndex(
                name: "IX_SupporterRatings_RelatedTicketId",
                table: "SupporterRatings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SupporterRatings_RelatedTicketId",
                table: "SupporterRatings",
                column: "RelatedTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_SupporterRatings_Tickets_RelatedTicketId",
                table: "SupporterRatings",
                column: "RelatedTicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SupporterRatings_Users_RatedUserId",
                table: "SupporterRatings",
                column: "RatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
