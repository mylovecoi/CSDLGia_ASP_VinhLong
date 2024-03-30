using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaVatLieuXayDungDm
    {
        [Key]
        public int Id { get; set; }
        public string Mavlxd { get; set; }
        public string Tenvlxd { get; set; }
        public string Dvt { get; set; }
        public string Tieuchuan { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
