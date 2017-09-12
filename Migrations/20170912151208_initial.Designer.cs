using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using designhubAPI.data;

namespace designhub.Migrations
{
    [DbContext(typeof(designhubAPIContext))]
    [Migration("20170912151208_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("designhubAPI.Models.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<int>("FileID");

                    b.Property<string>("Message")
                        .IsRequired();

                    b.HasKey("CommentID");

                    b.HasIndex("FileID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("designhubAPI.Models.File", b =>
                {
                    b.Property<int>("FileID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<int>("FileGroupID");

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.HasKey("FileID");

                    b.HasIndex("FileGroupID");

                    b.ToTable("File");
                });

            modelBuilder.Entity("designhubAPI.Models.FileGroup", b =>
                {
                    b.Property<int>("FileGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("FileGroupID");

                    b.ToTable("FileGroup");
                });

            modelBuilder.Entity("designhubAPI.Models.ProjectFileGroup", b =>
                {
                    b.Property<int>("ProjectFileGroupID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("FileGroupID");

                    b.Property<int>("ProjectsID");

                    b.HasKey("ProjectFileGroupID");

                    b.HasIndex("FileGroupID");

                    b.HasIndex("ProjectsID");

                    b.ToTable("ProjectFileGroup");
                });

            modelBuilder.Entity("designhubAPI.Models.Projects", b =>
                {
                    b.Property<int>("ProjectsID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("strftime('%Y-%m-%d')");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ProjectsID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("designhubAPI.Models.Comment", b =>
                {
                    b.HasOne("designhubAPI.Models.File", "File")
                        .WithMany()
                        .HasForeignKey("FileID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("designhubAPI.Models.File", b =>
                {
                    b.HasOne("designhubAPI.Models.FileGroup", "FileGroup")
                        .WithMany()
                        .HasForeignKey("FileGroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("designhubAPI.Models.ProjectFileGroup", b =>
                {
                    b.HasOne("designhubAPI.Models.FileGroup", "FileGroup")
                        .WithMany("ProjectFileGroup")
                        .HasForeignKey("FileGroupID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("designhubAPI.Models.Projects", "Projects")
                        .WithMany("ProjectFileGroup")
                        .HasForeignKey("ProjectsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
