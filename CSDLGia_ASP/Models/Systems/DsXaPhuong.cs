using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Systems
{
    public class DsXaPhuong
    {
        [Key]
        public int Id { get; set; }
        public string Maxp { get; set; }
        public string Tenxp { get; set; }
        public string Madiaban { get; set; }
        public string Level { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
