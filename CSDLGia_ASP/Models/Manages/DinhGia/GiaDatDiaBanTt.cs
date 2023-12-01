using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatDiaBanTt
    {
        [Key]
        public int Id { get; set; }
        public string Soqd { get; set; }
        public DateTime Ngayqd_banhanh { get; set; }
        public DateTime Ngayqd_apdung { get; set; }
        public string Mota { get; set; }
        public string Ipf1 { get; set; }
        [NotMapped]
        public IFormFile Ipf1upload { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
