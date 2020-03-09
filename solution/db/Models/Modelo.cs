using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using db.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using db.Util;
using db.Interfaces;

namespace db.Models
{
    public class Modelo : IMongoDBCollection
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Nome { get; set; }
        public List<string> Ano { get; set; }

        public string MarcaNome { get; set; }
        public string MarcaId { get; set; }

        [BsonIgnore]
        public int QuantidadeDePecas { get; set; }

        [BsonIgnore]
        public string FriendlyUrl
        {
            get
            {
                if(string.IsNullOrWhiteSpace(MarcaNome))
                {

                }
                return $@"mo-{Id}/{StringUtil.GetFriendlyName(MarcaNome)}/{StringUtil.GetFriendlyName(Nome)}";
            }
        }
    }
}
