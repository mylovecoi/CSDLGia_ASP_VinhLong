using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdHh
    {
        [Key]
        public int Id { get; set; }
        public string Matt { get; set; }
        public string Masonhomhanghoa { get; set; }
        public string Masohanghoa { get; set; }
        public string Tenhanghoa { get; set; }
        public string Masogoc { get; set; }
        public string Dvt { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        //public double Giagoc { get; set; }
        public double Gia { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
