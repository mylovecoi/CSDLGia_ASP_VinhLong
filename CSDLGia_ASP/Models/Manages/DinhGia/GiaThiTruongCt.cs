using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThiTruongCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Mahh { get; set; }
        public string Tenhh { get; set; }
        public string Dacdiemkt { get; set; }
        public string Dvt { get; set; }
        public string Loaigia { get; set; }
        public double Dongia { get; set; }
        public string Nguontt { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
