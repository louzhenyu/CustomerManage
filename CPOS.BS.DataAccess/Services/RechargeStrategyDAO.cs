/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-8-25 11:44:14
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
    /// 表RechargeStrategy的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class RechargeStrategyDAO : Base.BaseCPOSDAO, ICRUDable<RechargeStrategyEntity>, IQueryable<RechargeStrategyEntity>
    {
        /// <summary>
        /// 获取订单、出入库和商品信息(支付回调使用)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetInoutOrderItems(string orderId)
        {
            string sql = "select *  from VwInoutOrderItems where OrderId='" + orderId + "'";
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql);
        }
        /// <summary>
        /// 根据当前卡类型ID 获取充值活动列表
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <param name="vipCardTypeID"></param>
        /// <returns></returns>
        public DataSet GetRechargeActivityList(string CustomerID, string vipCardTypeID,int ActType)
        {
            string strsql = @" SELECT DISTINCT A.ActivityID,ISNULL(B.RechargeAmount,0) as RechargeAmount,ISNULL(B.GiftAmount,0) as GiftAmount,B.RuleType FROM C_TargetGroup A 
                               INNER JOIN RechargeStrategy B ON A.ActivityID=B.ActivityID
                               INNER JOIN C_Activity C ON C.ActivityID=A.ActivityID
                               WHERE A.CustomerID=@CustomerID AND A.GroupType=1 AND (C.[Status]=3 or C.[Status]=0)  AND ((C.StartTime<=GetDate() and C.EndTime>=GETDATE()) or C.IsLongTime=1)  
                               AND A.IsDelete=0 AND B.IsDelete=0 AND C.IsDelete=0 ";
            if (!string.IsNullOrEmpty(vipCardTypeID))
            {
                strsql += " AND (A.ObjectID=@vipCardTypeID OR C.IsAllVipCardType=1) ";
            }
            if (ActType > 0)
            {
                strsql += " AND C.ActivityType=@ActType  ";
            }
            strsql += " Order by RechargeAmount ASC ";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerID",CustomerID),
                new SqlParameter("@vipCardTypeID",vipCardTypeID),
                new SqlParameter("@ActType",ActType)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, strsql, parameter);
        }
    }
}
