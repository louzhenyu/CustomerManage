/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/28 10:12:43
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_SuperRetailTraderItemMappingBLL
    {
        /// <summary>
        /// ����������ȡ��Ʒ�б�
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemCategoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderItemMappingEntity> GetItemList(string itemName, string itemCategoryId, int pageSize, int pageIndex)
        {
            return _currentDAO.GetItemList(itemName, itemCategoryId, pageSize, pageIndex);
        }
        /// <summary>
        /// ��ȡ��������Ʒ�б�
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemCategoryId"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderItemMappingEntity> GetSuperRetailTraderItemList(string itemName,string itemCategoryId,int status, int pageSize, int pageIndex)
        {
            return _currentDAO.GetSuperRetailTraderItemList(itemName,itemCategoryId, status, pageSize, pageIndex);
        }
        /// <summary>
        /// ��ȡ��������Ʒ�б�
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public T_SuperRetailTraderItemMappingEntity GetSuperRetailTraderItemDetail(string ItemId)
        {
            return _currentDAO.GetSuperRetailTraderItemDetail(ItemId);
        }

        /// <summary>
        /// ������������Ƿ��㹻
        /// </summary>
        /// <param name="inoutDetailList"></param>
        /// <returns></returns>
        public bool IsStockEnough(List<InoutDetailInfo> inoutDetailList) 
        {
            T_SuperRetailTraderSkuMappingBLL SkuBll = new T_SuperRetailTraderSkuMappingBLL((LoggingSessionInfo)CurrentUserInfo);
            bool result = true;
            foreach (var i in inoutDetailList)
            {
                var skuEntity = SkuBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { CustomerID = CurrentUserInfo.ClientID, SkuId = i.sku_id }, null).FirstOrDefault();
                if (skuEntity.SalesQty + i.enter_qty > skuEntity.DistributerStock) //���Sku������С��Sku�ܿ�棬���������� 
                {
                    result = false;
                }
            }
            return result;
        }
        /// <summary>
        /// ���ӿ��
        /// </summary>
        /// <param name="inoutDetailList"></param>
        public void SetSuperRetailTraderItemStock(List<InoutDetailInfo> inoutDetailList) 
        {
            T_SuperRetailTraderSkuMappingBLL SkuBll = new T_SuperRetailTraderSkuMappingBLL((LoggingSessionInfo)CurrentUserInfo);
            foreach (var i in inoutDetailList)
            {
                var itemEntity = this.QueryByEntity(new T_SuperRetailTraderItemMappingEntity() { CustomerID = CurrentUserInfo.ClientID, ItemId = i.item_id },null).FirstOrDefault();
                var skuEntity = SkuBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { CustomerID = CurrentUserInfo.ClientID, SkuId = i.sku_id }, null).FirstOrDefault();

                //��Ʒ������������
                itemEntity.SalesQty += Convert.ToInt32(i.enter_qty);
                itemEntity.LastUpdateTime = DateTime.Now;
                this.Update(itemEntity);
                //Sku������������
                skuEntity.SalesQty += Convert.ToInt32(i.enter_qty);
                skuEntity.LastUpdateTime = DateTime.Now;
                SkuBll.Update(skuEntity);
            }
        }

        /// <summary>
        /// ���ٿ��
        /// </summary>
        /// <param name="inoutDetailList"></param>
        public void DeleteSuperRetailTraderItemStock(List<InoutDetailInfo> inoutDetailList)
        {
            T_SuperRetailTraderSkuMappingBLL SkuBll = new T_SuperRetailTraderSkuMappingBLL((LoggingSessionInfo)CurrentUserInfo);
            foreach (var i in inoutDetailList)
            {
                var itemEntity = this.QueryByEntity(new T_SuperRetailTraderItemMappingEntity() { CustomerID = CurrentUserInfo.ClientID, ItemId = i.item_id }, null).FirstOrDefault();
                var skuEntity = SkuBll.QueryByEntity(new T_SuperRetailTraderSkuMappingEntity() { CustomerID = CurrentUserInfo.ClientID, SkuId = i.sku_id }, null).FirstOrDefault();

                //��Ʒ������������
                itemEntity.SalesQty -= Convert.ToInt32(i.enter_qty);
                itemEntity.LastUpdateTime = DateTime.Now;
                this.Update(itemEntity);
                //Sku������������
                skuEntity.SalesQty -= Convert.ToInt32(i.enter_qty);
                skuEntity.LastUpdateTime = DateTime.Now;
                SkuBll.Update(skuEntity);

            }
      }

        /// <summary>
        /// ƴ�ӹ��ֵ
        /// </summary>
        /// <param name="propName1"></param>
        /// <param name="propName2"></param>
        /// <returns></returns>
        public string GetPropName(string propName1, string propName2)
        {
            string str = "";
            if (!string.IsNullOrEmpty(propName1))
            {
                str = propName1;
            }
            if (!string.IsNullOrEmpty(propName2))
            {
                str = str + "," + propName2;
            }
            return str;
        }
    }
}