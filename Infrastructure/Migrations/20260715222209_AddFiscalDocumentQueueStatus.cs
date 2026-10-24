using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFiscalDocumentQueueStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "InvoiceNumber",
                table: "FiscalDocuments",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CAEExpirationDate",
                table: "FiscalDocuments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CAE",
                table: "FiscalDocuments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AttemptCount",
                table: "FiscalDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FiscalDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastAttemptAt",
                table: "FiscalDocuments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastError",
                table: "FiscalDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "FiscalDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Las filas que ya existían antes de esta migración vienen del flujo viejo, que solo
            // insertaba un FiscalDocument después de obtener el CAE con éxito: todas están
            // Approved. Si se dejaran en Pending (el default de la columna nueva), la cola las
            // volvería a facturar y generaría comprobantes duplicados en ARCA.
            migrationBuilder.Sql(
                "UPDATE FiscalDocuments SET Status = 1, CreatedAt = GETDATE() WHERE CAE IS NOT NULL AND CAE <> '';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttemptCount",
                table: "FiscalDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FiscalDocuments");

            migrationBuilder.DropColumn(
                name: "LastAttemptAt",
                table: "FiscalDocuments");

            migrationBuilder.DropColumn(
                name: "LastError",
                table: "FiscalDocuments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "FiscalDocuments");

            migrationBuilder.AlterColumn<long>(
                name: "InvoiceNumber",
                table: "FiscalDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CAEExpirationDate",
                table: "FiscalDocuments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CAE",
                table: "FiscalDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
