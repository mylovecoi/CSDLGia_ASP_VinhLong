using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class Supports
    {
        [Key] 
        public int Id { get; set; }
        public string Hoten { get; set; }
        public string Sdt { get; set; }
        public string Phanloai { get; set; }
    }
}
