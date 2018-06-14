using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAPI.Models.InfoShoppingModels
{
    public class ClienteModel
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        [StringLength(100)]
        public String Nome { get; set; }
        [Required]
        public long Cpf { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }
    }
}