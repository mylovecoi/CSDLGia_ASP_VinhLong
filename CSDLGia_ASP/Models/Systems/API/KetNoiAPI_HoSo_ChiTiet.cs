using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems.API
{
    public class KetNoiAPI_HoSo_ChiTiet
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }//Mã chức năng
        public string Tendong_goc { get; set; }//Mã chức năng
        public string Tentruong { get; set; }
        public string Tendong { get; set; }
        public string Mota { get; set; }
        public string Kieudulieu { get; set; }
        public string Dinhdang { get; set; }
        public string Dodai { get; set; }
        public bool Batbuoc { get; set; }
        public string Macdinh { get; set; }
        public int Stt { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
