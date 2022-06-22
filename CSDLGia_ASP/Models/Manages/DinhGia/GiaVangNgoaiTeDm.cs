using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaVangNgoaiTeDm
    {
        [Key]
        public int Id { get; set; }
        public string Mahhdv { get; set; }
        public string Tenhhdv { get; set; }
        public string Dacdiemkt { get; set; }
        public string Xuatxu { get; set; }
        public string Dvt { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
