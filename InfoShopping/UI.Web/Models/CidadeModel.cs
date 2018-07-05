using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Web.Models
{
    public class CidadeModel
    {
        [Required]
        [Key]
        public int CidadeId { get; set; }
        [Required]
        [StringLength(100)]
        public String Nome { get; set; }
        public string OwnerId { get; set; }

        public int EstadoId { get; set; }
        public EstadoModel Estado { get; set; }
    }
}