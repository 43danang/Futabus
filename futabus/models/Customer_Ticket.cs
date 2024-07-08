using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Filtering.Templates;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
namespace futabus.models
{
    public class Customer_Ticket
    {
        public class CustomerTicket
        {
            [BsonId]
            public ObjectId _id { get; set; }
            public int MaVe { get; set; }
            public int MaChuyenDi { get; set; }
            public int MaKH { get; set; }
            public int MaTX { get; set; }
            public int MaXe { get; set; }
            public List<string> SoGhe { get; set; }
            public ThoiGian ThoiGian { get; set; }
            public string DiaDiemKhoiHanh { get; set; }
            public string DiaDiemDen { get; set; }
            public int GiaVe { get; set; }
            public string TrangThai { get; set; }  
        }
        public class ThoiGian
        {
            public string NgayKhoiHanh { get; set; }
            public string GioKhoiHanh { get; set; }
            public string NgayDenDuKien { get; set; }
            public string GioDenDuKien { get; set; }

            [BsonIgnore]
            public DateTime NgayKhoiHanhDateTime
            {
                get => DateTime.ParseExact(NgayKhoiHanh, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            [BsonIgnore]
            public TimeSpan GioKhoiHanhTimeSpan
            {
                get => TimeSpan.ParseExact(GioKhoiHanh, @"hh\:mm", CultureInfo.InvariantCulture);
            }

            [BsonIgnore]
            public DateTime NgayDenDuKienDateTime
            {
                get => DateTime.ParseExact(NgayDenDuKien, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            [BsonIgnore]
            public TimeSpan GioDenDuKienTimeSpan
            {
                get => TimeSpan.ParseExact(GioDenDuKien, @"hh\:mm", CultureInfo.InvariantCulture);
            }
        }

    }
}
