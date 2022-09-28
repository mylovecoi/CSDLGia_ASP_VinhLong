using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaPhiLePhiCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Phanloai { get; set; }
        public string Ptcp { get; set; }
        public string Dvt { get; set; }
        public string Phantram { get; set; }
        public double Mucthutu { get; set; }
        public double Mucthuden { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}
