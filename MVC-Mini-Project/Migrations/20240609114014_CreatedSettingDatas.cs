using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Mini_Project.Migrations
{
    public partial class CreatedSettingDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5537));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5539));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5540));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5541));

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "CreatedDate", "Key", "SoftDeleted", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 5, new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5542), "Twitter", false, null, "twitter.com" },
                    { 6, new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5543), "Facebook", false, null, "facebook.com" },
                    { 7, new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5543), "Youtube", false, null, "youtube.com" },
                    { 8, new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5544), "Linkedin", false, null, "linkedin.com" }
                });

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5410));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 9, 15, 40, 13, 616, DateTimeKind.Local).AddTicks(5412));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 23, 35, 25, 19, DateTimeKind.Local).AddTicks(4434));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 23, 35, 25, 19, DateTimeKind.Local).AddTicks(4436));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 23, 35, 25, 19, DateTimeKind.Local).AddTicks(4437));

            migrationBuilder.UpdateData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 23, 35, 25, 19, DateTimeKind.Local).AddTicks(4438));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 23, 35, 25, 19, DateTimeKind.Local).AddTicks(4352));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 23, 35, 25, 19, DateTimeKind.Local).AddTicks(4353));
        }
    }
}
