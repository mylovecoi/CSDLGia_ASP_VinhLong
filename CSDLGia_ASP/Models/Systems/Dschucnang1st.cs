using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Systems
{
    public class Dschucnang1st
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Chucnang { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
