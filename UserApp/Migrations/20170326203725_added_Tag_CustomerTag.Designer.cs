using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UserApp.Data;

namespace UserApp.Migrations
{
    [DbContext(typeof(HRContext))]
    [Migration("20170326203725_added_Tag_CustomerTag")]
    partial class added_Tag_CustomerTag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserApp.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("UserApp.Models.ContactTag", b =>
                {
                    b.Property<int>("ContactId");

                    b.Property<int>("TagId");

                    b.HasKey("ContactId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ContactTag");
                });

            modelBuilder.Entity("UserApp.Models.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContactId");

                    b.Property<string>("String");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Email");
                });

            modelBuilder.Entity("UserApp.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContactId");

                    b.Property<string>("String");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("UserApp.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("UserApp.Models.ContactTag", b =>
                {
                    b.HasOne("UserApp.Models.Contact", "Contact")
                        .WithMany("ContactTag")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UserApp.Models.Tag", "Tag")
                        .WithMany("ContactTag")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UserApp.Models.Email", b =>
                {
                    b.HasOne("UserApp.Models.Contact", "Contact")
                        .WithMany("Email")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UserApp.Models.Phone", b =>
                {
                    b.HasOne("UserApp.Models.Contact", "Contact")
                        .WithMany("Phone")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
