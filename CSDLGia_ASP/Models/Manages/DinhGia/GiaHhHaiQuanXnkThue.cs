using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHhHaiQuanXnkThue
    {
        [Key]
        public int Id { get; set; }
        public string MaThue { get; set; }
        public string TenThue { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
