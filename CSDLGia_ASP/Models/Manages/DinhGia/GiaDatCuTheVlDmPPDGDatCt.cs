using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatCuTheVlDmPPDGDatCt
    {
        [Key]
        public int Id { get; set; }
        public string Mapp { get; set; }
        public double SapXep { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }
        public string Noidungcv { get; set; }
        public bool NhapGia { get; set; } = false;
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
