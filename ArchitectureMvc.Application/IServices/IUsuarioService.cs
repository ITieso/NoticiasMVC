using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Application.IServices
{
    public  interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(UsuarioDto usuario);
        Task UpdateAsync(UsuarioDto usuario);
        Task DeleteAsync(int id);
    }
}
