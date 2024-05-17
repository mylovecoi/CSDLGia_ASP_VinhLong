using CSDLGia_ASP.Models.Manages.DinhGia;
using System.Collections.Generic;

namespace CSDLGia_ASP.ViewModels.Systems.CSDLQuocGia
{
    public class VMHoiDongThamDinhGia
    {
        public string DIA_BAN { get; set; }
        public string MA_HOI_DONG { get; set; }
        public string TO_TUNG { get; set; }
        public string CAN_CU_PHAP_LY { get; set; }
        public string THEO_DE_NGHI_CUA { get; set; }
        public int CAP_HOI_DONG { get; set; }
        public int LOAI_HINH_HOI_DONG { get; set; }
        public string SO_QUYET_DINH_THANH_LAP { get; set; }
        public string NGAY_BAN_HANH { get; set; }
        public string CO_QUAN_BAN_HANH { get; set; }
        public string TEN_HOI_DONG { get; set; }
        public string CHU_TICH_HOI_DONG { get; set; }
        public string CHUC_VU { get; set; }
        public string NHIEM_VU_HOI_DONG { get; set; }
        public string NOI_DUNG_QUYET_DINH { get; set; }
        public string MA_TINH_THANH { get; set; }
        public string MA_QUAN_HUYEN { get; set; }       
        public List<VMHoiDongThamDinhGia_DSTVHD> DS_TV_HOI_DONG { get; set; }
        public List<VMHoiDongThamDinhGia_DSDK> DS_DINH_KEM { get; set; }
    }
}
