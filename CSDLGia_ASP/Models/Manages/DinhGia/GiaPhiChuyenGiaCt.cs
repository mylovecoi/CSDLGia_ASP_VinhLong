using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaPhiChuyenGiaCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Maso { get; set; }
        public string Mucgia { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
