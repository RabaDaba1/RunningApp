﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RunningApp.Data;

#nullable disable

namespace RunningApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("RunningApp.Models.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Athletes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Arek"
                        },
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Kiptum"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Maciek"
                        },
                        new
                        {
                            Id = 4,
                            DateOfBirth = new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Kacper"
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Przemek"
                        });
                });

            modelBuilder.Entity("RunningApp.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Distance")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = "19.08.2023",
                            Distance = "10 km",
                            Location = "Warszawa",
                            Name = "Warszawska Dycha"
                        },
                        new
                        {
                            Id = 2,
                            Date = "29.08.2023",
                            Distance = "21 km",
                            Location = "Warszawa",
                            Name = "Półmaraton Warszawski"
                        },
                        new
                        {
                            Id = 3,
                            Date = "31.06.2022",
                            Distance = "42 km",
                            Location = "Warszawa",
                            Name = "Maraton Krakowski"
                        },
                        new
                        {
                            Id = 4,
                            Date = "09.07.2021",
                            Distance = "5 km",
                            Location = "Warszawa",
                            Name = "Zabawna Piątka"
                        });
                });

            modelBuilder.Entity("RunningApp.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AthleteId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<TimeSpan?>("Time")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.HasIndex("EventId");

                    b.ToTable("Results");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AthleteId = 1,
                            EventId = 1,
                            Time = new TimeSpan(0, 0, 25, 30, 0)
                        },
                        new
                        {
                            Id = 2,
                            AthleteId = 2,
                            EventId = 1,
                            Time = new TimeSpan(0, 0, 24, 10, 0)
                        },
                        new
                        {
                            Id = 3,
                            AthleteId = 3,
                            EventId = 2,
                            Time = new TimeSpan(0, 0, 30, 45, 0)
                        },
                        new
                        {
                            Id = 4,
                            AthleteId = 4,
                            EventId = 2,
                            Time = new TimeSpan(0, 0, 27, 15, 0)
                        },
                        new
                        {
                            Id = 5,
                            AthleteId = 5,
                            EventId = 3,
                            Time = new TimeSpan(0, 0, 29, 50, 0)
                        });
                });

            modelBuilder.Entity("RunningApp.Models.Result", b =>
                {
                    b.HasOne("RunningApp.Models.Athlete", "Athlete")
                        .WithMany("Results")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RunningApp.Models.Event", "Event")
                        .WithMany("Results")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("RunningApp.Models.Athlete", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("RunningApp.Models.Event", b =>
                {
                    b.Navigation("Results");
                });
#pragma warning restore 612, 618
        }
    }
}
