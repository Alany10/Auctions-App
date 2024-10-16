using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BidDbs",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "EndDate",
                value: new DateTime(2024, 10, 23, 18, 15, 59, 535, DateTimeKind.Local).AddTicks(8743));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AuctionDbs",
                keyColumn: "Id",
                keyValue: -1,
                column: "EndDate",
                value: new DateTime(2024, 10, 23, 17, 2, 30, 176, DateTimeKind.Local).AddTicks(8143));

            migrationBuilder.InsertData(
                table: "BidDbs",
                columns: new[] { "Id", "AuctionId", "Price", "UserName" },
                values: new object[] { -2, -1, 1000, "Ali" });
        }
    }
}
