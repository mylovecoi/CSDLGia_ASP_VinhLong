using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaPhiChuyenGiaDm
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Tenphi { get; set; }
        public string Tengia { get; set; }
        public string Dvt { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Manhom { get; set; }
    }
}
