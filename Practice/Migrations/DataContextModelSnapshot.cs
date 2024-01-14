﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Practice.Data;

#nullable disable

namespace Practice.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Practice.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Class")
                        .HasColumnType("integer");

                    b.Property<int>("Defense")
                        .HasColumnType("integer");

                    b.Property<int>("HitPoints")
                        .HasColumnType("integer");

                    b.Property<int>("Intelligence")
                        .HasColumnType("integer");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SkillPoints")
                        .HasColumnType("integer");

                    b.Property<int>("Strength")
                        .HasColumnType("integer");

                    b.Property<int>("XP")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("Practice.Models.CharacterQuest", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<int>("QuestId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateAccepted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<int>("Progress")
                        .HasColumnType("integer");

                    b.HasKey("CharacterId", "QuestId");

                    b.HasIndex("QuestId");

                    b.ToTable("CharacterQuests");
                });

            modelBuilder.Entity("Practice.Models.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Reward")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("Practice.Models.CharacterQuest", b =>
                {
                    b.HasOne("Practice.Models.Character", "Character")
                        .WithMany("CharacterQuests")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Practice.Models.Quest", "Quest")
                        .WithMany("CharacterQuests")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Quest");
                });

            modelBuilder.Entity("Practice.Models.Character", b =>
                {
                    b.Navigation("CharacterQuests");
                });

            modelBuilder.Entity("Practice.Models.Quest", b =>
                {
                    b.Navigation("CharacterQuests");
                });
#pragma warning restore 612, 618
        }
    }
}
