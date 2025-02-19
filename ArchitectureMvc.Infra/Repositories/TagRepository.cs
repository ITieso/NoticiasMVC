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
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var result = await _context.Tags.ToListAsync();
            return result;
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            var result = await _context.Tags.FindAsync(id);
            return result;
        }

        public async Task<List<Tag>> GetByNameRangeAsync(List<string> nomes)
        {
            foreach(var nome in nomes)
            {
                nome.ToLower();
            }
            var result = await _context.Tags.Where(t => nomes.Contains(t.Nome)).ToListAsync();
            return result;
        }


        public async Task<Tag> GetByNameAsync(string nome)
        {
            nome.ToLower();
            var result = await _context.Tags.Include(t=> t.NoticiaTags).FirstOrDefaultAsync(n => n.Nome == nome);
            return result;
        }
        public async Task<int> AddAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag.Id;
        }

        public async Task<List<Tag>> AddRangeAsync(List<Tag> tags)
        {
            await _context.Tags.AddRangeAsync(tags);
            await _context.SaveChangesAsync();
            return tags;
        }


        public async Task UpdateAsync(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tag tag)
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}
