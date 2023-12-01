using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaCayTrongVatNuoiDm
    {
        [Key]
        public int Id { get; set; }
        public string Level { get; set; }
        public string Ten { get; set; }
        public string Manhom { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string Cap3 { get; set; }
        public string Cap4 { get; set; }
        public string Cap5 { get; set; }
        public string Dvt { get; set; }
        public string Sapxep { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public double dongialk { get; set; }
        [NotMapped]
        public double dongiabc { get; set; }

        //excel
        [NotMapped]
        public string Tennhom { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }
    }
}
