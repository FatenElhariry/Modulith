using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Basket.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableNameUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItem_ShoppingCart_ShoppingCartId",
                schema: "Basket",
                table: "ShoppingCartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItem",
                schema: "Basket",
                table: "ShoppingCartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCart",
                schema: "Basket",
                table: "ShoppingCart");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItem",
                schema: "Basket",
                newName: "ShoppingCartItems",
                newSchema: "Basket");

            migrationBuilder.RenameTable(
                name: "ShoppingCart",
                schema: "Basket",
                newName: "ShoppingCarts",
                newSchema: "Basket");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItem_ShoppingCartId",
                schema: "Basket",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_UserName",
                schema: "Basket",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_UserName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                schema: "Basket",
                table: "ShoppingCartItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                schema: "Basket",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                schema: "Basket",
                table: "ShoppingCartItems",
                column: "ShoppingCartId",
                principalSchema: "Basket",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_ShoppingCarts_ShoppingCartId",
                schema: "Basket",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                schema: "Basket",
                table: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                schema: "Basket",
                table: "ShoppingCartItems");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                schema: "Basket",
                newName: "ShoppingCart",
                newSchema: "Basket");

            migrationBuilder.RenameTable(
                name: "ShoppingCartItems",
                schema: "Basket",
                newName: "ShoppingCartItem",
                newSchema: "Basket");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_UserName",
                schema: "Basket",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_ShoppingCartId",
                schema: "Basket",
                table: "ShoppingCartItem",
                newName: "IX_ShoppingCartItem_ShoppingCartId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCart",
                schema: "Basket",
                table: "ShoppingCart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItem",
                schema: "Basket",
                table: "ShoppingCartItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItem_ShoppingCart_ShoppingCartId",
                schema: "Basket",
                table: "ShoppingCartItem",
                column: "ShoppingCartId",
                principalSchema: "Basket",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
