/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/14 11:26:51
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
    /// 表WeixinWall的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class WeixinWallDAO : Base.BaseCPOSDAO, ICRUDable<WeixinWallEntity>, IQueryable<WeixinWallEntity>
    {
        #region 6.1 获取微信墙未读取的数据集合
        public int GetWeiXinWallCount(string eventKeyword)
        {
            string sql = GetWeiXinWallSql(eventKeyword);
            sql += " select count(*) From #tmp ;";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        public DataSet GetWeiXinWall(string eventKeyword)
        {
            string sql = GetWeiXinWallSql(eventKeyword);
            sql += " UPDATE weixinwall SET hasReader = 1 WHERE wallid IN (SELECT wallId FROM #tmp); ";
            sql += " select * From #tmp ;";
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        private string GetWeiXinWallSql(string eventKeyword)
        {
            string sql = "SELECT a.* "
                        + " ,b.VipName UserName "
                        + " ,'http://o2oapi.aladingyidong.com/iAlumni/Image/wallDefault.png' ImageURL "
                        + " ,DisplayIndex = row_number() over(order by a.createtime desc ) "
                        + " into #tmp  FROM weixinwall a "
                        + " INNER JOIN dbo.Vip b "
                        + " ON(a.openid = b.WeiXinUserId "
                        + " AND a.weixinid = b.WeiXin) "
                        + " WHERE 1=1 AND ISNULL(a.hasReader,0) = 0 "
                        + " AND a.eventkeyword = '"+eventKeyword+"' "
                        + " ; ";

            return sql;
        }
        #endregion
    }
}
