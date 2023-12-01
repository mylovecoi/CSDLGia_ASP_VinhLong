using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.ThamDinhGia
{
    public class ThamDinhGiaDmHh
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Mahanghoa { get; set; }
        public string Tenhanghoa { get; set; }
        public string Thongsokt { get; set; }
        public string Xuatxu { get; set; }
        public string Dvt { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Tennhom { get; set; }
    }
}
