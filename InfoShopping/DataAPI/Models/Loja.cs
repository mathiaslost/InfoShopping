using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAPI.Models
{
    public class Loja
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Nome { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Cnpj { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Nome_fantasia { get; set; }
        public Endereco Endereco { get; set; }
        public int IdEndereco { get; set; }
    }
}