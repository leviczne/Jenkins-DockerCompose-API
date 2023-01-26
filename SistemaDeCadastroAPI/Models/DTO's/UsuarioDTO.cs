using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastroAPI.Models.DTO_s
{
    public class UsuarioDTO
    {
        
  
        public string Email { get; set; }
        public string ViajanteName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
