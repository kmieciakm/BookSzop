using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseManager.Migrations
{
    public partial class AddedPlacedDatetoOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PlacedDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlacedDate",
                table: "Orders");
        }
    }
}
