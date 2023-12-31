﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleApp.RazorPage.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAdminToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "users");

        }
    }
}
