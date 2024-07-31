using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Convention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketUser_Tickets_AnsweredTicketId",
                table: "TicketUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketUser_Users_SupportersId",
                table: "TicketUser");

            migrationBuilder.RenameColumn(
                name: "SupportersId",
                table: "TicketUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "AnsweredTicketId",
                table: "TicketUser",
                newName: "TicketsId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketUser_SupportersId",
                table: "TicketUser",
                newName: "IX_TicketUser_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUser_Tickets_TicketsId",
                table: "TicketUser",
                column: "TicketsId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUser_Users_UsersId",
                table: "TicketUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketUser_Tickets_TicketsId",
                table: "TicketUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketUser_Users_UsersId",
                table: "TicketUser");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "TicketUser",
                newName: "SupportersId");

            migrationBuilder.RenameColumn(
                name: "TicketsId",
                table: "TicketUser",
                newName: "AnsweredTicketId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketUser_UsersId",
                table: "TicketUser",
                newName: "IX_TicketUser_SupportersId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUser_Tickets_AnsweredTicketId",
                table: "TicketUser",
                column: "AnsweredTicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketUser_Users_SupportersId",
                table: "TicketUser",
                column: "SupportersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
