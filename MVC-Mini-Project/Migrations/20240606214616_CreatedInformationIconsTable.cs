using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Mini_Project.Migrations
{
    public partial class CreatedInformationIconsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Informations");

            migrationBuilder.AddColumn<int>(
                name: "InformationIconId",
                table: "Informations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "InformationIcons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformationIcons", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 7, 1, 46, 16, 229, DateTimeKind.Local).AddTicks(4078));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 7, 1, 46, 16, 229, DateTimeKind.Local).AddTicks(4080));

            migrationBuilder.CreateIndex(
                name: "IX_Informations_InformationIconId",
                table: "Informations",
                column: "InformationIconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Informations_InformationIcons_InformationIconId",
                table: "Informations",
                column: "InformationIconId",
                principalTable: "InformationIcons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Informations_InformationIcons_InformationIconId",
                table: "Informations");

            migrationBuilder.DropTable(
                name: "InformationIcons");

            migrationBuilder.DropIndex(
                name: "IX_Informations_InformationIconId",
                table: "Informations");

            migrationBuilder.DropColumn(
                name: "InformationIconId",
                table: "Informations");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Informations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 7, 1, 29, 15, 781, DateTimeKind.Local).AddTicks(2744));

            migrationBuilder.UpdateData(
                table: "Sliders",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 7, 1, 29, 15, 781, DateTimeKind.Local).AddTicks(2746));
        }
    }
}
