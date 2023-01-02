using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SistemaDeCadastroAPI.Models;

namespace SistemaDeCadastroAPI.Data
{
    public partial class SistemaCadastroDBContex : DbContext
    {
        public SistemaCadastroDBContex()
        {
        }

        public SistemaCadastroDBContex(DbContextOptions<SistemaCadastroDBContex> options)
            : base(options)
        {
        }

        public virtual DbSet<UsuarioModel> Usuarios { get; set; } = null!;
        public virtual DbSet<ViagemModel> Viagems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-45OORD5;Database=DB_SistemaCadastro;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Senha).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<ViagemModel>(entity =>
            {
                entity.ToTable("Viagem");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.NomeViagem).HasColumnType("ntext");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
