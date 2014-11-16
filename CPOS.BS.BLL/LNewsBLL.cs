/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 15:46:07
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
    public partial class LNewsBLL
    {
        #region getNewsList
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public LNewsEntity getNewsList(LNewsEntity entity, int page, int pageSize)
        {
            try
            {
                var result = new LNewsEntity();

                IList<LNewsEntity> list = new List<LNewsEntity>();
                DataSet ds = _currentDAO.getNewsList(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<LNewsEntity>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getNewsListCount(entity);
                result.EntityList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public LNewsEntity[] GetIndexNewsList(string customerid)
        {
            return this._currentDAO.GetIndexNewsList(customerid);
        }
        #endregion

        #region ִ�з�ҳ��ѯ

        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<LNewsEntity> PagedQueryNews(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQueryNews(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ��ѯ�б��ѯ
        public DataSet GetLNewsList(string cid, string typeId)
        {
            return _currentDAO.GetLNewsList(cid, typeId);
        }
        #endregion

        #region  ��ѯ������Ѷ����
        public DataSet GetLNewsTypeList(string cid)
        {
            return _currentDAO.GetLNewsTypeList(cid);
        }
        #endregion

        #region ��ȡ�γ����ż���
        public DataSet GetNewsByCourseList(string courseTypeId, int page, int pageSize)
        {
            return _currentDAO.GetNewsByCourseList(courseTypeId, page, pageSize);
        }

        public int GetNewsByCourseCount(string courseTypeId)
        {
            return _currentDAO.GetNewsByCourseCount(courseTypeId);
        }
        #endregion

        #region  ��ѯ������Ѷ ������ѯͼƬ��
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="newTypeID"></param>
        /// <returns></returns>
        public DataSet GetLNews(string customerID, int pageSize, int pageIndex)
        {
            return _currentDAO.GetNews(customerID, pageSize, pageIndex);
        }

        /// <summary>
        /// ��ȡ������ϸ
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public DataSet GetNewDetail(string customerID, string newID)
        {
            return _currentDAO.GetNewDetail(customerID, newID);
        }
        #endregion

        #region ������������ȡ�γ����ż���
        public DataSet GetNewsListByType(string newsTypeId, int page, int pageSize)
        {
            return _currentDAO.GetNewsListByType(newsTypeId, page, pageSize);
        }

        public int GetNewsListByTypeCount(string newsTypeId)
        {
            return _currentDAO.GetNewsListByTypeCount(newsTypeId);
        }
        #endregion

        #region
        /// <summary>
        /// ��ȡ�����Ͻ���ѯ�����������
        /// </summary>
        /// <param name="NewsTypeId">��������</param>
        /// <param name="businessType">ҵ������</param>
        /// <returns></returns>
        public int getNewsListWeeklyCount(string NewsTypeId, string BusinessType)
        {

            return _currentDAO.getNewsListWeeklyCount(NewsTypeId, BusinessType);
        }

        public IList<LNewsEntity> getNewsListWeekly(string NewsTypeId, string BusinessType, int Page, int PageSize)
        {
            DataSet ds = new DataSet();
            ds = _currentDAO.getNewsListWeekly(NewsTypeId, BusinessType, Page, PageSize);
            IList<LNewsEntity> newsList = new List<LNewsEntity>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                newsList = DataTableToObject.ConvertToList<LNewsEntity>(ds.Tables[0]);
            }
            return newsList;
        }
        #endregion

        public DataSet GetNewsList(string customerId, string newsTypeId, string newsName, int pageIndex, int pageSize)
        {
            return this._currentDAO.GetNewsList(customerId, newsTypeId, newsName, pageIndex, pageSize);
        }

        public int GetNewsListCount(string customerId, string newsTypeId, string newsName)
        {
            return this._currentDAO.GetNewsListCount(customerId, newsTypeId, newsName);
        }

        /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||���°���Ѷ����Alan |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
        #region Alan 2014-08-14
        /// <summary>
        /// ��ȡ������Ѷ�б� Add By Alan 2014-08-14
        /// </summary>
        /// <param name="lNewsEn">������Ѷʵ����</param>
        /// <param name="startDate">��ʼ����</param>
        /// <param name="endDate">��������</param>
        /// <param name="keyword">�����ؼ���</param>
        /// <param name="sortOrder">����ʽ��0 ���� 1 ����</param>
        /// <param name="sortField">�����ֶ�</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <param name="pageCount">ҳ��С</param>
        /// <param name="rowCount">�д�С</param>
        /// <param name="isMappingNews">�Ƿ�ȡδ��������Ѷ</param>
        /// <returns>DataTable ���ݼ�</returns>
        public DataTable GetNewsList(LNewsEntity lNewsEn, string startDate, string endDate, string keyword, string sortField, int sortOrder, int pageIndex, int pageSize, bool isMappingNews, ref int pageCount, ref int rowCount)
        {
            DataSet ds = _currentDAO.GetNewsList(lNewsEn, startDate, endDate, keyword, sortField, sortOrder, pageIndex, pageSize, isMappingNews);
            if (ds == null)
            {
                return null;
            }
            rowCount = 0;
            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
            pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return ds.Tables[0];
        }
        #endregion

        #region �°�������Ѷģ��
        /// <summary>
        /// ɾ����ѯ��Ϣ
        /// </summary>
        /// <param name="Ids">������ʶ</param>
        public void DelNewsInfo(string Ids)
        {
            //֧������ɾ��
            _currentDAO.Delete(Ids.Split(','));
        }

        /// <summary>
        /// ����������Ѷ
        /// </summary>
        /// <param name="lNewsEn">������Ѷʵ��</param>
        /// <param name="microNumberID">����ID</param>
        /// <param name="microTypeID">���ID</param>
        /// <param name="labelID">��ǩID</param>
        public void AddNewsInfo(LNewsEntity lNewsEn, string microNumberID, string microTypeID, string labelID)
        {
            _currentDAO.AddNewsInfo(lNewsEn, microNumberID, microTypeID, labelID.Split(','));
        }

        /// <summary>
        /// ����������Ѷ
        /// </summary>
        /// <param name="lNewsEn">������Ѷʵ��</param>
        /// <param name="mappingId">�����ڿ�������ʶID</param>
        /// <param name="microNumberID">����ID</param>
        /// <param name="microTypeID">���ID</param>
        /// <param name="labelIds">��ǩID</param>
        /// <param name="isRelMicro">��Ѷ�Ƿ����΢��</param>
        public void UpdateNewsInfo(LNewsEntity lNewsEn, string mappingId, string oldNumberId, string oldTypeId, string microNumberID, string microTypeID, string labelIds, bool isRelMicro)
        {
            _currentDAO.UpdateNewsInfo(lNewsEn, mappingId, oldNumberId, oldTypeId, microNumberID, microTypeID, labelIds.Split(','), isRelMicro);
        }

        /// <summary>
        /// ��ȡ������Ѷ��ϸ��Ϣ
        /// </summary>
        /// <param name="newsId">������ѶID</param>
        /// <param name="IsRelMicro">�Ƿ����΢��</param>
        /// <returns></returns>
        public DataTable GetNewsDetail(string newsId, ref bool IsRelMicro)
        {
            DataSet ds = _currentDAO.GetNewsDetail(newsId);
            if (ds == null && ds.Tables.Count > 0)
            {
                return null;
            }
            IsRelMicro = true;
            if (ds.Tables[0].Rows[0]["MappingId"] == DBNull.Value)
            {
                IsRelMicro = false;
            }

            return ds.Tables[0];
        }
        #endregion

        #region ΢����������Ѷ����Ϣ�ռ�
        /// <summary>
        /// ��ȡ΢����������Ѷ����ϸ��Ϣ Add By Alan 2014-09-01
        /// </summary>
        /// <param name="newsId">����Id</param>
        /// <returns></returns>
        public DataTable GetMicroNewsDetail(string newsId)
        {
            return _currentDAO.GetMicroNewsDetail(newsId).Tables[0] ?? null;
        }

        /// <summary>
        /// ��ȡ΢����������Ѷ����ҳ��Ϣ Add By Alan 2014-09-01
        /// </summary>
        /// <param name="numberId">����Id</param>
        /// <param name="typeId">���Id</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <returns></returns>
        public DataTable GetMicroNewsPageList(string numberId, string typeId, int pageIndex, int pageSize, int sortOrder, string sortField, ref int pageCount, ref int rowCount)
        {
            DataSet ds = _currentDAO.GetMicroNewsPageList(numberId, typeId, pageIndex, pageSize, sortOrder, sortField);
            if (ds == null || ds.Tables.Count <= 0)
            {
                return null;
            }

            int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out rowCount);
            pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
            {
                pageCount++;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// ��ȡ΢����������Ѷ�������Ϣ��ʶ Add By Alan 2014-09-01
        /// </summary>
        /// <param name="numberId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public DataTable GetMicroNewsSiblingsId(string numberId, string typeId)
        {
            return _currentDAO.GetMicroNewsSiblingsId(numberId, typeId).Tables[0] ?? null;
        }
        //���΢����������Ѷ���Ķ��������ת����
        public int SetMicroNewsColl(string newsId, string fildName)
        {
            return _currentDAO.SetMicroNewsColl(newsId, fildName);
        }
        /// <summary>
        /// ȡ΢���Ķ������������
        /// </summary>
        /// <param name="newsId">����Id</param>
        /// <param name="field">�ֶΣ�BrowseCount, PraiseCount, CollCount</param>
        /// <returns></returns>
        public int GetMicroNewsStats(string newsId, string field)
        {
            return _currentDAO.GetMicroNewsStats(newsId, field);
        }
        #endregion
    }
}