using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvCuTheCt
    {
        [Key]

        public int Id { get; set; }
        public string Mahs { get; set; }
        
        // Bổ sung
        public string Maspdv { get; set; }
        public string TenSpDv { get; set; }
        public string Manhom { get; set; }
        public string Dvt { get; set; }
        public double Mucgia1 { get; set; }
        public double Mucgia2 { get; set; }
        public double Mucgia3 { get; set; }
        public double Mucgia4 { get; set; }
        public double Mucgia5 { get; set; }
        public double Mucgia6 { get; set; }
        public string Style { get; set; }
        public int Sapxep { get; set; }
        public string Tt { get; set; }
        //
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
       
        public string Trangthai { get; set; }
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Tennhom { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public string Soqd { get; set; }       

    }
}