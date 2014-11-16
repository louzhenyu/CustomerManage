/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/23 17:47:45
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
    public partial class ShoppingCartBLL
    {
        #region �б��ȡ
        /// <summary>
        /// �б��ȡ
        /// </summary>
        public IList<ShoppingCartEntity> GetList(ShoppingCartEntity entity, int Page, int PageSize)
        {
            var lNewsBLL = new LNewsBLL(CurrentUserInfo);
            var objectImagesBLL = new ObjectImagesBLL(CurrentUserInfo);
            var itemService = new ItemService(CurrentUserInfo);
            if (PageSize <= 0) PageSize = 15;

            IList<ShoppingCartEntity> eventsList = new List<ShoppingCartEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                eventsList = DataTableToObject.ConvertToList<ShoppingCartEntity>(ds.Tables[0]);

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
        /// �б�������ȡ
        /// </summary>
        public int GetListCount(ShoppingCartEntity entity)
        {
            return _currentDAO.GetListCount(entity);
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public decimal GetListAmount(ShoppingCartEntity entity)
        {
            return _currentDAO.GetListAmount(entity);
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetListQty(ShoppingCartEntity entity)
        {
            return _currentDAO.GetListQty(entity);
        }
        #endregion

        #region ���ݶ�����������ﳵ
        public bool SetCancelShoppingCartByOrderId(string OrderId, string VipId, string[] pSkuIDs)
        {
            bool bReturn = _currentDAO.SetCancelShoppingCartByOrderId(OrderId, VipId, pSkuIDs);
            return bReturn;
        }
        #endregion

        #region ���ﳵ����
        /// <summary>
        /// ��ȡ��Ա�Ĺ��ﳵ����
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public int GetShoppingCartByVipId(string vipId)
        {
            return _currentDAO.GetShoppingCartByVipId(vipId);
        }
        #endregion

        #region ɾ�����ﳵ
        public void DeleteShoppingCart(string pVipId, string[] pIDs)
        {
            this._currentDAO.DeleteShoppingCart(pVipId, pIDs);
        }

        public void DeleteShoppingCart(string pOrderId)
        {
            this._currentDAO.DeleteShoppingCart(pOrderId);
        }
        #endregion

        public DataSet GetShoppingGgBySkuId(string skuIdList)
        {
            return this._currentDAO.GetShoppingGgBySkuId(skuIdList);
        }
    }
}