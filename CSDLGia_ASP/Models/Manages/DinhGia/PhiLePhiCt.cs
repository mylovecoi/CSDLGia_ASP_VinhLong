using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class PhiLePhiCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string MaSo { get; set; }
        public string MaSoGoc { get; set; }
        public string HienThi { get; set; }//HIện thị dữ liệu ra màn hình
        public int STT { get; set; }//Số thứ tự theo mã gốc
        public int CapDo { get; set; }//Bắt đầu từ 1
        public string ChiTieu { get; set; }
        public string Dvt { get; set; }
        public double Dongia { get; set; } = 0;
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public string Ttqd { get; set; }
        public double SapXep { get; set; }
        public string Style { get; set; }
    }
}
