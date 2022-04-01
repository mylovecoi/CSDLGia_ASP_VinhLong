using CSDLGia_ASP.Database;
using System.Collections.Generic;
using System.Linq;
using CSDLGia_ASP.Models;

namespace CSDLGia_ASP.DAO
{
    public class PhanQuyenDao
    {
        private readonly CSDLGiaDBContext _db;
        public PhanQuyenDao(CSDLGiaDBContext db)
        {
            _db = db;
        }
        public List<tblPhanQuyen> GetPhanQuyen(string tenDangNhap)
        {
            var phanQuyen = _db.tblPhanQuyen.Where(e => e.TenDangNhap == tenDangNhap);
            if (phanQuyen == null)
            {
                return null;
            }
            return phanQuyen.ToList();

        }
        public bool ChkPhanQuyen(string tenDangNhap, string maChucNang)
        {
            var phanQuyen = _db.tblPhanQuyen.Where(e => e.TenDangNhap == tenDangNhap && e.MaChucNang == maChucNang);
            if (phanQuyen == null)
            {
                return false;
            }
            return true;

        }
    }
}
