using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class BgFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "PetStats");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Pets",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeathDate",
                table: "Pets",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "Pets");

            migrationBuilder.AddColumn<long>(
                name: "Age",
                table: "PetStats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
