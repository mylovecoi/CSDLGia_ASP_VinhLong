using System;
using System.Collections.Generic;
using CSDLGia_ASP.Models.Manages.ChiSoGiaTd;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd
{
    public class VMChiSoGiaTdDm
    {
        public int Id { get; set; }
        public string Tendanhmuc { get; set; }
        public string Mahs { get; set; }
        public string Masohanghoa { get; set; }
        public string Masonhomhanghoa { get; set; }
        public double Giakychon { get; set; }
        public double QuyensoTt { get; set; }
        public double QuyensoNt { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string Tennhom { get; set; }
        public string Tenhanghoa { get; set; }
        public string Masogoc { get; set; }
        public double Giagoc { get; set; }
        public string Dvt { get; set; }
        public string Baocao { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
