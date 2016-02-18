using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace AppleStore.Migrations
{
    public partial class currency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Apple_AppleID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_AppleColor_Color_ColorID", table: "AppleColor");
            migrationBuilder.DropForeignKey(name: "FK_Image_Apple_AppleID", table: "Image");
            migrationBuilder.DropForeignKey(name: "FK_ProductDetails_Apple_AppleID", table: "ProductDetails");
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrencyUSD = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyID);
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
            migrationBuilder.DropTable("Currency");
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
