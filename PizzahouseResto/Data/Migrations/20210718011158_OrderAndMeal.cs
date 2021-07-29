using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzahouseResto.Data.Migrations
{
    public partial class OrderAndMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealOrder_Meals_ProductListMealId",
                table: "MealOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MealOrder_Orders_OrderListOrderId",
                table: "MealOrder");

            migrationBuilder.RenameColumn(
                name: "ProductListMealId",
                table: "MealOrder",
                newName: "OrdersOrderId");

            migrationBuilder.RenameColumn(
                name: "OrderListOrderId",
                table: "MealOrder",
                newName: "MealsMealId");

            migrationBuilder.RenameIndex(
                name: "IX_MealOrder_ProductListMealId",
                table: "MealOrder",
                newName: "IX_MealOrder_OrdersOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealOrder_Meals_MealsMealId",
                table: "MealOrder",
                column: "MealsMealId",
                principalTable: "Meals",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealOrder_Orders_OrdersOrderId",
                table: "MealOrder",
                column: "OrdersOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealOrder_Meals_MealsMealId",
                table: "MealOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MealOrder_Orders_OrdersOrderId",
                table: "MealOrder");

            migrationBuilder.RenameColumn(
                name: "OrdersOrderId",
                table: "MealOrder",
                newName: "ProductListMealId");

            migrationBuilder.RenameColumn(
                name: "MealsMealId",
                table: "MealOrder",
                newName: "OrderListOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MealOrder_OrdersOrderId",
                table: "MealOrder",
                newName: "IX_MealOrder_ProductListMealId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealOrder_Meals_ProductListMealId",
                table: "MealOrder",
                column: "ProductListMealId",
                principalTable: "Meals",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealOrder_Orders_OrderListOrderId",
                table: "MealOrder",
                column: "OrderListOrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
