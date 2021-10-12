using Microsoft.EntityFrameworkCore.Migrations;

namespace CalendarApp.Data.Migrations
{
    public partial class AddedTimeToEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "CalendarEntry",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "CalendarEntry");
        }
    }
}
