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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表LEventSignUp的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class LEventSignUpDAO : Base.BaseCPOSDAO, ICRUDable<LEventSignUpEntity>, IQueryable<LEventSignUpEntity>
    {
        #region 活动报名人员列表
        /// <summary>
        /// 根据标识获取活动人员数量
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int GetEventAppliesCount(string EventID)
        {
            string sql = GetEventAppliesSql(EventID);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// 根据标识获取活动人员信息
        /// </summary>
        public DataSet GetEventAppliesList(string EventID, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventAppliesSql(EventID);
            sql = sql + "select * From #tmp a where 1=1 and a.displayindex between '" + beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetEventAppliesSql(string EventID)
        {
            BasicUserInfo pUserInfo = new BasicUserInfo();

            string sql = "";
            sql += "SELECT a.* "
                + " ,DisplayIndex = row_number() over(order by a.UserName ) "
                + " into #tmp FROM WEventUserMapping a "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            return sql;
        }


        /// <summary>
        /// 根据标识获取活动人员信息
        /// </summary>
        public DataSet GetEventSignUpList(LEventSignUpEntity entity, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventSignUpSql(entity);
            sql += @" select * From #tmp a where 1=1 and a.displayindex between '" +
                    beginSize + "' and '" + endSize + "' order by  a.displayindex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetEventSignUpSql(LEventSignUpEntity entity)
        {
            string sql = string.Empty;

            string sql1 = " select * into #tmp1 From vip where phone is not null and phone <> '' ";
            if (entity.VipVipId != null && entity.VipVipId.Trim().Length > 0)
            {
                sql1 += " and VipId = '" + entity.VipVipId + "' ; ";
            }
            sql = sql1;
            sql += "SELECT a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.VipId as VipVipId ";
            sql += " ,b.Status ";
            sql += " ,b.Weixin ";
            sql += " ,b.WeixinUserId ";
            sql += " ,b.HeadImgUrl,case when b.vipid is null then 0 else 1 end IsSign ";
            sql += " into #tmp ";
            sql += " from LEventSignUp a ";
            sql += " left join #tmp1 b on (a.phone=b.phone and b.isDelete='0') ";
            sql += " where a.IsDelete='0' ";
            sql += " and a.customerId='" + CurrentUserInfo.CurrentUser.customer_id + "' and a.phone is not null and a.phone <> '' ";
            if (entity.SignUpID != null && entity.SignUpID.ToString().Length > 0)
            {
                sql += " and a.SignUpID = '" + entity.SignUpID + "' ";
            }
            if (entity.VipVipId != null && entity.VipVipId.Length > 0)
            {
                sql += " and (b.VipId = '" + entity.VipVipId + "') ";
            }
            if (entity.Phone != null && entity.Phone.Trim().Length > 0)
            {
                sql += " and a.Phone = '" + entity.Phone + "' ";
            }
            if (entity.UserName != null && entity.UserName.Trim().Length > 0)
            {
                sql += " and a.VipName = '" + entity.UserName + "' ";
            }
            return sql;
        }


        /// <summary>
        /// 根据标识获取活动人员数量
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int GetEventAppliesCount2(string EventID)
        {
            string sql = GetEventAppliesSql2(EventID);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        private string GetEventAppliesSql2(string EventID)
        {
            BasicUserInfo pUserInfo = new BasicUserInfo();

            string sql = "";
            sql += "SELECT distinct a.userId ,a.eventId "
                + " into #tmp FROM WEventUserMapping a "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            return sql;
        }
        #endregion

        /*******************************************************************************************************************
         * 新版 活动管理 Add 2014-07-30 By Alan
         * this.CurrentUserInfo.ClientID
         * this.CurrentUserInfo.UserID
         */
        #region 获取报名列表
        /// <summary>
        /// 获取活动报名人员集合
        /// </summary>
        /// <param name="eventID">活动Id</param>
        /// <param name="status">状态值：未确认(1)、已确认(10)</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="isDivision">是否区分确认状体：True 区分（统计传入状态的记录条数），False 不区分（统计两种状态的记录条数）</param>
        /// <param name="dicCloumnNames">列名集合</param>
        /// <returns></returns>
        public DataSet GetEventSignUpList(string eventID, int status, int pageIndex, int pageSize, bool isDivision, out Dictionary<string, string> dicCloumnNames)
        {
            //Create SQL Text             
            StringBuilder getSignUpListSB = new StringBuilder();
            string strColNames = string.Empty;
            string strColName = string.Empty;
            if (status.Equals(10))
            {
                strColNames = ",SendStatus,IsPay ";
                strColName = ",RegistrationCode,GroupName ";
            }

            //获取显示的列名
            StringBuilder columnNamesSB = GetColumnNames(eventID, status, "LEventSignUp", out dicCloumnNames);
            getSignUpListSB.AppendFormat("select SignUpID,EventID {1} {0} {2} from(", columnNamesSB.ToString(), strColNames, strColName);
            getSignUpListSB.AppendFormat("select ROW_NUMBER()over(order by SignUpID) RowNum ,SignUpID,EventID {1} {0} {2} from LEventSignUp SU ", columnNamesSB.ToString(), strColNames, strColName);
            getSignUpListSB.AppendFormat("where IsDelete=0 and CustomerId='{0}' and EventID='{1}' and ISNULL([Status],0) = {2} ", CurrentUserInfo.ClientID, eventID, status);
            getSignUpListSB.AppendFormat(") as Res where RowNum between {0} and {1} ;", (pageIndex * pageSize + 1), (pageIndex + 1) * pageSize);

            if (isDivision)
            {
                getSignUpListSB.Append("select Count(SignUpID) from LEventSignUp ");
                getSignUpListSB.AppendFormat("where IsDelete=0 and CustomerId='{0}' and EventID='{1}' and ISNULL([Status],0) = {2} ", CurrentUserInfo.ClientID, eventID, status);
            }
            else
            {
                //未确认
                getSignUpListSB.Append("select Count(SignUpID) from LEventSignUp ");
                getSignUpListSB.AppendFormat("where IsDelete=0 and CustomerId='{0}' and EventID='{1}' and ISNULL([Status],0) = 1 ", CurrentUserInfo.ClientID, eventID);
                //已确认
                getSignUpListSB.Append("select Count(SignUpID) from LEventSignUp ");
                getSignUpListSB.AppendFormat("where IsDelete=0 and CustomerId='{0}' and EventID='{1}' and ISNULL([Status],0) = 10 ", CurrentUserInfo.ClientID, eventID);
            }
            return this.SQLHelper.ExecuteDataset(getSignUpListSB.ToString());
        }

        /// <summary>
        /// 获取活动报名人员显示列
        /// </summary>
        /// <param name="eventID">活动ID</param>
        /// <param name="status">状态值：未确认(1)、已确认(10)</param>
        /// <param name="tableName">表名</param>
        /// <param name="dicCloumnNames">列名集合</param>
        /// <returns></returns>
        private StringBuilder GetColumnNames(string eventID, int status, string tableName, out Dictionary<string, string> dicCloumnNames)
        {
            //Create SQL Text
            StringBuilder getColumnNamesSB = new StringBuilder();
            getColumnNamesSB.Append("select E.EventID,D.MobileBussinessDefinedID,D.TableName,D.ColumnDesc,D.ColumnDescEn,D.ColumnName from MobileBussinessDefined D ");
            getColumnNamesSB.Append("inner join MobilePageBlock P on P.IsDelete=0 and P.CustomerID=D.CustomerID and P.MobilePageBlockID=D.MobilePageBlockID ");
            getColumnNamesSB.Append("inner join MobileModule M on M.IsDelete=0 and M.CustomerID=P.CustomerID and M.MobileModuleID=P.MobileModuleID ");
            getColumnNamesSB.Append("inner join MobileModuleObjectMapping Map on Map.IsDelete=0 and Map.CustomerID=M.CustomerID and Map.MobileModuleID = M.MobileModuleID ");
            getColumnNamesSB.Append("inner join  LEvents E on E.IsDelete=0 and E.CustomerId=Map.CustomerID and E.EventID=Map.ObjectID ");
            getColumnNamesSB.AppendFormat("where D.IsDelete=0 and D.CustomerID='{0}' and E.EventID='{1}' and D.TableName='{2}' order by EditOrder;", CurrentUserInfo.CurrentLoggingManager.Customer_Id, eventID, tableName);

            //获取问卷信息
            DataSet columnNamesDT = this.SQLHelper.ExecuteDataset(getColumnNamesSB.ToString());

            //创建拼接展示列对象
            StringBuilder columnNamesSB = new StringBuilder();
            Dictionary<string, string> dicColumns = new Dictionary<string, string>();
            if (columnNamesDT.Tables.Count > 0 && columnNamesDT.Tables[0].Rows.Count > 0)
            {
                if (status.Equals(10))
                {
                    dicColumns.Add("SendStatus", "发送状态");
                    dicColumns.Add("IsPay", "是否支付");
                }
                foreach (DataRow row in columnNamesDT.Tables[0].Rows)
                {
                    columnNamesSB.AppendFormat(",{0}", row["ColumnName"]);
                    dicColumns.Add(row["ColumnName"].ToString(), row["ColumnDesc"].ToString());
                }
                if (status.Equals(10))
                {
                    dicColumns.Add("RegistrationCode", "签到码");
                    dicColumns.Add("GroupName", "分组名");
                }
            }
            dicCloumnNames = dicColumns;

            return columnNamesSB;
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
            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("update LEventSignUp set [Status]={0} ,CheckTime=GETDATE(),CheckUserId='{1}',RegistrationCode='{2}',LastUpdateBy='{1}',LastUpdateTime=GETDATE() ", status, CurrentUserInfo.UserID, signInCode);
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{0}' and SignUpID='{1}' ;", CurrentUserInfo.ClientID, signUpID);

            //Access DB
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }

        #endregion

        #region 发送通知
        /// <summary>
        /// 获取通知人员信息
        /// </summary>
        /// <param name="Ids">通知人员Ids集合</param>
        /// <returns>数据集</returns>
        public DataSet GetNotifyEventSignUpList(string[] Ids)
        {
            if (Ids == null)
            {
                return null;
            }
            //Append Ids
            StringBuilder sbIds = new StringBuilder();
            foreach (var Id in Ids)
            {
                sbIds.AppendFormat("'{0}',", Id);
            }
            sbIds.Remove(sbIds.Length - 1, 1);

            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select SignUpID, EventID, VipID, UserName, Phone, City, CreateTime, CreateBy, LastUpdateBy, LastUpdateTime, IsDelete, ");
            sbSQL.Append("Email, Col1, Col2, Col3, Col4, Col5, Col6, Col7, Col8, Col9, Col10, Col26, Col27, Col28, Col29, Col30, Col31, Col32, ");
            sbSQL.Append("Col33, Col34, Col35, Col36, Col37, Col38, Col39, Col40, Col41, Col42, Col43, Col44, Col45, Col46, Col47, Col48, Col49, ");
            sbSQL.Append("Col50, VipCompany, VipPost, Phone2, Seats, Profile, HeadImage, CustomerId, DCodeImageUrl, CanLottery, DataFrom, ");
            sbSQL.Append("[Status], GroupName, RegistrationCode, CheckTime, CheckUserId, SendStatus, SendType, SendTime, SendUserId, IsPay ");
            sbSQL.Append("from LEventSignUp ");
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{0}' and SignUpID in({1}) ;", CurrentUserInfo.ClientID, sbIds.ToString());

            //Access DB
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        /// <summary>
        /// 修改发送状态
        /// </summary>
        /// <param name="signUpId">活动报名人员ID</param>
        /// <param name="sendStatus">发送状态</param>
        /// <param name="sendType">发送方式</param>
        /// <param name="operID">发送人ID</param>
        /// <returns>执行结果</returns>
        public int SetEventSignUpStatus(string signUpId, int sendStatus, int sendType, string operID)
        {
            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("update LEventSignUp set SendStatus={0},SendType={1},SendTime=GETDATE(),SendUserId='{2}',LastUpdateBy='{2}',LastUpdateTime=GETDATE() ", sendStatus, sendType, CurrentUserInfo.UserID);
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{0}' and SignUpID ='{1}' ;", CurrentUserInfo.ClientID, signUpId);

            //Access DB
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
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
            //Create SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("update LEventSignUp set GroupName='{0}',LastUpdateBy='{1}',LastUpdateTime=GETDATE() ", groupName, CurrentUserInfo.UserID);
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{1}' and SignUpID ='{0}' ;", signUpId, CurrentUserInfo.ClientID);

            //Execut SQL Script
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }
        #endregion

        #region 获取签到列表
        /// <summary>
        /// 获取签到列表
        /// </summary>
        /// <param name="eventId">活动Id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>数据集</returns>
        public DataSet GetEventUserMappingList(string eventId, int pageIndex, int pageSize)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select Mapping, UserName, Mobile, Email, UserID, OpenID, Class, EventID, CreateTime, CreateBy, LastUpdateBy, LastUpdateTime, IsDelete from( ");
            sbSQL.Append("select ROW_NUMBER()over(order by Mapping) rowNum ,Mapping, UserName, Mobile, Email, UserID, OpenID, Class, EventID, CreateTime, CreateBy, LastUpdateBy, LastUpdateTime, IsDelete ");
            sbSQL.AppendFormat("from WEventUserMapping where IsDelete=0 and EventID='{0}' ", eventId);
            sbSQL.Append(") as pageInfo ");
            sbSQL.AppendFormat("where rowNum between {0} and {1} ;", (pageIndex * pageSize + 1), pageSize * (pageIndex + 1));
            sbSQL.Append("select COUNT(Mapping) from WEventUserMapping ");
            sbSQL.AppendFormat("where IsDelete=0 and EventID='{0}' ;", eventId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        #endregion

        #region 签到
        /// <summary>
        /// 获取签到人员信息
        /// </summary>
        /// <param name="signUpId">活动用户ID</param>
        /// <returns>数据信息</returns>
        public DataSet EventCheckInInfo(string signUpId, string phone, string regCode)
        {
            //Build SQL Text
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("select SignUpID, EventID, VipID, UserName, Phone, City, CreateTime, CreateBy, LastUpdateBy, LastUpdateTime, IsDelete,");
            sbSql.Append("Email, Col1, Col2, Col3, Col4, Col5, Col6, Col7, Col8, Col9, Col10, Col26, Col27, Col28, Col29, Col30, Col31, Col32, ");
            sbSql.Append("Col33, Col34, Col35, Col36, Col37, Col38, Col39, Col40, Col41, Col42, Col43, Col44, Col45, Col46, Col47, Col48, Col49, ");
            sbSql.Append("Col50, VipCompany, VipPost, Phone2, Seats, Profile, HeadImage, CustomerId, DCodeImageUrl, CanLottery, DataFrom, ");
            sbSql.Append("[Status], GroupName, RegistrationCode, CheckTime, CheckUserId, SendStatus, SendType, SendTime, SendUserId, IsPay ");
            sbSql.Append("from LEventSignUp ");
            sbSql.AppendFormat("where IsDelete=0 and CustomerId='{0}' ", CurrentUserInfo.ClientID);

            if (!string.IsNullOrEmpty(signUpId))
            {
                sbSql.AppendFormat("and SignUpID ='{0}' ", signUpId);
            }
            sbSql.AppendFormat("and (RegistrationCode ='{0}' OR Phone ='{1}') ", regCode, phone);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSql.ToString());
        }
        /// <summary>
        /// 签到码校验成功操作
        /// </summary>
        /// <param name="signUpId">活动报名人员Id</param>
        /// <param name="eventId">活动ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="phone">手机号码</param>
        /// <param name="email">邮箱</param>
        /// <param name="createBy">创建人</param>
        /// <returns></returns>
        public int EventCheckInRecord(string signUpId, string eventId, string userName, string phone, string email, string createBy)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("IF NOT EXISTS(SELECT TOP 1 1 FROM WEventUserMapping WHERE UserID='{0}' AND EventID ='{1}' ) ", signUpId, eventId);
            sbSQL.Append(" BEGIN ");
            sbSQL.Append("insert into WEventUserMapping(Mapping,UserID,EventID,UserName,Mobile,Email,CreateBy,LastUpdateBy,IsDelete,CreateTime) ");
            sbSQL.AppendFormat("values(NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{5}',0,Getdate()) ; ", signUpId, eventId, userName, phone, email, CurrentUserInfo.UserID);
            sbSQL.Append(" END ");

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }

        #endregion

        #region 签到成功，显示组信息
        /// <summary>
        /// 获取组成员信息
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public DataSet GetCheckInGroupsList(string eventID, string groupName)//, int pageIndex, int pageSize, out Dictionary<string, string> dicCloumnNames
        {
            //Create SQL Text             
            StringBuilder getSignUpListSB = new StringBuilder();

            //获取显示的列名
            //StringBuilder columnNamesSB = GetColumnNames(eventID, "LEventSignUp", out dicCloumnNames);
            //getSignUpListSB.AppendFormat("select SignUpID,EventID,SendStatus,IsPay {0} from(", columnNamesSB.ToString());
            //getSignUpListSB.AppendFormat("select ROW_NUMBER()over(order by SignUpID) RowNum ,SignUpID,EventID,SendStatus,IsPay {0} from LEventSignUp SU ", columnNamesSB.ToString());
            //getSignUpListSB.AppendFormat("where IsDelete=0 and CustomerId='{0}' and EventID='{1}' and GroupName = {2} ", CurrentUserInfo.ClientID, eventID, groupName);
            //getSignUpListSB.AppendFormat(") as Res where RowNum between {0} and {1} ;", (pageIndex * pageSize + 1), (pageIndex + 1) * pageSize);

            getSignUpListSB.Append("select SignUpID, EventID,UserName, Phone as Mobile, Col7 as Company, Col8 as Job ");
            getSignUpListSB.Append("from LEventSignUp ");
            getSignUpListSB.AppendFormat("where IsDelete=0 and CustomerId='{0}' and EventID='{1}' and GroupName = '{2}' ;", CurrentUserInfo.ClientID, eventID, groupName);

            return this.SQLHelper.ExecuteDataset(getSignUpListSB.ToString());
        }
        #endregion

        public void BulkInsert(DataTable dataTable)
        {
            throw new NotImplementedException();
        }

        public void AdapterUpdate(string connectionString, DataTable dataTable, string sql, List<SqlParameter> parameter)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdpater = new SqlDataAdapter("SELECT * FROM LEventSignUp where 1=0", connection);  //get table structure

                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(dataAdpater);
                dataAdpater.AcceptChangesDuringUpdate = false;
                dataAdpater.InsertCommand = new SqlCommand(sql);
                dataAdpater.InsertCommand.Parameters.AddRange(parameter.ToArray());

                try
                {
                    dataTable.AcceptChanges();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (dr.RowState != DataRowState.Modified)
                            dr.SetAdded();
                    }

                    connection.Open();
                    dataAdpater.Update(dataTable);
                    dataTable.AcceptChanges();
                    connection.Close();
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        /// <summary>
        /// 获取一条活动的所有手机号列表
        /// </summary>
        public List<string> GetPhoneList(string eventId)
        {
            List<string> li = new List<string>();
            SqlDataReader dr = this.SQLHelper.ExecuteReader("SELECT Phone FROM LEventSignUp WHERE EventId = '" + eventId + "'");
            while (dr.Read())
            {
                li.Add(dr[0].ToString());
            }
            dr.Close();
            return li;
        }

        /// <summary>
        /// 更新手机号重复的信息
        /// </summary>
        public void UpdateSignUpInfo(string sql)
        {
            this.SQLHelper.ExecuteNonQuery(sql);
        }

        //数据导入
        public void ImportExeclData(DataTable dataTable, List<SqlParameter> parameter, StringBuilder sbField, StringBuilder sbValue)
        {
            if (dataTable.Rows.Count > 1)
            {
                return;
            }

            //Create Transaction
            using (var trans = SQLHelper.CreateTransaction())
            {
                try
                {
                    //复制值
                    string strField = sbField.ToString();
                    string strValue = sbValue.ToString();
                    string[] fields = strField.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] values = strValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    //键/值
                    StringBuilder sbKevVal = new StringBuilder();
                    if (fields != null && fields.Length > 0)
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            sbKevVal.AppendFormat("{0} = {1},");
                        }
                        sbKevVal.Remove(sbKevVal.Length - 1, 1);
                    }
                    //Build SQL Text
                    StringBuilder sbSQL = new StringBuilder();
                    sbSQL.Append("IF NOT EXISTS( SELECT TOP 1 1 FROM [LEventSignUp] WHERE IsDelete = 0 AND CustomerId='{0}' AND Phone='{1}' ) ");
                    sbSQL.AppendFormat("BEGIN INSERT INTO [LEventSignUp] ({0}) VALUES ({1}) ", fields, values);
                    sbSQL.Append(" END ELSE BEGIN ");
                    sbSQL.Append("UPDATE [LEventSignUp] SET {2} WHERE IsDelete = 0 AND CustomerId = '{0}' AND Phone = '{1}' ");
                    sbSQL.Append("END ");

                    foreach (DataRow dr in dataTable.Rows)
                    {
                        //Set SQL Parameters Value
                        foreach (var parms in parameter)
                        {
                            parms.Value = dr[parms.SourceColumn];
                        }
                        this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, string.Format(sbSQL.ToString(), dr["CustomerId"], dr["Phone"], sbKevVal.ToString()), parameter.ToArray());
                    }

                    //Commit
                    trans.Commit();
                }
                catch
                {
                    //Rollback
                    trans.Rollback();
                    throw;
                }
            }
        }

    }
}
