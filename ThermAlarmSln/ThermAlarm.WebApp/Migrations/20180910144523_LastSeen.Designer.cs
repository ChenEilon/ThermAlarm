﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThermAlarm.WebApp.Models;

namespace ThermAlarm.WebApp.Migrations
{
    [DbContext(typeof(ThermAlarmDbContext))]
    [Migration("20180910144523_LastSeen")]
    partial class LastSeen
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ThermAlarm.Common.AlarmAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("AlarmAction");
                });

            modelBuilder.Entity("ThermAlarm.Common.MsgObj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("idsBTScanInternal");

                    b.Property<int>("mType");

                    b.Property<int>("pirValue");

                    b.Property<string>("thermValueInternal");

                    b.HasKey("Id");

                    b.ToTable("Msg");
                });

            modelBuilder.Entity("ThermAlarm.Common.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BTid")
                        .IsRequired();

                    b.Property<DateTime>("LastSeen");

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("firstName")
                        .IsRequired();

                    b.Property<string>("lastName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Person");
                });
#pragma warning restore 612, 618
        }
    }
}
