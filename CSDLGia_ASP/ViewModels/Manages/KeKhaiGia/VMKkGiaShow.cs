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
