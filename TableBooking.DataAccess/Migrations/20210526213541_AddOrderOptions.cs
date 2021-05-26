using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddOrderOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderOptionsId",
                table: "Restaurants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RestaurantOrderOptionsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LatestOrderDate = table.Column<int>(type: "int", nullable: false),
                    LongestReservationDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShortestReservationDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    OffDays = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantOrderOptionsEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_OrderOptionsId",
                table: "Restaurants",
                column: "OrderOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantOrderOptionsEntity_OrderOptionsId",
                table: "Restaurants",
                column: "OrderOptionsId",
                principalTable: "RestaurantOrderOptionsEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantOrderOptionsEntity_OrderOptionsId",
                table: "Restaurants");

            migrationBuilder.DropTable(
                name: "RestaurantOrderOptionsEntity");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_OrderOptionsId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OrderOptionsId",
                table: "Restaurants");
        }
    }
}
