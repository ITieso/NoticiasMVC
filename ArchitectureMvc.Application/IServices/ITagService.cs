using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Application.IServices
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllAsync();
        Task<TagDto> GetByIdAsync(int id);
        Task<List<TagDto>> GetByNameRangeAsync(List<string> nomes);
        Task<TagDto> GetByNameAsync(string nome);
        Task<int> AddAsync(TagDto tag);
        Task<List<Tag>> AddRangeAsync(List<TagDto> tag);
        Task UpdateAsync(TagDto tag);
        Task DeleteAsync(int id);
    }
}
