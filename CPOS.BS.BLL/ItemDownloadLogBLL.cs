/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/30 9:30:35
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
    public partial class ItemDownloadLogBLL
    {
        #region 商品下载记录保存
        public bool SetItemDownloadLog(ItemDownloadLogEntity info,out string strError)
        {
            try
            {
                var itemDownloadList = _currentDAO.QueryByEntity(new ItemDownloadLogEntity()
                {
                    ItemId = info.ItemId
                    ,UserId = info.UserId
                }, null);
                if (itemDownloadList == null || itemDownloadList.Length == 0)
                {
                    info.LogId = BaseService.NewGuidPub();
                    info.Count = 1;
                    _currentDAO.Create(info);
                }
                else
                {
                    var itemDownloadInfo = itemDownloadList[0];
                    itemDownloadInfo.Count = itemDownloadInfo.Count + 1;
                    itemDownloadInfo.LastUpdateTime = info.LastUpdateTime;
                    _currentDAO.Update(itemDownloadInfo);
                }
                strError = "成功";
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                return false;
            }
        }
        #endregion

        #region 获取已下载用户集合
        public ItemDownloadLogEntity GetDownloadUsersByItem(string ItemId, int Page, int PageSize,out string strErroe)
        {
            ItemDownloadLogEntity info = new ItemDownloadLogEntity();
            try
            {
                info.TotalCount = _currentDAO.GetDownloadUsersByItemCount(ItemId);
                IList<ItemDownloadLogEntity> list = new List<ItemDownloadLogEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetDownloadUsersByItem(ItemId, Page, PageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<ItemDownloadLogEntity>(ds.Tables[0]);
                }
                strErroe = "ok";
                info.ItemDownloadLogList = list;
            }
            catch (Exception ex) {
                strErroe = ex.ToString();
            }
            return info;
        }
        #endregion
    }
}