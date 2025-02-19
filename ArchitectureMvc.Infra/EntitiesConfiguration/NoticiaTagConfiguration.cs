using ArchitectureMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Infra.EntitiesConfiguration
{
    public class NoticiaTagConfiguration : IEntityTypeConfiguration<NoticiaTag>
    {
        public void Configure(EntityTypeBuilder<NoticiaTag> builder)
        {
            // Configuração da chave primária composta
            builder.HasKey(nt => new { nt.NoticiaId, nt.TagId });

            builder.HasOne(nt => nt.Noticia)
                .WithMany(n => n.NoticiaTags)
                .HasForeignKey(nt => nt.NoticiaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(nt => nt.Tag)
                .WithMany(t => t.NoticiaTags)
                .HasForeignKey(nt => nt.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
