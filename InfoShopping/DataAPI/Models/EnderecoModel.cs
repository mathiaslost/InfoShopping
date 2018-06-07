using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAPI.Models
{
    public class EnderecoModel
    {
        public int EnderecoId { get; set; }
        public String Rua { get; set; }
        public int Numero { get; set; }
        public String Bairro { get; set; }
        public String Complemento { get; set; }
        public long Cep { get; set; }
    }
}