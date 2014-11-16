/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/18 10:54:13
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.Common;
using JIT.CPOS.DTO.Base;
using Aspose.Cells;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class LEventSignUpBLL
    {
        #region 2.1.33 活动报名人员列表 Jermyn20130428
        /// <summary>
        /// 活动报名人员列表(2.1.33) Jermyn20130428
        /// </summary>
        /// <param name="EventID">活动ID</param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public GetResponseParams<LEventSignUpEntity> GetEventApplies(string EventID, int Page, int PageSize)
        {
            #region 判断对象不能为空
            if (EventID == null || EventID.ToString().Equals(""))
            {
                return new GetResponseParams<LEventSignUpEntity>
                {
                    Flag = "0",
                    Code = "419",
                    Description = "活动ID不能为空",
                };
            }
            if (PageSize == 0) PageSize = 15;
            #endregion

            GetResponseParams<LEventSignUpEntity> response = new GetResponseParams<LEventSignUpEntity>();
            response.Flag = "1";
            response.Code = "200";
            response.Description = "成功";
            try
            {
                #region 业务处理
                LEventSignUpEntity usersInfo = new LEventSignUpEntity();

                usersInfo.ICount = _currentDAO.GetEventAppliesCount(EventID);

                IList<LEventSignUpEntity> usersInfoList = new List<LEventSignUpEntity>();
                if (usersInfo.ICount > 0)
                {
                    DataSet ds = new DataSet();
                    ds = _currentDAO.GetEventAppliesList(EventID, Page, PageSize);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        usersInfoList = DataTableToObject.ConvertToList<LEventSignUpEntity>(ds.Tables[0]);
                    }
                }

                usersInfo.EntityList = usersInfoList;
                #endregion
                response.Params = usersInfo;
                return response;
            }
            catch (Exception ex)
            {
                response.Flag = "0";
                response.Code = "103";
                response.Description = "错误:" + ex.ToString();
                return response;
            }
        }
        #endregion

        #region 报名提交（评论报名）

        /// <summary>
        /// 报名提交（评论报名）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名称</param>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        public string SetEventSignUp(string userId, string userName, string phone, string eventId)
        {
            string signUpId = string.Empty;

            if (string.IsNullOrEmpty(userId))
            {
                //新增
                signUpId = Utils.NewGuid();

                this._currentDAO.Create(new LEventSignUpEntity()
                {
                    SignUpID = signUpId,
                    EventID = eventId,
                    UserName = userName,
                    Phone = phone
                });
            }
            else
            {
                //更新
                signUpId = userId;

                this._currentDAO.Update(new LEventSignUpEntity()
                {
                    SignUpID = signUpId,
                    EventID = eventId,
                    UserName = userName,
                    Phone = phone
                }, false);
            }

            return signUpId;
        }

        #endregion

        #region GetEventSignUp
        public IList<LEventSignUpEntity> GetEventSignUp(LEventSignUpEntity entity, int Page, int PageSize)
        {

            IList<LEventSignUpEntity> usersInfoList = new List<LEventSignUpEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetEventSignUpList(entity, Page, PageSize);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                usersInfoList = DataTableToObject.ConvertToList<LEventSignUpEntity>(ds.Tables[0]);
            }
            return usersInfoList;

        }
        #endregion

        #region 替换动态关键字
        public string ReplaceTemplate(string message, LEventSignUpEntity eventVip, VipEntity vip, LEventsEntity eventEntity)
        {
            message = message.Replace("$VipName$", eventVip.UserName);
            message = message.Replace("$Region$", null);
            message = message.Replace("$Seat$", null);
            message = message.Replace("$Ver$", new Random().Next().ToString());
            message = message.Replace("$CustomerId$", eventVip.CustomerId);
            message = message.Replace("$UserId$", vip.VIPID);
            message = message.Replace("$OpenId$", vip.WeiXinUserId);
            message = message.Replace("$EventName$", eventEntity.Title);

            return message;
        }
        #endregion

        #region 下载模板
        public DataTable DownEnrollTpl(string eventID, int status)
        {
            DataTable dataTable = new DataTable();

            DataSet formDetail = DynamicFormLoadByEventID(eventID);

            if (Utils.IsDataSetValid(formDetail))
            {
                dataTable.TableName = formDetail.Tables[0].Rows[0][0].ToString();

                //build a new table which column names are ColumnDesc
                foreach (DataRow field in formDetail.Tables[1].Rows)
                {
                    if (field["IsUsed"].ToString() == "1")
                        dataTable.Columns.Add(field["ColumnDesc"].ToString());
                }
            }

            //1代表未确认报名的，10代表已确认报名的
            //在已确认报名界面，下载模板除了要根据EventId动态的取出字段外，另外还有一个固定字段分组名（对应数据库字段GroupName）
            switch (status)
            {
                case 10:
                    dataTable.Columns.Add("是否支付");
                    dataTable.Columns.Add("分组");
                    break;
                default:
                    break;
            }


            return dataTable;
        }
        #endregion 下载模板

        public DataSet DynamicFormLoadByEventID(string eventID)
        {
            DataSet dataSet = new DataSet();

            MobileModuleObjectMappingBLL mobileModuleObjectMappingBLL = new MobileModuleObjectMappingBLL(CurrentUserInfo);
            var mobileModuleObjectMapping = mobileModuleObjectMappingBLL.Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "ObjectID", Value = eventID}
                , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
            }, null);

            if (mobileModuleObjectMapping.Length > 0)
            {
                string formID = mobileModuleObjectMapping[0].MobileModuleID.ToString();

                MobileModuleBLL mobileModuleBLL = new MobileModuleBLL(CurrentUserInfo);
                dataSet = mobileModuleBLL.DynamicFormLoad(formID, "LEventSignUp");
            }

            return dataSet;
        }

        #region 导入报名数据
        public string ImportEnrollData(string fileName, DownEnrollTplRP downEnrollTplRP, out int total, out int add, out int update)
        {
            string result = "";
            //前端返回值
            total = 0; add = 0; update = 0;

            if (!string.IsNullOrEmpty(fileName))
            {
                Aspose.Cells.License lic = new License();
                lic.SetLicense("Aspose.Total.lic");
                Workbook wb = new Workbook(fileName);

                //判断是否有worksheet
                if (wb.Worksheets.Count > 0)
                {
                    Worksheet ws = wb.Worksheets[0];

                    //判断是否存在有效数据
                    if (ws.Cells[0, 0].Value != null && !string.IsNullOrWhiteSpace(ws.Cells[0, 0].Value.ToString()))
                    {
                        DataTable dataTable = new DataTable();
                        dataTable = ws.Cells.ExportDataTableAsString(0, 0, ws.Cells.MaxRow + 1, ws.Cells.MaxColumn + 1, true);
                        //返回所有的总数
                        total = dataTable.Rows.Count;

                        DataSet formDetail = DynamicFormLoadByEventID(downEnrollTplRP.EventID);

                        if (Utils.IsDataSetValid(formDetail))
                        {
                            StringBuilder sbField = new StringBuilder();
                            StringBuilder sbValue = new StringBuilder();
                            string sql = "";
                            List<System.Data.SqlClient.SqlParameter> parameter = new List<System.Data.SqlClient.SqlParameter>();
                            Random random = new Random();

                            //1代表未确认报名的，10代表已确认报名的
                            //在未确认报名页面，导入excel数据，只需要根据EventId，将动态字段的值导入数据库。
                            //在已确认报名页面，导入excel数据时：
                            //1）需要将分组名列值存入到字段GroupName中
                            //2）需要生成一个6位数字签到码，并写入到字段RegistrationCode中。
                            switch (downEnrollTplRP.Status)
                            {
                                case 1:
                                    //默认给分组列一个空字符串，为了json里面不出现NULL。
                                    DataColumn groupCol = new DataColumn();
                                    groupCol.ColumnName = "Groupname";
                                    dataTable.Columns.Add(groupCol);
                                    sbField.Append("Groupname, ");
                                    sbValue.Append("@Groupname, ");
                                    parameter.Add(new System.Data.SqlClient.SqlParameter("@Groupname", SqlDbType.NVarChar, 4000, "Groupname"));
                                    foreach (DataRow dr in dataTable.Rows)
                                    {
                                        dr["Groupname"] = string.Empty;
                                    }
                                    break;
                                case 10:
                                    if (dataTable.Columns.Contains("分组"))
                                    {
                                        //支付状态
                                        dataTable.Columns["是否支付"].ColumnName = "IsPay";
                                        sbField.Append("IsPay, ");
                                        sbValue.Append("@IsPay, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@IsPay", SqlDbType.NVarChar, 4000, "IsPay"));

                                        //分组
                                        dataTable.Columns["分组"].ColumnName = "GroupName";
                                        sbField.Append("GroupName, ");
                                        sbValue.Append("@GroupName, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@GroupName", SqlDbType.NVarChar, 4000, "GroupName"));

                                        DataColumn dataColumn = new DataColumn();
                                        dataColumn.ColumnName = "RegistrationCode";
                                        dataTable.Columns.Add(dataColumn);
                                        sbField.Append("RegistrationCode, ");
                                        sbValue.Append("@RegistrationCode, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@RegistrationCode", SqlDbType.NVarChar, 4000, "RegistrationCode"));

                                        DataColumn statusColumn = new DataColumn();
                                        statusColumn.ColumnName = "Status";
                                        dataTable.Columns.Add(statusColumn);
                                        sbField.Append("Status, ");
                                        sbValue.Append("@Status, ");
                                        parameter.Add(new System.Data.SqlClient.SqlParameter("@Status", SqlDbType.NVarChar, 4000, "Status"));

                                        foreach (DataRow dr in dataTable.Rows)
                                        {
                                            dr["RegistrationCode"] = random.Next(100000, 999999);
                                            dr["Status"] = 10;
                                            dr["IsPay"] = dr["IsPay"].ToString().Length == 0 ? "0" : (dr["IsPay"].ToString() == "是" ? "1" : "0");
                                        }
                                    }
                                    else
                                        result = "已确认报名的数据必须包含“分组”列！";
                                    break;
                                default:
                                    break;
                            }

                            //默认添加CustomerId
                            DataColumn cusCol = new DataColumn();
                            cusCol.ColumnName = "CustomerId";
                            dataTable.Columns.Add(cusCol);
                            sbField.Append("CustomerId, ");
                            sbValue.Append("@CustomerId, ");
                            parameter.Add(new System.Data.SqlClient.SqlParameter("@CustomerId", SqlDbType.NVarChar, 4000, "CustomerId"));
                            foreach (DataRow dr in dataTable.Rows)
                            {
                                dr["CustomerId"] = CurrentUserInfo.ClientID;
                            }

                            foreach (DataRow field in formDetail.Tables[1].Rows)
                            {
                                if (field["IsUsed"].ToString() == "1")
                                {
                                    dataTable.Columns[field["ColumnDesc"].ToString()].ColumnName = field["ColumnName"].ToString();
                                    sbField.Append(field["ColumnName"] + ",");
                                    sbValue.Append("@" + field["ColumnName"] + ",");

                                    parameter.Add(new System.Data.SqlClient.SqlParameter("@" + field["ColumnName"].ToString(), SqlDbType.NVarChar, 4000, field["ColumnName"].ToString()));
                                }
                            }
                            sbField.Remove(sbField.Length - 1, 1);
                            sbValue.Remove(sbValue.Length - 1, 1);

                            DataColumn eventID = new DataColumn("EventID", typeof(string));
                            eventID.DefaultValue = downEnrollTplRP.EventID;
                            dataTable.Columns.Add(eventID);

                            sbField.Append(", EventID");
                            sbValue.Append(", @EventID");
                            parameter.Add(new System.Data.SqlClient.SqlParameter("@EventID", SqlDbType.NVarChar, 4000, "EventID"));

                            if (string.IsNullOrEmpty(result))
                            {
                                //检测手机号是否有重复 add by zyh
                                List<string> phones = _currentDAO.GetPhoneList(downEnrollTplRP.EventID);
                                int repeat = 0;
                                //生成用来更新的sql语句
                                StringBuilder sbUpdate = new StringBuilder();

                                for (int i = 0; i < dataTable.Rows.Count; i++)
                                {
                                    DataRow dr = dataTable.Rows[i];
                                    string phone = dr["Phone"].ToString();
                                    //导入Excel时，组件解析的有点问题。总是会多一行空数据。
                                    //这里暂时判断手机号是否空，空就认为是无效数据。
                                    if (phone.ToString().Length == 0)
                                    {
                                        dataTable.Rows.RemoveAt(i);
                                        i--;
                                    }
                                    else
                                    {
                                        //如果手机号有重复，直接从table中移除
                                        if (phones.Contains(phone))
                                        {
                                            repeat++;
                                            //构造更新字段
                                            string _fields = string.Empty;
                                            foreach (DataRow field in formDetail.Tables[1].Rows)
                                            {
                                                if (field["IsUsed"].ToString() == "1")
                                                {
                                                    string column = field["ColumnName"].ToString();
                                                    _fields += column + "='" + dr[column].ToString() + "',";
                                                }
                                            }
                                            if (_fields.Length > 0)
                                            {
                                                if (downEnrollTplRP.Status == 1)
                                                    _fields = _fields.Substring(0, _fields.Length - 1);
                                                else
                                                    _fields += "IsPay=" + dr["IsPay"].ToString() + ",GroupName='" + dr["GroupName"].ToString() + "'";
                                                sbUpdate.Append(string.Format("UPDATE LEventSignUp SET {0} WHERE EventId = '{1}' AND Phone = '{2}'", _fields, downEnrollTplRP.EventID, phone));
                                            }

                                            //移除该行
                                            dataTable.Rows.RemoveAt(i);
                                            i--;
                                        }
                                    }
                                }
                                //返回新增和更新的数量
                                update = repeat;
                                add = total - repeat;

                                sql = string.Format("insert into [LEventSignUp] ({0}) values ({1})", sbField.ToString(), sbValue.ToString());
                                _currentDAO.AdapterUpdate(CurrentUserInfo.CurrentLoggingManager.Connection_String, dataTable, sql, parameter);
                                //更新手机号重复的信息
                                if (sbUpdate.Length > 0)
                                    _currentDAO.UpdateSignUpInfo(sbUpdate.ToString());

                            }
                        }
                    }
                    else
                        result = "文件无数据！";
                }
                else
                    result = "文件无数据！";
            }

            return result;
        }
        #endregion 下载模板

        #region 活动类型列表
        public LEventsTypeEntity[] EventTypeList()
        {
            LEventsTypeBLL lEventsTypeBLL = new LEventsTypeBLL(CurrentUserInfo);
            return lEventsTypeBLL.Query(
                new IWhereCondition[] { 
                    new IsNullOrEqaulCondition(CurrentUserInfo, "ClientID")
            }, new OrderBy[] { 
                new OrderBy() { FieldName = "Title", Direction = OrderByDirections.Asc }
            });
        }
        #endregion

        #region 活动主办方列表
        public OptionsEntity[] EventSponsorList()
        {
            OptionsBLL optionsBLL = new OptionsBLL(CurrentUserInfo);
            return optionsBLL.Query(
                new IWhereCondition[] { 
                    new IsNullOrEqaulCondition(CurrentUserInfo, "ClientID")
                    , new EqualsCondition() { FieldName = "OptionName", Value = "EventSponsor"}
            }, new OrderBy[] { 
                new OrderBy() { FieldName = "Sequence", Direction = OrderByDirections.Asc }
            });
        }
        #endregion

        #region RP
        public class DownEnrollTplRP : IAPIRequestParameter
        {
            public string EventID { get; set; }
            public int Status { get; set; }
            public void Validate()
            {
                if (string.IsNullOrEmpty(EventID))
                    throw new APIException(201, "活动ID不能为空！");
            }
        }

        #endregion RP

        #region RD
        public class DownEnrollTplRD : IAPIResponseData
        {
            public int Total { get; set; }
            public int Add { get; set; }
            public int Update { get; set; }
        }

        public class EventTypeListRD : IAPIResponseData
        {
            public LEventsTypeEntity[] EventTypeList { get; set; }
        }

        public class EventSponsorListRD : IAPIResponseData
        {
            public OptionsEntity[] EventSponsorList { get; set; }
        }
        #endregion RD

        /*******************************************************************************************************************
         * 新版 活动管理 Add 2014-07-30 By Alan
         */
        #region 获取活动列表
        /// <summary>
        /// 获取活动报名人员集合
        /// </summary>
        /// <param name="eventID">活动Id</param>
        /// <param name="status">状态值：未确认(1)、已确认(10)</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageCount">页总数</param>
        /// <param name="TotalCountUn">未确认总记录条数</param>
        /// <param name="TotalCountYet">已确认总记录条数</param>
        /// <param name="dicCloumnNames">列名集合</param>
        /// <returns></returns>
        public DataTable GetEventSignUpList(string eventID, int status, int pageIndex, int pageSize, out int pageCount, out int TotalCountUn, out int TotalCountYet, out Dictionary<string, string> dicCloumnNames)
        {
            DataSet ds = _currentDAO.GetEventSignUpList(eventID, status, pageIndex, pageSize, false, out dicCloumnNames);

            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out TotalCountUn);
            int.TryParse(ds.Tables[2].Rows[0][0].ToString(), out TotalCountYet);

            if (status == 1)
            {
                pageCount = TotalCountUn / pageSize;

                if (TotalCountUn % pageSize > 0)
                {
                    pageCount++;
                }
            }
            else if (status == 10)
            {
                pageCount = TotalCountYet / pageSize;

                if (TotalCountYet % pageSize > 0)
                {
                    pageCount++;
                }
            }
            else
            {
                pageCount = -1;
            }
            return ds.Tables[0];
        }
        #endregion

        #region 确定参加报名
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">状态值：未确认(1)、已确认(10)</param>
        /// <param name="signUpID">活动报名人员ID</param>
        /// <param name="operID">操作人员ID</param>
        /// <param name="signInCode">签到码</param>
        /// <returns>执行结果</returns>
        public int SetEventSignUpOper(int status, string signUpID, string operID, string signInCode)
        {
            return _currentDAO.SetEventSignUpOper(status, signUpID, operID, signInCode);
        }
        #endregion

        #region 发送通知
        /// <summary>
        /// 获取通知人员信息
        /// </summary>
        /// <param name="Ids">通知人员Ids集合</param>
        /// <returns>数据集</returns>
        public DataTable GetNotifyEventSignUpList(string IdsStr)
        {
            if (string.IsNullOrEmpty(IdsStr))
            {
                return null;
            }
            string[] Ids = IdsStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return _currentDAO.GetNotifyEventSignUpList(Ids).Tables[0];
        }
        /// <summary>
        /// 修改发送状态
        /// </summary>
        /// <param name="signUpId">活动报名人员ID</param>
        /// <param name="sendStatus">发送状态</param>
        /// <param name="sendType">发送方式：1短信、2邮件、3微信</param>
        /// <param name="operID">发送人ID</param>
        /// <returns>执行结果</returns>
        public int SetEventSignUpStatus(string signUpId, int sendStatus, int sendType, string operID)
        {
            return _currentDAO.SetEventSignUpStatus(signUpId, sendStatus, sendType, operID);
        }
        #endregion

        #region 设置分组
        /// <summary>
        /// 设置分组接口
        /// </summary>
        /// <param name="signUpId">活动报名人员ID</param>
        /// <param name="groupName">组名</param>
        /// <param name="operId">操作人员Id</param>
        /// <returns>操作结果</returns>
        public int SetSignUpGroup(string signUpId, string groupName, string operId)
        {
            return _currentDAO.SetSignUpGroup(signUpId, groupName, operId);
        }
        #endregion

        #region 获取签到列表
        /// <summary>
        /// 获取签到列表
        /// </summary>
        /// <param name="eventId">活动Id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="TotalCount">总记录条数</param>
        /// <param name="TotalPage">总页数</param>
        /// <returns>数据集</returns>
        public DataTable GetEventUserMappingList(string eventId, int pageIndex, int pageSize, out int TotalCount, out int TotalPage)
        {
            if (string.IsNullOrEmpty(eventId))
            {
                TotalCount = 0;
                TotalPage = 0;
                return null;
            }
            //Get List
            DataSet ds = _currentDAO.GetEventUserMappingList(eventId, pageIndex, pageSize);
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out TotalCount);

            TotalPage = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
            {
                TotalPage++;
            }
            return ds.Tables[0];
        }
        #endregion

        #region 签到
        /// <summary>
        /// 获取签到人员信息
        /// </summary>
        /// <param name="signUpId">活动用户ID</param>
        /// <param name="regCode">签到码</param>
        /// <param name="phone">手机号码</param>
        /// <param name="dicInfo">返回数据信息</param>
        /// <returns>数据信息</returns>        
        public bool EventCheckInInfo(string signUpId, string regCode, string phone, out Dictionary<string, string> dicInfo)
        {
            bool res = false;

            dicInfo = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(regCode))
            {

                res = false;
            }
            DataSet ds = _currentDAO.EventCheckInInfo(signUpId, phone, regCode);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var v = ds.Tables[0].Rows[0];
                if (regCode.Equals(v["RegistrationCode"].ToString()))
                {
                    res = true;
                }
                else if (phone.Equals(v["Phone"].ToString()))
                {
                    res = true;
                }
                //校验成功
                if (res)
                {
                    _currentDAO.EventCheckInRecord(v["SignUpID"].ToString(), v["EventID"].ToString(), v["UserName"].ToString(), v["Phone"].ToString(), v["Email"].ToString(), v["UserName"].ToString());

                    dicInfo.Add("Name", v["UserName"].ToString());
                    dicInfo.Add("Company", v["Col7"].ToString());
                    dicInfo.Add("Job", v["Col8"].ToString());
                    dicInfo.Add("Mobile", v["Phone"].ToString());
                    dicInfo.Add("GroupName", v["GroupName"].ToString());
                    dicInfo.Add("IsPay", v["IsPay"].ToString());
                    dicInfo.Add("EventID", v["EventID"].ToString());
                }
            }

            return res;
        }
        #endregion

        #region 签到成功，显示组信息
        /// <summary>
        /// 获取组成员信息
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public DataTable GetCheckInGroupsList(string eventID, string groupName)//, int pageIndex, int pageSize, out Dictionary<string, string> dicCloumnNames
        {
            DataSet ds = _currentDAO.GetCheckInGroupsList(eventID, groupName);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        #endregion
    }
}