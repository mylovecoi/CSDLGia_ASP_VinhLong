using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.Database;
using CSDLGia_ASP.ViewModels.Manages;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.Models.Systems;
using System.Xml.Linq;

namespace CSDLGia_ASP.Helper
{
    public class Helpers
    {
        public static List<VMKeyValue> GetListColExcel()
        {
            List<VMKeyValue> list = new List<VMKeyValue> { };
            list.Add(new VMKeyValue { Key = "A", Value = 1 });
            list.Add(new VMKeyValue { Key = "B", Value = 2 });
            list.Add(new VMKeyValue { Key = "C", Value = 3 });
            list.Add(new VMKeyValue { Key = "D", Value = 4 });
            list.Add(new VMKeyValue { Key = "E", Value = 5 });
            list.Add(new VMKeyValue { Key = "F", Value = 6 });
            list.Add(new VMKeyValue { Key = "G", Value = 7 });
            list.Add(new VMKeyValue { Key = "H", Value = 8 });
            list.Add(new VMKeyValue { Key = "I", Value = 9 });
            list.Add(new VMKeyValue { Key = "J", Value = 10 });
            list.Add(new VMKeyValue { Key = "K", Value = 11 });
            list.Add(new VMKeyValue { Key = "L", Value = 12 });
            list.Add(new VMKeyValue { Key = "M", Value = 13 });
            list.Add(new VMKeyValue { Key = "N", Value = 14 });
            list.Add(new VMKeyValue { Key = "O", Value = 15 });
            list.Add(new VMKeyValue { Key = "P", Value = 16 });
            list.Add(new VMKeyValue { Key = "Q", Value = 17 });
            list.Add(new VMKeyValue { Key = "R", Value = 18 });
            list.Add(new VMKeyValue { Key = "S", Value = 19 });
            list.Add(new VMKeyValue { Key = "T", Value = 20 });
            list.Add(new VMKeyValue { Key = "U", Value = 21 });
            list.Add(new VMKeyValue { Key = "V", Value = 22 });
            list.Add(new VMKeyValue { Key = "W", Value = 23 });
            list.Add(new VMKeyValue { Key = "X", Value = 24 });
            list.Add(new VMKeyValue { Key = "Y", Value = 25 });
            list.Add(new VMKeyValue { Key = "Z", Value = 26 });

            return list;
        }

