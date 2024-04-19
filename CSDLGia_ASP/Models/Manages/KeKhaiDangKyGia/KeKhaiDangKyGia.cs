using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGia
    {
        public int Id { get; set; }
        public string MaCsKd { get; set; }
        public string Mahs { get; set; }
        public string PhanLoai { get; set; }
        public string Reports { get; set; }
        [NotMapped]
        public string MaNghe { get; set; }
        [NotMapped]
        public string MaCqCq { get; set; }
        [NotMapped]
        public string TenDv { get; set; }


        public string SoQD { get; set; }
        public DateTime NgayQD { get; set; }
        public string SoQdLk { get; set; }
        public DateTime NgayQdLk { get; set; }
        public DateTime NgayThucHien { get; set; }
        public string DonViTinh { get; set; }

        public string GhiChu { get; set; }


        public string TrangThai { get; set; }
        public string LyDo { get; set; }
        public string SoHsDuyet { get; set; }
        public DateTime NgayDuyet { get; set; }
        

        
    }
}
