using CSDLGia_ASP.Models.Systems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.ViewModels.Systems
{
    public class VMGroupPermissions
    {
        public int Id { get; set; }
        public string KeyLink { get; set; }
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string TenNhomQ { get; set; }
        public string ChucNang { get; set; }
        public List<Permissions> Permissions { get; set; }
    }
}
