using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaBanNhaTaiDinhCu
    {
        [Key]
        public int Id { get; set; }
        public string District { get; set; }
        public string Manhom { get; set; }
        public string Tenduan { get; set; }
        public string Mota { get; set; }
        public string Dientich { get; set; }
        public string Dvt { get; set; }
        public string Dongiathuemua { get; set; }
        public string Dongiaban { get; set; }
        public string Thoidiemkc { get; set; }
        public string Thoidiemht { get; set; }
        public string Ttqd { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
