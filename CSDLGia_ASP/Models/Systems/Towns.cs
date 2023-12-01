using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class Towns
    {
        [Key]
        public int Id { get; set; }
        public string Mahuyen { get; set; }
        public string Maxa { get; set; }
        public string Tendv { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Diachi { get; set; }
        public string Ttlienhe { get; set; }
        public string Emailql { get; set; }
        public string Emailqt { get; set; }
        public string Songaylv { get; set; }
        public string Tendvhienthi { get; set; }
        public string Tendvcqhienthi { get; set; }
        public string Chucvuky { get; set; }
        public string Chucvukythay { get; set; }
        public string Nguoiky { get; set; }
        public string Diadanh { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
