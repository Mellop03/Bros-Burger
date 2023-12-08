using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrosBurger.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [Display(Name = "Nome completo")]
        public string NomeUsuario { get; set; }

        [Display(Name = "Nome de login")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [Display(Name = "Insira sua senha")]
        public string SenhaUsuario { get; set; } 
    }
}