using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Systems
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        [RegularExpression(@"^(?=.{5,32}$)(?!.*[._-]{2})(?!.*[0-9]{5,})[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$"
            , ErrorMessage = "Tên đăng nhập không có ký tự đặc biệt, độ dài ít nhất 5 và lớn nhất 32 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Status { get; set; }
        public string Maxa { get; set; }
        public string Mahuyen { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Level { get; set; }
        public bool Sadmin { get; set; }
        public string Permission { get; set; }
        public string Emailxt { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Ttnguoitao { get; set; }
        public string Lydo { get; set; }
        public string Madv { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Manhomtk { get; set; }
        public string Chucnang { get; set; }
        public double Solandn { get; set; }
        public string Group { get; set; }
        public string LinkAPI { get; set; }
        public string Manghanh { get; set; }
        public string Manghe { get; set; }


        [NotMapped]
        public double Vtxk { get; set; }
        [NotMapped]
        public double Vtxb { get; set; }
        [NotMapped]
        public double Vtxtx { get; set; }
        [NotMapped]
        public double Vtch { get; set; }
        [NotMapped]
        public string Loaihinhhd { get; set; }
        [NotMapped]
        public double Xangdau { get; set; }
        [NotMapped]
        public double Dien { get; set; }
        [NotMapped]
        public double Khidau { get; set; }
        [NotMapped]
        public double Phan { get; set; }
        [NotMapped]
        public double Thuocbvtv { get; set; }
        [NotMapped]
        public double Vacxingsgc { get; set; }
        [NotMapped]
        public double Muoi { get; set; }
        [NotMapped]
        public double Suate6t { get; set; }
        [NotMapped]
        public double Duong { get; set; }
        [NotMapped]
        public double Thocgao { get; set; }
        [NotMapped]
        public double Thuocpcb { get; set; }
       
        [NotMapped]
        public double XmThepXd { get; set; }
        [NotMapped]
        public double SachGk { get; set; }
        [NotMapped]
        public double Etanol { get; set; }
        [NotMapped]
        public double ThucPhamCn { get; set; }
        [NotMapped]
        public double VlXdCatSan { get; set; }
        [NotMapped]
        public double HocPhiDaoTaoLaiXe { get; set; }
        [NotMapped]
        public double Than { get; set; }
        [NotMapped]
        public double Giay { get; set; }
        [NotMapped]
        public double ThucAnChanNuoi { get; set; }
        [NotMapped]
        public double VlXdDatSanlap { get; set; }
        [NotMapped]
        public double VlXdDaXayDung { get; set; }
        [NotMapped]
        public double VanTaiKhachBangOtoCoDinh { get; set; }
        [NotMapped]
        public double VanTaiKhachBangXeBuyt { get; set; }
        [NotMapped]
        public double VanTaiKhachBangTaXi { get; set; }
        [NotMapped]
        public double CaHue { get; set; }
        [NotMapped]
        public double SieuThi { get; set; }
        [NotMapped]
        public double Dvlt { get; set; }
        [NotMapped]
        public double BOG { get; set; }
        [NotMapped]
        public double KKNYGIA { get; set; }
        [NotMapped]
        public double VlXd { get; set; }
        [NotMapped]
        public double KhamChuaBenh { get; set; }
        [NotMapped]
        public double DvThuongMai { get; set; }
    }
}
