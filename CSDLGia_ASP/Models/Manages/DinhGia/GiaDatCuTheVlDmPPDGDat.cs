using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatCuTheVlDmPPDGDat
    {
        [Key]
        public int Id { get; set; }
        public string Mapp { get; set; }
        public string Tenpp { get; set; }
        public string Mota { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
