﻿using System;
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
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    .HasConstraintName("FK__Alerte__Fk_Group__0F2E0C25");
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
                entity.Property(e => e.FkImage).HasColumnName("Fk_Image");

                entity.Property(e => e.FkUtilisateur)
                    .IsRequired()
                    .HasColumnName("Fk_Utilisateur")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkImageNavigation)
                    .WithMany(p => p.Diffuserimage)
                    .HasForeignKey(d => d.FkImage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Diffuseri__Fk_Im__0B5D7B41");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Diffuserimage)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Diffuseri__Fk_Ut__0C519F7A");
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
                    .HasConstraintName("FK__Don__Fk_Collecte__16CF2DED");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Don)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Don__Fk_Utilisat__17C35226");
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
                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(2083);
                    
                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion();
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

                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkCollecteNavigation)
                    .WithMany(p => p.Jourouverture)
                    .HasForeignKey(d => d.FkCollecte)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jourouver__Fk_Co__13F2C142");
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
                    .HasConstraintName("FK__Lancerale__Fk_Al__1A9FBED1");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Lanceralerte)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lancerale__Fk_Ut__1B93E30A");
            });

            modelBuilder.Entity<Partagerimage>(entity =>
            {
                entity.Property(e => e.FkImage).HasColumnName("Fk_Image");

                entity.Property(e => e.FkUtilisateur)
                    .IsRequired()
                    .HasColumnName("Fk_Utilisateur")
                    .HasMaxLength(50);

                entity.HasOne(d => d.FkImageNavigation)
                    .WithMany(p => p.Partagerimage)
                    .HasForeignKey(d => d.FkImage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Partageri__Fk_Im__078CEA5D");

                entity.HasOne(d => d.FkUtilisateurNavigation)
                    .WithMany(p => p.Partagerimage)
                    .HasForeignKey(d => d.FkUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Partageri__Fk_Ut__08810E96");
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

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NumGsm).HasColumnName("NumGSM");

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Rue)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Rv)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.FkGroupesanguinNavigation)
                    .WithMany(p => p.Utilisateur)
                    .HasForeignKey(d => d.FkGroupesanguin)
                    .HasConstraintName("FK__Utilisate__Fk_Gr__04B07DB2");

                entity.HasOne(d => d.FkRoleNavigation)
                    .WithMany(p => p.Utilisateur)
                    .HasForeignKey(d => d.FkRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Utilisate__Fk_Ro__03BC5979");
            });
        }
    }
}
