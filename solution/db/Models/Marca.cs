using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using pecacompativel.db.Util;

namespace pecacompativel.db.Models
{
    public class Marca
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public List<Modelo> Modelo { get; set; }

        public Marca()
        {
            Modelo = new List<Modelo>();
        }
        [BsonIgnore]
        public string FriendlyUrl
        {
            get
            {
                return $@"ma-{Id}/{StringUtil.GetFriendlyName(Nome)}";
            }
        }
    }
}
