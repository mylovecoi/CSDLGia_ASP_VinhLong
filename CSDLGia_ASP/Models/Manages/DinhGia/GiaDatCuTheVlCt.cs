using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatCuTheVlCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Manhom { get; set; }
        public double ChiPhiNhanCong { get; set; }
        public double ChiPhiDungCu { get; set; }
        public double ChiPhiNangLuong { get; set; }
        public double ChiPhiKhauHao { get; set; }
        public double ChiPhiVatLieu { get; set; }
        public double ChiPhiTrucTiepKkh { get; set; }
        public double ChiPhiTrucTiepCkh { get; set; }
        public double ChiPhiQlChungKkh { get; set; }
        public double ChiPhiQlChungCkh { get; set; }
        public double DonGiaKkh { get; set; }
        public double DonGiaCkh { get; set; }

        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Madv { get; set; }
        public string MaDiaBan { get; set; }
        public string MaXaPhuong { get; set; }

        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Loaidat { get; set; }
        [NotMapped]
        public string PhanLoai { get; set; }
        [NotMapped]
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

        public int STTSapXep { get; set; }
        public string STTHienThi { get; set; }
        public string MoTa { get; set; }
        public string Style { get; set; }
        [NotMapped]
        public string TenDiaBan { get; set; }
        [NotMapped]
        public string MaDiaBanCapHuyen { get; set; }

        [NotMapped]
        public string Soqd { get; set; }
    }
}
