using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class ToDoContext : DbContext
{
    public ToDoContext()
    {
    }

    public ToDoContext(DbContextOptions<ToDoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("assignments_pkey");

            entity.ToTable("assignments");

            entity.HasIndex(e => e.AssignedById, "idx_assignments_assigned_by");

            entity.HasIndex(e => e.AssignedToId, "idx_assignments_assigned_to");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AssignedById).HasColumnName("assigned_by_id");
            entity.Property(e => e.AssignedToId).HasColumnName("assigned_to_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status)
                .HasDefaultValue((short)1)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AssignedBy).WithMany(p => p.AssignmentAssignedBies)
                .HasForeignKey(d => d.AssignedById)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user_assigned_by");

            entity.HasOne(d => d.AssignedTo).WithMany(p => p.AssignmentAssignedTos)
                .HasForeignKey(d => d.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_user_assigned_to");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("Id del usuario.")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Fecha y hora de creacion del usuario.")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasComment("Email del usuario.")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("Nombre del usuario.")
                .HasColumnName("name");
            entity.Property(e => e.Role)
                .HasComment("Rol del usuario.")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasComment("Fecha y hora de la ultima modificacion del usuario.")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
