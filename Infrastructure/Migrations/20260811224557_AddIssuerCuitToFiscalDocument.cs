using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssuerCuitToFiscalDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssuerCuit",
                table: "FiscalDocuments",
                type: "nvarchar(max)",
                nullable: true);

            // Todo lo que ya tiene CAE se emitió con el CUIT de VERTICE PROYECTOS SA, antes de
            // pasar a facturar con el CUIT del cliente (monotributo). Sin este backfill, al
            // reimprimir una factura vieja saldría el CUIT nuevo junto al CAE viejo.
            // Los pendientes quedan en NULL a propósito: todavía no se emitieron, así que les
            // corresponde el CUIT con el que finalmente se los facture.
            migrationBuilder.Sql(
                "UPDATE FiscalDocuments SET IssuerCuit = '30719138825' WHERE CAE IS NOT NULL AND CAE <> '';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssuerCuit",
                table: "FiscalDocuments");
        }
    }
}
