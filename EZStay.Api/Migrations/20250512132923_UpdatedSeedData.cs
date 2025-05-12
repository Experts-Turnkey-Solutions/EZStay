using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EZStay.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0a1e2f3-4d56-7890-1234-56789abcdef0"),
                column: "Roles",
                value: "Admin,Manager,AccountManager,ContentManager,Owner,User");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "PasswordHash", "Roles", "Username" },
                values: new object[,]
                {
                    { new Guid("d0a2e3f4-5d67-8901-2345-67890abcde01"), "manager@gmail.com", "Manager User", "$2a$12$FB8ZwL2s9RcwDAwpthyWve/.4Q.Dc88SwsbbCQ9doaQjI4xpWk0YG", "Manager,User", "manager" },
                    { new Guid("e1a3e4f5-6d78-9012-3456-78901bcdef23"), "contentmanager@gmail.com", "Content Manager User", "$2a$12$FB8ZwL2s9RcwDAwpthyWve/.4Q.Dc88SwsbbCQ9doaQjI4xpWk0YG", "ContentManager,User", "contentmanager" },
                    { new Guid("f2a4e5f6-7d89-0123-4567-89012cde2345"), "accountmanager@gmail.com", "Account Manager User", "$2a$12$FB8ZwL2s9RcwDAwpthyWve/.4Q.Dc88SwsbbCQ9doaQjI4xpWk0YG", "AccountManager,User", "accountmanager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d0a2e3f4-5d67-8901-2345-67890abcde01"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e1a3e4f5-6d78-9012-3456-78901bcdef23"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2a4e5f6-7d89-0123-4567-89012cde2345"));

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 3, "Guest" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0a1e2f3-4d56-7890-1234-56789abcdef0"),
                column: "RoleId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
