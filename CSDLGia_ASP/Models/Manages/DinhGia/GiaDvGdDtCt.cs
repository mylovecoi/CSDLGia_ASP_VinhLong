﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDvGdDtCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maspdv { get; set; }
        public string Mota { get; set; }
        public string Namapdung1 { get; set; }
        public double Giathanhthi1 { get; set; }
        public double Gianongthon1 { get; set; }
        public double Giamiennui1 { get; set; }
        public string Namapdung2 { get; set; }
        public double Giathanhthi2 { get; set; }
        public double Gianongthon2 { get; set; }
        public double Giamiennui2 { get; set; }
        public string Namapdung3 { get; set; }
        public double Giathanhthi3 { get; set; }
        public double Gianongthon3 { get; set; }
        public double Giamiennui3 { get; set; }
        public string Namapdung4 { get; set; }
        public double Giathanhthi4 { get; set; }
        public double Gianongthon4 { get; set; }
        public double Giamiennui4 { get; set; }
        public string Namapdung5 { get; set; }
        public double Giathanhthi5 { get; set; }
        public double Gianongthon5 { get; set; }
        public double Giamiennui5 { get; set; }

        public string Madv{ get; set; }
        public string Trangthai { get; set; }

        public string Gc { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Tenspdv { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }
        public string MaNhom { get; set; }

        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Tennhom { get; set; }
        [NotMapped]
        public string SoQD { get; set; }
        [NotMapped]
        public DateTime ThoiDiem { get; set; }
    }
}
