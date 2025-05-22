using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KKB1App.Migrations
{
    /// <inheritdoc />
    public partial class fixesInModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Shows",
                newName: "DateStartTime");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_DateTime",
                table: "Shows",
                newName: "IX_Shows_DateStartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEndTime",
                table: "Shows",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateEndTime",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "DateStartTime",
                table: "Shows",
                newName: "DateTime");

            migrationBuilder.RenameIndex(
                name: "IX_Shows_DateStartTime",
                table: "Shows",
                newName: "IX_Shows_DateTime");
        }
    }
}
