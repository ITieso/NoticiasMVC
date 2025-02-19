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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(UsuarioDto usuario)
        {
            var usuarioEntity = _mapper.Map<Usuario>(usuario);
            await _usuarioRepository.AddAsync(usuarioEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var usuarioEntity = _usuarioRepository.GetByIdAsync(id).Result;
            await _usuarioRepository.DeleteAsync(usuarioEntity);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuariEntity = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuariEntity);
        
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            var usuarioEntity = await _usuarioRepository.GetByIdAsync(id);
            return usuarioEntity;
        }

        public async Task UpdateAsync(UsuarioDto usuario)
        {
            var usuarioEntity = _mapper.Map<Usuario>(usuario);
            await _usuarioRepository.UpdateAsync(usuarioEntity);
        }
    }
}
