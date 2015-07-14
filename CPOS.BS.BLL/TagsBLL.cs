/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/2 13:33:22
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
    public partial class TagsBLL
    {
        #region Web列表获取
        /// <summary>
        /// Web列表获取
        /// </summary>
        public IList<TagsEntity> GetWebTags(TagsEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<TagsEntity> list = new List<TagsEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebTags(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<TagsEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetWebTagsCount(TagsEntity entity)
        {
            return _currentDAO.GetWebTagsCount(entity);
        }
        #endregion

        #region Jermyn20131127 处理初始化时自动复制固定标签
        /// <summary>
        /// 拷贝固定的标签
        /// </summary>
        /// <returns></returns>
        public bool setCopyTag(string CustomerId)
        {
            return _currentDAO.setCopyTag(CustomerId);
        }
        #endregion

        public DataSet GetVipTagsList(string TypeId, string vipid)
        {
            return this._currentDAO.GetVipTagsList(TypeId, vipid);
        }
        public DataSet GetTagsList(string TypeId, string CustomerId)
        {
            return this._currentDAO.GetTagsList(TypeId, CustomerId);
        }
        /// <summary>
        /// 根据id删除信息
        /// </summary>
        /// <param name="propIds"></param>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        public bool DeleteByIds(string propIds, TagsTypeEntity TagsTypeEn)
        {
            return this._currentDAO.DeleteByIds(propIds, TagsTypeEn);
        }
    }
}