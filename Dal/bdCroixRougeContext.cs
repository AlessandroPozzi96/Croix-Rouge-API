using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CroixRouge.Model;

namespace CroixRouge.Dal
{
    public partial class bdCroixRougeContext : DbContext
    {
        public bdCroixRougeContext()
        {
        }

        public bdCroixRougeContext(DbContextOptions<bdCroixRougeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresse> Adresse { get; set; }
        public virtual DbSet<Alerte> Alerte { get; set; }
        public virtual DbSet<Collecte> Collecte { get; set; }
        public virtual DbSet<Diffuserimage> Diffuserimage { get; set; }
        public virtual DbSet<Don> Don { get; set; }
        public virtual DbSet<Groupesanguin> Groupesanguin { get; set; }
        public virtual DbSet<Imagepromotion> Imagepromotion { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<Jourouverture> Jourouverture { get; set; }
        public virtual DbSet<Lanceralerte> Lanceralerte { get; set; }
        public virtual DbSet<Partagerimage> Partagerimage { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<TrancheHoraire> TrancheHoraire { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(4);

                entity.Property(e => e.Rue)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Ville)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Alerte>(entity =>
            {
                entity.Property(e => e.Contenu)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.FkGroupesanguin)
                    .HasColumnName("Fk_Groupesanguin")
                    .HasMaxLength(3);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkGroupesanguinNavigation)
                    .WithMany(p => p.Alerte)
                    .HasForeignKey(d => d.FkGroupesanguin)
                    .HasConstraintName("FK__Alerte__Fk_Group__70BE939E");
            });

            modelBuilder.Entity<Collecte>(entity =>
            {
                entity.Property(e => e.Latitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(9, 6)");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion();
            });

            modelBuilder.Entity<Diffuserimage>(entity =>
            {
                entity.Property(e => e.FkImage)
                    .IsRequired()
                    .HasColumnName("Fk_Image")
                    .HasMaxLength(400);

                entity.Property(e => e.FkUtilisateur)
                    .IsRequired()
                    .HasColumnName("Fk_Utilisateur")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkImageNavigation)
                    .WithMany(p => p.Diffuserimage)
                    .HasForeignKey(d => d.FkImage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Diffuseri__Fk_Im__6CEE02BA");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Diffuserimage)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Diffuseri__Fk_Ut__6DE226F3");
            });

            modelBuilder.Entity<Don>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FkCollecte).HasColumnName("Fk_Collecte");

                entity.Property(e => e.FkUtilisateur)
                    .IsRequired()
                    .HasColumnName("Fk_Utilisateur")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkCollecteNavigation)
                    .WithMany(p => p.Don)
                    .HasForeignKey(d => d.FkCollecte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Don__Fk_Collecte__7B3C2211");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Don)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Don__Fk_Utilisat__7C30464A");
            });

            modelBuilder.Entity<Groupesanguin>(entity =>
            {
                entity.HasKey(e => e.Nom);

                entity.Property(e => e.Nom)
                    .HasMaxLength(3)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Imagepromotion>(entity =>
            {
                entity.HasKey(e => e.Url);

                entity.Property(e => e.Url)
                    .HasMaxLength(400)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.Property(e => e.Question).IsRequired();

                entity.Property(e => e.Reponse).IsRequired();
            });

            modelBuilder.Entity<Jourouverture>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.FkCollecte).HasColumnName("Fk_Collecte");

                entity.Property(e => e.FkTrancheHoraire).HasColumnName("Fk_TrancheHoraire");

                entity.Property(e => e.LibelleJour).HasMaxLength(8);

                entity.HasOne(d => d.FkCollecteNavigation)
                    .WithMany(p => p.Jourouverture)
                    .HasForeignKey(d => d.FkCollecte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jourouver__Fk_Co__776B912D");

                entity.HasOne(d => d.FkTrancheHoraireNavigation)
                    .WithMany(p => p.Jourouverture)
                    .HasForeignKey(d => d.FkTrancheHoraire)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jourouver__Fk_Tr__785FB566");
            });

            modelBuilder.Entity<Lanceralerte>(entity =>
            {
                entity.Property(e => e.FkAlerte).HasColumnName("Fk_Alerte");

                entity.Property(e => e.FkUtilisateur)
                    .IsRequired()
                    .HasColumnName("Fk_Utilisateur")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkAlerteNavigation)
                    .WithMany(p => p.Lanceralerte)
                    .HasForeignKey(d => d.FkAlerte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lancerale__Fk_Al__7F0CB2F5");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Lanceralerte)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lancerale__Fk_Ut__0000D72E");
            });

            modelBuilder.Entity<Partagerimage>(entity =>
            {
                entity.Property(e => e.FkImage)
                    .IsRequired()
                    .HasColumnName("Fk_Image")
                    .HasMaxLength(400);

                entity.Property(e => e.FkUtilisateur)
                    .IsRequired()
                    .HasColumnName("Fk_Utilisateur")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkImageNavigation)
                    .WithMany(p => p.Partagerimage)
                    .HasForeignKey(d => d.FkImage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Partageri__Fk_Im__691D71D6");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Partagerimage)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Partageri__Fk_Ut__6A11960F");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Libelle);

                entity.Property(e => e.Libelle)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.Login);

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.DateNaissance).HasColumnType("date");

                entity.Property(e => e.FkAdresse).HasColumnName("Fk_Adresse");

                entity.Property(e => e.FkGroupesanguin)
                    .HasColumnName("Fk_Groupesanguin")
                    .HasMaxLength(3);

                entity.Property(e => e.FkRole)
                    .IsRequired()
                    .HasColumnName("Fk_Role")
                    .HasMaxLength(50);

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(320);

                entity.Property(e => e.Nom).HasMaxLength(100);

                entity.Property(e => e.NumGsm).HasColumnName("NumGSM");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Prenom).HasMaxLength(100);

                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkAdresseNavigation)
                    .WithMany(p => p.Utilisateur)
                    .HasForeignKey(d => d.FkAdresse)
                    .HasConstraintName("FK__Utilisate__Fk_Ad__654CE0F2");

                entity.HasOne(d => d.FkGroupesanguinNavigation)
                    .WithMany(p => p.Utilisateur)
                    .HasForeignKey(d => d.FkGroupesanguin)
                    .HasConstraintName("FK__Utilisate__Fk_Gr__6641052B");

                entity.HasOne(d => d.FkRoleNavigation)
                    .WithMany(p => p.Utilisateur)
                    .HasForeignKey(d => d.FkRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Utilisate__Fk_Ro__6458BCB9");
            });
        }
    }
}
