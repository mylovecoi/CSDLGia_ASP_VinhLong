using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsNhomTaiKhoan
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Mota { get; set; }
        public string Permission { get; set; }
        public bool Macdinh { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Chucnang { get; set; }
    }
}
