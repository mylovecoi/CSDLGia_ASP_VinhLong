using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaNuocShCt
    {
        [Key]
        public int Id { get; set; }
        public string Madoituong { get; set; }
        public string Doituongsd { get; set; }
        public string Thuevat { get; set; }
        public string Giacothue { get; set; }
        public string Phibvmttyle { get; set; }
        public string Phibvmt { get; set; }
        public string Thanhtien { get; set; }
        public string Mahs { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Namchuathue { get; set; }
        public double Giachuathue { get; set; }
        public string Namchuathue1 { get; set; }
        public double Giachuathue1 { get; set; }
        public string Namchuathue2 { get; set; }
        public double Giachuathue2 { get; set; }
        public string Namchuathue3 { get; set; }
        public double Giachuathue3 { get; set; }
        public string Namchuathue4 { get; set; }
        public double Giachuathue4 { get; set; }
    }
}
