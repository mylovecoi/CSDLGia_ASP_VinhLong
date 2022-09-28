using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.ThamDinhGia
{
    public class ThamDinhGiaDmHhExcel
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Mahanghoa { get; set; }
        public string Tenhanghoa { get; set; }
        public string Thongsokt { get; set; }
        public string Xuatxu { get; set; }
        public string Dvt { get; set; }
    }
}
