using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeCadastroAPI.Models
{
    public partial class ViagemModel
    {

        [Key]
        public int Id { get; set; }
        public string NomeViagem { get; set; } = null!;
        public int Ano { get; set; }
    }
}