        public static List<VMRoleList> GetRoleList()
        {
            List<VMRoleList> roldelist = new List<VMRoleList> { };
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv", Name = "CSDL VỀ MỨC GIÁ HÀNG HÓA, DỊCH VỤ" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //chức năng định giá
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia", Name = "Định giá(ĐG)" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn", Name = "ĐG - Giá thuế tài nguyên" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.thongtin", Name = "ĐG - Giá thuế tài nguyên - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.xetduyet", Name = "ĐG - Giá thuế tài nguyên - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.timkiem", Name = "ĐG - Giá thuế tài nguyên - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.baocao", Name = "ĐG - Giá thuế tài nguyên - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe", Name = "ĐG - Giá đất cụ thể" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", Name = "ĐG - Giá đất cụ thể - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet", Name = "ĐG - Giá đất cụ thể - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.timkiem", Name = "ĐG - Giá đất cụ thể - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.baocao", Name = "ĐG - Giá đất cụ thể - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc", Name = "ĐG - Giá thuê mặt đất, mặt nước" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", Name = "ĐG - Giá thuê mặt đất, mặt nước - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", Name = "ĐG - Giá thuê mặt đất, mặt nước - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.xetduyet", Name = "ĐG - Giá thuê mặt đất, mặt nước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", Name = "ĐG - Giá thuê mặt đất, mặt nước - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.baocao", Name = "ĐG - Giá thuê mặt đất, mặt nước - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung", Name = "ĐG - Giá rừng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.danhmuc", Name = "ĐG - Giá rừng - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.thongtin", Name = "ĐG - Giá rừng - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.xetduyet", Name = "ĐG - Giá rừng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.timkiem", Name = "ĐG - Giá rừng - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.baocao", Name = "ĐG - Giá rừng - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha", Name = "ĐG - Giá thuê mua nhà ở" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.danhmuc", Name = "ĐG - Giá thuê mua nhà ở - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", Name = "ĐG - Giá thuê mua nhà ở - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", Name = "ĐG - Giá thuê mua nhà ở - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.timkiem", Name = "ĐG - Giá thuê mua nhà ở - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.baocao", Name = "ĐG - Giá thuê mua nhà ở - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh", Name = "ĐG - Giá nước sạch sinh hoạt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.danhmuc", Name = "ĐG - Giá nước sạch sinh hoạt - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", Name = "ĐG - Giá nước sạch sinh hoạt - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet", Name = "ĐG - Giá nước sạch sinh hoạt - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.timkiem", Name = "ĐG - Giá nước sạch sinh hoạt - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.baocao", Name = "ĐG - Giá nước sạch sinh hoạt - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong", Name = "ĐG - Giá thuê tài sản công" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.danhmuc", Name = "ĐG - Giá thuê tài sản công - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", Name = "ĐG - Giá thuê tài sản công - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", Name = "ĐG - Giá thuê tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.timkiem", Name = "ĐG - Giá thuê tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.baocao", Name = "ĐG - Giá thuê tài sản công - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.baocao", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao", Name = "ĐG - Dịch vụ giáo dục đào tạo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc", Name = "ĐG - Dịch vụ giáo dục đào tạo - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", Name = "ĐG - Dịch vụ giáo dục đào tạo - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.xetduyet", Name = "ĐG - Dịch vụ giáo dục đào tạo - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.timkiem", Name = "ĐG - Dịch vụ giáo dục đào tạo - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.baocao", Name = "ĐG - Dịch vụ giáo dục đào tạo - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh", Name = "ĐG - Dịch vụ khám chữa bệnh" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc", Name = "ĐG - Dịch vụ khám chữa bệnh - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", Name = "ĐG - Dịch vụ khám chữa bệnh - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", Name = "ĐG - Dịch vụ khám chữa bệnh - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.timkiem", Name = "ĐG - Dịch vụ khám chữa bệnh - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", Name = "ĐG - Dịch vụ khám chữa bệnh - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc", Name = "ĐG - Mức trợ giá trợ cước" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.danhmuc", Name = "ĐG - Mức trợ giá trợ cước - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", Name = "ĐG - Mức trợ giá trợ cước - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.xetduyet", Name = "ĐG - Mức trợ giá trợ cước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.timkiem", Name = "ĐG - Mức trợ giá trợ cước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.baocao", Name = "ĐG - Mức trợ giá trợ cước - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.danhmuc", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.thongtin", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.xetduyet", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.timkiem", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.baocao", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.thongtin", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.xetduyet", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.timkiem", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.baocao", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia", Name = "ĐG - Khung giá sản phẩm, dịch vụ" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.danhmuc", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.thongtin", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.xetduyet", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.timkiem", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.baocao", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida", Name = "ĐG - Giá sản phẩm dịch vụ tối đa" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.danhmuc", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.xetduyet", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.timkiem", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.baocao", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.danhmuc", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.thue", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Thuế" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu- Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.xetduyet", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.timkiem", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.baocao", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths", Name = "ĐG - Giá tài sản trong tố tụng hình sự" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.thongtin", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.xetduyet", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.timkiem", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.baocao", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //chức năng bình ổn giá
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog", Name = "Mặt hàng bình ổn giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.ttdn", Name = "Thông tin doanh nghiệp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.xdtttddn", Name = "Xét duyệt thông tin thay đổi doanh nghiệp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.phanloai", Name = "Mặt hàng bình ổn giá - Phân loại" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.thongtin", Name = "Mặt hàng bình ổn giá - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.xetduyet", Name = "Mặt hàng bình ổn giá - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.timkiem", Name = "Mặt hàng bình ổn giá - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.baocao", Name = "Mặt hàng bình ổn giá - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //chức năng kê khai giá
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia", Name = "Mức giá kê khai - đăng ký(KKNYG)" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.ttdn", Name = "Thông tin doanh nghiệp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.xdtttddn", Name = "Xét duyệt thông tin thay đổi doanh nghiệp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt", Name = "KKNYG - Dịch vụ lưu trú" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", Name = "KKNYG - Dịch vụ lưu trú - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakkxd", Name = "KKNYG - Dịch vụ lưu trú - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd", Name = "KKNYG - Xi măng, thép xây dựng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk", Name = "KKNYG - Xi măng, thép xây dựng - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkxd", Name = "KKNYG - Xi măng, thép xây dựng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach", Name = "KKNYG - Sách giáo khoa" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakk", Name = "KKNYG - Sách giáo khoa - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakkxd", Name = "KKNYG - Sách giáo khoa - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol", Name = "KKNYG - Etanol" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakk", Name = "KKNYG - Etanol - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakkxd", Name = "KKNYG - Etanol - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn", Name = "KKNYG - Thực phẩm chức năng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakk", Name = "KKNYG - Thực phẩm chức năng - Kê khai giá " });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakkxd", Name = "KKNYG - Thực phẩm chức năng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan", Name = "KKNYG - Cát sạn" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakk", Name = "KKNYG - Cát sạn - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakkxd", Name = "KKNYG - Cát sạn - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx", Name = "KKNYG - Học phí lái xe" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakk", Name = "KKNYG - Học phí lái xe - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakkxd", Name = "KKNYG - Học phí lái xe - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan", Name = "KKNYG - Than" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakk", Name = "KKNYG - Than - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", Name = "KKNYG - Than - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay", Name = "KKNYG - Giấy" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakk", Name = "KKNYG - Giấy - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakkxd", Name = "KKNYG - Giấy - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn", Name = "KKNYG - Thức ăn chăn nuôi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakk", Name = "KKNYG - Thức ăn chăn nuôi - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakkxd", Name = "KKNYG - Thức ăn chăn nuôi - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap", Name = "KKNYG - Đất san lấp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakk", Name = "KKNYG - Đất san lấp - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkxd", Name = "KKNYG - Đất san lấp - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung", Name = "KKNYG - Đá xây dựng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakk", Name = "KKNYG - Đá xây dựng - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkxd", Name = "KKNYG - Đá xây dựng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb", Name = "KKNYG - Vận tải xe buýt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakk", Name = "KKNYG - Vận tải xe buýt - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakkxd", Name = "KKNYG - Vận tải xe buýt - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk", Name = "KKNYG - Vận tải xe khách" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakk", Name = "KKNYG - Vận tải xe khách - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakkxd", Name = "KKNYG - Vận tải xe khách - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx", Name = "KKNYG - Vận tải xe taxi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk", Name = "KKNYG - Vận tải xe taxi - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkxd", Name = "KKNYG - Vận tải xe taxi - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk", Name = "KKNYG - Cước vận chuyển hành khách" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakk", Name = "KKNYG - Cước vận chuyển hành khách - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakkxd", Name = "KKNYG - Cước vận chuyển hành khách - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue", Name = "KKNYG - Ca huế" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakk", Name = "KKNYG - Ca huế - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakkxd", Name = "KKNYG - Ca huế - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsieuthi", Name = "KKNYG - Siêu thị" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsieuthi.giakk", Name = "KKNYG - Siêu thị - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsieuthi.giakkxd", Name = "KKNYG - Siêu thị - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });
            //Chức năng giá khác
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk", Name = "Giá HH-DV khác" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.dm", Name = "Giá HH-DV khác - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.dmdv", Name = "Giá HH-DV khác - Danh mục đơn vị" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.tt", Name = "Giá HH-DV khác - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.xd", Name = "Giá HH-DV khác - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.th", Name = "Giá HH-DV khác - Tổng hợp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.tk", Name = "Giá HH-DV khác - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.bc", Name = "Giá HH-DV khác - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong", Name = "Giá tài sản công" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong.thongtin", Name = "Giá tài sản công - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong.xetduyet", Name = "Giá tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong.timkiem", Name = "Giá tài sản công - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi", Name = "Giá lệ phí trước bạ" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.danhmuc", Name = "Giá lệ phí trước bạ - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.thongtin", Name = "Giá lệ phí trước bạ - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.xetduyet", Name = "Giá lệ phí trước bạ - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.timkiem", Name = "Giá lệ phí trước bạ - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb", Name = "Bảng giá các loại đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.thongtu", Name = "Bảng giá các loại đất - Thông tư" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.thongtin", Name = "Bảng giá các loại đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.xetduyet", Name = "Bảng giá các loại đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.timkiem", Name = "Bảng giá các loại đất - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.daugiadat", Name = "Giá trúng thầu quyền sử dụng đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.daugiadat.thongtin", Name = "Giá trúng thầu quyền sử dụng đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.daugiadat.xetduyet", Name = "Giá trúng thầu quyền sử dụng đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.daugiadat.timkiem", Name = "Giá trúng thầu quyền sử dụng đất - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.muataisan", Name = "Giá trúng thầu của HH-DV được mua sắm theo QĐ của PL về đấu thầu" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.muataisan.thongtin", Name = "Giá trúng thầu của HH-DV được mua sắm theo QĐ của PL về đấu thầu - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.muataisan.xetduyet", Name = "Giá trúng thầu của HH-DV được mua sắm theo QĐ của PL về đấu thầu - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.muataisan.timkiem", Name = "Giá trúng thầu của HH-DV được mua sắm theo QĐ của PL về đấu thầu - Tìm kiếm" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });
            roldelist.Add(new VMRoleList { Role = "", Name = "---------------------------------------------------------------------" });
            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //Thẩm định giá
            roldelist.Add(new VMRoleList { Role = "csdltdg", Name = "CSDL THẨM ĐỊNH GIÁ" });
            roldelist.Add(new VMRoleList { Role = "csdltdg.tdg", Name = "Thẩm định giá" });
            roldelist.Add(new VMRoleList { Role = "csdltdg.tdg.dv", Name = "Thẩm định giá - Danh mục đơn vị" });
            roldelist.Add(new VMRoleList { Role = "csdltdg.tdg.hh", Name = "Thẩm định giá - Danh mục hàng hóa" });
            roldelist.Add(new VMRoleList { Role = "csdltdg.tdg.tt", Name = "Thẩm định giá - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdltdg.tdg.xd", Name = "Thẩm định giá - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdltdg.tdg.tk", Name = "Thẩm định giá - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdltdg.tdg.bc", Name = "Thẩm định giá - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });
            roldelist.Add(new VMRoleList { Role = "", Name = "---------------------------------------------------------------------" });
            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //Văn bản quản lý nhà nước
            roldelist.Add(new VMRoleList { Role = "vbqlnnvegiaplp", Name = "VĂN BẢN QUẢN LÝ NHÀ NƯỚC VỀ GIÁ, PHÍ, LỆ PHÍ" });
            roldelist.Add(new VMRoleList { Role = "vbqlnnvegiaplp.vbqlnn", Name = "Văn bản quản lý nhà nước về giá - phí, lệ phí" });
            roldelist.Add(new VMRoleList { Role = "vbqlnnvegiaplp.vbqlnn.ds", Name = "Danh sách văn bản quản lý nhà nước về giá - phí, lệ phí" });


            roldelist.Add(new VMRoleList { Role = "", Name = "" });
            roldelist.Add(new VMRoleList { Role = "", Name = "---------------------------------------------------------------------" });
            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //Hệ thống
            roldelist.Add(new VMRoleList { Role = "hethong", Name = "HỆ THỐNG" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            roldelist.Add(new VMRoleList { Role = "hethong.nguoidung", Name = "Quản trị người dùng" });
            roldelist.Add(new VMRoleList { Role = "hethong.nguoidung.dsnhomtaikhoan", Name = "Danh sách nhóm tài khoản" });
            roldelist.Add(new VMRoleList { Role = "hethong.nguoidung.dstaikhoan", Name = "Danh sách tài khoản" });
            roldelist.Add(new VMRoleList { Role = "hethong.nguoidung.dstaikhoan.phanquyen", Name = "Danh sách tài khoản phân quyền" });
            roldelist.Add(new VMRoleList { Role = "hethong.nguoidung.dsdangky", Name = "Tài khoản đăng ký" });
            roldelist.Add(new VMRoleList { Role = "hethong.hethong", Name = "Quản trị hệ thống" });
            roldelist.Add(new VMRoleList { Role = "hethong.hethong.dsdiaban", Name = "Danh sách địa bàn" });
            roldelist.Add(new VMRoleList { Role = "hethong.hethong.dsdonvi", Name = "Danh sách đơn vị sử dụng" });
            roldelist.Add(new VMRoleList { Role = "hethong.hethong.dsxaphuong", Name = "Danh sách xã, phường, thị trấn" });
            roldelist.Add(new VMRoleList { Role = "hethong.danhmuc", Name = "Quản trị danh mục" });
            roldelist.Add(new VMRoleList { Role = "hethong.danhmuc.dmnganhnghekd", Name = "Danh mục ngành nghề kinh doanh" });
            roldelist.Add(new VMRoleList { Role = "hethong.danhmuc.dmloaidat", Name = "Danh mục loại đất" });
            roldelist.Add(new VMRoleList { Role = "hethong.danhmuc.dmnhomhh", Name = "Danh mục nhóm hàng hóa" });

            return roldelist;
        }

        public static string[] GetRolesList()
        {
            string[] roles = new string[]
            {
                "csdlmucgiahhdv",
                //chức năng định giá
                "csdlmucgiahhdv.dinhgia",
                "csdlmucgiahhdv.dinhgia.datcuthe",
                "csdlmucgiahhdv.dinhgia.datcuthe.thongtin",
                "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet",
                "csdlmucgiahhdv.dinhgia.thuedatnuoc",
                "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc",
                "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin",
                "csdlmucgiahhdv.dinhgia.thuedatnuoc.xetduyet",
                "csdlmucgiahhdv.dinhgia.rung",
                "csdlmucgiahhdv.dinhgia.rung.danhmuc",
                "csdlmucgiahhdv.dinhgia.rung.thongtin",
                "csdlmucgiahhdv.dinhgia.rung.xetduyet",
                "csdlmucgiahhdv.dinhgia.thuemuanha",
                "csdlmucgiahhdv.dinhgia.thuemuanha.danhmuc",
                "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin",
                "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet",
                "csdlmucgiahhdv.dinhgia.nuocsh",
                "csdlmucgiahhdv.dinhgia.nuocsh.danhmuc",
                "csdlmucgiahhdv.dinhgia.nuocsh.thongtin",
                "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet",

                "csdlmucgiahhdv.dinhgia.giaoducdaotao",
                "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc",
                "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin",
                "csdlmucgiahhdv.dinhgia.giaoducdaotao.xetduyet",
                "csdlmucgiahhdv.dinhgia.giaoducdaotao.timkiem",
                "csdlmucgiahhdv.dinhgia.khamchuabenh",
                "csdlmucgiahhdv.dinhgia.khamchuabenh.nhom",
                "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc",
                "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin",
                "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet",
                "csdlmucgiahhdv.dinhgia.khamchuabenh.timkiem",
                "csdlmucgiahhdv.dinhgia.trogiatrocuoc",
                "csdlmucgiahhdv.dinhgia.trogiatrocuoc.danhmuc",
                "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin",
                "csdlmucgiahhdv.dinhgia.trogiatrocuoc.xetduyet",
                "csdlmucgiahhdv.dinhgia.trogiatrocuoc.timkiem",
                "csdlmucgiahhdv.dinhgia.dichvucongich",
                "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc",
                "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin",
                "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet",
                "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem",
                "csdlmucgiahhdv.dinhgia.thuetaisancong",
                "csdlmucgiahhdv.dinhgia.thuetaisancong.danhmuc",
                "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin",
                "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet",
                "csdlmucgiahhdv.dinhgia.thuetaisancong.timkiem",
                // Tài sản công
                "csdlmucgiahhdv.taisancong",
                "csdlmucgiahhdv.taisancong.thongtin",
                "csdlmucgiahhdv.taisancong.xetduyet",
                "csdlmucgiahhdv.taisancong.timkiem",
                //trúng thầu mua hàng hóa dịch vụ
                "csdlmucgiahhdv.muataisan",
                "csdlmucgiahhdv.muataisan.thongtin",
                "csdlmucgiahhdv.muataisan.xetduyet",
                "csdlmucgiahhdv.muataisan.timkiem",
                //trung thầu quyền sd đất
                "csdlmucgiahhdv.daugiadat",
                "csdlmucgiahhdv.daugiadat.thongtin",
                "csdlmucgiahhdv.daugiadat.xetduyet",
                "csdlmucgiahhdv.daugiadat.timkiem",
                //giá lệ phí trước bạ
                "csdlmucgiahhdv.lephi",
                "csdlmucgiahhdv.lephi.danhmuc",
                "csdlmucgiahhdv.lephi.thongtin",
                "csdlmucgiahhdv.lephi.xetduyet",
                "csdlmucgiahhdv.lephi.timkiem",




                //chức năng bình ổn giá
                "csdlmucgiahhdv.bog",
                "csdlmucgiahhdv.bog.ttdn",
                "csdlmucgiahhdv.bog.xdtttddn",
                "csdlmucgiahhdv.bog.phanloai",
                "csdlmucgiahhdv.bog.thongtin",
                "csdlmucgiahhdv.bog.xetduyet",
                "csdlmucgiahhdv.bog.timkiem",
                "csdlmucgiahhdv.bog.baocao",




                //chức năng kê khai giá
                "csdlmucgiahhdv.kknygia",
                "csdlmucgiahhdv.kknygia.ttdn",
                "csdlmucgiahhdv.kknygia.xdtttddn",
                "csdlmucgiahhdv.kknygia.kkgxmtxd",
                "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk",
                "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgdvlt",
                "csdlmucgiahhdv.kknygia.kkgdvlt.giakk",
                "csdlmucgiahhdv.kknygia.kkgdvlt.giakkxd",

                "csdlmucgiahhdv.kknygia.kkgsach",
                "csdlmucgiahhdv.kknygia.kkgsach.giakk",
                "csdlmucgiahhdv.kknygia.kkgsach.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgetanol",
                "csdlmucgiahhdv.kknygia.kkgetanol.giakk",
                "csdlmucgiahhdv.kknygia.kkgetanol.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgtpcn",
                "csdlmucgiahhdv.kknygia.kkgtpcn.giakk",
                "csdlmucgiahhdv.kknygia.kkgtpcn.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgcatsan",
                "csdlmucgiahhdv.kknygia.kkgcatsan.giakk",
                "csdlmucgiahhdv.kknygia.kkgcatsan.giakkxd",
                "csdlmucgiahhdv.kknygia.kkghplx",
                "csdlmucgiahhdv.kknygia.kkghplx.giakk",
                "csdlmucgiahhdv.kknygia.kkghplx.giakkxd",

                "csdlmucgiahhdv.kknygia.kkgthan",
                "csdlmucgiahhdv.kknygia.kkgthan.giakk",
                "csdlmucgiahhdv.kknygia.kkgthan.giakkxd",
                "csdlmucgiahhdv.kknygia.kkggiay",
                "csdlmucgiahhdv.kknygia.kkggiay.giakk",
                "csdlmucgiahhdv.kknygia.kkggiay.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgtacn",
                "csdlmucgiahhdv.kknygia.kkgtacn.giakk",
                "csdlmucgiahhdv.kknygia.kkgtacn.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgdatsanlap",
                "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakk",
                "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgdaxaydung",
                "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakk",
                "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkxd",


                "csdlmucgiahhdv.kknygia.kkgvtxb",
                "csdlmucgiahhdv.kknygia.kkgvtxb.giakk",
                "csdlmucgiahhdv.kknygia.kkgvtxb.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgvtxk",
                "csdlmucgiahhdv.kknygia.kkgvtxk.giakk",
                "csdlmucgiahhdv.kknygia.kkgvtxk.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgvtxtx",
                "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk",
                "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkxd",
                "csdlmucgiahhdv.kknygia.kkgcahue",
                "csdlmucgiahhdv.kknygia.kkgcahue.giakk",
                "csdlmucgiahhdv.kknygia.kkgcahue.giakkxd",
                "csdlmucgiahhdv.kknygia.kkcvchk",
                "csdlmucgiahhdv.kknygia.kkcvchk.giakk",
                "csdlmucgiahhdv.kknygia.kkcvchk.giakkxd",

                //Hệ thống
                "hethong",
                "hethong.nguoidung",
                "hethong.nguoidung.dsnhomtaikhoan",
                "hethong.nguoidung.dstaikhoan",
                "hethong.nguoidung.dstaikhoan.phanquyen",
                "hethong.nguoidung.dsdangky",
                "hethong.hethong",
                "hethong.hethong.dsdiaban",
                "hethong.hethong.dsdonvi",
                "hethong.hethong.dsxaphuong",
                "hethong.danhmuc",
                "hethong.danhmuc.dmnganhnghekd",

            };
            return roles;

        }

        public static string GetMenuMinimize(ISession session)
        {
            if (!string.IsNullOrEmpty(session.GetString("MenuMinimize")))
            {
                return session.GetString("MenuMinimize");
            }
            else
            {
                return "False";
            }
        }

        public static string GetSsAdmin(ISession session, string key)
        {
            if (!string.IsNullOrEmpty(session.GetString("SsAdmin")))
            {
                string ssadmin = session.GetString("SsAdmin");
                dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin);
                string value = sessionInfo[key];
                return value;
            }
            else
            {
                return "";
            }
        }

        public static string GetThongTinDonVi(ISession session, string key)
        {
            if (!string.IsNullOrEmpty(session.GetString("ThongTinDonVi")))
            {
                string thongtindonvi = session.GetString("ThongTinDonVi");
                dynamic thongtindonviInfo = JsonConvert.DeserializeObject(thongtindonvi);
                string value = thongtindonviInfo[key];
                return value;
            }
            else
            {
                return "";
            }
        }

        public static string GetThongTinUsers(ISession session)
        {
            string ssadmin = session.GetString("SsAdmin");
            dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin);
            string value = sessionInfo["Name"] + " (" + sessionInfo["Username"] + ")";
            return value;
        }

        public static bool CheckPermission(ISession session, string roles, string key)
        {
            if (!string.IsNullOrEmpty(session.GetString("SsAdmin")))
            {
                string ssadmin = session.GetString("SsAdmin");
                dynamic sessionInfo = JsonConvert.DeserializeObject(ssadmin);
                bool ssa = sessionInfo["Sadmin"];
                if (ssa)
                {
                    return true;
                }
                else
                {
                    string per = session.GetString("Permission");
                    if (!string.IsNullOrEmpty(per))
                    {
                        dynamic info = JsonConvert.DeserializeObject(per);

                        foreach (var item in info)
                        {
                            if (item["Roles"] == roles && item[key] == true)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static string ConvertDateToText(DateTime date)
        {
            string date_convert = date.Date.ToString("dd/MM/yyyy");
            if (date_convert == "01/01/0001")
            {
                return " Ngày .. tháng .. năm ....";
            }
            else
            {
                string str = "";
                str += " Ngày " + date.ToString("dd");
                str += " tháng " + date.ToString("MM");
                str += " năm " + date.ToString("yyyy");
                return str;
            }
        }

        public static string ConvertDateToFormView(DateTime date)
        {
            string date_convert = date.Date.ToString("dd/MM/yyyy");
            if (date_convert == "01/01/0001")
            {
                string str = DateTime.Now.Date.ToString("yyyy-MM-dd");
                //string str = string.Format("{0:MM/dd/yyyy}");
                return str;
            }
            else
            {
                string str = date.Date.ToString("yyyy-MM-dd");
                return str;
            }
        }

        public static string ConvertDateTimeToFormView(DateTime date)
        {
            string date_convert = date.Date.ToString("dd/MM/yyyy");
            if (date_convert == "01/01/0001")
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                return str;
            }
            else
            {
                string str = date.ToString("yyyy-MM-dd HH:mm:ss").Replace(' ', 'T');
                return str;
            }
        }

        public static string ConvertDbToStrDecimal(double db)
        {
            if (db == 0)
            {
                return "";
            }
            else
            {
                return db.ToString();
            }
        }

        public static string ConvertDbToStr(double db)
        {
            if (db == 0)
            {
                return "";
            }
            else
            {
                string str = String.Format("{0:n0}", db);
                return str;
            }
        }

        public static string ConvertIntToStr(int value)
        {
            if (value == 0)
            {
                return "";
            }
            else
            {
                string str = String.Format("{0:n0}", value);
                return str;
            }
        }

        public static string ConvertDateToStr(DateTime date)
        {

            string str = date.Date.ToString("dd/MM/yyyy");
            if (str == "01/01/0001")
            {
                return "";
            }
            else
            {
                return str;
            }
        }
        
        public static string ConvertDateToStrAjax(DateTime date)
        {

            string str = date.Date.ToString("yyyy-MM-dd");
            if (str == "0001-01-01")
            {
                return "";
            }
            else
            {
                return str;
            }
        }

        public static string ConvertDateTimeToStr(DateTime datetime)
        {
            string str = datetime.Date.ToString("dd/MM/yyyy HH:mm:ss,fff tt");
            return str;
        }

        public static string ConvertDateTimeToText(DateTime datetime)
        {
            string gio = datetime.Hour < 10 ? "0" + datetime.Hour.ToString() : datetime.Hour.ToString();
            string phut = datetime.Minute < 10 ? "0" + datetime.Minute.ToString() : datetime.Minute.ToString();
            string ngay = datetime.Day < 10 ? "0" + datetime.Day.ToString() : datetime.Day.ToString();
            string thang = datetime.Month < 10 ? "0" + datetime.Month.ToString() : datetime.Month.ToString();
            string text = gio + " giờ " + phut + " phút";
            text += ", ngày " + ngay + " tháng " + thang + " năm " + datetime.Year.ToString();
            return text;
        }

        public static double ConvertStrToDb(string str)
        {
            if (str == "")
            {
                return 0;
            }
            else
            {
                double db = double.Parse(str.Replace(",", ""));
                return db;
            }
        }

        public static string ConvertYearToStr(int year)
        {
            if (year == 0)
            {
                return "";
            }
            else
            {
                return year.ToString();
            }
        }

        public static string ConvertMonthToStr(int month)
        {
            if (month == 0)
            {
                return "";
            }
            else
            {
                return month.ToString();
            }
        }

        public static string ConvertDbToMoneyText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            string[] str = result.Split(" ");
            int len_array = str.Count() - 1;
            string new_str = str[0].Substring(0, 1).ToUpper() + str[0].Substring(1);
            for (int i = 1; i <= len_array; i++)
            {
                new_str += " " + str[i];
            }
            if (isNegative) result = "Âm " + new_str;
            return new_str + (suffix ? " đồng chẵn %." : "");
        }

        public static string ConvertDbToText(double inputNumber, bool suffix = true)
        {
            string[] unitNumbers = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] placeValues = new string[] { "", "nghìn", "triệu", "tỷ" };
            bool isNegative = false;

            // -12345678.3445435 => "-12345678"
            string sNumber = inputNumber.ToString("#");
            double number = Convert.ToDouble(sNumber);
            if (number < 0)
            {
                number = -number;
                sNumber = number.ToString();
                isNegative = true;
            }


            int ones, tens, hundreds;

            int positionDigit = sNumber.Length;   // last -> first

            string result = " ";


            if (positionDigit == 0)
                result = unitNumbers[0] + result;
            else
            {
                // 0:       ###
                // 1: nghìn ###,###
                // 2: triệu ###,###,###
                // 3: tỷ    ###,###,###,###
                int placeValue = 0;

                while (positionDigit > 0)
                {
                    // Check last 3 digits remain ### (hundreds tens ones)
                    tens = hundreds = -1;
                    ones = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                    positionDigit--;
                    if (positionDigit > 0)
                    {
                        tens = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                        positionDigit--;
                        if (positionDigit > 0)
                        {
                            hundreds = Convert.ToInt32(sNumber.Substring(positionDigit - 1, 1));
                            positionDigit--;
                        }
                    }

                    if ((ones > 0) || (tens > 0) || (hundreds > 0) || (placeValue == 3))
                        result = placeValues[placeValue] + result;

                    placeValue++;
                    if (placeValue > 3) placeValue = 1;

                    if ((ones == 1) && (tens > 1))
                        result = "một " + result;
                    else
                    {
                        if ((ones == 5) && (tens > 0))
                            result = "lăm " + result;
                        else if (ones > 0)
                            result = unitNumbers[ones] + " " + result;
                    }
                    if (tens < 0)
                        break;
                    else
                    {
                        if ((tens == 0) && (ones > 0)) result = "lẻ " + result;
                        if (tens == 1) result = "mười " + result;
                        if (tens > 1) result = unitNumbers[tens] + " mươi " + result;
                    }
                    if (hundreds < 0) break;
                    else
                    {
                        if ((hundreds > 0) || (tens > 0) || (ones > 0))
                            result = unitNumbers[hundreds] + " trăm " + result;
                    }
                    result = " " + result;
                }
            }
            result = result.Trim();
            string[] str = result.Split(" ");
            int len_array = str.Count() - 1;
            string new_str = str[0].Substring(0, 1).ToUpper() + str[0].Substring(1);
            for (int i = 1; i <= len_array; i++)
            {
                new_str += " " + str[i];
            }
            if (isNegative) new_str = "Âm " + new_str;
            return new_str;
        }

        public static string ConvertIntToRoman(int roman)
        {
            string str = "";
            switch (roman)
            {
                case 1:
                    str = "I";
                    break;
                case 2:
                    str = "II";
                    break;
                case 3:
                    str = "III";
                    break;
                case 4:
                    str = "IV";
                    break;
                case 5:
                    str = "V";
                    break;
                case 6:
                    str = "VI";
                    break;
                case 7:
                    str = "VII";
                    break;
                case 8:
                    str = "VIII";
                    break;
                case 9:
                    str = "IX";
                    break;
                case 10:
                    str = "X";
                    break;
                case 11:
                    str = "XI";
                    break;
                case 12:
                    str = "XII";
                    break;
                case 13:
                    str = "XIII";
                    break;
                case 14:
                    str = "XIV";
                    break;
                case 15:
                    str = "XV";
                    break;
                case 16:
                    str = "XVI";
                    break;
                case 17:
                    str = "XVII";
                    break;
                case 18:
                    str = "XVIII";
                    break;
                case 19:
                    str = "XIX";
                    break;
                case 20:
                    str = "XX";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

        public static string[] GetListsDays()
        {
            string[] days = new string[]
               {
                    "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                    "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
                    "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"
               };
            return days;
        }

        public static string[] GetListsMonths()
        {
            string[] months = new string[]
               {
                    "01", "02", "03", "04", "05", "06", "07", "08", "09", "10",
                    "11", "12"
               };
            return months;
        }

        public static string ConvertDayMonthYearToString(string day, string month, string year)
        {
            string str = "";
            if (!string.IsNullOrEmpty(day))
            {
                str += day + "/";
            }
            if (!string.IsNullOrEmpty(month))
            {
                str += month + "/";
            }
            if (!string.IsNullOrEmpty(year))
            {
                str += year;
            }
            return str;
        }

        public static bool CheckFileType(string extension)
        {
            if (extension == ".jpg") return true;
            if (extension == ".jpeg") return true;
            if (extension == ".png") return true;
            if (extension == ".doc") return true;
            if (extension == ".docx") return true;
            if (extension == ".pdf") return true;
            if (extension == ".xls") return true;
            if (extension == ".xlsx") return true;
            return false;
        }

        public static bool CheckFileSize(long size)
        {
            if (size <= 5242880) return true;
            return false;
        }

        public static string[] GetGroupPer()
        {
            string[] group = new string[]
               { "K" , "T", "H", "X"};
            return group;
        }

        public static DateTime ConvertStringToDate(string Ngay, string Thang, string Nam)
        {
            if (string.IsNullOrEmpty(Ngay)) Ngay = "01";
            if (string.IsNullOrEmpty(Thang)) Thang = "01";
            if (string.IsNullOrEmpty(Nam)) Nam = "1900";
            string date = Ngay + "/" + Thang + "/" + Nam;
            DateTime dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return dt;
        }

        public static string ConvertMonthYearToString(string Thang, string Nam)
        {
            string str = "";
            if (!string.IsNullOrEmpty(Thang))
            {
                str += Thang + " tháng ";
            }
            if (!string.IsNullOrEmpty(Nam))
            {
                str += Nam + " năm";
            }

            return str;
        }

        public static string StringtoMD5(string chuoi)
        {
            MD5 mD5 = MD5.Create();
            byte[] mahoa = mD5.ComputeHash(Encoding.UTF8.GetBytes(chuoi));
            StringBuilder kq = new StringBuilder();
            for (int i = 0; i < mahoa.Length; i++)
            {
                kq.Append(mahoa[i].ToString("x2"));
            }
            return kq.ToString();
        }

        public static string GetSessionValue(ISession session, string key, string deFault = "")
        {
            try
            {
                dynamic sessionInfo = JsonConvert.DeserializeObject(session.GetString("CSDLGia"));
                deFault = sessionInfo[key];
            }
            catch (NullReferenceException) { return deFault; }

            return deFault;
        }

        public static string GetChucNang(ISession session, string key, string deFault = "")
        {
            try
            {
                dynamic sessionInfo = JsonConvert.DeserializeObject(session.GetString("ChucNang"));
                deFault = sessionInfo[key];
            }
            catch (NullReferenceException)
            {
                return deFault;
            }

            return deFault;
        }

        static public bool ChkPhanQuyen(ISession session, string maChucNang, string phanQuyen)
        {
            //Kiểm tra chức năng
            if (!ChkChucNang(session, maChucNang)) { return false; }
            bool bKQ = false;

            return bKQ;
        }

        static public bool ChkChucNang(ISession session, string maChucNang)
        {
            bool bKQ = false;

            return bKQ;
        }

        //Mặc định SSA có tất cả quyền
        static public bool ChkChucNangTaiKhoan(ISession session, string maChucNang)
        {
            bool bKQ = false;
            try
            {
                dynamic sessionInfo = JsonConvert.DeserializeObject(session.GetString("CSDLGia"));
                if (sessionInfo["CapDo"] == "SSA")
                {
                    bKQ = true;
                }
                else
                {
                    bKQ = sessionInfo[maChucNang];
                }

            }
            catch (NullReferenceException) { return false; }
            return bKQ;
        }

        public static string[] DviTinhRung()
        {
            string[] dvi = new string[]
           {
                "đồng",
                "đồng/ngày",
                "đồng/gói",
                "VNĐ/m2",
                "đồng/m2",
                "đồng/ha/năm",
                "đồng/tháng",
                "đ/kg",
                "đg/kg",
                "đồng/người/chuyến",
                "đ/tấn",
                "đồng/lần",
                "đ/m2/ngày",
                "đồng/buổi",
                "đồng/m2/ngày",
                "đồng/lít",
                "đồng/bình",
                "bình",
                "đồng/vé/lượt",
                "bao",
                "chai"
           };
            return dvi;
        }

        public static string[] PlTroGiaTroCuoc()
        {
            string[] phanloai = new string[]
           {
            "Chi từ Nhân sách địa phương và trung ương",
            "Mức giá hoặc khung giá bán lẻ",
            "Cung ứng hàng hóa, dịch vụ thiết yếu phục vụ đồng bào miền núi, vùng sâu, xa và hải đảo",
            "Trợ giá, trợ cước khác",
           };
            return phanloai;
        }

        public static string[] PlDvCi()
        {
            string[] phanloai = new string[]
           {
            "Sản phẩm",
            "Dịch vụ công ích",
            "Dịch vụ sự công nghiệp",
            "hàng hóa, dịch vụ",
            "Sản phẩm, dịch vụ khác"
           };
            return phanloai;
        }

        public static string[] HientrangThueTsc()
        {
            string[] phanloai = new string[]
           {
            "Đang cho thuê",
            "Đang sử dụng",
            "Đã bán",
            "Chưa sử dụng",
           };
            return phanloai;
        }

        public static string[] PlDauGiaDat()
        {
            string[] phanloai = new string[]
           {
            "Theo dự án",
            "Theo lô",
            "Đất ở",
            "Đất công ích",
            "Đất khác"
           };
            return phanloai;
        }
        public static string[] Loaigia()
        {
            string[] loaigia = new string[]
            {
                "Giá bán buôn",
                "Giá bán lẻ",
                "Giá kê khai",
                "Giá đăng ký"
            };
            return loaigia;
        }
        public static string[] Nguonthongtin()
        {
            string[] nguontt = new string[]
            {
                "Do trục tiếp điều tra, thu thập",
                "Hợp đồng mua tin",
                "Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định",
                "Từ thống kê đăng ký giá, kê khai giá, thông báo giá của doanh nghiệp",
                "Các nguồn thông tin khác",
            };
            return nguontt;
        }
        public static string[] TinhtrangTs()
        {
            string[] ttrang = new string[]
            {
                "Chưa qua sử dụng",
                "Đã qua sử dụng",
                "Bị hủy hoại",
                "Hư hỏng một phần",
                "Bị hủy hoại, hư hỏng toàn bộ nhưng vẫn có khả năng khôi phục lại tình trạng của tài sản trước khi bị hủy hoại, hư hỏng",
                "Bị mất, thất lạc",
                "Bị hủy hoại, hư hỏng toàn bộ và không có khả năng khôi phục lại tình trạng của tài sản trước khi bị hủy hoại, hư hỏng",
                "Hàng giả",
                "Không mua bán phổ biến trên thị trường"
            };
            return ttrang;
        }

        public static string[] NhomChiSoGia()
        {
            string[] csg = new string[]
            {
                "1","2","3","4"
            };
            return csg;
        }

        /*public static string checkManhom(int tiento, int hauto)
        {
            if (hauto == 9)
            {
                tiento = tiento + 1;
                hauto = 0;
                return Convert.ToString(tiento) + Convert.ToString(hauto);
            }
            else
            {
                hauto += 1;
                return Convert.ToString(tiento) + Convert.ToString(hauto);
            }
        }*/
        /*public static string ConvertStrToMothYear(DateTime date)
        {
            string date_convert = date.Date.ToString("dd/MM/yyyy");

            string str = "";
            str += " Tháng " + date.ToString("MM");
            str += " Năm " + date.ToString("yyyy");
            return str;
        }*/

    }
}
