using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaCuocVanChuyenCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Tencuoc { get; set; }
        public double Tukm { get; set; }
        public double Denkm { get; set; }
        public string Bachh { get; set; }
        public string Phanloai { get; set; }
        public double Giavc1 { get; set; }
        public double Giavc2 { get; set; }
        public double Giavc3 { get; set; }
        public double Giavc4 { get; set; }
        public double Giavc5 { get; set; }
        public string Gc { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
