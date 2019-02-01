using KBS2.WijkagentApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KBS2.WijkagentApp.API.Context
{
    public partial class WijkagentContext : DbContext
    {
        public WijkagentContext()
        {
        }

        public WijkagentContext(DbContextOptions<WijkagentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Antecedent> Antecedent { get; set; }
        public virtual DbSet<Emergency> Emergency { get; set; }
        public virtual DbSet<Officer> Officer { get; set; }
        public virtual DbSet<OfficialReport> OfficialReport { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<PushMessage> PushMessage { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<ReportDetails> ReportDetails { get; set; }
        public virtual DbSet<SocialMessage> SocialMessage { get; set; }
        public virtual DbSet<Socials> Socials { get; set; }
        public virtual DbSet<SoundRecord> SoundRecord { get; set; }
        public virtual DbSet<testTable> testTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.personId);

                entity.Property(e => e.personId).ValueGeneratedNever();

                entity.Property(e => e.number)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.street)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.town)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.zipcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Antecedent>(entity =>
            {
                entity.HasKey(e => e.antecedentId);

                entity.Property(e => e.antecedentId);

                entity.Property(e => e.personId);

                entity.Property(e => e.crime)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.verdict)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Emergency>(entity =>
            {
                entity.HasKey(e => e.emergencyId);

                entity.Property(e => e.emergencyId);

                entity.Property(e => e.officerId).ValueGeneratedNever();

                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.time).HasColumnType("time(7)");

                entity.Property(e => e.latitude).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.longitude).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.status)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Officer>(entity =>
            {
                entity.HasKey(e => e.officerId);

                entity.Property(e => e.officerId);

                entity.Property(e => e.personId).ValueGeneratedNever();

                entity.Property(e => e.passWord)
                    .HasMaxLength(356)
                    .IsUnicode(false);

                entity.Property(e => e.salt)
                    .HasMaxLength(228)
                    .IsUnicode(false);

                entity.Property(e => e.userName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OfficialReport>(entity =>
            {
                entity.HasKey(e => e.officialReportId);

                entity.Property(e => e.officialReportId);

                entity.Property(e => e.reporterId).ValueGeneratedNever();

                entity.Property(e => e.reportId).ValueGeneratedNever();

                entity.Property(e => e.time).HasColumnType("time(7)");

                entity.Property(e => e.location)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.observation)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.personId);

                entity.Property(e => e.personId);

                entity.Property(e => e.socialSecurityNumber).HasColumnType("int");

                entity.Property(e => e.birthDate).HasColumnType("date");

                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.emailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.firstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.lastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.phoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.HasKey(e => e.pictureId);

                entity.Property(e => e.pictureId);

                entity.Property(e => e.officialReportId).ValueGeneratedNever();

                entity.Property(e => e.URL)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PushMessage>(entity =>
            {
                entity.HasKey(e => e.pushMessageId);

                entity.Property(e => e.pushMessageId);

                entity.Property(e => e.officerId).ValueGeneratedNever();

                entity.Property(e => e.message)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.time).HasColumnType("time(7)");

                entity.Property(e => e.location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.latitude).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.longitude).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.status)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.reportId);
                
                entity.Property(e => e.reportId);

                entity.Property(e => e.reporterId).ValueGeneratedNever();

                entity.Property(e => e.processedBy).ValueGeneratedNever();

                entity.Property(e => e.time).HasColumnType("time(7)");

                entity.Property(e => e.comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.latitude).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.location)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.longitude).HasColumnType("decimal(18, 10)");

                entity.Property(e => e.status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReportDetails>(entity =>
            {
                entity.HasKey(e => e.reportDetailsId);

                entity.Property(e => e.reportDetailsId);

                entity.Property(e => e.reportId).ValueGeneratedNever();

                entity.Property(e => e.personId).ValueGeneratedNever();

                entity.Property(e => e.description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.statement)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.type)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SocialMessage>(entity =>
            {
                entity.HasKey(e => e.socialMessageId);

                entity.Property(e => e.socialMessageId);

                entity.Property(e => e.socialsId).ValueGeneratedNever();

                entity.Property(e => e.message)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Socials>(entity =>
            {
                entity.HasKey(e => e.socialsId);

                entity.Property(e => e.socialsId);

                entity.Property(e => e.profile)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SoundRecord>(entity =>
            {
                entity.HasKey(e => e.soundRecordId);

                entity.Property(e => e.soundRecordId);

                entity.Property(e => e.officialReportId).ValueGeneratedNever();

                entity.Property(e => e.URL)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<testTable>(entity =>
            {
                entity.HasKey(e => e.id);

                entity.Property(e => e.id);
                
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