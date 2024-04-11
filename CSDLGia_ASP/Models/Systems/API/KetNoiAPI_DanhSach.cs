using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems.API
{
    public class KetNoiAPI_DanhSach
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }//Mã chức năng
        //Link Truyền dữ liệu
        public string LinkTruyenGet { get; set; }
        public string LinkTruyenPost { get; set; }
        public string LinkTruyenPut { get; set; }
        //Link Nhận dữ liệu
        public string LinkNhanGet { get; set; }
        public string LinkNhanPost { get; set; }
        public string LinkNhanPut { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
