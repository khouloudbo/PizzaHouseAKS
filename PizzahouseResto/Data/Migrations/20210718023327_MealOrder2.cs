using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzahouseResto.Data.Migrations
{
    public partial class MealOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealOrder_Meals_MealId",
                table: "MealOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MealOrder_Orders_OrderId",
                table: "MealOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealOrder",
                table: "MealOrder");

            migrationBuilder.DropIndex(
                name: "IX_MealOrder_OrderId",
                table: "MealOrder");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "MealOrder");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "MealOrder",
                newName: "OrdersOrderId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "MealOrder",
                newName: "MealsMealId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealOrder",
                table: "MealOrder",
                columns: new[] { "MealsMealId", "OrdersOrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_MealOrder_OrdersOrderId",
                table: "MealOrder",
                column: "OrdersOrderId");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_MealOrder",
                table: "MealOrder");

            migrationBuilder.DropIndex(
                name: "IX_MealOrder_OrdersOrderId",
                table: "MealOrder");

            migrationBuilder.RenameColumn(
                name: "OrdersOrderId",
                table: "MealOrder",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "MealsMealId",
                table: "MealOrder",
                newName: "OrderId");

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "MealOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MealOrder",
                table: "MealOrder",
                columns: new[] { "MealId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_MealOrder_OrderId",
                table: "MealOrder",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealOrder_Meals_MealId",
                table: "MealOrder",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "MealId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MealOrder_Orders_OrderId",
                table: "MealOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
