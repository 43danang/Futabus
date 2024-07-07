using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using futabus.models;
using System.Configuration;
using futabus.Utils;

namespace futabus.Login
{

    public partial class Login : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void usernameTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string username = usernameTxt.Text.Trim();
            string password = passwordTxt.Text.Trim();


            UserService userService = new UserService(connectionString);
            User user = userService.GetUserByUsernameAndPassword(username, password);

            if (user != null)
            {
                Session.username = usernameTxt.Text;


                MessageBox.Show($"Xin chào {Session.username}", "Đăng nhập thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Thực hiện các hành động sau khi đăng nhập thành công
                this.Hide(); // Ẩn form đăng nhập

                // Mở form Form1
                Form1 form1 = new Form1();
                form1.Show(); // Hiển thị form Form1
                this.Close();
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập bị sai !");
            }

        }
    }
}
