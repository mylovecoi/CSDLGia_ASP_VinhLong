using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
