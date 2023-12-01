using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaHhDvkTh
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Matt { get; set; }
        public string Sobc { get; set; }
        public DateTime Ngaybc { get; set; }
        public DateTime Ngaychotbc { get; set; }
        public string Ttbc { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public string Phanloai { get; set; }
        public string Ghichu { get; set; }
        public string Congbo { get; set; }
        public string Trangthai { get; set; }
        public string Mahstonghop { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        //Index
        [NotMapped]
        public string Tentt { get; set; }
    }
}
