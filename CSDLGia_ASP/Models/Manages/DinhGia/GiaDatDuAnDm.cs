using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatDuAnDm
    {
        [Key]
        public int Id { get; set; }
        public string Manhomduan { get; set; }
        public string Tennhomduan { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
