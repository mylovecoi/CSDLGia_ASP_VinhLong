using CSDLGia_ASP.Models.Manages.DinhGia;
using System;
using System.Collections.Generic;

namespace CSDLGia_ASP.ViewModels.Manages.DinhGia
{
    public class VMDinhGiaThueMuaNhaXhSearch
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Madiaban { get; set; }
        public string Diemdau { get; set; }
        public string Diemcuoi { get; set; }
        public int Vitri { get; set; }
        public string Mota { get; set; }
        public double Dongia { get; set; }
        public DateTime Thoidiem { get; set; }
        public double Giatri { get; set; }
        public string Macqcq { get; set; }
        public string Maso { get; set; }
        public string Dvt { get; set; }
        public string Phanloai { get; set; }
        public string Tenvitri { get; set; }
        public double Dientichsd { get; set; }
        public string Tennha { get; set; }
        //public IEnumerable<GiaThueMuaNhaXhCt> GiaThueMuaNhaXhCt { get; set; }
        public List<GiaThueMuaNhaXhCt> GiaThueMuaNhaXhCt { get; set; }
        public string Dvthue { get; set; }
    }
}
