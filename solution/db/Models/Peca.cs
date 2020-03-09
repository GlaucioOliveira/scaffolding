using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace db.Models
{
    public class Peca : IMongoDBCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        [BsonElement("Peca")]
        public string PecaNome { get; set; }
        public string Observacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string ModeloOrigem { get; set; }
        public bool NecessitaAdaptacao { get; set; }

        [BsonIgnore]
        public int QuantidadePecas { get; set; }
        public int QuantidadeAprovacao { get; set; }
        public int QuantidadeReprovacao { get; set; }

    }
}
