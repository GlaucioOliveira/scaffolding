using MongoDB.Driver;
using System.Collections.Generic;
using db.Interfaces;
using System;
using db.Util;

namespace db.Services
{
    public class BaseService<T> where T : IMongoDBCollection
    {
        internal IMongoCollection<T> _collection;

        public BaseService(IDatabaseSettings settings) 
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            var collectionName = getCollectionName(settings);

            MongoDBUtil.GenerateTableIfNotExist(database, collectionName);

            _collection = database.GetCollection<T>(collectionName);
        }

        public virtual List<T> Get() => _collection.Find(x => true).ToList();

        public virtual T Get(string id) => _collection.Find<T>(x => x.Id == id).FirstOrDefault();

        public virtual T Create(T newObject)
        {
            _collection.InsertOne(newObject);
            return newObject;
        }

        public virtual void Update(string id, T ObjectToUpdate) =>
            _collection.ReplaceOne(x => x.Id == id, ObjectToUpdate);

        public virtual void Remove(T ObjectToRemove) =>
            _collection.DeleteOne(x => x.Id == ObjectToRemove.Id);

        public virtual void Remove(string id) =>
            _collection.DeleteOne(x => x.Id == id);

        public virtual void RemoveAll()
        {
            _collection.DeleteMany(x => x.Id == null || x.Id != null);
        }
        private string getCollectionName(IDatabaseSettings settings)
        {
            string serviceName = "";
            string collectionName = "";

            try
            {
                serviceName = this.GetType().Name.Replace("Service", string.Empty).Trim();
                collectionName = settings.GetType().GetProperty(serviceName).GetValue(settings).ToString();

                return collectionName;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar obter o nome da collection. ServiceName = {serviceName}\nDetalhes: {ex.Message}");
            }
        }
    }
}
