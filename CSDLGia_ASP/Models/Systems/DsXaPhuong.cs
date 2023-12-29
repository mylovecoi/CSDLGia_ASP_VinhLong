using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsXaPhuong
    {
        [Key]
        public int Id { get; set; }
        public string Maxp { get; set; }
        public string Tenxp { get; set; }
        public string Madiaban { get; set; }
        public string Level { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
