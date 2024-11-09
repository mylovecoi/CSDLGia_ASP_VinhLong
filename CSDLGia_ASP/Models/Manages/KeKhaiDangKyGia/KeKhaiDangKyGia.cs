using CSDLGia_ASP.Models.Manages.DinhGia;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGia
    {
        [Key]
        public int Id { get; set; }
        public string MaCsKd { get; set; }
        public string Mahs { get; set; }
        public string PhanLoai { get; set; }
        public string Reports { get; set; }
        public string MaNghe { get; set; }
        public string MaCqCq { get; set; }
        [NotMapped]
        public string TenDv { get; set; }
        [NotMapped]
        public string TenCsKd { get; set; }
        [NotMapped]
        public string TenCqCq { get; set; }
        [NotMapped]
        public string TenNghe { get; set; }

        public string SoQD { get; set; }
        public DateTime NgayQD { get; set; }
        public string SoQdLk { get; set; }
        public DateTime NgayQdLk { get; set; }
        public DateTime NgayThucHien { get; set; }
        public string DonViTinh { get; set; }
        public string GhiChu { get; set; }

        public string ThongTinNguoiChuyen { get; set; }
        public string SoDtNguoiChuyen { get; set; }
        public DateTime NgayChuyen { get; set; }

        public string TrangThai { get; set; }
        public string LyDo { get; set; }
        public string SoHsDuyet { get; set; }
        public DateTime NgayDuyet { get; set; }

        public string Ytcauthanhgia { get; set; }
        public string Thydggadgia { get; set; }

        public DateTime Thoidiem { get; set; }

        //Trang thái kết nối CSDLQG
        public string TrangThaiCSDLQG { get; set; }
        public DateTime NgayKetNoi { get; set; }

        [NotMapped]
        public List<KeKhaiDangKyGiaCt> KeKhaiDangKyGiaCt { get; set; }

        [NotMapped]
        public List<ThongTinGiayTo> ThongTinGiayTo { get; set; }

    }
}
