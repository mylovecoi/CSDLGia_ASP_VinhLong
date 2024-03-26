using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaRungCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Manhom { get; set; }
        public string Phanloai { get; set; }
        public string Noidung { get; set; }
        public string Dvt { get; set; }
        public double Dientich { get; set; }
        public double Dientichsd { get; set; }
        public double Giatri { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public double Giakhoidiem { get; set; }
        public double Dongia { get; set; }
        public string Dvthue { get; set; }
        public string Diachi { get; set; }
        public string Soqdpd { get; set; }
        public DateTime Thoigianpd { get; set; }
        public string Soqdgkd { get; set; }
        public DateTime Thoigiangkd { get; set; }
        public DateTime Thuetungay { get; set; }
        public DateTime Thuedenngay { get; set; }
        public string Trangthai { get; set; }
        /*public double SapXep { get; set; }
        public string HienThi { get; set; }*/
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }

        //
        public int STTSapXep { get; set; }
        public string STTHienThi { get; set; }
        public string MoTa { get; set; }
        public string Style { get; set; }
        public double GiaRung1 { get; set; }
        public double GiaRung2 { get; set; }
        public double GiaRung3 { get; set; }
        public double GiaRung4 { get; set; }
        public double GiaRung5 { get; set; }
        public double GiaRung6 { get; set; }
        public double GiaChoThue1 { get; set; }
        public double GiaChoThue2 { get; set; }
        public double GiaBoiThuong1 { get; set; }
        public double GiaBoiThuong2 { get; set; }
        public double GiaBoiThuong3 { get; set; }
        public double GiaBoiThuong4 { get; set; }
        public double GiaBoiThuong5 { get; set; }
        public double GiaBoiThuong6 { get; set; }

        [NotMapped]
        public string Tennhom { get; set; }
      
        [NotMapped]
        public string SoQD { get; set; }


    }
}
