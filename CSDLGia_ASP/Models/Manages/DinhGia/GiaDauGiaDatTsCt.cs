using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDauGiaDatTsCt
    {
        [Key]
        public int Id { get; set; }
        public string Loaidat { get; set; }
        public string Tenduong { get; set; }
        public string Loaiduong { get; set; }
        public string Vitri { get; set; }
        public string Qdgiadato { get; set; }
        public string Qdgiadattmdv { get; set; }
        public string Qdgiadatsxkd { get; set; }
        public string Qdgiadatnn { get; set; }
        public string Qdgiadatnuoits { get; set; }
        public string Qdgiadatmuoi { get; set; }
        public string Qdpddato { get; set; }
        public string Qdpddattmdv { get; set; }
        public string Qdpddatsxkd { get; set; }
        public string Qdpddatnn { get; set; }
        public string Qdpddatnuoits { get; set; }
        public string Qdpddatmuoi { get; set; }
        public string Qdpdgiatstd { get; set; }
        public string Kqgiadaugiadat { get; set; }
        public string Kqgiadaugiats { get; set; }
        public string Kqgiadaugiadatts { get; set; }
        public string Ghichu { get; set; }
        public string Dientichdat { get; set; }
        public string Dientichsanxd { get; set; }
        public string Mahs { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
