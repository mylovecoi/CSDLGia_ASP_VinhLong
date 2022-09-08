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
            List<VMRoleList> list = new List<VMRoleList> { };
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv", Name = "" });
            //chức năng định giá
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia", Name = "Định giá" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe", Name = "Giá đất cụ thể" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", Name = "Giá đất cụ thể thông tin" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet", Name = "Giá đất cụ thể xét duyệt" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc", Name = "Giá thuê mặt đất, mặt nước" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", Name = "Giá thuê mặt đất, mặt nước danh mục" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", Name = "Giá thuê mặt đất, mặt nước thông tin" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuedatnuoc.xetduyet", Name = "Giá thuê mặt đất, mặt nước xét duyệt" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung", Name = "Giá rừng" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.danhmuc", Name = "Giá rừng danh mục" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.thongtin", Name = "Giá rừng thông tin" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.rung.xetduyet", Name = "Giá rừng xét duyệt" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha", Name = "Giá thuê mua nhà ở" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.danhmuc", Name = "Giá thuê mua nhà ở danh mục" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", Name = "Giá thuê mua nhà ở thông tin" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", Name = "Giá thuê mua nhà ở xét duyệt" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh", Name = "Giá nước sạch sinh hoạt" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.danhmuc", Name = "Giá nước sạch sinh hoạt danh mục" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", Name = "Giá nước sạch sinh hoạt danh mục" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet", Name = "Giá nước sạch sinh hoạt xét duyệt" });

            //chức năng bình ổn giá
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog", Name = "Mặt hàng bình ổn giá" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.ttdn", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.xdtttddn", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.phanloai", Name = "Mặt hàng bình ổn giá phân loại" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.thongtin", Name = "Mặt hàng bình ổn giá thông tin" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.xetduyet", Name = "Mặt hàng bình ổn giá xét duyệt" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.timkiem", Name = "Mặt hàng bình ổn giá tìm kiếm" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.bog.baocao", Name = "Mặt hàng bình ổn giá báo cáo" });

            //chức năng kê khai giá
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia", Name = "Mức giá kê khai - đăng ký" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.ttdn", Name = "Thông tin doanh nghiệp" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.xdtttddn", Name = "Xét duyệt thông tin thay đổi doanh nghiệp" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd", Name = "Xi măng, thép xây dựng" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk", Name = "Xi măng, thép xây dựng kê khai giá" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkxd", Name = "Xi măng, thép xây dựng xét duyệt" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt", Name = "Dịch vụ lưu trú" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", Name = "Dịch vụ lưu trú kê khai giá" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdvlt.giakkxd", Name = "Dịch vụ lưu trú xét duyệt" });

            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgsach.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgetanol.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtpcn.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcatsan.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkghplx.giakkxd", Name = "" });

            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkggiay.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgtacn.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdatsanlap.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgdaxaydung.giakkxd", Name = "" });

            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxb.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxk.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgvtxtx.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkgcahue.giakkxd", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakk", Name = "" });
            list.Add(new VMRoleList { Role = "csdlmucgiahhdv.kknygia.kkcvchk.giakkxd", Name = "" });

            //Hệ thống
            list.Add(new VMRoleList { Role = "hethong", Name = "HỆ THỐNG" });
            list.Add(new VMRoleList { Role = "hethong.nguoidung", Name = "Quản trị người dùng" });
            list.Add(new VMRoleList { Role = "hethong.nguoidung.dsnhomtaikhoan", Name = "Danh sách nhóm tài khoản" });
            list.Add(new VMRoleList { Role = "hethong.nguoidung.dstaikhoan", Name = "Danh sách tài khoản" });
            list.Add(new VMRoleList { Role = "hethong.nguoidung.dstaikhoan.phanquyen", Name = "Danh sách tài khoản phân quyền" });
            list.Add(new VMRoleList { Role = "hethong.nguoidung.dsdangky", Name = "" });
            list.Add(new VMRoleList { Role = "hethong.hethong", Name = "Quản trị hệ thống" });
            list.Add(new VMRoleList { Role = "hethong.hethong.dsdiaban", Name = "Danh sách địa bàn" });
            list.Add(new VMRoleList { Role = "hethong.hethong.dsdonvi", Name = "Danh sách đơn vị sử dụng" });
            list.Add(new VMRoleList { Role = "hethong.hethong.dsxaphuong", Name = "Danh sách xã, phường, thị trấn" });
            list.Add(new VMRoleList { Role = "hethong.danhmuc", Name = "Quản trị danh mục" });
            list.Add(new VMRoleList { Role = "hethong.danhmuc.dmnganhnghekd", Name = "Danh mục ngành nghề kinh doanh" });


            return list;
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
    }
}
