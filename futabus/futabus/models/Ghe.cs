using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Libmongocrypt;


namespace futabus.models
{
    public class Ghe
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }



        [BsonElement("MaChuyenDi")]
        public int MaChuyenDi { get; set; }

        [BsonElement("DSGhe")]
        public List<GheItem> DSGhe { get; set; }
    }

    public class GheItem
    {
        [BsonElement("SoGhe")]
        public string SoGhe { get; set; }

        [BsonElement("TrangThai")]
        public string TrangThai { get; set; }
    }
}