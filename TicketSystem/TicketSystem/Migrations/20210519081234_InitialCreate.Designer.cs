﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketSystem.Data;

namespace TicketSystem.Migrations
{
    [DbContext(typeof(TicketSystemContext))]
    [Migration("20210519081234_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

          

            modelBuilder.Entity("TicketSystem.Models.Department", b =>
                {
                    b.Property<Guid>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime>("ModifDate17118162")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("DepartmentID");

                    b.HasIndex("DepartmentName")
                        .IsUnique();

                    b.ToTable("Department");
                });

            modelBuilder.Entity("TicketSystem.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                  

                    b.Property<Guid>("DepartmentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.Property<DateTime>("ModifDate17118162")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<Guid?>("TicketCategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeID");

                    

                    b.HasIndex("DepartmentID");

                    b.HasIndex("TicketCategoryID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("TicketSystem.Models.Ticket", b =>
                {
                    b.Property<Guid>("TicketID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CreatorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifDate17118162")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("OpenDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("TicketCategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TicketPriorityID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid?>("АcceptedАТicketID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketID");

                    b.HasIndex("CreatorID");

                    b.HasIndex("TicketCategoryID");

                    b.HasIndex("TicketPriorityID");

                    b.HasIndex("АcceptedАТicketID");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("TicketSystem.Models.TicketCategory", b =>
                {
                    b.Property<Guid>("TicketCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<DateTime>("ModifDate17118162")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("TicketCategoryID");

                    b.HasIndex("CategoryName")
                        .IsUnique();

                    b.ToTable("TicketCategory");
                });

            modelBuilder.Entity("TicketSystem.Models.TicketPriority", b =>
                {
                    b.Property<Guid>("TicketPriorityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifDate17118162")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("PriorityType")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("TicketPriorityID");

                    b.HasIndex("PriorityType")
                        .IsUnique();

                    b.ToTable("TicketPriority");
                });

            modelBuilder.Entity("TicketSystem.Models.TicketStatus", b =>
                {
                    b.Property<Guid>("TicketStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EmployeeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ModifDate17118162")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("StatusDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatusName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TicketID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TicketStatusID");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("TicketID");

                    b.ToTable("TicketStatus");
                });

            modelBuilder.Entity("TicketSystem.Models.Employee", b =>
                {
                    
                    b.HasOne("TicketSystem.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSystem.Models.TicketCategory", null)
                        .WithMany("Employees")
                        .HasForeignKey("TicketCategoryID");

                   

                    b.Navigation("Department");
                });

            modelBuilder.Entity("TicketSystem.Models.Ticket", b =>
                {
                    b.HasOne("TicketSystem.Models.Employee", "CreatorTicket")
                        .WithMany("CreatorTickets")
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TicketSystem.Models.TicketCategory", "TicketCategory")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSystem.Models.TicketPriority", "TicketPriority")
                        .WithMany("Tickets")
                        .HasForeignKey("TicketPriorityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSystem.Models.Employee", "АcceptedАТicket")
                        .WithMany("АcceptedАТickets")
                        .HasForeignKey("АcceptedАТicketID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("CreatorTicket");

                    b.Navigation("TicketCategory");

                    b.Navigation("TicketPriority");

                    b.Navigation("АcceptedАТicket");
                });

            modelBuilder.Entity("TicketSystem.Models.TicketStatus", b =>
                {
                    b.HasOne("TicketSystem.Models.Employee", "Employee")
                        .WithMany("TicketStatuses")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSystem.Models.Ticket", "Ticket")
                        .WithMany("TicketStatuses")
                        .HasForeignKey("TicketID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Ticket");
                });

            

            modelBuilder.Entity("TicketSystem.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TicketSystem.Models.Employee", b =>
                {
                    b.Navigation("CreatorTickets");

                    b.Navigation("TicketStatuses");

                    b.Navigation("АcceptedАТickets");
                });

            modelBuilder.Entity("TicketSystem.Models.Ticket", b =>
                {
                    b.Navigation("TicketStatuses");
                });

            modelBuilder.Entity("TicketSystem.Models.TicketCategory", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketSystem.Models.TicketPriority", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}