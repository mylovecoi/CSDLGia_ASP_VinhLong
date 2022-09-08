using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.KeKhaiGia
{
    public class KkGiaDvLtCskd
    {
        [Key]
        public int Id { get; set; }
        public string Macskd { get; set; }
        public string Madv { get; set; }
        public string Mahuyen { get; set; }
        public string Tencskd { get; set; }
        public string Loaihang { get; set; }
        public string Diachikd { get; set; }
        public string Telkd { get; set; }
        public string Link { get; set; }
        public string Avatar { get; set; }
        [NotMapped]
        public string Avatarupload { get; set; }
        public string Town { get; set; }
        public string District { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        [NotMapped]
        public int Lankk { get; set; }
        [NotMapped]
        public string Kklc { get; set; }
    }
}
