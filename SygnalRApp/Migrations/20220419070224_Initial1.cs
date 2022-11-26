using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRApp.Migrations
{
    public partial class Initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AuthEntities");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenLifetime",
                table: "AuthEntities",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UserEntities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailPrimary",
                table: "UserEntities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "UserEntities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JpegPhoto",
                table: "UserEntities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UserEntities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AuthEntities",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "EmailPrimary",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "JpegPhoto",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UserEntities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AuthEntities");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "AuthEntities",
                newName: "RefreshTokenLifetime");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AuthEntities",
                type: "text",
                nullable: true);
        }
    }
}
