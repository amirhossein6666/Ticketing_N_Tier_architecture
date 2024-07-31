using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class navab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketSupporter");

            migrationBuilder.CreateTable(
                name: "TicketUser",
                columns: table => new
                {
                    AnsweredTicketId = table.Column<int>(type: "int", nullable: false),
                    SupportersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketUser", x => new { x.AnsweredTicketId, x.SupportersId });
                    table.ForeignKey(
                        name: "FK_TicketUser_Tickets_AnsweredTicketId",
                        column: x => x.AnsweredTicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketUser_Users_SupportersId",
                        column: x => x.SupportersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketUser_SupportersId",
                table: "TicketUser",
                column: "SupportersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketUser");

            migrationBuilder.CreateTable(
                name: "TicketSupporter",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketSupporter", x => new { x.TicketId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TicketSupporter_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketSupporter_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketSupporter_UserId",
                table: "TicketSupporter",
                column: "UserId");
        }
    }
}
