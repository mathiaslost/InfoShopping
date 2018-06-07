using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DataAPI.Models.InfoShoppingModels
{
    public class CidadeModel
    {
        [Required]
        [StringLength(10)]
        public int CidadeId { get; set; }
        [Required]
        [StringLength(100)]
        public String Nome { get; set; }

        public int EstadoId { get; set; }
        public EstadoModel Estado { get; set; }
    }
}