using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Application.IServices
{
    public interface INoticiaService
    {
        Task<IEnumerable<NoticiaDto>> GetAllAsync();
        Task<NoticiaDto> GetByIdAsync(int id);
        Task AddAsync(NoticiaDto noticia);
        Task UpdateAsync(NoticiaDto noticia);
        Task DeleteAsync(int id);
    }
}
