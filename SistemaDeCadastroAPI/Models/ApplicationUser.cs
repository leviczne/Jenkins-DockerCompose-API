using Microsoft.AspNetCore.Identity;

namespace SistemaDeCadastroAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ViajanteName { get; set; }

        public virtual ICollection<UsuariosViagemModel> UsuariosViagems { get; set; }

    }
}
