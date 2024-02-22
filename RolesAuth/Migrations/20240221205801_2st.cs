using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RolesAuth.Migrations
{
    /// <inheritdoc />
    public partial class _2st : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CafeId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CafeId",
                table: "Order",
                column: "CafeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Cafes_CafeId",
                table: "Order",
                column: "CafeId",
                principalTable: "Cafes",
                principalColumn: "CafeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Cafes_CafeId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CafeId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CafeId",
                table: "Order");
        }
    }
}
