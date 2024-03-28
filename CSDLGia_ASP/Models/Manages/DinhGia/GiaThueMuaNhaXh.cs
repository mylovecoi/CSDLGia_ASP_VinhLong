using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueMuaNhaXh
    {
        [Key]
        public int Id { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Mahs { get; set; }
        public string Maso { get; set; }
        public string Mota { get; set; }
        public string Phanloai { get; set; }
        public string Dvt { get; set; }
        public double Dongia { get; set; }
        public DateTime Tungay { get; set; }
        public DateTime Denngay { get; set; }
        public string Soqd { get; set; }
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
        public double Dongiathue { get; set; }
        public string Ipf1 { get; set; }
        [NotMapped]
        public IFormFile Ipf1upload { get; set; }
        public string Ipf2 { get; set; }
        [NotMapped]
        public IFormFile Ipf2upload { get; set; }
        public string Ipf3 { get; set; }
        [NotMapped]
        public IFormFile Ipf3upload { get; set; }
        public string Ipf4 { get; set; }
        [NotMapped]
        public IFormFile Ipf4upload { get; set; }
        public string Ipf5 { get; set; }
        [NotMapped]
        public IFormFile Ipf5upload { get; set; }
        [NotMapped]
        public string Level { get; set; }
        [NotMapped]
        public string MadvCh { get; set; }
        [NotMapped]
        public string TendvCh { get; set; }
        [NotMapped]
        public string Tencqcq { get; set; }
        /*[NotMapped]
        public List<DsDonVi> DsDonVi { get; set; }
        [NotMapped]
        public List<DsDiaBan> DsDiaBan { get; set; }*/
        [NotMapped]
        public List<GiaThueMuaNhaXhCt> GiaThueMuaNhaXhCt { get; set; }
        [NotMapped]
        public string Dvthue { get; set; }
        public string PhanLoaiHoSo { get; set; }//0: Hồ sơ nhập chi tiết; 1: Hồ sơ nhận dữ liệu từ file excel
        public string CodeExcel { get; set; }

        [NotMapped]
        public List<ThongTinGiayTo> ThongTinGiayTo { get; set; }
        [NotMapped]
        public string TenDonVi { get; set; }

    }
}
