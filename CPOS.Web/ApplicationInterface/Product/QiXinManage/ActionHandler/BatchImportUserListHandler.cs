using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;
using System.Data.OleDb;
using JIT.Utility.DataAccess.Query;
using System.Linq;
using System.Text;
using JIT.Utility.Log;
using JIT.Utility;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    /// <summary>
    ///  BatchImportUserList的摘要说明
    /// </summary>
    [Export(typeof(IQiXinRequestHandler))]
    [ExportMetadata("Action", "BatchImportUserList")]
    public class BatchImportUserListHandler : IQiXinRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return BatchImportUserList(pRequest);
        }

        public string BatchImportUserList(string pRequest)
        {
            var rd = new APIResponse<BatchImportUserListRD>();
            var rdData = new BatchImportUserListRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<BatchImportUserListRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            try
            {
                string[] msg = Export(loggingSessionInfo, rp.UserID, rp.CustomerID, rp.Parameters.BatchImportUrl).Split('|');
                if (msg[0] == "1")
                {
                    rd.ResultCode = 0;
                    rdData.IsSuccess = true;
                }
                else
                {
                    rd.ResultCode = 101;
                    rdData.IsSuccess = false;
                }
                rd.Message = msg[1];
            }
            catch (Exception ex)
            {
                rd.ResultCode = 101;
                rdData.IsSuccess = false;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }

        public string Export(LoggingSessionInfo pLoggingSessionInfo, string pUserID, string pCustomerID, string pPath)
        {
            string website_WWW = System.Configuration.ConfigurationManager.AppSettings["website_WWW"].ToString();
            string customer_service_url = System.Configuration.ConfigurationManager.AppSettings["customer_service_url"].ToString();
            pPath = pPath.Replace(website_WWW, "");
            pPath = pPath.Replace(customer_service_url, "");
            pPath = pPath.Replace("http://test.o2omarketing.cn:9100/", "");

            #region 读取上传文件的excel数据
            string mapPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            pPath = mapPath + pPath;
            System.IO.FileInfo exporstFile = new System.IO.FileInfo(pPath);
            if (!exporstFile.Exists)
                return "0|文件不存在";
            DataTable excelTable = new DataTable();
            DataSet ds = new DataSet();
            //Excel的连接
            OleDbConnection objConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pPath + ";" + "Extended Properties=Excel 8.0;");
            objConn.Open();
            //DataTable schemaTable = objConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            //string tableName = schemaTable.Rows[0][2].ToString().Trim();//获取 Excel 的表名，默认值是Sheet1$
            //string strSql = "select * from [" + tableName + "]";
            string tableName = "Sheet1$";
            string strSql = "select * from [" + tableName + "]";
            OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
            OleDbDataAdapter myData = new OleDbDataAdapter(strSql, objConn);
            myData.Fill(ds, tableName);//填充数据
            objConn.Close();
            excelTable = ds.Tables[tableName];

            //过滤空行
            DataRow[] drs = excelTable.Select("邮箱<>''");
            DataTable dtClone = excelTable.Clone();
            foreach (var row in drs)
                dtClone.ImportRow(row);
            #endregion

            #region 获取职衔、部门、角色
            DataView _dv = dtClone.DefaultView;
            DataTable jobFuncDT = _dv.ToTable(true, "职衔");
            DataTable deptDT = _dv.ToTable(true, "部门");
            DataTable roleDT = _dv.ToTable(true, "角色");
            #endregion

            #region 职衔验证
            var jobFuncBll = new JobFunctionBLL(pLoggingSessionInfo);
            var jobFuncList = jobFuncBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "CustomerID", Value =pCustomerID },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "IsDelete", Value = "0"},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "Status", Value ="1" }
                    },
                null);
            foreach (DataRow row in jobFuncDT.Rows)
            {
                if (row["职衔"] == null || string.IsNullOrEmpty(row["职衔"].ToString())) continue;
                int count = jobFuncList.Count(job => job.Name == row["职衔"].ToString().Trim());
                if (count <= 0)
                {
                    return "0|职衔不存在";
                }
            }
            #endregion

            #region 部门验证
            string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
            TUnitBLL deptBll = new TUnitBLL(pLoggingSessionInfo);
            var deptList = deptBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "customer_id", Value =pCustomerID },
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "type_id", Value = typeID},
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "Status", Value ="1" }
                    },
                null);
            foreach (DataRow row in deptDT.Rows)
            {
                if (row["部门"] == null || string.IsNullOrEmpty(row["部门"].ToString())) continue;
                int count = deptList.Count(dept => dept.UnitName == row["部门"].ToString().Trim());
                if (count <= 0)
                {
                    return "0|部门不存在";
                }
            }
            #endregion

            #region 角色验证
            RoleService rs = new RoleService(pLoggingSessionInfo);
            IList<RoleModel> roleList = rs.GetAllRoles();
            foreach (DataRow row in roleDT.Rows)
            {
                if (row["角色"] == null || string.IsNullOrEmpty(row["角色"].ToString())) continue;
                int count = roleList.Count(role => role.Role_Name == row["角色"].ToString().Trim());
                if (count <= 0)
                {
                    return "0|角色不存在";
                }
            }
            #endregion

            return ExportData(pCustomerID, pLoggingSessionInfo, dtClone, jobFuncList, deptList, roleList);
            //return "1|成功";
        }

        public string ExportData(string pCustomerID, LoggingSessionInfo pLoggingSessionInfo, DataTable pExportData, JobFunctionEntity[] pJobFuncE, TUnitEntity[] pDeptE, IList<RoleModel> pRoleE)
        {
            T_UserBLL tubll = new T_UserBLL(pLoggingSessionInfo);
            UserDeptJobMappingBLL deptJobMapBll = new UserDeptJobMappingBLL(pLoggingSessionInfo);
            //客户id
            string customerID = pCustomerID;
            //var tran = this.SQLHelper.CreateTransaction();
            //using (tran.Connection)
            //{
            try
            {
                string createUserID = pLoggingSessionInfo.CurrentUser.User_Id;
                string lastUpdateUserID = pLoggingSessionInfo.CurrentUser.User_Id;
                DateTime dt = DateTime.Now;
                int existsEmaliNum = 0, notExistsEmailNum = 0;
                string email = string.Empty;

                if (pExportData != null && pExportData.Rows.Count > 0)
                {
                    DataTable dTable = pExportData;
                    T_UserEntity tue = null;
                    foreach (DataRow row in dTable.Rows)
                    {
                        dt = DateTime.Now;
                        //检测邮箱是否存在
                        if (row["邮箱"] == null || string.IsNullOrEmpty(row["邮箱"].ToString()))
                            continue;
                        email = row["邮箱"].ToString().Trim().ToLower();
                        tue = tubll.GetUserEntityByEmail(email, customerID);
                        if (tue != null)
                        {
                            //存在
                            existsEmaliNum++;
                            continue;
                        }
                        else
                        {
                            //不存在
                            notExistsEmailNum++;
                            #region 保存用户
                            tue = new T_UserEntity();
                            string userId = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                            tue.user_id = userId;
                            if (email != null && !string.IsNullOrEmpty(email))
                                tue.user_email = email;
                            tue.user_name = row["用户名称"].ToString();
                            tue.user_name_en = row["英文名"].ToString();
                            if (row["性别"] != null && !string.IsNullOrEmpty(row["性别"].ToString()))
                            {
                                if (row["性别"].ToString().Trim() == "男") tue.user_gender = "1";
                                else if (row["性别"].ToString().Trim() == "女") tue.user_gender = "2";
                                else tue.user_gender = "0";//未知
                            }
                            else tue.user_gender = "0";
                            if (row["生日"] != null && !string.IsNullOrEmpty(row["生日"].ToString()))
                                tue.user_birthday = Convert.ToDateTime(row["生日"]).ToString("yyyy-MM-dd");
                            if (row["手机"] != null && !string.IsNullOrEmpty(row["手机"].ToString()))
                                tue.user_telephone = row["手机"].ToString();
                            if (row["固话"] != null && !string.IsNullOrEmpty(row["固话"].ToString()))
                                tue.user_cellphone = row["固话"].ToString();
                            if (row["用户编码"] != null && !string.IsNullOrEmpty(row["用户编码"].ToString()))
                                tue.user_code = row["用户编码"].ToString();

                            tue.user_status = "1";
                            tue.user_status_desc = "正常";
                            tue.fail_date = "2020-01-02";
                            tue.user_address = "";
                            tue.user_password = MD5Helper.Encryption("123");//默认密码
                            tue.customer_id = customerID;
                            tue.create_user_id = tue.modify_user_id = createUserID;
                            tue.create_time = tue.modify_time = dt.ToString("yyyy-MM-dd HH:mm:ss");
                            tue.msn = "";
                            tue.qq = "";
                            tue.blog = "";
                            tue.user_postcode = "";
                            tue.user_remark = "";
                            //T_User表
                            //Create(tue, tran);
                            tubll.Create(tue);
                            #endregion

                            #region 保存用户角色
                            //T_User_Role 表
                            string deptID = pDeptE.Where(a => a.UnitName == row["部门"].ToString().Trim()).First().UnitID.ToString();
                            RoleModel rm = pRoleE.Where(a => a.Role_Name == row["角色"].ToString().Trim()).First();
                            TUserRoleEntity ture = new TUserRoleEntity
                            {
                                user_role_id = Guid.NewGuid().ToString(),
                                user_id = userId,
                                role_id = rm.Role_Id,
                                unit_id = deptID,
                                status = "1",
                                create_time = dt,
                                create_user_id = createUserID,
                                modify_time = dt,
                                modify_user_id = lastUpdateUserID,
                                default_flag = "1"
                            };
                            tubll.InsertUserRole(ture);
                            #endregion

                            #region 保存部门、职衔
                            //UserDeptJobMapping表
                            string jobFuncID = pJobFuncE.Where(a => a.Name == row["职衔"].ToString().Trim()).First().JobFunctionID.ToString();
                            UserDeptJobMappingEntity udjme = new UserDeptJobMappingEntity
                            {
                                UserDeptID = Guid.NewGuid(),
                                UserID = userId,
                                JobFunctionID = jobFuncID,
                                USERID = userId,
                                CustomerID = customerID,
                                CreateTime = dt,
                                CreateUserID = createUserID,
                                LastUpdateTime = dt,
                                LastUpdateUserID = lastUpdateUserID,
                                IsDelete = 0,
                                UnitID = deptID
                            };
                            deptJobMapBll.Create(udjme);
                            #endregion
                        }
                    }
                    Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "重复：" + existsEmaliNum + "条，新增：" + notExistsEmailNum + "条,共：" + dTable.Rows.Count });
                }
                //tran.Commit();
            }
            catch (Exception ex)
            {
                //回滚&转抛异常
                //tran.Rollback();
                throw;
            }
            return "1|成功";
            //}
        }
    }

    #region 批量导入用户名单
    public class BatchImportUserListRP : IAPIRequestParameter
    {
        public string BatchImportUrl { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(BatchImportUrl))
                throw new APIException("【BatchImportUrl】不能为空") { ErrorCode = 102 };
        }
    }
    public class BatchImportUserListRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}