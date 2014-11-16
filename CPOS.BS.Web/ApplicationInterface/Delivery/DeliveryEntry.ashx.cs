using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS.Web.Base.Excel;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Entity;
using System.Data;
using JIT.Utility.DataAccess.Query;
/********************************************************************************

    * 创建时间: 2014-10-21 14:46:57
    * 作    者：donal
    * 说    明：配送方式类
    * 修改时间：2014-10-21 14:46:57
    * 修 改 人：donal

*********************************************************************************/


namespace JIT.CPOS.BS.Web.ApplicationInterface.Delivery
{
    /// <summary>
    /// DeliveryEntry 的摘要说明
    /// </summary>
    public class DeliveryEntry : BaseGateway
    {
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetDeliveryList":
                    rst = this.GetDeliveryList(pRequest);
                    break;
                case "GetDeliveryDetail":
                    rst = this.GetDeliveryDetail(pRequest);
                    break;
                case "SaveDelivery":
                    rst = this.SaveDelivery(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }

        /// <summary>
        /// 获取配送方式列表
        /// </summary>
        private string GetDeliveryList(string pRequest)
        {            

            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            DeliveryBLL dService = new DeliveryBLL(loggingSessionInfo);
            var deliveryList = dService.GetAll();

            List<DeliveryItemData> list = new List<DeliveryItemData>();
            foreach (var paymentInfo in deliveryList)
            {
                DeliveryItemData info = new DeliveryItemData();
                info.deliveryId = paymentInfo.DeliveryId;
                info.deliveryName = paymentInfo.DeliveryName;
                info.isAddress = paymentInfo.IsDelete.ToString();
                
                //判断是否启用                
                if (paymentInfo.DeliveryId == "1")
                {
                    CustomerDeliveryStrategyBLL deliveryStrategyBLL = new CustomerDeliveryStrategyBLL(loggingSessionInfo);
                    var deliveryStrategy = deliveryStrategyBLL.QueryByEntity(
                        new CustomerDeliveryStrategyEntity
                        {
                            CustomerId = loggingSessionInfo.ClientID,
                            DeliveryId = paymentInfo.DeliveryId
                        },
                        null
                    ).FirstOrDefault();

                    if (deliveryStrategy != null)
                    {
                        info.IsOpen = deliveryStrategy.Status == 1 ? true : false;
                    }
                }
                else if (paymentInfo.DeliveryId == "2")
                {
                    //查询提货配置信息
                    CustomerTakeDeliveryBLL takeDeliveryBLL = new CustomerTakeDeliveryBLL(loggingSessionInfo);
                    var takeDelivery = takeDeliveryBLL.QueryByEntity(
                        new CustomerTakeDeliveryEntity()
                        {
                            CustomerId = loggingSessionInfo.ClientID
                        }, null
                    ).FirstOrDefault();

                    if (takeDelivery != null)
                    {
                        info.IsOpen = takeDelivery.Status == 1 ? true : false;
                    }
                }
                list.Add(info);
            }

            var rd = new DeliveryListRD() { 
                DeliveryList = list
            };

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 获取配送信息
        /// </summary>
        private string GetDeliveryDetail(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<DeliveryInfoRp>>();

            if (string.IsNullOrWhiteSpace(rp.Parameters.DeliveryId))
	        {
                throw new APIException("请求参数中缺少DeliveryId或值为空.") { ErrorCode = 121 };
	        }

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            //配送信息
            DeliveryInfo DeliveryInfo = new DeliveryInfo();

            //TO DO 查询配送方式信息
            if (rp.Parameters.DeliveryId == "1")
            //送货到家信息
            {
                //查询商户配送策略表
                CustomerDeliveryStrategyBLL deliveryStrategyBLL = new CustomerDeliveryStrategyBLL(loggingSessionInfo);
                var deliveryStrategy = deliveryStrategyBLL.QueryByEntity(
                    new CustomerDeliveryStrategyEntity
                    {
                        CustomerId = loggingSessionInfo.ClientID,
                        DeliveryId = rp.Parameters.DeliveryId
                    },
                    null
                ).FirstOrDefault();

                //查询商户基数设置
                CustomerBasicSettingBLL basicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);
                var basicSetting = basicSettingBLL.QueryByEntity(
                    new CustomerBasicSettingEntity
                    {
                        SettingCode = "DeliveryStrategy",
                        CustomerID = loggingSessionInfo.ClientID

                    },
                    null
                ).FirstOrDefault();

                //组织配送信息

                if (deliveryStrategy != null)
                {
                    DeliveryInfo.DeliveryStrategyId = deliveryStrategy.Id.ToString(); //配送策略id
                    DeliveryInfo.Status = deliveryStrategy.Status; //是否启用  1启用 0停用
                    DeliveryInfo.DeliveryAmount = deliveryStrategy.DeliveryAmount;//默认配送费用
                    DeliveryInfo.AmountEnd = deliveryStrategy.AmountEnd;//免配送费最低订单金额
                }
                else
                {
                    DeliveryInfo.Status = 0;
                }

                if (basicSetting !=null)
                {
                    DeliveryInfo.SettingId = basicSetting.SettingID.ToString();//商户基础设置
                    DeliveryInfo.Description = basicSetting.SettingValue;//描述
                }
            }
            else if (rp.Parameters.DeliveryId == "2")
            //到点提货信息
            {
                //查询提货配置信息
                CustomerTakeDeliveryBLL takeDeliveryBLL = new CustomerTakeDeliveryBLL(loggingSessionInfo);
                var takeDelivery = takeDeliveryBLL.QueryByEntity(
                    new CustomerTakeDeliveryEntity() { 
                        CustomerId = loggingSessionInfo.ClientID
                    },null
                ).FirstOrDefault();


                DeliveryInfo.DeliveryAmount = 0;
                DeliveryInfo.AmountEnd = 0;

                if (takeDelivery != null)
                {
                    DeliveryInfo.TakeDeliveryId = takeDelivery.Id.ToString();
                    DeliveryInfo.StockUpPeriod = takeDelivery.StockUpPeriod;
                    DeliveryInfo.BeginWorkTime = takeDelivery.BeginWorkTime;
                    DeliveryInfo.EndWorkTime = takeDelivery.EndWorkTime;
                    DeliveryInfo.MaxDelivery = takeDelivery.MaxDelivery;
                    DeliveryInfo.Status = takeDelivery.Status;
                }
                else
                {
                    DeliveryInfo.Status = 0;
                }
            }
            else
            {
                throw new APIException("DeliveryId不存在.") { ErrorCode = 121 };
            }

            var rd = new DeliveryInfoRD
            {
                DeliveryInfo = DeliveryInfo
            };

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }

