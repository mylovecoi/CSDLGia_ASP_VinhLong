using CSDLGia_ASP.Models.Manages.DinhGia;
using System;
using System.Collections.Generic;

namespace CSDLGia_ASP.ViewModels.Manages.DinhGia
{
    public class VMDinhGiaPrint
    {
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Soqd { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Madiaban { get; set; }
        public string Ghichu { get; set; }
        public string Maxp { get; set; }
        public string Vitri { get; set; }
        public double Giatri { get; set; }
        public string Tendv { get; set; }
        public string Tendb { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Phanloaidv { get; set; }

        public List<GiaNuocShCt> GiaNuocShCt { get; set; }
        public List<GiaDatPhanLoaiCt> GiaDatPhanLoaiCt { get; set; }
        public List<GiaRungCt> GiaRungCt { get; set; }
        public List<GiaThueMatDatMatNuocCt> GiaThueMatDatMatNuocCt { get; set; }
        public List<GiaThueMuaNhaXhCt> GiaThueMuaNhaXhCt { get; set; }
        public List<GiaSpDvCuTheCt> GiaSpDvCuTheCt { get; set; }
        public List<GiaSpDvKhungGiaCt> GiaSpDvKhungGiaCt { get; set; }
        public List<GiaSpDvToiDaCt> GiaSpDvToiDaCt { get; set; }
        public List<GiaTaiSanTthsCt> GiaTaiSanTthsCt { get; set; }
    }
}
