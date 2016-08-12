/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 17:12:31
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表C_Activity的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class C_ActivityDAO : Base.BaseCPOSDAO, ICRUDable<C_ActivityEntity>, IQueryable<C_ActivityEntity>
    {
        public bool IsActivityValid(string activityId)
        {
            var sqlType = new StringBuilder();
            sqlType.AppendFormat("select count(1) from c_activity where activityid = '{0}' and activitytype=3 and isdelete = 0",
                activityId);
            if (Convert.ToInt32(this.SQLHelper.ExecuteScalar(sqlType.ToString())) > 0)
                return true;
            var sql = new StringBuilder();
            sql.AppendFormat("select count(1) from c_activitymessage a inner join C_Prizes b on a.ActivityID = b.ActivityID inner join C_PrizesDetail c on b.PrizesID = c.PrizesID where a.activityid = '{0}' and a.isdelete = 0 and b.IsDelete = 0 and c.IsDelete = 0", activityId);
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString())) > 0;
        }

        /// <summary>
        /// 当活动类型3(充值活动）,检查已有活动起止时间是否重叠，重叠返回true
        /// </summary>
        /// <returns></returns>
        public bool IsActivityOverlap(string customerId, string activityId, int activityType, string startTime, string endTime, List<string> vipCardTypeIdList)
        {
            if (activityType == 2 || activityType == 1)
                return false;
            bool result = true;
            if(string.IsNullOrWhiteSpace(customerId))
                return result;
            if(vipCardTypeIdList == null || vipCardTypeIdList.Count == 0)
                return result;
            var endTimeTemp = endTime ?? "2099-01-01"; 
            endTimeTemp += " 23:59:59";
            var sql = new StringBuilder();
            sql.AppendFormat("select count(1) from c_activity a inner join c_targetgroup b with(nolock) on a.ActivityID = b.ActivityID where a.customerid = '{0}' and a.isdelete = 0 and b.isdelete = 0 and a.ActivityType = {4} and b.ObjectID in ('{1}') and a.StartTime <= '{2}' and isnull(a.EndTime, '2099-01-01') >= '{3}'",
                customerId, string.Join("','", vipCardTypeIdList.ToArray()), endTimeTemp, startTime, activityType);
            if (!string.IsNullOrWhiteSpace(activityId))
            {
                sql.AppendFormat(" and a.ActivityID <> '{0}'", activityId);
            }
            result = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString())) > 0 ? true : false;
            return result;
        }

        /// <summary>
        /// 根据活动ID获取目标群体名称,如果IsAllCardType = 1,返回所有卡类型
        /// </summary>
        /// <param name="IsAllCardType"></param>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        
        public string GetTargetGroups(int IsAllCardType, string ActivityID)
        {
            var strTargetGroups = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(ActivityID))
            {
                string sql = "";
                if(IsAllCardType == 0)
                {
                    sql = string.Format("select b.VipCardTypeName from C_TargetGroup as a inner join SysVipCardType as b on a.ObjectID=b.VipCardTypeID where a.ActivityID='{0}' and a.IsDelete = 0 and b.IsDelete = 0", ActivityID);
                }
                else if(IsAllCardType == 1)
                {
                    sql = string.Format("select VipCardTypeName from SysVipCardType where CustomerID = '{0}' and isdelete = 0", CurrentUserInfo.ClientID);
                }
                if (string.IsNullOrWhiteSpace(sql))
                    return strTargetGroups.ToString();
                using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        if (strTargetGroups.Length > 0)
                        {
                            strTargetGroups.Append(",");
                        }
                        strTargetGroups.Append(rdr.GetString(0));
                    }
                }
            }
            return strTargetGroups.ToString();
        }
        
        /// <summary>
        /// 根据活动ID获取目标群体编号,如果IsAllCardType = 1,返回所有卡类型
        /// </summary>
        /// <returns></returns>
        public List<string> GetTargetGroupId(int IsAllCardType, string ActivityID)
        {
            var result = new List<string>();
            if (!string.IsNullOrWhiteSpace(ActivityID))
            {
                string sql = "";
                if (IsAllCardType == 0)
                {
                    sql = string.Format("select b.VipCardTypeID from C_TargetGroup as a inner join SysVipCardType as b on a.ObjectID=b.VipCardTypeID where a.ActivityID='{0}' and a.IsDelete = 0 and b.IsDelete = 0", ActivityID);
                }
                else if (IsAllCardType == 1)
                {
                    sql = string.Format("select VipCardTypeID from SysVipCardType where CustomerID = '{0}'", CurrentUserInfo.ClientID);
                }
                if (string.IsNullOrWhiteSpace(sql))
                    return result;
                using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql))
                {
                    while (rdr.Read())
                    {
                        result.Add(rdr.GetInt32(0).ToString());
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 条件获取获取持卡人数
        /// </summary>
        /// <returns></returns>
        public int GetHolderCardCount(List<string> vipCardTypeIdList)
        {
            var count = 0;
            if (vipCardTypeIdList == null || vipCardTypeIdList.Count == 0)
            {
                return count;
            }
            var sql = new StringBuilder();
            sql.Append("select count(distinct c.vipid) from VipCardVipMapping a with(nolock)");
            sql.Append(" inner join vipcard b with(nolock) on a.vipcardid = b.vipcardid");
            sql.Append(" inner join vip c with(nolock) on a.vipid = c.vipid");
            sql.Append(" where a.IsDelete = 0 and b.isdelete = 0 and c.isdelete = 0 and b.VipCardStatusId = 1 ");
            var vipCardTypeIdCondition = new StringBuilder();

            foreach (var i in vipCardTypeIdList)
            {
                if (!string.IsNullOrWhiteSpace(i))
                {
                    if (vipCardTypeIdCondition.Length > 0)
                    {
                        vipCardTypeIdCondition.Append("or ");
                    }
                    else
                    {
                        vipCardTypeIdCondition.Append("and (");
                    }
                    vipCardTypeIdCondition.AppendFormat("b.VipCardTypeID={0} ", i);
                }
            }
            vipCardTypeIdCondition.Append(") ");
            sql.Append(vipCardTypeIdCondition);
            sql.AppendFormat("and a.customerid='{0}'", CurrentUserInfo.ClientID);
            count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString()));
            return count;
        }
        /// <summary>
        /// 获取生日关怀持卡人数
        /// </summary>
        /// <param name="vipCardTypeIdList"></param>
        /// <returns></returns>

        public int GetBirthHolderCardCount(List<string> vipCardTypeIdList, string activityStartTime, string activityEndTime)
        {
            var count = 0;
            if (vipCardTypeIdList == null || vipCardTypeIdList.Count == 0)
            {
                return count;
            }
            var sql = new StringBuilder();
            sql.Append("select count(distinct c.vipid) from VipCardVipMapping a with(nolock)");
            sql.Append(" inner join vipcard b with(nolock) on a.vipcardid = b.vipcardid");
            sql.Append(" inner join vip c with(nolock) on a.vipid = c.vipid");
            sql.Append(" where a.IsDelete = 0 and b.isdelete = 0 and c.isdelete = 0 and b.VipCardStatusId = 1 ");
            var vipCardTypeIdCondition = new StringBuilder();

            foreach (var i in vipCardTypeIdList)
            {
                if (!string.IsNullOrWhiteSpace(i))
                {
                    if (vipCardTypeIdCondition.Length > 0)
                    {
                        vipCardTypeIdCondition.Append("or ");
                    }
                    else
                    {
                        vipCardTypeIdCondition.Append("and (");
                    }
                    vipCardTypeIdCondition.AppendFormat("b.VipCardTypeID={0} ", i);
                }
            }
            vipCardTypeIdCondition.Append(") ");
            sql.Append(vipCardTypeIdCondition);
            sql.AppendFormat("and a.customerid='{0}'", CurrentUserInfo.ClientID);
            int startYear = int.Parse(activityStartTime.Substring(0, 4));
            int endYear = int.Parse(activityEndTime.Substring(0, 4));
            if (startYear == endYear)
            {
                sql.AppendFormat(
                    " and replace(c.birthday,SUBSTRING(c.birthday,1,4),'{2}') >= '{0}' and replace(c.birthday,SUBSTRING(c.birthday,1,4),'{2}') <= '{1}'",
                    activityStartTime, activityEndTime, activityStartTime.Substring(0, 4));
            }
            else if (startYear + 1 == endYear)
            {
                sql.AppendFormat(" and (replace(c.birthday,SUBSTRING(c.birthday,1,4),'{2}') >= '{0}' and replace(c.birthday,SUBSTRING(c.birthday,1,4),'{2}') < '{1}'",
                    activityStartTime, endYear + "-01-01", activityStartTime.Substring(0, 4));
                sql.AppendFormat(" or replace(c.birthday,SUBSTRING(c.birthday,1,4),'{2}') >= '{0}' and replace(c.birthday,SUBSTRING(c.birthday,1,4),'{2}') <= '{1}')",
    endYear + "-01-01", activityEndTime, activityEndTime.Substring(0, 4));
            }
            count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString()));
            return count;
        }

        public bool IsActivityNameValid(string activityName)
        {
            if (string.IsNullOrWhiteSpace(activityName))
            {
                return false;
            }
            var sql = string.Format(
                "select count(1) from c_activity with(nolock) where activityname = '{0}' and isdelete = 0 and customerid = '{1}'", activityName, CurrentUserInfo.ClientID);
            var count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
            return count == 0;
        }

        /// <summary>
        /// 说明：获取活动未送奖品的会员信息(活动目标群体是所有会员卡用户)
        /// 使用：定时送券使用
        /// </summary>
        /// <param name="activityId">活动ID</param>
        /// <param name="prizesId">奖品ID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId)
        {
            string sql = string.Format(@"
                    SELECT  m.VipID
                    FROM    VipCardVipMapping m
                            INNER JOIN VipCard vc ON vc.VipCardID = m.VipCardID
                            LEFT JOIN C_PrizeReceive pr ON pr.VipID = m.VipID
                            LEFT JOIN C_Activity a ON a.ActivityID = pr.ActivityID
                            LEFT JOIN C_Prizes p ON p.PrizesID = pr.PrizesID
                    WHERE   m.IsDelete = 0
                            AND vc.IsDelete = 0
                            AND vc.VipCardStatusId = 1
                            AND ( pr.VipID IS NULL
                                  OR ( pr.vipID IS NOT NULL
                                       AND a.ActivityID = '{0}'
                                       AND p.PrizesID = '{1}'
                                     )
                                )
                    GROUP BY m.VipID
                ", activityId, prizesId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// 说明：获取活动所有未送奖品的会员信息（活动目标群体是某个卡类别用户）
        /// 使用：定时送券使用
        /// </summary>
        /// <param name="activityId">活动ID</param>
        /// <param name="prizesId">奖品ID</param>
        /// <param name="VipCardTypeID">卡类型ID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId, string vipCardTypeID)
        {
            string sql = string.Format(@"
                    SELECT  m.VipID
                    FROM    VipCardVipMapping m
                            INNER JOIN VipCard vc ON vc.VipCardID = m.VipCardID
                            LEFT JOIN C_PrizeReceive pr ON pr.VipID = m.VipID
                            LEFT JOIN C_Activity a ON a.ActivityID = pr.ActivityID
                            LEFT JOIN C_Prizes p ON p.PrizesID = pr.PrizesID
                            LEFT JOIN C_TargetGroup tg ON tg.ObjectID = vc.VipCardTypeID
                                                          AND tg.GroupType = 1
                    WHERE   m.IsDelete = 0
                            AND vc.IsDelete = 0
                            AND vc.VipCardStatusId = 1
                            AND ( pr.VipID IS NULL
                                  OR ( pr.vipID IS NOT NULL
                                       AND a.ActivityID = '{0}'
                                       AND p.PrizesID = '{1}'
                                     )
                                )
                            AND tg.ObjectID = {2}
                    GROUP BY m.VipID
                ", activityId, prizesId, vipCardTypeID);
            return this.SQLHelper.ExecuteDataset(sql);
        }


        #region 批量营销活动送券、消息业务
        /// <summary>
        /// 获取所有商户信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetALLCustomerInfo(string sql)
        {
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// 批量送券业务
        /// </summary>
        /// <param name="MinCouponNum"></param>
        /// <param name="ActivityID"></param>
        /// <param name="ActivityType"></param>
        /// <param name="CouponTypeID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <returns></returns>
        public int BatchAddSendCoupon(int MinCouponNum, string ActivityID, int ActivityType, string CouponTypeID, int VipCardTypeID)
        {
            int Count = 0;
            //是否是生日营销活动
            bool IsBirthdayMarketing = false;
            if (ActivityType == 1)
                IsBirthdayMarketing = true;


            var parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@Num", System.Data.SqlDbType.Int) { Value = MinCouponNum };
            parm[1] = new SqlParameter("@IsBirthdayMarketing", System.Data.SqlDbType.Bit) { Value = IsBirthdayMarketing };
            parm[2] = new SqlParameter("@ActivityID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(ActivityID) };
            parm[3] = new SqlParameter("@CouponTypeID", System.Data.SqlDbType.NVarChar) { Value = CouponTypeID };
            parm[4] = new SqlParameter("@VipCardTypeID", System.Data.SqlDbType.Int) { Value = VipCardTypeID };
            parm[5] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.UserID };
            parm[6] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.ClientID };

            var Result = this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "BatchAddSendCoupon", parm);
            if (Result != null)
                Count = Convert.ToInt32(Result);

            return Count;
        }
        /// <summary>
        /// 获取符合活动发券的VIP信息
        /// </summary>
        /// <param name="MinCouponNum"></param>
        /// <param name="ActivityID"></param>
        /// <param name="ActivityType"></param>
        /// <param name="CouponTypeID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <returns></returns>
        public DataSet GetBatchAddSendCouponVIPInfo(int MinCouponNum, string ActivityID, int ActivityType, string VipCardTypeID)
        {
          
            //是否是生日营销活动
            bool IsBirthdayMarketing = false;
            if (ActivityType == 1)
                IsBirthdayMarketing = true;


            var parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@Num", System.Data.SqlDbType.Int) { Value = MinCouponNum };
            parm[1] = new SqlParameter("@IsBirthdayMarketing", System.Data.SqlDbType.Bit) { Value = IsBirthdayMarketing };
            parm[2] = new SqlParameter("@ActivityID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(ActivityID) };
            //parm[3] = new SqlParameter("@CouponTypeID", System.Data.SqlDbType.NVarChar) { Value = CouponTypeID };
            parm[3] = new SqlParameter("@VipCardTypeID", System.Data.SqlDbType.Int) { Value = VipCardTypeID };
            parm[4] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.UserID };
            parm[5] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.ClientID };

            var Result = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Pro_GetBatchAddSendCouponVIPInfo", parm);
            

            return Result;
        }
        /// <summary>
        /// 批量生成活动消息业务
        /// </summary>
        /// <param name="ResultConunt"></param>
        /// <param name="ActivityID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <param name="MessageID"></param>
        /// <param name="MessageType"></param>
        /// <param name="Content"></param>
        /// <param name="SendTime"></param>
        /// <param name="StrCouponName"></param>
        /// <param name="ActivityType"></param>
        public void BatchAddMessageSend(int ResultConunt, string ActivityID, string VipCardTypeID, string MessageID, string MessageType, string Content, DateTime SendTime, int ActivityType,string vipid)
        {
            var parm = new SqlParameter[11];
            parm[0] = new SqlParameter("@Num", System.Data.SqlDbType.Int) { Value = ResultConunt };
            parm[1] = new SqlParameter("@ActivityType", System.Data.SqlDbType.Int) { Value = ActivityType };
            parm[2] = new SqlParameter("@ActivityID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(ActivityID) };
            parm[3] = new SqlParameter("@VipCardTypeID", System.Data.SqlDbType.Int) { Value = VipCardTypeID };
            parm[4] = new SqlParameter("@MessageID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(MessageID) };
            parm[5] = new SqlParameter("@MessageType", System.Data.SqlDbType.NVarChar) { Value = MessageType };
            parm[6] = new SqlParameter("@Content", System.Data.SqlDbType.NVarChar) { Value = Content };
            parm[7] = new SqlParameter("@SendTime", System.Data.SqlDbType.DateTime) { Value = SendTime };
            parm[8] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.UserID };
            parm[9] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.ClientID };
            parm[10] = new SqlParameter("@vipid", System.Data.SqlDbType.NVarChar) { Value = vipid };

            var Result = this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BatchAddMessageSend", parm);

        }
        /// <summary>
        /// 批量新增活动获赠信息
        /// </summary>
        /// <param name="ResultConunt"></param>
        /// <param name="ActivityID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <param name="ActivityType"></param>
        public void BatchAddPrizeReceive(int ResultConunt, string ActivityID, string VipCardTypeID, int ActivityType)
        {
            //是否是生日营销活动
            bool IsBirthdayMarketing = false;
            if (ActivityType == 1)
                IsBirthdayMarketing = true;

            var parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@Num", System.Data.SqlDbType.Int) { Value = ResultConunt };
            parm[1] = new SqlParameter("@IsBirthdayMarketing", System.Data.SqlDbType.Bit) { Value = IsBirthdayMarketing };
            parm[2] = new SqlParameter("@ActivityID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(ActivityID) };
            parm[3] = new SqlParameter("@VipCardTypeID", System.Data.SqlDbType.Int) { Value = VipCardTypeID };
            parm[4] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.UserID };
            parm[5] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.ClientID };

            var Result = this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BatchAddPrizeReceive", parm);
        }
        #endregion

    }
}
