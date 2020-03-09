using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pecacompativel.db.Models;
using MongoDB.Driver;

namespace pecacompativel.db.Services
{
    public class MarcaService
    {
        private readonly IMongoCollection<Marca> _marcas;

        public MarcaService(IPecaCompativelDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _marcas = database.GetCollection<Marca>(settings.MarcaCollectionName);
        }

        public List<Marca> Get() =>
            _marcas.Find(Marca => true).ToList();


        public Marca Get(string id) =>
            _marcas.Find<Marca>(Marca => Marca.Id == id).FirstOrDefault();

        public Marca Create(Marca Marca)
        {
            _marcas.InsertOne(Marca);
            return Marca;
        }

        public void Update(string id, Marca MarcaIn) =>
            _marcas.ReplaceOne(Marca => Marca.Id == id, MarcaIn);

        public void Remove(Marca MarcaIn) =>
            _marcas.DeleteOne(Marca => Marca.Id == MarcaIn.Id);

        public void Remove(string id) =>
            _marcas.DeleteOne(Marca => Marca.Id == id);

        public void RemoveAll()
        {
            //remove everything
            _marcas.DeleteMany(x => x.Id == null || x.Id != null);
        }
    }
}
