using System;
using System.Collections.Generic;
using System.Text;
using db.Interfaces;
using MongoDB.Driver;

namespace db.Util
{
    public static class MongoDBUtil 
    {
        public static void GenerateTableIfNotExist(IMongoDatabase database, string collectionName)
        {
            var collectionsInDatabase = database.ListCollectionNames().ToList();

            if (collectionsInDatabase.Contains(collectionName) == false)
            {
                database.CreateCollection(collectionName);
            }
        }
    }
}
