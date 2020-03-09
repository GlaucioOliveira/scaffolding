using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pecacompativel.db.Models;
using MongoDB.Driver;

namespace pecacompativel.db.Services
{
    public class ModeloService
    {
        private readonly IMongoCollection<Modelo> _modelos;

        public ModeloService(IPecaCompativelDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _modelos = database.GetCollection<Modelo>(settings.ModeloCollectionName);
        }

        public List<Modelo> Get() =>
            _modelos.Find(x => true).ToList();


        public Modelo Get(string id) =>
            _modelos.Find<Modelo>(x => x.Id == id).FirstOrDefault();

        public Modelo Create(Modelo Marca)
        {
            _modelos.InsertOne(Marca);
            return Marca;
        }

        public void Update(string id, Modelo ModeloIn) =>
            _modelos.ReplaceOne(x => x.Id == id, ModeloIn);

        public void Remove(Modelo ModeloIn) =>
            _modelos.DeleteOne(x => x.Id == ModeloIn.Id);

        public void Remove(string id) =>
            _modelos.DeleteOne(x => x.Id == id);

        public void RemoveAll()
        {
            //remove everything
            _modelos.DeleteMany(x => true);
        }
    }
}
