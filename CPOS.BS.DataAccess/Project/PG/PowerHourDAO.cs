/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/14 14:03:25
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
    /// 表PowerHour的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PowerHourDAO : Base.BaseCPOSDAO, ICRUDable<PowerHourEntity>, IQueryable<PowerHourEntity>
    {
        /// <summary>
        /// 按财年获取所有有效的PowerHour
        /// </summary>
        /// <param name="pFinanceYear">int Year（财年的后一个年份，比如，2013-2014财年，就写2014）</param>
        /// <param name="cityID">城市id</param>
        /// <returns></returns>
        public DataSet GetPowerHourByCity(string customerID, int pFinanceYear, string cityID)
        {
            string sql = @"
                            select city.CityID,city.Longitude,city.Latitude,
                            hour.PowerHourID,tUser.user_name as TrainerName,hour.SiteAddress,hour.Topic,city.CityName,hour.StartTime,hour.EndTime,hour.SitePictureUrl
                            ,(select COUNT(1)  from PowerHourInvite invite where invite.PowerHourID=hour.PowerHourID and AcceptState=1 and IsDelete='0') as ConfirmNum
                            from PowerHour hour
                            inner join T_User tUser on tUser.user_id=hour.TrainerID
                            inner join City city on city.CityID=hour.CityID
                            where hour.FinanceYear=@FinanceYear 
                            and cityID=hour.CityID
                            and hour.CustomerID=@CustomerID and hour.IsDelete='0' and city.IsDelete='0'
                            Order by hour.EndTime DESC ";

            var para = new List<SqlParameter>() { 
                new SqlParameter("@CustomerID",customerID),
                new SqlParameter("@FinanceYear",pFinanceYear)
            };

            var ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        /// <summary>
        /// 按财年获取所有有效的PowerHour
        /// </summary>
        /// <param name="pFinanceYear">int Year（财年的前一个年份，比如，2013-2014财年，就写2013）</param>
        /// <returns></returns>
        public DataSet GetAllAvailablePowerHour(string customerID, int pFinanceYear)
        {
            string sql = @"
                            select city.CityID,city.Longitude,city.Latitude,
                            hour.PowerHourID,tUser.user_name as TrainerName,hour.SiteAddress,hour.Topic,city.CityName,hour.StartTime,hour.EndTime,hour.SitePictureUrl
                            ,(select COUNT(1)  from PowerHourInvite invite where invite.PowerHourID=hour.PowerHourID and AcceptState=1 and IsDelete='0') as ConfirmNum
                            from PowerHour hour
                            inner join T_User  tUser
                            on tUser.user_id=hour.TrainerID
                            inner join City city on city.CityID=hour.CityID
                            where hour.FinanceYear=@FinanceYear 
                            and hour.CustomerID=@CustomerID and hour.IsDelete='0' and city.IsDelete='0' ";

            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@CustomerID",customerID),
                new SqlParameter("@FinanceYear",pFinanceYear)
            };

            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());

            return ds;
        }

        /// <summary>
        /// 获取特定城市员工数
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public int GetLocalEmployeeCount(string customerID, string cityId)
        {
            //参数检查
            if (cityId == null)
                return 0;

            string id = cityId.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select LocalStaffCount from [City] where CityID='{0}' and IsDelete=0 and CustomerID='{1}'", id.ToString(), customerID);
            DataSet ds = ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString());

            int num = 0;
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                int.TryParse(ds.Tables[0].Rows[0]["LocalStaffCount"].ToString(), out num);
                return num;
            }

            //返回
            return 0;
        }

        /// <summary>
        /// 获取PowerHour得到的全部Most Valuable  Comments.  
        /// 
        /// *提示*
        /// Most Valuable  Comments.是PowerHourRemark表中，QuestionIndex字段的值为12 或者13的记录
        /// </summary>
        /// <param name="TrainningID">讲师UserID</param>
        /// 
        /// <param name="index">1:12; 2:13</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public DataSet GetTrainningComments(string customerID, string powerHourID, int index, int pageIndex, int pageSize)
        {
            int beginSize = (pageIndex - 1) * pageSize + 1;
            int endSize = pageIndex * pageSize;

            StringBuilder sb = new StringBuilder();
            sb.Append(@"select * from (");
            sb.Append(" select Answer, ROW_NUMBER() over (Order by CreateTime) as Row from PowerHourRemark");
            sb.Append(" where PowerHourID=@PowerHourID and ");
            if (index == 1)
            {
                sb.Append("QuestionIndex=12 and ");
            }
            else if (index == 2)
            {
                sb.Append(" QuestionIndex=13 and ");
            }

            sb.Append("CustomerID=@CustomerID and IsDelete='0'");
            sb.Append(")t");
            sb.Append("  where t.Row between @Start and @End");

            List<SqlParameter> para = new List<SqlParameter>()
            {
               new SqlParameter("@PowerHourID",powerHourID),
               new SqlParameter("@CustomerID",customerID),
               new SqlParameter("@Start",beginSize),
               new SqlParameter("@End",endSize),
            };

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sb.ToString(), para.ToArray());
            return ds;
        }

        /// <summary>
        /// 获取所有受邀参加PowerHour的人的反馈状态（接受/拒绝）
        /// </summary>
        /// <param name="powerHourId">讲座ID</param>
        /// <param name="invitorUserID">邀请人ID</param>
        /// <returns></returns>
        public DataSet GetPowerHourInviteState(string customerID, string powerHourID)
        {
            string sql = @"
                            select tUser.user_id as UserID,tUser.user_name as UserName,tUser.user_email AS Email,invite.AcceptState from PowerHourInvite invite
                            inner join 
                            T_User tUser on tUser.user_id=invite.StaffUserID
                            where  invite.PowerHourID=@PowerHourID and invite.CustomerID=@CustomerID and invite.IsDelete='0'";
            List<SqlParameter> para = new List<SqlParameter>()
            {
               new SqlParameter("@CustomerID",customerID),
               new SqlParameter("@PowerHourID",powerHourID),
            };

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        /// <summary>
        /// 获取所有接受邀请的学员的出席状态.
        /// </summary>
        /// <param name="powerHourId">讲座ID</param>
        /// <returns></returns>
        public DataSet GetAcceptInviteState(string customerID, string powerHourID)
        {
            string sql = @"
                            select tUser.user_id as UserID,tUser.user_name as UserName,tUser.user_email AS Email,invite.Attendence from PowerHourInvite invite
                            inner join 
                            T_User tUser on tUser.user_id=invite.StaffUserID
                            where  invite.PowerHourID=@PowerHourID and invite.AcceptState='1' and invite.CustomerID=@CustomerID and invite.IsDelete='0'";
            List<SqlParameter> para = new List<SqlParameter>()
            {
               new SqlParameter("@CustomerID",customerID),
               new SqlParameter("@PowerHourID",powerHourID),
            };

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }

        #region Power Hour 详细
        /// <summary>
        /// 验证Power Hour是否结束
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public bool VerifyPowerHourIsEnd(string pPowerHourID, string pCustomerID)
        {
            bool f = false;
            string sql = "if exists (SELECT * FROM PowerHour WHERE PowerHourID=@PowerHourID AND CustomerID=@CustomerID AND EndTime>GETDATE() AND IsDelete=0) select 'false' else select 'true' AS IS_END";
            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@PowerHourID",pPowerHourID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            object obj = this.SQLHelper.ExecuteScalar(CommandType.Text, sql, para.ToArray());
            if (obj.ToString() == "true")
                f = true;
            return f;
        }

        /// <summary>
        /// 是否可邀请
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public bool VerifyPowerHourIsInvite(string pPowerHourID, string pCustomerID)
        {
            bool f = false;
            string sql = "if exists (SELECT * FROM PowerHour WHERE PowerHourID=@PowerHourID AND CustomerID=@CustomerID AND StartTime>GETDATE() AND IsDelete=0) select 'true' else select 'false' AS IS_END";
            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@PowerHourID",pPowerHourID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            object obj = this.SQLHelper.ExecuteScalar(CommandType.Text, sql, para.ToArray());
            if (obj.ToString() == "true")
                f = true;
            return f;
        }

        /// <summary>
        /// 讲师界面所需数据(PH结束前)
        /// </summary>
        /// <param name="pPowerHourID"></param>
        /// <param name="pCustomerID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourInfo(string pPowerHourID, string pCustomerID)
        {
            string sql = "SELECT ph.PowerHourID, ph.TrainerID,ct.CityID,ct.CityName,Topic,tu.user_name AS TrainerName,CityName,StartTime,EndTime,LocalStaffCount,SitePictureUrl,";
            sql += " '' IsInvite, '' AcceptState,'' InviteRight,'' Attendence,'' AttendenceStaffCount,'' AbsentStaffCount, '' ReviewCount,'' AvgScore,'' DoReviewState,'' Flag,'' IsAboveStartTime,'' IsAboveEndTime";
            sql += " ,'' IsAgreeRemark,CASE WHEN StartTime>GETDATE() THEN 0 WHEN DATEDIFF(dd,StartTime,GETDATE())<=30 THEN 1 WHEN DATEDIFF(dd,StartTime,GETDATE())>30 THEN 2 END AS ExceedState";
            sql += " FROM  PowerHour AS ph  INNER JOIN City AS ct ON ph.CityID=ct.CityID INNER JOIN T_User AS tu ON tu.user_id=ph.TrainerID AND ph.IsDelete=0";
            sql += " WHERE PowerHourID=@PowerHourID AND ph.CustomerID=@CustomerID ";
            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@PowerHourID",pPowerHourID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }

        /// <summary>
        /// 验证学员是否参加培训状态
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pUserID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet VerifyPowerHourInvite(string pCustomerID, string pUserID, string pPowerHourID)
        {
            string sql = "SELECT AcceptState,Attendence FROM PowerHourInvite AS phi INNER JOIN PowerHour AS ph ON phi.PowerHourID=ph.PowerHourID";
            sql += " WHERE phi.PowerHourID=@PowerHourID AND StaffUserID=@StaffUserID AND phi.CustomerID=@CustomerID AND phi.IsDelete=0 ";
            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@PowerHourID",pPowerHourID),
                new SqlParameter("@StaffUserID",pUserID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }

        /// <summary>
        ///  培训详细（参加人数，缺席人数）
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourAttendStaffInfo(string pCustomerID, string pPowerHourID)
        {
            string sql = "SELECT COUNT(*) AS Total,SUM(CASE WHEN Attendence=1 THEN 1 ELSE 0 END) AS AttendenceStaffCount,";
            sql += "SUM(CASE WHEN Attendence=0 THEN 1 ELSE 0 END) AS AbsentStaffCount FROM PowerHourInvite AS phi INNER JOIN dbo.PowerHour AS ph";
            sql += " ON phi.PowerHourID=ph.PowerHourID WHERE phi.PowerHourID=@PowerHourID AND phi.CustomerID=@CustomerID AND phi.IsDelete=0 GROUP BY phi.PowerHourID";
            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@PowerHourID",pPowerHourID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }

        /// <summary>
        ///  培训详细（评论数，平均得分）
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourRemarkReviewScore(string pCustomerID, string pPowerHourID)
        {
            string sql = "SELECT CAST(CAST(AVG(MemberTotalScore) AS DECIMAL)/10 AS NUMERIC(2,1)) AS AvgScore,COUNT(*) AS ReviewCount,PowerHourID FROM(";
            sql += " SELECT SUM(CAST(Answer AS INT)) MemberTotalScore,RemarkerUserID,phr.PowerHourID FROM PowerHourRemark AS phr ";
            sql += " INNER JOIN PowerHour AS ph ON phr.PowerHourID=ph.PowerHourID WHERE phr.PowerHourID=@PowerHourID AND phr.CustomerID=@CustomerID";
            sql += " AND QuestionIndex<>5 AND QuestionIndex<11 AND phr.IsDelete=0 GROUP BY phr.RemarkerUserID,phr.PowerHourID";
            sql += " ) tt GROUP BY tt.PowerHourID";
            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@PowerHourID",pPowerHourID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }

        /// <summary>
        /// 返回学员评论信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pPowerHourID"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public DataSet GetPowerHourRemarkForMember(string pCustomerID, string pPowerHourID, string pUserID)
        {
            string sql = "SELECT * FROM PowerHourRemark WHERE PowerHourID =@PowerHourID AND RemarkerUserID=@RemarkerUserID AND CustomerID=@CustomerID AND IsDelete=0";
            List<SqlParameter> para = new List<SqlParameter>() 
            {
                new SqlParameter("@PowerHourID",pPowerHourID),
                new SqlParameter("@RemarkerUserID",pUserID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion

        #region 城市Powerhour状态
        /// <summary>
        /// 城市Powerhour状态
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pFinanceYear"></param>
        /// <returns></returns>
        public DataSet GetCityPowerHourState(string pCustomerID, int pFinanceYear)
        {
            //string sql = @"SELECT CityID,CityName,Longitude,Latitude,LocalStaffCount,ISNULL(CityPowerhourState,'Open') AS CityPowerhourState";
            //sql += " FROM ( SELECT C.CityID,c.CityName,C.Longitude,C.Latitude,C.LocalStaffCount";
            //sql += " ,(SELECT TOP 1 CASE WHEN EndTime > GETDATE() THEN 'Comming' ELSE 'Finished' END) FROM PowerHour PH";
            //sql += "  WHERE PH.CityID = C.cityID AND PH.FinanceYear=@FinanceYear AND PH.IsDelete=0 ORDER BY  ph.EndTime DESC) AS CityPowerhourState";
            //sql += " FROM City AS C WHERE C.CustomerID=@CustomerID AND C.IsDelete=0 ) t";

            string sql = "SELECT CityID,CityName,Longitude,Latitude,LocalStaffCount,ISNULL(CityPowerHourState,'Open') AS CityPowerHourState ";
            sql += " FROM( SELECT C.CityID,c.CityName,C.Longitude,C.Latitude,C.LocalStaffCount,";
            sql += " (SELECT TOP 1 CASE WHEN EndTime > GETDATE() THEN 'Comming' ELSE 'Finished' END FROM PowerHour PH";
            sql += " WHERE PH.CityID = C.cityID  AND PH.FinanceYear =@FinanceYear  AND PH.IsDelete = 0 ORDER BY ph.EndTime DESC) AS CityPowerHourState";
            sql += " FROM City AS C WHERE C.CustomerID = @CustomerID AND c.IsDelete = 0) t";

            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@FinanceYear",pFinanceYear),
                 new SqlParameter("@CustomerID",pCustomerID)
            };
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
            return ds;
        }
        #endregion

        #region 特定城市的Power Hour
        /// <summary>
        /// 特定城市的Power Hour
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pFinanceYear"></param>
        /// <param name="pCityID"></param>
        /// <returns></returns>
        public DataSet GetSpecificCityAvailablePowerHour(string pCustomerID, int pFinanceYear, string pCityID)
        {
            string sql = @" select city.CityID,city.Longitude,city.Latitude,
                            hour.PowerHourID,tUser.user_name as TrainerName,hour.SiteAddress,hour.Topic,city.CityName,hour.StartTime,hour.EndTime,hour.SitePictureUrl
                            ,(select COUNT(1)  from PowerHourInvite invite where invite.PowerHourID=hour.PowerHourID and AcceptState=1 and IsDelete='0') as ConfirmNum
                            from PowerHour hour
                            inner join T_User  tUser
                            on tUser.user_id=hour.TrainerID
                            inner join City city on city.CityID=hour.CityID
                            where hour.FinanceYear=@FinanceYear and city.CityID=@CityID
                            and hour.CustomerID=@CustomerID and hour.IsDelete='0' and city.IsDelete='0' ";
            List<SqlParameter> para = new List<SqlParameter>() { 
                new SqlParameter("@FinanceYear",pFinanceYear),
                new SqlParameter("@CityID",pCityID),
                new SqlParameter("@CustomerID",pCustomerID)
            };
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());

            return ds;
        }
        #endregion


        #region 获取现场图片信息
        /// <summary>
        /// 获取现场图片信息
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pObjectID"></param>
        /// <param name="pImageID"></param>
        /// <returns></returns>
        public DataSet GetImageThumbsList(string pCustomerID, string pObjectID, string pImageID)
        {
            string sql = "SELECT ImageId AS ImageID,ObjectId AS ObjectID,ImageURL,DisplayIndex,Title,Description FROM ObjectImages";
            //sql += " WHERE CustomerId=@CustomerId AND IsDelete=0 AND DisplayIndex=0";
            sql += " WHERE CustomerId=@CustomerId AND IsDelete=0 AND Description=''";
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@CustomerId", pCustomerID));
            if (!string.IsNullOrEmpty(pImageID))
            {
                sql += " AND ImageId=@ImageId";
                para.Add(new SqlParameter("@ImageId", pImageID));
            }
            if (!string.IsNullOrEmpty(pObjectID))
            {
                sql += " AND ObjectId=@ObjectId";
                para.Add(new SqlParameter("@ObjectId", pObjectID));
            }
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, para.ToArray());
        }
        #endregion
    }
}
