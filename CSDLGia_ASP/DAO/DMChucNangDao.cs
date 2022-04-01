using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.DAO
{
    public class DMChucNangDao
    {
        private readonly CSDLGiaDBContext _db;
        public DMChucNangDao(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public List<tblDMChucNang> ChucNang()
        {
            return _db.tblDMChucNang.ToList();
            //var kq = _db.tblDMChucNang.Where(h => h.CapDo > 0);
            //if (kq.Count() > 0)
            //{
            //    return kq.ToList();
            //}
            //else
            //{
            //    return null;
            //}

        }
        public tblDMChucNang GetChucNang(string maChucNang)
        {
            return _db.tblDMChucNang.FirstOrDefault(h => h.MaChucNang == maChucNang);
            //return _db.tblDMChucNang.Where(h=>h.TenChucNang == tenChucNang);
        }
    }
}
