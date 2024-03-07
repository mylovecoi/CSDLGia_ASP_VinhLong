using System;
using System.Collections.Generic;

namespace CSDLGia_ASP.Models.Systems.KetNoiGiaDichVu
{
    public class VMDoanhNghiepCsKd
    {
        public int id { get; set; }
        public string tendn { get; set; }
        public string masothue { get; set; }
        public string diachidn { get; set; }
        public string teldn { get; set; }
        public string faxdn { get; set; }
        public string email { get; set; }
        public string noidknopthue { get; set; }
        public string giayphepkd { get; set; }
        public string chucdanhky { get; set; }
        public string nguoiky { get; set; }
        public string diadanh { get; set; }
        public string trangthai { get; set; }
        public string tailieu { get; set; }
        public string cqcq { get; set; }
        public List<VMCoSoKinhDoanhDVLT> ds_cskd { get; set; }    
    }
}
