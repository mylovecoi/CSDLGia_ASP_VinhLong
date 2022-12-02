using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdDm
    {
        [Key]
        public int Id { get; set; }
        public string Matt { get; set; }
        public string Masonhomhanghoa { get; set; }
        public string Masohanghoa { get; set; }
        public string Tenhanghoa { get; set; }
        public string Masogoc { get; set; }
        public string Baocao { get; set; }
        public string Diaphuong { get; set; }
        public double QuyensoTt { get; set; }
        public double QuyensoNt { get; set; }
        public double Gia { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
