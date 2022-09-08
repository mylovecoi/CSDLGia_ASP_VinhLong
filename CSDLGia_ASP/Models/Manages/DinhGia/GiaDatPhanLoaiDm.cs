using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatPhanLoaiDm
    {
        [Key]
        public int Id { get; set; }
        public string Loaidat { get; set; }
        public string Mavitri { get; set; }
        public string Tenvitri { get; set; }
        public string Dientich { get; set; }
        public string Mota { get; set; }
        public double Giatri { get; set; }
        public string Mahuyen { get; set; }
        public string Maxa { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Maloaidat { get; set; }
    }
}
