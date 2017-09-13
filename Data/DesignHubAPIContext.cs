using Microsoft.EntityFrameworkCore;
using designhubAPI.Models;

namespace designhubAPI.data
{
    public class designhubAPIContext : DbContext
    {
        public designhubAPIContext(DbContextOptions<designhubAPIContext> options)
            : base(options)
        {}

        public DbSet<Comment> Comment {get; set;}
        public DbSet<File> File {get; set;}
        public DbSet<FileGroup> FileGroup {get; set;}
        public DbSet<Projects> Projects {get; set;}
        public DbSet<ProjectFileGroup> ProjectFileGroup {get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
            .Property(b => b.DateCreated)
            .HasDefaultValueSql("strftime('%Y-%m-%d %H %M %S')");

             modelBuilder.Entity<File>()
            .Property(b => b.DateCreated)
            .HasDefaultValueSql("strftime('%Y-%m-%d %H %M %S')");

             modelBuilder.Entity<FileGroup>()
            .Property(b => b.DateCreated)
            .HasDefaultValueSql("strftime('%Y-%m-%d %H %M %S')");

             modelBuilder.Entity<Projects>()
            .Property(b => b.DateCreated)
            .HasDefaultValueSql("strftime('%Y-%m-%d %H %M %S')");
        }


    }
}