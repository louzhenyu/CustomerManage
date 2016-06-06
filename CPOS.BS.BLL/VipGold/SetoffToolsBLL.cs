/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 13:59:40
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class SetoffToolsBLL
    {
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// 获取集客工具数量
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ApplicationType"></param>
        /// <param name="pBeShareVipID"></param>
        /// <returns></returns>
        public int GeSetoffToolsListCount(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, string pSetoffEventID)
        {
            return _currentDAO.GeSetoffToolsListCount(entity, ApplicationType, pBeShareVipID, pSetoffEventID);
        }
        /// <summary>
        /// 获取集客工具列表
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ApplicationType"></param>
        /// <param name="pBeShareVipID"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public DataSet GetSetoffToolsList(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, int Page, int PageSize, string pSetoffEventID)
        {
            return _currentDAO.GetSetoffToolsList(entity, ApplicationType, pBeShareVipID, Page, PageSize, pSetoffEventID);
        }
        /// <summary>
        /// 获取未推送或分享数量
        /// </summary>
        /// <param name="ShareVipType">分享人ID</param>
        /// <param name="BeShareVipID">被分享人ID</param>
        /// <param name="BusTypeCode">分享类型</param>
        /// <returns></returns>
        public int GetIsPushCount(string ShareVipType, string BeShareVipID, string BusTypeCode, string SetOffEventID)
        {
            return _currentDAO.GetIsPushCount(ShareVipType, BeShareVipID, BusTypeCode, SetOffEventID);
        }

        public DataSet GetToolsDetails(string SetoffEventID)
        {
            return this._currentDAO.GetToolsDetails(SetoffEventID);
        }

        /// <summary>
        /// 获取未读集客工具个数
        /// </summary>
        /// <param name="UserId">用户编号</param>
        /// <param name="CustomerId">商户编号</param>
        /// <param name="NoticePlatformTypeId">平台编号1=微信用户 2=APP员工</param>
        /// <param name="SetoffTypeId">1=会员集客 2=员工集客 3=超级分销商</param>
        /// <returns></returns>
        public int GetUnReadSetoffToolsCount(string UserId, string CustomerId, int NoticePlatformTypeId, int SetoffTypeId)
        {
            return _currentDAO.GetUnReadSetoffToolsCount(UserId, CustomerId, NoticePlatformTypeId, SetoffTypeId);
        }
    }
}