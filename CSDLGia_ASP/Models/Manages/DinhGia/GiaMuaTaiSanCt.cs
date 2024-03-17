using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaMuaTaiSanCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Mota { get; set; }//Nội dung công việc
        public string Dvt { get; set; }
        public double KhoiLuong { get; set; }
        public double DonGia { get; set; }
        public double SapXep { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }
        [NotMapped]
        public double ThanhTien {get; set; }

        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
