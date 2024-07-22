﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ticketing.DataAccessLayer.Context;

#nullable disable

namespace Ticketing.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.6.24327.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketSupporter", b =>
                {
                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TicketId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TicketSupporter");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentMessageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("TicketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentMessageId");

                    b.HasIndex("SenderId");

                    b.HasIndex("TicketId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.SupporterRating", b =>
                {
                    b.Property<int>("RatedUserId")
                        .HasColumnType("int");

                    b.Property<int>("RelatedTicketId")
                        .HasColumnType("int");

                    b.Property<int>("SupporterId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("RatedUserId", "RelatedTicketId", "SupporterId");

                    b.HasIndex("RelatedTicketId");

                    b.HasIndex("SupporterId");

                    b.ToTable("SupporterRatings");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TicketSupporter", b =>
                {
                    b.HasOne("Ticketing.DataAccessLayer.Entities.Ticket", null)
                        .WithMany()
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ticketing.DataAccessLayer.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.Message", b =>
                {
                    b.HasOne("Ticketing.DataAccessLayer.Entities.Message", "ParentMessage")
                        .WithMany("Replies")
                        .HasForeignKey("ParentMessageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ticketing.DataAccessLayer.Entities.User", "Sender")
                        .WithMany("Messages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ticketing.DataAccessLayer.Entities.Ticket", "Ticket")
                        .WithMany("Messages")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentMessage");

                    b.Navigation("Sender");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.SupporterRating", b =>
                {
                    b.HasOne("Ticketing.DataAccessLayer.Entities.User", "RatedUser")
                        .WithMany()
                        .HasForeignKey("RatedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ticketing.DataAccessLayer.Entities.Ticket", "RelatedTicket")
                        .WithMany()
                        .HasForeignKey("RelatedTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ticketing.DataAccessLayer.Entities.User", "Supporter")
                        .WithMany()
                        .HasForeignKey("SupporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RatedUser");

                    b.Navigation("RelatedTicket");

                    b.Navigation("Supporter");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.Ticket", b =>
                {
                    b.HasOne("Ticketing.DataAccessLayer.Entities.User", "Creator")
                        .WithMany("CreatedTickets")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.Message", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.Ticket", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Ticketing.DataAccessLayer.Entities.User", b =>
                {
                    b.Navigation("CreatedTickets");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
