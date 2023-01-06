using System;
using System.Collections.Generic;

namespace SistemaDeCadastroAPI.Models
{
    public partial class UsuarioModel
    {
        public UsuarioModel()
        {
            Viagems = new HashSet<ViagemModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;

        public virtual ICollection<ViagemModel> Viagems { get; set; }
    }
}
