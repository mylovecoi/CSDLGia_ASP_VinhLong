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
        
        public static List<VMType> GetTypeData()
        {
            List<VMType> list = new List<VMType> { };
            list.Add(new VMType { Value = "STRING", Description = "STRING"});
            list.Add(new VMType { Value = "DATE", Description = "DATE"});
            list.Add(new VMType { Value = "BASES64", Description = "BASES64"});
            list.Add(new VMType { Value = "OBJECT", Description = "OBJECT"});
            list.Add(new VMType { Value = "NUMBER", Description = "NUMBER"});
            list.Add(new VMType { Value = "BOOLEAN", Description = "BOOLEAN"});

            return list;
        }

        public static List<VMType> GetClass()
        {
            List<VMType> list = new List<VMType> { };
            list.Add(new VMType { Value = "Header", Description = "Header" });
            list.Add(new VMType { Value = "Body", Description = "Body" });
            list.Add(new VMType { Value = "Security", Description = "Security/Signature" });

            return list;
        }
        public static List<VMRoleList> GetRoleList()
        {
            List<VMRoleList> roldelist = new List<VMRoleList> { };
            //I.CSDL VỀ MỨC GIÁ HÀNG HÓA, DỊCH VỤ
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv", Name = "CSDL VỀ MỨC GIÁ HÀNG HÓA, DỊCH VỤ" });
            //1.Giá hàng hóa dịch vụ do UBND tỉnh định giá
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia", Name = "Giá hàng hóa dịch vụ do UBND tỉnh định giá" });
            //1.1 Quản lý giá các loại đất
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe", Name = "ĐG - Giá đất cụ thể" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", Name = "ĐG - Giá đất cụ thể - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet", Name = "ĐG - Giá đất cụ thể - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.timkiem", Name = "ĐG - Giá đất cụ thể - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.baocao", Name = "ĐG - Giá đất cụ thể - Báo cáo" });
            //1.2 Quản lý giá cho thuê đất, mặt nước (đơn giá thuê đất, thuê mặt nước)
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc", Name = "ĐG - Giá thuê mặt đất, mặt nước" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", Name = "ĐG - Giá thuê mặt đất, mặt nước - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", Name = "ĐG - Giá thuê mặt đất, mặt nước - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.xetduyet", Name = "ĐG - Giá thuê mặt đất, mặt nước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", Name = "ĐG - Giá thuê mặt đất, mặt nước - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.baocao", Name = "ĐG - Giá thuê mặt đất, mặt nước - Báo cáo" });
            //1.3 Quản lý giá rừng
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung", Name = "ĐG - Giá rừng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.danhmuc", Name = "ĐG - Giá rừng - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.thongtin", Name = "ĐG - Giá rừng - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.xetduyet", Name = "ĐG - Giá rừng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.timkiem", Name = "ĐG - Giá rừng - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.baocao", Name = "ĐG - Giá rừng - Báo cáo" });
            //1.4 Quản lý giá cho thuê, thuê mua nhà ở xã hội, nhà ở công vụ
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh", Name = "ĐG - Giá thuê mua nhà xã hội" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", Name = "ĐG - Giá thuê mua nhà xã hội - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.danhmuc", Name = "ĐG - Giá thuê mua nhà xã hội - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.xetduyet", Name = "ĐG - Giá thuê mua nhà xã hội - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.timkiem", Name = "ĐG - Giá thuê mua nhà xã hội - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.baocao", Name = "ĐG - Giá thuê mua nhà xã hội - Báo cáo" });
            //1.5 Quản lý giá nước sạch sinh hoạt
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh", Name = "ĐG - Giá nước sạch sinh hoạt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.danhmucvung", Name = "ĐG - Giá nước sạch sinh hoạt - Danh mục vùng sử dụng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", Name = "ĐG - Giá nước sạch sinh hoạt - Danh mục khung giá sử dụng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", Name = "ĐG - Giá nước sạch sinh hoạt - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet", Name = "ĐG - Giá nước sạch sinh hoạt - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.timkiem", Name = "ĐG - Giá nước sạch sinh hoạt - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.baocao", Name = "ĐG - Giá nước sạch sinh hoạt - Báo cáo" });
            //1.6 Quản lý giá cho thuê tài sản Nhà nước
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong", Name = "ĐG - Giá thuê tài sản công" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.danhmuc", Name = "ĐG - Giá thuê tài sản công - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", Name = "ĐG - Giá thuê tài sản công - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", Name = "ĐG - Giá thuê tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.timkiem", Name = "ĐG - Giá thuê tài sản công - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.baocao", Name = "ĐG - Giá thuê tài sản công - Báo cáo" });
            //1.7 Quản lý giá sản phẩm, dịch vụ công ích
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci", Name = "ĐG - Sản phẩm dịch vụ công ích" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.danhmuc", Name = "ĐG - Sản phẩm dịch vụ công ích - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.thongtin", Name = "ĐG - Sản phẩm dịch vụ công ích - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.xetduyet", Name = "ĐG - Sản phẩm dịch vụ công ích - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.timkiem", Name = "ĐG - Sản phẩm dịch vụ công ích- Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.baocao", Name = "ĐG - Sản phẩm dịch vụ công ích - Báo cáo" });
            //1.8 Quản lý giá dịch vụ giáo dục, đào tạo
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao", Name = "ĐG - Dịch vụ giáo dục đào tạo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc", Name = "ĐG - Dịch vụ giáo dục đào tạo - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", Name = "ĐG - Dịch vụ giáo dục đào tạo - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.xetduyet", Name = "ĐG - Dịch vụ giáo dục đào tạo - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.timkiem", Name = "ĐG - Dịch vụ giáo dục đào tạo - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.baocao", Name = "ĐG - Dịch vụ giáo dục đào tạo - Báo cáo" });
            //1.9 Quản lý giá dịch vụ khám bệnh, chữa bệnh
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh", Name = "ĐG - Dịch vụ khám chữa bệnh" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc", Name = "ĐG - Dịch vụ khám chữa bệnh - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", Name = "ĐG - Dịch vụ khám chữa bệnh - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", Name = "ĐG - Dịch vụ khám chữa bệnh - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.timkiem", Name = "ĐG - Dịch vụ khám chữa bệnh - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", Name = "ĐG - Dịch vụ khám chữa bệnh - Báo cáo" });
            //1.10 Quản lý giá mức trợ giá, trợ cước
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc", Name = "ĐG - Mức trợ giá trợ cước" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.danhmuc", Name = "ĐG - Mức trợ giá trợ cước - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", Name = "ĐG - Mức trợ giá trợ cước - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.xetduyet", Name = "ĐG - Mức trợ giá trợ cước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.timkiem", Name = "ĐG - Mức trợ giá trợ cước - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.baocao", Name = "ĐG - Mức trợ giá trợ cước - Báo cáo" });
            //1.11 Quản lý gíá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.danhmuc", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.thongtin", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.xetduyet", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.timkiem", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.baocao", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Báo cáo" });
            //2.Danh mục bình ổn giá
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog", Name = "Mặt hàng bình ổn giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.ttdn", Name = "Thông tin doanh nghiệp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.xdtttddn", Name = "Xét duyệt thông tin thay đổi doanh nghiệp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.phanloai", Name = "Mặt hàng bình ổn giá - Phân loại" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.thongtin", Name = "Mặt hàng bình ổn giá - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.xetduyet", Name = "Mặt hàng bình ổn giá - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.timkiem", Name = "Mặt hàng bình ổn giá - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.baocao", Name = "Mặt hàng bình ổn giá - Báo cáo" });
            //3.Giá kê khai của hàng hóa, dịch vụ thuộc danh mục kê phải khai giá
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia", Name = "Mức giá kê khai - đăng ký(KKNYG)" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.ttdn", Name = "Thông tin doanh nghiệp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.xdtttddn", Name = "Xét duyệt thông tin thay đổi doanh nghiệp" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt", Name = "KKNYG - Dịch vụ lưu trú" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", Name = "KKNYG - Dịch vụ lưu trú - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakkbc", Name = "KKNYG - Dịch vụ lưu trú - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakkxd", Name = "KKNYG - Dịch vụ lưu trú - Xét duyệt" });


            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd", Name = "KKNYG - Xi măng, thép xây dựng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk", Name = "KKNYG - Xi măng, thép xây dựng - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkxd", Name = "KKNYG - Xi măng, thép xây dựng - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach", Name = "KKNYG - Sách giáo khoa" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakk", Name = "KKNYG - Sách giáo khoa - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakkbc", Name = "KKNYG - Sách giáo khoa - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakkxd", Name = "KKNYG - Sách giáo khoa - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol", Name = "KKNYG - Etanol" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakkbc", Name = "KKNYG - Etanol - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakk", Name = "KKNYG - Etanol - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakkxd", Name = "KKNYG - Etanol - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn", Name = "KKNYG - Thực phẩm chức năng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakk", Name = "KKNYG - Thực phẩm chức năng - Kê khai giá " });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakkbc", Name = "KKNYG - Thực phẩm chức năng - Báo cáo " });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakkxd", Name = "KKNYG - Thực phẩm chức năng - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan", Name = "KKNYG - Cát sạn" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakk", Name = "KKNYG - Cát sạn - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakkbc", Name = "KKNYG - Cát sạn - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakkxd", Name = "KKNYG - Cát sạn - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx", Name = "KKNYG - Học phí lái xe" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakk", Name = "KKNYG - Học phí lái xe - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakkbc", Name = "KKNYG - Học phí lái xe - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakkxd", Name = "KKNYG - Học phí lái xe - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan", Name = "KKNYG - Than" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakk", Name = "KKNYG - Than - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakkbc", Name = "KKNYG -Than - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", Name = "KKNYG - Than - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay", Name = "KKNYG - Giấy" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakk", Name = "KKNYG - Giấy - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakkbc", Name = "KKNYG - Giấy - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakkxd", Name = "KKNYG - Giấy - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn", Name = "KKNYG - Thức ăn chăn nuôi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakk", Name = "KKNYG - Thức ăn chăn nuôi - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakkbc", Name = "KKNYG - Thức ăn chăn nuôi - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakkxd", Name = "KKNYG - Thức ăn chăn nuôi - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap", Name = "KKNYG - Đất san lấp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakk", Name = "KKNYG - Đất san lấp - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkbc", Name = "KKNYG - Đất san lấp - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkxd", Name = "KKNYG - Đất san lấp - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung", Name = "KKNYG - Đá xây dựng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakk", Name = "KKNYG - Đá xây dựng - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkbc", Name = "KKNYG - Đá xây dựng - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkxd", Name = "KKNYG - Đá xây dựng - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb", Name = "KKNYG - Vận tải xe buýt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakk", Name = "KKNYG - Vận tải xe buýt - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakkbc", Name = "KKNYG - Vận tải xe buýt - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakkxd", Name = "KKNYG - Vận tải xe buýt - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk", Name = "KKNYG - Vận tải xe khách" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakk", Name = "KKNYG - Vận tải xe khách - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakkbc", Name = "KKNYG - Vận tải xe khách - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakkxd", Name = "KKNYG - Vận tải xe khách - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx", Name = "KKNYG - Vận tải xe taxi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk", Name = "KKNYG - Vận tải xe taxi - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkbc", Name = "KKNYG - Vận tải xe taxi - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkxd", Name = "KKNYG - Vận tải xe taxi - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk", Name = "KKNYG - Cước vận chuyển hành khách" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakk", Name = "KKNYG - Cước vận chuyển hành khách - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakkbc", Name = "KKNYG - Cước vận chuyển hành khách - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakkxd", Name = "KKNYG - Cước vận chuyển hành khách - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue", Name = "KKNYG - Ca huế" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakk", Name = "KKNYG - Ca huế - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakkbc", Name = "KKNYG - Ca huế - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakkxd", Name = "KKNYG - Ca huế - Xét duyệt" });
            //4.Quản lý giá thị trường hàng hóa, dịch vụ khác do UBND tỉnh, thành phố trực thuộc trung ương và các Bộ quản lý ngành, lĩnh vực tự quy định thuộc nội dung CSDL giá của mình
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk", Name = "Giá HH-DV khác" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.dm", Name = "Giá HH-DV khác - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.dmdv", Name = "Giá HH-DV khác - Danh mục đơn vị" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.tt", Name = "Giá HH-DV khác - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.xd", Name = "Giá HH-DV khác - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.th", Name = "Giá HH-DV khác - Tổng hợp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.tk", Name = "Giá HH-DV khác - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.hhdvk.bc", Name = "Giá HH-DV khác - Báo cáo" });
            //5. Giá tài sản công theo quy định của pháp luật về quản lý, sử dụng tài sản nhà nước; tài sản tài chính, tài sản vô hình
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong", Name = "Giá tài sản công" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong.thongtin", Name = "Giá tài sản công - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong.xetduyet", Name = "Giá tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.taisancong.timkiem", Name = "Giá tài sản công - Tìm kiếm" });
            //6.Giá lệ phí trước bạ
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi", Name = "Giá lệ phí trước bạ" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.danhmuc", Name = "Giá lệ phí trước bạ - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.thongtin", Name = "Giá lệ phí trước bạ - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.xetduyet", Name = "Giá lệ phí trước bạ - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.lephi.timkiem", Name = "Giá lệ phí trước bạ - Tìm kiếm" });
            //7.Khung giá đất do chính phủ ban hành, bảng giá đất cho UBND tỉnh, thành phố trực thuộc trung ương ban hành. Giá trúng đấu giá quyền sử dụng đất tại địa phương. Giá đất giao dịch thực tế trên thị trường
            //7.1 Khung giá đất do chính phủ ban hành
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd", Name = "ĐG - Khung giá đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.thongtin", Name = "ĐG - Khung giá đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.xetduyet", Name = "ĐG - Khung giá đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.timkiem", Name = "ĐG - Khung giá đất- Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.baocao", Name = "ĐG - Khung giá đất - Báo cáo" });
            //7.2 Bảng giá đất cho UBND tỉnh, thành phố trực thuộc trung ương ban hành
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb", Name = "Bảng giá các loại đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.thongtu", Name = "Bảng giá các loại đất - Thông tư" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.thongtin", Name = "Bảng giá các loại đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.xetduyet", Name = "Bảng giá các loại đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.giadatdb.timkiem", Name = "Bảng giá các loại đất - Tìm kiếm" });
            //7.3 Giá trúng đấu giá quyền sử dụng đất tại địa phương
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat", Name = "ĐG - Giá trúng thầu đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.thongtin", Name = "ĐG - Giá trúng thầu đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.danhmuc", Name = "ĐG - Giá trúng thầu đất - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", Name = "ĐG - Giá trúng thầu đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.timkiem", Name = "ĐG - Giá trúng thầu đất - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.baocao", Name = "ĐG - Giá trúng thầu đất - Báo cáo" });
            //7.4 Giá đất giao dịch thực tế trên thị trường
            //8. Giá giao dịch bất động sản
            //9. Giá trúng thầu của hàng hóa, dịch vụ được mua sắm theo quy định của pháp luật về đấu thầu
            //10. Gia thị trường hàng hóa, dịch vụ cần thiết khác phục vụ công tác dự báo và quản lý nhà nước về giá
            //10.1 Quản lý Giá hàng hóa thị trường theo thông tư 116 / 2018 / TT - BTC của Bộ Tài chính
            //10.2 Quản lý Giá hàng hóa thị trường khác ( giá hàng hoá tại các siêu thị)

            //II.Quản lý CSDL về thẩm định giá tại địa phương
            roldelist.Add(new VMRoleList { Role = "csdlvethamdinhgiataidiaphuong", Name = "CSDL VỀ THẨM ĐỊNH GIÁ TẠI ĐỊA PHƯƠNG" });
            //III.Các loại giá theo yêu cầu quản lý của địa phương
            roldelist.Add(new VMRoleList { Role = "cacloaigiatheoyeucauquanlytaidiaphuong", Name = "CÁC LOẠI GIÁ THEO YÊU CẦU QUẢN LÝ TẠI ĐỊA PHƯƠNG" });
            //IV.Tổng hợp báo cáo
            roldelist.Add(new VMRoleList { Role = "tonghopbaocao", Name = "TỔNG HỢP BÁO CÁO" });
            //V.Văn bản quản lý nhà nước về giá
            roldelist.Add(new VMRoleList { Role = "vanbanquanlynhanuocvegia", Name = "VĂN BẢN QUẢN LÝ NHÀ NƯỚC VỀ GIÁ" });
            //VI.Các thông tin phục vụ công tác quản lý nhà nước về giá
            roldelist.Add(new VMRoleList { Role = "thongtinphucvucongtacquanlynhanuocvegia", Name = "THÔNG TIN PHỤC VỤ CÔNG TÁC QUẢN LÝ NHÀ NƯỚC VỀ GIÁ" });
            //VII.QUẢN TRỊ HỆ THỐNG
            roldelist.Add(new VMRoleList { Role = "hethong", Name = "QUẢN TRỊ HỆ THỐNG" });


            return roldelist;
        }

        public static List<VMRoleList> GetRoleListcothexoa()
        {
            List<VMRoleList> roldelist = new List<VMRoleList> { };
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv", Name = "CSDL VỀ MỨC GIÁ HÀNG HÓA, DỊCH VỤ" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //Chức năng định giá

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia", Name = "Định giá(ĐG)" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn", Name = "ĐG - Giá thuế tài nguyên" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", Name = "ĐG - Giá thuế tài nguyên, mặt nước - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.thongtin", Name = "ĐG - Giá thuế tài nguyên - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.xetduyet", Name = "ĐG - Giá thuế tài nguyên - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.timkiem", Name = "ĐG - Giá thuế tài nguyên - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.baocao", Name = "ĐG - Giá thuế tài nguyên - Báo cáo" });

         

      

           

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha", Name = "ĐG - Giá thuê mua nhà ở" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.danhmuc", Name = "ĐG - Giá thuê mua nhà ở - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", Name = "ĐG - Giá thuê mua nhà ở - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", Name = "ĐG - Giá thuê mua nhà ở - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.timkiem", Name = "ĐG - Giá thuê mua nhà ở - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.baocao", Name = "ĐG - Giá thuê mua nhà ở - Báo cáo" });

     

            

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.baocao", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Báo cáo" });


            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.thongtin", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.xetduyet", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.timkiem", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.baocao", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia", Name = "ĐG - Khung giá sản phẩm, dịch vụ" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.danhmuc", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.thongtin", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.xetduyet", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.timkiem", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.baocao", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida", Name = "ĐG - Giá sản phẩm dịch vụ tối đa" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.danhmuc", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.xetduyet", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.timkiem", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.baocao", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.danhmuc", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.thue", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Thuế" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu- Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.xetduyet", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.timkiem", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.baocao", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths", Name = "ĐG - Giá tài sản trong tố tụng hình sự" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.thongtin", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.xetduyet", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.timkiem", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.baocao", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Báo cáo" });



            

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn", Name = "ĐG - Giá thuê mặt đất mặt nước" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.thongtin", Name = "ĐG - Giá thuê mặt đất mặt nước - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.xetduyet", Name = "ĐG - Giá thuê mặt đất mặt nước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.timkiem", Name = "ĐG - Giá thuê mặt đất mặt nước - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.baocao", Name = "ĐG - Giá thuê mặt đất mặt nước - Báo cáo" });

     

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc", Name = "ĐG - Giá thuê tài sản công" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.thongtin", Name = "ĐG - Giá thuê tài sản công - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.danhmuc", Name = "ĐG - Giá thuê tài sản công - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.xetduyet", Name = "ĐG - Giá thuê tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.timkiem", Name = "ĐG - Giá thuê tài sản công - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.baocao", Name = "ĐG - Giá thuê tài sản công - Báo cáo" });

            

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi", Name = "ĐG - Giá cây trồng vật nuôi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.thongtin", Name = "ĐG - Giá cây trồng vật nuôi - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.danhmuc", Name = "ĐG - Giá cây trồng vật nuôi - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.xetduyet", Name = "ĐG - Giá cây trồng vật nuôi - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.timkiem", Name = "ĐG - Giá cây trồng vật nuôi - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.baocao", Name = "ĐG - Giá cây trồng vật nuôi- Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi", Name = "ĐG - Giá xây dựng mới" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.thongtin", Name = "ĐG - Giá xây dựng mới - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.danhmuc", Name = "ĐG - Giá xây dựng mới - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.xetduyet", Name = "ĐG - Giá xây dựng mới - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.timkiem", Name = "ĐG - Giá xây dựng mới - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.baocao", Name = "ĐG - Giá xây dựng mới- Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien", Name = "ĐG - Giá cho thuê nhà ở sinh viên" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.thongtin", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.danhmuc", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.xetduyet", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.timkiem", Name = "ĐG -Giá cho thuê nhà ở sinh viên - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.baocao", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //chức năng bình ổn giá
            

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //chức năng kê khai giá
          
           

            roldelist.Add(new VMRoleList { Role = "", Name = "" });
            //Chức năng giá khác


           

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat", Name = "ĐG - Giá trúng thầu quyền sử dụng đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.thongtin", Name = "ĐG - Giá trúng thầu quyền sử dụng đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", Name = "ĐG - Giá trúng thầu quyền sử dụng đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.baocao", Name = "ĐG - Giá trúng thầu quyền sử dụng đất - Báo cáo" });

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
        public static List<VMRoleList> GetRoleListcu()
        {
            List<VMRoleList> roldelist = new List<VMRoleList> { };
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv", Name = "CSDL VỀ MỨC GIÁ HÀNG HÓA, DỊCH VỤ" });

            roldelist.Add(new VMRoleList { Role = "", Name = "" });

            //Chức năng định giá

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia", Name = "Định giá(ĐG)" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn", Name = "ĐG - Giá thuế tài nguyên" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", Name = "ĐG - Giá thuế tài nguyên, mặt nước - Danh mục" });
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
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.danhmucvung", Name = "ĐG - Giá nước sạch sinh hoạt - Danh mục vùng sử dụng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", Name = "ĐG - Giá nước sạch sinh hoạt - Danh mục khung giá sử dụng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", Name = "ĐG - Giá nước sạch sinh hoạt - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet", Name = "ĐG - Giá nước sạch sinh hoạt - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.timkiem", Name = "ĐG - Giá nước sạch sinh hoạt - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.baocao", Name = "ĐG - Giá nước sạch sinh hoạt - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong", Name = "ĐG - Giá thuê tài sản công" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.danhmuc", Name = "ĐG - Giá thuê tài sản công - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", Name = "ĐG - Giá thuê tài sản công - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", Name = "ĐG - Giá thuê tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.timkiem", Name = "ĐG - Giá thuê tài sản công - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetaisancong.baocao", Name = "ĐG - Giá thuê tài sản công - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.dichvucongich.baocao", Name = "ĐG - Giá SP, DVCI, DVSNC, HH- DV đặt hàng - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao", Name = "ĐG - Dịch vụ giáo dục đào tạo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc", Name = "ĐG - Dịch vụ giáo dục đào tạo - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", Name = "ĐG - Dịch vụ giáo dục đào tạo - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.xetduyet", Name = "ĐG - Dịch vụ giáo dục đào tạo - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.timkiem", Name = "ĐG - Dịch vụ giáo dục đào tạo - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.giaoducdaotao.baocao", Name = "ĐG - Dịch vụ giáo dục đào tạo - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh", Name = "ĐG - Dịch vụ khám chữa bệnh" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc", Name = "ĐG - Dịch vụ khám chữa bệnh - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", Name = "ĐG - Dịch vụ khám chữa bệnh - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", Name = "ĐG - Dịch vụ khám chữa bệnh - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.timkiem", Name = "ĐG - Dịch vụ khám chữa bệnh - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", Name = "ĐG - Dịch vụ khám chữa bệnh - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc", Name = "ĐG - Mức trợ giá trợ cước" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.danhmuc", Name = "ĐG - Mức trợ giá trợ cước - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", Name = "ĐG - Mức trợ giá trợ cước - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.xetduyet", Name = "ĐG - Mức trợ giá trợ cước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.timkiem", Name = "ĐG - Mức trợ giá trợ cước - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trogiatrocuoc.baocao", Name = "ĐG - Mức trợ giá trợ cước - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.danhmuc", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.thongtin", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.xetduyet", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.timkiem", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hhdvcn.baocao", Name = "ĐG - Giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.thongtin", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.xetduyet", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.timkiem", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvcuthe.baocao", Name = "ĐG - Giá sản phẩm dịch vụ cụ thể - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia", Name = "ĐG - Khung giá sản phẩm, dịch vụ" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.danhmuc", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.thongtin", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.xetduyet", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.timkiem", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvkhunggia.baocao", Name = "ĐG - Khung giá sản phẩm, dịch vụ - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida", Name = "ĐG - Giá sản phẩm dịch vụ tối đa" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.danhmuc", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.xetduyet", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.timkiem", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvtoida.baocao", Name = "ĐG - Giá sản phẩm dịch vụ tối đa - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.danhmuc", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.thue", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Thuế" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu- Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.xetduyet", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.timkiem", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.hqxnk.baocao", Name = "ĐG - Giá hàng hoá hải quan trong xuất nhập khẩu - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths", Name = "ĐG - Giá tài sản trong tố tụng hình sự" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.thongtin", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.xetduyet", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.timkiem", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.tths.baocao", Name = "ĐG - Giá tài sản trong tố tụng hình sự - Báo cáo" });


            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd", Name = "ĐG - Khung giá đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.thongtin", Name = "ĐG - Khung giá đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.xetduyet", Name = "ĐG - Khung giá đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.timkiem", Name = "ĐG - Khung giá đất- Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.khunggd.baocao", Name = "ĐG - Khung giá đất - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci", Name = "ĐG - Sản phẩm dịch vụ công ích" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.danhmuc", Name = "ĐG - Sản phẩm dịch vụ công ích - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.thongtin", Name = "ĐG - Sản phẩm dịch vụ công ích - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.xetduyet", Name = "ĐG - Sản phẩm dịch vụ công ích - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.timkiem", Name = "ĐG - Sản phẩm dịch vụ công ích- Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.spdvci.baocao", Name = "ĐG - Sản phẩm dịch vụ công ích - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn", Name = "ĐG - Giá thuê mặt đất mặt nước" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.thongtin", Name = "ĐG - Giá thuê mặt đất mặt nước - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.xetduyet", Name = "ĐG - Giá thuê mặt đất mặt nước - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.timkiem", Name = "ĐG - Giá thuê mặt đất mặt nước - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetn.baocao", Name = "ĐG - Giá thuê mặt đất mặt nước - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh", Name = "ĐG - Giá thuê mua nhà xã hội" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", Name = "ĐG - Giá thuê mua nhà xã hội - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.danhmuc", Name = "ĐG - Giá thuê mua nhà xã hội - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.xetduyet", Name = "ĐG - Giá thuê mua nhà xã hội - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.timkiem", Name = "ĐG - Giá thuê mua nhà xã hội - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanhaxh.baocao", Name = "ĐG - Giá thuê mua nhà xã hội - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc", Name = "ĐG - Giá thuê tài sản công" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.thongtin", Name = "ĐG - Giá thuê tài sản công - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.danhmuc", Name = "ĐG - Giá thuê tài sản công - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.xetduyet", Name = "ĐG - Giá thuê tài sản công - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.timkiem", Name = "ĐG - Giá thuê tài sản công - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuetsc.baocao", Name = "ĐG - Giá thuê tài sản công - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat", Name = "ĐG - Giá trúng thầu đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.thongtin", Name = "ĐG - Giá trúng thầu đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.danhmuc", Name = "ĐG - Giá trúng thầu đất - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", Name = "ĐG - Giá trúng thầu đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.timkiem", Name = "ĐG - Giá trúng thầu đất - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.baocao", Name = "ĐG - Giá trúng thầu đất - Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi", Name = "ĐG - Giá cây trồng vật nuôi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.thongtin", Name = "ĐG - Giá cây trồng vật nuôi - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.danhmuc", Name = "ĐG - Giá cây trồng vật nuôi - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.xetduyet", Name = "ĐG - Giá cây trồng vật nuôi - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.timkiem", Name = "ĐG - Giá cây trồng vật nuôi - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.baocao", Name = "ĐG - Giá cây trồng vật nuôi- Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi", Name = "ĐG - Giá xây dựng mới" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.thongtin", Name = "ĐG - Giá xây dựng mới - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.danhmuc", Name = "ĐG - Giá xây dựng mới - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.xetduyet", Name = "ĐG - Giá xây dựng mới - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.timkiem", Name = "ĐG - Giá xây dựng mới - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.xaydungmoi.baocao", Name = "ĐG - Giá xây dựng mới- Báo cáo" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien", Name = "ĐG - Giá cho thuê nhà ở sinh viên" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.thongtin", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.danhmuc", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Danh mục" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.xetduyet", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.timkiem", Name = "ĐG -Giá cho thuê nhà ở sinh viên - Tìm kiếm" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nhaosinhvien.baocao", Name = "ĐG - Giá cho thuê nhà ở sinh viên - Báo cáo" });

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
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakkbc", Name = "KKNYG - Dịch vụ lưu trú - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakkxd", Name = "KKNYG - Dịch vụ lưu trú - Xét duyệt" });


            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd", Name = "KKNYG - Xi măng, thép xây dựng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk", Name = "KKNYG - Xi măng, thép xây dựng - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkxd", Name = "KKNYG - Xi măng, thép xây dựng - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach", Name = "KKNYG - Sách giáo khoa" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakk", Name = "KKNYG - Sách giáo khoa - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakkbc", Name = "KKNYG - Sách giáo khoa - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakkxd", Name = "KKNYG - Sách giáo khoa - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol", Name = "KKNYG - Etanol" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakkbc", Name = "KKNYG - Etanol - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakk", Name = "KKNYG - Etanol - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakkxd", Name = "KKNYG - Etanol - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn", Name = "KKNYG - Thực phẩm chức năng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakk", Name = "KKNYG - Thực phẩm chức năng - Kê khai giá " });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakkbc", Name = "KKNYG - Thực phẩm chức năng - Báo cáo " });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakkxd", Name = "KKNYG - Thực phẩm chức năng - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan", Name = "KKNYG - Cát sạn" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakk", Name = "KKNYG - Cát sạn - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakkbc", Name = "KKNYG - Cát sạn - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakkxd", Name = "KKNYG - Cát sạn - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx", Name = "KKNYG - Học phí lái xe" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakk", Name = "KKNYG - Học phí lái xe - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakkbc", Name = "KKNYG - Học phí lái xe - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakkxd", Name = "KKNYG - Học phí lái xe - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan", Name = "KKNYG - Than" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakk", Name = "KKNYG - Than - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakkbc", Name = "KKNYG -Than - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", Name = "KKNYG - Than - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay", Name = "KKNYG - Giấy" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakk", Name = "KKNYG - Giấy - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakkbc", Name = "KKNYG - Giấy - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakkxd", Name = "KKNYG - Giấy - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn", Name = "KKNYG - Thức ăn chăn nuôi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakk", Name = "KKNYG - Thức ăn chăn nuôi - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakkbc", Name = "KKNYG - Thức ăn chăn nuôi - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakkxd", Name = "KKNYG - Thức ăn chăn nuôi - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap", Name = "KKNYG - Đất san lấp" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakk", Name = "KKNYG - Đất san lấp - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkbc", Name = "KKNYG - Đất san lấp - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkxd", Name = "KKNYG - Đất san lấp - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung", Name = "KKNYG - Đá xây dựng" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakk", Name = "KKNYG - Đá xây dựng - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkbc", Name = "KKNYG - Đá xây dựng - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkxd", Name = "KKNYG - Đá xây dựng - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb", Name = "KKNYG - Vận tải xe buýt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakk", Name = "KKNYG - Vận tải xe buýt - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakkbc", Name = "KKNYG - Vận tải xe buýt - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakkxd", Name = "KKNYG - Vận tải xe buýt - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk", Name = "KKNYG - Vận tải xe khách" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakk", Name = "KKNYG - Vận tải xe khách - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakkbc", Name = "KKNYG - Vận tải xe khách - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakkxd", Name = "KKNYG - Vận tải xe khách - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx", Name = "KKNYG - Vận tải xe taxi" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk", Name = "KKNYG - Vận tải xe taxi - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkbc", Name = "KKNYG - Vận tải xe taxi - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkxd", Name = "KKNYG - Vận tải xe taxi - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk", Name = "KKNYG - Cước vận chuyển hành khách" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakk", Name = "KKNYG - Cước vận chuyển hành khách - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakkbc", Name = "KKNYG - Cước vận chuyển hành khách - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakkxd", Name = "KKNYG - Cước vận chuyển hành khách - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue", Name = "KKNYG - Ca huế" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakk", Name = "KKNYG - Ca huế - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakkbc", Name = "KKNYG - Ca huế - Báo cáo" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakkxd", Name = "KKNYG - Ca huế - Xét duyệt" });

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsieuthi", Name = "KKNYG - Siêu thị" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsieuthi.giakk", Name = "KKNYG - Siêu thị - Kê khai giá" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsieuthi.giakkcb", Name = "KKNYG - Siêu thị - Báo cáo" });
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

            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat", Name = "ĐG - Giá trúng thầu quyền sử dụng đất" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.thongtin", Name = "ĐG - Giá trúng thầu quyền sử dụng đất - Thông tin" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", Name = "ĐG - Giá trúng thầu quyền sử dụng đất - Xét duyệt" });
            roldelist.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.trungthaudat.baocao", Name = "ĐG - Giá trúng thầu quyền sử dụng đất - Báo cáo" });

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

        //public static string[] GetRolesList()
        //{
        //    string[] roles = new string[]
        //    {
        //        "csdlmucgiahhdv",
        //        //chức năng định giá
        //        "csdlmucgiahhdv.dinhgia",
        //        "csdlmucgiahhdv.dinhgia.datcuthe",
        //        "csdlmucgiahhdv.dinhgia.datcuthe.thongtin",
        //        "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.thuedatnuoc",
        //        "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin",
        //        "csdlmucgiahhdv.dinhgia.thuedatnuoc.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.rung",
        //        "csdlmucgiahhdv.dinhgia.rung.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.rung.thongtin",
        //        "csdlmucgiahhdv.dinhgia.rung.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.thuemuanha",
        //        "csdlmucgiahhdv.dinhgia.thuemuanha.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin",
        //        "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.nuocsh",
        //        "csdlmucgiahhdv.dinhgia.nuocsh.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.nuocsh.thongtin",
        //        "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet",

        //        "csdlmucgiahhdv.dinhgia.giaoducdaotao",
        //        "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin",
        //        "csdlmucgiahhdv.dinhgia.giaoducdaotao.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.giaoducdaotao.timkiem",
        //        "csdlmucgiahhdv.dinhgia.khamchuabenh",
        //        "csdlmucgiahhdv.dinhgia.khamchuabenh.nhom",
        //        "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin",
        //        "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.khamchuabenh.timkiem",
        //        "csdlmucgiahhdv.dinhgia.trogiatrocuoc",
        //        "csdlmucgiahhdv.dinhgia.trogiatrocuoc.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin",
        //        "csdlmucgiahhdv.dinhgia.trogiatrocuoc.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.trogiatrocuoc.timkiem",
        //        "csdlmucgiahhdv.dinhgia.dichvucongich",
        //        "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin",
        //        "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem",
        //        "csdlmucgiahhdv.dinhgia.thuetaisancong",
        //        "csdlmucgiahhdv.dinhgia.thuetaisancong.danhmuc",
        //        "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin",
        //        "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet",
        //        "csdlmucgiahhdv.dinhgia.thuetaisancong.timkiem",
        //        // Tài sản công
        //        "csdlmucgiahhdv.taisancong",
        //        "csdlmucgiahhdv.taisancong.thongtin",
        //        "csdlmucgiahhdv.taisancong.xetduyet",
        //        "csdlmucgiahhdv.taisancong.timkiem",
        //        //trúng thầu mua hàng hóa dịch vụ
        //        "csdlmucgiahhdv.muataisan",
        //        "csdlmucgiahhdv.muataisan.thongtin",
        //        "csdlmucgiahhdv.muataisan.xetduyet",
        //        "csdlmucgiahhdv.muataisan.timkiem",
        //        //trung thầu quyền sd đất
        //        "csdlmucgiahhdv.daugiadat",
        //        "csdlmucgiahhdv.daugiadat.thongtin",
        //        "csdlmucgiahhdv.daugiadat.xetduyet",
        //        "csdlmucgiahhdv.daugiadat.timkiem",
        //        //giá lệ phí trước bạ
        //        "csdlmucgiahhdv.lephi",
        //        "csdlmucgiahhdv.lephi.danhmuc",
        //        "csdlmucgiahhdv.lephi.thongtin",
        //        "csdlmucgiahhdv.lephi.xetduyet",
        //        "csdlmucgiahhdv.lephi.timkiem",




        //        //chức năng bình ổn giá
        //        "csdlmucgiahhdv.bog",
        //        "csdlmucgiahhdv.bog.ttdn",
        //        "csdlmucgiahhdv.bog.xdtttddn",
        //        "csdlmucgiahhdv.bog.phanloai",
        //        "csdlmucgiahhdv.bog.thongtin",
        //        "csdlmucgiahhdv.bog.xetduyet",
        //        "csdlmucgiahhdv.bog.timkiem",
        //        "csdlmucgiahhdv.bog.baocao",




        //        //chức năng kê khai giá
        //        "csdlmucgiahhdv.kknygia",
        //        "csdlmucgiahhdv.kknygia.ttdn",
        //        "csdlmucgiahhdv.kknygia.xdtttddn",
        //        "csdlmucgiahhdv.kknygia.kkgxmtxd",
        //        "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgdvlt",
        //        "csdlmucgiahhdv.kknygia.kkgdvlt.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgdvlt.giakkxd",

        //        "csdlmucgiahhdv.kknygia.kkgsach",
        //        "csdlmucgiahhdv.kknygia.kkgsach.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgsach.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgetanol",
        //        "csdlmucgiahhdv.kknygia.kkgetanol.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgetanol.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgtpcn",
        //        "csdlmucgiahhdv.kknygia.kkgtpcn.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgtpcn.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgcatsan",
        //        "csdlmucgiahhdv.kknygia.kkgcatsan.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgcatsan.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkghplx",
        //        "csdlmucgiahhdv.kknygia.kkghplx.giakk",
        //        "csdlmucgiahhdv.kknygia.kkghplx.giakkxd",

        //        "csdlmucgiahhdv.kknygia.kkgthan",
        //        "csdlmucgiahhdv.kknygia.kkgthan.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgthan.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkggiay",
        //        "csdlmucgiahhdv.kknygia.kkggiay.giakk",
        //        "csdlmucgiahhdv.kknygia.kkggiay.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgtacn",
        //        "csdlmucgiahhdv.kknygia.kkgtacn.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgtacn.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgdatsanlap",
        //        "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgdaxaydung",
        //        "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkxd",


        //        "csdlmucgiahhdv.kknygia.kkgvtxb",
        //        "csdlmucgiahhdv.kknygia.kkgvtxb.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgvtxb.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgvtxk",
        //        "csdlmucgiahhdv.kknygia.kkgvtxk.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgvtxk.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgvtxtx",
        //        "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkgcahue",
        //        "csdlmucgiahhdv.kknygia.kkgcahue.giakk",
        //        "csdlmucgiahhdv.kknygia.kkgcahue.giakkxd",
        //        "csdlmucgiahhdv.kknygia.kkcvchk",
        //        "csdlmucgiahhdv.kknygia.kkcvchk.giakk",
        //        "csdlmucgiahhdv.kknygia.kkcvchk.giakkxd",

        //        //Hệ thống
        //        "hethong",
        //        "hethong.nguoidung",
        //        "hethong.nguoidung.dsnhomtaikhoan",
        //        "hethong.nguoidung.dstaikhoan",
        //        "hethong.nguoidung.dstaikhoan.phanquyen",
        //        "hethong.nguoidung.dsdangky",
        //        "hethong.hethong",
        //        "hethong.hethong.dsdiaban",
        //        "hethong.hethong.dsdonvi",
        //        "hethong.hethong.dsxaphuong",
        //        "hethong.danhmuc",
        //        "hethong.danhmuc.dmnganhnghekd",

        //    };
        //    return roles;

        //}

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
            string str = datetime.ToString("dd/MM/yyyy HH:mm:ss,fff tt");
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
        public static List<VMGetAPIThietLap> getAPIThietLapMacDinh()
        {
            //Thiết lập chung
            List<VMGetAPIThietLap> list = new List<VMGetAPIThietLap> { };
            list.Add(new VMGetAPIThietLap { Stt = 1, Phanloai = "Header", Tendong = "Version", Mota = "Tên phiên bản XML truyền nhận dữ liệu", Kieudulieu = "String", Dinhdang = "", Dodai = "250", Batbuoc = true, Macdinh = "1.0", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 2, Phanloai = "Header", Tendong = "Sender_Code", Mota = "Mã nơi gửi, giá trị thay đổi qua các nút truyền dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "50", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 3, Phanloai = "Header", Tendong = "Sender_Name", Mota = "Tên nơi gửi, giá trị thay đổi qua các nút truyền dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "250", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 4, Phanloai = "Header", Tendong = "Receiver_Code", Mota = "Mã nơi nhận, giá trị thay đổi qua các nút truyền dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "50", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 5, Phanloai = "Header", Tendong = "Receiver_Name", Mota = "Tên nơi nhận, giá trị thay đổi qua các nút truyền dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "250", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 6, Phanloai = "Header", Tendong = "Tran_Code", Mota = "Mã loại dữ liệu trao đổi.", Kieudulieu = "String", Dinhdang = "", Dodai = "10", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 7, Phanloai = "Header", Tendong = "Tran_Name", Mota = "Tên loại dữ liệu trao đổi.", Kieudulieu = "String", Dinhdang = "", Dodai = "150", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 8, Phanloai = "Header", Tendong = "Msg_ID", Mota = "Mã gói tin. Mã gói tin sẽ thay đổi qua các nút truyền dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "50", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 9, Phanloai = "Header", Tendong = "Msg_RefID", Mota = "Mã gói tham chiếu. Đây là mã gói được sinh ra tại ứng dụng gốc qua các nút truyền nhận mã không thay đổi.", Kieudulieu = "String", Dinhdang = "", Dodai = "50", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 10, Phanloai = "Header", Tendong = "Send_Date", Mota = "Ngày gửi gói tin, giá trị Send Date thay đổi qua các nút truyền dữ liệu.", Kieudulieu = "String", Dinhdang = "DD/MM/YYYY HH24:MI:SS", Dodai = "19", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 11, Phanloai = "Header", Tendong = "Original_Code", Mota = "Mã gốc nơi gửi dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "50", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 12, Phanloai = "Header", Tendong = "Original_name", Mota = "Tên gốc nơi gửi dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "250", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 13, Phanloai = "Header", Tendong = "Export_Date", Mota = "Ngày đóng gói gói tin tại ứng dụng nguồn, khi gửi qua các nút truyền dữ liệu thì giá trị Export_Date không thay đổi.", Kieudulieu = "String", Dinhdang = "DD/MM/YYYY HH24:MI:SS", Dodai = "19", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 14, Phanloai = "Header", Tendong = "Notes", Mota = "Trường hợp này phục vụ rẽ nhánh dữ liệu trong trường hợp cùng một mã loại dữ liệu được gửi cho nhiều nơi khác nhau nhưng thông tin chi tiết của gói tin không giống nhau. Trục sẽ sử dụng thông tin này để gửi đến đúng đích..", Kieudulieu = "String", Dinhdang = "", Dodai = "5", Batbuoc = false, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 15, Phanloai = "Header", Tendong = "Tran_Num", Mota = "Tổng số dòng trong phần body.", Kieudulieu = "String", Dinhdang = "", Dodai = "5", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 16, Phanloai = "Header", Tendong = "Path", Mota = "Đường dẫn của gói tin. Mỗi gói tin đi qua nút chuyển dữ liệu, nút đó điền thêm thông tin vào đường dẫn của gói tin này.", Kieudulieu = "String", Dinhdang = "", Dodai = "50", Batbuoc = true, Macdinh = "", Ghichu = "" });
            list.Add(new VMGetAPIThietLap { Stt = 17, Phanloai = "Header", Tendong = "NumMsg_InGroup", Mota = "Số lượng của gói tin tách ra, thành bao nhiêu gói tin nhỏ.", Kieudulieu = "String", Dinhdang = "", Dodai = "3", Batbuoc = true, Macdinh = "", Ghichu = "Khi một gói tin có số lượng dòng lớn hơn 5000 phải tách thành các gói tin nhỏ hơn (gói lớn nhất có số dòng = 5000)" });
            list.Add(new VMGetAPIThietLap { Stt = 18, Phanloai = "Header", Tendong = "SPARE1", Mota = "Trường thông tin dự phòng. Hiện tại, dữ liệu xuất phát từ DMDC sử dụng để đưa thông tin từ user webservice được hệ thống DMDC cấp cho ứng dụng để trao đổi dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "10", Batbuoc = false, Macdinh = "", Ghichu = "Hệ thống DMDC cung cấp qua văn bản đến các ứng dụng" });
            list.Add(new VMGetAPIThietLap { Stt = 19, Phanloai = "Header", Tendong = "SPARE2", Mota = "Trường thông tin dự phòng. Hiện tại, dữ liệu xuất phát từ DMDC sử dụng để đưa thông tin từ mật khẩu webservice được hệ thống DMDC cấp cho ứng dụng để trao đổi dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "10", Batbuoc = false, Macdinh = "", Ghichu = "Hệ thống DMDC cung cấp qua văn bản đến các ứng dụng" });
            list.Add(new VMGetAPIThietLap { Stt = 20, Phanloai = "Header", Tendong = "SPARE3", Mota = "Trường thông tin dự phòng. Hiện tại, dữ liệu xuất phát từ DMDC sử dụng để đưa thông tin từ giá trị quy định DMDC nhận dữ liệu hay cung cấp dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "10", Batbuoc = false, Macdinh = "", Ghichu = "0: PUT (đẩy dữ liệu)1: GET (Nhận dữ liệu)" });
            list.Add(new VMGetAPIThietLap { Stt = 21, Phanloai = "Header", Tendong = "Finish_Code", Mota = "Dùng để phân biệt gói phản hồi đối soát dữ liệu.", Kieudulieu = "String", Dinhdang = "", Dodai = "0", Batbuoc = false, Macdinh = "", Ghichu = "" });

            //Thiết lập Hồ sơ kê khai giá HOSO
            list.Add(new VMGetAPIThietLap { Tentruong = "plhs", Macdinh = "", Dinhdang = "", Stt = 1, Tendong = "LOAI_HO_SO", Kieudulieu = "NUMBER", Dodai = "1", Batbuoc = true, Ghichu = "NUMBER", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "0", Dinhdang = "", Stt = 2, Tendong = "LOAI_XNK", Kieudulieu = "NUMBER", Dodai = "1", Batbuoc = true, Ghichu = "NUMBER", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "madv", Macdinh = "", Dinhdang = "", Stt = 3, Tendong = "DOANH_NGHIEP_DKKK", Kieudulieu = "STRING", Dodai = "100", Batbuoc = true, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "socv", Macdinh = "", Dinhdang = "", Stt = 4, Tendong = "SO_VAN_BAN", Kieudulieu = "STRING", Dodai = "100", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ngaynhap", Macdinh = "", Dinhdang = "DD/MM/YY", Stt = 5, Tendong = "NGAY_THUC_HIEN", Kieudulieu = "STRING(DATE)", Dodai = "8", Batbuoc = true, Ghichu = "STRING(DATE)", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ngayhieuluc", Macdinh = "", Dinhdang = "DD/MM/YY", Stt = 6, Tendong = "NGAY_BD_HIEU_LUC", Kieudulieu = "STRING(DATE)", Dodai = "8", Batbuoc = true, Ghichu = "STRING(DATE)", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "", Dinhdang = "", Stt = 7, Tendong = "TY_GIA", Kieudulieu = "NUMBER", Dodai = "(18,0)", Batbuoc = false, Ghichu = "NUMBER", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "GET_NGUOI_KY", Dinhdang = "", Stt = 8, Tendong = "NGUOI_KY", Kieudulieu = "STRING", Dodai = "500", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ngaychuyen", Macdinh = "", Dinhdang = "DD/MM/YY", Stt = 9, Tendong = "NGAY_KY", Kieudulieu = "STRING(DATE)'", Dodai = "8", Batbuoc = false, Ghichu = "STRING(DATE)", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "", Dinhdang = "", Stt = 10, Tendong = "TRICH_YEU", Kieudulieu = "STRING", Dodai = "4000", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "", Dinhdang = "", Stt = 11, Tendong = "QUOC_GIA_XNK", Kieudulieu = "STRING", Dodai = "1000", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "", Dinhdang = "", Stt = 12, Tendong = "CHI_NHANH", Kieudulieu = "STRING", Dodai = "1000", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "", Dinhdang = "", Stt = 13, Tendong = "KHO_HANG", Kieudulieu = "STRING", Dodai = "1000", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "", Dinhdang = "", Stt = 14, Tendong = "TINH_THANH", Kieudulieu = "STRING", Dodai = "1000", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "", Dinhdang = "", Stt = 15, Tendong = "DOI_TUONG_AP_DUNG", Kieudulieu = "STRING", Dodai = "1000", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "mahinhthucthanhtoan", Macdinh = "dinhdang", Dinhdang = "", Stt = 16, Tendong = "HINH_THUC_THANH_TOAN", Kieudulieu = "STRING", Dodai = "1000", Batbuoc = false, Ghichu = "STRING", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "DS_HHDV", Macdinh = "", Dinhdang = "", Stt = 17, Tendong = "DS_HHDV_DKG", Kieudulieu = "OBJECT", Dodai = "", Batbuoc = true, Ghichu = "OBJECT", Phanloai = "KeKhaiGia", });

            //Thiết lập Hồ sơ kê khai giá CHITIET
            list.Add(new VMGetAPIThietLap { Tentruong = "maloaigia", Macdinh = "1", Dinhdang = "", Stt = 1, Tendong = "LOAI_GIA", Kieudulieu = "STRING", Dodai = "3", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_DKG", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "MA_HHDV", Dinhdang = "", Stt = 2, Tendong = "MA_HHDV", Kieudulieu = "STRING", Dodai = "50", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_DKG", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "dvt", Macdinh = "", Dinhdang = "", Stt = 3, Tendong = "MA_DON_VI_TINH", Kieudulieu = "STRING", Dodai = "10", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_DKG", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "giakk", Macdinh = "", Dinhdang = "", Stt = 4, Tendong = "MUC_GIA_MOI", Kieudulieu = "NUMBER", Dodai = "(18,0)", Batbuoc = true, Ghichu = "NUMBER", Tendong_goc = "DS_HHDV_DKG", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ghichu", Macdinh = "", Dinhdang = "", Stt = 5, Tendong = "GHI_CHU", Kieudulieu = "STRING", Dodai = "4000", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_DKG", Phanloai = "KeKhaiGia", });

            list.Add(new VMGetAPIThietLap { Tentruong = "ghichu1", Macdinh = "", Dinhdang = "", Stt = 1, Tendong = "GHI_CHU1", Kieudulieu = "STRING", Dodai = "4000", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "TINH_THANH", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ghichu2", Macdinh = "", Dinhdang = "", Stt = 2, Tendong = "GHI_CHU2", Kieudulieu = "STRING", Dodai = "4000", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "TINH_THANH", Phanloai = "KeKhaiGia", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ghichu3", Macdinh = "", Dinhdang = "", Stt = 3, Tendong = "GHI_CHU3", Kieudulieu = "STRING", Dodai = "4000", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "TINH_THANH", Phanloai = "KeKhaiGia", });

            //Thiết lập Hồ sơ hàng hóa thị trường HOSO
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "GET_DIABAN", Dinhdang = "", Stt = 1, Tendong = "DIA_BAN", Kieudulieu = "STRING", Dodai = "3", Batbuoc = true, Ghichu = "STRING", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "2", Dinhdang = "", Stt = 2, Tendong = "NGUON_SO_LIEU", Kieudulieu = "STRING", Dodai = "3", Batbuoc = true, Ghichu = "STRING", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "24", Dinhdang = "", Stt = 3, Tendong = "DINH_KY", Kieudulieu = "NUMBER", Dodai = "2", Batbuoc = true, Ghichu = "NUMBER", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "1", Dinhdang = "", Stt = 4, Tendong = "THOI_GIAN_BC_1", Kieudulieu = "NUMBER", Dodai = "3", Batbuoc = true, Ghichu = "NUMBER", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "thang", Macdinh = "", Dinhdang = "", Stt = 5, Tendong = "THOI_GIAN_BC_2", Kieudulieu = "NUMBER", Dodai = "3", Batbuoc = false, Ghichu = "NUMBER", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "nam", Macdinh = "", Dinhdang = "", Stt = 6, Tendong = "THOI_GIAN_BC_NAM", Kieudulieu = "NUMBER", Dodai = "4", Batbuoc = true, Ghichu = "NUMBER", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ipf1", Macdinh = "1", Dinhdang = "", Stt = 7, Tendong = "FILE_DINH_KEM", Kieudulieu = "STRING(BASES64)", Dodai = "", Batbuoc = true, Ghichu = "STRING(BASES64)", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "DS_HHDV", Macdinh = "", Dinhdang = "", Stt = 8, Tendong = "DS_HHDV_TT", Kieudulieu = "OBJECT", Dodai = "", Batbuoc = true, Ghichu = "OBJECT", Phanloai = "giahhdvk", });

            //Thiết lập Hồ sơ hàng hóa thị trường CHITIET
            list.Add(new VMGetAPIThietLap { Tentruong = "maloaigia", Macdinh = "1", Dinhdang = "", Stt = 1, Tendong = "LOAI_GIA", Kieudulieu = "STRING", Dodai = "3", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "mahhdv", Macdinh = "", Dinhdang = "", Stt = 2, Tendong = "MA_HHDV", Kieudulieu = "STRING", Dodai = "3", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "GET_TEN_HHDV", Dinhdang = "", Stt = 3, Tendong = "TEN_HANG_HOA_DICH_VU", Kieudulieu = "STRING", Dodai = "1000", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "GET_TEN_HHDV", Dinhdang = "", Stt = 4, Tendong = "DON_VI_TINH", Kieudulieu = "STRING", Dodai = "3", Batbuoc = true, Ghichu = "STRING", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "gialk", Macdinh = "", Dinhdang = "", Stt = 5, Tendong = "GIA_KY_TRUOC", Kieudulieu = "NUMBER", Dodai = "(18,0)", Batbuoc = false, Ghichu = "NUMBER", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "gia", Macdinh = "", Dinhdang = "", Stt = 6, Tendong = "GIA_KY_NAY", Kieudulieu = "NUMBER", Dodai = "(18,0)", Batbuoc = true, Ghichu = "NUMBER", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "NULL", Macdinh = "GET_NGUONTT", Dinhdang = "", Stt = 7, Tendong = "NGUON_THONG_TIN", Kieudulieu = "NUMBER", Dodai = "1", Batbuoc = true, Ghichu = "NUMBER", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });
            list.Add(new VMGetAPIThietLap { Tentruong = "ghichu", Macdinh = "", Dinhdang = "", Stt = 8, Tendong = "GHI_CHU", Kieudulieu = "STRING", Dodai = "4000", Batbuoc = false, Ghichu = "STRING", Tendong_goc = "DS_HHDV_TT", Phanloai = "giahhdvk", });

            return list;
        }

        public static string ToAlpha(int number)
        {
            char[] alphabet = { ' ', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            if (number <= 25)
            {
                return alphabet[number].ToString();
            }
            else if (number > 25)
            {
                int dividend = (number + 1);
                string alpha = "";
                while (dividend > 0)
                {
                    int modulo = (dividend - 1) % 26;
                    alpha = alphabet[modulo] + alpha;
                    dividend = ((dividend - modulo) / 26);
                }
                return alpha;
            }
            return "";
        }

        public static string ToRoman(int number)
        {
            /*if (-9999 >= number || number <= 9999)
            {
                throw new ArgumentOutOfRangeException("number");
            }
*/
            if (number == 0)
            {
                return "NUL";
            }

            StringBuilder sb = new StringBuilder(10);

            if (number < 0)
            {
                sb.Append('-');
                number *= -1;
            }

            string[,] table = new string[,] {
            { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" },
            { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" },
            { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" },
            { "", "M", "MM", "MMM", "M(V)", "(V)", "(V)M", "(V)MM", "(V)MMM", "M(X)" }
        };

            for (int i = 1000, j = 3; i > 0; i /= 10, j--)
            {
                int digit = number / i;
                sb.Append(table[j, digit]);
                number -= digit * i;
            }

            return sb.ToString();
        }
    }
}
