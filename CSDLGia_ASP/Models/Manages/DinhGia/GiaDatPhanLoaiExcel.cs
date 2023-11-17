using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatPhanLoaiExcel
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Vitri { get; set; }
        public string Maloaidat { get; set; }
        public string Soqd { get; set; }
        public string Dvt { get; set; }
        public double Dientich { get; set; }
        public double Giatri { get; set; }
        public string Congbo { get; set; }
        public string Thaotac { get; set; }
        public string Ghichu { get; set; }
        public string Lichsu { get; set; }
        public string Khuvuc { get; set; }
        public double Banggiadat { get; set; }
        public double Giacuthe { get; set; }
        public double Hesodc { get; set; }
        public double Sapxep { get; set; }
        public string Trangthai { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Macqcq { get; set; }
        public string Madv { get; set; }
        public string Lydo { get; set; }
        public string Thongtin { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }


    }
}
