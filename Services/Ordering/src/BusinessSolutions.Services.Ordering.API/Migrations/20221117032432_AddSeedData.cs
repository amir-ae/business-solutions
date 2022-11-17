using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessSolutions.Services.Ordering.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "ordering",
                table: "Providers",
                columns: new[] { "ProviderId", "ProviderName" },
                values: new object[,]
                {
                    { 1, "Considine-Bauch" },
                    { 2, "Herzog PLC" },
                    { 3, "Steuber, Considine and Hermann" },
                    { 4, "Klocko Group" },
                    { 5, "Stracke Group" }
                });

            migrationBuilder.InsertData(
                schema: "ordering",
                table: "Orders",
                columns: new[] { "Id", "Date", "IsInactive", "Number", "ProviderId" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2021, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, "13eb0c8c-8362-4bcb-b732-7dc4700233e0", 1 },
                    { 2, new DateTimeOffset(new DateTime(2017, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, "12963b79-3207-4ed9-b353-7c66fe3f3ae9", 2 },
                    { 3, new DateTimeOffset(new DateTime(2017, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), false, "0e360e54-5b27-40bc-bc91-f10354357952", 3 }
                });

            migrationBuilder.InsertData(
                schema: "ordering",
                table: "Items",
                columns: new[] { "ItemId", "Name", "OrderId", "Quantity", "Unit" },
                values: new object[,]
                {
                    { 1, "Green Apples", 1, 1m, "Kilogram" },
                    { 2, "Lifejacket", 1, 2m, "Item" },
                    { 3, "Soccer Ball", 1, 2m, "Item" },
                    { 4, "Chess Board", 2, 1m, "Item" },
                    { 5, "Red Fabric", 2, 5m, "Meter" },
                    { 6, "GOOD KID, m.A.A.d CITY", 3, 1m, "Item" },
                    { 7, "Organic Milk", 3, 3m, "Liter" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "ordering",
                table: "Providers",
                keyColumn: "ProviderId",
                keyValue: 3);
        }
    }
}
