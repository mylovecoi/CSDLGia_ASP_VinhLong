using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThiTruongTt
    {
        [Key]
        public int Id { get; set; }
        public string Matt { get; set; }
        public string Ttqd { get; set; }
        public string Mota { get; set; }
        public string Ghichu { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
