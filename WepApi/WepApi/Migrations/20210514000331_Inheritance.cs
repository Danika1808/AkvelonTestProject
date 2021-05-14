using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WepApi.Migrations
{
    public partial class Inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Tasks",
                newName: "Entities");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ProjectId",
                table: "Entities",
                newName: "IX_Entities_ProjectId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Entities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Entities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Entities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Entities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entities",
                table: "Entities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Entities_ProjectId",
                table: "Entities",
                column: "ProjectId",
                principalTable: "Entities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Entities_ProjectId",
                table: "Entities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entities",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Entities");

            migrationBuilder.RenameTable(
                name: "Entities",
                newName: "Tasks");

            migrationBuilder.RenameIndex(
                name: "IX_Entities_ProjectId",
                table: "Tasks",
                newName: "IX_Tasks_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tasks",
                table: "Tasks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_ProjectId",
                table: "Tasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
