using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.KeKhaiGia
{
    public class KkGiaVlXdDm
    {
        [Key]
        public int id { get; set; }
        public string tennhom { get; set; }
        public string ten { get; set; }
        public string level { get; set; }
        public string theodoi { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
