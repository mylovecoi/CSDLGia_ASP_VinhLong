﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Systems
{

   
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Madv { get; set; }
        public string Macqcq { get; set; }
        public string Madiaban { get; set; }
        public string Tendn { get; set; }
        public string Diachi { get; set; }
        [Phone]
        public string Tel { get; set; }
        [Phone]
        public string Fax { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Website { get; set; }
        public string Diadanh { get; set; }
        public string Chucdanh { get; set; }
        public string Nguoiky { get; set; }
        public string Noidknopthue { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public string Tailieu { get; set; }
        public string Giayphepkd { get; set; }
        [NotMapped]
        public IFormFile Giayphepkdupload { get; set; }
        public string Level { get; set; }
        public string Avatar { get; set; }
        public string Pl { get; set; }
        public string Mahs { get; set; }
        public string Settingdvvt { get; set; }
        public double Vtxk { get; set; }
        public double Vtxb { get; set; }
        public double Vtxtx { get; set; }
        public double Vtch { get; set; }
        public string Loaihinhhd { get; set; }
        public double Xangdau { get; set; }
        public double Dien { get; set; }
        public double Khidau { get; set; }
        public double Phan { get; set; }
        public double Thuocbvtv { get; set; }
        public double Vacxingsgc { get; set; }
        public double Muoi { get; set; }
        public double Suate6t { get; set; }
        public double Duong { get; set; }
        public double Thocgao { get; set; }
        public double Thuocpcb { get; set; }
        public bool Kiemtra { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public double Than { get; set; }
        public double XmThepXd { get; set; }
        public double SachGk { get; set; }
        public double Etanol { get; set; }
        public double ThucPhamCn { get; set; }
        public double VlXdCatSan { get; set; }
        public double HocPhiDaoTaoLaiXe { get; set; }
        public double Giay { get; set; }
        public double ThucAnChanNuoi { get; set; }
        public double VlXdDatSanlap { get; set; }
        public double VlXdDaXayDung { get; set; }
        public double VlXd { get; set; }
        public double VanTaiKhachBangOtoCoDinh { get; set; }
        public double VanTaiKhachBangXeBuyt { get; set; }
        public double VanTaiKhachBangTaXi { get; set; }
        public double CaHue { get; set; }
        public double SieuThi { get; set; }
        public double Dvlt { get; set; }
        public double BOG { get; set; }
        public double KKNYGIA { get; set; }
        public double DvThuongMai { get; set; }
        public double KhamChuaBenh { get; set; }
        public double LuHanh { get; set; }

    }
}
