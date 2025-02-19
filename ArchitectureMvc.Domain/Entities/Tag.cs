using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<NoticiaTag> NoticiaTags { get; set; }
    }
}
