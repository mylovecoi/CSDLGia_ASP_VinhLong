using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class GroupPermissions
    {
        [Key]
        public int Id { get; set; }
        public string KeyLink { get; set; }
        public string TenNhomQ { get; set; }
        public string ChucNang { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}
