/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 15:34:04
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
    /// 表VipShowLog的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipShowLogDAO : Base.BaseCPOSDAO, ICRUDable<VipShowLogEntity>, IQueryable<VipShowLogEntity>
    {
        #region
        /// <summary>
        /// 获取近期的值
        /// </summary>
        /// <param name="Weixin"></param>
        /// <param name="TimeLength"></param>
        /// <returns></returns>
        public DataSet GetRecentfollowers(string Weixin, string TimeLength)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            sql = " SELECT DISTINCT OpenID,VipName,(SELECT Gender FROM cpos_ap.dbo.vip x WHERE x.WeiXinUserId=a.OpenID) Gender,City,Language,a.CreateTime,WeiXin "
                + " ,(SELECT VIPID FROM cpos_ap.dbo.vip x WHERE x.WeiXinUserId=a.OpenID) VIPID,'3' VipSourceId "
                + " ,(SELECT VipCode FROM cpos_ap.dbo.vip x WHERE x.WeiXinUserId=a.OpenID) VipCode "
                + " ,(SELECT Birthday FROM cpos_ap.dbo.vip x WHERE x.WeiXinUserId=a.OpenID) Birthday "
                + " ,(SELECT Phone FROM cpos_ap.dbo.vip x WHERE x.WeiXinUserId=a.OpenID) Phone "
                + " ,ISNULL((SELECT COUNT(*) FROM dbo.Vip x WHERE x.WeiXinUserId = a.OpenID),0) IsOld "
                + " ,(SELECT Integration FROM cpos_ap.dbo.vip x WHERE x.WeiXinUserId=a.OpenID) Integration "
                + " FROM cpos_ap.dbo.VipShowLog a  "
                + " INNER JOIN (SELECT MAX(CreateTime) CreateTime FROM cpos_ap.dbo.VipShowLog GROUP BY OpenID ) b "
                + " ON(a.CreateTime = b.CreateTime)  "
                + " WHERE a.IsDelete = 0 AND a.IsShow = 1 " 
                + " and datediff(minute ,a.CreateTime,getdate()) < '" + TimeLength + "' "
                + " AND isnull(WeiXin,1) = '" + Weixin + "' "
                + " order by  a.CreateTime DESC ;";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion

        #region
        /// <summary>
        /// 根据流水号获取客户信息
        /// </summary>
        /// <param name="Weixin"></param>
        /// <param name="TimeLength"></param>
        /// <returns></returns>
        public DataSet GetVipInfoBySerialNumber()
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            sql = " SELECT DISTINCT b.OpenId,a.VipName,a.Gender,a.City,'' LANGUAGE,b.CreateTime,a.WeiXin "
                + " ,a.VIPID,a.VipSourceId,a.VipCode,a.Birthday,a.Phone,1 IsOld,b.No SerialNumber,a.Integration"
                + " FROM dbo.Vip a "
                + " INNER JOIN dbo.WLegalize b ON(a.WeiXinUserId = b.OpenId) "
                + " WHERE a.IsDelete = 0 "
                + " AND b.IsDelete = 0 ";
            //if (SerialNumber != null && !SerialNumber.Equals(""))
            //{
            //    sql += " AND b.No = '" + SerialNumber + "'";
            //}
            //else {
                sql += "and datediff(minute ,b.CreateTime,getdate()) < '10' ";
            //}
            sql += " order by b.createTime desc ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        #endregion
    }
}
