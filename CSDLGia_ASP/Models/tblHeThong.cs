namespace CSDLGia_ASP.Models
{
    public class tblHeThong
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public string DiaDanh { get; set; }
        public string TenDonVi { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string SoFax { get; set; }
        public string Email { get; set; }
    }
}
