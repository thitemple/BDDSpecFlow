using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeAnota.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public virtual IList<Bloco> Blocos { get; set; }
    }
}