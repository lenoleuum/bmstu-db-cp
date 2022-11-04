using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using db_cp.data_access.EntityStructures;

namespace db_cp.data_access
{
    public partial class MSSQLContext : DbContext
    {
        private System.String ConString;
        public MSSQLContext()
        {
            this.ConString = "Server=DESKTOP-2LBKKJG;Database=db_cp;Persist Security Info=False;" +
            "TrustServerCertificate=true;User Id=sa;Password=1234;";
        }
        public MSSQLContext(string con)
        {
            //this.ConString = con;
            this.ConString = System.Configuration.ConfigurationManager.ConnectionStrings["MSSQL"].ConnectionString;
        }

        public MSSQLContext(DbContextOptions<MSSQLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Playlists> Playlists { get; set; }
        public virtual DbSet<Tp> Tp { get; set; }
        public virtual DbSet<Tracks> Tracks { get; set; }
        public virtual DbSet<Tu> Tu { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=DESKTOP-2LBKKJG;Database=db_cp;Persist Security Info=False;TrustServerCertificate=true;User Id=sa;Password=1234;");
                optionsBuilder.UseSqlServer(this.ConString);

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlists>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.DateOfCreation)
                    .HasColumnName("date_of_creation")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Playlists__autho__3A81B327");
            });

            modelBuilder.Entity<Tp>(entity =>
            {
                entity.ToTable("TP");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.Tp)
                    .HasForeignKey(d => d.IdPlaylist)
                    .HasConstraintName("FK__TP__id_playlist__3E52440B");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.Tp)
                    .HasForeignKey(d => d.IdTrack)
                    .HasConstraintName("FK__TP__id_track__3D5E1FD2");
            });

            modelBuilder.Entity<Tracks>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateOfCreation)
                    .HasColumnName("date_of_creation")
                    .HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.MidiFile)
                    .IsRequired()
                    .HasColumnName("midi_file")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tu>(entity =>
            {
                entity.ToTable("TU");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                //entity.HasOne(d => d.IdTrackNavigation)
                entity.HasOne(d => d.Track)
                    .WithMany(p => p.Tu)
                    .HasForeignKey(d => d.IdTrack)
                    .HasConstraintName("FK__TU__id_track__412EB0B6");

                //entity.HasOne(d => d.IdUserNavigation)
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tu)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__TU__id_user__4222D4EF");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("datetime");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserType).HasColumnName("user_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
