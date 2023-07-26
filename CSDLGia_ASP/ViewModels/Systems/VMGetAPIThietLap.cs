using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.ViewModels.Systems
{
    public class VMGetAPIThietLap
    {
        public string Phanloai { get; set; }//Heder;Body;Security/Signature
        public string Tendong { get; set; }
        public string Mota { get; set; }
        public string Kieudulieu { get; set; }
        public string Dinhdang { get; set; }
        public string Dodai { get; set; }
        public bool Batbuoc { get; set; }
        public string Macdinh { get; set; }
        public int Stt { get; set; }
        public string Ghichu { get; set; }
        //Thiết lập Hồ sơ kê khai giá+ hàng hóa thị trường
        public string Tentruong { get; set; }
        public string Tendong_goc { get; set; }

    }
}
