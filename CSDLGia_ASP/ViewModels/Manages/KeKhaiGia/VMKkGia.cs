using System;
using System.Collections.Generic;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;

namespace CSDLGia_ASP.ViewModels.Manages.KeKhaiGia
{
    public class VMKkGia
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madiaban { get; set; }
        public DateTime Ngaynhap { get; set; }
        public DateTime Ngayhieuluc { get; set; }
        public string Manghe { get; set; }
        public string Socv { get; set; }
        public string Socvlk { get; set; }
        public DateTime Ngaycvlk { get; set; }
        public string Ytcauthanhgia { get; set; }
        public string Thydggadgia { get; set; }
        public string Ttnguoinop { get; set; }
        public string Dtll { get; set; }
        public string Plhs { get; set; }
        public string Thoihan { get; set; }
        public string Dvt { get; set; }
        public string Congbo { get; set; }
        public string Lichsu { get; set; }
        public string Ghichu { get; set; }
        public string Macqcq { get; set; }
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
        public string Tendn { get; set; }
        public string Level { get; set; }
        public List<KkGiaXmTxdCt> KkGiaXmTxdCt { get; set; }
        public List<KkGiaEtanolCt> KkGiaEtanolCt { get; set; }
        public List<KkGiaSachCt> KkGiaSachCt { get; set; }
        public List<KkGsCt> KkGsCt { get; set; }
        public List<KkGiaVtXbCt> KkGiaVtXbCt { get; set; }
        public List<KkGiaVtXkCt> KkGiaVtXkCt { get; set; }
        public List<KkGiaVtXtxCt> KkGiaVtXtxCt { get; set; }
        public List<KkGiaThanCt> KkGiaThanCt { get; set; }
        public List<KkGiaGiayCt> KkGiaGiayCt { get; set; }
        public List<KkGiaTaCnCt> KkGiaTaCnCt { get; set; }
    }
}
