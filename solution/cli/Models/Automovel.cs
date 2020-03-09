using System;
using System.Collections.Generic;
using System.Text;

namespace pecacompativel.cli.Models
{
    public class Automovel
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public List<string> Ano { get; set; }

        public Automovel()
        {
            Ano = new List<string>();
        }
    }
}
