using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.ThamDinhGia
{
    public class ThamDinhGiaDvLichSu
    {
        [Key]
        public int Id { get; set; }
        public int IdDV { get; set; } //do trường Maso của đơn vị trùng nhau nên lấy IdDV để rằng với bảng "ThamDinhGiaDv"   
        public string Maso { get; set; }       
        public string SoQD { get; set; }
        public DateTime NgayQD { get; set; }
        public string FileQD { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public IFormFile FileQDUpLoad { get; set; }
    }
}
