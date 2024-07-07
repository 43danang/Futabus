using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace futabus.Utils
{
    public static class Session
    {
        // Các thuộc tính lưu trữ thông tin session
        public static string username { get; set; }

        // Phương thức để đặt giá trị cho Username
        public static void SetUsername(string username)
        {
            username = username;
        }

        // Phương thức để lấy giá trị của Username
        public static string GetUsername()
        {
            return username;
        }

        // Các phương thức khác có thể bổ sung tại đây

        // Ví dụ:
        // public static void ClearSession()
        // {
        //     Username = null;
        // }
    }
}
