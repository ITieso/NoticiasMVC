using ArchitectureMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Domain.Interfaces
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<List<Tag>> GetByNameRangeAsync(List<string> name);
        Task<Tag> GetByNameAsync(string name);
        Task<Tag> GetByIdAsync(int id);
        Task<int> AddAsync(Tag tag);
        Task<List<Tag>> AddRangeAsync(List<Tag> tag);
        Task UpdateAsync(Tag tag);
        Task DeleteAsync(Tag id);
    }
}
