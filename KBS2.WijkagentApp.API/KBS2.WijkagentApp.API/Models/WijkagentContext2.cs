using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KBS2.WijkagentApp.API.Models
{
    public partial class WijkagentContext2 : DbContext
    {
        public WijkagentContext2()
        {
        }

        public WijkagentContext2(DbContextOptions<WijkagentContext2> options)
            : base(options)
        {
        }

        public virtual DbSet<testTable> testTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=wijkagent.database.windows.net;Initial Catalog=Wijkagent;Persist Security Info=True;User ID=dbAgent1;Password=123dbAgent!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<testTable>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.alsosomething)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.something)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}