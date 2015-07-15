using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Order.SalesReturn.Request
{
    public class SetSalesReturnRP : IAPIRequestParameter
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Guid? SalesReturnID { get; set; }
        /// <summary>
        /// 操作类型（1=申请；2=取消申请；3=确认收货；4=确认退款；5=App退换货）
        /// </summary>
        public int OperationType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String VipID { get; set; }

        /// <summary>
        /// 1=退货   2=换货
        /// </summary>
        public Int32? ServicesType { get; set; }

        /// <summary>
        /// 1=送回门店；   2=快递送回
        /// </summary>
        public Int32? DeliveryType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String OrderID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String ItemID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SkuID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UnitID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UnitName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String UnitTel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Contacts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Phone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Reason { get; set; }
        /// <summary>
        /// 确定退款金额
        /// </summary>
        public decimal ConfirmAmount { get; set; }
        /// <summary>
        /// 实退数量
        /// </summary>
        public int ActualQty { get; set; }

        /// <summary>
        /// 后台-操作描述
        /// </summary>
        public string Desc { get; set; }
        #endregion
        public void Validate()
        {

        }
    }
}
