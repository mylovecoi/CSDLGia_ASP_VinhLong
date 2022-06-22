using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsThamDinhVien
    {
        [Key]
        public int Id { get; set; }
        public string GIAY_CN_DU_DK_DKKD { get; set; }
        public string TEN_TIENG_VIET { get; set; }
        public string HO_TEN { get; set; }
        public DateTime NGAY_SINH { get; set; }
        public string GIOI_TINH { get; set; }
        public string CMT_HO_CHIEU { get; set; }
        public DateTime NGAY_CAP_CMT { get; set; }
        public string NOI_CAP_CMT { get; set; }
        public string NGUYEN_QUAN { get; set; }
        public string TINH_THANH { get; set; }
        public string DIA_CHI_THUONG_TRU { get; set; }
        public string DIA_CHI_TAM_TRU { get; set; }
        public string DIEN_THOAI { get; set; }
        public string EMAIL { get; set; }
        public string SO_THE_TDV { get; set; }
        public DateTime NGAY_CAP_THE_TDV { get; set; }
        public bool LA_NGUOI_DAI_DIEN_PL { get; set; }
        public bool LA_LANH_DAO_DN { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
