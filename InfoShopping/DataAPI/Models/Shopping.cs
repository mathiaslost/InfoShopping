using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAPI.Models
{
    public class Shopping
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Nome { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Cnpj { get; set; }
        public Endereco Endereco { get; set; }
        public int IdEndereco { get; set; }
    }
}