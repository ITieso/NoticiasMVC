using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Application.IServices;
using ArchitectureMvc.Domain.Entities;
using ArchitectureMvc.Domain.Interfaces;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Application.Services
{
    public class NoticiaService : INoticiaService
    {
        private readonly INoticiaRepository _noticiaRepository;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        public NoticiaService(INoticiaRepository noticiaRepository, IMapper mapper, ITagService tagService, IUsuarioService usuarioService)
        {
            _noticiaRepository = noticiaRepository;
            _mapper = mapper;
            _tagService = tagService;
            _usuarioService = usuarioService;
        }
        public async Task AddAsync(NoticiaDto noticia)
        {
            var usuario = await _usuarioService.GetByIdAsync(noticia.UsuarioId);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }
            var noticiaEntity = _mapper.Map<Noticia>(noticia);
            noticiaEntity.Usuario = null;
            noticiaEntity.UsuarioId = usuario.Id;
            noticiaEntity.NoticiaTags = new List<NoticiaTag>();

            var noticiaTags = new List<NoticiaTag>();

            var tagsExistentes = await _tagService.GetByNameRangeAsync(noticia.Tags);
            var tagsDict = tagsExistentes.ToDictionary(t => t.Nome, t => t);

            // Lista para armazenar novas tags a serem criadas
            var novasTags = new List<TagDto>();

            foreach (var tagNome in noticia.Tags)
            {
                if (!tagsDict.ContainsKey(tagNome))
                {
                    // Criar um objeto TagDto para ser salvo posteriormente
                    novasTags.Add(new TagDto { Nome = tagNome });
                }
            }

            // Adicionar todas as novas tags de uma vez ao banco
            if (novasTags.Any())
            {
                await _tagService.AddRangeAsync(novasTags);

                // Buscar novamente todas as tags para garantir que temos os IDs corretos
                tagsExistentes = await _tagService.GetByNameRangeAsync(noticia.Tags);
                tagsDict = tagsExistentes.ToDictionary(t => t.Nome, t => t);
            }

            // Criar as relações NoticiaTag com os IDs corretos
            noticiaTags = noticia.Tags.Select(tagNome => new NoticiaTag
            {
                TagId = tagsDict[tagNome].Id
            }).ToList();

            // Associar as tags à notícia
            noticiaEntity.NoticiaTags = noticiaTags;

            // Salvar a notícia no banco
            await _noticiaRepository.AddAsync(noticiaEntity);
          
        }

        public async Task DeleteAsync(int id)
        {
            var noticiaEntity = _noticiaRepository.GetByIdAsync(id).Result;
            await _noticiaRepository.DeleteAsync(noticiaEntity);
        }

        public async Task<IEnumerable<NoticiaDto>> GetAllAsync()
        {
            var noticiaEntity = await _noticiaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NoticiaDto>>(noticiaEntity);
        }

        public async Task<NoticiaDto> GetByIdAsync(int id)
        {
            var noticiaEntity = await _noticiaRepository.GetByIdAsync(id);
            return _mapper.Map<NoticiaDto>(noticiaEntity);
        }

        public async Task UpdateAsync(NoticiaDto noticia)
        {
            var noticiaEntity = await _noticiaRepository.GetByIdAsync(noticia.Id);
            noticiaEntity.Titulo = noticia.Titulo;
            noticiaEntity.Texto = noticia.Texto;
            noticiaEntity.UsuarioId = noticia.UsuarioId;

            noticiaEntity.NoticiaTags.Clear();

            // Buscar tags existentes no banco
            if (noticia.Tags != null)
            {
                var tagsExistentes = await _tagService.GetByNameRangeAsync(noticia.Tags);
                var tagsDict = tagsExistentes.ToDictionary(t => t.Nome, t => t);

                // Identifica tags novas que precisam ser criadas
                var novasTags = noticia.Tags
                    .Where(tagNome => !tagsDict.ContainsKey(tagNome))
                    .ToList();

                // Adiciona as novas tags ao banco de dados, se houver
                if (novasTags.Any())
                {
                    var tagsParaAdicionar = novasTags.Select(tagNome => new TagDto { Nome = tagNome }).ToList();
                    await _tagService.AddRangeAsync(tagsParaAdicionar);

                    // Recarrega as tags para garantir que os IDs corretos sejam atribuídos
                    tagsExistentes = await _tagService.GetByNameRangeAsync(noticia.Tags);
                    tagsDict = tagsExistentes.ToDictionary(t => t.Nome, t => t);
                }

                // Cria as associações entre Noticia e Tag
                var noticiaTags = noticia.Tags
                    .Select(tagNome => new NoticiaTag
                    {
                        NoticiaId = noticiaEntity.Id,
                        TagId = tagsDict[tagNome].Id
                    })
                    .ToList();

                // Associa as tags à notícia
                noticiaEntity.NoticiaTags = noticiaTags;
            }
            // Atualiza a notícia no banco de dados
            await _noticiaRepository.UpdateAsync(noticiaEntity); 
        }
    }
}
