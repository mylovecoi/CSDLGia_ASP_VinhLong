using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using System;
using System.Collections.Generic;

namespace CSDLGia_ASP.ViewModels.Manages.KeKhaiGia
{
    public class VMKkGiaShow
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Tendn { get; set; }
        public string Socv { get; set; }
        public string Diadanh { get; set; }
        public DateTime Ngaynhap { get; set; }
        public string Tendvhienthi { get; set; }
        public DateTime Ngayhieuluc { get; set; }
        public string Ttnguoinop { get; set; }
        public string Diachi { get; set; }
        public string Dtll { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Sohsnhan { get; set; }
        public string Ytcauthanhgia { get; set; }
        public string Thydggadgia { get; set; }
        public DateTime Ngaychuyen { get; set; }
        public DateTime Ngaynhan { get; set; }
        public List<KkGiaXmTxdCt> KkGiaXmTxdCt { get; set; }
    }
}
