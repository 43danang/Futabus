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
using System;
using System.Windows.Forms;

namespace futabus
{
    public partial class Form1 : Form
    {
        private IDriver _driver;
        public Form1()
        {
            InitializeComponent();
           // this.Width = Screen.PrimaryScreen.WorkingArea.Width;

           

            this.Load += new EventHandler(Form1_Load);

        }
        //private void Form1_Resize(object sender, EventArgs e)
        //{
        //    panel2.Width = (int)(this.Width * 0.95);
        //}
        private async void Form1_Load(object sender, EventArgs e)
        {
            await ConnectToNeo4j();
        }
        private void btnConnectNeo4j_Click(object sender, EventArgs e)
        {
            ConnectToNeo4j().Wait();
        }
        public async Task ConnectToNeo4j()
        {
            string uri = "bolt://localhost:7687/FUTABUS"; // Thay bằng địa chỉ của Neo4j
            string user = "neo4j"; // Thay bằng tên người dùng của bạn
            string password = "12345678"; // Thay bằng mật khẩu của bạn
            IResultCursor cursor;
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            IAsyncSession session = _driver.AsyncSession();

            
            try
            {
                cursor = await session.RunAsync("CREATE (s1:Sinhvien { SVid:1, Name: 'Tiên', age: 21})");
                List <IRecord> lst = await cursor.ToListAsync();
            }
            catch (Exception ex)
            {
            }
            
        }
        private async Task<List<IRecord>> GetDataFromNeo4j()
        {
            List<IRecord> resultRecords = new List<IRecord>();

            try
            {
                IAsyncSession session = _driver.AsyncSession();
                var cursor = await session.RunAsync("MATCH (s:Sinhvien) RETURN s.SVid AS SVid, s.Name AS Name, s.age AS Age");
                resultRecords = await cursor.ToListAsync();
                await session.CloseAsync();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ nếu cần thiết
                MessageBox.Show($"Error retrieving data from Neo4j: {ex.Message}");
            }

            return resultRecords;
        }
        private async void btnFetchData_Click(object sender, EventArgs e)
        {
            var data = await GetDataFromNeo4j();

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

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel_trangchu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
