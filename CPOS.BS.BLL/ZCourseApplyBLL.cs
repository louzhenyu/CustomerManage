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
    public partial class ZCourseApplyBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        public IList<ZCourseApplyEntity> GetList(ZCourseApplyEntity entity, int Page, int PageSize)
        {
            //var lNewsBLL = new LNewsBLL(CurrentUserInfo);
            //var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            //var itemService = new ItemService(CurrentUserInfo);
            if (PageSize <= 0) PageSize = 15;

            IList<ZCourseApplyEntity> eventsList = new List<ZCourseApplyEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<ZCourseApplyEntity>(ds.Tables[0]);

                //if (eventsList != null)
                //{
                //    foreach (var item in eventsList)
                //    {
                //        item.ItemDetail = itemService.GetVwItemDetailById(item.ItemId, entity.VipId);
                //    }
                //}
            }
            return eventsList;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(ZCourseApplyEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion
    }
}