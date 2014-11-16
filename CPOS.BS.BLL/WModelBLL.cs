/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/27 20:34:17
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
    public partial class WModelBLL
    {
        #region 获取根据公众平台获取模板集合
        public IList<WModelEntity> GetWModelListByAppId(string AppId)
        {
            IList<WModelEntity> list = new List<WModelEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWModelListByAppId(AppId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WModelEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<WModelEntity> GetList(WModelEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WModelEntity> list = new List<WModelEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WModelEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(WModelEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        public bool IsExist(string ApplicationId, string ModelName, string ModelId)
        {
            return _currentDAO.IsExist(ApplicationId, ModelName, ModelId);
        }

        public DataSet GetWModelList(string customerId,string applicationId, int pageIndex, int pageSize)
        {
            return this._currentDAO.GetWModelList(customerId,applicationId, pageIndex, pageSize);
        }
    }
}