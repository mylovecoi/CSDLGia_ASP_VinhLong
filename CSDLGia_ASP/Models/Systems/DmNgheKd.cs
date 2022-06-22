using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmNgheKd
    {
        [Key]
        public int Id { get; set; }
        public string Manganh { get; set; }
        public string Manghe { get; set; }
        public string Tennghe { get; set; }
        public string Madv { get; set; }
        public string Theodoi { get; set; }
        public string Phanloai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
