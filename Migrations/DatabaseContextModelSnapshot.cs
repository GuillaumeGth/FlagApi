﻿// <auto-generated />
using System;
using FlagApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

namespace FlagApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("FlagApi.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uuid")
                        .HasColumnName("content_id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<NpgsqlPoint>("Location")
                        .HasColumnType("point")
                        .HasColumnName("location");

                    b.Property<Guid?>("RecipientId")
                        .HasColumnType("uuid")
                        .HasColumnName("recipient_id");

                    b.Property<string>("Text")
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("RecipientId");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("FlagApi.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("text")
                        .HasColumnName("picture_url");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("FlagApi.Models.Message", b =>
                {
                    b.HasOne("FlagApi.Models.User", "Author")
                        .WithMany("MessagesSent")
                        .HasForeignKey("AuthorId");

                    b.HasOne("FlagApi.Models.User", "Recipient")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("RecipientId");

                    b.Navigation("Author");

                    b.Navigation("Recipient");
                });

            modelBuilder.Entity("FlagApi.Models.User", b =>
                {
                    b.Navigation("MessagesReceived");

                    b.Navigation("MessagesSent");
                });
#pragma warning restore 612, 618
        }
    }
}
