using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class Districts
    {
        [Key]
        public int Id { get; set; }
        public string Mahuyen { get; set; }
        public string Tendv { get; set; }
        public string District { get; set; }
        public string Diachi { get; set; }
        public string Phanloaiql { get; set; }
        public string Ttlienhe { get; set; }
        public string Emailql { get; set; }
        public string Emailqt { get; set; }
        public string Tendvhienthi { get; set; }
        public string Tendvcqhienthi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
