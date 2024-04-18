using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvToiDaCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maspdv { get; set; }
        public string Trangthai { get; set; }       
        public double Dongia { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Phanloaidv { get; set; }
        public string Dvt { get; set; }
        public string Tenspdv { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }
        public int Sapxep { get; set; }

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
        [NotMapped]
        public string Ttqd { get; set; }
        [NotMapped]
        public string Ghichu { get; set; }
        [NotMapped]
        public string Madiaban { get; set; }
      

    }
}
