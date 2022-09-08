using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueMatDatMatNuocCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Diemdau { get; set; }
        public string Diemcuoi { get; set; }
        public int Vitri { get; set; }
        public string Mota { get; set; }
        public double Dientich { get; set; }
        public double Dongia { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
