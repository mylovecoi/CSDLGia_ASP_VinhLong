using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHhHaiQuanXnkCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string TenHh { get; set; }
        public string Dvt { get; set; }
        public double GiaTruocThue { get; set; }
        public double PhanTramThue { get; set; }
        public double GiaSauThue { get; set; }
        public string MaThue { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
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
        public string TenThue { get; set; }
    }
}
