using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;

namespace CSDLGia_ASP.Controllers.Admin.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetDataKeKhaiGiaController : ControllerBase
    {
        private readonly CSDLGiaDBContext _db;
        public GetDataKeKhaiGiaController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        #region XMTXD
        [Route("BaoCaoKkGiaXmTxd/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaXmTxdTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "XMTXD" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaXmTxd/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaXmTxdChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
                var model = (from kk in _db.KkGia.Where(t => t.Manghe == "XMTXD" && t.Trangthai == "DD")
                             join com in _db.Company on kk.Madv equals com.Madv
                             select new VMKkGia
                             {
                                 Id = kk.Id,
                                 Mahs = kk.Mahs,
                                 Ngaynhap = kk.Ngaynhap,
                                 Ngayhieuluc = kk.Ngayhieuluc,
                                 Manghe = kk.Manghe,
                                 Socv = kk.Socv,
                                 Socvlk = kk.Socvlk,
                                 Ngaycvlk = kk.Ngaycvlk,
                                 Ytcauthanhgia = kk.Ytcauthanhgia,
                                 Thydggadgia = kk.Thydggadgia,
                                 Ttnguoinop = kk.Ttnguoinop,
                                 Dtll = kk.Dtll,
                                 Sohsnhan = kk.Sohsnhan,
                                 Ngaychuyen = kk.Ngaychuyen,
                                 Ngaynhan = kk.Ngaynhan,
                                 Trangthai = kk.Trangthai,
                                 Madv = com.Madv,
                                 Tendn = com.Tendn,
                             });

                if (phanloai == "ngaychuyen")
                {
                    if (time == "ngay")
                    {
                        model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                    }
                    else if (time == "thang")
                    {
                        model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                    }
                    else
                    {
                        if (quy == "1")
                        {
                            tungay = new DateTime(int.Parse(nam), 1, 1);
                            denngay = new DateTime(int.Parse(nam), 3, 31);
                        }
                        else if (quy == "2")
                        {
                            tungay = new DateTime(int.Parse(nam), 4, 1);
                            denngay = new DateTime(int.Parse(nam), 6, 30);
                        }
                        else if (quy == "3")
                        {
                            tungay = new DateTime(int.Parse(nam), 7, 1);
                            denngay = new DateTime(int.Parse(nam), 9, 30);
                        }
                        else
                        {
                            tungay = new DateTime(int.Parse(nam), 10, 1);
                            denngay = new DateTime(int.Parse(nam), 12, 31);
                        }

                        model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                    }
                }
                else
                {
                    if (time == "ngay")
                    {
                        model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                    }
                    else if (time == "thang")
                    {
                        model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                    }
                    else
                    {
                        if (quy == "1")
                        {
                            tungay = new DateTime(int.Parse(nam), 1, 1);
                            denngay = new DateTime(int.Parse(nam), 3, 31);
                        }
                        else if (quy == "2")
                        {
                            tungay = new DateTime(int.Parse(nam), 4, 1);
                            denngay = new DateTime(int.Parse(nam), 6, 30);
                        }
                        else if (quy == "3")
                        {
                            tungay = new DateTime(int.Parse(nam), 7, 1);
                            denngay = new DateTime(int.Parse(nam), 9, 30);
                        }
                        else
                        {
                            tungay = new DateTime(int.Parse(nam), 10, 1);
                            denngay = new DateTime(int.Parse(nam), 12, 31);
                        }

                        model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                    }
                }
            return await model.ToListAsync();
        }
        #endregion

        #region GiaSach
        [Route("BaoCaoKkGiaSach/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaSachTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "SACH" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaSach/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaSachChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "SACH" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region ETANOL
        [Route("BaoCaoKkGiaEtanol/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaEtanolTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "ETANOL" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaEtanol/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaEtanolChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "ETANOL" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region TPCNTE6T
        [Route("BaoCaoKkGiaTpcn/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaTpcnTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "TPCNTE6T" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaTpcn/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaTpcnChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "TPCNTE6T" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region CATSAN
        [Route("BaoCaoKkGiaCatSan/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaCatSanTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "CATSAN" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaCatSan/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaCatSanChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "CATSAN" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region HOCPHILX
        [Route("BaoCaoKkGiaHplx/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaHplxTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "HOCPHILX" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaHplx/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaHplxChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "HOCPHILX" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region THAN
        [Route("BaoCaoKkGiaThan/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaThanTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "THAN" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaThan/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaThanChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "THAN" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region GIAY
        [Route("BaoCaoKkGiaGiay/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaGiayTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "GIAY" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaGiay/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaGiayChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "GIAY" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region TACN
        [Route("BaoCaoKkGiaTaCn/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaTaCnTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "TACN" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaTaCn/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaTaCnChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "TACN" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region DATSANLAP
        [Route("BaoCaoKkGiaDatSanLap/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaDatSanLapTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "DATSANLAP" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaDatSanLap/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaDatSanLapChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "DATSANLAP" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region DAXAYDUNG
        [Route("BaoCaoKkGiaDaXayDung/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaDaXayDungTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "DAXAYDUNG" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaDaXayDung/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaDaXayDungChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "DAXAYDUNG" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region VTXB
        [Route("BaoCaoKkGiaVtXb/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaVtXbTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXB" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaVtXb/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaVtXbChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXB" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region VTXK
        [Route("BaoCaoKkGiaVtXk/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaVtXkTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXK" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaVtXk/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaVtXkChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXK" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region VTXTX
        [Route("BaoCaoKkGiaVtXtx/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaVtXtxTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXTX" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaVtXtx/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaVtXtxChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXTX" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region VCHK
        [Route("BaoCaoKkCuocVcHk/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> CuocVcHkTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VCHK" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkCuocVcHk/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> CuocVcHkChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VCHK" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region CAHUE
        [Route("BaoCaoKkGiaDvCh/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaDvChTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "CAHUE" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaDvCh/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaDvChChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "CAHUE" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion

        #region SIEUTHI
        [Route("BaoCaoKkGiaSieuThi/BcTongHop")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaSieuThiTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "SIEUTHI" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngaychuyen")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }

        [Route("BaoCaoKkGiaSieuThi/BcChiTiet")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<VMKkGia>>> GiaSieuThiChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model = (from kk in _db.KkGia.Where(t => t.Manghe == "SIEUTHI" && t.Trangthai == "DD")
                         join com in _db.Company on kk.Madv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.Ngaynhap,
                             Ngayhieuluc = kk.Ngayhieuluc,
                             Manghe = kk.Manghe,
                             Socv = kk.Socv,
                             Socvlk = kk.Socvlk,
                             Ngaycvlk = kk.Ngaycvlk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.Ttnguoinop,
                             Dtll = kk.Dtll,
                             Sohsnhan = kk.Sohsnhan,
                             Ngaychuyen = kk.Ngaychuyen,
                             Ngaynhan = kk.Ngaynhan,
                             Trangthai = kk.Trangthai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
                else if (time == "thang")
                {
                    model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                }
            }
            return await model.ToListAsync();
        }
        #endregion
    }
}
