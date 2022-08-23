using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticketing.Data.Migrations
{
    public partial class mig_AddIsClosedToTicket_And_ChangeDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket");

            migrationBuilder.RenameColumn(
                name: "IsFinally",
                table: "Ticket",
                newName: "IsClosed");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket");

            migrationBuilder.RenameColumn(
                name: "IsClosed",
                table: "Ticket",
                newName: "IsFinally");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTicket_Ticket_TicketId",
                table: "UserTicket",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id");
        }
    }
}
