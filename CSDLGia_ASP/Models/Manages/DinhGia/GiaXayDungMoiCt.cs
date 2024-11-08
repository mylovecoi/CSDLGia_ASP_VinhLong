﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaXayDungMoiCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Ten { get; set; }
        public string Dvt { get; set; }
        public string Gia { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
       
        public string Manhom { get; set; }
 
        public string Tennhom { get; set; }
        
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public double Dientich { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }
    }
}
