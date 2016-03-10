using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace AppleStore.Migrations
{
    public partial class orders1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoriesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoriesID);
                });
            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ColorID);
                });
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
            migrationBuilder.CreateTable(
                name: "DetailNames",
                columns: table => new
                {
                    DetailNamesID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailNames", x => x.DetailNamesID);
                });
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrdersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<string>(nullable: false),
                    Delivery = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: false),
                    Payment = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Sum = table.Column<decimal>(nullable: false),
                    UserID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrdersID);
                });
            migrationBuilder.CreateTable(
                name: "Apple",
                columns: table => new
                {
                    AppleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriesCategoriesID = table.Column<int>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Subcategory = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apple", x => x.AppleID);
                    table.ForeignKey(
                        name: "FK_Apple_Categories_CategoriesCategoriesID",
                        column: x => x.CategoriesCategoriesID,
                        principalTable: "Categories",
                        principalColumn: "CategoriesID",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "AppleColor",
                columns: table => new
                {
                    AppleColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppleID = table.Column<int>(nullable: false),
                    ColorID = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppleColor", x => x.AppleColorID);
                    table.ForeignKey(
                        name: "FK_AppleColor_Apple_AppleID",
                        column: x => x.AppleID,
                        principalTable: "Apple",
                        principalColumn: "AppleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppleColor_Color_ColorID",
                        column: x => x.ColorID,
                        principalTable: "Color",
                        principalColumn: "ColorID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppleID = table.Column<int>(nullable: false),
                    ColorID = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK_Image_Apple_AppleID",
                        column: x => x.AppleID,
                        principalTable: "Apple",
                        principalColumn: "AppleID",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "AppleOrders",
                columns: table => new
                {
                    AppleOrdersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppleID = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: true),
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
            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppleID = table.Column<int>(nullable: false),
                    DetailNamesDetailNamesID = table.Column<int>(nullable: true),
                    Measure = table.Column<string>(nullable: true),
                    Other = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductDetailsID);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Apple_AppleID",
                        column: x => x.AppleID,
                        principalTable: "Apple",
                        principalColumn: "AppleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetails_DetailNames_DetailNamesDetailNamesID",
                        column: x => x.DetailNamesDetailNamesID,
                        principalTable: "DetailNames",
                        principalColumn: "DetailNamesID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("AppleColor");
            migrationBuilder.DropTable("Currency");
            migrationBuilder.DropTable("Image");
            migrationBuilder.DropTable("AppleOrders");
            migrationBuilder.DropTable("ProductDetails");
            migrationBuilder.DropTable("Color");
            migrationBuilder.DropTable("Orders");
            migrationBuilder.DropTable("Apple");
            migrationBuilder.DropTable("DetailNames");
            migrationBuilder.DropTable("Categories");
        }
    }
}
