using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedLiveSupport2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "LiveSupports");

            migrationBuilder.RenameColumn(
                name: "Messages",
                table: "LiveSupports",
                newName: "MessageContent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageContent",
                table: "LiveSupports",
                newName: "Messages");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "LiveSupports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
