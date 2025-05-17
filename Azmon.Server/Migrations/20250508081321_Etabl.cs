using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Azmon.Server.Migrations
{
    /// <inheritdoc />
    public partial class Etabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buy_Suppliers_SuppliersId",
                table: "Buy");

            migrationBuilder.DropForeignKey(
                name: "FK_Buy_Detail_Buy_BuyId",
                table: "Buy_Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Buy_Detail_Product_ProductId",
                table: "Buy_Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Cus_Pay_Customer_CustomerId",
                table: "Cus_Pay");

            migrationBuilder.DropForeignKey(
                name: "FK_Sell_Customer_CustomerId",
                table: "Sell");

            migrationBuilder.DropForeignKey(
                name: "FK_Sell_Detail_Product_ProductId",
                table: "Sell_Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Sell_Detail_Sell_SellId",
                table: "Sell_Detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Sup_Pay_Suppliers_SuppliersId",
                table: "Sup_Pay");

            migrationBuilder.DropIndex(
                name: "IX_Sup_Pay_SuppliersId",
                table: "Sup_Pay");

            migrationBuilder.DropIndex(
                name: "IX_Sell_Detail_ProductId",
                table: "Sell_Detail");

            migrationBuilder.DropIndex(
                name: "IX_Sell_Detail_SellId",
                table: "Sell_Detail");

            migrationBuilder.DropIndex(
                name: "IX_Sell_CustomerId",
                table: "Sell");

            migrationBuilder.DropIndex(
                name: "IX_Cus_Pay_CustomerId",
                table: "Cus_Pay");

            migrationBuilder.DropIndex(
                name: "IX_Buy_Detail_BuyId",
                table: "Buy_Detail");

            migrationBuilder.DropIndex(
                name: "IX_Buy_Detail_ProductId",
                table: "Buy_Detail");

            migrationBuilder.DropIndex(
                name: "IX_Buy_SuppliersId",
                table: "Buy");

            migrationBuilder.DropColumn(
                name: "SuppliersId",
                table: "Sup_Pay");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Cus_Pay");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuppliersId",
                table: "Sup_Pay",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Cus_Pay",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sup_Pay_SuppliersId",
                table: "Sup_Pay",
                column: "SuppliersId");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_Detail_ProductId",
                table: "Sell_Detail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_Detail_SellId",
                table: "Sell_Detail",
                column: "SellId");

            migrationBuilder.CreateIndex(
                name: "IX_Sell_CustomerId",
                table: "Sell",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cus_Pay_CustomerId",
                table: "Cus_Pay",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Buy_Detail_BuyId",
                table: "Buy_Detail",
                column: "BuyId");

            migrationBuilder.CreateIndex(
                name: "IX_Buy_Detail_ProductId",
                table: "Buy_Detail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Buy_SuppliersId",
                table: "Buy",
                column: "SuppliersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buy_Suppliers_SuppliersId",
                table: "Buy",
                column: "SuppliersId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buy_Detail_Buy_BuyId",
                table: "Buy_Detail",
                column: "BuyId",
                principalTable: "Buy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Buy_Detail_Product_ProductId",
                table: "Buy_Detail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cus_Pay_Customer_CustomerId",
                table: "Cus_Pay",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sell_Customer_CustomerId",
                table: "Sell",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sell_Detail_Product_ProductId",
                table: "Sell_Detail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sell_Detail_Sell_SellId",
                table: "Sell_Detail",
                column: "SellId",
                principalTable: "Sell",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sup_Pay_Suppliers_SuppliersId",
                table: "Sup_Pay",
                column: "SuppliersId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
