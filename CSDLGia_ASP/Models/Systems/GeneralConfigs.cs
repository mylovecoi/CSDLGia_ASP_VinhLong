using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Systems
{
    public class GeneralConfigs
    {
        [Key]
        public int Id { get; set; }
        public string Tendonvi { get; set; }
        public string Maqhns { get; set; }
        public string Diachi { get; set; }
        public string Tel { get; set; }
        public string Thutruong { get; set; }
        public string Ketoan { get; set; }
        public string Nguoilapbieu { get; set; }
        public string Diadanh { get; set; }
        public string Setting { get; set; }
        public string Thongtinhd { get; set; }
        public double Thoihanlt { get; set; }
        public double Thoihanvt { get; set; }
        public double Thoihangs { get; set; }
        public double Thoihantacn { get; set; }
        public double Sodvvt { get; set; }
        public string Emailql { get; set; }
        public string Tendvhienthi { get; set; }
        public string Tendvcqhienthi { get; set; }
        public string Ipf1 { get; set; }
        public string Ipf2 { get; set; }
        public string Ipf3 { get; set; }
        public string Ipf4 { get; set; }
        public string Ipf5 { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public bool Sudungemail { get; set; }
    }
}
