using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaTaiSanCongCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Mataisan { get; set; }
        public string Tentaisan { get; set; }
        public string Dacdiem { get; set; }
        public double Giathue { get; set; }
        public double Giaban { get; set; }
        public double Giapheduyet { get; set; }
        public double Giaconlai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
