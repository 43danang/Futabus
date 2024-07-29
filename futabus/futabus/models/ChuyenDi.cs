using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futabus.models
{
    internal class ChuyenDi
    {
        public string DiemDi { get; set; }
        public string DiemDen { get; set; }
        public string SoChuyen { get; set; }
        public DateTime NgayDi { get; set; }
        public string GioDi { get; set; }
        public DateTime NgayDen { get; set; }
        public string GioDen { get; set; }
        public string LoaiXe { get; set; }
        public int SoVeTrong { get; set; }
        public string LuuY { get; set; }
        public decimal GiaVe { get; set; }
        public double QuangDuong { get; set; }
        public string ThoiGianHanhTrinh { get; set; }
        public string DauBen { get; set; }
        public string CuoiBen { get; set; }
        public int SoGheTrongHangDau { get; set; }
        public int SoGheTrongHangGiua { get; set; }
        public int SoGheTrongHangCuoi { get; set; }
        public int SoGheTrongTangTren { get; set; }
        public int SoGheTrongTangDuoi { get; set; }
        public string MaXe { get; set; }
        public string MaTaiXe { get; set; }
        //trạng thái là chưa chạy, đang chạy, đã hoàn thành, đã hủy
        public string TrangThai { get; set; }
        public ChuyenDi() { }
    }
}
