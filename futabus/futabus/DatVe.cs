using System;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Neo4j.Driver;


namespace futabus
{
    public partial class DatVe : Form
    {
        private IMongoDatabase database;
        private bool isButtonOrange = false;
        private IMongoCollection<Customer> customerCollection;
        private IDriver neo4jDriver;
        private IDriver _driver;
        private IDriver driver;


        public DatVe()
        {
            InitializeComponent();
            ConnectToMongoDB();
            ConnectToNeo4J();
        }

        private void ConnectToNeo4J()
        {
            string uri = "bolt://localhost:7687/FUTABUS"; // Thay bằng địa chỉ của Neo4j
            string user = "neo4j"; // Thay bằng tên người dùng của bạn
            string password = "12345678"; // Thay bằng mật khẩu của bạn
            IResultCursor cursor;
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            IAsyncSession session = _driver.AsyncSession();
           

        }
        public class Customer
        {
            public string hoTen { get; set; }
            // Add other properties as needed
        }

        private void ConnectToMongoDB()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("futabus");
        }


        private List<Button> selectedButtons = new List<Button>();
        private int selectedButtonCount = 0;

        private void LoadButtonData()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("futabus");
            var collection = database.GetCollection<BsonDocument>("Ghe");
            var filter = Builders<BsonDocument>.Filter.Eq("MaChuyenDi", 1);
            var document = collection.Find(filter).FirstOrDefault();

