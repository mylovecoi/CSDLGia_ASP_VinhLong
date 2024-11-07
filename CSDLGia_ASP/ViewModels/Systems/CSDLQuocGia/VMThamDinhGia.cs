using CSDLGia_ASP.Models.Manages.DinhGia;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;

namespace CSDLGia_ASP.ViewModels.Systems.CSDLQuocGia
{
    public class VMThamDinhGia
    {
        public string DIA_BAN { get; set; }
        public string MA_HOI_DONG_DINH_GIA_TAI_SAN { get; set; }
        public string SO_KET_LUAN { get; set; }
        public string NGAY_BAN_HANH { get; set; }
        public string MUC_DICH { get; set; }
        public string THOI_DIEM_TDG { get; set; }
        public double LOAI_TAI_SAN { get; set; }
        public string TEN_TAI_SAN { get; set; }
        public double GIA_TOAN_BO_TAI_SAN { get; set; }
        public double GIA_LAM_TRON { get; set; }
        public string CAN_CU { get; set; }
        public string CAN_CU_KHAC { get; set; }
        public string NGUYEN_TAC { get; set; }
        public string PHUONG_PHAP { get; set; }
        public string HAN_CHE { get; set; }
        public string NGUOI_NHAP { get; set; }
        public string NGAY_NHAP { get; set; }
        public string NGUOI_DUYET_ID { get; set; } //Tên tài khoản xét duyệt
        public string NGAY_DUYET { get; set; }

        //LOAI_TAI_SAN =1,2 đối với tài sản là đất, nhà
        public string DIA_CHI { get; set; }
        public string MA_TINH_THANH { get; set; }
        public string MA_QUAN_HUYEN { get; set; }
        public string MA_PHUONG_XA { get; set; }
        public string SO_LO { get; set; }
        public string SO_DIA_CHINH { get; set; }
        public double DIEN_TICH { get; set; }
        public string PHAN_LOAI { get; set; }
        //public string DAC_DIEM { get; set; }
        public string MUC_DICH_SU_DUNG { get; set; }
        public string GCN_QUYEN_SD_DAT { get; set; }
        //public string DAC_DIEM_PHAP_LY { get; set; }
        public string LOAI_NHA { get; set; }
        public string CAP_NHA { get; set; }
        public string DIEN_TICH_SU_DUNG { get; set; }
        public string CHAT_LUONG { get; set; }

        //LOAI_TAI_SAN  =3,4: Đối với tài sản là máy móc thiết bị, và tài sản khác
        public string CONG_SUAT { get; set; }       
        public string CHI_TIEU_KY_THUAT { get; set; }
        public string NAM_SX { get; set; }
        public string TEN_NHA_MAY { get; set; }
        public string XUAT_XU { get; set; }
        public string KY_HIEU { get; set; }
        public string NAM_SD { get; set; }
        public string TY_LE_HAO_MON { get; set; }
        public string HOA_DON { get; set; }
        //Dùng chung cho cả 4 loại tài sản
        public string DAC_DIEM { get; set; }
        public string DAC_DIEM_PHAP_LY { get; set; }
        //DS đối tượng
        public List<VMThamDinhGia_DSTS> DS_CHI_TIET_TS_BP { get; set; }
        public List<VMThamDinhGia_DSDK> DS_DINH_KEM { get; set; }
    }
}
