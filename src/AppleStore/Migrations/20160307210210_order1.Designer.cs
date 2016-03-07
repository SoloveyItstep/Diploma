using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Store.Context.Context;

namespace AppleStore.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20160307210210_order1")]
    partial class order1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Store.Entity.Apple", b =>
                {
                    b.Property<int>("AppleID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoriesCategoriesID");

                    b.Property<string>("Construction")
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("Model")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<decimal>("Price");

                    b.Property<string>("Subcategory")
                        .HasAnnotation("MaxLength", 30);

                    b.Property<string>("Type")
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("Url")
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("AppleID");
                });

            modelBuilder.Entity("Store.Entity.AppleColor", b =>
                {
                    b.Property<int>("AppleColorID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppleID");

                    b.Property<int>("ColorID");

                    b.Property<int>("Count");

                    b.HasKey("AppleColorID");
                });

            modelBuilder.Entity("Store.Entity.Categories", b =>
                {
                    b.Property<int>("CategoriesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("CategoriesID");
                });

            modelBuilder.Entity("Store.Entity.Color", b =>
                {
                    b.Property<int>("ColorID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorName")
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("ColorID");
                });

            modelBuilder.Entity("Store.Entity.Currency", b =>
                {
                    b.Property<int>("CurrencyID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CurrencyUSD")
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("Date")
                        .HasAnnotation("MaxLength", 10);

                    b.HasKey("CurrencyID");
                });

            modelBuilder.Entity("Store.Entity.DetailNames", b =>
                {
                    b.Property<int>("DetailNamesID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 30);

                    b.HasKey("DetailNamesID");
                });

            modelBuilder.Entity("Store.Entity.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppleID");

                    b.Property<int>("ColorID");

                    b.Property<string>("Path")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("Size")
                        .HasAnnotation("MaxLength", 6);

                    b.HasKey("ImageID");
                });

            modelBuilder.Entity("Store.Entity.ProductDetails", b =>
                {
                    b.Property<int>("ProductDetailsID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AppleID");

                    b.Property<int?>("DetailNamesDetailNamesID");

                    b.Property<string>("Measure")
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("Other");

                    b.Property<string>("Value")
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("ProductDetailsID");
                });

            modelBuilder.Entity("Store.Entity.Apple", b =>
                {
                    b.HasOne("Store.Entity.Categories")
                        .WithMany()
                        .HasForeignKey("CategoriesCategoriesID");
                });

            modelBuilder.Entity("Store.Entity.AppleColor", b =>
                {
                    b.HasOne("Store.Entity.Apple")
                        .WithMany()
                        .HasForeignKey("AppleID");

                    b.HasOne("Store.Entity.Color")
                        .WithMany()
                        .HasForeignKey("ColorID");
                });

            modelBuilder.Entity("Store.Entity.Image", b =>
                {
                    b.HasOne("Store.Entity.Apple")
                        .WithMany()
                        .HasForeignKey("AppleID");
                });

            modelBuilder.Entity("Store.Entity.ProductDetails", b =>
                {
                    b.HasOne("Store.Entity.Apple")
                        .WithMany()
                        .HasForeignKey("AppleID");

                    b.HasOne("Store.Entity.DetailNames")
                        .WithMany()
                        .HasForeignKey("DetailNamesDetailNamesID");
                });
        }
    }
}
