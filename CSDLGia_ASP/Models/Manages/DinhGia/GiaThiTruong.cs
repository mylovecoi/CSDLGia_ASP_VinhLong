using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThiTruong
    {
        [Key]
        public int Id { get; set; }
        public string Thanglk { get; set; }
        public string Thang { get; set; }
        public string Namlk { get; set; }
        public string Nam { get; set; }
        public string Sobc { get; set; }
        public DateTime Ngaybc { get; set; }
        public string Mahuyen { get; set; }
        public string Trangthai { get; set; }
        public string Mahs { get; set; }
        public string Matt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
