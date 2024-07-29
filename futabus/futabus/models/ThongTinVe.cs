using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futabus.Models
{
    public class Ve
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonRepresentation(BsonType.Int32)]
        public int MaVe { get; set; }

        [BsonElement("MaChuyenDi")]
        public int MaChuyenDi { get; set; }

        [BsonElement("MaKH")]
        public int MaKH { get; set; }

        [BsonElement("MaTX")]
        public int MaTX { get; set; }

        [BsonElement("MaXe")]
        public int MaXe { get; set; }

        [BsonElement("SoGhe")]
        //[BsonRepresentation(BsonType.String)]
        public List<string> SoGhe { get; set; }

        [BsonElement("ThoiGian")]
        public ThoiGian ThoiGian { get; set; }

        [BsonElement("DiaDiemKhoiHanh")]
        public string DiaDiemKhoiHanh { get; set; }

        [BsonElement("DiaDiemDen")]
        public string DiaDiemDen { get; set; }

        [BsonElement("GiaVe")]
        //[BsonRepresentation(BsonType.Decimal128)]
        public decimal GiaVe { get; set; }
        [BsonElement("TrangThai")]
        public string TrangThai { get; set; }

        [BsonElement("ThanhToan")]
        public ThanhToan ThanhToan { get; set; }

    }

    public class ThoiGian
    {
        [BsonElement("NgayKhoiHanh")]

        public string NgayKhoiHanh { get; set; }
        [BsonElement("GioKhoiHanh")]
        public string GioKhoiHanh { get; set; }

        [BsonElement("NgayDenDuKien")]
        public string NgayDenDuKien { get; set; }

        [BsonElement("GioDenDuKien")]
        public string GioDenDuKien { get; set; }
    }

    public class ThanhToan
    {
        [BsonElement("MaGiaoDich")]
        public int MaGiaoDich { get; set; }

        [BsonElement("SoTien")]
        public string SoTien { get; set; }

        [BsonElement("PhuongThucThanhToan")]
        public string PhuongThucThanhToan { get; set; }
    }

}