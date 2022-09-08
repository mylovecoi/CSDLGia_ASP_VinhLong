using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Temp.TempSystems
{
    public class DiaBanHd
    {
        [Key]
        public int Id { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public string Diaban { get; set; }
        public string Level { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
