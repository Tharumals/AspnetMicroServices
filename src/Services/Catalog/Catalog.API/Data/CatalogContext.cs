using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration confinguration)
        {
            var client = new MongoClient(confinguration.GetValue<string>("DatabaseSettings:ConnectingString"));
            var database = client.GetDatabase(confinguration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(confinguration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
