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
    /// ҵ����  
    /// </summary>
    public partial class LEventsAlbumBLL
    {
        #region ��ȡ����б��ȡ
        /// <summary>
        /// ����б��ȡ
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
        /// ����б�������ȡ
        /// </summary>
        public int GetAlbumCount(LEventsAlbumEntity albumEntity)
        {
            return this._currentDAO.GetAlbumCount(albumEntity);
        }
        #endregion

        #region ��ȡ��ģ���б�
        /// <summary>
        /// ��ȡ��ģ���б�
        /// </summary>
        public DataSet GetAlbumModuleList(string moduleType, string moduleName, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            return this._currentDAO.GetAlbumModuleList(moduleType, moduleName, Page, PageSize);
        }
        /// <summary>
        /// ��ȡ��ģ���б�����
        /// </summary>
        public int GetAlbumModuleCount(string moduleType, string moduleName)
        {
            return this._currentDAO.GetAlbumModuleCount(moduleType, moduleName);
        }
        #endregion

        #region ��ȡ��Ƭ�б�
        /// <summary>
        /// ��ȡ��Ƭ�б�
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
        /// ��ȡ��Ƭ����
        /// </summary>
        public int GetAlbumImageCount(string albumId)
        {
            return this._currentDAO.GetAlbumImageCount(albumId);
        }
        #endregion

        #region 2014-04-23 �޸���:tiansheng.zhu
         
        #region ִ�з�ҳ��ѯ
        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public DataSet PagedQueryNews(string pWhere,int pPageSize, int pCurrentPageIndex, out int pPageCount)
        {
            return _currentDAO.GetLEventsAlbumList(pWhere, pPageSize, pCurrentPageIndex, out pPageCount);
        }
        #endregion

        #endregion

        #region ���������Ƶ���Ͳ�ѯ  Add by changjian.tian 2014-6-3

        public DataSet GetLEventsAlbumByType(string pModuleType)
        {
            return _currentDAO.GetLEventsAlbumByType(pModuleType);
        }
        #endregion
    }
}