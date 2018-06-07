using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAPI.Models
{
    public class ShoppingModel
    {
        public int ShoppingId { get; set; }
        public String Nome { get; set; }
        public long CNPJ { get; set; }
        public String Email { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }

    }
}