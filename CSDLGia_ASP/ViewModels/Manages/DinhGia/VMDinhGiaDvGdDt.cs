using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.ViewModels.Manages.DinhGia
{
    public class VMDinhGiaDvGdDt
    {

        public int Id { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Mahs { get; set; }
        public string Soqd { get; set; }
        public string Nam { get; set; }
        public string Mota { get; set; }
        public string Congbo { get; set; }
        public string Lichsu { get; set; }
        public string Ghichu { get; set; }
        public string Tunam { get; set; }
        public string Dennam { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Macqcq { get; set; }
        public string Madv { get; set; }
        public string Lydo { get; set; }
        public string Thongtin { get; set; }
        public string Trangthai { get; set; }
        public string Ipf1 { get; set; }
        public string Ipf2 { get; set; }
        public string Ipf3 { get; set; }
        public string Ipf4 { get; set; }
        public string Ipf5 { get; set; }
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


        public string Maspdv { get; set; }
        public string TenDv { get; set; }
        public string TenDiaBan { get; set; }
        public string Level { get; set; }



        public string Namapdung1 { get; set; }
        public double Giathanhthi1 { get; set; }
        public double Gianongthon1 { get; set; }
        public double Giamiennui1 { get; set; }
        public string Namapdung2 { get; set; }
        public double Giathanhthi2 { get; set; }
        public double Gianongthon2 { get; set; }
        public double Giamiennui2 { get; set; }
        public string Namapdung3 { get; set; }
        public double Giathanhthi3 { get; set; }
        public double Gianongthon3 { get; set; }
        public double Giamiennui3 { get; set; }


        public List<DsDonVi> DsDonVi { get; set; }
        public List<DsDiaBan> DsDiaBan { get; set; }

        [NotMapped]
        public List<GiaDvGdDtCt> GiaDvGdDtCt { get; set; }
    }
}
