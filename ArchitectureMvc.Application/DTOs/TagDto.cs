using ArchitectureMvc.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Application.DTOs
{
    public class TagDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<NoticiaTag> NoticiaTags { get; set; }
    }
}
