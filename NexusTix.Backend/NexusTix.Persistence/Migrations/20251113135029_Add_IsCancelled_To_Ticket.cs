using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NexusTix.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsCancelled_To_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Tickets");
        }
    }
}
