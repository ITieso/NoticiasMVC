using ArchitectureMvc.Application.IServices;
using ArchitectureMvc.Application.Mappings;
using ArchitectureMvc.Application.Services;
using ArchitectureMvc.Domain.Interfaces;
using ArchitectureMvc.Infra.Context;
using ArchitectureMvc.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitectureMvc.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),

            //indico para o projeto onde que as migrations serão criadas
            //que sera onde esta meu arquivo de contexto que é o dbcontext
            x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<INoticiaRepository, NoticiaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<INoticiaService, NoticiaService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddAutoMapper(typeof(DtoMappingProfile));
            return services;
        }
    }
}
