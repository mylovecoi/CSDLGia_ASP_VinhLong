using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CSDLGia_ASP
{
    public class HamDungChung
    {
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
        //Kiểm tra giao diện + phân quyền tài khoản
        //Kiểm tra level: nếu là DN thì kiểm tra xem có ở lĩnh vực kinh doanh đó ko
        //nên chia nhỏ từng bước do đã gọi các hàm lồng nhau nên mặc định là bước trước đã đúng
        //ví dụ: kiểm tra $action thì đã gọi hàm kiểm tra: $csdl -> $group -> $feature trước đó (if lồng)
        //do đó chạy thẳng đến hàm kiểm tra $action để ko pải lập lại thao tác
        //    function chkPer($csdl = null, $group = null, $feature = null, $action = null, $per = null)
        //    {
        //        //@if(chkPer('csdlmucgiahhdv','bog', 'bog', 'danhmuc','index')
        //        if (session('admin')->level == 'SSA')
        //        {
        //    $gui = session('admin')->setting;
        //            if ($per != null) {
        //                if (isset($gui[$csdl][$group][$feature]['index']) && $gui[$csdl][$group][$feature]['index'] == '1')
        //            return true;
        //        else
        //                    return false;
        //            }

        //            if ($feature != null) {
        //                if (isset($gui[$csdl][$group][$feature]['index']) && $gui[$csdl][$group][$feature]['index'] == '1')
        //            return true;
        //        else
        //                    return false;
        //            }

        //            if ($group != null) {
        //                if (isset($gui[$csdl][$group]['index']) && $gui[$csdl][$group]['index'] == '1')
        //            return true;
        //        else
        //                    return false;
        //            }

        //            if (isset($gui[$csdl]['index']) && $gui[$csdl]['index'] == '1')
        //        return true;
        //    else
        //                return false;

        //        }

        //        //dd(session('admin'));
        //        if (session('admin')->level == 'DN')
        //        {
        //    $a_nghe = array_column(CompanyLvCc::where('madv', session('admin')->madv)->get()->toarray(), 'manghe');
        //    $a_nganh = array_column(view_dmnganhnghe::wherein('manghe', $a_nghe)->get()->toarray(), 'manganh');
        //            //dd($group);
        //            //Doanh nghiệp không phân quyền
        //            if ($per != null) {
        //                return true;
        //            }
        //            //kiểm tra giao diện
        //            if ($feature == null) {//chkPer('csdlmucgiahhdv','bog'): kiểm tra doanh nghiệp có ngành đó ko
        //                return in_array(strtoupper($group), $a_nganh);
        //            } else
        //            {
        //                return in_array(strtoupper($feature), $a_nghe);
        //            }
        //        }
        //        //kiểm tra giao diên xem có sử dụng ko
        //        if ($per != null) {
        //            return chkPer_perm($csdl, $group, $feature, $action, $per);
        //        }

        //        if ($feature != null) {
        //            return chkPer_feature($csdl, $group, $feature);
        //        }

        //        if ($group != null) {
        //            return chkPer_group($csdl, $group);
        //        }

        //        return chkPer_csdl($csdl, $group);
        //    }

        //    function chkPer_perm($csdl, $group, $feature, $action, $per)
        //    {
        //$gui = session('admin')->setting;
        //$per_user = session('admin')->permission;
        //        if (isset($gui[$csdl][$group][$feature]['index']) && $gui[$csdl][$group][$feature]['index'] == '1'
        //            && isset($per_user[$feature][$action][$per]) && $per_user[$feature][$action][$per] == '1')
        //    return true;
        //else
        //            return false;
        //    }

        //    function chkPer_feature($csdl, $group, $feature)
        //    {
        //$gui = session('admin')->setting;
        //$per_user = session('admin')->permission;
        //        if (isset($gui[$csdl][$group][$feature]['index']) && $gui[$csdl][$group][$feature]['index'] == '1'
        //            && isset($per_user[$feature]['index']) && $per_user[$feature]['index'] == '1')
        //    return true;
        //else
        //            return false;
        //        return false;
        //    }

        //    function chkPer_group($csdl, $group)
        //    {
        //$gui = session('admin')->setting;
        //$per_user = session('admin')->permission;
        //        //dd($per_user);
        //        if (isset($gui[$csdl][$group]['index']) && $gui[$csdl][$group]['index'] == '1'
        //            && isset($per_user[$group]['index']) && $per_user[$group]['index'] == '1')
        //    return true;
        //else
        //            return false;
        //    }

        //    function chkPer_csdl($csdl)
        //    {
        //$gui = session('admin')->setting;
        //$per_user = session('admin')->permission;
        //        if (isset($gui[$csdl]['index']) && $gui[$csdl]['index'] == '1'
        //            && isset($per_user[$csdl]['index']) && $per_user[$csdl]['index'] == '1')
        //    return true;
        //else
        //            return false;
        //    }

    }
}
