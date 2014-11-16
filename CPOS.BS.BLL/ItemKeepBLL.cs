/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/28 16:50:21
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
    public partial class ItemKeepBLL
    {
        #region 列表获取
        /// <summary>
        /// 列表获取
        /// </summary>
        public IList<ItemKeepEntity> GetList(ItemKeepEntity entity, int Page, int PageSize)
        {
            var lNewsBLL = new LNewsBLL(CurrentUserInfo);
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            var itemService = new ItemService(CurrentUserInfo);
            if (PageSize <= 0) PageSize = 15;

            IList<ItemKeepEntity> eventsList = new List<ItemKeepEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<ItemKeepEntity>(ds.Tables[0]);

                if (eventsList != null)
                {
                    foreach (var item in eventsList)
                    {
                        item.ItemDetail = itemService.GetVwItemDetailById(item.ItemId, entity.VipId);
                    }
                }
            }
            return eventsList;
        }
        /// <summary>
        /// 列表数量获取
        /// </summary>
        public int GetListCount(ItemKeepEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        #region
        /// <summary>
        /// 设置收藏
        /// </summary>
        /// <param name="ItemId"></param>
        /// <param name="ItemKeepStatus"></param>
        /// <param name="UserId"></param>
        /// <param name="strError"></param>
        /// <returns></returns>
        public bool SetItemKeep(string ItemId, string ItemKeepStatus,string UserId,out string strError)
        {
            try
            {
                ItemKeepEntity itemKeepInfo = new ItemKeepEntity();
                //1.判断是否存在
                var itemKeepInfoList = _currentDAO.QueryByEntity(new ItemKeepEntity()
                {
                    ItemId = ItemId
                    ,VipId = UserId
                }, null);
                if (itemKeepInfoList == null || itemKeepInfoList.Length == 0)
                {
                    itemKeepInfo.ItemKeepId = BaseService.NewGuidPub();
                    itemKeepInfo.ItemId = ItemId;
                    itemKeepInfo.KeepStatus = Convert.ToInt32(ItemKeepStatus);
                    itemKeepInfo.CreateBy = UserId;
                    itemKeepInfo.LastUpdateBy = UserId;
                    itemKeepInfo.CreateTime = System.DateTime.Now;
                    itemKeepInfo.LastUpdateTime = System.DateTime.Now;
                    itemKeepInfo.VipId = UserId;
                    _currentDAO.Create(itemKeepInfo);
                }
                else
                {
                    itemKeepInfo = itemKeepInfoList[0];
                    itemKeepInfo.KeepStatus = Convert.ToInt32(ItemKeepStatus);
                    if (itemKeepInfo.VipId == null || itemKeepInfo.VipId.Equals(""))
                    {
                        itemKeepInfo.VipId = UserId;
                    }
                    _currentDAO.Update(itemKeepInfo);
                }
                strError = "收藏成功";
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion
    }
}