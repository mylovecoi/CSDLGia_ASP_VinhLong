using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueMatDatMatNuocDm
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Magoc { get; set; }
        public string Capdo { get; set; }
        public string Maloaidat { get; set; }
        public bool NhapGia { get; set; } = false;

        public string Manhom { get; set; }
        public double SapXep { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }
        public string Loaidat { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
