using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DsPhanQuyenController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        public DsPhanQuyenController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("Permissions/Store")]
        [HttpPost]
        public JsonResult Store(string Username, string Roles, bool Index, bool Create, bool Edit, bool Delete, bool Approve, string Status)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var check = _db.Permissions.Where(t => t.Username == Username && t.Roles == Roles);
                if (check.Count() == 0)
                {
                    var request = new Permissions
                    {
                        Username = Username,
                        Roles = Roles,
                        Index = Index,
                        Create = Create,
                        Edit = Edit,
                        Delete = Delete,
                        Approve = Approve,
                        Status = Status,
                    };
                    _db.Permissions.Add(request);
                    _db.SaveChanges();

                    string result = this.GetDataPermission(Username);
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Quyền " + Roles + " đã tồn tại. Bạn cần kiểm tra lại!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Permissions/StoreSinglePermission")]
        [HttpPost]
        public JsonResult StoreSinglePermission(string Username, string Tendangnhap, string Madv, string Roles, bool Index, bool Create, bool Edit, bool Delete, bool Approve, string Status)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var check = _db.Permissions.Where(t => t.Username == Username && t.Roles == Roles && t.Tendangnhap == Tendangnhap && t.Madv == Madv);
                if (check.Count() == 0)
                {
                    var request = new Permissions
                    {
                        Username = Username,
                        Tendangnhap = Tendangnhap,
                        Madv = Madv,
                        Roles = Roles,
                        Index = Index,
                        Create = Create,
                        Edit = Edit,
                        Delete = Delete,
                        Approve = Approve,
                        Status = Status,
                    };
                    _db.Permissions.Add(request);
                    _db.SaveChanges();

                    string result = this.GetDataSinglePermission(Username, Tendangnhap, Madv);
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Quyền " + Roles + " đã tồn tại. Bạn cần kiểm tra lại!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Permissions/Edit")]
        [HttpPost]
        public JsonResult Edit(int id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                var model = _db.Permissions.Find(id);
                if (model != null)
                {
                    string result = "<div class='modal-body' id='edit_record'>";
                    result += "<div class='row'>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group fv-plugins-icon-container'>";
                    result += "<label>Chức năng:&nbsp</label><span style='font-weight:bold; color:blue'>" + model.Roles + "</span>";
                    result += "</div>";
                    result += "</div>";
                    result += "</div>";
                    result += "<div class='row'>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group fv-plugins-icon-container'>";
                    result += "<label>Tương tác với dữ liệu</label>";

                    result += "<div class='checkbox-inline'>";
                    if (model.Index)
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' checked id='Index_edit' name='Index_edit' /><span></span>Xem";
                        result += "</label>";
                    }
                    else
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' id='Index_edit' name='Index_edit'/><span></span>Xem";
                        result += "</label>";
                    }
                    if (model.Create)
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' checked id='Create_edit' name='Create_edit' /><span></span>Thêm";
                        result += "</label>";
                    }
                    else
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' id='Create_edit' name='Create_edit'/><span></span>Thêm";
                        result += "</label>";
                    }
                    if (model.Edit)
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' checked id='Edit_edit' name='Edit_edit' /><span></span>Sửa";
                        result += "</label>";
                    }
                    else
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' id='Edit_edit' name='Edit_edit'/><span></span>Sửa";
                        result += "</label>";
                    }
                    if (model.Delete)
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' checked id='Delete_edit' name='Delete_edit' /><span></span>Xóa";
                        result += "</label>";
                    }
                    else
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' id='Delete_edit' name='Delete_edit'/><span></span>Xóa";
                        result += "</label>";
                    }
                    if (model.Approve)
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' checked id='Approve_edit' name='Approve_edit' /><span></span>Chuyển/Xét duyệt";
                        result += "</label>";
                    }
                    else
                    {
                        result += "<label class='checkbox'>";
                        result += "<input type='checkbox' id='Approve_edit' name='Approve_edit'/><span></span>Xét duyệt";
                        result += "</label>";
                    }
                    result += "</div>";
                    result += "<input hidden id='Id_edit' name='Id_edit' value='" + model.Id + "'>";
                    result += "</div>";
                    result += "</div>";
                    result += "</div>";
                    result += "</div>";

                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Không tìm thấy thông tin roles chỉnh sửa" };
                    return Json(data);
                }

            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }


        [Route("Permissions/Update")]
        [HttpPost]
        public JsonResult Update(string Username, string Roles, bool Index, bool Create, bool Edit, bool Delete, bool Approve, string Status, int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.Permissions.FirstOrDefault(t => t.Id == Id);
                model.Index = Index;
                model.Create = Create;
                model.Edit = Edit;
                model.Delete = Delete;
                model.Approve = Approve;
                model.Status = Status;

                _db.Permissions.Update(model);
                _db.SaveChanges();

                string result = this.GetDataPermission(Username);
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Permissions/UpdateSinglePermission")]
        [HttpPost]
        public JsonResult UpdateSinglePermission(string Username, string Tendangnhap, string Madv, string Roles, bool Index, bool Create, bool Edit, bool Delete, bool Approve, string Status, int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.Permissions.FirstOrDefault(t => t.Id == Id);
                model.Index = Index;
                model.Create = Create;
                model.Edit = Edit;
                model.Delete = Delete;
                model.Approve = Approve;
                model.Status = Status;

                _db.Permissions.Update(model);
                _db.SaveChanges();

                string result = this.GetDataSinglePermission(Username, Tendangnhap, Madv);
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Permissions/Delete")]
        [HttpPost]
        public JsonResult Delete(string Username, int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.Permissions.FirstOrDefault(t => t.Id == Id);
                _db.Permissions.Remove(model);
                _db.SaveChanges();

                string result = this.GetDataPermission(Username);
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        public string GetDataPermission(string Username)
        {
            var model = _db.Permissions.Where(t => t.Username == Username);
            int record_id = 1;
            string result = "<div class='card-body' id='thongtin_permission'>";
            result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<td width='2%'>#</td>";
            result += "<th>Chức năng</th>";
            result += "<th width='10%'>Xem</th>";
            result += "<th width='10%'>Thêm</th>";
            result += "<th width='10%'>Sửa</th>";
            result += "<th width='10%'>Xóa</th>";
            result += "<th width='10%'>Chuyển/<br>Xét Duyệt</th>";
            result += "<th width='15%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            if (model != null)
            {
                foreach (var item in model)
                {
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record_id++) + "</td>";


                    result += "<td style='font-weight:bold;color:blue'>" + item.Roles + "</td>";


                    result += "<td style=text-align:center'>";
                    if (item.Index)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Create)
                    {
                        result += "<i class= 'la la-check icon-2x text-info mr-5' ></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Edit)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Delete)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Approve)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td>";
                    result += "<button type='button' onclick='editId(`" + item.Id + "`)' data-target='#Edit_Modal' data-toggle='modal'";
                    result += " class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'><i class='icon-lg la la-edit text-primary'></i></button>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa' data-toggle='modal' data-target='#Delete_Modal'";
                    result += " onclick='getId(`" + item.Id + "`,`" + item.Roles + "`)'><i class='icon-lg la la-trash text-danger'></i></button>";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;
        }

        public string GetDataSinglePermission(string Username, string Tendangnhap, string Madv)
        {
            var model = _db.Permissions.Where(t => t.Username == Username && t.Tendangnhap == Tendangnhap && t.Madv == Madv);
            int record_id = 1;
            string result = "<div class='card-body' id='thongtin_permission'>";
            result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<td width='2%'>#</td>";
            result += "<th>Chức năng</th>";
            result += "<th width='10%'>Xem</th>";
            result += "<th width='10%'>Thêm</th>";
            result += "<th width='10%'>Sửa</th>";
            result += "<th width='10%'>Xóa</th>";
            result += "<th width='10%'>Chuyển/<br>Xét Duyệt</th>";
            result += "<th width='15%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            if (model != null)
            {
                foreach (var item in model)
                {
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record_id++) + "</td>";


                    result += "<td style='font-weight:bold;color:blue'>" + item.Roles + "</td>";


                    result += "<td style=text-align:center'>";
                    if (item.Index)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Create)
                    {
                        result += "<i class= 'la la-check icon-2x text-info mr-5' ></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Edit)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Delete)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td style='text-align:center'>";
                    if (item.Approve)
                    {
                        result += "<i class='la la-check icon-2x text-info mr-5'></i>";
                    }
                    else
                    {
                        result += "<i class='la la-remove icon-2x text-danger mr-5'></i>";
                    }
                    result += "</td>";
                    result += "<td>";
                    result += "<button type='button' onclick='editId(`" + item.Id + "`)' data-target='#Edit_Modal' data-toggle='modal'";
                    result += " class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'><i class='icon-lg la la-edit text-primary'></i></button>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa' data-toggle='modal' data-target='#Delete_Modal'";
                    result += " onclick='getId(`" + item.Id + "`,`" + item.Roles + "`)'><i class='icon-lg la la-trash text-danger'></i></button>";
                    result += "</td>";
                    result += "</tr>";
                }
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;
        }
    }
}
