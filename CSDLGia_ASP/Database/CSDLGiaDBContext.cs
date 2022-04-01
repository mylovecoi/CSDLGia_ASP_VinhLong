using CSDLGia_ASP.Models;
using Microsoft.EntityFrameworkCore;



namespace CSDLGia_ASP.Database
{
    public class CSDLGiaDBContext : DbContext
    {
        public CSDLGiaDBContext(DbContextOptions<CSDLGiaDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<viewDonVi_TaiKhoan>()
                .HasNoKey()
                .ToView("viewDonVi_TaiKhoan");
            modelBuilder.Entity<tblPhanQuyen>().HasKey(e => new
            {
                e.MaChucNang,
                e.TenDangNhap,
            });
        }

        public DbSet<tblHeThong> tblHeThong { get; set; }

        public DbSet<tblDMChucNang> tblDMChucNang { get; set; }

        public DbSet<tblDSTaiKhoan> tblDSTaiKhoan { get; set; }

        public DbSet<tblDSDiaBan> tblDSDiaBan { get; set; }

        public DbSet<tblDSDonVi> tblDSDonVi { get; set; }

        public DbSet<tblPhanQuyen> tblPhanQuyen { get; set; }

        public DbSet<viewDonVi_TaiKhoan> viewDonVi_TaiKhoan { get; set; }

    }
}
