using ArchitectureMvc.Domain.Entities;
using ArchitectureMvc.Domain.Interfaces;
using ArchitectureMvc.Infra.Context;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Infra.Repositories
{
    public class NoticiaRepository : INoticiaRepository
    {
        private readonly ApplicationDbContext _context;
        public NoticiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Noticia>> GetAllAsync()
        {
            var result = await _context.Noticias.Include(n => n.Usuario).Include(n => n.NoticiaTags).ThenInclude(nt => nt.Tag).ToListAsync();
            return result;
        }

        public async Task<Noticia> GetByIdAsync(int id)
        {
            var result =   await _context.Noticias.Include(n => n.Usuario).Include(n => n.NoticiaTags).ThenInclude(nt => nt.Tag).FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task AddAsync(Noticia noticia)
        {
            try
            {
            _context.Noticias.Add(noticia);
            await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UpdateAsync(Noticia noticia)
        {
            _context.Noticias.Update(noticia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Noticia noticia)
        {
                _context.Noticias.Remove(noticia);
                await _context.SaveChangesAsync();
        }
    }
}
