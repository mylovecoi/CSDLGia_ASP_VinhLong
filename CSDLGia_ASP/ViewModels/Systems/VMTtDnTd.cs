using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.ViewModels.Systems
{
    public class VMTtDnTd
    {
        public int Id { get; set; }
        public string Madv { get; set; }
        public string Macqcq { get; set; }
        public string Madiaban { get; set; }
        public string Tendiaban { get; set; }
        public string Manghe { get; set; }
        public string Tendn { get; set; }
        public string Diachi { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Diadanh { get; set; }
        public string Chucdanh { get; set; }
        public string Nguoiky { get; set; }
        public string Noidknopthue { get; set; }
        public string Tailieu { get; set; }
        public string Giayphepkd { get; set; }
        [NotMapped]
        public IFormFile Giayphepkdupload { get; set; }
        public string Settingdvvt { get; set; }
        public double Vtxk { get; set; }
        public double Vtxb { get; set; }
        public double Vtxtx { get; set; }
        public double Vtch { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public string Level { get; set; }
        public string Lydo { get; set; }
        public DateTime Ngaychuyen { get; set; }
    }
}
