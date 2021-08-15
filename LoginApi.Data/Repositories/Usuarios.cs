using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginApi.Data.Repositories
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
