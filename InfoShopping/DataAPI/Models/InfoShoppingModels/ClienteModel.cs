using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAPI.Models.InfoShoppingModels
{
    public class ClienteModel
    {
        public int ClienteId { get; set; }
        public String Nome { get; set; }
        public long Cpf { get; set; }

        public int EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; }
    }
}