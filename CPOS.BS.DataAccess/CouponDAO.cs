/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
using System.Collections;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// 数据访问：  
    /// 表Coupon的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class CouponDAO : Base.BaseCPOSDAO, ICRUDable<CouponEntity>, IQueryable<CouponEntity>
    {
        #region 根据ID获取优惠券列表

        /// <summary>
        /// 根据ID获取优惠券列表
        /// </summary>
        /// <param name="vipId">用户ID</param>
        /// <returns></returns>
        public DataSet GetMyCouponList(string vipId)
        {
            string sql = string.Empty;
            sql += " SELECT couponType = b.CouponTypeName, a.CouponCode as CouponCode ";
            sql += " , parValue = b.ParValue ";
            sql += " , conditionValue = b.ConditionValue ";
            sql += " , couponSource = c.CouponSource FROM Coupon a ";
            sql += " INNER JOIN CouponType b ON a.CouponTypeID = b.CouponTypeID ";
            sql += " LEFT JOIN CouponSource c ON b.CouponSourceID = c.CouponSourceID ";
            sql += " INNER JOIN dbo.VipCouponMapping d ON a.CouponID = d.CouponID ";
            sql += " WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND d.IsDelete = 0 ";
            sql += " AND a.EndDate > GETDATE() ";
            sql += " AND a.[Status] = 0 ";
            sql += " AND NOT EXISTS(SELECT 1 FROM TOrderCouponMapping t WHERE t.IsDelete = 0 AND t.CouponId = a.CouponId) ";
            sql += " AND d.VIPID = '" + vipId + "' ORDER BY b.ParValue DESC";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 获取订单使用的优惠券总计

        /// <summary>
        /// 获取订单使用的优惠券总计
        /// </summary>
        /// <param name="vipId">用户ID</param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public DataSet OrderCouponSum(string vipId, string orderId)
        {
            string sql = string.Empty;
            sql += " SELECT [count] = COUNT(*), amount = ISNULL(SUM(b.ParValue),0) ";
            sql += " FROM Coupon a ";
            sql += " INNER JOIN CouponType b ON a.CouponTypeID = b.CouponTypeID ";
            sql += " INNER JOIN dbo.VipCouponMapping d ON a.CouponID = d.CouponID ";
            sql += " INNER JOIN TOrderCouponMapping e ON e.CouponId = a.CouponID ";
            sql += " WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND d.IsDelete = 0 AND e.IsDelete = 0 ";
            sql += " AND a.EndDate > GETDATE() ";
            sql += " AND d.VIPID = '" + vipId + "' ";
            sql += " AND e.OrderId = '" + orderId + "' ";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 订单使用的优惠券列表

        /// <summary>
        /// 订单使用的优惠券列表
        /// </summary>
        /// <param name="vipId">用户ID</param>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public DataSet OrderCouponList(string vipId, string orderId)
        {
            string sql = string.Empty;
            sql += " SELECT couponId = a.couponId ";
            sql += " , IsChecked = ( ";
            sql += " 	SELECT COUNT(*) FROM TOrderCouponMapping t ";
            sql += " 	WHERE t.IsDelete = 0 AND t.orderId = '" + orderId + "' ";
            sql += " 	AND t.CouponId = a.CouponID) ";
            sql += " , couponType = b.CouponTypeName ";
            sql += " , parValue = b.ParValue ";
            sql += " , conditionValue = b.ConditionValue ";
            sql += " , couponSource = c.CouponSource ";
            sql += " FROM Coupon a ";
            sql += " INNER JOIN CouponType b ON a.CouponTypeID = b.CouponTypeID ";
            sql += " LEFT JOIN CouponSource c ON b.CouponSourceID = c.CouponSourceID ";
            sql += " INNER JOIN dbo.VipCouponMapping d ON a.CouponID = d.CouponID ";
            sql += " WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND d.IsDelete = 0 ";
            sql += " AND a.EndDate > GETDATE() ";
            sql += " AND a.[Status] = 0 ";
            sql += " AND d.VIPID = '" + vipId + "' ";
            sql += " AND NOT EXISTS (SELECT 1 FROM TOrderCouponMapping e WHERE e.OrderId <> '" + orderId + "' AND e.CouponId = a.CouponId AND e.IsDelete = 0)";
            sql += " ORDER BY b.ParValue DESC";

            var ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        #endregion

        #region 订单中取消使用优惠券

        /// <summary>
        /// 订单中取消使用优惠券
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="couponId">优惠券ID</param>
        /// <returns></returns>
        public void CancelCouponMapping(string orderId, string couponId)
        {
            string sql = string.Empty;
            sql += " UPDATE TOrderCouponMapping ";
            sql += " SET IsDelete = 1, ";
            sql += " LastUpdateBy = '" + this.CurrentUserInfo.UserID + "', ";
            sql += " LastUpdateTime = GETDATE() ";
            sql += " WHERE OrderId = '" + orderId + "' ";

            if (!string.IsNullOrEmpty(couponId))
            {
                sql += " AND CouponId = '" + couponId + "' ";
            }

            this.SQLHelper.ExecuteNonQuery(sql);
        }

        #endregion

        #region 更新订单表数据

        /// <summary>
        /// 更新订单表数据
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <returns></returns>
        public void CancelCouponOrder(string orderId)
        {
            string sql = string.Empty;
            sql += " UPDATE dbo.T_Inout ";
            sql += " SET total_retail = total_amount, ";
            sql += " actual_amount = total_amount, ";
            sql += " Field16 = '', ";
            sql += " modify_time = CONVERT(VARCHAR(20), GETDATE(), 120), ";
            sql += " modify_user_id = '" + this.CurrentUserInfo.UserID + "' ";
            sql += " WHERE order_id = '" + orderId + "' ";

            this.SQLHelper.ExecuteNonQuery(sql);
        }

        #endregion

        #region 计算订单使用的优惠券

        public DataSet CheckCouponForOrder(string vipId, string orderId, string couponId)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@VIPID", System.Data.SqlDbType.VarChar, 100);
            param[0].Value = vipId;
            param[1] = new SqlParameter("@OrderID", System.Data.SqlDbType.VarChar, 100);
            param[1].Value = orderId;
            param[2] = new SqlParameter("@CouponID", System.Data.SqlDbType.VarChar, 100);
            param[2].Value = couponId;

            var ds = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "CheckCouponForOrder", param);
            return ds;
        }

        #endregion

        #region 创建优惠券

        /// <summary>
        /// 创建优惠券
        /// </summary>
        /// <param name="vipId">会员ID</param>
        /// <param name="eventId">活动ID</param>
        /// <returns></returns>
        public void CreateCoupon(string vipId, string eventId, string couponId, IDbTransaction tran)
        {
            string sql = string.Empty;
            sql += " INSERT INTO Coupon ( CouponID, CouponCode, CouponDesc, ";
            sql += " BeginDate, EndDate, CouponUrl, ImageUrl, ";
            sql += " [Status], CreateTime, CreateBy, LastUpdateTime, ";
            sql += " LastUpdateBy, IsDelete, CouponTypeID ) ";
            sql += " SELECT '" + couponId + "', '', b.CouponTypeName, GETDATE(), ";
            //优惠券有效期，单位：分钟，为0时一百年有效
            sql += " CASE b.ValidPeriod WHEN 0 THEN DATEADD(YEAR, 100, GETDATE()) ELSE DATEADD(MINUTE, b.ValidPeriod, GETDATE()) END, ";
            sql += " '', '', 0, GETDATE(),'" + this.CurrentUserInfo.UserID + "', ";
            sql += " GETDATE(), '" + this.CurrentUserInfo.UserID + "', 0, b.CouponTypeID ";
            sql += " FROM CouponType b ";
            sql += " INNER JOIN PrizeCouponTypeMapping c ON b.CouponTypeID = c.CouponTypeID ";
            sql += " INNER JOIN LPrizeWinner d ON d.PrizeID = c.PrizesID ";
            sql += " INNER JOIN LPrizes e ON e.PrizesID = d.PrizeID ";
            sql += " WHERE b.IsDelete = 0 AND c.IsDelete = 0 ";
            sql += " AND d.VipID = '" + vipId + "' ";
            sql += " AND e.EventID = '" + eventId + "' ";

            this.SQLHelper.ExecuteNonQuery((SqlTransaction)tran, CommandType.Text, sql);
        }

        #endregion

        #region 推荐新人奖励
        public void RecommenderPrize(string VipID, string EventId)
        {
            string sql = "RecommenderPrize";

            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            sqlParameter.Add(new SqlParameter("@VipID", VipID));
            sqlParameter.Add(new SqlParameter("@EventId", EventId));
            Loggers.Debug(new DebugLogInfo() { Message = "RecommenderPrize '" + CurrentUserInfo.ClientID + "','" + VipID + "'" });

            this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, sql, sqlParameter.ToArray());
        }
        #endregion

        #region 获取推荐排行榜列表

        /// <summary>
        /// 获取推荐排行榜列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendList()
        {
            string sql = string.Empty;
            sql += " SELECT vipId = a.VIPID, ";
            sql += " vipName = CASE LEN(ISNULL(ltrim(rtrim(a.VipName)), '')) WHEN 0 THEN '匿名' WHEN 1 THEN ltrim(rtrim(a.VipName)) + '***' ELSE LEFT(ltrim(rtrim(a.VipName)), 1) + '***' + RIGHT(ltrim(rtrim(a.VipName)), 1) END , ";
            sql += " recommendCount = (SELECT COUNT(*) FROM vip b WHERE a.VIPID = b.HigherVipID) ";
            sql += " INTO #tmp ";
            sql += " FROM dbo.Vip a ";
            // sql += " WHERE a.HigherVipID IS NOT NULL AND a.HigherVipID <> '' ";
            sql += " WHERE a.isdelete=0 ";
            sql += "  ";
            sql += " SELECT TOP 10 vipId = a.VIPID, ";
            sql += " vipName = a.vipName, ";
            sql += " recommendCount = a.recommendCount, ";
            sql += " parValue = CAST(b.ParValue AS INT), ";
            sql += " displayIndex = ROW_NUMBER() OVER(ORDER BY a.recommendCount desc) ";
            sql += ",integral=cast(c.integral as INT)";
            sql += " FROM #tmp a ";
            sql += " LEFT JOIN ( ";
            sql += " 	SELECT a.VIPID, ParValue = SUM(c.ParValue) ";
            sql += " 	FROM dbo.VipCouponMapping a ";
            sql += " 	INNER JOIN dbo.Coupon b ON a.CouponID = b.CouponID ";
            sql += " 	INNER JOIN dbo.CouponType c ON b.CouponTypeID = c.CouponTypeID ";
            sql += " 	WHERE a.IsDelete = 0 ";
            sql += " 	GROUP BY a.VIPID) b ON a.VIPID = b.VIPID ";
            sql += "left join (";
            sql += "	select VipID,sum(Integral) as Integral from dbo.VipIntegralDetail";
            sql += "	where IntegralSourceID =5 and isdelete=0	group by VipID";
            sql += ") c on a.VipID=c.VipID";
            sql += " WHERE a.recommendCount > 0 ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取推荐战绩

        /// <summary>
        /// 获取推荐战绩
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendRecord(string userId)
        {
            string sql = string.Empty;
            sql += " SELECT vipId = a.VIPID, ";
            sql += " vipName = a.vipName, ";
            sql += " recommendCount = (SELECT COUNT(*) FROM vip b WHERE a.VIPID = b.HigherVipID), ";
            sql += " parValue = CAST(b.ParValue AS INT), ";
            sql += " integral = CAST(c.Integral AS INT) ";
            sql += " FROM dbo.Vip a ";
            sql += " LEFT JOIN ( ";
            sql += " 	SELECT a.VIPID, ParValue = SUM(c.ParValue) ";
            sql += " 	FROM dbo.VipCouponMapping a ";
            sql += " 	INNER JOIN dbo.Coupon b ON a.CouponID = b.CouponID ";
            sql += " 	INNER JOIN dbo.CouponType c ON b.CouponTypeID = c.CouponTypeID ";
            sql += " 	WHERE a.IsDelete = 0 ";
            sql += " 	GROUP BY a.VIPID) b ON a.VIPID = b.VIPID ";
            sql += "left join (";
            sql += "	select VipID,sum(Integral) as Integral from dbo.VipIntegralDetail";
            sql += "	where IntegralSourceID =5 and isdelete=0	group by VipID";
            sql += ") c on a.VipID=c.VipID";
            sql += " WHERE a.VIPID = '" + userId + "' ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        #region 获取推荐战绩人员列表

        /// <summary>
        /// 获取推荐战绩人员列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendRecordList(string userId)
        {
            string sql = string.Empty;
            sql += " SELECT vipId = a.VIPID, ";
            sql += " vipName = a.VipName, ";
            sql += " recommendDate = convert(varchar(10),CreateTime,120) ";
            sql += " FROM dbo.Vip a ";
            sql += " WHERE a.HigherVipID = '" + userId + "' and isdelete=0";
            sql += "  Order by CreateTime Desc ";

            return this.SQLHelper.ExecuteDataset(sql);
        }

        #endregion

        public void UpdateVipRecommandTrace(string vipID, string higherVipId)
        {
            int level = 1;
            string path = higherVipId + "_" + vipID;
            DataSet ds = SQLHelper.ExecuteDataset(string.Format("SELECT RecommandLevel,AllPath FROM VipRecommandTrace WHERE VIPID='{0}'", higherVipId));
            if (ds.Tables[0].Rows.Count > 0)
            {
                level = int.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1;
                path = ds.Tables[0].Rows[0][1] + "_" + vipID;
            }
            string sql = string.Format(@"
                    insert into VipRecommandTrace
                    (VipRecommandID,VipID,HigerVipID,RecommandLevel,AllPath,CreateTime,IsDelete)
                    VALUES
                    (newid(),'{0}','{1}','{2}','{3}',getdate(),0)
                        ", vipID, higherVipId, level, path);
            SQLHelper.ExecuteNonQuery(sql);
        }

        public object IfRecordedRecommendTrace(string vipID, string reCommandId)
        {
            return SQLHelper.ExecuteScalar(string.Format("SELECT 1 FROM VipRecommandTrace WHERE VIPID='{0}' and [HigerVipID] = '{1}'", vipID, reCommandId));
        }

        #region 获取优惠劵列表
        /// <summary>
        /// 获取优惠劵列表
        /// </summary>
        /// <param name="vipID"></param>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public DataSet GetCouponList(string vipID, string TypeID)
        {
            //begdate<getdate() 生效
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@" SELECT a.CouponID,CouponDesc,CouponCode,a.CouponTypeID,b.CouponTypeName,a.Status,convert(date,BeginDate)  BeginDate,convert(date,EndDate) EndDate,b.Discount
             ,b.ParValue,b.IsRepeatable,b.IsMixable,b.ValidPeriod,a.CoupnName as CouponName,
             (case when BeginDate>getdate() then '-1' when EndDate='9999-12-31 00:00:00.000' then '0'  when EndDate is null then '0'  when  CONVERT(VARCHAR(10), GETDATE(), 120) between BeginDate and  EndDate then '0' else '1' end) isexpired,
             (case when EndDate='9999-12-31 00:00:00.000' then '1'  when EndDate is null then '1' else '0' end) iseffective,u.Comment 
            FROM Coupon a 
            INNER JOIN CouponType b ON a.CouponTypeID = b.CouponTypeID 
            LEFT JOIN CouponSource c ON b.CouponSourceID = c.CouponSourceID 
            INNER JOIN dbo.VipCouponMapping d ON a.CouponID = d.CouponID 
            LEFT JOIN couponUse u on  a.CouponID=u.CouponID 
            WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND d.IsDelete = 0 AND a.Status=0 and convert(varchar(10),a.EndDate,120) >= convert(varchar(10),getdate(),120)  
            AND NOT EXISTS(SELECT 1 FROM TOrderCouponMapping t WHERE t.IsDelete = 0 AND t.CouponId = a.CouponId) 
            AND d.VIPID = '{0}'
            ", vipID);
            if (!string.IsNullOrEmpty(TypeID))
            {
                strb.Append(" AND b.CouponTypeID = '" + TypeID + "'");
            }
            strb.Append("  ORDER BY isnull(a.Status,0) Asc,isexpired Asc,d.CreateTime desc");
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;
        }
        #endregion

        #region 根据优惠券ID获取优惠券详情
        /// <summary>
        /// 获取优惠券详情
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        public DataSet GetCouponDetail(string couponID, string userID)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat(@"SELECT a.CouponID,CouponDesc,CouponCode,a.CouponTypeID,b.CouponTypeName,b.Discount,a.CoupnName as CouponName
                                 ,a.Status,convert(date,BeginDate)  BeginDate,convert(date,EndDate) EndDate
                                 ,b.ParValue,b.IsRepeatable,b.IsMixable,b.ValidPeriod,f.VipName,f.WeiXin,
                                 (case when BeginDate>getdate() then '-1'  when EndDate='9999-12-31 00:00:00.000' then '0' when EndDate is null then '0' when  GETDATE() <= EndDate then '0' else '1' end) isexpired,
                                 (case when EndDate='9999-12-31 00:00:00.000' then '1' when EndDate is null then '1' else '0' end) iseffective
                                 ,(case when EndDate='9999-12-31 00:00:00.000' then 0 when EndDate is null then 0 else (select datediff( dd, getdate(), EndDate)) end) diffDay
                                 ,[SettingValue] LogoUrl,'' FollowUrl 
                                 FROM Coupon a 
                                 INNER JOIN CouponType b ON a.CouponTypeID = b.CouponTypeID 
                                 LEFT JOIN CouponSource c ON b.CouponSourceID = c.CouponSourceID 
                                 INNER JOIN dbo.VipCouponMapping d ON a.CouponID = d.CouponID 
                                 left join  Vip f on d.VIPID=f.VIPID
                                 LEFT JOIN [CustomerBasicSetting] CBS ON a.CustomerID=cbs.[CustomerID] and [SettingCode]='WebLogo' and CBS.[IsDelete]=0
                                 WHERE a.IsDelete = 0 AND b.IsDelete = 0 AND d.IsDelete = 0
                                 and a.CouponID='{0}' ", couponID);
            //strb.AppendFormat(" and a.CustomerID='{0}'", CurrentUserInfo.CurrentLoggingManager.Customer_Id);//考虑一下，暂时不要？因为洗衣客培训环境里用的是是正式环境的券。
            //上面加上商户的这个，在洗衣项目里暂时去掉，在连锁云掌柜这个项目里要加上******！！！


            //if (!string.IsNullOrEmpty(userID) && userID != "-1")
            //{
            //    strb.AppendFormat(" and d.VIPID='{0}'", userID);
            //}
            DataSet ds = this.SQLHelper.ExecuteDataset(strb.ToString());
            return ds;

        }
        #endregion

        #region 使用优惠劵
        /// <summary>
        /// 使用优惠劵
        /// </summary>
        /// <param name="couponID">优惠劵ID</param>
        /// <param name="doorID">使用门店ID</param>
        /// <returns></returns>
        public int BestowCoupon(string couponID, string doorID)
        {
            StringBuilder strb = new StringBuilder();
            strb.AppendFormat("update Coupon  set LastUpdateBy='{0}' ,LastUpdateTime=getdate() , DoorID='{1}' , Status='1' where CouponID='{2}' and IsDelete='0' and Status<>'1'  ", this.CurrentUserInfo.UserID, doorID, couponID);
            int res = this.SQLHelper.ExecuteNonQuery(strb.ToString());
            return res;

        }
        #endregion

        /// <summary>
        /// 根据券类商品ID获取有效优惠券
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetCouponByItemId(string itemId)
        {
            string couponId = string.Empty;
            string sql = string.Format(@"SELECT top 1 c.CouponID FROM Coupon c
                            inner join ItemCouponTypeMapping ictm on c.CouponTypeID=ictm.CouponTypeId
                            inner join CouponType CT on c.CouponTypeID=CT.CouponTypeID
                            LEFT JOIN VipCouponMapping vcm 
                            ON c.CouponID=vcm.CouponID 
                            where vcm.CouponID IS NULL and ictm.ItemId='{0}'
                            and c.BeginDate<=GETDATE() and c.EndDate>=GETDATE()
                            order by c.EndDate ASC", itemId);
            DataSet ds = SQLHelper.ExecuteDataset(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                couponId = ds.Tables[0].Rows[0]["CouponID"].ToString();
            }
            return couponId;
        }

        #region 批量生成优惠券
        public string GenerateCoupon(string couponTypeID, string couponName, DateTime? beginTime, string endTime, string description, string qty)
        {
            string sql = "GenerateCoupon";

            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            sqlParameter.Add(new SqlParameter("@UserID", CurrentUserInfo.UserID));
            sqlParameter.Add(new SqlParameter("@CouponTypeID", couponTypeID));
            sqlParameter.Add(new SqlParameter("@CouponName", couponName));
            sqlParameter.Add(new SqlParameter("@BeginTime", beginTime));
            sqlParameter.Add(new SqlParameter("@EndTime", endTime));
            sqlParameter.Add(new SqlParameter("@Description", description));
            sqlParameter.Add(new SqlParameter("@Qty", qty));

            return this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, sql, sqlParameter.ToArray()).ToString();
        }
        #endregion

        #region 管理优惠券列表
        public DataSet ManageCouponPagedSearch(string couponTypeID, string couponName, string couponUseStatus, string couponStatus, string beginTime, string endTime, string couponCode, string comment, string useTime, string createByName, string useEndTime, int pageIndex, int pageSize)
        {
            DataSet dataSet = new DataSet();

            string sql = "ManageCouponPagedSearch";

            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            if (!string.IsNullOrEmpty(couponUseStatus))
                sqlParameter.Add(new SqlParameter("@CouponUseStatus", couponUseStatus));

            if (!string.IsNullOrEmpty(couponStatus))
                sqlParameter.Add(new SqlParameter("@CouponStatus", couponStatus));

            if (!string.IsNullOrEmpty(couponTypeID))
                sqlParameter.Add(new SqlParameter("@CouponTypeID", couponTypeID));

            if (!string.IsNullOrEmpty(couponName))
                sqlParameter.Add(new SqlParameter("@CouponName", couponName));

            if (!string.IsNullOrEmpty(couponCode))
                sqlParameter.Add(new SqlParameter("@CouponCode", couponCode));

            if (!string.IsNullOrEmpty(comment))
                sqlParameter.Add(new SqlParameter("@Comment", comment));

            if (!string.IsNullOrEmpty(createByName))
                sqlParameter.Add(new SqlParameter("@CreateByName", createByName));

            if (!string.IsNullOrEmpty(useTime))
                sqlParameter.Add(new SqlParameter("@UseTime", useTime));

            if (!string.IsNullOrEmpty(useEndTime))
                sqlParameter.Add(new SqlParameter("@UseEndTime", useEndTime));

            if (!string.IsNullOrEmpty(beginTime))
                sqlParameter.Add(new SqlParameter("@BeginTime", beginTime));

            if (!string.IsNullOrEmpty(endTime))
                sqlParameter.Add(new SqlParameter("@EndTime", endTime));

            sqlParameter.Add(new SqlParameter("@PageIndex", pageIndex));
            sqlParameter.Add(new SqlParameter("@PageSize", pageSize));

            dataSet = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql, sqlParameter.ToArray());

            return dataSet;
        }
        #endregion

        #region 分发优惠券记录
        public DataSet BindCouponLog(string couponTypeID, string couponName, string couponCode, string vipCriteria, string bindingBeginTime, string bindingEndTime, string _operator, string pageIndex, string pageSize)
        {
            DataSet dataSet = new DataSet();

            string sql = "BindCouponLog";

            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            if (!string.IsNullOrEmpty(couponTypeID))
                sqlParameter.Add(new SqlParameter("@CouponTypeID", couponTypeID));

            if (!string.IsNullOrEmpty(couponName))
                sqlParameter.Add(new SqlParameter("@CouponName", couponName));

            if (!string.IsNullOrEmpty(couponCode))
                sqlParameter.Add(new SqlParameter("@CouponCode", couponCode));

            if (!string.IsNullOrEmpty(vipCriteria))
                sqlParameter.Add(new SqlParameter("@VipCriteria", vipCriteria));

            if (!string.IsNullOrEmpty(_operator))
                sqlParameter.Add(new SqlParameter("@Operator", _operator));

            if (!string.IsNullOrEmpty(bindingBeginTime))
                sqlParameter.Add(new SqlParameter("@BindingBeginTime", bindingBeginTime));

            if (!string.IsNullOrEmpty(bindingEndTime))
                sqlParameter.Add(new SqlParameter("@BindingEndTime", bindingEndTime));

            sqlParameter.Add(new SqlParameter("@PageIndex", pageIndex));
            sqlParameter.Add(new SqlParameter("@PageSize", pageSize));

            dataSet = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql, sqlParameter.ToArray());

            return dataSet;
        }
        #endregion

        /// <summary>
        /// 批量生成优惠券
        /// </summary>
        /// <param name="htCouponInfo"></param>
        /// <returns></returns>
        public void GenerateCoupon(Hashtable htCouponInfo)
        {
            string sql = "ProcGenerateCoupon";

            List<SqlParameter> sqlParameter = new List<SqlParameter>();
            sqlParameter.Add(new SqlParameter("@CustomerID", CurrentUserInfo.ClientID));
            sqlParameter.Add(new SqlParameter("@UserID", CurrentUserInfo.UserID));
            sqlParameter.Add(new SqlParameter("@CouponTypeID", htCouponInfo["CouponTypeID"]));
            sqlParameter.Add(new SqlParameter("@CouponName", htCouponInfo["CouponName"]));
            sqlParameter.Add(new SqlParameter("@CouponDesc", htCouponInfo["CouponDesc"]));
            sqlParameter.Add(new SqlParameter("@BeginDate", htCouponInfo["BeginTime"]));
            sqlParameter.Add(new SqlParameter("@EndDate", htCouponInfo["EndTime"]));
            sqlParameter.Add(new SqlParameter("@IssuedQty", htCouponInfo["IssuedQty"]));

            this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, sql, sqlParameter.ToArray());
        }
        /// <summary>
        /// 根据优惠券类型获取可用的优惠券ID
        /// </summary>
        /// <param name="couponTypeID"></param>
        /// <returns></returns>
        public string GetUsableCouponID(string couponTypeID) 
        {
            string sql = string.Format(@"
             SELECT c.CouponID FROM Coupon c 
		     INNER JOIN CouponType ct ON ct.CouponTypeID=c.couponTypeID
		     LEFT JOIN VipCouponMapping vcm ON vcm.CouponID=c.CouponID
		     WHERE c.couponTypeID='{0}' AND vcm.VipCouponMapping IS NULL
		     AND c.isdelete=0",couponTypeID);
            return this.SQLHelper.ExecuteScalar(sql.ToString()).ToString();
        }

        /// <summary>
        /// 根据优惠券类型获取优惠券
        /// </summary>
        /// <param name="couponTypeID"></param>
        /// <returns></returns>
        public DataSet GetCouponBycouponType(string couponTypeID)
        {

            DataSet dataSet = new DataSet();
            string sql = string.Format(@"
             SELECT c.* FROM Coupon c 
		     INNER JOIN CouponType ct ON ct.CouponTypeID=c.couponTypeID
		     WHERE c.couponTypeID='{0}' 
		     AND c.isdelete=0", couponTypeID);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }


        /// <summary>
        /// 根据优惠券类型获取优惠券id
        /// </summary>
        /// <param name="couponTypeID"></param>
        /// <returns></returns>
        public DataSet GetCouponIDBycouponType(string couponTypeID)
        {

            DataSet dataSet = new DataSet();
            string sql = string.Format(@"
             SELECT c.CouponCode FROM Coupon c 
		     INNER JOIN CouponType ct ON ct.CouponTypeID=c.couponTypeID
		     WHERE c.couponTypeID='{0}' 
		     AND c.isdelete=0", couponTypeID);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }

        /// <summary>
        /// 优惠券核销列表
        /// Create By: Sun Xu
        /// Create Date:2015-11-02
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="couponCode"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public DataSet WriteOffCouponList(string mobile, string couponCode, string status, int pageSize, int pageIndex)
        {
            DataSet dataSet = new DataSet();

            string sql = "sp_WriteOffCouponList";

            List<SqlParameter> sqlParameter = new List<SqlParameter>();

            sqlParameter.Add(new SqlParameter("@Mobile", mobile));
            sqlParameter.Add(new SqlParameter("@CouponCode", couponCode));
            sqlParameter.Add(new SqlParameter("@Status", status));
            sqlParameter.Add(new SqlParameter("@PageIndex", pageIndex));
            sqlParameter.Add(new SqlParameter("@PageSize", pageSize));
            sqlParameter.Add(new SqlParameter("@CustomerId", CurrentUserInfo.ClientID));

            dataSet = this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, sql, sqlParameter.ToArray());

            return dataSet;
        }
        /// <summary>
        /// 查询未被绑定用户的优惠券
        /// </summary>
        /// <param name="strCouponTypeId"></param>
        /// <returns></returns>
        public DataSet GetCouponIdByCouponTypeID(string strCouponTypeId)
        {
            string strSql = string.Format(@"select top 1  a. *,C.CouponTypeName,C.ParValue 
                                            from Coupon a WITH(NOLOCK) 
                                            INNER JOIN dbo.CouponType C ON A.CouponTypeID=C.CouponTypeID 
                                            WHERE   a.IsDelete = 0 AND a.[Status] = 0  and a.CouponTypeID='{0}'"
                                        , strCouponTypeId);
            return this.SQLHelper.ExecuteDataset(strSql);
        }
        /// <summary>
        /// 优惠券剩余数量
        /// </summary>
        /// <param name="strCouponTypeId"></param>
        /// <returns></returns>
        public int GetCouponCountByCouponTypeID(string strCouponTypeId)
        {
            //string strSql = string.Format("select  count(1)CouponCount from Coupon a WITH(NOLOCK) LEFT join VipCouponMapping b WITH(NOLOCK) ON a.CouponID=b.CouponID WHERE   a.IsDelete = 0 AND a.[Status] = 0 and  b.VIPID is null and a.CouponTypeID='{0}'", strCouponTypeId);
            string strSql = string.Format("SELECT  COUNT(1) CouponCount FROM    Coupon a WITH ( NOLOCK )WHERE   a.IsDelete = 0     AND a.[Status] = 0 AND a.CouponTypeID = '{0}'", strCouponTypeId);
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql));
        }
        /// <summary>
        /// 优惠券绑定vip
        /// </summary>
        /// <param name="strVipId"></param>
        /// <param name="strCouponTypeID"></param>
        /// <returns></returns>
        public int CouponBindVip(string strVipId, string strCouponTypeID)
        {
            var parameters = new List<SqlParameter>();
            var para = new SqlParameter("@VipId", SqlDbType.NVarChar);
            para.Value = strVipId;
            parameters.Add(para);

            para = new SqlParameter("@CouponTypeID", SqlDbType.NVarChar);
            para.Value = strCouponTypeID;
            parameters.Add(para);

            var result = new SqlParameter("@Result", SqlDbType.Int);
            result.Direction = ParameterDirection.Output;
            parameters.Add(result);

            SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_CouponBindVip", parameters.ToArray());

            return Convert.ToInt32(result.Value.ToString());
        }
     }
}
