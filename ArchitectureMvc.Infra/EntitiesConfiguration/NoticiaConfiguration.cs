using ArchitectureMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Infra.EntitiesConfiguration
{
    class NoticiaConfiguration : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.HasKey(n => n.Id);

            // Configuração das propriedades
            builder.Property(n => n.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.Texto)
                .IsRequired();


            builder.HasOne(n => n.Usuario)
                .WithMany(u => u.Noticias)
                .HasForeignKey(n => n.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(n => n.NoticiaTags)
                .WithOne(nt => nt.Noticia)
                .HasForeignKey(nt => nt.NoticiaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
