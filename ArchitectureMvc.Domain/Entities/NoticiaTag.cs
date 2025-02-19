﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchitectureMvc.Domain.Entities
{
    public class NoticiaTag
    {
        public int Id { get; set; }
        public int NoticiaId { get; set; }
        public Noticia Noticia { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
