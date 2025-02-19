using ArchitectureMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Application.DTOs
{
    public class NoticiaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O Título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O Texto é obrigatório.")]
        public string Texto { get; set; }

        [Required(ErrorMessage = "O Usuário é obrigatório.")]
        public int UsuarioId { get; set; }
        public string? UsuarioNome { get; set; }
        public List<string> Tags { get; set; }
    }
}
