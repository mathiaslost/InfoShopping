
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Web.Models
{
    public class EnderecoModel
    {
        [Required]
        [Key]
        public int EnderecoId { get; set; }
        public string OwnerId { get; set; }
        public String Rua { get; set; }
        public int Numero { get; set; }
        public String Bairro { get; set; }
        public String Complemento { get; set; }
        public long Cep { get; set; }

        public int CidadeId { get; set; }
        public CidadeModel Cidade { get; set; }
    }
}