        /// <summary>
        /// 保存配送信息
        /// </summary>
        private string SaveDelivery(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<DeliveryInfoRp>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            if (string.IsNullOrWhiteSpace(rp.Parameters.DeliveryId) || (rp.Parameters.DeliveryId != "1" && rp.Parameters.DeliveryId != "2"))
            {
                throw new APIException("请求参数中缺少DeliveryId或值为空.") { ErrorCode = 121 };
            }

            if (loggingSessionInfo ==null)
            {
                throw new APIException("请重新登录") { ErrorCode = 122 };
            }
            
            if (rp.Parameters.DeliveryId =="1")
                //保存送货到家信息
            {
                CustomerDeliveryStrategyBLL deliveryStrategyBLL = new CustomerDeliveryStrategyBLL(loggingSessionInfo);
                CustomerBasicSettingBLL basicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);

                if (rp.Parameters.AmountEnd<=0)
                {
                    throw new APIException("请求参数中AmountEnd值为空,或者不正确") { ErrorCode = 123 };
                }
                if (rp.Parameters.DeliveryAmount<=0)
                {
                    throw new APIException("请求参数中DeliveryAmount值为空,或者不正确") { ErrorCode = 124 };
                }
                if (rp.Parameters.Status!=0&&rp.Parameters.Status!=1)
                {
                    throw new APIException("请求参数中Status值为空,或者不正确") { ErrorCode = 125 };
                }
                if (string.IsNullOrWhiteSpace(rp.Parameters.Description))
                {
                    throw new APIException("请求参数中Description值为空,或者不正确") { ErrorCode = 126 };
                }
                
                //要保存的送货到家信息
                CustomerDeliveryStrategyEntity DeliveryStrategyEntity = new CustomerDeliveryStrategyEntity()
                {
                    CustomerId = loggingSessionInfo.ClientID,
                    AmountBegin =0m,
                    AmountEnd = rp.Parameters.AmountEnd,
                    DeliveryAmount = rp.Parameters.DeliveryAmount,
                    Status = rp.Parameters.Status,
                    DeliveryId = "1"
                };

                if (!string.IsNullOrWhiteSpace(rp.Parameters.DeliveryStrategyId))
                {
                    DeliveryStrategyEntity.Id = new Guid(rp.Parameters.DeliveryStrategyId);
                }

                //要保存的基数设置（描述）信息
                CustomerBasicSettingEntity BasicSettingEntity = new CustomerBasicSettingEntity()
                {
                    CustomerID = loggingSessionInfo.ClientID,
                    SettingCode = "DeliveryStrategy",
                    SettingValue  = rp.Parameters.Description
                };

                if (!string.IsNullOrWhiteSpace(rp.Parameters.SettingId))
                {
                    BasicSettingEntity.SettingID = new Guid(rp.Parameters.SettingId);
                }

                deliveryStrategyBLL.SaveDeliveryStrategyAndBasicSetting(DeliveryStrategyEntity, BasicSettingEntity, loggingSessionInfo,rp.Parameters.DeliveryId);
                
            }
            if (rp.Parameters.DeliveryId=="2")
                //保存到店提货信息
            {
                if (rp.Parameters.StockUpPeriod<=0)
                {
                    throw new APIException("请求参数中StockUpPeriod值为空,或者不正确") { ErrorCode = 127 };
                }

                if (rp.Parameters.BeginWorkTime==null)
                {
                     throw new APIException("请求参数中BeginWorkTime值为空,或者不正确") { ErrorCode = 127 };
                }

                if (rp.Parameters.EndWorkTime==null)
                {
                    throw new APIException("请求参数中EndWorkTime值为空,或者不正确") { ErrorCode = 127 };
                }

                if (rp.Parameters.MaxDelivery <= 0)
                {
                    throw new APIException("请求参数中MaxDelivery值为空,或者不正确") { ErrorCode = 127 };
                }

                if (rp.Parameters.Status!=1&&rp.Parameters.Status!=0)
                {
                    throw new APIException("请求参数中Status值为空,或者不正确") { ErrorCode = 127 };
                }

                CustomerTakeDeliveryEntity takeDeliveryEntity = new CustomerTakeDeliveryEntity()
                {                    
                    CustomerId = loggingSessionInfo.ClientID,
                    StockUpPeriod = rp.Parameters.StockUpPeriod,
                    BeginWorkTime = rp.Parameters.BeginWorkTime,
                    EndWorkTime = rp.Parameters.EndWorkTime,
                    MaxDelivery = rp.Parameters.MaxDelivery,
                    Status = rp.Parameters.Status
                };

                if (!string.IsNullOrWhiteSpace(rp.Parameters.TakeDeliveryId))
                {
                    takeDeliveryEntity.Id = new Guid(rp.Parameters.TakeDeliveryId);
                }

                //查询提货配置信息
                CustomerTakeDeliveryBLL takeDeliveryBLL = new CustomerTakeDeliveryBLL(loggingSessionInfo);
                takeDeliveryBLL.SaveCustomerTakeDelivery(takeDeliveryEntity, loggingSessionInfo,rp.Parameters.DeliveryId);
                
            }
            
