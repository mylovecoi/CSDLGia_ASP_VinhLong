using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmSieuThi
    {
        [Key]
        public int Id { get; set; }
        public string masieuthi { get; set; }
        public string tensieuthi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
