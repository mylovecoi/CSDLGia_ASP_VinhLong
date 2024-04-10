using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsDiaBan
    {
        [Key]
        public int Id { get; set; }
        public string MaDiaBan { get; set; }
        public string MaDiaBanCq { get; set; }
        [Required]
        public string TenDiaBan { get; set; }
        public string Level { get; set; }
        public string GhiChu { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }

    }
}
