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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class WeixinWallBLL
    {
        #region 6.1 获取微信墙未读取的数据集合
        /// <summary>
        /// 6.1 获取微信墙未读取的数据集合
        /// </summary>
        /// <param name="eventKeyword"></param>
        /// <returns></returns>
        public WeixinWallEntity GetWeiXinWall(string eventKeyword)
        {
            WeixinWallEntity wallInfo = new WeixinWallEntity();
            IList<WeixinWallEntity> wallList = new List<WeixinWallEntity>();
            wallInfo.WallsCount = _currentDAO.GetWeiXinWallCount(eventKeyword);
            if (wallInfo.WallsCount > 0)
            {
                DataSet ds = new DataSet();
                ds = _currentDAO.GetWeiXinWall(eventKeyword);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    wallList = DataTableToObject.ConvertToList<WeixinWallEntity>(ds.Tables[0]);
                }
                wallInfo.WallList = wallList;
            }
            return wallInfo;
        }
        #endregion
    }
}