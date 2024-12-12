﻿// <auto-generated />
using System;
using GMarket.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GMarket.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GMarket.Domain.Entities.Forum.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FormattedContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Header")
                        .IsRequired()
                        .HasColumnType("text");

                    b.PrimitiveCollection<string[]>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.PrimitiveCollection<string[]>("s3Images")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Forum.ArticleCommentary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CommentaryText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ReplyToCommentaryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ReplyToCommentaryId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleCommentaries");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("S3Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserSecurityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserSecurityId")
                        .IsUnique();

                    b.ToTable("CustomerContexts");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.FavoriteCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductIds")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("FavoriteCategories");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<decimal>("BuyPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TrackCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.UserSecurity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Password");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("Role");

                    b.HasKey("Id");

                    b.ToTable("UserSecurity");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Market.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductItemId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Market.ProductItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.PrimitiveCollection<string[]>("Images")
                        .HasColumnType("text[]");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.PrimitiveCollection<string[]>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Market.ProductReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.PrimitiveCollection<string[]>("Images")
                        .HasColumnType("text[]");

                    b.Property<int>("Mark")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReplyText")
                        .HasColumnType("text");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProductItemId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductReviews");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Forum.Article", b =>
                {
                    b.HasOne("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Forum.ArticleCommentary", b =>
                {
                    b.HasOne("GMarket.Domain.Entities.Forum.Article", "Article")
                        .WithMany("ArticleCommentaries")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GMarket.Domain.Entities.Forum.ArticleCommentary", "ReplyToCommentary")
                        .WithMany()
                        .HasForeignKey("ReplyToCommentaryId");

                    b.HasOne("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", "User")
                        .WithMany("ArticleCommentaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("ReplyToCommentary");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", b =>
                {
                    b.HasOne("GMarket.Domain.Entities.IdentityCustomer.UserSecurity", "UserSecurity")
                        .WithOne("Context")
                        .HasForeignKey("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", "UserSecurityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSecurity");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.FavoriteCategory", b =>
                {
                    b.HasOne("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", "User")
                        .WithMany("FavoritesCategories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.OrderItem", b =>
                {
                    b.HasOne("GMarket.Domain.Entities.Market.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", "User")
                        .WithMany("OrderItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Market.ProductItem", b =>
                {
                    b.HasOne("GMarket.Domain.Entities.Market.Product", "Product")
                        .WithOne("ProductItem")
                        .HasForeignKey("GMarket.Domain.Entities.Market.ProductItem", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Market.ProductReview", b =>
                {
                    b.HasOne("GMarket.Domain.Entities.Market.ProductItem", "ProductItem")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", "User")
                        .WithMany("ProductReviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Forum.Article", b =>
                {
                    b.Navigation("ArticleCommentaries");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.CustomerContext", b =>
                {
                    b.Navigation("ArticleCommentaries");

                    b.Navigation("Articles");

                    b.Navigation("FavoritesCategories");

                    b.Navigation("OrderItems");

                    b.Navigation("ProductReviews");
                });

            modelBuilder.Entity("GMarket.Domain.Entities.IdentityCustomer.UserSecurity", b =>
                {
                    b.Navigation("Context")
                        .IsRequired();
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Market.Product", b =>
                {
                    b.Navigation("ProductItem")
                        .IsRequired();
                });

            modelBuilder.Entity("GMarket.Domain.Entities.Market.ProductItem", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
