using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueTaiNguyenDm
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
    }
}
