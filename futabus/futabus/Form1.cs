using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Neo4j.Driver;
using futabus.config;
using DevExpress.Utils.Gesture;
using futabus.model;
using futabus;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace futabus
{
    public partial class Form1 : Form
    {
        private IDriver _driver;
        private database _databaseConnection;
        private Form_datve _formdatve;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
    int nLeftRect,
    int nTopRect,
    int nRightRect,
    int nBottomRect,
    int nWidthEllipse,
    int nHeightEllipse);
        public Form1()
        {
            InitializeComponent();
            //bo góc
            btn_timchuyendi.FlatStyle = FlatStyle.Flat;
            btn_timchuyendi.FlatAppearance.BorderSize = 0;
            btn_timchuyendi.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btn_timchuyendi.Width, btn_timchuyendi.Height, 40, 40));
            // this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            panel5.Visible = false;
            flowLayoutPanel1.Visible = false;
            label_sochuyen.Visible = false;
            panel_thongbao.Visible = false;
            LoadComboBoxData();
            // Thiết lập giá trị mặc định
            label_kiemtralocloaixe.Text = "no";
            cbb_sove_motchieu.SelectedIndex = 0; // Chọn giá trị đầu tiên, tức là "1"
            this.Load += new EventHandler(Form1_Load);
            cbb_diemdi.SelectedIndexChanged += cbb_diemdi_SelectedIndexChanged;
            cbb_diemden.SelectedIndexChanged += cbb_diemden_SelectedIndexChanged;
            dateTime_ngaydi.ValueChanged += dateTimePicker1_ValueChanged;
            cbb_sove_motchieu.SelectedIndexChanged += cbb_sove_motchieu_SelectedIndexChanged;
        }
        //private void Form1_Resize(object sender, EventArgs e)
        //{
        //    panel2.Width = (int)(this.Width * 0.95);
        //}
        private async void LoadComboBoxData()
        {
            _databaseConnection = new database();

            string query = "MATCH (n:Diem) RETURN n";
            var results = await _databaseConnection.GetDataFromneo4j(query);

            // Xóa các giá trị hiện có trong ComboBox
            cbb_diemdi.Items.Clear();
            cbb_diemden.Items.Clear();

            // Lặp qua các kết quả và thêm vào ComboBox
            foreach (var record in results)
            {
                var diem = record["n"].As<INode>();
                string tenDiem = diem["tendiem"].As<string>();

                cbb_diemdi.Items.Add(tenDiem);
                cbb_diemden.Items.Add(tenDiem);
            }

            // Thiết lập giá trị mặc định cho ComboBox (nếu có)
            if (cbb_diemdi.Items.Count > 0) cbb_diemdi.SelectedIndex = 0;
            if (cbb_diemden.Items.Count > 0) cbb_diemden.SelectedIndex = 0;
        }
        private  void Form1_Load(object sender, EventArgs e)
        {
            _databaseConnection = new database();

            //await _databaseConnection.GetDataFromneo4j("MATCH (s:Sinhvien) RETURN s");
        }
        
        private async void btnFetchData_Click(object sender, EventArgs e)
        {
            var data = await _databaseConnection.GetDataFromneo4j("MATCH (s:Sinhvien) RETURN s");

            // Hiển thị dữ liệu lên ListView, DataGridView hoặc ListBox
            // Ví dụ, hiển thị lên ListBox
            //listBoxData.Items.Clear();
            foreach (var record in data)
            {
                int SVid = record["SVid"].As<int>();
                string name = record["Name"].As<string>();
                int age = record["Age"].As<int>();
                //listBoxData.Items.Add($"SVid: {SVid}, Name: {name}, Age: {age}");
            }
        }
        private void Form1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTime_ngaydi.Value;
        }

        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string loaixe = "";
            if(label_kiemtralocloaixe.Text== "Limousine")
            {
                loaixe = "Limousine";
            } else if (label_kiemtralocloaixe.Text == "Giường")
            {
                loaixe = "Giường";
            }
            else if (label_kiemtralocloaixe.Text == "Ghế")
            {
                loaixe = "Ghế";
            }
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;
            

            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);
            _databaseConnection = new database();

            string query2 = "";

            if (label_kiemtralocloaixe.Text == "no")
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";
            }
            else
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy',loaixe: '{loaixe}' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";

            }
            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            int kiemTraThoiGian = 0;
            if (checkBox_sangsom.Checked || checkBox_buoisang.Checked || checkBox_buoichieu.Checked || checkBox_buoitoi.Checked)
            {
                kiemTraThoiGian = 1;
            }
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {

                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                string lx = chuyenDi["loaixe"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                //lọc thời gian nếu có
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);

                int locthoigian = 0;
                if (kiemTraThoiGian == 1)
                {
                    if (checkBox_sangsom.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoisang.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoichieu.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoitoi.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(18) && timeGiodi < TimeSpan.FromHours(24))
                        {
                            locthoigian = 1;
                        }
                    }
                }


                if (locthoigian == 1 || kiemTraThoiGian == 0)
                {
                    sochuyen = sochuyen + 1;
                    // Tạo một panel mới (panel7) cho mỗi chuyến đi
                    Panel panel7 = new Panel();
                    panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                    panel7.BorderStyle = BorderStyle.FixedSingle;
                    panel7.BackColor = Color.White;
                    // Tạo và thiết lập các điều khiển con của panel7
                    Label label_giodi = new Label();
                    label_giodi.Text = giodi;
                    label_giodi.Location = new System.Drawing.Point(10, 10);
                    label_giodi.Width = 70;

                    Label label_thoigianhanhtrinh = new Label();
                    label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                    label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                    label_thoigianhanhtrinh.Width = 120;

                    Label label_gioden = new Label();
                    label_gioden.Text = gioden;
                    label_gioden.Location = new System.Drawing.Point(250, 10);
                    label_gioden.Width = 70;

                    Label label_dauben = new Label();
                    label_dauben.Text = dauben;
                    label_dauben.Location = new System.Drawing.Point(10, 40);
                    label_dauben.Width = 150;

                    Label label13 = new Label();
                    label13.Text = "-----------------------------------------------------------------";
                    label13.Location = new System.Drawing.Point(30, 60);
                    label13.Height = 10;
                    label13.Width = 250;

                    Label label_cuoiben = new Label();
                    label_cuoiben.Text = cuoiben;
                    label_cuoiben.Location = new System.Drawing.Point(250, 40);
                    label_cuoiben.Width = 150;

                    Label label_soghetrong = new Label();
                    label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                    label_soghetrong.Location = new System.Drawing.Point(390, 10);

                    Label label_loaixe = new Label();
                    label_loaixe.Text = lx;
                    label_loaixe.ForeColor = Color.Green;
                    label_loaixe.Location = new System.Drawing.Point(330, 10);
                    label_loaixe.Width = 90;

                    Label label_giaban = new Label();
                    label_giaban.Text = $"{giaVe:N0} Đ";
                    label_giaban.ForeColor = Color.Red;
                    label_giaban.Location = new System.Drawing.Point(10, 80);
                    label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                    Button btn_chonchuyen = new Button();
                    btn_chonchuyen.Text = "Chọn chuyến";
                    btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                    btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                    btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                    btn_chonchuyen.ForeColor = Color.Red;
                    //tạo label ẩn để truyền dữ liệu qua trang khác
                    Label hiddenLabel_idchuyendi = new Label();
                    hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                    hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemdi = new Label();
                    hiddenLabel_diemdi.Text = diemdi;
                    hiddenLabel_diemdi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemden = new Label();
                    hiddenLabel_diemden.Text = diemden;
                    hiddenLabel_diemden.Visible = false; // Ẩn Label
                                                         // Thêm các điều khiển con vào panel7
                    panel7.Controls.Add(label_giodi);
                    panel7.Controls.Add(label_thoigianhanhtrinh);
                    panel7.Controls.Add(label_gioden);
                    panel7.Controls.Add(label_dauben);
                    panel7.Controls.Add(label_cuoiben);
                    panel7.Controls.Add(label_soghetrong);
                    panel7.Controls.Add(label_loaixe);
                    panel7.Controls.Add(label13);
                    panel7.Controls.Add(label_giaban);
                    panel7.Controls.Add(btn_chonchuyen);

                    panel7.Controls.Add(hiddenLabel_idchuyendi);
                    panel7.Controls.Add(hiddenLabel_diemdi);
                    panel7.Controls.Add(hiddenLabel_diemden);
                    //thêm sự kiện click cho nút chọn chuyến
                    btn_chonchuyen.Click += (s, ev) => {
                        long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                        string selectedDiemDi = hiddenLabel_diemdi.Text;
                        string selectedDiemDen = hiddenLabel_diemden.Text;

                        // Tạo một form mới và truyền dữ liệu vào form đó
                        _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                        _formdatve.Show();
                    };

                    // Thêm panel7 vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(panel7);
                }
                label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
                
            }
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {
                flowLayoutPanel1.Visible = true;
            }
            else
            {
                panel_thongbao.Visible = true;
            }
        }

        private async void linkLabel_trangchu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkBox_sangsom.Checked = false;
            checkBox_buoisang.Checked = false;
            checkBox_buoichieu.Checked = false;
            checkBox_buoitoi.Checked = false;
            label_kiemtralocloaixe.Text = "no";
            //ẩn bảng hiển thị chuyến đi và thông báo
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;
            //lấy thông tin
            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);

            string query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";

            _databaseConnection = new database();

            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            int somsang = 0;
            int buoisang = 0;
            int buoichieu = 0;
            int buoitoi = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {
                sochuyen = sochuyen + 1;
                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                string loaixe = chuyenDi["loaixe"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();
                
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);
                if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                {
                    somsang = somsang + 1;
                }
                else if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                {
                    buoisang = buoisang + 1;
                }
                else if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                {
                    buoichieu = buoichieu + 1;
                }
                else
                {
                    buoitoi = buoitoi + 1;
                }
                // Tạo một panel mới (panel7) cho mỗi chuyến đi
                Panel panel7 = new Panel();
                panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                panel7.BorderStyle = BorderStyle.FixedSingle;
                panel7.BackColor = Color.White;
                // Tạo và thiết lập các điều khiển con của panel7
                Label label_giodi = new Label();
                label_giodi.Text = giodi;
                label_giodi.Location = new System.Drawing.Point(10, 10);
                label_giodi.Width = 70;

                Label label_thoigianhanhtrinh = new Label();
                label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                label_thoigianhanhtrinh.Width = 120;

                Label label_gioden = new Label();
                label_gioden.Text = gioden;
                label_gioden.Location = new System.Drawing.Point(250, 10);
                label_gioden.Width = 70;

                Label label_dauben = new Label();
                label_dauben.Text = dauben;
                label_dauben.Location = new System.Drawing.Point(10, 40);
                label_dauben.Width = 150;

                Label label13 = new Label();
                label13.Text = "-----------------------------------------------------------------";
                label13.Location = new System.Drawing.Point(30, 60);
                label13.Height = 10;
                label13.Width = 250;

                Label label_cuoiben = new Label();
                label_cuoiben.Text = cuoiben;
                label_cuoiben.Location = new System.Drawing.Point(250, 40);
                label_cuoiben.Width = 150;

                Label label_soghetrong = new Label();
                label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                label_soghetrong.Location = new System.Drawing.Point(390, 10);

                Label label_loaixe = new Label();
                label_loaixe.Text = loaixe;
                label_loaixe.ForeColor = Color.Green;
                label_loaixe.Location = new System.Drawing.Point(330, 10);
                label_loaixe.Width = 90;

                Label label_giaban = new Label();
                label_giaban.Text = $"{giaVe:N0} Đ";
                label_giaban.ForeColor = Color.Red;
                label_giaban.Location = new System.Drawing.Point(10, 80);
                label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                Button btn_chonchuyen = new Button();
                btn_chonchuyen.Text = "Chọn chuyến";
                btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                btn_chonchuyen.ForeColor = Color.Red;
                //tạo label ẩn để truyền dữ liệu qua trang khác
                Label hiddenLabel_idchuyendi = new Label();
                hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                Label hiddenLabel_diemdi = new Label();
                hiddenLabel_diemdi.Text = diemdi;
                hiddenLabel_diemdi.Visible = false; // Ẩn Label

                Label hiddenLabel_diemden = new Label();
                hiddenLabel_diemden.Text = diemden;
                hiddenLabel_diemden.Visible = false; // Ẩn Label
                // Thêm các điều khiển con vào panel7
                panel7.Controls.Add(label_giodi);
                panel7.Controls.Add(label_thoigianhanhtrinh);
                panel7.Controls.Add(label_gioden);
                panel7.Controls.Add(label_dauben);
                panel7.Controls.Add(label_cuoiben);
                panel7.Controls.Add(label_soghetrong);
                panel7.Controls.Add(label_loaixe);
                panel7.Controls.Add(label13);
                panel7.Controls.Add(label_giaban);
                panel7.Controls.Add(btn_chonchuyen);

                panel7.Controls.Add(hiddenLabel_idchuyendi);
                panel7.Controls.Add(hiddenLabel_diemdi);
                panel7.Controls.Add(hiddenLabel_diemden);
                //thêm sự kiện click cho nút chọn chuyến
                btn_chonchuyen.Click += (s, ev) => {
                    long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                    string selectedDiemDi = hiddenLabel_diemdi.Text;
                    string selectedDiemDen = hiddenLabel_diemden.Text;

                    // Tạo một form mới và truyền dữ liệu vào form đó
                    _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                    _formdatve.Show();
                };

                // Thêm panel7 vào flowLayoutPanel1
                flowLayoutPanel1.Controls.Add(panel7);
            }
            label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {
                
                flowLayoutPanel1.Visible = true;

            }
            else
            {
                panel_thongbao.Visible = true;
            }
            panel5.Visible = true;
            label11.Text = $"Sáng sớm 00:00 - 06:00 ({somsang})";
            label_buoisang.Text = $"Buổi sáng 06:00 - 12:00 ({buoisang})";
            label_buoichieu.Text = $"Buổi chiều 12:00 - 18:00 ({buoichieu})";
            label_buoitoi.Text = $"Buổi tối 18:00 - 24:00 ({buoitoi})";
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void btn_timchuyendi_Click(object sender, EventArgs e)
        {
            label_kiemtralocloaixe.Text = "no";
            checkBox_sangsom.Checked = false;
            checkBox_buoisang.Checked = false;
            checkBox_buoichieu.Checked = false;
            checkBox_buoitoi.Checked = false;
            //ẩn bảng hiển thị chuyến đi và thông báo
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;
            //lấy thông tin
            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);

            

           string query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";


            _databaseConnection = new database();

            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            //kiểm tra số chuyến của từng buổi

            int somsang = 0;
            int buoisang = 0;
            int buoichieu = 0;
            int buoitoi = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {
                
                sochuyen = sochuyen + 1;
                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                string loaixe = chuyenDi["loaixe"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);
                if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                {
                    somsang = somsang + 1;
                }
                else if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                {
                    buoisang = buoisang + 1;
                }
                else if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                {
                    buoichieu = buoichieu + 1;
                }
                else
                {
                    buoitoi = buoitoi + 1;
                }
                // Tạo một panel mới (panel7) cho mỗi chuyến đi
                Panel panel7 = new Panel();
                panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                panel7.BorderStyle = BorderStyle.FixedSingle;
                panel7.BackColor = Color.White;
                // Tạo và thiết lập các điều khiển con của panel7
                Label label_giodi = new Label();
                label_giodi.Text = giodi;
                label_giodi.Location = new System.Drawing.Point(10, 10);
                label_giodi.Width = 70;

                Label label_thoigianhanhtrinh = new Label();
                label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                label_thoigianhanhtrinh.Width = 120;

                Label label_gioden = new Label();
                label_gioden.Text = gioden;
                label_gioden.Location = new System.Drawing.Point(250, 10);
                label_gioden.Width = 70;

                Label label_dauben = new Label();
                label_dauben.Text = dauben;
                label_dauben.Location = new System.Drawing.Point(10, 40);
                label_dauben.Width = 150;

                Label label13 = new Label();
                label13.Text = "-----------------------------------------------------------------";
                label13.Location = new System.Drawing.Point(30, 60);
                label13.Height = 10;
                label13.Width = 250;

                Label label_cuoiben = new Label();
                label_cuoiben.Text = cuoiben;
                label_cuoiben.Location = new System.Drawing.Point(250, 40);
                label_cuoiben.Width = 150;

                Label label_soghetrong = new Label();
                label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                label_soghetrong.Location = new System.Drawing.Point(390, 10);

                Label label_loaixe = new Label();
                label_loaixe.Text = loaixe;
                label_loaixe.ForeColor = Color.Green;
                label_loaixe.Location = new System.Drawing.Point(330, 10);
                label_loaixe.Width = 90;

                Label label_giaban = new Label();
                label_giaban.Text = $"{giaVe:N0} Đ";
                label_giaban.ForeColor = Color.Red;
                label_giaban.Location = new System.Drawing.Point(10, 80);
                label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                Button btn_chonchuyen = new Button();
                btn_chonchuyen.Text = "Chọn chuyến";
                btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                btn_chonchuyen.ForeColor = Color.Red;
                //tạo label ẩn để truyền dữ liệu qua trang khác
                Label hiddenLabel_idchuyendi = new Label();
                hiddenLabel_idchuyendi.Text = idchuyendi.ToString() ;
                hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                Label hiddenLabel_diemdi = new Label();
                hiddenLabel_diemdi.Text = diemdi;
                hiddenLabel_diemdi.Visible = false; // Ẩn Label

                Label hiddenLabel_diemden = new Label();
                hiddenLabel_diemden.Text = diemden;
                hiddenLabel_diemden.Visible = false; // Ẩn Label
                // Thêm các điều khiển con vào panel7
                panel7.Controls.Add(label_giodi);
                panel7.Controls.Add(label_thoigianhanhtrinh);
                panel7.Controls.Add(label_gioden);
                panel7.Controls.Add(label_dauben);
                panel7.Controls.Add(label_cuoiben);
                panel7.Controls.Add(label_soghetrong);
                panel7.Controls.Add(label_loaixe);
                panel7.Controls.Add(label13); 
                panel7.Controls.Add(label_giaban);
                panel7.Controls.Add(btn_chonchuyen);

                panel7.Controls.Add(hiddenLabel_idchuyendi);
                panel7.Controls.Add(hiddenLabel_diemdi);
                panel7.Controls.Add(hiddenLabel_diemden);
                //thêm sự kiện click cho nút chọn chuyến
                btn_chonchuyen.Click += (s, ev) => {
                    long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                    string selectedDiemDi = hiddenLabel_diemdi.Text;
                    string selectedDiemDen = hiddenLabel_diemden.Text;

                    // Tạo một form mới và truyền dữ liệu vào form đó
                    _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);
                 

                    _formdatve.Show();
                };

                // Thêm panel7 vào flowLayoutPanel1
                flowLayoutPanel1.Controls.Add(panel7);
            }
            label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {
                
                flowLayoutPanel1.Visible = true;
                
            }else
            {
                panel_thongbao.Visible = true;
            }
            panel5.Visible = true;
            label11.Text = $"Sáng sớm 00:00 - 06:00 ({somsang})";
            label_buoisang.Text = $"Buổi sáng 06:00 - 12:00 ({buoisang})";
            label_buoichieu.Text = $"Buổi chiều 12:00 - 18:00 ({buoichieu})";
            label_buoitoi.Text = $"Buổi tối 18:00 - 24:00 ({buoitoi})";
        }

        private void cbb_diemdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diemdi = cbb_diemdi.SelectedItem.ToString();
        }

        private void cbb_diemden_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diemden = cbb_diemden.SelectedItem.ToString();
        }

        private void cbb_sove_motchieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
        }

        private void btn_chonchuyen_Click(object sender, EventArgs e)
        {
            Panel panel7 = (Panel)((Button)sender).Parent;
            string idchuyendi_string = panel7.Controls["hiddenLabel_idchuyendi"].Text;
            string diemdi = panel7.Controls["label_diemDiInPanel"].Text;
            string diemden = panel7.Controls["label_diemDenInPanel"].Text;
            long idchuyendi = long.Parse(idchuyendi_string);

            // Tạo một instance mới của Form_datve và truyền các thông tin đã lấy vào constructor
            _formdatve = new Form_datve(idchuyendi, diemdi,diemden);
            var currentForm = this;

            // Đóng form hiện tại (Form1)
            currentForm.Close();
            // Hiển thị form mới
            _formdatve.Show();
        }


        private async void link_boloc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            checkBox_sangsom.Checked = false;
            checkBox_buoisang.Checked = false;
            checkBox_buoichieu.Checked = false;
            checkBox_buoitoi.Checked = false;
            label_kiemtralocloaixe.Text = "no";
            //ẩn bảng hiển thị chuyến đi và thông báo
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;
            //lấy thông tin
            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);

            string query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";

            _databaseConnection = new database();

            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            //kiểm tra số chuyến của từng buổi

            int somsang = 0;
            int buoisang = 0;
            int buoichieu = 0;
            int buoitoi = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {
                sochuyen = sochuyen + 1;
                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                string loaixe = chuyenDi["loaixe"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                TimeSpan timeGiodi = TimeSpan.Parse(giodi);
                if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                {
                    somsang = somsang + 1;
                }
                else if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                {
                    buoisang = buoisang + 1;
                }
                else if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                {
                    buoichieu = buoichieu + 1;
                }
                else
                {
                    buoitoi = buoitoi + 1;
                }
                // Tạo một panel mới (panel7) cho mỗi chuyến đi
                Panel panel7 = new Panel();
                panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                panel7.BorderStyle = BorderStyle.FixedSingle;
                panel7.BackColor = Color.White;
                // Tạo và thiết lập các điều khiển con của panel7
                Label label_giodi = new Label();
                label_giodi.Text = giodi;
                label_giodi.Location = new System.Drawing.Point(10, 10);
                label_giodi.Width = 70;

                Label label_thoigianhanhtrinh = new Label();
                label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                label_thoigianhanhtrinh.Width = 120;

                Label label_gioden = new Label();
                label_gioden.Text = gioden;
                label_gioden.Location = new System.Drawing.Point(250, 10);
                label_gioden.Width = 70;

                Label label_dauben = new Label();
                label_dauben.Text = dauben;
                label_dauben.Location = new System.Drawing.Point(10, 40);
                label_dauben.Width = 150;

                Label label13 = new Label();
                label13.Text = "-----------------------------------------------------------------";
                label13.Location = new System.Drawing.Point(30, 60);
                label13.Height = 10;
                label13.Width = 250;

                Label label_cuoiben = new Label();
                label_cuoiben.Text = cuoiben;
                label_cuoiben.Location = new System.Drawing.Point(250, 40);
                label_cuoiben.Width = 150;

                Label label_soghetrong = new Label();
                label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                label_soghetrong.Location = new System.Drawing.Point(390, 10);

                Label label_loaixe = new Label();
                label_loaixe.Text = loaixe;
                label_loaixe.ForeColor = Color.Green;
                label_loaixe.Location = new System.Drawing.Point(330, 10);
                label_loaixe.Width = 90;

                Label label_giaban = new Label();
                label_giaban.Text = $"{giaVe:N0} Đ";
                label_giaban.ForeColor = Color.Red;
                label_giaban.Location = new System.Drawing.Point(10, 80);
                label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                Button btn_chonchuyen = new Button();
                btn_chonchuyen.Text = "Chọn chuyến";
                btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                btn_chonchuyen.ForeColor = Color.Red;
                //tạo label ẩn để truyền dữ liệu qua trang khác
                Label hiddenLabel_idchuyendi = new Label();
                hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                Label hiddenLabel_diemdi = new Label();
                hiddenLabel_diemdi.Text = diemdi;
                hiddenLabel_diemdi.Visible = false; // Ẩn Label

                Label hiddenLabel_diemden = new Label();
                hiddenLabel_diemden.Text = diemden;
                hiddenLabel_diemden.Visible = false; // Ẩn Label
                // Thêm các điều khiển con vào panel7
                panel7.Controls.Add(label_giodi);
                panel7.Controls.Add(label_thoigianhanhtrinh);
                panel7.Controls.Add(label_gioden);
                panel7.Controls.Add(label_dauben);
                panel7.Controls.Add(label_cuoiben);
                panel7.Controls.Add(label_soghetrong);
                panel7.Controls.Add(label_loaixe);
                panel7.Controls.Add(label13);
                panel7.Controls.Add(label_giaban);
                panel7.Controls.Add(btn_chonchuyen);

                panel7.Controls.Add(hiddenLabel_idchuyendi);
                panel7.Controls.Add(hiddenLabel_diemdi);
                panel7.Controls.Add(hiddenLabel_diemden);
                //thêm sự kiện click cho nút chọn chuyến
                btn_chonchuyen.Click += (s, ev) => {
                    long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                    string selectedDiemDi = hiddenLabel_diemdi.Text;
                    string selectedDiemDen = hiddenLabel_diemden.Text;

                    // Tạo một form mới và truyền dữ liệu vào form đó
                    _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                    _formdatve.Show();
                };

                // Thêm panel7 vào flowLayoutPanel1
                flowLayoutPanel1.Controls.Add(panel7);
            }
            label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {
                
                flowLayoutPanel1.Visible = true;

            }
            else
            {
                panel_thongbao.Visible = true;
            }
            panel5.Visible = true;
            label11.Text = $"Sáng sớm 00:00 - 06:00 ({somsang})";
            label_buoisang.Text = $"Buổi sáng 06:00 - 12:00 ({buoisang})";
            label_buoichieu.Text = $"Buổi chiều 12:00 - 18:00 ({buoichieu})";
            label_buoitoi.Text = $"Buổi tối 18:00 - 24:00 ({buoitoi})";
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private async void btn_ghe_Click(object sender, EventArgs e)
        {
            label_kiemtralocloaixe.Text = "Ghế";
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;
            string loaixe = "Ghế";

            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);
            _databaseConnection = new database();

            string query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy',loaixe: '{loaixe}' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";
            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            int kiemTraThoiGian = 0;
            if (checkBox_sangsom.Checked || checkBox_buoisang.Checked || checkBox_buoichieu.Checked || checkBox_buoitoi.Checked)
            {
                kiemTraThoiGian = 1;
            }
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {
                
                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                //lọc thời gian nếu có
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);

                int locthoigian=0;
                if (kiemTraThoiGian == 1)
                {
                    if (checkBox_sangsom.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoisang.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoichieu.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoitoi.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(18) && timeGiodi < TimeSpan.FromHours(24))
                        {
                            locthoigian = 1;
                        }
                    }
                }
                

                if (locthoigian==1 || kiemTraThoiGian==0)
                {
                    sochuyen = sochuyen + 1;
                    // Tạo một panel mới (panel7) cho mỗi chuyến đi
                    Panel panel7 = new Panel();
                    panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                    panel7.BorderStyle = BorderStyle.FixedSingle;
                    panel7.BackColor = Color.White;
                    // Tạo và thiết lập các điều khiển con của panel7
                    Label label_giodi = new Label();
                    label_giodi.Text = giodi;
                    label_giodi.Location = new System.Drawing.Point(10, 10);
                    label_giodi.Width = 70;

                    Label label_thoigianhanhtrinh = new Label();
                    label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                    label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                    label_thoigianhanhtrinh.Width = 120;

                    Label label_gioden = new Label();
                    label_gioden.Text = gioden;
                    label_gioden.Location = new System.Drawing.Point(250, 10);
                    label_gioden.Width = 70;

                    Label label_dauben = new Label();
                    label_dauben.Text = dauben;
                    label_dauben.Location = new System.Drawing.Point(10, 40);
                    label_dauben.Width = 150;

                    Label label13 = new Label();
                    label13.Text = "-----------------------------------------------------------------";
                    label13.Location = new System.Drawing.Point(30, 60);
                    label13.Height = 10;
                    label13.Width = 250;

                    Label label_cuoiben = new Label();
                    label_cuoiben.Text = cuoiben;
                    label_cuoiben.Location = new System.Drawing.Point(250, 40);
                    label_cuoiben.Width = 150;

                    Label label_soghetrong = new Label();
                    label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                    label_soghetrong.Location = new System.Drawing.Point(390, 10);

                    Label label_loaixe = new Label();
                    label_loaixe.Text = loaixe;
                    label_loaixe.ForeColor = Color.Green;
                    label_loaixe.Location = new System.Drawing.Point(330, 10);
                    label_loaixe.Width = 90;

                    Label label_giaban = new Label();
                    label_giaban.Text = $"{giaVe:N0} Đ";
                    label_giaban.ForeColor = Color.Red;
                    label_giaban.Location = new System.Drawing.Point(10, 80);
                    label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                    Button btn_chonchuyen = new Button();
                    btn_chonchuyen.Text = "Chọn chuyến";
                    btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                    btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                    btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                    btn_chonchuyen.ForeColor = Color.Red;
                    //tạo label ẩn để truyền dữ liệu qua trang khác
                    Label hiddenLabel_idchuyendi = new Label();
                    hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                    hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemdi = new Label();
                    hiddenLabel_diemdi.Text = diemdi;
                    hiddenLabel_diemdi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemden = new Label();
                    hiddenLabel_diemden.Text = diemden;
                    hiddenLabel_diemden.Visible = false; // Ẩn Label
                                                         // Thêm các điều khiển con vào panel7
                    panel7.Controls.Add(label_giodi);
                    panel7.Controls.Add(label_thoigianhanhtrinh);
                    panel7.Controls.Add(label_gioden);
                    panel7.Controls.Add(label_dauben);
                    panel7.Controls.Add(label_cuoiben);
                    panel7.Controls.Add(label_soghetrong);
                    panel7.Controls.Add(label_loaixe);
                    panel7.Controls.Add(label13);
                    panel7.Controls.Add(label_giaban);
                    panel7.Controls.Add(btn_chonchuyen);

                    panel7.Controls.Add(hiddenLabel_idchuyendi);
                    panel7.Controls.Add(hiddenLabel_diemdi);
                    panel7.Controls.Add(hiddenLabel_diemden);
                    //thêm sự kiện click cho nút chọn chuyến
                    btn_chonchuyen.Click += (s, ev) => {
                        long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                        string selectedDiemDi = hiddenLabel_diemdi.Text;
                        string selectedDiemDen = hiddenLabel_diemden.Text;

                        // Tạo một form mới và truyền dữ liệu vào form đó
                        _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                        _formdatve.Show();
                    };

                    // Thêm panel7 vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(panel7);
                }
                label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
                
            }
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {

                flowLayoutPanel1.Visible = true;

            }
            else
            {
                panel_thongbao.Visible = true;
            }
        }

        private async void btn_giuong_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            label_kiemtralocloaixe.Text = "Giường";
            panel_thongbao.Visible = false;
            string loaixe = "Giường";

            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);
            _databaseConnection = new database();

            string query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy',loaixe: '{loaixe}' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";
            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            int kiemTraThoiGian = 0;
            if (checkBox_sangsom.Checked || checkBox_buoisang.Checked || checkBox_buoichieu.Checked || checkBox_buoitoi.Checked)
            {
                kiemTraThoiGian = 1;
            }
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {

                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                //lọc thời gian nếu có
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);

                int locthoigian = 0;
                if (kiemTraThoiGian == 1)
                {
                    if (checkBox_sangsom.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoisang.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoichieu.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoitoi.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(18) && timeGiodi < TimeSpan.FromHours(24))
                        {
                            locthoigian = 1;
                        }
                    }
                }


                if (locthoigian == 1 || kiemTraThoiGian == 0)
                {
                    sochuyen = sochuyen + 1;
                    // Tạo một panel mới (panel7) cho mỗi chuyến đi
                    Panel panel7 = new Panel();
                    panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                    panel7.BorderStyle = BorderStyle.FixedSingle;
                    panel7.BackColor = Color.White;
                    // Tạo và thiết lập các điều khiển con của panel7
                    Label label_giodi = new Label();
                    label_giodi.Text = giodi;
                    label_giodi.Location = new System.Drawing.Point(10, 10);
                    label_giodi.Width = 70;

                    Label label_thoigianhanhtrinh = new Label();
                    label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                    label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                    label_thoigianhanhtrinh.Width = 120;

                    Label label_gioden = new Label();
                    label_gioden.Text = gioden;
                    label_gioden.Location = new System.Drawing.Point(250, 10);
                    label_gioden.Width = 70;

                    Label label_dauben = new Label();
                    label_dauben.Text = dauben;
                    label_dauben.Location = new System.Drawing.Point(10, 40);
                    label_dauben.Width = 150;

                    Label label13 = new Label();
                    label13.Text = "-----------------------------------------------------------------";
                    label13.Location = new System.Drawing.Point(30, 60);
                    label13.Height = 10;
                    label13.Width = 250;

                    Label label_cuoiben = new Label();
                    label_cuoiben.Text = cuoiben;
                    label_cuoiben.Location = new System.Drawing.Point(250, 40);
                    label_cuoiben.Width = 150;

                    Label label_soghetrong = new Label();
                    label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                    label_soghetrong.Location = new System.Drawing.Point(390, 10);

                    Label label_loaixe = new Label();
                    label_loaixe.Text = loaixe;
                    label_loaixe.ForeColor = Color.Green;
                    label_loaixe.Location = new System.Drawing.Point(330, 10);
                    label_loaixe.Width = 90;

                    Label label_giaban = new Label();
                    label_giaban.Text = $"{giaVe:N0} Đ";
                    label_giaban.ForeColor = Color.Red;
                    label_giaban.Location = new System.Drawing.Point(10, 80);
                    label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                    Button btn_chonchuyen = new Button();
                    btn_chonchuyen.Text = "Chọn chuyến";
                    btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                    btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                    btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                    btn_chonchuyen.ForeColor = Color.Red;
                    //tạo label ẩn để truyền dữ liệu qua trang khác
                    Label hiddenLabel_idchuyendi = new Label();
                    hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                    hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemdi = new Label();
                    hiddenLabel_diemdi.Text = diemdi;
                    hiddenLabel_diemdi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemden = new Label();
                    hiddenLabel_diemden.Text = diemden;
                    hiddenLabel_diemden.Visible = false; // Ẩn Label
                                                         // Thêm các điều khiển con vào panel7
                    panel7.Controls.Add(label_giodi);
                    panel7.Controls.Add(label_thoigianhanhtrinh);
                    panel7.Controls.Add(label_gioden);
                    panel7.Controls.Add(label_dauben);
                    panel7.Controls.Add(label_cuoiben);
                    panel7.Controls.Add(label_soghetrong);
                    panel7.Controls.Add(label_loaixe);
                    panel7.Controls.Add(label13);
                    panel7.Controls.Add(label_giaban);
                    panel7.Controls.Add(btn_chonchuyen);

                    panel7.Controls.Add(hiddenLabel_idchuyendi);
                    panel7.Controls.Add(hiddenLabel_diemdi);
                    panel7.Controls.Add(hiddenLabel_diemden);
                    //thêm sự kiện click cho nút chọn chuyến
                    btn_chonchuyen.Click += (s, ev) => {
                        long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                        string selectedDiemDi = hiddenLabel_diemdi.Text;
                        string selectedDiemDen = hiddenLabel_diemden.Text;

                        // Tạo một form mới và truyền dữ liệu vào form đó
                        _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                        _formdatve.Show();
                    };

                    // Thêm panel7 vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(panel7);
                }
                label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
                
            }
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {

                flowLayoutPanel1.Visible = true;

            }
            else
            {
                panel_thongbao.Visible = true;
            }
        }

        private async void btn_limousine_Click(object sender, EventArgs e)
        {
            label_kiemtralocloaixe.Text = "Limousine";
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;
            string loaixe = "Limousine";

            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);
            _databaseConnection = new database();

            string query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy',loaixe: '{loaixe}' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";
            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            int kiemTraThoiGian = 0;
            if (checkBox_sangsom.Checked || checkBox_buoisang.Checked || checkBox_buoichieu.Checked || checkBox_buoitoi.Checked)
            {
                kiemTraThoiGian = 1;
            }
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {

                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                //lọc thời gian nếu có
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);

                int locthoigian = 0;
                if (kiemTraThoiGian == 1)
                {
                    if (checkBox_sangsom.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoisang.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoichieu.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoitoi.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(18) && timeGiodi < TimeSpan.FromHours(24))
                        {
                            locthoigian = 1;
                        }
                    }
                }


                if (locthoigian == 1 || kiemTraThoiGian == 0)
                {
                    sochuyen = sochuyen + 1;
                    // Tạo một panel mới (panel7) cho mỗi chuyến đi
                    Panel panel7 = new Panel();
                    panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                    panel7.BorderStyle = BorderStyle.FixedSingle;
                    panel7.BackColor = Color.White;
                    // Tạo và thiết lập các điều khiển con của panel7
                    Label label_giodi = new Label();
                    label_giodi.Text = giodi;
                    label_giodi.Location = new System.Drawing.Point(10, 10);
                    label_giodi.Width = 70;

                    Label label_thoigianhanhtrinh = new Label();
                    label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                    label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                    label_thoigianhanhtrinh.Width = 120;

                    Label label_gioden = new Label();
                    label_gioden.Text = gioden;
                    label_gioden.Location = new System.Drawing.Point(250, 10);
                    label_gioden.Width = 70;

                    Label label_dauben = new Label();
                    label_dauben.Text = dauben;
                    label_dauben.Location = new System.Drawing.Point(10, 40);
                    label_dauben.Width = 150;

                    Label label13 = new Label();
                    label13.Text = "-----------------------------------------------------------------";
                    label13.Location = new System.Drawing.Point(30, 60);
                    label13.Height = 10;
                    label13.Width = 250;

                    Label label_cuoiben = new Label();
                    label_cuoiben.Text = cuoiben;
                    label_cuoiben.Location = new System.Drawing.Point(250, 40);
                    label_cuoiben.Width = 150;

                    Label label_soghetrong = new Label();
                    label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                    label_soghetrong.Location = new System.Drawing.Point(390, 10);

                    Label label_loaixe = new Label();
                    label_loaixe.Text = loaixe;
                    label_loaixe.ForeColor = Color.Green;
                    label_loaixe.Location = new System.Drawing.Point(330, 10);
                    label_loaixe.Width = 90;

                    Label label_giaban = new Label();
                    label_giaban.Text = $"{giaVe:N0} Đ";
                    label_giaban.ForeColor = Color.Red;
                    label_giaban.Location = new System.Drawing.Point(10, 80);
                    label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                    Button btn_chonchuyen = new Button();
                    btn_chonchuyen.Text = "Chọn chuyến";
                    btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                    btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                    btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                    btn_chonchuyen.ForeColor = Color.Red;
                    //tạo label ẩn để truyền dữ liệu qua trang khác
                    Label hiddenLabel_idchuyendi = new Label();
                    hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                    hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemdi = new Label();
                    hiddenLabel_diemdi.Text = diemdi;
                    hiddenLabel_diemdi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemden = new Label();
                    hiddenLabel_diemden.Text = diemden;
                    hiddenLabel_diemden.Visible = false; // Ẩn Label
                                                         // Thêm các điều khiển con vào panel7
                    panel7.Controls.Add(label_giodi);
                    panel7.Controls.Add(label_thoigianhanhtrinh);
                    panel7.Controls.Add(label_gioden);
                    panel7.Controls.Add(label_dauben);
                    panel7.Controls.Add(label_cuoiben);
                    panel7.Controls.Add(label_soghetrong);
                    panel7.Controls.Add(label_loaixe);
                    panel7.Controls.Add(label13);
                    panel7.Controls.Add(label_giaban);
                    panel7.Controls.Add(btn_chonchuyen);

                    panel7.Controls.Add(hiddenLabel_idchuyendi);
                    panel7.Controls.Add(hiddenLabel_diemdi);
                    panel7.Controls.Add(hiddenLabel_diemden);
                    //thêm sự kiện click cho nút chọn chuyến
                    btn_chonchuyen.Click += (s, ev) => {
                        long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                        string selectedDiemDi = hiddenLabel_diemdi.Text;
                        string selectedDiemDen = hiddenLabel_diemden.Text;

                        // Tạo một form mới và truyền dữ liệu vào form đó
                        _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                        _formdatve.Show();
                    };

                    // Thêm panel7 vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(panel7);
                }
                label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
                
            }
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {

                flowLayoutPanel1.Visible = true;

            }
            else
            {
                panel_thongbao.Visible = true;
            }
        }

        private async void checkBox_buoisang_CheckedChanged(object sender, EventArgs e)
        {
            string loaixe = "";
            if (label_kiemtralocloaixe.Text == "Limousine")
            {
                loaixe = "Limousine";
            }
            else if (label_kiemtralocloaixe.Text == "Giường")
            {
                loaixe = "Giường";
            }
            else if (label_kiemtralocloaixe.Text == "Ghế")
            {
                loaixe = "Ghế";
            }
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;


            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);
            _databaseConnection = new database();

            string query2 = "";

            if (label_kiemtralocloaixe.Text == "no")
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";
            }
            else
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy',loaixe: '{loaixe}' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";

            }
            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            int kiemTraThoiGian = 0;
            if (checkBox_sangsom.Checked || checkBox_buoisang.Checked || checkBox_buoichieu.Checked || checkBox_buoitoi.Checked)
            {
                kiemTraThoiGian = 1;
            }
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {

                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                string lx = chuyenDi["loaixe"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                //lọc thời gian nếu có
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);

                int locthoigian = 0;
                if (kiemTraThoiGian == 1)
                {
                    if (checkBox_sangsom.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoisang.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoichieu.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoitoi.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(18) && timeGiodi < TimeSpan.FromHours(24))
                        {
                            locthoigian = 1;
                        }
                    }
                }


                if (locthoigian == 1 || kiemTraThoiGian == 0)
                {
                    sochuyen = sochuyen + 1;
                    // Tạo một panel mới (panel7) cho mỗi chuyến đi
                    Panel panel7 = new Panel();
                    panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                    panel7.BorderStyle = BorderStyle.FixedSingle;
                    panel7.BackColor = Color.White;
                    // Tạo và thiết lập các điều khiển con của panel7
                    Label label_giodi = new Label();
                    label_giodi.Text = giodi;
                    label_giodi.Location = new System.Drawing.Point(10, 10);
                    label_giodi.Width = 70;

                    Label label_thoigianhanhtrinh = new Label();
                    label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                    label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                    label_thoigianhanhtrinh.Width = 120;

                    Label label_gioden = new Label();
                    label_gioden.Text = gioden;
                    label_gioden.Location = new System.Drawing.Point(250, 10);
                    label_gioden.Width = 70;

                    Label label_dauben = new Label();
                    label_dauben.Text = dauben;
                    label_dauben.Location = new System.Drawing.Point(10, 40);
                    label_dauben.Width = 150;

                    Label label13 = new Label();
                    label13.Text = "-----------------------------------------------------------------";
                    label13.Location = new System.Drawing.Point(30, 60);
                    label13.Height = 10;
                    label13.Width = 250;

                    Label label_cuoiben = new Label();
                    label_cuoiben.Text = cuoiben;
                    label_cuoiben.Location = new System.Drawing.Point(250, 40);
                    label_cuoiben.Width = 150;

                    Label label_soghetrong = new Label();
                    label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                    label_soghetrong.Location = new System.Drawing.Point(390, 10);

                    Label label_loaixe = new Label();
                    label_loaixe.Text = lx;
                    label_loaixe.ForeColor = Color.Green;
                    label_loaixe.Location = new System.Drawing.Point(330, 10);
                    label_loaixe.Width = 90;

                    Label label_giaban = new Label();
                    label_giaban.Text = $"{giaVe:N0} Đ";
                    label_giaban.ForeColor = Color.Red;
                    label_giaban.Location = new System.Drawing.Point(10, 80);
                    label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                    Button btn_chonchuyen = new Button();
                    btn_chonchuyen.Text = "Chọn chuyến";
                    btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                    btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                    btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                    btn_chonchuyen.ForeColor = Color.Red;
                    //tạo label ẩn để truyền dữ liệu qua trang khác
                    Label hiddenLabel_idchuyendi = new Label();
                    hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                    hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemdi = new Label();
                    hiddenLabel_diemdi.Text = diemdi;
                    hiddenLabel_diemdi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemden = new Label();
                    hiddenLabel_diemden.Text = diemden;
                    hiddenLabel_diemden.Visible = false; // Ẩn Label
                                                         // Thêm các điều khiển con vào panel7
                    panel7.Controls.Add(label_giodi);
                    panel7.Controls.Add(label_thoigianhanhtrinh);
                    panel7.Controls.Add(label_gioden);
                    panel7.Controls.Add(label_dauben);
                    panel7.Controls.Add(label_cuoiben);
                    panel7.Controls.Add(label_soghetrong);
                    panel7.Controls.Add(label_loaixe);
                    panel7.Controls.Add(label13);
                    panel7.Controls.Add(label_giaban);
                    panel7.Controls.Add(btn_chonchuyen);

                    panel7.Controls.Add(hiddenLabel_idchuyendi);
                    panel7.Controls.Add(hiddenLabel_diemdi);
                    panel7.Controls.Add(hiddenLabel_diemden);
                    //thêm sự kiện click cho nút chọn chuyến
                    btn_chonchuyen.Click += (s, ev) => {
                        long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                        string selectedDiemDi = hiddenLabel_diemdi.Text;
                        string selectedDiemDen = hiddenLabel_diemden.Text;

                        // Tạo một form mới và truyền dữ liệu vào form đó
                        _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                        _formdatve.Show();
                    };

                    // Thêm panel7 vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(panel7);
                }
                label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
                
            }
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {
                flowLayoutPanel1.Visible = true;
            }
            else
            {
                panel_thongbao.Visible = true;
            }
        }

        private async void checkBox_buoichieu_CheckedChanged(object sender, EventArgs e)
        {
            string loaixe = "";
            if (label_kiemtralocloaixe.Text == "Limousine")
            {
                loaixe = "Limousine";
            }
            else if (label_kiemtralocloaixe.Text == "Giường")
            {
                loaixe = "Giường";
            }
            else if (label_kiemtralocloaixe.Text == "Ghế")
            {
                loaixe = "Ghế";
            }
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;


            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);
            _databaseConnection = new database();

            string query2 = "";

            if (label_kiemtralocloaixe.Text == "no")
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";
            }
            else
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy',loaixe: '{loaixe}' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";

            }
            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            int kiemTraThoiGian = 0;
            if (checkBox_sangsom.Checked || checkBox_buoisang.Checked || checkBox_buoichieu.Checked || checkBox_buoitoi.Checked)
            {
                kiemTraThoiGian = 1;
            }
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {

                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                string lx = chuyenDi["loaixe"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                //lọc thời gian nếu có
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);

                int locthoigian = 0;
                if (kiemTraThoiGian == 1)
                {
                    if (checkBox_sangsom.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoisang.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoichieu.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoitoi.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(18) && timeGiodi < TimeSpan.FromHours(24))
                        {
                            locthoigian = 1;
                        }
                    }
                }


                if (locthoigian == 1 || kiemTraThoiGian == 0)
                {
                    sochuyen = sochuyen + 1;
                    // Tạo một panel mới (panel7) cho mỗi chuyến đi
                    Panel panel7 = new Panel();
                    panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                    panel7.BorderStyle = BorderStyle.FixedSingle;
                    panel7.BackColor = Color.White;
                    // Tạo và thiết lập các điều khiển con của panel7
                    Label label_giodi = new Label();
                    label_giodi.Text = giodi;
                    label_giodi.Location = new System.Drawing.Point(10, 10);
                    label_giodi.Width = 70;

                    Label label_thoigianhanhtrinh = new Label();
                    label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                    label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                    label_thoigianhanhtrinh.Width = 120;

                    Label label_gioden = new Label();
                    label_gioden.Text = gioden;
                    label_gioden.Location = new System.Drawing.Point(250, 10);
                    label_gioden.Width = 70;

                    Label label_dauben = new Label();
                    label_dauben.Text = dauben;
                    label_dauben.Location = new System.Drawing.Point(10, 40);
                    label_dauben.Width = 150;

                    Label label13 = new Label();
                    label13.Text = "-----------------------------------------------------------------";
                    label13.Location = new System.Drawing.Point(30, 60);
                    label13.Height = 10;
                    label13.Width = 250;

                    Label label_cuoiben = new Label();
                    label_cuoiben.Text = cuoiben;
                    label_cuoiben.Location = new System.Drawing.Point(250, 40);
                    label_cuoiben.Width = 150;

                    Label label_soghetrong = new Label();
                    label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                    label_soghetrong.Location = new System.Drawing.Point(390, 10);

                    Label label_loaixe = new Label();
                    label_loaixe.Text = lx;
                    label_loaixe.ForeColor = Color.Green;
                    label_loaixe.Location = new System.Drawing.Point(330, 10);
                    label_loaixe.Width = 90;

                    Label label_giaban = new Label();
                    label_giaban.Text = $"{giaVe:N0} Đ";
                    label_giaban.ForeColor = Color.Red;
                    label_giaban.Location = new System.Drawing.Point(10, 80);
                    label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                    Button btn_chonchuyen = new Button();
                    btn_chonchuyen.Text = "Chọn chuyến";
                    btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                    btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                    btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                    btn_chonchuyen.ForeColor = Color.Red;
                    //tạo label ẩn để truyền dữ liệu qua trang khác
                    Label hiddenLabel_idchuyendi = new Label();
                    hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                    hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemdi = new Label();
                    hiddenLabel_diemdi.Text = diemdi;
                    hiddenLabel_diemdi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemden = new Label();
                    hiddenLabel_diemden.Text = diemden;
                    hiddenLabel_diemden.Visible = false; // Ẩn Label
                                                         // Thêm các điều khiển con vào panel7
                    panel7.Controls.Add(label_giodi);
                    panel7.Controls.Add(label_thoigianhanhtrinh);
                    panel7.Controls.Add(label_gioden);
                    panel7.Controls.Add(label_dauben);
                    panel7.Controls.Add(label_cuoiben);
                    panel7.Controls.Add(label_soghetrong);
                    panel7.Controls.Add(label_loaixe);
                    panel7.Controls.Add(label13);
                    panel7.Controls.Add(label_giaban);
                    panel7.Controls.Add(btn_chonchuyen);

                    panel7.Controls.Add(hiddenLabel_idchuyendi);
                    panel7.Controls.Add(hiddenLabel_diemdi);
                    panel7.Controls.Add(hiddenLabel_diemden);
                    //thêm sự kiện click cho nút chọn chuyến
                    btn_chonchuyen.Click += (s, ev) => {
                        long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                        string selectedDiemDi = hiddenLabel_diemdi.Text;
                        string selectedDiemDen = hiddenLabel_diemden.Text;

                        // Tạo một form mới và truyền dữ liệu vào form đó
                        _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                        _formdatve.Show();
                    };

                    // Thêm panel7 vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(panel7);
                }
                label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
                
            }
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {
                flowLayoutPanel1.Visible = true;
            }
            else
            {
                panel_thongbao.Visible = true;
            }
        }

        private async void checkBox_buoitoi_CheckedChanged(object sender, EventArgs e)
        {
            string loaixe = "";
            if (label_kiemtralocloaixe.Text == "Limousine")
            {
                loaixe = "Limousine";
            }
            else if (label_kiemtralocloaixe.Text == "Giường")
            {
                loaixe = "Giường";
            }
            else if (label_kiemtralocloaixe.Text == "Ghế")
            {
                loaixe = "Ghế";
            }
            flowLayoutPanel1.Visible = false;
            panel_thongbao.Visible = false;


            string diemdi = cbb_diemdi.SelectedItem.ToString();
            string diemden = cbb_diemden.SelectedItem.ToString();
            DateTime selectedDate = dateTime_ngaydi.Value;
            string sove_motchieu = cbb_sove_motchieu.SelectedItem.ToString();
            int soveMotChieu = int.Parse(sove_motchieu);
            _databaseConnection = new database();

            string query2 = "";

            if (label_kiemtralocloaixe.Text == "no")
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";
            }
            else
            {
                query2 = $"MATCH (d1:Diem {{tendiem: '{diemdi}'}}) MATCH (d2:Diem {{tendiem: '{diemden}'}}) MATCH (d1)-[r:CHUYENDI {{ ngaydi: '{selectedDate.ToString("yyyy-MM-dd")}', trangthai: 'chưa chạy',loaixe: '{loaixe}' }}]->(d2) WHERE r.sovetrong  >= {soveMotChieu} RETURN r, d1.tendiem AS diemdi, d2.tendiem AS diemden";

            }
            // Fetching the data from Neo4j
            List<IRecord> results = await _databaseConnection.GetDataFromneo4j(query2);
            int kiemTraThoiGian = 0;
            if (checkBox_sangsom.Checked || checkBox_buoisang.Checked || checkBox_buoichieu.Checked || checkBox_buoitoi.Checked)
            {
                kiemTraThoiGian = 1;
            }
            flowLayoutPanel1.Controls.Clear();
            int sochuyen = 0;
            // Lặp qua các kết quả và hiển thị chúng trên flowLayoutPanel1
            foreach (var record in results)
            {

                var chuyenDi = record["r"].As<IRelationship>();

                int soVeTrong = chuyenDi["sovetrong"].As<int>();
                long idchuyendi = chuyenDi.Id;
                //long idchuyendi = id;
                string giodi = chuyenDi["giodi"].As<string>();
                string gioden = chuyenDi["gioden"].As<string>();
                string thoigianhanhtrinh = chuyenDi["thoigianhanhtrinh"].As<string>();
                string dauben = chuyenDi["dauben"].As<string>();
                string cuoiben = chuyenDi["cuoiben"].As<string>();
                string lx = chuyenDi["loaixe"].As<string>();
                decimal giaVe = chuyenDi["giave"].As<decimal>();

                //lọc thời gian nếu có
                TimeSpan timeGiodi = TimeSpan.Parse(giodi);

                int locthoigian = 0;
                if (kiemTraThoiGian == 1)
                {
                    if (checkBox_sangsom.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(0) && timeGiodi < TimeSpan.FromHours(6))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoisang.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(6) && timeGiodi < TimeSpan.FromHours(12))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoichieu.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(12) && timeGiodi < TimeSpan.FromHours(18))
                        {
                            locthoigian = 1;
                        }
                    }
                    if (checkBox_buoitoi.Checked)
                    {
                        if (timeGiodi >= TimeSpan.FromHours(18) && timeGiodi < TimeSpan.FromHours(24))
                        {
                            locthoigian = 1;
                        }
                    }
                }


                if (locthoigian == 1 || kiemTraThoiGian == 0)
                {
                    sochuyen = sochuyen + 1;
                    // Tạo một panel mới (panel7) cho mỗi chuyến đi
                    Panel panel7 = new Panel();
                    panel7.Size = new Size(500, 100); // Điều chỉnh kích thước panel nếu cần
                    panel7.BorderStyle = BorderStyle.FixedSingle;
                    panel7.BackColor = Color.White;
                    // Tạo và thiết lập các điều khiển con của panel7
                    Label label_giodi = new Label();
                    label_giodi.Text = giodi;
                    label_giodi.Location = new System.Drawing.Point(10, 10);
                    label_giodi.Width = 70;

                    Label label_thoigianhanhtrinh = new Label();
                    label_thoigianhanhtrinh.Text = $"---- {thoigianhanhtrinh} giờ ----";
                    label_thoigianhanhtrinh.Location = new System.Drawing.Point(100, 10);
                    label_thoigianhanhtrinh.Width = 120;

                    Label label_gioden = new Label();
                    label_gioden.Text = gioden;
                    label_gioden.Location = new System.Drawing.Point(250, 10);
                    label_gioden.Width = 70;

                    Label label_dauben = new Label();
                    label_dauben.Text = dauben;
                    label_dauben.Location = new System.Drawing.Point(10, 40);
                    label_dauben.Width = 150;

                    Label label13 = new Label();
                    label13.Text = "-----------------------------------------------------------------";
                    label13.Location = new System.Drawing.Point(30, 60);
                    label13.Height = 10;
                    label13.Width = 250;

                    Label label_cuoiben = new Label();
                    label_cuoiben.Text = cuoiben;
                    label_cuoiben.Location = new System.Drawing.Point(250, 40);
                    label_cuoiben.Width = 150;

                    Label label_soghetrong = new Label();
                    label_soghetrong.Text = $"{soVeTrong} chỗ trống";
                    label_soghetrong.Location = new System.Drawing.Point(390, 10);

                    Label label_loaixe = new Label();
                    label_loaixe.Text = lx;
                    label_loaixe.ForeColor = Color.Green;
                    label_loaixe.Location = new System.Drawing.Point(330, 10);
                    label_loaixe.Width = 90;

                    Label label_giaban = new Label();
                    label_giaban.Text = $"{giaVe:N0} Đ";
                    label_giaban.ForeColor = Color.Red;
                    label_giaban.Location = new System.Drawing.Point(10, 80);
                    label_giaban.Font = new Font(label_giaban.Font, FontStyle.Bold);

                    Button btn_chonchuyen = new Button();
                    btn_chonchuyen.Text = "Chọn chuyến";
                    btn_chonchuyen.Location = new System.Drawing.Point(360, 65);
                    btn_chonchuyen.Size = new System.Drawing.Size(120, 28);
                    btn_chonchuyen.BackColor = Color.Orange; // Thiết lập màu nền là màu cam
                    btn_chonchuyen.ForeColor = Color.Red;
                    //tạo label ẩn để truyền dữ liệu qua trang khác
                    Label hiddenLabel_idchuyendi = new Label();
                    hiddenLabel_idchuyendi.Text = idchuyendi.ToString();
                    hiddenLabel_idchuyendi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemdi = new Label();
                    hiddenLabel_diemdi.Text = diemdi;
                    hiddenLabel_diemdi.Visible = false; // Ẩn Label

                    Label hiddenLabel_diemden = new Label();
                    hiddenLabel_diemden.Text = diemden;
                    hiddenLabel_diemden.Visible = false; // Ẩn Label
                                                         // Thêm các điều khiển con vào panel7
                    panel7.Controls.Add(label_giodi);
                    panel7.Controls.Add(label_thoigianhanhtrinh);
                    panel7.Controls.Add(label_gioden);
                    panel7.Controls.Add(label_dauben);
                    panel7.Controls.Add(label_cuoiben);
                    panel7.Controls.Add(label_soghetrong);
                    panel7.Controls.Add(label_loaixe);
                    panel7.Controls.Add(label13);
                    panel7.Controls.Add(label_giaban);
                    panel7.Controls.Add(btn_chonchuyen);

                    panel7.Controls.Add(hiddenLabel_idchuyendi);
                    panel7.Controls.Add(hiddenLabel_diemdi);
                    panel7.Controls.Add(hiddenLabel_diemden);
                    //thêm sự kiện click cho nút chọn chuyến
                    btn_chonchuyen.Click += (s, ev) => {
                        long selectedIdChuyenDi = long.Parse(hiddenLabel_idchuyendi.Text);
                        string selectedDiemDi = hiddenLabel_diemdi.Text;
                        string selectedDiemDen = hiddenLabel_diemden.Text;

                        // Tạo một form mới và truyền dữ liệu vào form đó
                        _formdatve = new Form_datve(selectedIdChuyenDi, selectedDiemDi, selectedDiemDen);


                        _formdatve.Show();
                    };

                    // Thêm panel7 vào flowLayoutPanel1
                    flowLayoutPanel1.Controls.Add(panel7);
                }
                label_sochuyen.Text = $"{diemdi} - {diemden} ({sochuyen})";
                
            }
            label_sochuyen.Visible = true;
            if (sochuyen > 0)
            {
                flowLayoutPanel1.Visible = true;
            }
            else
            {
                panel_thongbao.Visible = true;
            }
        }
    }
}

