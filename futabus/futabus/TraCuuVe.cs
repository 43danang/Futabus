using futabus.hoadon;
using futabus.lienhe;
using futabus.Models;
using futabus.Services;
using futabus.tintuc;
using futabus.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace futabus
{
    public partial class TraCuuVe : Form
    {
        //private IMongoCollection<Ve> collection;
        //private MongoClient client;
        //private IMongoDatabase database;
        private MongoDBService _mongoService;
        public static string connectionString = "mongodb://localhost:27017";

        public TraCuuVe()
        {
            InitializeComponent();
            //var client = new MongoClient("mongodb://localhost:27017"); // Kết nối tới MongoDB server
            //var database = client.GetDatabase("MDM"); // Thay thế 'database_name' bằng tên cơ sở dữ liệu của bạn
            //collection = database.GetCollection<Ve>("ThongTinVe"); // Thay thế 'Ve' bằng tên collection của bạn
            try
            {
                _mongoService = new MongoDBService(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kết nối MongoDB thất bại: {ex.Message}");
            }
            btn_dangnhap.Visible = false;
            btn_dangky.Text = Session.username;
            panel1.Visible = false;
            pictureBox1.ImageLocation = @"D:\hk8\doanfutabus\Futabus\banner.png";
            linkLabel_trangchu.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel_trangchu2_LinkClicked);
            
            linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel4_LinkClicked);
            linkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel5_LinkClicked);
            linkLabel6.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel6_LinkClicked);
            linkLabel7.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel7_LinkClicked);
        }


        private void btnhuy_Click(object sender, EventArgs e)
        {
            int mave = int.Parse(txtmave.Text);
            HuyVe.HuyVe _form = new HuyVe.HuyVe(mave);
            _form = new HuyVe.HuyVe(mave);
            this.Hide();


            _form.Show();
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

        private async void btntracuuve_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(txtmave.Text, out int maVe))
            {
                try
                {
                    Ve ve = await _mongoService.GetVe1(maVe);

                    if (ve != null)
                    {
                        // Hiển thị thông tin vé lên các Label
                        //txttmave.Text = ve.MaVe.ToString();                    
                        txtsoghe.Text = string.Join(", ", ve.SoGhe);
                        txtngaykh.Text = ve.ThoiGian.NgayKhoiHanh.ToString();
                        txtgiokh.Text = ve.ThoiGian.GioKhoiHanh;
                        txtnddk.Text = ve.ThoiGian.NgayDenDuKien;
                        txtgddk.Text = ve.ThoiGian.GioDenDuKien;
                        txtddkh.Text = ve.DiaDiemKhoiHanh;
                        txtddden.Text = ve.DiaDiemDen;
                        txtgv.Text = ve.GiaVe.ToString();
                        txttt.Text = ve.TrangThai;

                        panel1.Visible = true;


                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy vé với mã này.");
                        panel1.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ khi truy xuất dữ liệu từ MongoDB
                    MessageBox.Show("Đã xảy ra lỗi khi truy xuất dữ liệu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Mã vé không hợp lệ.");
            }
            btnhuy.Visible = false;
            Button btnhuy1 = new Button();
            btnhuy1.Text = "Hủy vé";
            btnhuy1.Location = new System.Drawing.Point(300, 160);
            btnhuy1.Size = new System.Drawing.Size(127, 39);
            btnhuy1.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
            btnhuy1.ForeColor = Color.Red;
            panel1.Controls.Add(btnhuy1);
            btnhuy1.Click += (s, ev) => {
                int mave = int.Parse(txtmave.Text);
                HuyVe.HuyVe _form = new HuyVe.HuyVe(mave);
                _form = new HuyVe.HuyVe(mave);
                this.Hide();
                _form.Show();
            };
        }

    
        private void TraCuuVe_Load(object sender, EventArgs e)
        {

        }

  
        private void TraCuuVe_Load_1(object sender, EventArgs e)
        {

        }



        //private void TraCuuVe_Load(object sender, EventArgs e)
        //{

        //}



        //private void TraCuuVe_Load_1(object sender, EventArgs e)
        //{

        //}
    }


}
