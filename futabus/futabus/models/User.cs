using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futabus.models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("userID")]
        public int userID { get; set; }

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("password")]
        public string password { get; set; }

        [BsonElement("email")]
        public string email { get; set; }
    }

    public class LoginUser
    {

        [BsonElement]
        public string username { get; set; }
        [BsonElement]
        public string password { get; set; }

    }
}
