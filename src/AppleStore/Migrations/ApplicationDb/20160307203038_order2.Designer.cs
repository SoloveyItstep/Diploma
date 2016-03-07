using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using AppleStore.Models;

namespace AppleStore.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160307203038_order2")]
    partial class order2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppleStore.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 40);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 20);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("OrdersID");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("AppleStore.Models.CartEntity.Orders", b =>
                {
                    b.Property<string>("id");

                    b.Property<int>("AppleID");

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Devivery");

                    b.Property<string>("OrdersID");

                    b.Property<string>("Payment");

                    b.Property<string>("Status");

                    b.Property<decimal>("Sum");

                    b.HasKey("id");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "AspNetUserRoles");
                });

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

                    b.Property<string>("Ordersid");

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

            modelBuilder.Entity("AppleStore.Models.CartEntity.Orders", b =>
                {
                    b.HasOne("AppleStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AppleStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AppleStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNet.Identity.EntityFramework.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("AppleStore.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Store.Entity.Apple", b =>
                {
                    b.HasOne("Store.Entity.Categories")
                        .WithMany()
                        .HasForeignKey("CategoriesCategoriesID");

                    b.HasOne("AppleStore.Models.CartEntity.Orders")
                        .WithMany()
                        .HasForeignKey("Ordersid");
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
