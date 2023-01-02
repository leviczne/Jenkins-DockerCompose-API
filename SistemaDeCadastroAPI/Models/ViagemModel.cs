using System;
using System.Collections.Generic;

namespace SistemaDeCadastroAPI.Models
{
    public partial class ViagemModel
    {
        public int Id { get; set; }
        public string NomeViagem { get; set; } = null!;
        public int Ano { get; set; }
    }
}
