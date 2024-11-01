using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Mvc;
using System;

public class DatabaseController : Controller
{
    private readonly BackupService _backupService;

    public DatabaseController(BackupService backupService)
    {
        _backupService = backupService;
    }

    [HttpGet]
    public IActionResult Backup()
    {
        return View("Views/Admin/Systems/Database/Backup.cshtml");
    }

    [HttpPost]
    public IActionResult Backup(string backupPath = @"C:\backups\CSDLGIAKH_ASP_DEMO.bak")
    {
        try
        {
            _backupService.BackupDatabase(backupPath);
            ViewBag.Message = "Backup completed successfully!";
        }
        catch (Exception ex)
        {
            ViewBag.Message = $"Backup failed: {ex.Message}";
        }
        return View("Views/Admin/Systems/Database/Backup.cshtml");
    }

    [HttpGet]
    public IActionResult SaoLuu(DateTime? NgayTu, DateTime? NgayDen)
    {
        DateTime nowDate = DateTime.Now;
        DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
        DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

        NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
        NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;

        ViewData["NgayTu"] = NgayTu;
        ViewData["NgayDen"] = NgayDen;
        ViewData["Title"] = "Sao lưu dữ liệu";
        
        ViewData["MenuLv1"] = "menu_qthethong";
        ViewData["MenuLv2"] = "menu_dulieu";
        ViewData["MenuLv3"] = "menu_dulieu_saoluu";
        return View("Views/Admin/Systems/Database/SaoLuu.cshtml");
    }

    [HttpPost]
    public IActionResult Export(DateTime? NgayTu, DateTime? NgayDen)
    {
        DateTime nowDate = DateTime.Now;
        DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
        DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

        NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
        NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
        ViewBag.Message = "Sao lưu dữ liệu thành công!";
        ViewData["NgayTu"] = NgayTu;
        ViewData["NgayDen"] = NgayDen;
        ViewData["Title"] = "Sao lưu dữ liệu";
        
        ViewData["MenuLv1"] = "menu_qthethong";
        ViewData["MenuLv2"] = "menu_dulieu";
        ViewData["MenuLv3"] = "menu_dulieu_saoluu";
        //return View("Views/Admin/Systems/Database/SaoLuu.cshtml");
        return View("Views/Admin/Error/Test.cshtml");
    }

    [HttpGet]
    public IActionResult KhoiPhuc()
    {
        ViewData["Title"] = "Khôi phục dữ liệu";
        
        ViewData["MenuLv1"] = "menu_qthethong";
        ViewData["MenuLv2"] = "menu_dulieu";
        ViewData["MenuLv3"] = "menu_dulieu_khoiphuc";
        return View("Views/Admin/Systems/Database/KhoiPhuc.cshtml");
    }

    [HttpPost]
    public IActionResult Import()
    {
        ViewBag.Message = "Khôi phục dữ liệu thành công!";
        ViewData["Title"] = "Khôi phục dữ liệu";
        
        ViewData["MenuLv1"] = "menu_qthethong";
        ViewData["MenuLv2"] = "menu_dulieu";
        ViewData["MenuLv3"] = "menu_dulieu_khoiphuc";
        //return View("Views/Admin/Systems/Database/KhoiPhuc.cshtml");
        return View("Views/Admin/Error/Test.cshtml");
    }
}
