using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Temp.TempSystems
{
    public class DsDonViTdg
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Tendv { get; set; }
        public string Diachi { get; set; }
        public string Nguoidaidien { get; set; }
        public string Chucvu { get; set; }
        public string Sothe { get; set; }
        public DateTime Ngaycap { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
