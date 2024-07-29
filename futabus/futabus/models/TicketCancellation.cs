using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;

namespace futabus.models
{
    public class TicketCancellation


    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int ID { get; set; }

        public int MaVe { get; set; }
        public int MaChuyenDi { get; set; }
        public int MaKH { get; set; }
        public List<string> SoGhe { get; set; }
        public string NgayHuy { get; set; }
        public int HoanTien { get; set; }
        public string TrangThai { get; set; }

    }
}
