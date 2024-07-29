using DevExpress.XtraPrinting;
using futabus.hoadon;
using futabus.lienhe;
using futabus.tintuc;
using futabus.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace futabus.ThanhToan
{
    public partial class ThanhToanThanhCong : Form
    {
        string _dsghe;
        int _idCHUYENDI;
        string _phuongThucThanhToan;
        string chuyendi;
        public ThanhToanThanhCong(string dsghe = "A05,A06", int idCHUYENDI = 3, string phuongThucThanhToan = "MoMo")
        {
            InitializeComponent();
            _dsghe = dsghe.Replace(" ", "");
            _idCHUYENDI = idCHUYENDI;
            _phuongThucThanhToan = phuongThucThanhToan;

            linkLabel_trangchu.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel_trangchu2_LinkClicked);
            linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel3_LinkClicked);
            linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel4_LinkClicked);
            linkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel5_LinkClicked);
            linkLabel6.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel6_LinkClicked);
            linkLabel7.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel7_LinkClicked);

        }
        private void linkLabel_trangchu2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            form1.FormClosed += (s, args) => this.Show();

            form1.Show();
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TraCuuVe tracuuve = new TraCuuVe();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            tracuuve.FormClosed += (s, args) => this.Show();

            tracuuve.Show();
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TinTuc tintuc = new TinTuc();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            tintuc.FormClosed += (s, args) => this.Show();

            tintuc.Show();
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HoaDon hoadon = new HoaDon();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            hoadon.FormClosed += (s, args) => this.Show();

            hoadon.Show();
        }
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LienHe lienhe = new LienHe();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            lienhe.FormClosed += (s, args) => this.Show();

            lienhe.Show();
        }
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            vechungtoi.vechungtoi vechungtoi = new vechungtoi.vechungtoi();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            vechungtoi.FormClosed += (s, args) => this.Show();

            vechungtoi.Show();
        }

        private async void ThanhToanThanhCong_Load(object sender, EventArgs e)
        {
            btn_dangnhap.Visible = false;
            btn_dangky.Text = Session.username;
            chuyendi = await CapNhatChuyenDi(_idCHUYENDI, _dsghe);
            CapNhatTinhTrangGhe(_idCHUYENDI, _dsghe);
            string thongTinVe = await TaoVe(_dsghe, _phuongThucThanhToan, chuyendi);
            GenderateQRcode(thongTinVe);
        }

        private async Task<string> CapNhatChuyenDi(int idCHUYENDI, string dsghe)
        {
            string[] listGhe = dsghe.Split(',');
            int soLuongGhe = listGhe.Length;

            var conn = new Neo4jConnection();
            using (var session = conn.CreateSession())
            {
                // Giảm số lượng ghế trống trong chuyến đi bằng số lượng ghế khách hàng đã chọn mua
                var query = @"
                MATCH ()-[r:CHUYENDI]-()
                WHERE id(r) = $idCHUYENDI
                SET r.soghetrong = r.soghetrong - 1
                RETURN r";

                var parameters = new { idCHUYENDI, soLuongGhe };
                var result = await session.RunAsync(query, parameters);
                var record = await result.ToListAsync();
                var CHUYENDI = record[0].Get<IRelationship>("r");

                if (CHUYENDI == null)
                {
                    return "Giảm số lượng thất bại";
                }
                else
                {
                    // Chuyển đổi kết quả sang JSON
                    var jsonResult = new
                    {
                        CHUYENDI = CHUYENDI
                    };
                    return Newtonsoft.Json.JsonConvert.SerializeObject(jsonResult);
                }
            }
            conn.Dispose();
        }

        private async void CapNhatTinhTrangGhe(int idCHUYENDI, string dsghe)
        {
            // Tách chuỗi ds ghế ra thành 1 mảng dạng dsghe = "A02,A03"
            string[] listGhe = dsghe.Split(','); //listGhe = ["A02","A03"]

            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("MDM");
                var collection = database.GetCollection<Ghe>("Ghe");

                var gheFilter = Builders<Ghe>.Filter.Eq("MaChuyenDi", idCHUYENDI);
                var arrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem.SoGhe", new BsonDocument("$in", new BsonArray(listGhe))))
                    };

                var gheUpdateDefinition = Builders<Ghe>.Update.Set("DSGhe.$[elem].TrangThai", "Đã Bán");
                var options = new UpdateOptions { ArrayFilters = arrayFilters };

                collection.UpdateMany(gheFilter, gheUpdateDefinition, options);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật ghế: " + ex.Message);
            }

        }

        private async Task<string> TaoVe(string dsghe, string phuongThucThanhToan, string chuyendi)
        {
            /* Tạo mã vé*/
            Random random = new Random();
            int MaVe = random.Next(10000, 100000);
            /*Tạo mã giao dịch*/
            int MaGD = random.Next(100000000, 1000000000);
            /*Tạm thời gán cứng mã KH*/
            int MaKH = Session.userID;

            string[] listGhe = dsghe.Split(',');
            int soLuongGhe = listGhe.Length;
            var gheArray = new BsonArray(listGhe);
            // Phân tích JSON thành object
            dynamic chuyendiObject = Newtonsoft.Json.JsonConvert.DeserializeObject(chuyendi);

            // Truy cập các thuộc tính của đối tượng
            string MaChuyenDi = chuyendiObject.CHUYENDI.Id;
            string DiaDiemKhoiHanh = chuyendiObject.CHUYENDI.Properties.dauben;
            string DiaDiemDen = chuyendiObject.CHUYENDI.Properties.cuoiben;
            string GiaDonVe = chuyendiObject.CHUYENDI.Properties.giave;
            string NgayKhoiHanh = chuyendiObject.CHUYENDI.Properties.ngaydi;
            string GioKhoiHanh = chuyendiObject.CHUYENDI.Properties.giodi;
            string NgayDenDuKien = chuyendiObject.CHUYENDI.Properties.ngayden;
            string GioDenDuKien = chuyendiObject.CHUYENDI.Properties.gioden;

            string SoTienThanhToan = (int.Parse(GiaDonVe) * soLuongGhe).ToString();

            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("MDM");
                var collection = database.GetCollection<BsonDocument>("ThongTinVe");

                var ve = new BsonDocument
                {
                    { "MaVe",MaVe },
                    { "MaChuyenDi",int.Parse(MaChuyenDi) },
                    { "MaKH",MaKH },
                    { "SoGhe",gheArray },
                    { "GiaVe",int.Parse(GiaDonVe) },
                    { "DiaDiemKhoiHanh",DiaDiemKhoiHanh },
                    { "DiaDiemDen",DiaDiemDen },
                    { "ThoiGian",new BsonDocument
                        {
                            { "NgayKhoiHanh", NgayKhoiHanh },
                            { "GioKhoiHanh", GioKhoiHanh },
                            { "NgayDenDuKien", NgayDenDuKien },
                            { "GioDenDuKien", GioDenDuKien}
                        }
                    },
                    {"TrangThai","Đã thanh toán" },
                    { "ThanhToan",new BsonDocument
                        {
                            { "MaGiaoDich", MaGD },
                            { "SoTien", SoTienThanhToan },
                            { "PhuongThucThanhToan", phuongThucThanhToan }
                        }
                    }
                };

                //Thêm vé mới vào database
                await collection.InsertOneAsync(ve);

                // Kiểm tra xem vé đã được thêm thành công hay chưa
                var filter = Builders<BsonDocument>.Filter.Eq("MaVe", MaVe);
                var result = await collection.Find(filter).FirstOrDefaultAsync();

                if (result != null)
                {
                    // Trả về vé dưới dạng chuỗi JSON
                    return ve.ToJson();
                }
                else
                {
                    // Nếu vé không được tìm thấy trong cơ sở dữ liệu, thông báo lỗi
                    MessageBox.Show("Lỗi: Vé không được tạo thành công.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return null;
            }
        }

        private void GenderateQRcode(String ThongTinVe)
        {
            QRCoder.QRCodeGenerator QG = new QRCoder.QRCodeGenerator();
            var VeQR = QG.CreateQrCode(ThongTinVe, QRCoder.QRCodeGenerator.ECCLevel.H);
            var qrcode = new QRCoder.QRCode(VeQR);
            QRcodeBox.Image = qrcode.GetGraphic(50);
        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // ThanhToanThanhCong
        //    // 
        //    this.ClientSize = new System.Drawing.Size(282, 253);
        //    this.Name = "ThanhToanThanhCong";
        //    this.Load += new System.EventHandler(this.ThanhToanThanhCong_Load_1);
        //    this.ResumeLayout(false);

        //}

        private void ThanhToanThanhCong_Load_1(object sender, EventArgs e)
        {

        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // ThanhToanThanhCong
        //    // 
        //    this.ClientSize = new System.Drawing.Size(282, 253);
        //    this.Name = "ThanhToanThanhCong";
        //    this.Load += new System.EventHandler(this.ThanhToanThanhCong_Load_2);
        //    this.ResumeLayout(false);

        //}

        private void ThanhToanThanhCong_Load_2(object sender, EventArgs e)
        {

        }

        

        private void ThanhToanThanhCong_Load_3(object sender, EventArgs e)
        {

        }

       
        private void ThanhToanThanhCong_Load_4(object sender, EventArgs e)
        {

        }

      
        private void ThanhToanThanhCong_Load_5(object sender, EventArgs e)
        {

        }

      

        private void ThanhToanThanhCong_Load_6(object sender, EventArgs e)
        {

        }

  

        private void ThanhToanThanhCong_Load_7(object sender, EventArgs e)
        {

        }
    }
    public class Neo4jConnection
    {
        private readonly IDriver _driver;

        public Neo4jConnection()
        {
            string uri = "bolt://localhost:7687/FUTABUS"; // Thay bằng địa chỉ của Neo4j
            string username = "neo4j"; // Thay bằng tên người dùng của bạn
            string password = "12345678"; // Thay bằng mật khẩu của bạn
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));
        }

        public IAsyncSession CreateSession()
        {
            return _driver.AsyncSession();
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }
    }

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
