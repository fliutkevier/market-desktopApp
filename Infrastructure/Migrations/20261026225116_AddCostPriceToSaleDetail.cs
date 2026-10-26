using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCostPriceToSaleDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostPrice",
                table: "SaleDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            // Completado único de las ventas que ya existían (no tenían el costo guardado): se
            // usa el costo actual de cada producto. Corre una sola vez, cuando se aplica esta
            // migración; desde ahí cada venta guarda el costo del momento en que se hizo.
            migrationBuilder.Sql(
                "UPDATE d SET d.CostPrice = p.CostPrice " +
                "FROM SaleDetails d INNER JOIN Products p ON p.Code = d.ProductCode;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "SaleDetails");
        }
    }
}
