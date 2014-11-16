/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/17 9:59:18
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
    public partial class ZCourseReflectionsBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        public IList<ZCourseReflectionsEntity> GetList(ZCourseReflectionsEntity entity, int Page, int PageSize)
        {
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            if (PageSize <= 0) PageSize = 15;

            IList<ZCourseReflectionsEntity> eventsList = new List<ZCourseReflectionsEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<ZCourseReflectionsEntity>(ds.Tables[0]);

                foreach (var item in eventsList)
                {
                    item.ImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity()
                    {
                        ObjectId = item.ReflectionsId
                    }, null).ToList();
                    if (item.ImageList.Count > 0)
                    {
                        item.ImageURL = item.ImageList[0].ImageURL;
                    }
                }
            }
            return eventsList;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(ZCourseReflectionsEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion
    }
}