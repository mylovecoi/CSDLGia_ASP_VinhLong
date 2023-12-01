using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDvGdDtDm
    {
        [Key]
        public int Id { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string Phanloai { get; set; }
        public string Hientrang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
