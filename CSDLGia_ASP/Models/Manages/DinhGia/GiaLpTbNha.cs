using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaLpTbNha
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Soqd { get; set; }
        public DateTime Ngaybh { get; set; }
        public DateTime Ngayad { get; set; }
        public string Dvbh { get; set; }
        public string Ghichuxdm { get; set; }
        public string Ghichuclcl { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
