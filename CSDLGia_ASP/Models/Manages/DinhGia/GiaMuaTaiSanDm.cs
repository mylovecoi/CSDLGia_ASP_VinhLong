using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaMuaTaiSanDm
    {
        [Key]
        public int Id { get; set; }
        public string Phanloai { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Dvt { get; set; }
        public string Stt { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}
