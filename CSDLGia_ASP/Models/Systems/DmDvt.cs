using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmDvt
    {
        [Key]
        public int Id { get; set; }
        public string Madvt { get; set; }
        public string Dvt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
