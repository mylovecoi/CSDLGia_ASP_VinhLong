using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDvGdDtCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maspdv { get; set; }
        public string Mota { get; set; }
        public string Namapdung1 { get; set; }
        public double Giathanhthi1 { get; set; }
        public double Gianongthon1 { get; set; }
        public double Giamiennui1 { get; set; }
        public string Namapdung2 { get; set; }
        public double Giathanhthi2 { get; set; }
        public double Gianongthon2 { get; set; }
        public double Giamiennui2 { get; set; }
        public string Namapdung3 { get; set; }
        public double Giathanhthi3 { get; set; }
        public double Gianongthon3 { get; set; }
        public double Giamiennui3 { get; set; }
        public string Namapdung4 { get; set; }
        public double Giathanhthi4 { get; set; }
        public double Gianongthon4 { get; set; }
        public double Giamiennui4 { get; set; }
        public string Namapdung5 { get; set; }
        public double Giathanhthi5 { get; set; }
        public double Gianongthon5 { get; set; }
        public double Giamiennui5 { get; set; }
        public string Gc { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
