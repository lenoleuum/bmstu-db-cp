using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using db_cp.data_access.EntityStructures;

namespace db_cp.data_access
{
    public partial class PSQLContext : DbContext
    {
        private System.String ConString;
        public PSQLContext()
        {
            //this.ConString = "Host=localhost;Port=5432;Database=db_cp;Username=postgres;Password=1234";
            this.ConString = System.Configuration.ConfigurationManager.ConnectionStrings["PSQL"].ConnectionString;
        }

        public PSQLContext(string con)
        {
            //this.ConString = "Host=localhost;Port=5432;Database=db_cp;Username=" + Login + ";Password" + Password;
            this.ConString = con;
        }

        public PSQLContext(DbContextOptions<PSQLContext> options)
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
                //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_cp;Username=postgres;Password=1234");
                optionsBuilder.UseNpgsql(this.ConString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Playlists>(entity =>
            {
                entity.ToTable("playlists");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.DateOfCreation)
                    .HasColumnName("date_of_creation")
                    .HasColumnType("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("playlists_author_id_fkey");
            });

            modelBuilder.Entity<Tp>(entity =>
            {
                entity.ToTable("tp");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdPlaylist).HasColumnName("id_playlist");

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.HasOne(d => d.IdPlaylistNavigation)
                    .WithMany(p => p.Tp)
                    .HasForeignKey(d => d.IdPlaylist)
                    .HasConstraintName("tp_id_playlist_fkey");

                entity.HasOne(d => d.IdTrackNavigation)
                    .WithMany(p => p.Tp)
                    .HasForeignKey(d => d.IdTrack)
                    .HasConstraintName("tp_id_track_fkey");
            });

            modelBuilder.Entity<Tracks>(entity =>
            {
                entity.ToTable("tracks");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOfCreation)
                    .HasColumnName("date_of_creation")
                    .HasColumnType("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("time without time zone");

                entity.Property(e => e.MidiFile).HasColumnName("midi_file");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");
            });

            modelBuilder.Entity<Tu>(entity =>
            {
                entity.ToTable("tu");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdTrack).HasColumnName("id_track");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.HasOne(d => d.Track)
                    .WithMany(p => p.Tu)
                    .HasForeignKey(d => d.IdTrack)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tu_track_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tu)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tu_user_id_fkey");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasColumnType("character varying");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("date_of_birth")
                    .HasColumnType("date");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasColumnType("character varying");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasColumnName("mail")
                    .HasColumnType("character varying");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("character varying");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("character varying");

                entity.Property(e => e.UserType).HasColumnName("user_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
