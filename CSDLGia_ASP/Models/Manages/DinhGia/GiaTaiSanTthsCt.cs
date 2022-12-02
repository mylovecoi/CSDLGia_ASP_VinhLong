using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaTaiSanTthsCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Mataisan { get; set; }
        public string Tentaisan { get; set; }
        public string TaisanTd { get; set; }
        public string DacdiemKt { get; set; }
        public string Tinhtrang { get; set; }
        public string Dactinh { get; set; }
        public double Giabanbuon { get; set; }
        public double Giabanle { get; set; }
        public double Phantram { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Loaigia { get; set; }
    }
}
