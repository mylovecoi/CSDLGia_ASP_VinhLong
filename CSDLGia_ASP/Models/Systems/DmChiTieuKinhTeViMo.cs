using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmChiTieuKinhTeViMo
    {
        [Key]
        public int Id { get; set; }
        public string machitieu { get; set; }
        public string tenchitieu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

    }
}
