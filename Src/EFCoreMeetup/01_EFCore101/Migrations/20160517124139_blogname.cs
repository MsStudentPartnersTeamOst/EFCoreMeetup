using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _01_EFCore101.Migrations
{
    public partial class blogname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Blogs");
        }
    }
}
