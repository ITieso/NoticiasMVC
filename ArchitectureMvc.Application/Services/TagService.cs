using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Application.IServices;
using ArchitectureMvc.Domain.Entities;
using ArchitectureMvc.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Application.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task<int> AddAsync(TagDto tag)
        {
            var tagEntity = _mapper.Map<Tag>(tag);
            var result = await _tagRepository.AddAsync(tagEntity);
            return result;
        }

        public async Task<List<Tag>> AddRangeAsync(List<TagDto> tag)
        {
            var tagEntity = _mapper.Map<List<Tag>>(tag);
            var result = await _tagRepository.AddRangeAsync(tagEntity);
            return result;
        }


        public async Task DeleteAsync(int id)
        {
             var tagEntity = _tagRepository.GetByIdAsync(id).Result;
            await _tagRepository.DeleteAsync(tagEntity);
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            var tagEntity = await _tagRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TagDto>>(tagEntity);
        }

        public async Task<TagDto> GetByIdAsync(int id)
        {
            var tagEntity = await _tagRepository.GetByIdAsync(id);
            return _mapper.Map<TagDto>(tagEntity);
        }

        public async Task<TagDto> GetByNameAsync(string nome)
        {
            var tagEntity = await _tagRepository.GetByNameAsync(nome);
            return _mapper.Map<TagDto>(tagEntity);
        }
        public async Task<List<TagDto>> GetByNameRangeAsync(List<string> nomes)
        {
            var tagEntity = await _tagRepository.GetByNameRangeAsync(nomes);
            return _mapper.Map<List<TagDto>>(tagEntity);
        }
        public async Task UpdateAsync(TagDto tag)
        {
            var tagEntity = _mapper.Map<Tag>(tag);
            await _tagRepository.UpdateAsync(tagEntity);
        }
    }
}
