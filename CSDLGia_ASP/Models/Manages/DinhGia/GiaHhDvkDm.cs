using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHhDvkDm
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("NHOM_HHDV")]
        public string Manhom { get; set; }
        public string Matt { get; set; }
        [JsonProperty("MA_HHDV")]
        public string Mahhdv { get; set; }
        public string Tenhhdv { get; set; }
        [JsonProperty("DAC_DIEM_KY_THUAT")]
        public string Dacdiemkt { get; set; }
        [JsonProperty("DON_VI_TINH")]
        public string Dvt { get; set; }
        public string Xuatxu { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
