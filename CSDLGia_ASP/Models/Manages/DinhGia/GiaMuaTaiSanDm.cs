using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaMuaTaiSanDm
    {
        [Key]
        public int Id { get; set; }
        public string Mota { get; set; }//Nội dung công việc
        public string Dvt { get; set; }
        public double KhoiLuong { get; set; }
        public double SapXep { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}
