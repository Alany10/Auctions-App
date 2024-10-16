using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialDebug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "BidDbs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AuctionDbs",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "AuctionDbs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                columns: new[] { "EndDate", "Price" },
                values: new object[] { new DateTime(2024, 10, 23, 17, 2, 30, 176, DateTimeKind.Local).AddTicks(8143), 700 });

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2,
                column: "Price",
                value: 1000);

            migrationBuilder.UpdateData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "Price",
                value: 800);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "BidDbs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AuctionDbs");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "AuctionDbs");
        }
    }
}