            var rsp = new SuccessResponse<IAPIResponseData>(new EmptyResponseData());
            return rsp.ToJSON();
        }

    }


    #region  构建参数对象

    /// <summary>
    /// 配送列表
    /// </summary>
    public class DeliveryListRD : IAPIResponseData
    {
        public List<DeliveryItemData> DeliveryList { get; set; }
    }

    /// <summary>
    /// 配送列表对象
    /// </summary>
    public class DeliveryItemData
    {
        public string deliveryId { get; set; } //支付方式标识
        public string deliveryName { get; set; } //支付产品类别
        public string isAddress { get; set; } //是否手动设置地址

        public bool IsOpen { get; set; } //是否启用
    }

    /// <summary>
    /// 配送信息请求参数
    /// </summary>
    public class DeliveryInfoRp : IAPIRequestParameter
    {
        /// <summary>
        /// 配送方式Id
        /// </summary>
        public string DeliveryId { get; set; }
        /// <summary>
        /// 配送策略Id
        /// </summary>
        public string DeliveryStrategyId { get; set; }
        /// <summary>
        /// 商户基础设置Id
        /// </summary>
        public string SettingId { get; set; }
        /// <summary>
        /// 到点提货Id
        /// </summary>
        public string TakeDeliveryId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 默认配送费
        /// </summary>
        public decimal DeliveryAmount { get; set; }
        /// <summary>
        /// 免配送费最低订单金额
        /// </summary>
        public decimal AmountEnd { get; set; }
        /// <summary>
        /// 备货期
        /// </summary>
        public int? StockUpPeriod { get; set; }
        /// <summary>
        /// 门店工作时间开始
        /// </summary>
        public DateTime? BeginWorkTime { get; set; }
        /// <summary>
        /// 门店工作时间结束
        /// </summary>
        public DateTime? EndWorkTime { get; set; }
        /// <summary>
        /// 提货期最长
        /// </summary>
        public int? MaxDelivery { get; set; }

        public void Validate()
        {
        }
    }

    /// <summary>
    /// 获取配送信息
    /// </summary>
    public class DeliveryInfoRD : IAPIResponseData
    { 
        public DeliveryInfo DeliveryInfo{get;set;}
    }

    /// <summary>
    /// 配送信息对象
    /// </summary>
    public class DeliveryInfo
    {
        /// <summary>
        /// 配送策略Id
        /// </summary>
        public string DeliveryStrategyId { get; set; }
        /// <summary>
        /// 商户基础设置Id
        /// </summary>
        public string SettingId { get; set; }
        /// <summary>
        /// 到点提货Id
        /// </summary>
        public string TakeDeliveryId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Int32? Status { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description{get;set;}
        /// <summary>
        /// 默认配送费
        /// </summary>
        public decimal? DeliveryAmount {get;set;}
        /// <summary>
        /// 免配送费最低订单金额
        /// </summary>
        public decimal? AmountEnd {get;set;}
        /// <summary>
        /// 备货期
        /// </summary>
        public int? StockUpPeriod{get;set;}
        /// <summary>
        /// 门店工作时间开始
        /// </summary>
        public DateTime? BeginWorkTime{get;set;}
        /// <summary>
        /// 门店工作时间结束
        /// </summary>
        public DateTime? EndWorkTime {get;set;}
        /// <summary>
        /// 提货期最长
        /// </summary>
        public int? MaxDelivery{get;set;}
    }


    #endregion
}