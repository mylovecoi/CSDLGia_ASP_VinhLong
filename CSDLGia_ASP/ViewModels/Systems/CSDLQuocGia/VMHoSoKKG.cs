using CSDLGia_ASP.Models.Manages.DinhGia;
using System.Collections.Generic;

namespace CSDLGia_ASP.ViewModels.Systems.CSDLQuocGia
{
    public class VMHoSoKKG
    {
        public int MAU_BIEU { get; set; } //Mặc định = 3
        public string DIA_BAN { get; set; }
        public int LOAI_HO_SO { get; set; } //Mặc định = 1
        public int LOAI_XNK { get; set; } //Mặc định = 0
        public string DOANH_NGHIEP_DKKK { get; set; }
        public string SO_VAN_BAN { get; set; }
        public string NGAY_THUC_HIEN { get; set; }
        public string NGAY_BD_HIEU_LUC { get; set; }
        public string DONVI_TTSL { get; set; }
        public string QUOC_GIA_XNK { get; set; }
        public string CHI_NHANH { get; set; }
        public string KHO_HANG { get; set; }
        public string TINH_THANH { get; set; }
        public string DOI_TUONG_AP_DUNG { get; set; }
        public double TY_GIA { get; set; }
        public string NGUOI_KY { get; set; }
        public string NGAY_KY { get; set; }
        public string TRICH_YEU { get; set; }
        public string PHAN_TICH_NGUYEN_NHAN { get; set; }
        public string HINH_THUC_THANH_TOAN { get; set; }
        public string NGUOI_TAO { get; set; }
        public string NGAY_TAO { get; set; }
        public string NGUOI_DUYET { get; set; }
        public string NGAY_DUYET { get; set; }
        public List<VMHoSoKKG_DSHH> DS_HHDV_DKG { get; set; }
        public List<VMHoSoKKG_DK> DS_FILE_DINH_KEM { get; set; }
    }
}
