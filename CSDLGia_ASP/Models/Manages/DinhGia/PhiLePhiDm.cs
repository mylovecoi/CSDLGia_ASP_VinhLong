using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class PhiLePhiDm
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string Dvt { get; set; }
        public string Mota { get; set; }       
        public string HienTrang { get; set; }       
        
        public string Maso { get; set; }        
        public string Magoc { get; set; }
        public string Capdo { get; set; }
        public double SapXep { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
