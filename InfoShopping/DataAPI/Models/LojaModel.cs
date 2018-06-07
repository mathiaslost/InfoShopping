using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAPI.Models
{
    public class LojaModel
    {
        public int LojaId { get; set; }
        public String Nome { get; set; }
        public long cnpj { get; set; }
        public String NomeFantasia { get; set; }
    }
}