using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.Order.Delivery.Request
{
   public class UpdateOrderDeliveryInfoRP:IAPIRequestParameter
   {
       #region 属性
       /// <summary>
       /// 订单编号（必填）
       /// </summary>
       public string OrderID { get; set; }

       /// <summary>
       /// 配送方式 1.送货到家，2到店自提（必填）
       /// </summary>
       public int DeliveryTypeID { get; set; }

       /// <summary>
       /// 自提的门店ID（当配送方式为到店自提的时候必填）
       /// </summary>
       public string StoreID { get; set; }

       /// <summary>
       /// 自提开始时间-结束时间（当配送方式为到店自提的时候必填）
       /// </summary>
       public string PickupUpDateRange{ get; set; }

       /// <summary>
       /// 手机号码
       /// </summary>
       public string Mobile { get; set; }

       /// <summary>
       /// 邮箱
       /// </summary>
       public string Email { get; set; }

       /// <summary>
       /// 收货地址
       /// </summary>
       public string ReceiverAddress { get; set; }

       /// <summary>
       /// 收货人姓名
       /// </summary>
       public string ReceiverName { get; set; }


       /// <summary>
       /// 更新方式为到店自提时，自提ID，自提开始时间，自提结束时间都不能为空
       /// </summary>
       const int ERROR_CODE_NO_STOREID = 301;
       const int ERROR_CODE_NO_DATERANGE = 302;
       #endregion
       public void Validate()
        {
            if (this.DeliveryTypeID==2)
            {
                if (string.IsNullOrWhiteSpace(StoreID))
                {
                    throw new APIException("更新方式为到店自提时,未传自提门店ID") { ErrorCode = ERROR_CODE_NO_STOREID };
                }
                if (string.IsNullOrWhiteSpace(this.PickupUpDateRange.ToString()))
                {
                    throw new APIException("更新方式为到店自提时,未传自提时间范围") { ErrorCode = ERROR_CODE_NO_DATERANGE };
                }
            }
        }
    }
}
