using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSDLGia_ASP.Models.Manages.ChiSoGiaTd;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd
{
    public class VMChiSoGiaTdDd
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Masohanghoa { get; set; }
        public string Masonhomhanghoa { get; set; }
        public string Tenhanghoa { get; set; }
        public string Masogoc { get; set; }
        public int Stt { get; set; }
        public int SttBc { get; set; }
        public string Dvt { get; set; }
        public string Baocao { get; set; }
        public double Giagoc { get; set; }
        public double Giakychon { get; set; }
        public double QuyensoTt { get; set; }
        public double QuyensoNt { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string Trangthai { get; set; }
        [Display(Name = "Tags")]
        public IEnumerable<string> SelectedTags { get; set; }
        public IEnumerable<SelectListItem> TagsList { get; set; }
    }
}
