﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaTaiSanCong
    {
        [Key]
        public int Id { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Mahs { get; set; }
        public string Soqd { get; set; }
        public string Mataisan { get; set; }
        public string Thongtinhs { get; set; }
        public DateTime Tungay { get; set; }
        public DateTime Denngay { get; set; }
        public double Giathue { get; set; }
        public string Congbo { get; set; }
        public string Lichsu { get; set; }
        public string Ghichu { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Macqcq { get; set; }
        public string Madv { get; set; }
        public string Lydo { get; set; }
        public string Thongtin { get; set; }
        public string Trangthai { get; set; }
        public DateTime Thoidiem_h { get; set; }
        public string Macqcq_h { get; set; }
        public string Madv_h { get; set; }
        public string Lydo_h { get; set; }
        public string Thongtin_h { get; set; }
        public string Trangthai_h { get; set; }
        public DateTime Thoidiem_t { get; set; }
        public string Macqcq_t { get; set; }
        public string Madv_t { get; set; }
        public string Lydo_t { get; set; }
        public string Thongtin_t { get; set; }
        public string Trangthai_t { get; set; }
        public DateTime Thoidiem_ad { get; set; }
        public string Macqcq_ad { get; set; }
        public string Madv_ad { get; set; }
        public string Lydo_ad { get; set; }
        public string Thongtin_ad { get; set; }
        public string Trangthai_ad { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public double Giapheduyet { get; set; }
        public double Giaconlai { get; set; }
        public double Giaban { get; set; }
        public string Phanloai { get; set; }
        public string Dacdiem { get; set; }
        public string Tentaisan { get; set; }
        public string Mota { get; set; }
        public string Ipf1 { get; set; }
        public string Ipf2 { get; set; }
        public string Ipf3 { get; set; }
        public string Ipf4 { get; set; }
        public string Ipf5 { get; set; }
        public string PhanLoaiHoSo { get; set; }//0: Hồ sơ nhập chi tiết; 1: Hồ sơ nhận dữ liệu từ file excel
        public string CodeExcel { get; set; }
        //Trang thái kết nối CSDLQG
        public string TrangThaiCSDLQG { get; set; }
        public DateTime NgayKetNoi { get; set; }
        [NotMapped]
        public string Level { get; set; }
        [NotMapped]
        public List<GiaTaiSanCongCt> GiaTaiSanCongCt { get; set; }
        [NotMapped]
        public List<ThongTinGiayTo> ThongTinGiayTo { get; set; }
       
        [NotMapped]
        public string TenDonVi { get; set; }

    }
}
