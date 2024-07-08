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

namespace futabus.HuyVe
{
    public partial class HuyVe : Form
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private int MaVe = 6532; // Mã vé mặc định, bạn có thể thay đổi hoặc nhập từ TextBox

        public HuyVe()
        {
            _client = new MongoClient("mongodb://localhost:27017"); // Thay đổi kết nối nếu cần thiết
            _database = _client.GetDatabase("BookingCar");
            InitializeComponent();
        }

        private void HuyVe_Load(object sender, EventArgs e)
        {
            LoadDataFromMongoDB(MaVe);
        }
        private void LoadDataFromMongoDB(int maVe)
        {
          
            
            var collection = _database.GetCollection<CustomerTicket>("Customer_Ticket");

            // Tạo bộ lọc tìm kiếm theo MaVe
            var filter = Builders<CustomerTicket>.Filter.Eq("MaVe", MaVe);
            var customerTicket = collection.Find(filter).FirstOrDefault();

            if (customerTicket != null)
            {
                // Hiển thị dữ liệu lên các Label
                tenKH.Text = "Thịnh";
                label_maVe.Text = customerTicket.MaVe.ToString();
               maChuyenDi.Text =  customerTicket.MaChuyenDi.ToString();
                
                diaDiemKhoiHanh.Text = customerTicket.DiaDiemKhoiHanh;
               diaDiemDen.Text =  customerTicket.DiaDiemDen;
                ngayKhoiHanh.Text = customerTicket.ThoiGian.NgayKhoiHanhDateTime.ToString("dd/MM/yyyy");
         gioKhoiHanh.Text = customerTicket.ThoiGian.GioKhoiHanhTimeSpan.ToString(@"hh\:mm");

                soGhe.Text = string.Join(", ", customerTicket.SoGhe);
                giaTien.Text=customerTicket.GiaVe.ToString();

                //giaVe.Text = "Giá Vé: " + customerTicket.GiaVe;
            }
            else
            {
                MessageBox.Show("Không tìm thấy vé với Mã Vé: " + maVe);
            }
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

            var customerTicketCollection = _database.GetCollection<CustomerTicket>("Customer_Ticket");
            var _filter = Builders<CustomerTicket>.Filter.Eq("MaVe", MaVe);
            var customerTicket = customerTicketCollection.Find(_filter).FirstOrDefault();
            var gheCollection = _database.GetCollection<Ghe>("Ghe");


            if (customerTicket != null)
            {
                var newCancellation = new TicketCancellation
                {
                    ID = GetNextCancellationID(),
                    MaVe = customerTicket.MaVe,
                    MaChuyenDi = customerTicket.MaChuyenDi,
                    MaKH = customerTicket.MaKH,
                    SoGhe = customerTicket.SoGhe,
                    NgayHuy = DateTime.Now.ToString("dd/MM/yyyy"),
                    HoanTien = (int)(customerTicket.GiaVe * 0.6),
                    TrangThai = "Đã hoàn tiền"
                };

                collection.InsertOne(newCancellation);
                var updateFilter = Builders<CustomerTicket>.Filter.Eq("_id", customerTicket._id); //
                var updateDefinition = Builders<CustomerTicket>.Update.Set("TrangThai", "Đã Huỷ");
                customerTicketCollection.UpdateOne(updateFilter, updateDefinition);


                //Ghe
                var gheFilter = Builders<Ghe>.Filter.Eq("MaChuyenDi", customerTicket.MaChuyenDi);

                var arrayFilters = new List<ArrayFilterDefinition<Ghe>>
        {
            new BsonDocumentArrayFilterDefinition<Ghe>(BsonDocument.Parse("{ 'elem.SoGhe': { $in: [" + string.Join(",", customerTicket.SoGhe.Select(g => "'" + g + "'")) + "] } }"))
        };

                var gheUpdateDefinition = Builders<Ghe>.Update.Set("DSGhe.$[elem].TrangThai", "Còn Trống");
                var options = new UpdateOptions { ArrayFilters = arrayFilters };

                gheCollection.UpdateMany(gheFilter, gheUpdateDefinition, options);

                MessageBox.Show("Đã hủy vé thành công và cập nhật trạng thái ghế!");

                MessageBox.Show("Đã hủy vé thành công và cập nhật trạng thái ghế!");




            }
            else
            {
                MessageBox.Show("Không tìm thấy vé với Mã Vé: " + MaVe);
            }




        }
    }
}
