using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDvGdDtDm
    {
        [Key]
        public int Id { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string Phanloai { get; set; }
        public string Hientrang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
