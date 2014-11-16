/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 9:59:17
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
    public partial class ZCourseBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        public IList<ZCourseEntity> GetCourses(ZCourseEntity entity, int Page, int PageSize)
        {
            var zCourseReflectionsBLL = new ZCourseReflectionsBLL(CurrentUserInfo);
            var zCourseNewsMappingBLL = new ZCourseNewsMappingBLL(CurrentUserInfo);
            var lNewsBLL = new LNewsBLL(CurrentUserInfo);
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            if (PageSize <= 0) PageSize = 15;

            IList<ZCourseEntity> eventsList = new List<ZCourseEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetCourses(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<ZCourseEntity>(ds.Tables[0]);

                if (eventsList != null)
                {
                    foreach (var item in eventsList)
                    {
                        item.CourseReflectionsList = zCourseReflectionsBLL.GetList(new ZCourseReflectionsEntity()
                        {
                            CourseId = item.CourseId
                        }, 0, 10000);
                        var mapList = zCourseNewsMappingBLL.QueryByEntity(new ZCourseNewsMappingEntity()
                        {
                            CourseId = item.CourseId
                        }, null);
                        if (mapList != null)
                        {
                            item.NewsList = new List<LNewsEntity>();
                            foreach (var mapItem in mapList)
                            {
                                var newsObj = lNewsBLL.GetByID(mapItem.NewsId);
                                if (newsObj == null) continue;
                                item.NewsList.Add(new LNewsEntity() {
                                    NewsId = newsObj.NewsId,
                                    NewsTitle = newsObj.NewsTitle,
                                    PublishTime = newsObj.PublishTime,
                                    displayIndex = newsObj.displayIndex,
                                });
                            }
                        }
                        item.ImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity()
                        {
                            ObjectId = item.CourseId
                        }, null).ToList();
                        if (item.ImageList.Count > 0)
                        {
                            item.ImageUrl = item.ImageList[0].ImageURL;
                        }
                    }
                }
            }
            return eventsList;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetCoursesCount(ZCourseEntity entity)
        {
            return _currentDAO.GetCoursesCount(entity);
        }
        #endregion
    }
}