using CSDLGia_ASP.Models.Manages.DinhGia;
using System.Collections.Generic;

namespace CSDLGia_ASP.ViewModels.Systems.CSDLQuocGia
{
    public class VMGiaHHDVK
    {
        public string DIA_BAN { get; set; }
        public string NGUON_SO_LIEU { get; set; }
        public int DINH_KY { get; set; }
        public int THOI_GIAN_BC_1 { get; set; }
        public int THOI_GIAN_BC_2 { get; set; }
        public int THOI_GIAN_BC_NAM { get; set; }
        public string FILE_DINH_KEM_WORD { get; set; }
        public string FILE_DINH_KEM_PDF { get; set; }
        public string NGUOI_TAO { get; set; }
        public string NGUOI_DUYET { get; set; }
        public List<VMGiaHHDVK_DSHH> DS_HHDV_TT { get; set; }
    }
}
