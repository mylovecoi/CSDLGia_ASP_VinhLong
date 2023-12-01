using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsVanPhong
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Vanphong { get; set; }
        public string Hoten { get; set; }
        public string Chucvu { get; set; }
        public string Sdt { get; set; }
        public string Skype { get; set; }
        public string Facebook { get; set; }
        public int Stt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
