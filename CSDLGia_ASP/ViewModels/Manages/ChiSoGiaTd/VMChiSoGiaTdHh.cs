using System;

namespace CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd
{
    public class VMChiSoGiaTdHh
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Thongtinbc { get; set; }
        public string Tennhom { get; set; }
        public string Tenhanghoa { get; set; }
        //public string Masonhomhanghoa { get; set; }
        public string MasohanghoaInDm { get; set; }
        public string Masohanghoa { get; set; }
        public string Masogoc { get; set; }
        public string MasogocInDm { get; set; }
        public double Giagoc { get; set; }
        public double Giakychon { get; set; }
        public double QuyensoTt { get; set; }
        public double QuyensoNt { get; set; }
        public string Dvt { get; set; }
        public string Kychon { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
