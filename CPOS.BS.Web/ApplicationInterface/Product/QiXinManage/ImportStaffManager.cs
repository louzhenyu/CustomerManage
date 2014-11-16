using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Product.QiXinManage
{
    public class ImportStaffManager
    {
        public string Export(LoggingSessionInfo pLoggingSessionInfo, string pPath)
        {
            try
            {
                string customerID = pLoggingSessionInfo.ClientID;
                string userID = pLoggingSessionInfo.CurrentUser.User_Id;

                ////下载保存获取本地路径
                //string localPath = string.Empty;
                //DownloadFile(pPath, "", out localPath);
                //pPath = localPath;

                string path = HttpContext.Current.Server.MapPath(@"~");
                pPath = path + pPath;

                #region 读取Excel数据并简单过滤
                System.IO.FileInfo exporstFile = new System.IO.FileInfo(pPath);
                if (!exporstFile.Exists)
                    return "0|文件不存在";

                //读取Excel
                DataTable excelTable = ExcelReader.ReadExcelToDataTable(pPath);

                //return "0|" + excelTable.Rows[0][0];
                
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
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "CustomerID", Value =customerID },
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
                        JobFunctionEntity entity = new JobFunctionEntity
                        {
                            Name = row["职衔"].ToString().Trim(),
                            Description = "",
                            JobFunctionID = Guid.NewGuid(),
                            Status = 1,
                            IsDelete = 0,
                            CustomerID = customerID
                        };
                        jobFuncBll.Create(entity);
                        //return "0|职衔不存在";
                    }
                }
                #endregion

                #region 部门验证
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                TUnitBLL deptBll = new TUnitBLL(pLoggingSessionInfo);
                var deptList = deptBll.Query(new IWhereCondition[]
                    {
                        new EqualsCondition{DateTimeAccuracy = null, FieldName = "customer_id", Value =customerID },
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
                        TUnitEntity unitEntity = new TUnitEntity
                        {
                            UnitID = Guid.NewGuid().ToString().Replace("-", ""),
                            UnitName = row["部门"].ToString().Trim(),
                            UnitCode = "1",
                            UnitContact = "",
                            UnitRemark = "",
                            TypeID = typeID,
                            CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            CreateUserID = pLoggingSessionInfo.CurrentUser.User_Id,
                            ModifyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            ModifyUserID = pLoggingSessionInfo.CurrentUser.User_Id,
                            CustomerID = pLoggingSessionInfo.CurrentUser.customer_id,
                            CUSTOMERLEVEL = 0,
                            Status = "1",
                            StatusDesc = "正常",
                            IfFlag = "1"
                        };
                        deptBll.Create(unitEntity);
                        //return "0|部门不存在";
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

                return ExportData(customerID, pLoggingSessionInfo, dtClone, jobFuncList, deptList, roleList);
            }
            catch (Exception ex)
            {
                return "0|" + ex.Message;
            }
        }

        public string ExportData(string pCustomerID, LoggingSessionInfo pLoggingSessionInfo, DataTable pExportData, JobFunctionEntity[] pJobFuncE, TUnitEntity[] pDeptE, IList<RoleModel> pRoleE)
        {
            T_UserBLL tubll = new T_UserBLL(pLoggingSessionInfo);
            UserDeptJobMappingBLL deptJobMapBll = new UserDeptJobMappingBLL(pLoggingSessionInfo);
            //客户id
            string customerID = pCustomerID;
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

                            try
                            {
                                //激活云通讯帐户
                                YTXManager.Instance.SingleReisterCall(userId, customerID, createUserID);
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "重复：" + existsEmaliNum + "条，新增：" + notExistsEmailNum + "条,共：" + dTable.Rows.Count });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return "1|成功";
        }

        public string DownloadFile(string pAddress, string pDownloadUrl, out string pLocalAddress)
        {

            try
            {
                if (pDownloadUrl == null || pDownloadUrl.Equals(""))
                {
                    pDownloadUrl = "http://o2oapi.aladingyidong.com";
                }
                string host = pDownloadUrl + "/File/qixin/";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                WebClient webClient = new WebClient();

                //创建下载根文件夹
                //var dirPath = @"C:\DownloadFile\";
                var dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "File\\qixin\\";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //根据年月日创建下载子文件夹
                var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + @"\";
                host += ymd + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //下载到本地文件
                var fileExt = Path.GetExtension(pAddress).ToLower();
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                var filePath = dirPath + newFileName;
                host += newFileName;
                webClient.DownloadFile(pAddress, filePath);
                pLocalAddress = filePath;
                return host;
            }
            catch (Exception ex)
            {
                pLocalAddress = string.Empty;
                return string.Empty;
            }
        }
    }
}