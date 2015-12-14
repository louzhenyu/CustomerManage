/*
 * Author		:Jack.Chen
 * EMail		:jack.chen@jitmarketing.cn
 * Company		:JIT
 * Create On	:2014/4/4 20:36:59
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// MHCategoryAreaBLL 
    /// </summary>
    public partial class MHCategoryAreaBLL
    {
        /// <summary>
        /// 获取客户下的所有数据
        /// </summary>
        /// <returns></returns>
        //public MHCategoryAreaEntity[] GetByCustomerID()
        //{
        //    return this._currentDAO.GetByCustomerID();
        //}
        #region 更新商品分类区域表

        /// <summary>
        /// 更新商品分类区域表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public void UpdateMHCategoryArea(MHCategoryAreaEntity entity)
        //{
        //    this._currentDAO.UpdateMHCategoryArea(entity);
        //}

        //public void DeleteItemCategoryByGroupIdandHomeID(int GroupID, Guid HomeId)
        //{
        //    this._currentDAO.DeleteItemCategoryByGroupIdandHomeID(GroupID, HomeId);
        //}
        //public void DeleteCategoryGroupByGroupIdandCustomerId(int GroupID, string customerId, string strHomeId)
        //{
        //    this._currentDAO.DeleteCategoryGroupByGroupIdandCustomerId(GroupID, customerId, strHomeId);
        //}
        #endregion

        public int GetObjectTypeIDByGroupId(int intGropuId)
        {
            return this._currentDAO.GetObjectTypeIDByGroupId(intGropuId);
        }
        public int DeleteCategoryAreaByGroupId(int intGropuId)
        {
            return this._currentDAO.DeleteCategoryAreaByGroupId(intGropuId);
        }
    }
}
