using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Mini_Project.Migrations
{
    public partial class CreatedCourseImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 11, 19, 37, 146, DateTimeKind.Local).AddTicks(2408));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 11, 19, 37, 146, DateTimeKind.Local).AddTicks(2409));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 10, 48, 13, 581, DateTimeKind.Local).AddTicks(1071));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 10, 48, 13, 581, DateTimeKind.Local).AddTicks(1073));
        }
    }
}
