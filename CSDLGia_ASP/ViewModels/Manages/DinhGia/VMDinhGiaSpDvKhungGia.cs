﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.ViewModels.Manages.DinhGia
{
    public class VMDinhGiaSpDvKhungGia
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Soqd { get; set; }
        public string Ttqd { get; set; }
        public string Congbo { get; set; }
        public string Thaotac { get; set; }
        public string Ghichu { get; set; }
        public string Lichsu { get; set; }
        public string Tinhtrang { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Macqcq { get; set; }
        public string Madv { get; set; }
        public string Lydo { get; set; }
        public string Thongtin { get; set; }
        public string Trangthai { get; set; }
        public string Ipf1 { get; set; }
        public string Ipf2 { get; set; }
        public string Ipf3 { get; set; }
        public string Ipf4 { get; set; }
        public string Ipf5 { get; set; }
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
        public string Maspdv { get; set; }
        public string Mota { get; set; }
        public double Giatoithieu { get; set; }
        public double Giatoida { get; set; }
        public string Phanloaidv { get; set; }
        public string Dvt { get; set; }
       
        public string MaDiaBan { get; set; }
        public string MaQhNs { get; set; }
        public string MaDv { get; set; }
        public string TenDv { get; set; }
        public string DiaChi { get; set; }
        public string TtLienHe { get; set; }
        public string EmailQl { get; set; }
        public string EmailQt { get; set; }
        public string SoNgayLv { get; set; }
        public string TenDvHienThi { get; set; }
        public string TenDvCqHienThi { get; set; }
        public string ChucVuKy { get; set; }
        public string ChucVuKyThay { get; set; }
        public string NguoiKy { get; set; }
        public string DiaDanh { get; set; }
        public string ChucNang { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
       
        public string TenDiaBan { get; set; }
        public string Level { get; set; }
        public string GhiChu { get; set; }
       
        public List<DsDonVi> DsDonVi { get; set; }
        public List<DsDiaBan> DsDiaBan { get; set; }
        public List<GiaSpDvKhungGia> GiaSpDvKhungGia { get; set; }
        public List<GiaSpDvKhungGiaCt> GiaSpDvKhungGiaCt { get; set; }
    }
}