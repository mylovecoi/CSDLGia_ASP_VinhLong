using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg
{
    public class VMKkMhBog
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Manghe { get; set; }
        public string Tennghe { get; set; }
        public string Theoqd { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Socv { get; set; }
        public string Socvlk { get; set; }
        public DateTime Ngaycvlk { get; set; }
        public DateTime Ngayhieuluc { get; set; }
        public string Dtll { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Plhs { get; set; }
        public string Pldn { get; set; }
        public string Thoihan { get; set; }
        public string Phanloai { get; set; }
        public string Ptnguyennhan { get; set; }
        public string Chinhsachkm { get; set; }
        public string Congbo { get; set; }
        public string Thaotac { get; set; }
        public string Ghichu { get; set; }
        public string Lichsu { get; set; }
        public string Tinhtrang { get; set; }
        public string Macqcq { get; set; }
        public string Tencqcq { get; set; }
        public string Madv { get; set; }
        public DateTime Ngaynhan { get; set; }
        public string Sohsnhan { get; set; }
        public string Nguoichuyen { get; set; }
        public DateTime Ngaychuyen { get; set; }
        public string Lydo { get; set; }
        public string Trangthai { get; set; }
        public string Macqcq_h { get; set; }
        public string Madv_h { get; set; }
        public DateTime Ngaynhan_h { get; set; }
        public string Sohsnhan_h { get; set; }
        public string Nguoichuyen_h { get; set; }
        public DateTime Ngaychuyen_h { get; set; }
        public string Lydo_h { get; set; }
        public string Trangthai_h { get; set; }
        public string Macqcq_t { get; set; }
        public string Madv_t { get; set; }
        public DateTime Ngaynhan_t { get; set; }
        public string Sohsnhan_t { get; set; }
        public string Nguoichuyen_t { get; set; }
        public DateTime Ngaychuyen_t { get; set; }
        public string Lydo_t { get; set; }
        public string Trangthai_t { get; set; }
        public string Macqcq_ad { get; set; }
        public string Madv_ad { get; set; }
        public DateTime Ngaynhan_ad { get; set; }
        public string Sohsnhan_ad { get; set; }
        public string Nguoichuyen_ad { get; set; }
        public DateTime Ngaychuyen_ad { get; set; }
        public string Lydo_ad { get; set; }
        public string Trangthai_ad { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Hoso { get; set; }
        public string Tendn { get; set; }
        public string Tendb { get; set; }
        public string Level { get; set; }
        public string Diachi { get; set; }
        public string Tel { get; set; }

        public List<KkMhBogCt> KkMhBogCt { get; set; }
       

    }
}
