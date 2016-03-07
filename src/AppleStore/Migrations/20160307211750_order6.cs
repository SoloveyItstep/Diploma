using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace AppleStore.Migrations
{
    public partial class order6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Apple_AppleID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Color_ColorID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_Image_Apple_AppleID", table: "Image");
            migrationBuilder.DropForeignKey(name: "FK_AppleOrders_Apple_AppleID", table: "AppleOrders");
            migrationBuilder.DropForeignKey(name: "FK_ProductDetails_Apple_AppleID", table: "ProductDetails");
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
                name: "FK_AppleOrders_Apple_AppleID",
                table: "AppleOrders",
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
            migrationBuilder.DropForeignKey(name: "FK_AppleOrders_Apple_AppleID", table: "AppleOrders");
            migrationBuilder.DropForeignKey(name: "FK_ProductDetails_Apple_AppleID", table: "ProductDetails");
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
                name: "FK_AppleOrders_Apple_AppleID",
                table: "AppleOrders",
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
