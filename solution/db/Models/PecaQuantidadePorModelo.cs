using System;
using System.Collections.Generic;
using System.Text;
using db.Interfaces;

namespace db.Models
{
    public class PecaQuantidadePorModelo : IMongoDBCollection
    {
        public string Id { get; set; }
        public string ModeloOrigem { get; set; }
        public int ContagemPecas { get; set; }
    }
}
