using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;
using System.Data;
using JIT.CPOS.Common;
using System.Web;
using System.IO;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;


namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "ExportStaff")]
    public class ExportStaffHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return ExportStaff(pRequest);
        }

        public string ExportStaff(string pRequest)
        {
            var rd = new APIResponse<ExportStaffRD>();
            var rdData = new ExportStaffRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<ExportStaffRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = new LoggingSessionManager().CurrentSession;

            try
            {
                //获取普通员工employee角色标识
                string roleId = string.Empty;
                var appSysService = new AppSysService(loggingSessionInfo);
                RoleModel list = new RoleModel();
                string key = "D8C5FF6041AA4EA19D83F924DBF56F93";
                list = appSysService.GetRolesByAppSysId(key, 1000, 0, "", "", "");

                foreach (var item in list.RoleInfoList)
                {
                    if (item.Role_Code.ToLower() == "employee")
                    {
                        roleId = item.Role_Id;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(roleId) || roleId == "")
                    throw new APIException("employee的roleId未获取到") { ErrorCode = 103 };

                rp.Parameters.ExportType = rp.Parameters.ExportType.ToLower();
                //所有页
                if (rp.Parameters.ExportType == "allpage")
                {
                    rp.Parameters.PageIndex = 0;
                    rp.Parameters.PageSize = 5000;
                }

                //数据获取
                T_UserBLL userBll = new T_UserBLL(loggingSessionInfo);
                int totalPage = 0;
                QueryUserEntity entity = new QueryUserEntity();
                entity.QUserName = rp.Parameters.Keyword;
                entity.QUnitID = rp.Parameters.UnitID;
                entity.QJobFunctionID = rp.Parameters.JobFunctionID;
                entity.QRoleID = roleId;
                DataTable dTable = userBll.GetUserList(rp.Parameters.UserID, rp.Parameters.PageIndex, rp.Parameters.PageSize, out totalPage, entity);

                //过滤选择的员工
                if (rp.Parameters.ExportType == "select")
                {
                    string[] ids = rp.Parameters.StaffIds.Split(',');
                    string strIds = "'',";
                    for (int i = 0; i < ids.Length; i++)
                        strIds += "'" + ids[i] + "',";
                    strIds = strIds.Substring(0, strIds.Length - 1);
                    DataRow[] drs = dTable.Select("UserID in (" + strIds + ")");
                    DataTable d = dTable.Clone();
                    foreach (var item in drs)
                        d.ImportRow(item);
                    dTable = d;
                }
                else
                    if (rp.Parameters.ExportType == "noselect")
                        dTable = dTable.Clone();

                //排序
                DataView dv = dTable.DefaultView;
                string sort = string.IsNullOrEmpty(rp.Parameters.sort) ? "UserEmail asc" : rp.Parameters.sort;
                sort = "UserStatus desc," + sort;
                dv.Sort = sort;
                DataTable dt2 = dv.ToTable();
                dTable = dt2;

                //表格标题设置
                DataTable lastTable = dTable.DefaultView.ToTable(false, "UserCode", "UserName", "UserNameEn", "UserEmail", "UserGenderT", "UserBirthday", "UserTelephone", "UserCellphone", "DeptName", "JobFuncName", "UserStatusDesc");
                //重置标题名称
                lastTable.Columns["UserCode"].ColumnName = "用户编码";
                lastTable.Columns["UserName"].ColumnName = "姓名";
                lastTable.Columns["UserNameEn"].ColumnName = "英文名";
                lastTable.Columns["UserEmail"].ColumnName = "邮箱";
                lastTable.Columns["UserGenderT"].ColumnName = "性别";
                lastTable.Columns["UserBirthday"].ColumnName = "生日";
                lastTable.Columns["UserTelephone"].ColumnName = "手机";
                lastTable.Columns["UserCellphone"].ColumnName = "电话";
                lastTable.Columns["DeptName"].ColumnName = "部门";
                lastTable.Columns["JobFuncName"].ColumnName = "职务";
                lastTable.Columns["UserStatusDesc"].ColumnName = "状态";

                //数据获取
                Workbook wb = DataTableExporter.WriteXLS(lastTable, 0);
                string savePath = HttpContext.Current.Server.MapPath(@"~/File/Excel");
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath = savePath + "\\企信员工-" + lastTable.TableName + ".xls";
                wb.Save(savePath);//保存Excel文件
                new ExcelCommon().OutPutExcel(HttpContext.Current, savePath);

                HttpContext.Current.Response.End();

                rd.ResultCode = 0;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            return rd.ToJSON();
        }
    }

    #region 导出员工
    public class ExportStaffRP : IAPIRequestParameter
    {
        public string UserID { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public string Keyword { set; get; }

        public string UnitID { set; get; }
        public string JobFunctionID { set; get; }

        /// <summary>
        /// 当前页，所有页，自由选择
        /// </summary>
        public string ExportType { set; get; }
        public string StaffIds { set; get; }

        public string sort { set; get; }

        public void Validate()
        {
            if (PageSize == 0) PageSize = 15;
            //if (string.IsNullOrEmpty(StaffIds)) throw new APIException("StaffIds不能为空") { ErrorCode = 102 };
        }
    }
    public class ExportStaffRD : IAPIResponseData
    {
    }
    #endregion
}