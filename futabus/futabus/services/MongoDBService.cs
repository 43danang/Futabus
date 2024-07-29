using futabus.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MongoDB.Driver.Core.Configuration;
using DevExpress.XtraPrinting;

namespace futabus.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Ve> collection;

        public MongoDBService(string connectionString)
        {
 
            // Kết nối tới MongoDB
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("MDM");
            collection = database.GetCollection<Ve>("ThongTinVe");
        }


        public async Task<Ve> GetVe1(int maVe)
        {
            var filter = Builders<Ve>.Filter.Eq("MaVe", maVe);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

    }
}

