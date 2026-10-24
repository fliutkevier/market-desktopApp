using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixSessionDurationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDuration",
                table: "Sessions");

            // 2. Creamos la columna nueva con el tipo correcto (float/double)
            migrationBuilder.AddColumn<double>(
                name: "TotalDuration",
                table: "Sessions",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDuration",
                table: "Sessions");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalDuration",
                table: "Sessions",
                type: "time",
                nullable: true);
        }
    }
}
