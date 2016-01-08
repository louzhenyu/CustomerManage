/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
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
    public partial class WApplicationInterfaceBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<WApplicationInterfaceEntity> GetWebWApplicationInterface(WApplicationInterfaceEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WApplicationInterfaceEntity> list = new List<WApplicationInterfaceEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebWApplicationInterface(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WApplicationInterfaceEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 获取打印配送单对应的标题 包含客户微信信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetWebWApplicationDelivery(string clientId)
        {
            return this._currentDAO.GetWebWApplicationDelivery(clientId);
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebWApplicationInterfaceCount(WApplicationInterfaceEntity entity)
        {
            return _currentDAO.GetWebWApplicationInterfaceCount(entity);
        }
        #endregion

        #region Jermyn20131120处理管理平台微信公众号信息
        public bool setCposApMapping(WApplicationInterfaceEntity info)
        {
            return _currentDAO.setCposApMapping(info);
        }
        #endregion

        #region Jermyn20131120处理管理平台微信公众号信息
        public bool setCreateWXMenu(WApplicationInterfaceEntity info)
        {
            return _currentDAO.setCreateWXMenu(info);
        }
        #endregion

        public DataSet GetAccountList(string customerId, int pageIndex, int pageSize)
        {
            return this._currentDAO.GetAccountList(customerId, pageIndex, pageSize);
        }

        public int GetTotalcount(string customerId)
        {
            return this._currentDAO.GetTotalcount(customerId);
        }

        /// <summary>
        /// 获取是否支持多客服
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="WeixinId"></param>
        /// <returns></returns>
        public DataSet GetIsMoreCS(string pCustomerId, string pWeixinId)
        {
            return this._currentDAO.GetIsMoreCs(pCustomerId, pWeixinId);
        }

        public void RemoveSession(string Id)
        {
            this._currentDAO.RemoveSession(Id);
        }


        /// <summary>
        /// Trade-获取平台微信公众号信息  add by Henry 2014-12-11
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <returns></returns>
        public WApplicationInterfaceEntity[] GetCloudWAppInterface(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            return this._currentDAO.GetCloudWAppInterface(pWhereConditions, pOrderBys);
        }

        /// <summary>
        /// 获取奥斯认证会员中心链接地址
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public string GetOsVipMemberCentre(string OpenID)
        {
            return this._currentDAO.GetOsVipMemberCentre(OpenID);
        }

        public DataSet GetWXOpenOAuth(string OpenOAuthAppid)
        {
            return this._currentDAO.GetWXOpenOAuth(OpenOAuthAppid);
        }

    }
}