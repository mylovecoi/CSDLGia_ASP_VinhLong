using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaRungDmCt
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public int STTSapXep { get; set; }
        public string STTHienThi { get; set; }
        public string MoTa { get; set; }
        public string Style { get; set; }
    }
}