            if (document != null)
            {
                var dsghe = document.GetValue("DSGhe").AsBsonArray;

                for (int i = 0; i < 44; i++)
                {
                    var button = this.Controls.Find($"button{i + 1}", true).FirstOrDefault() as Button;
                    if (button != null)
                    {
                        if (i < dsghe.Count)
                        {
                            var ghe = dsghe[i].AsBsonDocument;
                            button.Text = ghe.GetValue("SoGhe").AsString;

                            if (ghe.GetValue("TrangThai").AsString == "Còn Trống")
                            {
                                button.BackColor = Color.Aqua;
                            }
                            else
                            {
                                button.BackColor = SystemColors.ButtonShadow;
                            }
                        }
                        else
                        {
                            button.Text = string.Empty;
                            button.BackColor = SystemColors.Control;
                        }
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }


        private void button18_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void button45_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button37_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            // Kiểm tra màu nền của button
            if (clickedButton.BackColor == System.Drawing.SystemColors.ButtonShadow)
            {
                // Nếu màu nền là Button Shadow, không làm gì cả
                return;
            }

            if (!isButtonOrange)
            {
                clickedButton.BackColor = System.Drawing.Color.OrangeRed;
                isButtonOrange = true;
                selectedButtons.Add(clickedButton);
                selectedButtonCount++;
                UpdateLabel13();
                UpdateLabel12();
            }
            else
            {
                clickedButton.BackColor = System.Drawing.Color.Aqua;
                isButtonOrange = false;
                selectedButtons.Remove(clickedButton);
                selectedButtonCount--;
                UpdateLabel13();
                UpdateLabel12();
            }
            UpdateLabel14();
            UpdateLabel16();
            UpdateLabel21();
        }

        private void UpdateLabel13()
        {
            if (selectedButtons.Count == 0)
            {
                label13.Text = "";
            }
            else
            {
                label13.Text = string.Join(", ", selectedButtons.Select(b => b.Text));
            }
        }

        private int CountSelectedButtons()
        {
            int count = 0;
            foreach (Button button in selectedButtons)
            {
                if (button.BackColor == System.Drawing.Color.OrangeRed)
                {
                    count++;
                }
            }
            return count;
        }

        private void UpdateLabel12()
        {
            label12.Text = CountSelectedButtons().ToString();
        }

        private int giave;

        private async void GetTripFareForTrip1()
        {
            // Lấy giá trị của thuộc tính "giave" trong quan hệ "chuyến đi" có id = 1
            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(@"
                        MATCH ()-[r:CHUYENDI]->()
                        WHERE id(r) = 1
                        RETURN r.giave
                    ");

                var record = await result.SingleAsync();
                //giave = record["r.giave"].As<int>();
                giave = record["r.giave"].As<int>();
            }
        }
        private void UpdateLabel14()
        {
            int selectedButtonCount = CountSelectedButtons();
            string formattedValue = (selectedButtonCount * giave).ToString("N0") + "đ";
            label14.Text = formattedValue;
        }
        private void UpdateLabel16()
        {

            // Lấy giá trị từ label14
            string label14Text = label14.Text;

            // Gán giá trị từ label14 vào label16
            label16.Text = label14Text;
        }

        private void UpdateLabel21()
        {

            // Lấy giá trị từ label14
            string label14Text = label14.Text;

            // Gán giá trị từ label14 vào label21
            label21.Text = label14Text;
        }

        private async void LoadCustomerName()
        {
            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("futabus");
                var collection = database.GetCollection<BsonDocument>("Customer");

                // Tìm khách hàng có userID = 1
                var filter = Builders<BsonDocument>.Filter.Eq("userID", 1);
                var customer = await collection.Find(filter).FirstOrDefaultAsync();

                // Lưu họ tên của khách hàng vào textBox1
                if (customer != null)
                {
                    textBox1.Text = customer["hoTen"].AsString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void LoadCustomerPhone()
        {
            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("futabus");
                var collection = database.GetCollection<BsonDocument>("Customer");

                // Tìm khách hàng có userID = 1
                var filter = Builders<BsonDocument>.Filter.Eq("userID", 1);
                var customer = await collection.Find(filter).FirstOrDefaultAsync();

                // Lưu họ tên của khách hàng vào textBox1
                if (customer != null)
                {
                    textBox2.Text = customer["sdt"].AsString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void LoadCustomerEmail()
        {
            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("futabus");
                var collection = database.GetCollection<BsonDocument>("Customer");

                // Tìm khách hàng có userID = 1
                var filter = Builders<BsonDocument>.Filter.Eq("userID", 1);
                var customer = await collection.Find(filter).FirstOrDefaultAsync();

                // Lưu họ tên của khách hàng vào textBox1
                if (customer != null)
                {
                    textBox3.Text = customer["email"].AsString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private async void LoadBenDau()
        {
            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(@"
            MATCH ()-[r:CHUYENDI]->()
            WHERE id(r) = 1
            RETURN r.dauben
        ");

                var record = await result.SingleAsync();
                label10.Text = record["r.dauben"].As<string>();
            }
        }

        private async void LoadBenCuoi()
        {
            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(@"
            MATCH ()-[r:CHUYENDI]->()
            WHERE id(r) = 1
            RETURN r.cuoiben
        ");

                var record = await result.SingleAsync();
                label30.Text = record["r.cuoiben"].As<string>();
            }
        }

        private async void LoadGioDi()
        {
            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(@"
            MATCH ()-[r:CHUYENDI]->()
            WHERE id(r) = 1
            RETURN r.giodi
        ");

                var record = await result.SingleAsync();
                label32.Text = record["r.giodi"].As<string>();
            }
        }

        private async void LoadChuyenDiData()
        {
            using (var session = _driver.AsyncSession())
            {
                var result = await session.RunAsync(@"
            MATCH ()-[r:CHUYENDI]->()
            WHERE id(r) = 1
            RETURN r.ngaydi
        ");

                var record = await result.SingleAsync();
                label11.Text = record["r.ngaydi"].As<string>();
                label27.Text = record["r.ngaydi"].As<string>();
            }
        }


        private async void UpdateBtn_DangNhap()
        {
            try
            {
                // Kết nối tới MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("futabus");
                var collection = database.GetCollection<BsonDocument>("Customer");

                // Tìm khách hàng có userID = 1
                var filter = Builders<BsonDocument>.Filter.Eq("userID", 1);
                var customer = await collection.Find(filter).FirstOrDefaultAsync();

                // Lưu họ tên của khách hàng vào textBox1
                if (customer != null)
                {
                    btn_dangnhap.Text = " 👤 " + customer["hoTen"].AsString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void DatVe_Load(object sender, EventArgs e)
        {
            LoadCustomerName();
            LoadCustomerPhone();
            LoadCustomerEmail();
            LoadButtonData();
            LoadChuyenDiData();
            GetTripFareForTrip1();
            LoadBenDau();
            LoadBenCuoi();
            UpdateBtn_DangNhap();
        }

        
    }
}