﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHhDvkNhom
    {
        [Key]
        public int Id { get; set; }
        public string Matt { get; set; }
        public string Tentt { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}