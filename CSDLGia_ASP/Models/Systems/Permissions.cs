using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class Permissions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        public string Tendangnhap { get; set; }
        public string Madv { get; set; }

        [Required]
        public string Roles { get; set; }
        public string Name { get; set; }

        public bool Index { get; set; }

        public bool Create { get; set; }

        public bool Edit { get; set; }

        public bool Delete { get; set; }

        public bool Approve { get; set; }
        public bool Public { get; set; }

        public string Status { get; set; }
    }
}
