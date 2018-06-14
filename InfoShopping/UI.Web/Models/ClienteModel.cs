using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace UI.Web.Models
{
    public class ClienteModel
    {
        [Required]
        [Key]
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