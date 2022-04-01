using CSDLGia_ASP.Database;
using System.Linq;
using CSDLGia_ASP.Models;

namespace CSDLGia_ASP.DAO
{
    public class DSTaiKhoanDao
    {
        private readonly CSDLGiaDBContext _db;
        public DSTaiKhoanDao(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public tblDSTaiKhoan chkTaiKhoan(string tenDangNhap)
        {
            return _db.tblDSTaiKhoan.FirstOrDefault(e => e.TenDangNhap == tenDangNhap);
        }

        public viewDonVi_TaiKhoan GetTaiKhoan(string tenDangNhap)
        {
            return _db.viewDonVi_TaiKhoan.FirstOrDefault(e => e.TenDangNhap == tenDangNhap);
        }
    }
}

