﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commece.DA.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrdersT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsT_OrdersT_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OrdersT",
                columns: new[] { "Id", "OrderDate", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 },
                    { 2, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.InsertData(
                table: "ProductsT",
                columns: new[] { "Id", "Name", "OrderId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Product A", 1, 10.99m, 5 },
                    { 2, "Product B", 2, 20.50m, 3 },
                    { 3, "Product C", 1, 15.75m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsT_OrderId",
                table: "ProductsT",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsT");

            migrationBuilder.DropTable(
                name: "OrdersT");
        }
    }
}
