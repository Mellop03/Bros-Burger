using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrosBurger.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }
        public string NomeCategoria { get; set; }

        public List<Produto> Produtos { get; set; }
    }
}