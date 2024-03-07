using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Systems.KetNoiGiaDichVu
{
    public class VMCoSoKinhDoanhDVLT
    {        
        public string macskd { get; set; }
        public string masothue { get; set; }
        public string tencskd { get; set; }
        public string loaihang { get; set; }
        public string diachikd { get; set; }
        public string telkd { get; set; }
        public string toado { get; set; }
        public string link { get; set; }
        public string cqcq { get; set; }
        public string ghichu { get; set; }
        //public DateTime Created_at { get; set; }
        //public DateTime Updated_at { get; set; }
    }
}
