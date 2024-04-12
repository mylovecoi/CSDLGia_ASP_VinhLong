using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Systems;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Services
{
    public interface IDsDiaBanService
    {
        List<DsDiaBan> GetListDsDiaBan(string MaDiaBan);
        List<DsDiaBan> GetListDsDiaBanCapDuoi(string MaDiaBan);
    }
    public class DsDiaBanService : IDsDiaBanService
    {
        private readonly CSDLGiaDBContext _db;

        public DsDiaBanService(CSDLGiaDBContext db)
        {
            _db = db;
        }      

        public List<DsDiaBan> GetListDsDiaBan(string MaDiaBan)
        {
            List<DsDiaBan> list = new List<DsDiaBan>();
            if (!string.IsNullOrEmpty(MaDiaBan))
            {
                var model = _db.DsDiaBan.FirstOrDefault(t => t.MaDiaBan == MaDiaBan);
                if (model != null)
                {
                    list.Add(new DsDiaBan
                    {
                        MaDiaBan = model.MaDiaBan,
                        TenDiaBan = model.TenDiaBan,
                        Level = model.Level,
                    });
                    Recursive(list, model.MaDiaBan);
                }
            }
            return list;
        }

       

        private void Recursive(List<DsDiaBan> list, string MaDiaBan)
        {
            var childDiaBans = _db.DsDiaBan.Where(t => t.MaDiaBanCq == MaDiaBan).ToList();
            foreach (var childDiaBan in childDiaBans)
            {
                list.Add(new DsDiaBan
                {
                    MaDiaBan = childDiaBan.MaDiaBan,
                    TenDiaBan = childDiaBan.TenDiaBan,
                    Level = childDiaBan.Level,
                });
                Recursive(list, childDiaBan.MaDiaBan);
            }
        }

        public List<DsDiaBan> GetListDsDiaBanCapDuoi(string MaDiaBan)
        {
            List<DsDiaBan> list = new List<DsDiaBan>();
            if (!string.IsNullOrEmpty(MaDiaBan))
            {
                var model = _db.DsDiaBan.FirstOrDefault(t => t.MaDiaBan == MaDiaBan);
                if (model != null)
                {                   
                    Recursive(list, model.MaDiaBan);
                }
            }
            return list;
        }
    }
}
