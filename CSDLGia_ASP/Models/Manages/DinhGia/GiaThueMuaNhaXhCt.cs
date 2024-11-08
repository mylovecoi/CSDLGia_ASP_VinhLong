﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueMuaNhaXhCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maso { get; set; }
        public string Phanloai { get; set; }
        public string Dvt { get; set; }
        public double Dongia { get; set; }
        public double Dongiathue { get; set; }
        public string Dvthue { get; set; }
        public string Hdthue { get; set; }
        public string Ththue { get; set; }
        public string Diachi { get; set; }
        public string Soqdpd { get; set; }
        public DateTime Thoigianpd { get; set; }
        public string Soqddg { get; set; }
        public DateTime Thoigiandg { get; set; }
        public DateTime Tungay { get; set; }
        public DateTime Denngay { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }

        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Ghichu { get; set; }
        [NotMapped]
        public double Dientich { get; set; }
        
        public string Tennha { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }

        
        [NotMapped]
        public string SoQD { get; set; }
    }
}
