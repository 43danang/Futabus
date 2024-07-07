using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using futabus.models;
using System.Configuration;

namespace futabus.Login
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(string connectionString)
        {

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("BookingCar");
            _usersCollection = database.GetCollection<User>("User");
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            var filter = Builders<User>.Filter.Eq("username", username) & Builders<User>.Filter.Eq("password", password);
            return _usersCollection.Find(filter).FirstOrDefault();
        }
    }
}
