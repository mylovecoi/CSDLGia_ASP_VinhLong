using CSDLGia_ASP.Database;
using System.Linq;
using CSDLGia_ASP.Models;

namespace CSDLGia_ASP.DAO
{
    public class HeThongDao
    {
        private readonly CSDLGiaDBContext _db;
        public HeThongDao(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public tblHeThong GetTblHeThong()
        {
            return _db.tblHeThong.FirstOrDefault();

        }
    }
}
