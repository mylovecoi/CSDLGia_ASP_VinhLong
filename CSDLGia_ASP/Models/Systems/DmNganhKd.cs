using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmNganhKd
    {
        [Key]
        public int Id { get; set; }
        public string Manganh { get; set; }
        public string Tennganh { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
