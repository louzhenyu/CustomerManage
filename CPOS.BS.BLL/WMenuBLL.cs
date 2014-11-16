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
using JIT.Utility.Web.ComponentModel.ExtJS.Menu;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class WMenuBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<WMenuEntity> GetWebWMenu(WMenuEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WMenuEntity> list = new List<WMenuEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebWMenu(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WMenuEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebWMenuCount(WMenuEntity entity)
        {
            return _currentDAO.GetWebWMenuCount(entity);
        }
        #endregion

        #region 获取菜单
        public IList<WMenuEntity> GetWebWMenuTree(WMenuEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WMenuEntity> list = new List<WMenuEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebWMenu(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WMenuEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebWMenuTreeCount(WMenuEntity entity)
        {
            return _currentDAO.GetWebWMenuCount(entity);
        }
        #endregion

        #region 根据customerCode获取CustomerID
        /// <summary>
        /// 根据微信公众账号获取CustomerID
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public string GetCustomerIDByCustomerCode(string customerCode)
        {
            return _currentDAO.GetCustomerIDByCustomerCode(customerCode);
        }
        #endregion

        #region Jermyn 20131107 根据微信号码获取客户标识
        /// <summary>
        /// 根据微信号码获取客户标识
        /// </summary>
        /// <param name="weixinID"></param>
        /// <returns></returns>
        public string GetCustomerIdByWx(string weixinID)
        {
            return _currentDAO.GetCustomerIdByWx(weixinID);
        }
        #endregion


        public DataSet GetMenuList(string customerId, string applicationId)
        {
            return this._currentDAO.GetMenuList(customerId, applicationId);
        }

        public DataSet GetMenuDetail(string menuId)
        {
            return this._currentDAO.GetMenuDetail(menuId);
        }

        public DataSet GetMenuTextIdListByMenuId(string customerId, string menuId)
        {
            return this._currentDAO.GetMenuTextIdListByMenuId(customerId, menuId);
        }

        public DataSet GetMenuTextIdList(string customerId, string weiXinId,string menuList)
        {
            return this._currentDAO.GetMenuTextIdList(customerId, weiXinId, menuList);
        }

        public int GetLevel2CountByMenuId(string menuId)
        {
            return this._currentDAO.GetLevel2CountByMenuId(menuId);
        }

        public void UpdateMenuData(string menuId, int status, Guid? pageid, string pageParamJson, string pageUrlJson,int unionTypeId)
        {
            this._currentDAO.UpdateMenuData(menuId, status, pageid, pageParamJson, pageUrlJson, unionTypeId);
        }

        #region 检查删除模板是否关联


        public bool CheckDelete(string ids)
        {
            return this._currentDAO.CheckDelete(ids);
        }
        #endregion


        public bool CheckExistLevel2Menu(string menuId)
        {
            return this._currentDAO.CheckExistLevel2Menu(menuId);
        }
    }
}