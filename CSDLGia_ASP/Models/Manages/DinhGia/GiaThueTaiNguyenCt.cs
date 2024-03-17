using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueTaiNguyenCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Level { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string Cap3 { get; set; }
        public string Cap4 { get; set; }
        public string Cap5 { get; set; }
        public string Cap6 { get; set; }
        public string Ten { get; set; }
        public string Dvt { get; set; }
        public double Gia { get; set; }
        public double SapXep { get; set; }
        public double Style { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Manhom { get; set; }
        [NotMapped]
        public string Tennhom { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
       
    }
}
