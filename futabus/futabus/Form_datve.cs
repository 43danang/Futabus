using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace futabus
{
    public partial class Form_datve : Form
    {
        private long idchuyendi;
        private string diemdi;
        private string diemden;
        public Form_datve()
        {
            InitializeComponent();
        }
        public  Form_datve(long idchuyendi, string diemdi, string diemden)
        {
            InitializeComponent();
            this.idchuyendi = idchuyendi;
            this.diemdi = diemdi;
            this.diemden = diemden;

            // Gán các giá trị vào các label hoặc control tương ứng trên form
            label_idchuyendi.Text = idchuyendi.ToString();
            label_diemdi.Text = diemdi;
            label_diemden.Text = diemden;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label_diemdi_Click(object sender, EventArgs e)
        {

        }
    }
}
