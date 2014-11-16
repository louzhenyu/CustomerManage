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
    /// 业务处理：  
    /// </summary>
    public partial class LNewsBLL
    {
        #region getNewsList
        /// <summary>
        /// 消息集合
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

        #region 执行分页查询

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<LNewsEntity> PagedQueryNews(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQueryNews(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 咨询列表查询
        public DataSet GetLNewsList(string cid, string typeId)
        {
            return _currentDAO.GetLNewsList(cid, typeId);
        }
        #endregion

        #region  查询新闻资讯类型
        public DataSet GetLNewsTypeList(string cid)
        {
            return _currentDAO.GetLNewsTypeList(cid);
        }
        #endregion

        #region 获取课程新闻集合
        public DataSet GetNewsByCourseList(string courseTypeId, int page, int pageSize)
        {
            return _currentDAO.GetNewsByCourseList(courseTypeId, page, pageSize);
        }

        public int GetNewsByCourseCount(string courseTypeId)
        {
            return _currentDAO.GetNewsByCourseCount(courseTypeId);
        }
        #endregion

        #region  查询新闻资讯 包括查询图片。
        /// <summary>
        /// 获取新闻列表。
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="newTypeID"></param>
        /// <returns></returns>
        public DataSet GetLNews(string customerID, int pageSize, int pageIndex)
        {
            return _currentDAO.GetNews(customerID, pageSize, pageIndex);
        }

        /// <summary>
        /// 获取新闻详细
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="newID"></param>
        /// <returns></returns>
        public DataSet GetNewDetail(string customerID, string newID)
        {
            return _currentDAO.GetNewDetail(customerID, newID);
        }
        #endregion

        #region 根据新闻类别获取课程新闻集合
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
        /// 获取泸州老窖咨询类的新闻数量
        /// </summary>
        /// <param name="NewsTypeId">新闻类型</param>
        /// <param name="businessType">业务类型</param>
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

        /*||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||【新版资讯管理】Alan |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||*/
        #region Alan 2014-08-14
        /// <summary>
        /// 获取新闻资讯列表 Add By Alan 2014-08-14
        /// </summary>
        /// <param name="lNewsEn">新闻资讯实体类</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="sortOrder">排序方式：0 升序 1 降序</param>
        /// <param name="sortField">排序字段</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageCount">页大小</param>
        /// <param name="rowCount">行大小</param>
        /// <param name="isMappingNews">是否取未关联的资讯</param>
        /// <returns>DataTable 数据集</returns>
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

        #region 新版新闻资讯模块
        /// <summary>
        /// 删除咨询信息
        /// </summary>
        /// <param name="Ids">主键标识</param>
        public void DelNewsInfo(string Ids)
        {
            //支持批量删除
            _currentDAO.Delete(Ids.Split(','));
        }

        /// <summary>
        /// 新增新闻资讯
        /// </summary>
        /// <param name="lNewsEn">新闻资讯实体</param>
        /// <param name="microNumberID">刊号ID</param>
        /// <param name="microTypeID">类别ID</param>
        /// <param name="labelID">标签ID</param>
        public void AddNewsInfo(LNewsEntity lNewsEn, string microNumberID, string microTypeID, string labelID)
        {
            _currentDAO.AddNewsInfo(lNewsEn, microNumberID, microTypeID, labelID.Split(','));
        }

        /// <summary>
        /// 更新新闻资讯
        /// </summary>
        /// <param name="lNewsEn">新闻资讯实体</param>
        /// <param name="mappingId">新闻期刊关联标识ID</param>
        /// <param name="microNumberID">刊号ID</param>
        /// <param name="microTypeID">类别ID</param>
        /// <param name="labelIds">标签ID</param>
        /// <param name="isRelMicro">资讯是否关联微刊</param>
        public void UpdateNewsInfo(LNewsEntity lNewsEn, string mappingId, string oldNumberId, string oldTypeId, string microNumberID, string microTypeID, string labelIds, bool isRelMicro)
        {
            _currentDAO.UpdateNewsInfo(lNewsEn, mappingId, oldNumberId, oldTypeId, microNumberID, microTypeID, labelIds.Split(','), isRelMicro);
        }

        /// <summary>
        /// 获取新闻资讯详细信息
        /// </summary>
        /// <param name="newsId">新闻资讯ID</param>
        /// <param name="IsRelMicro">是否关联微刊</param>
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

        #region 微刊（新闻资讯表）信息收集
        /// <summary>
        /// 获取微刊（新闻资讯表）详细信息 Add By Alan 2014-09-01
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns></returns>
        public DataTable GetMicroNewsDetail(string newsId)
        {
            return _currentDAO.GetMicroNewsDetail(newsId).Tables[0] ?? null;
        }

        /// <summary>
        /// 获取微刊（新闻资讯表）分页信息 Add By Alan 2014-09-01
        /// </summary>
        /// <param name="numberId">期数Id</param>
        /// <param name="typeId">类别Id</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
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
        /// 获取微刊（新闻资讯表）姊妹信息标识 Add By Alan 2014-09-01
        /// </summary>
        /// <param name="numberId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public DataTable GetMicroNewsSiblingsId(string numberId, string typeId)
        {
            return _currentDAO.GetMicroNewsSiblingsId(numberId, typeId).Tables[0] ?? null;
        }
        //标记微刊（新闻资讯表）阅读、浏览、转发数
        public int SetMicroNewsColl(string newsId, string fildName)
        {
            return _currentDAO.SetMicroNewsColl(newsId, fildName);
        }
        /// <summary>
        /// 取微刊阅读、分享、浏览量
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <param name="field">字段：BrowseCount, PraiseCount, CollCount</param>
        /// <returns></returns>
        public int GetMicroNewsStats(string newsId, string field)
        {
            return _currentDAO.GetMicroNewsStats(newsId, field);
        }
        #endregion
    }
}