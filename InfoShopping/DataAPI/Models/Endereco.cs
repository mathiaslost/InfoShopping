namespace DataAPI.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        // propriedades do endereço
        [System.ComponentModel.DataAnnotations.StringLength(100)]
        public string Rua { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(6)]
        public int Numero { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Bairro { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Complemento { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.StringLength(12)]
        public string Cep { get; set; }

        public Cidade Cidade { get; set; }
        public int CidadeId { get; set; }
    }
}