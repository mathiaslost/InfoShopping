using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Web.Models
{
    public class ShoppingModel
    {
        [Required]
        [Key]
        public int ShoppingId { get; set; }
        public string OwnerId { get; set; }
        public String Nome { get; set; }
        public long CNPJ { get; set; }
        public String Email { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

    }
}