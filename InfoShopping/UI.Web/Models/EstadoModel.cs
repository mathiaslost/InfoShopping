using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Web.Models
{
    public class EstadoModel
    {
        [Required]
        [Key]
        public int EstadoId { get; set; }
        public string OwnerId { get; set; }
        public String Nome { get; set; }
    }
}