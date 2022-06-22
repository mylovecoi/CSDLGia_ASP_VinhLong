using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDkg
{
    public class KkDkgCt
    {
        [Key]
        public int Id { get; set; }
        public string Maxa { get; set; }
        public string Mahuyen { get; set; }
        public string Mahs { get; set; }
        public string Tenhh { get; set; }
        public string Quycach { get; set; }
        public string Dvt { get; set; }
        public string Plhh { get; set; }
        public string Gialk { get; set; }
        public string Giakk { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public string Nkdonvisxkd { get; set; }
        public string Nkqcpc { get; set; }
        public string Nksanluongdvt { get; set; }
        public double Nksanluongtt { get; set; }
        public string Nksanluonggc { get; set; }
        public string Nkgiamuackdvt { get; set; }
        public double Nkgiamuacktt { get; set; }
        public string Nkgiamuackgc { get; set; }
        public string Nkthuedvt { get; set; }
        public double Nkthuett { get; set; }
        public string Nkthueghichu { get; set; }
        public string Nkthuettdbdvt { get; set; }
        public double Nkthuettdbtt { get; set; }
        public string Nkthuettdbgc { get; set; }
        public string Nkthuephikdvt { get; set; }
        public double Nkthuephiktt { get; set; }
        public string Nkthuephikgc { get; set; }
        public string Nktienkdvt { get; set; }
        public double Nktienktt { get; set; }
        public string Nktienkgc { get; set; }
        public string Nkchiphitcdvt { get; set; }
        public double Nkchiphitctt { get; set; }
        public string Nkchiphitcgc { get; set; }
        public string Nkchiphibhdvt { get; set; }
        public double Nkchiphibhtt { get; set; }
        public string Nkchiphibhgc { get; set; }
        public string Nkchiphiqldvt { get; set; }
        public double Nkchiphiqltt { get; set; }
        public string Nkchiphiqlgc { get; set; }
        public string Nkgiathanh1spdvt { get; set; }
        public double Nkgiathanh1sptt { get; set; }
        public string Nkgiathanh1spgc { get; set; }
        public string Nkloinhuandkdvt { get; set; }
        public double Nkloinhuandktt { get; set; }
        public string Nkloinhuandkgc { get; set; }
        public string Nkthuegtgtkdvt { get; set; }
        public double Nkthuegtgtktt { get; set; }
        public string Nkthuegtgtkgc { get; set; }
        public string Nkgiabandkdvt { get; set; }
        public double Nkgiabandktt { get; set; }
        public string Nkgiabandkgc { get; set; }
        public string Nkgtgiamuack { get; set; }
        public string Nkgtthuenk { get; set; }
        public string Nkgtthuettdb { get; set; }
        public string Nkgtthuephik { get; set; }
        public string Nkgttienk { get; set; }
        public string Nkgtchiphitc { get; set; }
        public string Nkgtchiphibh { get; set; }
        public string Nkgtchiphiql { get; set; }
        public string Nkgtloinhuandk { get; set; }
        public string Nkgtthuegtgt { get; set; }
        public string Nkgtgiabandk { get; set; }
        public string Sxdonvisxkd { get; set; }
        public string Sxqcpc { get; set; }
        public string Sxchiphinvldvt { get; set; }
        public double Sxchiphinvlsl { get; set; }
        public double Sxchiphinvldg { get; set; }
        public string Sxchiphincdvt { get; set; }
        public double Sxchiphincsl { get; set; }
        public double Sxchiphincdg { get; set; }
        public string Sxchiphinvpxdvt { get; set; }
        public double Sxchiphinvpxsl { get; set; }
        public double Sxchiphinvpxdg { get; set; }
        public string Sxchiphivldvt { get; set; }
        public double Sxchiphivlsl { get; set; }
        public double Sxchiphivldg { get; set; }
        public string Sxchiphidcsxdvt { get; set; }
        public double Sxchiphidcsxsl { get; set; }
        public double Sxchiphidcsxdg { get; set; }
        public string Sxchiphikhtscddvt { get; set; }
        public double Sxchiphikhtscdsl { get; set; }
        public double Sxchiphikhtscddg { get; set; }
        public string Sxchiphidvmndvt { get; set; }
        public double Sxchiphidvmnsl { get; set; }
        public double Sxchiphidvmndg { get; set; }
        public string Sxchiphitienkdvt { get; set; }
        public double Sxchiphitienksl { get; set; }
        public double Sxchiphitienkdg { get; set; }
        public string Sxchiphibhdvt { get; set; }
        public double Sxchiphibhsl { get; set; }
        public double Sxchiphibhdg { get; set; }
        public string Sxchiphiqldndvt { get; set; }
        public double Sxchiphiqldnsl { get; set; }
        public double Sxchiphiqldndg { get; set; }
        public string Sxchiphitcdvt { get; set; }
        public double Sxchiphitcsl { get; set; }
        public double Sxchiphitcdg { get; set; }
        public string Sxloinhuandkdvt { get; set; }
        public double Sxloinhuandksl { get; set; }
        public double Sxloinhuandkdg { get; set; }
        public string Sxgiabanctdvt { get; set; }
        public double Sxgiabanctsl { get; set; }
        public double Sxgiabanctdg { get; set; }
        public string Sxthuettdbdvt { get; set; }
        public double Sxthuettdbsl { get; set; }
        public double Sxthuettdbdg { get; set; }
        public string Sxthuegtgtdvt { get; set; }
        public double Sxthuegtgtsl { get; set; }
        public double Sxthuegtgtdg { get; set; }
        public string Sxgiabandvt { get; set; }
        public double Sxgiabansl { get; set; }
        public double Sxgiabandg { get; set; }
        public string Sxgtchiphisx { get; set; }
        public string Sxgtchiphibh { get; set; }
        public string Sxgtchiphiqldn { get; set; }
        public string Sxgtchiphitc { get; set; }
        public string Sxgtloinhuandk { get; set; }
        public string Sxgtthuettdb { get; set; }
        public string Sxgtthuegtgt { get; set; }
        public string Sxgtgiaban { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
