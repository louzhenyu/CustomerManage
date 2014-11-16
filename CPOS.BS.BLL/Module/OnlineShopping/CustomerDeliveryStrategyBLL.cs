/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/25 17:27:27
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
    public partial class CustomerDeliveryStrategyBLL : BaseService
    {
        #region 根据custoerID和总金额取得运费
        /// <summary>
        /// 根据custoerID和总金额取得运费
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public JIT.CPOS.BS.Entity.CustomerDeliveryStrategyEntity GetDeliveryAmount(string CustomerId, decimal total_amount, string DeliveryId)
        {
            JIT.CPOS.BS.Entity.CustomerDeliveryStrategyEntity orderInfo = new JIT.CPOS.BS.Entity.CustomerDeliveryStrategyEntity();
            DataSet ds = new DataSet();
            ds = this._currentDAO.GetDeliveryAmount(CustomerId, total_amount, DeliveryId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                orderInfo = DataTableToObject.ConvertToObject<JIT.CPOS.BS.Entity.CustomerDeliveryStrategyEntity>(ds.Tables[0].Rows[0]);
            }
            return orderInfo;
        }
        #endregion

        /// <summary>
        ///保存送货到家配置信息
        /// </summary>
        /// <param name="DeliveryStrategyEntity"></param>
        /// <param name="BasicSettingEntity"></param>
        /// add by donal 2014-10-22 13:29:07
        public void SaveDeliveryStrategyAndBasicSetting(CustomerDeliveryStrategyEntity DeliveryStrategyEntity, CustomerBasicSettingEntity BasicSettingEntity, LoggingSessionInfo loggingSessionInfo, string deliveryId)
        {
            
            TransactionHelper tranHelper = new TransactionHelper(loggingSessionInfo);
            IDbTransaction tran = tranHelper.CreateTransaction();

            CustomerBasicSettingBLL basicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);

            #region 删除信息

            //此商户下送货到家信息
            CustomerDeliveryStrategyEntity[] DeliveryStrategyList = QueryByEntity(
                new CustomerDeliveryStrategyEntity
                {
                    CustomerId = loggingSessionInfo.ClientID,
                    DeliveryId = DeliveryStrategyEntity.DeliveryId
                },
                null
            );

            //此商户下基数设置（描述）信息
            CustomerBasicSettingEntity[] BasicSettingList = basicSettingBLL.QueryByEntity(new CustomerBasicSettingEntity
            {
                SettingCode = "DeliveryStrategy",
                CustomerID = loggingSessionInfo.ClientID
            },
                null
            );

            //除本次保存数据的此商户下送货到家信息
            CustomerDeliveryStrategyEntity[] DeliveryStrategyList_d = DeliveryStrategyList.Where(m => m.Id !=DeliveryStrategyEntity.Id).ToArray();
            //除本次保存数据的此商户下基数设置（描述）信息
            CustomerBasicSettingEntity[] BasicSettingList_d = BasicSettingList.Where(m => m.SettingID != BasicSettingEntity.SettingID).ToArray();

            if (DeliveryStrategyList_d != null && DeliveryStrategyList_d.Count() > 0)
            {
                Delete(DeliveryStrategyList_d, tran);
            }
            if (BasicSettingList_d != null && BasicSettingList_d.Count() > 0)
            {
                basicSettingBLL.Delete(BasicSettingList_d, tran);
            }
            #endregion

            #region 保存

            if (DeliveryStrategyEntity.Id == null || string.IsNullOrWhiteSpace(DeliveryStrategyEntity.Id.ToString()))
            {
                DeliveryStrategyEntity.Id = Guid.NewGuid();
                Create(DeliveryStrategyEntity, tran);
            }
            else
            {
                Update(DeliveryStrategyEntity, tran);
            }

            if (BasicSettingEntity.SettingID == null || string.IsNullOrWhiteSpace(BasicSettingEntity.SettingID.ToString()))
            {
                BasicSettingEntity.SettingID = Guid.NewGuid();
                basicSettingBLL.Create(BasicSettingEntity, tran);
            }
            else
            {
                basicSettingBLL.Update(BasicSettingEntity, tran);
            }

            //修改配送方式状态
            DeliveryBLL deliveryBll = new DeliveryBLL(loggingSessionInfo);
            deliveryBll.Update(
                   new DeliveryEntity() { 
                    DeliveryId = deliveryId,
                    Status = DeliveryStrategyEntity.Status
                   }
                   ,tran
                );
            #endregion
            tran.Commit();
        }

        

    }
}