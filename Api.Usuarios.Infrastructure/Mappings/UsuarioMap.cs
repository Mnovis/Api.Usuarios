using Api.Usuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Usuarios.Infrastructure.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Nome da tabela
            builder.ToTable("USUARIOS");

            //Chave primária
            builder.HasKey(u => u.Id);

            //Mapeamentos dos campos
            builder.Property(u => u.Id)
                .HasColumnName("ID");

            builder.Property(u => u.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.DataCadastro)
                .HasColumnName("DATACADASTRO")
                .IsRequired();

            builder.Property(u => u.PerfilId)
                .HasColumnName("PERFIL_ID")
                .IsRequired();

            //Definindo o campo EMAIL como único
            builder.HasIndex(u => u.Email)
                .IsUnique();

            //Mapeamento do relacionamento + chave estrangeira
            builder.HasOne(u => u.Perfil) //Usuário TEM 1 Perfil
                .WithMany(p => p.Usuarios) //Perfil TEM MUITOS Usuários
                .HasForeignKey(u => u.PerfilId); //Chave estrangeira
        }
    }
}
