﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Systems.KetNoiGiaDichVu
{
    public class HoSoKeKhaiGia_ChiTiet
    {
        [Key]
        public int id { get; set; }
        public string macskd { get; set; }
        public string mahs { get; set; }
        public string maloaip { get; set; }
        public string loaip { get; set; }
        public string qccl { get; set; }
        public string sohieu { get; set; }
        public string ghichu { get; set; }
        public string mucgialk { get; set; }
        public string mucgiakk { get; set; }
        public string tendoituong { get; set; }
        public string apdungpublic { get; set; }
        //public DateTime Created_at { get; set; }
        //public DateTime Updated_at { get; set; }
    }
}