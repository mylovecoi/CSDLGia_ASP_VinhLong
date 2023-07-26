using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Systems.API
{
    public class KetNoiAPI_HoSo
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }//Mã chức năng
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

        [NotMapped]
        public List<KetNoiAPI_HoSo_ChiTiet> KetNoiAPI_HoSo_ChiTiet { get; set; }
    }
}
