using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Libmongocrypt;
using static futabus.models.Customer_Ticket;
using futabus.models;
using static futabus.models.TicketCancellation;
using static futabus.models.Ghe;
using futabus.hoadon;
using futabus.lienhe;
using futabus.tintuc;
using futabus.Models;


namespace futabus.HuyVe
{
    public partial class HuyVe : Form
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private int _MaVe; // Mã vé mặc định, bạn có thể thay đổi hoặc nhập từ TextBox

        public HuyVe(int MaVe)
        {
            _client = new MongoClient("mongodb://localhost:27017"); // Thay đổi kết nối nếu cần thiết
            _database = _client.GetDatabase("MDM");
            _MaVe = MaVe;


            InitializeComponent();
            //thanh header
            // Khởi tạo và cấu hình LinkLabel
            linkLabel_trangchu.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel_trangchu2_LinkClicked);
            linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel3_LinkClicked);
            linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel4_LinkClicked);
            linkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel5_LinkClicked);
            linkLabel6.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel6_LinkClicked);
            linkLabel7.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel7_LinkClicked);
        }

        private void HuyVe_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle; // Chỉ cho phép cố định kích thước
            MaximizeBox = false; // Ẩn nút phóng to
            MinimizeBox = false; // Ẩn nút thu nhỏ
            LoadDataFromMongoDB(_MaVe);
        }
        private void LoadDataFromMongoDB(int maVe)
        {


            var collection = _database.GetCollection<CustomerTicket>("ThongTinVe");
            
            // Tạo bộ lọc tìm kiếm theo MaVe
            var filter = Builders<CustomerTicket>.Filter.Eq("MaVe", maVe);
            var customerTicket = collection.Find(filter).FirstOrDefault();

            if (customerTicket != null)
            {
                label10.Text = "Giá vé";
                // Hiển thị dữ liệu lên các Label
                label_maVe.Text = customerTicket.MaVe.ToString();
                maChuyenDi.Text = customerTicket.MaChuyenDi.ToString();

                diaDiemKhoiHanh.Text = customerTicket.DiaDiemKhoiHanh;
                diaDiemDen.Text = customerTicket.DiaDiemDen;
                ngayKhoiHanh.Text = customerTicket.ThoiGian.NgayKhoiHanhDateTime.ToString("dd/MM/yyyy");
                gioKhoiHanh.Text = customerTicket.ThoiGian.GioKhoiHanhTimeSpan.ToString(@"hh\:mm");

                soGhe.Text = string.Join(", ", customerTicket.SoGhe);
                giaTien.Text = customerTicket.GiaVe.ToString();

                //giaVe.Text = "Giá Vé: " + customerTicket.GiaVe;
            }
            else
            {
                MessageBox.Show("Không tìm thấy vé với Mã Vé: " + maVe);
            }
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
            futabus.Form1 form1 = new Form1();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            form1.FormClosed += (s, args) => this.Show();

            form1.Show();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void tenKH_Click(object sender, EventArgs e)
        {

        }

        private void maChuyenDi_Click(object sender, EventArgs e)
        {

        }

        private void diaDiemKhoiHanh_Click(object sender, EventArgs e)
        {

        }

        private void diaDiemDen_Click(object sender, EventArgs e)
        {

        }

        private void ngayKhoiHanh_Click(object sender, EventArgs e)
        {

        }

        private void gioKhoiHanh_Click(object sender, EventArgs e)
        {

        }



        private void label_maVe_Click(object sender, EventArgs e)
        {

        }

        private void soGhe_Click(object sender, EventArgs e)
        {

        }

        private void giaTien_Click(object sender, EventArgs e)
        {

        }
        private int GetNextCancellationID()
        {
            var collection = _database.GetCollection<TicketCancellation>("Ticket_Cancellation");
            var maxId = collection.AsQueryable().Any() ? collection.AsQueryable().Max(x => x.ID) : 0;
            return maxId + 1;
        }

        private void huyVeBtn_Click(object sender, EventArgs e)
        {
            var collection = _database.GetCollection<TicketCancellation>("Ticket_Cancellation");

            var customerTicketCollection = _database.GetCollection<CustomerTicket>("ThongTinVe");
            var _filter = Builders<CustomerTicket>.Filter.Eq("MaVe", _MaVe);
            var customerTicket = customerTicketCollection.Find(_filter).FirstOrDefault();
            var gheCollection = _database.GetCollection<Ghe>("Ghe");


            if (customerTicket != null)
            {
                DateTime ngayHuy = DateTime.Now;
                DateTime ngayKhoiHanh = customerTicket.ThoiGian.NgayKhoiHanhDateTime;

                // Kiểm tra điều kiện NgayHuy phải trước ngayKhoiHanh 1 ngày
                if (ngayHuy < ngayKhoiHanh.AddDays(-1))
                {
                    var newCancellation = new TicketCancellation
                    {
                        ID = GetNextCancellationID(),
                        MaVe = customerTicket.MaVe,
                        MaChuyenDi = customerTicket.MaChuyenDi,
                        MaKH = customerTicket.MaKH,
                        SoGhe = customerTicket.SoGhe,
                        NgayHuy = ngayHuy.ToString("dd/MM/yyyy"),
                        HoanTien = (int)(customerTicket.GiaVe * 0.6),
                        TrangThai = "Đã hoàn tiền"
                    };

                    collection.InsertOne(newCancellation);
                    var updateFilter = Builders<CustomerTicket>.Filter.Eq("_id", customerTicket._id);
                    var updateDefinition = Builders<CustomerTicket>.Update.Set("TrangThai", "Đã Huỷ");
                    customerTicketCollection.UpdateOne(updateFilter, updateDefinition);

                    // Cập nhật trạng thái ghế
                    var gheFilter = Builders<Ghe>.Filter.Eq("MaChuyenDi", customerTicket.MaChuyenDi);
                    var arrayFilters = new List<ArrayFilterDefinition>
                    {
                        new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem.SoGhe", new BsonDocument("$in", new BsonArray(customerTicket.SoGhe))))
                    };

                    var gheUpdateDefinition = Builders<Ghe>.Update.Set("DSGhe.$[elem].TrangThai", "Còn Trống");
                    var options = new UpdateOptions { ArrayFilters = arrayFilters };

                    gheCollection.UpdateMany(gheFilter, gheUpdateDefinition, options);

                    // Hiển thị thông báo "Đang xử lý"
                    var processingForm = new Form
                    {
                        Size = new Size(200, 100),
                        StartPosition = FormStartPosition.CenterScreen,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        BackColor = Color.DarkGray

                    };

                    var label = new Label
                    {
                        Text = "Đang xử lý...",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter

                    };

                    processingForm.Controls.Add(label);
                    processingForm.Show();

                    // Đóng form "Đang xử lý" sau 3 giây
                    Task.Delay(3000).ContinueWith(_ =>
                    {
                        processingForm.Invoke(new Action(() => processingForm.Close()));
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("Đã hủy vé thành công ");
                            // Đóng form hiện tại và mở form khác
                            //this.Close();
                            //var loginForm = new LoginForm(); // Thay LoginForm bằng form thực tế mà bạn muốn mở
                            //loginForm.Show();
                        }));
                    });
                }
                else
                {
                    MessageBox.Show("Không thể hủy vé vì thời gian hủy phải trước ngày khởi hành ít nhất 1 ngày.");
                }

            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi");
            }




            }


            private void HuyVe_Load_1(object sender, EventArgs e)
        {

        }

   
        private void HuyVe_Load_2(object sender, EventArgs e)
        {

        }

     
        private void HuyVe_Load_3(object sender, EventArgs e)
        {

        }
    }
}
