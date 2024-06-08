using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Mini_Project.Migrations
{
    public partial class CreatedSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "CreatedDate", "Key", "SoftDeleted", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 8, 22, 34, 56, 27, DateTimeKind.Local).AddTicks(2261), "Location", false, null, "123 Street, New York, USA" },
                    { 2, new DateTime(2024, 6, 8, 22, 34, 56, 27, DateTimeKind.Local).AddTicks(2263), "Phone", false, null, "+012 345 67890" },
                    { 3, new DateTime(2024, 6, 8, 22, 34, 56, 27, DateTimeKind.Local).AddTicks(2264), "Email", false, null, "info@example.com" },
                    { 4, new DateTime(2024, 6, 8, 22, 34, 56, 27, DateTimeKind.Local).AddTicks(2265), "Logo", false, null, "fa fa-book me-3" }
                });

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 22, 34, 56, 27, DateTimeKind.Local).AddTicks(2185));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 8, 22, 34, 56, 27, DateTimeKind.Local).AddTicks(2187));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

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
    }
}
