/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/10/21 17:38:06
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
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class CustomerTakeDeliveryBLL : BaseService
    {
        public void SaveCustomerTakeDelivery(CustomerTakeDeliveryEntity takeDeliveryEntity, LoggingSessionInfo loggingSessionInfo,string deliveryId)
        {
            TransactionHelper tranHelper = new TransactionHelper(loggingSessionInfo);
            IDbTransaction tran = tranHelper.CreateTransaction();


            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = loggingSessionInfo.ClientID });
            var takeDeliveryList = Query(lstWhereCondition.ToArray(), null);

            //除本次保存数据的此商户下提货配置信息
            var takeDeliveryList_d = takeDeliveryList.Where(m => m.Id != takeDeliveryEntity.Id).ToArray();
            //删除除本次保存数据的此商户下提货配置信息
            if (takeDeliveryList_d != null && takeDeliveryList_d.Count() > 0)
            {
                Delete(takeDeliveryList_d, tran);
            }


            if (takeDeliveryEntity.Id == null || string.IsNullOrWhiteSpace(takeDeliveryEntity.Id.ToString()))
            {
                takeDeliveryEntity.Id = Guid.NewGuid();
                Create(takeDeliveryEntity, tran);
            }
            else
            {
                Update(takeDeliveryEntity, tran);
            }

            //修改配送方式状态
            DeliveryBLL deliverBll = new DeliveryBLL(loggingSessionInfo);
            deliverBll.Update(
                new DeliveryEntity() { 
                    DeliveryId = deliveryId,
                    Status = takeDeliveryEntity.Status
                }
                ,tran
                );

            tran.Commit();
        }
    }
}