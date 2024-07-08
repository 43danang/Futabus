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


namespace futabus.HuyVe
{
    public partial class HuyVe : Form
    {
        public int MaVe = 6532;
        public HuyVe()
        {
            InitializeComponent();
        }

        private void HuyVe_Load(object sender, EventArgs e)
        {
            LoadDataFromMongoDB(MaVe);
        }
        private void LoadDataFromMongoDB(int maVe)
        {
            // Kết nối tới MongoDB
            var connectionString = "mongodb://localhost:27017"; // Thay đổi kết nối nếu cần thiết
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("BookingCar");
            var collection = database.GetCollection<CustomerTicket>("Customer_Ticket");

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
    }
}
