using ArchitectureMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Domain.Interfaces
{
    public interface INoticiaRepository
    {
        Task<IEnumerable<Noticia>> GetAllAsync();
        Task<Noticia> GetByIdAsync(int id);
        Task AddAsync(Noticia noticia);
        Task UpdateAsync(Noticia noticia);
        Task DeleteAsync(Noticia noticia);
    }
}
