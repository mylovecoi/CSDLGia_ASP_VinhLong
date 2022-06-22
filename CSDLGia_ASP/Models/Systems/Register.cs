using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Systems
{
    public class Register
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Madv { get; set; }
        public string Madiaban { get; set; }
        public string Macqcq { get; set; }
        [Required]
        public string Tendn { get; set; }
        public string Diachi { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Diadanh { get; set; }
        public string Chucdanh { get; set; }
        public string Nguoiky { get; set; }
        public string Noidknopthue { get; set; }
        public string Ghichu { get; set; }
        public string Tailieu { get; set; }
        public string Giayphepkd { get; set; }
        [NotMapped]
        public IFormFile Giayphepkdupload { get; set; }
        [Required]
        [RegularExpression(@"^(?=.{5,32}$)(?!.*[._-]{2})(?!.*[0-9]{5,})[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Level { get; set; }
        public string Trangthai { get; set; }
        public string Lydo { get; set; }
        public string Ma { get; set; }
        public string Pl { get; set; }
        public string Settingdvvt { get; set; }
        public double Vtxk { get; set; }
        public double Vtxb { get; set; }
        public double Vtxtx { get; set; }
        public double Vtch { get; set; }
        public string Loaihinhhd { get; set; }
        public double Xangdau { get; set; }
        public double Dien { get; set; }
        public double Khidau { get; set; }
        public double Phan { get; set; }
        public double Thuocbvtv { get; set; }
        public double Vacxingsgc { get; set; }
        public double Muoi { get; set; }
        public double Suate6t { get; set; }
        public double Duong { get; set; }
        public double Thocgao { get; set; }
        public double Thuocpcb { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
