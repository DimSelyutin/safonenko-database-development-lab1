using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace safonenko.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "StorageLocations",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Row = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                Shelf = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                Cell = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StorageLocations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Suppliers",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Details = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Suppliers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Article = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                MinStock = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                CategoryId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
                table.ForeignKey(
                    name: "FK_Products_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "StockMovements",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                OperationType = table.Column<int>(type: "int", nullable: false),
                Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                DocumentBase = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                ProductId = table.Column<int>(type: "int", nullable: false),
                SupplierId = table.Column<int>(type: "int", nullable: false),
                StorageLocationId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_StockMovements", x => x.Id);
                table.ForeignKey(
                    name: "FK_StockMovements_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_StockMovements_StorageLocations_StorageLocationId",
                    column: x => x.StorageLocationId,
                    principalTable: "StorageLocations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_StockMovements_Suppliers_SupplierId",
                    column: x => x.SupplierId,
                    principalTable: "Suppliers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Products_Article",
            table: "Products",
            column: "Article",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Products_CategoryId",
            table: "Products",
            column: "CategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_StockMovements_ProductId",
            table: "StockMovements",
            column: "ProductId");

        migrationBuilder.CreateIndex(
            name: "IX_StockMovements_StorageLocationId",
            table: "StockMovements",
            column: "StorageLocationId");

        migrationBuilder.CreateIndex(
            name: "IX_StockMovements_SupplierId",
            table: "StockMovements",
            column: "SupplierId");

        migrationBuilder.CreateIndex(
            name: "IX_StorageLocations_Row_Shelf_Cell",
            table: "StorageLocations",
            columns: new[] { "Row", "Shelf", "Cell" },
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "StockMovements");

        migrationBuilder.DropTable(
            name: "Products");

        migrationBuilder.DropTable(
            name: "StorageLocations");

        migrationBuilder.DropTable(
            name: "Suppliers");

        migrationBuilder.DropTable(
            name: "Categories");
    }
}
