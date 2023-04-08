using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class productaveragerating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cust");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Cust",
                newName: "Customers",
                newSchema: "cust");

            migrationBuilder.AddColumn<int>(
                name: "AverageRating_RatingCount",
                schema: "prod",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "AverageRating_Value",
                schema: "prod",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "CustomersCart",
                schema: "cust",
                columns: table => new
                {
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersCart", x => new { x.CartId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_CustomersCart_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "cust",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                schema: "prod",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => new { x.ReviewId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "prod",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                schema: "cust",
                columns: table => new
                {
                    CartItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price_Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Price_Cureency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => new { x.CartItemId, x.CartId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_CartItem_CustomersCart_CartId_CustomerId",
                        columns: x => new { x.CartId, x.CustomerId },
                        principalSchema: "cust",
                        principalTable: "CustomersCart",
                        principalColumns: new[] { "CartId", "CustomerId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId",
                schema: "cust",
                table: "CartItem",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_CartId_CustomerId",
                schema: "cust",
                table: "CartItem",
                columns: new[] { "CartId", "CustomerId" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersCart_CustomerId",
                schema: "cust",
                table: "CustomersCart",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId",
                schema: "prod",
                table: "ProductReviews",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem",
                schema: "cust");

            migrationBuilder.DropTable(
                name: "ProductReviews",
                schema: "prod");

            migrationBuilder.DropTable(
                name: "CustomersCart",
                schema: "cust");

            migrationBuilder.DropColumn(
                name: "AverageRating_RatingCount",
                schema: "prod",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AverageRating_Value",
                schema: "prod",
                table: "Products");

            migrationBuilder.EnsureSchema(
                name: "Cust");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "cust",
                newName: "Customers",
                newSchema: "Cust");
        }
    }
}
