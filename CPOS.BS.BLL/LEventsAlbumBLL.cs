/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-18 17:58
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
    public partial class LEventsAlbumBLL
    {
        #region 获取相册列表获取
        /// <summary>
        /// 相册列表获取
        /// </summary>
        public IList<LEventsAlbumEntity> GetAlbumList(LEventsAlbumEntity albumEntity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<LEventsAlbumEntity> imageList = new List<LEventsAlbumEntity>();

            var ds = this._currentDAO.GetAlbumList(albumEntity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                imageList = DataTableToObject.ConvertToList<LEventsAlbumEntity>(ds.Tables[0]);
            }
            return imageList;
        }
        /// <summary>
        /// 相册列表数量获取
        /// </summary>
        public int GetAlbumCount(LEventsAlbumEntity albumEntity)
        {
            return this._currentDAO.GetAlbumCount(albumEntity);
        }
        #endregion

        #region 获取绑定模块列表
        /// <summary>
        /// 获取绑定模块列表
        /// </summary>
        public DataSet GetAlbumModuleList(string moduleType, string moduleName, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            return this._currentDAO.GetAlbumModuleList(moduleType, moduleName, Page, PageSize);
        }
        /// <summary>
        /// 获取绑定模块列表数量
        /// </summary>
        public int GetAlbumModuleCount(string moduleType, string moduleName)
        {
            return this._currentDAO.GetAlbumModuleCount(moduleType, moduleName);
        }
        #endregion

        #region 获取相片列表
        /// <summary>
        /// 获取相片列表
        /// </summary>
        public IList<LEventsAlbumPhotoEntity> GetAlbumImageList(string albumId, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<LEventsAlbumPhotoEntity> imageList = new List<LEventsAlbumPhotoEntity>();

            var ds = this._currentDAO.GetAlbumImageList(albumId, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                imageList = DataTableToObject.ConvertToList<LEventsAlbumPhotoEntity>(ds.Tables[0]);
            }
            return imageList;
        }
        /// <summary>
        /// 获取相片数量
        /// </summary>
        public int GetAlbumImageCount(string albumId)
        {
            return this._currentDAO.GetAlbumImageCount(albumId);
        }
        #endregion

        #region 2014-04-23 修改者:tiansheng.zhu
         
        #region 执行分页查询
        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public DataSet PagedQueryNews(string pWhere,int pPageSize, int pCurrentPageIndex, out int pPageCount)
        {
            return _currentDAO.GetLEventsAlbumList(pWhere, pPageSize, pCurrentPageIndex, out pPageCount);
        }
        #endregion

        #endregion

        #region 根据相册视频类型查询  Add by changjian.tian 2014-6-3

        public DataSet GetLEventsAlbumByType(string pModuleType)
        {
            return _currentDAO.GetLEventsAlbumByType(pModuleType);
        }
        #endregion
    }
}