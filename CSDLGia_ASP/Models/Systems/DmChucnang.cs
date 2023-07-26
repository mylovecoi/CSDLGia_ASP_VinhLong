using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmChucnang
    {
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Capdo { get; set; }
        public string Maso_goc { get; set; }
        public string Menu { get; set; }
        public string Mota { get; set; }
        public string Api { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
