namespace DataAPI.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Nome { get; set; }
        public Estado Estado { get; set; }
        public int EstadoId { get; set; }
    }
}