using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueMatDatMatNuocCt
    {
        [Key]
        public int Id { get; set; }
        public string PhanLoaiDatNuoc { get; set; }
        public string Mahs { get; set; }
        public string Diemdau { get; set; }
        public string Diemcuoi { get; set; }
        public string Vitri { get; set; }
        public string Mota { get; set; }
        public double Dientich { get; set; }
        public string Trangthai { get; set; }
        public double Dongia1 { get; set; }
        public double Dongia2 { get; set; }
        public double Dongia3 { get; set; }
        public double Dongia4 { get; set; }
        public double Dongia5 { get; set; }


        public double SapXep { get; set; }
        public string HienThi { get; set; }
        public string Style { get; set; }
        public string LoaiDat { get; set; }
        public string MaNhom { get; set; }

        public string TyLe1 { get; set; }
        public string TyLe2 { get; set; }
        public string TyLe3 { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Madv { get; set; }
    }
}
