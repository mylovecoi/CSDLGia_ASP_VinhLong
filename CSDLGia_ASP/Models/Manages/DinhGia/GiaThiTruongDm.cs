using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThiTruongDm
    {
        [Key]
        public int Id { get; set; }
        public string Matt { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Mahh { get; set; }
        public string Tenhh { get; set; }
        public string Dacdiemkt { get; set; }
        public string Dvt { get; set; }
        public string Mahuyen { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
