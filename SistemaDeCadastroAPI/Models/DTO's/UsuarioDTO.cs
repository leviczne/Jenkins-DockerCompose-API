using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastroAPI.Models.DTO_s
{
    public class UsuarioDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<ViagemModel> Viagems { get; set; }
    }
}
