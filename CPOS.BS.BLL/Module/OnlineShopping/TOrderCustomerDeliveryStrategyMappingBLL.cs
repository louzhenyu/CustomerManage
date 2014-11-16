/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/26 13:27:09
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class TOrderCustomerDeliveryStrategyMappingBLL : BaseService
    {
        private BasicUserInfo CurrentUserInfo;
        private TOrderCustomerDeliveryStrategyMappingDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public TOrderCustomerDeliveryStrategyMappingBLL(LoggingSessionInfo loggingSessionInfo)
        {
            this._currentDAO = new TOrderCustomerDeliveryStrategyMappingDAO(loggingSessionInfo);
            this.CurrentUserInfo = loggingSessionInfo;//
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion
        #region ICRUDable 成员
        /// <summary>
        /// 创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Create(TOrderCustomerDeliveryStrategyMappingEntity pEntity)
        {
            _currentDAO.Create(pEntity);
        }


        /// <summary>
        /// 在事务内创建一个新实例
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Create(TOrderCustomerDeliveryStrategyMappingEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Create(pEntity, pTran);
        }

        /// <summary>
        /// 根据标识符获取实例
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public TOrderCustomerDeliveryStrategyMappingEntity GetByID(object pID)
        {
            return _currentDAO.GetByID(pID);
        }

        /// <summary>
        /// 获取所有实例
        /// </summary>
        /// <returns></returns>
        public TOrderCustomerDeliveryStrategyMappingEntity[] GetAll()
        {
            return _currentDAO.GetAll();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Update(TOrderCustomerDeliveryStrategyMappingEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Update(pEntity, pTran);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        public void Update(TOrderCustomerDeliveryStrategyMappingEntity pEntity)
        {
            _currentDAO.Update(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TOrderCustomerDeliveryStrategyMappingEntity pEntity)
        {
            _currentDAO.Delete(pEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pEntity">实体实例</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TOrderCustomerDeliveryStrategyMappingEntity pEntity, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntity, pTran);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID">标识符的值</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            _currentDAO.Delete(pID, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(TOrderCustomerDeliveryStrategyMappingEntity[] pEntities, IDbTransaction pTran)
        {
            _currentDAO.Delete(pEntities, pTran);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pEntities">实体实例数组</param>
        public void Delete(TOrderCustomerDeliveryStrategyMappingEntity[] pEntities)
        {
            _currentDAO.Delete(pEntities);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        public void Delete(object[] pIDs)
        {
            _currentDAO.Delete(pIDs);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="pIDs">标识符值数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void Delete(object[] pIDs, IDbTransaction pTran)
        {
            _currentDAO.Delete(pIDs, pTran);
        }
        #endregion

        #region IQueryable 成员
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <returns></returns>
        public TOrderCustomerDeliveryStrategyMappingEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            return _currentDAO.Query(pWhereConditions, pOrderBys);
        }

        /// <summary>
        /// 执行分页查询
        /// </summary>
        /// <param name="pWhereConditions">筛选条件</param>
        /// <param name="pOrderBys">排序</param>
        /// <param name="pPageSize">每页的记录数</param>
        /// <param name="pCurrentPageIndex">以0开始的当前页码</param>
        /// <returns></returns>
        public PagedQueryResult<TOrderCustomerDeliveryStrategyMappingEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQuery(pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        /// <summary>
        /// 根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public TOrderCustomerDeliveryStrategyMappingEntity[] QueryByEntity(TOrderCustomerDeliveryStrategyMappingEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntity(pQueryEntity, pOrderBys);
        }

        /// <summary>
        /// 分页根据实体条件查询实体
        /// </summary>
        /// <param name="pQueryEntity">以实体形式传入的参数</param>
        /// <param name="pOrderBys">排序组合</param>
        /// <returns>符合条件的实体集</returns>
        public PagedQueryResult<TOrderCustomerDeliveryStrategyMappingEntity> PagedQueryByEntity(TOrderCustomerDeliveryStrategyMappingEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQueryByEntity(pQueryEntity, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region 扩展方法
        #region 如果是送货到家，根据订单和用户ID来给总金额和实际支付金额加上运费
        /// <summary>
        /// 如果是送货到家，根据订单和用户ID来给总金额和实际支付金额加上运费
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
   
        public void UpdateOrderAddDeliveryAmount(string OrderId, string CustomerId)
        {
            //取订单信息
            OnlineShoppingItemBLL serer = new OnlineShoppingItemBLL(base.loggingSessionInfo);
            JIT.CPOS.BS.Entity.Interface.SetOrderEntity orderInfo = serer.GetOrderOnline(OrderId);
            //如果订单状态不为100，return；
            if (orderInfo.Status != "100")//状态为100时，才是正式的订单，才进行下面的操作
            {
                return;
            }
            OnlineShoppingItemService tInoutDAO = new OnlineShoppingItemService(base.loggingSessionInfo);

            #region 如果订单已产生配送费
            TOrderCustomerDeliveryStrategyMappingEntity[] mappingList= Query(new IWhereCondition[] { 
                new EqualsCondition() { FieldName = "OrderId", Value =OrderId}
            }, null);
            if (mappingList.Count() > 0)
            {
                 mappingList[0].IsDelete = 1;
                 Delete(mappingList[0]);//逻辑删除订单的配送费
                 //修改订单金额
                 orderInfo.TotalAmount=orderInfo.TotalAmount - mappingList[0].DeliveryAmount.Value;
                 orderInfo.ActualAmount = orderInfo.ActualAmount - mappingList[0].DeliveryAmount.Value;
                 tInoutDAO.UpdateOrderAddDeliveryAmount(orderInfo);
            }
            #endregion

            //根据totalAmount和customerID取对应的运费
            CustomerDeliveryStrategyBLL customerDeliveryStrategyBLL = new CustomerDeliveryStrategyBLL(base.loggingSessionInfo);
            JIT.CPOS.BS.Entity.CustomerDeliveryStrategyEntity customerDeliveryStrategyEntity = customerDeliveryStrategyBLL.GetDeliveryAmount(CustomerId, orderInfo.TotalAmount, orderInfo.DeliveryId);

            if (customerDeliveryStrategyEntity.Id != null)
            {
                //更新订单主表totalAmount和actual_amount
                decimal DeliveryAmount = customerDeliveryStrategyEntity.DeliveryAmount == null ? 0 : Convert.ToDecimal(customerDeliveryStrategyEntity.DeliveryAmount);
                orderInfo.TotalAmount = orderInfo.TotalAmount + DeliveryAmount;
                orderInfo.ActualAmount = orderInfo.ActualAmount + DeliveryAmount;
                //  serer.UpdateWEventByPhone
                tInoutDAO.UpdateOrderAddDeliveryAmount(orderInfo);


                //插入“订单与配送策略关系表”
                JIT.CPOS.BS.Entity.TOrderCustomerDeliveryStrategyMappingEntity tOrderCustomerDelivery = new TOrderCustomerDeliveryStrategyMappingEntity();
                // tOrderCustomerDelivery.MappingId
                tOrderCustomerDelivery.OrderId = OrderId;
                tOrderCustomerDelivery.DSId = customerDeliveryStrategyEntity.Id;
                tOrderCustomerDelivery.DeliveryAmount = customerDeliveryStrategyEntity.DeliveryAmount;
                tOrderCustomerDelivery.CreateBy = loggingSessionInfo.CurrentUser.User_Name;
                tOrderCustomerDelivery.IsDelete = 0;
                //TOrderCustomerDeliveryStrategyMappingBLL tOrderCustomerDeliveryBLL = new TOrderCustomerDeliveryStrategyMappingBLL(loggingSessionInfo);
                _currentDAO.Create(tOrderCustomerDelivery);
            }
        }
        #endregion
        #endregion
    }
}