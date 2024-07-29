using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson.Serialization.Attributes;
using Neo4j.Driver;
using Neo4j.Driver.Mapping;
using System.Numerics;
using futabus.Utils;
using futabus.hoadon;
using futabus.lienhe;
using futabus.tintuc;


namespace futabus.ThanhToan
{
    public partial class ChonPhuongThucThanhToan : Form
    {
        private IMongoDatabase database;
        string _dsghe;
        int _idCHUYENDI;

        public ChonPhuongThucThanhToan(string dsghe = "A03,A02", int idCHUYENDI = 11)
        {
            InitializeComponent();
            _dsghe = dsghe;
            _idCHUYENDI = idCHUYENDI;
        }
        private void ChonPhuongThucThanhToan_Load(object sender, EventArgs e)
        {   //userid lấy từ session
            int userid = Session.userID;
            btn_dangnhap.Visible = false;
            btn_dangky.Text = Session.username;
            //thanh header
            // Khởi tạo và cấu hình LinkLabel
            linkLabel_trangchu.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel_trangchu2_LinkClicked);
            linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel3_LinkClicked);
            linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel4_LinkClicked);
            linkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel5_LinkClicked);
            linkLabel6.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel6_LinkClicked);
            linkLabel7.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel7_LinkClicked);
            LoadPhuongThucThanhToan();
            LoadThongTinHanhKhach(userid);
            LoadThongTinChuyenDi(_idCHUYENDI, _dsghe);
            LoadThongTinChiTietVeVaThongTinThanhToan(_idCHUYENDI, _dsghe);
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
        private void linkLabel_trangchu2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            form1.FormClosed += (s, args) => this.Show();

            form1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void LoadPhuongThucThanhToan()
        {
            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("MDM");
                var collection = database.GetCollection<BsonDocument>("PhuongThucThanhToan");

                // Lấy ra document MoMo
                var filter = Builders<BsonDocument>.Filter.Eq("TenPhuongThuc", "MoMo");
                var pttt = await collection.Find(filter).FirstOrDefaultAsync();

                // Hiển thị thông tin mã giảm giá của phương thức thanh toán
                if (pttt != null)
                {
                    MoMoDescription.Text = pttt["MoTaMaGiamGia"].AsString;
                }
                // Lấy ra document VNPay
                filter = Builders<BsonDocument>.Filter.Eq("TenPhuongThuc", "VNPay");
                pttt = await collection.Find(filter).FirstOrDefaultAsync();

                // Hiển thị thông tin mã giảm giá của phương thức thanh toán
                if (pttt != null)
                {
                    VNPayDescription.Text = pttt["MoTaMaGiamGia"].AsString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void LoadThongTinHanhKhach(int userID)
        {
            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("MDM");
                var collection = database.GetCollection<BsonDocument>("Customer");

                // Lấy ra document user của khách hàng
                var filter = Builders<BsonDocument>.Filter.Eq("userID", userID);
                var kh = await collection.Find(filter).FirstOrDefaultAsync();

                // Hiển thị thông tin khách hàng lên form
                if (kh != null)
                {
                    HoVaTen.Text = kh["hoTen"].AsString;
                    sdt.Text = kh["sdt"].AsString;
                    email.Text = kh["email"].AsString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        //lấy ra button của phương thức thanh toán nào được check
        private RadioButton GetSelectedRadioButton(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is RadioButton && ((RadioButton)control).Checked)
                {
                    return (RadioButton)control;
                }
            }
            return null;
        }

        //Chuyển qua hiển thị form tiếp theo
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            string phuongThucThanhToan = GetSelectedRadioButton(panel1).Text;
            //Tạo form thanh toán thành công
            ThanhToanThanhCong newForm = new ThanhToanThanhCong(_dsghe, _idCHUYENDI, phuongThucThanhToan);
            this.Hide(); // Ẩn form hiện tại
            newForm.Show(); // Hiển thị form mới
        }

        //Lấy thông tin chuyến đi 
        private async Task<string> LayThongTinChuyenDi(int idCHUYENDI)
        {
            var conn = new Neo4jConnection();
            using (var session = conn.CreateSession())
            {
                // Lấy thông tin về chuyến đi dựa trên idCHUYENDI
                var query = @"
                MATCH ()-[r:CHUYENDI]-()
                WHERE id(r) = $idCHUYENDI
                RETURN r";

                var parameters = new { idCHUYENDI };
                var result = await session.RunAsync(query, parameters);
                var record = await result.ToListAsync();
                var CHUYENDI = record[0].Get<IRelationship>("r");

                if (CHUYENDI == null)
                {
                    return "Không tìm thấy chuyến đi";
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

        //Load thông tin chuyến đi vào form
        private async void LoadThongTinChuyenDi(int idCHUYENDI, string dsghe)
        {
            string CHUYENDI = await LayThongTinChuyenDi(idCHUYENDI);

            //Lấy số lượng ghế đã chọn
            string[] listGhe = dsghe.Split(',');
            int soLuongGhe = listGhe.Length;

            // Phân tích JSON thành object
            dynamic chuyendi = Newtonsoft.Json.JsonConvert.DeserializeObject(CHUYENDI);

            // Truy cập các thuộc tính của chuyendi
            string DiaDiemKhoiHanh = chuyendi.CHUYENDI.Properties.dauben;
            string DiaDiemDen = chuyendi.CHUYENDI.Properties.cuoiben;
            string GiaDonVe = chuyendi.CHUYENDI.Properties.giave;
            string NgayKhoiHanh = chuyendi.CHUYENDI.Properties.ngaydi;
            string GioKhoiHanh = chuyendi.CHUYENDI.Properties.giodi;

            string TuyenXe = DiaDiemKhoiHanh + " - " + DiaDiemDen;
            string ThoiGianXuatBen = GioKhoiHanh + " " + NgayKhoiHanh;
            Decimal tongTienThanhToan = int.Parse(GiaDonVe) * soLuongGhe;
            string TongTienLuotDi = tongTienThanhToan.ToString();

            //Gán giá trị của các thuộc tính trong chuyến đi vào form
            label12.Text = TuyenXe;
            label22.Text = ThoiGianXuatBen;
            label23.Text = soLuongGhe.ToString() + " ghế";
            label24.Text = DiaDiemKhoiHanh;
            label25.Text = dsghe;
            label27.Text = TongTienLuotDi;
        }

        //Load thông tin chi tiết vé và thông tin thanh toán vào form
        private async void LoadThongTinChiTietVeVaThongTinThanhToan(int idCHUYENDI, string dsghe)
        {
            string CHUYENDI = await LayThongTinChuyenDi(idCHUYENDI);

            // Phân tích JSON thành object
            dynamic chuyendi = Newtonsoft.Json.JsonConvert.DeserializeObject(CHUYENDI);

            //Gán giá trị của các thuộc tính trong chuyến đi vào form
            label30.Text = chuyendi.CHUYENDI.Properties.giave;

            //Lấy số lượng ghế đã chọn
            string[] listGhe = dsghe.Split(',');
            int soLuongGhe = listGhe.Length;
            string GiaDonVe = chuyendi.CHUYENDI.Properties.giave;
            Decimal tongTienThanhToan = int.Parse(GiaDonVe) * soLuongGhe;
            label28.Text = tongTienThanhToan.ToString();
            label3.Text = tongTienThanhToan.ToString();

        }

        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // ChonPhuongThucThanhToan
        //    // 
        //    this.ClientSize = new System.Drawing.Size(282, 253);
        //    this.Name = "ChonPhuongThucThanhToan";
        //    this.Load += new System.EventHandler(this.ChonPhuongThucThanhToan_Load_1);
        //    this.ResumeLayout(false);

        //}

        private void ChonPhuongThucThanhToan_Load_1(object sender, EventArgs e)
        {

        }

      

        private void ChonPhuongThucThanhToan_Load_2(object sender, EventArgs e)
        {

        }

       
        private void ChonPhuongThucThanhToan_Load_3(object sender, EventArgs e)
        {

        }

     
        private void ChonPhuongThucThanhToan_Load_4(object sender, EventArgs e)
        {

        }

    
        private void ChonPhuongThucThanhToan_Load_5(object sender, EventArgs e)
        {

        }
    }
}

