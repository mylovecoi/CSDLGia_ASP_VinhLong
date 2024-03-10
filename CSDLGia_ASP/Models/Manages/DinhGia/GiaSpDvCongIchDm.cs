using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvCongIchDm
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string Mota { get; set; }
        public string Cap1 { get; set; }
        public string Cap2 { get; set; }
        public string Cap3 { get; set; }
        public string Cap4 { get; set; }

        public string Maso { get; set; }
        public string Ten { get; set; }
        public string Magoc { get; set; }
        public string Capdo { get; set; }
        public string HienThi { get; set; }
        

        public double Mucgiatu { get; set; }
        public double Mucgiaden { get; set; }
        public string Dvt { get; set; }
        public string Phanloai { get; set; }
        public string Hientrang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
