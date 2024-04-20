using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using System;
using System.Collections.Generic;

namespace CSDLGia_ASP.ViewModels.Manages.KeKhaiGia
{
    public class VMKkGiaShow
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Macskd { get; set; }
        public string Tendn { get; set; }
        public string Tencskd { get; set; }
        public string Socv { get; set; }
        public string Diadanh { get; set; }
        public DateTime Ngaynhap { get; set; }
        public string Tendvhienthi { get; set; }
        public DateTime Ngayhieuluc { get; set; }
        public string Ttnguoinop { get; set; }
        public string Diachi { get; set; }
        public string Diachikd { get; set; }
        public string Dtll { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Fax { get; set; }
        public string Sohsnhan { get; set; }
        public string Ytcauthanhgia { get; set; }
        public string Thydggadgia { get; set; }
        public string Ptnguyennhan { get; set; }
        public string Chinhsachkm { get; set; }
        public string Loaihang { get; set; }
        public string Chucvuky { get; set; }
        public string Nguoiky { get; set; }
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
        public List<KkGiaDatSanLapCt> KkGiaDatSanLapCt { get; set; }
        public List<KkGiaCatSanCt> KkGiaCatSanCt { get; set; }
        public List<KkGiaHpLxCt> KkGiaHpLxCt { get; set; }
        public List<KkGiaDvChCt> KkGiaDvChCt { get; set; }
        public List<KkGiaDaXayDungCt> KkGiaDaXayDungCt { get; set; }
        public List<KkCuocVcHkCt> KkCuocVcHkCt { get; set; }
        public List<KkGiaDvLtCt> KkGiaDvLtCt { get; set; }
        public List<KkGiaSieuThiCt> KkGiaSieuThiCt { get; set; }
        public List<KkGiaVlXdCt> KkGiaVlXdCt { get; set; }
        public List<KkGiaLuHanhCt> KkGiaLuHanhCt { get; set; }
        public List<KkMhBogCt> KkMhBogCt { get; set; }
    }
}
