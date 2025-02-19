using ArchitectureMvc.Domain.Entities;
using ArchitectureMvc.Domain.Interfaces;
using ArchitectureMvc.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            var result = await _context.Usuarios.AsNoTracking().Include(n => n.Noticias).ToListAsync();
            return result;
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var result = await _context.Usuarios.AsNoTracking().Include(x=>x.Noticias).FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
