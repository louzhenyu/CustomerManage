/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/31 15:58:37
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class MHAdAreaBLL
    {
        #region 获取广告集合

        /// <summary>
        /// 获取广告集合
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetAdList(string homeId)
        {
            return this._currentDAO.GetAdList(homeId);
        }
        /// <summary>
        /// 获取搜索框信息
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetMHSearchArea(string homeId)
        {
            return this._currentDAO.GetMHSearchArea(homeId);
        }

        /// <summary>
        /// 获取客户下的所有数据
        /// </summary>
        /// <returns></returns>
        public MHAdAreaEntity[] GetByCustomerID()
        {
            return this._currentDAO.GetByCustomerID();
        }
        public MHAdAreaEntity[] GetAdByHomeId(string strHomeId)
        {
            return this._currentDAO.GetAdByHomeId(strHomeId);
        }
        #endregion

        #region 获取活动区域数据

        /// <summary>
        /// 获取活动区域数据
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetEventInfo(string homeId)
        {
            return this._currentDAO.GetEventInfo(homeId);
        }
        public DataSet GetEventInfoByGroupId(string homeId, string strGroupId)
        {
            return this._currentDAO.GetEventInfoByGroupId(homeId, strGroupId);
        }
        public DataSet GetEventListInfoByGroupId(string homeId, string strGroupId)
        {
            return this._currentDAO.GetEventListInfoByGroupId(homeId, strGroupId);
        }
        #endregion

        #region 获取分类分组ID

        /// <summary>
        /// 获取分类分组ID
        /// </summary>
        /// <param name="homeId"></param>
        /// <returns></returns>
        public DataSet GetCategoryGroupId(string homeId)
        {
            return this._currentDAO.GetCategoryGroupId(homeId);
        }

        #endregion

        #region 获取商品集合

        /// <summary>
        /// 获取商品集合
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataSet GetItemList(string groupId, string homeId)
        {
            return this._currentDAO.GetItemList(groupId,homeId);
        }
        public DataSet GetCategoryProductList(string groupId, string homeId, int intShowCount)
        {
            return this._currentDAO.GetCategoryProductList(groupId, homeId, intShowCount);
        }
        public DataSet GetGroupProductList(string groupId, string homeId, int intShowCount)
        {
            return this._currentDAO.GetGroupProductList(groupId, homeId, intShowCount);
        }
        
        public DataSet GetModelTypeIdByGroupId(string groupId,string strHomeId)
        {
            return this._currentDAO.GetModelTypeIdByGroupId(groupId, strHomeId);
        }
        public DataSet GetModelTypeIdByGroupId(string groupId)
        {
            return this._currentDAO.GetModelTypeIdByGroupId(groupId);
        }
        #endregion

        #region 更新商品分类区域表

        /// <summary>
        /// 更新商品分类区域表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void UpdateMHCategoryArea(MHCategoryAreaEntity entity)
        {
            this._currentDAO.UpdateMHCategoryArea(entity);
        }

        public void DeleteItemCategoryByGroupIdandHomeID(int GroupID, Guid HomeId)
        {
            this._currentDAO.DeleteItemCategoryByGroupIdandHomeID(GroupID, HomeId);
        }
        public void DeleteCategoryGroupByGroupIdandCustomerId(int GroupID, string customerId,string strHomeId)
        {
            this._currentDAO.DeleteCategoryGroupByGroupIdandCustomerId(GroupID, customerId, strHomeId);
        }
        #endregion
    }
}