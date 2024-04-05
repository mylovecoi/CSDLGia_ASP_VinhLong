using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsDonVi
    {
        [Key]
        public int Id { get; set; }
        public string MaDiaBan { get; set; }
        public string MaQhNs { get; set; }
        public string MaDv { get; set; }
        public string TenDv { get; set; }
        public string DiaChi { get; set; }
        public string TtLienHe { get; set; }
        public string EmailQl { get; set; }
        public string EmailQt { get; set; }
        public string SoNgayLv { get; set; }
        public string TenDvHienThi { get; set; }
        public string TenDvCqHienThi { get; set; }
        public string ChucVuKy { get; set; }
        public string ChucVuKyThay { get; set; }
        public string NguoiKy { get; set; }
        public string DiaDanh { get; set; }
        public string ChucNang { get; set; }
        public bool XetDuyet { get; set; }
        public bool CongBo { get; set; }
        public bool NhapLieu { get; set; }
        public bool QuanTri { get; set; }
        public string DiaBanApDung { get; set; }//Các địa bàn áp dụng (Huyện A;Huyện B;....)
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
