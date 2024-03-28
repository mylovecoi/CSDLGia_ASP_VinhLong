using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.ViewModels.Manages.DinhGia
{
    public class VMDinhGiaThueTsc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Mahs { get; set; }
        public string Soqd { get; set; }
        public string Thongtinhs { get; set; }
        public string Congbo { get; set; }
        public string Lichsu { get; set; }
        public string Ghichu { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Macqcq { get; set; }
        public string Madv { get; set; }
        public string Lydo { get; set; }
        public string Thongtin { get; set; }
        public string Trangthai { get; set; }
        public DateTime Thoidiem_h { get; set; }
        public string Macqcq_h { get; set; }
        public string Madv_h { get; set; }
        public string Lydo_h { get; set; }
        public string Thongtin_h { get; set; }
        public string Trangthai_h { get; set; }
        public DateTime Thoidiem_t { get; set; }
        public string Macqcq_t { get; set; }
        public string Madv_t { get; set; }
        public string Lydo_t { get; set; }
        public string Thongtin_t { get; set; }
        public string Trangthai_t { get; set; }
        public DateTime Thoidiem_ad { get; set; }
        public string Macqcq_ad { get; set; }
        public string Madv_ad { get; set; }
        public string Lydo_ad { get; set; }
        public string Thongtin_ad { get; set; }
        public string Trangthai_ad { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }


        public string Level { get; set; }

        public string Mataisan { get; set; }
        public double Dongiathue { get; set; }
        public double Sotienthuenam { get; set; }


        public string Dvthue { get; set; }
        public string Tentaisan { get; set; }
        public string Diachi { get; set; }
        public string Soqdpd { get; set; }
        public DateTime Thoigianpd { get; set; }
        public string Soqddg { get; set; }
        public DateTime Thoigiandg { get; set; }
        public string Hdthue { get; set; }
        public string Ththue { get; set; }
        public DateTime Thuetungay { get; set; }
        public DateTime Thuedenngay { get; set; }


        public DateTime Thoigianpd_create { get; set; }
        public DateTime Thoigiandg_create { get; set; }
        public DateTime Thuetungay_create { get; set; }
        public DateTime Thuedenngay_create { get; set; }

        public List<DsDonVi> DsDonVi { get; set; }
        public List<DsDiaBan> DsDiaBan { get; set; }

        [NotMapped]
        public List<GiaThueTaiSanCongCt> GiaThueTaiSanCongCt { get; set; }
        [NotMapped]
        public List<ThongTinGiayTo> ThongTinGiayTo { get; set; }
    }
}
