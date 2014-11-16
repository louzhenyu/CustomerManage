/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/10 10:06:08
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
using JIT.CPOS.BS.Entity.User;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class MVipShowBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        public IList<MVipShowEntity> GetList(MVipShowEntity entity, int Page, int PageSize)
        {
            var lNewsBLL = new LNewsBLL(CurrentUserInfo);
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            var itemService = new ItemService(CurrentUserInfo);
            if (PageSize <= 0) PageSize = 15;

            IList<MVipShowEntity> eventsList = new List<MVipShowEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<MVipShowEntity>(ds.Tables[0]);

                if (eventsList != null)
                {
                    foreach (var item in eventsList)
                    {
                        item.ImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity()
                        {
                            ObjectId = item.VipShowId
                        }, new OrderBy[] { new  OrderBy { FieldName = "CreateTime", Direction = OrderByDirections.Desc }  });
                    }
                }
            }
            return eventsList;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(MVipShowEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        public IList<UserInfo> GetHairStylistByStoreId(string customerId, string unitId, int Page, int PageSize)
        {
            var lNewsBLL = new LNewsBLL(CurrentUserInfo);
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            var itemService = new ItemService(CurrentUserInfo);
            if (PageSize <= 0) PageSize = 15;

            IList<UserInfo> eventsList = new List<UserInfo>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetHairStylistByStoreId(customerId, unitId, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]);

                //if (eventsList != null)
                //{
                //    foreach (var item in eventsList)
                //    {
                //        item.ImageList = objectImagesBLL.QueryByEntity(new ObjectImagesEntity()
                //        {
                //            ObjectId = item.VipShowId
                //        }, null);
                //    }
                //}
            }
            return eventsList;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetHairStylistByStoreIdCount(string customerId, string unitId)
        {
            return _currentDAO.GetHairStylistByStoreIdCount(customerId, unitId);
        }
        #endregion

        #region 停用启用
        /// <summary>
        /// 停用启用
        /// </summary>
        public void SetStatus(LoggingSessionInfo loggingSessionInfo, string id, string Status)
        {
            MVipShowEntity obj = new MVipShowEntity();
            obj.IsDelete = Convert.ToInt32(Status);
            obj.VipShowId = id;
            obj.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
            obj.LastUpdateTime = DateTime.Now;
            _currentDAO.SetStatus(obj);
        }
        #endregion

        #region 获取抽奖机会
        /// <summary>
        /// 获取抽奖机会
        /// </summary>
        /// <param name="customerId">客户标识</param>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public IList<MVipShowEntity> GetLotteryCount(string customerId, string vipId)
        {
            IList<MVipShowEntity> list = new List<MVipShowEntity>();
            DataSet ds = _currentDAO.GetLotteryCount(customerId, vipId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<MVipShowEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

    }
}