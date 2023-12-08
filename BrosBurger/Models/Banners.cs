using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrosBurger.Models
{
    [Table("Banners")]
    public class Banners
    {
        [Key]
        public int BannerId { get; set; }
        public string NomeBanner { get; set; }
        
        [Required(ErrorMessage = "Imagem é obrigatória")]
        [Display(Name = "Insira sua imagem")]
        public string ImagemBanner { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}