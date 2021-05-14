using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WepApi.Migrations
{
    public partial class RenameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Projects_NameId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_NameId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Projects");

            migrationBuilder.AddColumn<DateTime>(
                name: "Estimate",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SpentTime",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estimate",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SpentTime",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "NameId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_NameId",
                table: "Projects",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Projects_NameId",
                table: "Projects",
                column: "NameId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
