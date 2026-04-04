#pragma warning disable IDE0005
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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
                .HasComment("Fecha y hora de ultima modificacion del usuario.")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
