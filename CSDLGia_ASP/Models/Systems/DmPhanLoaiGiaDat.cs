﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmPhanLoaiGiaDat
    {
        [Key]
        public int Id { get; set; }
        public string MaPhanLoaiGiaDat { get; set; }
        public string TenPhanLoaiGiaDat { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int STTSapxep { get; set; }
        public string STTHienthi { get; set; }
        public string Style { get; set; }
    }
}