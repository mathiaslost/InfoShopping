using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;

namespace DataAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Nome { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
        public int IdEndereco { get; set; }
    }
}