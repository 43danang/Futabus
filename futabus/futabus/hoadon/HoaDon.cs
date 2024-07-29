using futabus.lienhe;
using futabus.tintuc;
using futabus.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace futabus.hoadon
{
    public partial class HoaDon : Form
    {
        public HoaDon()
        {
            InitializeComponent();
            btn_nguoidung.Text = Session.username;
            linkLabel_trangchu.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel_trangchu2_LinkClicked);
            linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel3_LinkClicked);
            linkLabel4.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel4_LinkClicked);
           // linkLabel5.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel5_LinkClicked);
            linkLabel6.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel6_LinkClicked);
            linkLabel7.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel7_LinkClicked);
        }
        private void linkLabel_trangchu2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();

            // Đăng ký sự kiện FormClosed cho form2
            form1.FormClosed += (s, args) => this.Show();

            form1.Show();
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            futabus.TraCuuVe tracuuve = new TraCuuVe();
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
        //private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    HoaDon hoadon = new HoaDon();
        //    this.Hide();

        //    // Đăng ký sự kiện FormClosed cho form2
        //    hoadon.FormClosed += (s, args) => this.Show();

        //    hoadon.Show();
        //}
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

        private void btn_nguoidung_Click(object sender, EventArgs e)
        {

        }
    }
}
