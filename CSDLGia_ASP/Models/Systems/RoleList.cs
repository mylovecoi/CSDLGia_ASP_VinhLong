using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Systems
{
    public class RoleList
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string MaGoc { get; set; }
        public string TrangThai { get; set; }
        public string PhanLoai { get; set; }
        public int Level { get; set; }
        public int STTSapXep { get; set; }
    }
}
