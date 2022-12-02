using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatPhanLoaiCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maloaidat { get; set; }
        public string Khuvuc { get; set; }
        public int Vitri { get; set; }
        public double Banggiadat { get; set; }
        public double Giacuthe { get; set; }
        public double Hesodc { get; set; }
        public double Sapxep { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public string Loaidat { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public double Dientich { get; set; }
    }
}
