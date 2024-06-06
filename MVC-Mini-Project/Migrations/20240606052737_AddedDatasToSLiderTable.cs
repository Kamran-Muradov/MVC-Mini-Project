using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Mini_Project.Migrations
{
    public partial class AddedDatasToSLiderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "SoftDeleted", "Title", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 6, 6, 9, 27, 37, 10, DateTimeKind.Local).AddTicks(6144), "Vero elitr justo clita lorem. Ipsum dolor at sed stet sit diam no. Kasd rebum ipsum et diam justo clita et kasd rebum sea sanctus eirmod elitr.", "carousel-1.jpg", false, "The Best Online Learning Platform", null });

            migrationBuilder.InsertData(
                table: "Sliders",
                columns: new[] { "Id", "CreatedDate", "Description", "Image", "SoftDeleted", "Title", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2024, 6, 6, 9, 27, 37, 10, DateTimeKind.Local).AddTicks(6146), "Vero elitr justo clita lorem. Ipsum dolor at sed stet sit diam no. Kasd rebum ipsum et diam justo clita et kasd rebum sea sanctus eirmod elitr.", "carousel-2.jpg", false, "Get Educated Online From Your Home", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
