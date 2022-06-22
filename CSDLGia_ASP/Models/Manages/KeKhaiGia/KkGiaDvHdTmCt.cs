using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.KeKhaiGia
{
    public class KkGiaDvHdTmCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Tendvcu { get; set; }
        public string Qccl { get; set; }
        public string Dvt { get; set; }
        public double Gialk { get; set; }
        public double Giakk { get; set; }
        public string Ghichu { get; set; }
        public string Thuevat { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
