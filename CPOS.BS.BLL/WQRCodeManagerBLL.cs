/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/21 19:20:01
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
    public partial class WQRCodeManagerBLL
    {
        #region GetList
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="Page">分页页码。从0开始</param>
        /// <param name="PageSize">每页的数量。未指定时默认为15</param>
        /// <returns></returns>
        public IList<WQRCodeManagerEntity> GetList(WQRCodeManagerEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WQRCodeManagerEntity> list = new List<WQRCodeManagerEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WQRCodeManagerEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        /// <param name="entity">entity</param>
        public int GetListCount(WQRCodeManagerEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        #endregion

        /// <summary>
        /// 获取一个可以使用的二维码
        /// </summary>
        /// <param name="WeiXinId"></param>
        /// <param name="QRCodeTypeId"></param>
        /// <returns></returns>
        public WQRCodeManagerEntity GetOne(string appId, string QRCodeTypeId)
        {
            WQRCodeManagerBLL wQRCodeManagerBLL = new WQRCodeManagerBLL(CurrentUserInfo);
            var list = wQRCodeManagerBLL.GetList(new WQRCodeManagerEntity()
            {
                ApplicationId = appId,
                QRCodeTypeId = Guid.Parse(QRCodeTypeId),
                CustomerId = CurrentUserInfo.CurrentUser.customer_id,
                IsUse = 0,
                OrderBy = "a.QRCode asc"
            }, 0, 1);

            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        public bool CheckByObjectId(string objectId)
        {
            WQRCodeManagerBLL wQRCodeManagerBLL = new WQRCodeManagerBLL(CurrentUserInfo);
            var list = wQRCodeManagerBLL.GetList(new WQRCodeManagerEntity()
            {
                ObjectId = objectId,
                CustomerId = CurrentUserInfo.CurrentUser.customer_id
            }, 0, 1);

            if (list != null && list.Count > 0)
            {
                return true;
            }
            return false;
        }

        public int GetMaxWQRCod()
        {

            return this._currentDAO.GetMaxWQRCod();
        }



    }
}