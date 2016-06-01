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
    public partial class WMaterialTextBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<WMaterialTextEntity> GetWebList(WMaterialTextEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            DataSet ds = new DataSet();

            ds = _currentDAO.GetWebList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WMaterialTextEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebListCount(WMaterialTextEntity entity)
        {
            return _currentDAO.GetWebListCount(entity);
        }
        #endregion

        #region 根据模板标识获取图文消息集合 Jermyn20131209
        /// <summary>
        /// 根据模板标识获取图文消息集合
        /// </summary>
        /// <param name="ModelId">模板标识</param>
        /// <returns></returns>
        public IList<WMaterialTextEntity> GetMaterialTextListByModelId(string ModelId)
        {

            IList<WMaterialTextEntity> list = new List<WMaterialTextEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetMaterialTextListByModelId(ModelId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WMaterialTextEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        public WMaterialTextEntity[] GetWMaterialTextList(string pCustomerID, string pName, string pTextId,string typeId, int? pPageSize, int? pPageIndex)
        {
            return this._currentDAO.GetWMaterialTextList(pCustomerID, pName, pTextId,typeId, pPageSize ?? 15, pPageIndex ?? 0);
        }

        public int GetWMaterialTextListCount(string pCustomerID, string pName, string pMaterialTextId, string typeId)
        {
            return this._currentDAO.GetWMaterialTextListCount(pCustomerID, pName, pMaterialTextId, typeId);
        }

        #region GetWMaterialTextPage
        /// <summary>
        ///获取分页列表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="typeID"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <returns></returns>
        public DataSet GetWMaterialTextPage(string title, string typeID, int? pPageSize, int? pPageIndex)
        {
            return this._currentDAO.GetWMaterialTextPage(title, typeID, pPageSize, pPageIndex);


        }
        #endregion

        #region GetWmType
        /// <summary>
        ///获取分类
        /// </summary>
        /// <returns></returns>
        public DataSet GetWmType()
        {
            DataSet ds = this._currentDAO.GetWmType();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].NewRow();
                dr[0] = "";
                dr[1] = "---请选择---";
                ds.Tables[0].Rows.InsertAt(dr, 0);
            }
            return ds;
        }
        #endregion

        public DataSet GetMaterialTextTitleList(string textId, string customerId)
        {
            return this._currentDAO.GetMaterialTextTitleList(textId, customerId);
        }
        public bool CheckName(string appId, string name, string textId)
        {
            return this._currentDAO.CheckName(appId, name, textId);
        }
    }
}