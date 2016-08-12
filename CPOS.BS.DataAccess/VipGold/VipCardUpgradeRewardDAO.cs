/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 15:10:55
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
    /// 表VipCardUpgradeReward的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipCardUpgradeRewardDAO : Base.BaseCPOSDAO, ICRUDable<VipCardUpgradeRewardEntity>, IQueryable<VipCardUpgradeRewardEntity>
    {
        /// <summary>
        /// 获取开卡礼列表
        /// </summary>
        /// <param name="VipCardTypeID">卡类型编号</param>
        /// <param name="CustomerId">商户编号</param>
        /// <returns></returns>
        public DataSet GetVipCardUpgradeRewardList(int ? VipCardTypeID, string CustomerId)
        {
            string sql = @"SELECT *,[Type]=2 FROM VipCardUpgradeReward AS cvur
									INNER JOIN SysVipCardType AS svct ON cvur.VipCardTypeID=svct.VipCardTypeID
									INNER JOIN CouponType AS ct ON ct.CouponTypeId=cvur.CouponTypeId
									WHERE cvur.IsDelete=0 AND cvur.CustomerID=@CustomerId AND cvur.VipCardTypeID=@VipCardTypeID";
            SqlParameter[] parameter = new SqlParameter[]{
                    new SqlParameter("@CustomerId",CustomerId),
                    new SqlParameter("@VipCardTypeID",VipCardTypeID)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }
    }
}
