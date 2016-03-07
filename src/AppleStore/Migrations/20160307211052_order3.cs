using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace AppleStore.Migrations
{
    public partial class order3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Apple_AppleID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Color_ColorID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_Image_Apple_AppleID", table: "Image");
            migrationBuilder.DropForeignKey(name: "FK_ProductDetails_Apple_AppleID", table: "ProductDetails");
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrdersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Delivary = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    Payment = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Sum = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrdersID);
                });
            migrationBuilder.CreateTable(
                name: "AppleOrders",
                columns: table => new
                {
                    AppleOrdersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppleID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false),
                    OrdersOrdersID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppleOrders", x => x.AppleOrdersID);
                    table.ForeignKey(
                        name: "FK_AppleOrders_Apple_AppleID",
                        column: x => x.AppleID,
                        principalTable: "Apple",
                        principalColumn: "AppleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppleOrders_Orders_OrdersOrdersID",
                        column: x => x.OrdersOrdersID,
                        principalTable: "Orders",
                        principalColumn: "OrdersID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_AppleColor_Apple_AppleID",
                table: "AppleColor",
                column: "AppleID",
                principalTable: "Apple",
                principalColumn: "AppleID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_AppleColor_Color_ColorID",
                table: "AppleColor",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Image_Apple_AppleID",
                table: "Image",
                column: "AppleID",
                principalTable: "Apple",
                principalColumn: "AppleID",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Apple_AppleID",
                table: "ProductDetails",
                column: "AppleID",
                principalTable: "Apple",
                principalColumn: "AppleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Apple_AppleID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Color_ColorID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_Image_Apple_AppleID", table: "Image");
            migrationBuilder.DropForeignKey(name: "FK_ProductDetails_Apple_AppleID", table: "ProductDetails");
            migrationBuilder.DropTable("AppleOrders");
            migrationBuilder.DropTable("Orders");
            migrationBuilder.AddForeignKey(
                name: "FK_AppleColor_Apple_AppleID",
                table: "AppleColor",
                column: "AppleID",
                principalTable: "Apple",
                principalColumn: "AppleID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_AppleColor_Color_ColorID",
                table: "AppleColor",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ColorID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Image_Apple_AppleID",
                table: "Image",
                column: "AppleID",
                principalTable: "Apple",
                principalColumn: "AppleID",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_ProductDetails_Apple_AppleID",
                table: "ProductDetails",
                column: "AppleID",
                principalTable: "Apple",
                principalColumn: "AppleID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
