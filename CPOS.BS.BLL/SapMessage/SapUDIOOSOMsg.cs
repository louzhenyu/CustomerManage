using CPOS.Common;
using JIT.CPOS.BS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.SapMessage
{
    /// <summary>
    /// sap-订单头
    /// </summary>
    public class SapUDIOOSOMsg : BaseSapMsg
    {
        public T_InoutBLL inoutBll { get; set; }
        public TInoutStatusEntity inoutStatus { get; set; }

        public TInoutStatusBLL inoutstatusBll { get; set; }
        public SapUDIOOSOMsg()
            : base()
        {
            inoutBll = new T_InoutBLL(loggingSessionInfo);

            inoutStatus = new TInoutStatusEntity();
            inoutstatusBll = new TInoutStatusBLL(loggingSessionInfo);
        }

        public override bool Add()
        {
            string path = "BOM/BO/UDIOOSO/row/";
            string orderNo = ReadXml(path + "oBarCode");
            T_InoutEntity inout = inoutBll.QueryByEntity(new T_InoutEntity() { order_no = orderNo }, null).FirstOrDefault();


            if (inout != null)
            {
                if ("P,C".Contains(ReadXml(path + "StatusCode")))
                {
                    inout.Field7 = "600";
                    inout.Field10 = "已发货";

                    inout.status = "600";
                    inout.status_desc = "已发货";
                }
                string logisticsPath = "BOM/BO/UDIOOSOLogi/row/";
                inout.Field2 = ReadXml(logisticsPath + "LogiNo");
                inout.Field18 = GetLogisticsCompanyId(ReadXml(logisticsPath + "LogiServiceCode"));
                inoutBll.Update(inout);

                try
                {
                    inoutStatus.CustomerID = inout.customer_id;
                    inoutStatus.InoutStatusID = Guid.NewGuid();
                    inoutStatus.OrderID = inout.order_id;
                    inoutStatus.OrderStatus = TypeParse.ToInt(inout.status);
                    inoutStatus.Remark = "SAP同步";
                    inoutStatus.StatusRemark = "SAP消息：" + ReadXml(path + "StatusCode") + "，已经发货";
                    inoutStatus.unit_name = ReadXml(logisticsPath + "LogiServiceName");
                    inoutStatus.CreateBy = "SAP";
                    inoutStatus.CreateTime = DateTime.Now;
                    inoutStatus.LastUpdateBy = "SAP";
                    inoutStatus.LastUpdateTime = DateTime.Now;
                    inoutstatusBll.Update(inoutStatus);

                }
                catch (Exception ex)
                {
                }
                return true;
            }
            Msg = "未查询到订单：oBarCode：" + orderNo;
            return false;
        }

        #region 更新订单信息
        /// <summary>
        /// 更新订单信息
        /// </summary>
        public override bool Update()
        {
            string path = "BOM/BO/UDIOOSO/row/";
            string orderNo = ReadXml(path + "oBarCode");
            T_InoutEntity inout = inoutBll.QueryByEntity(new T_InoutEntity() { order_no = orderNo }, null).FirstOrDefault();
            if (inout != null)
            {
                if ("P,C".Contains(ReadXml(path + "StatusCode")))
                {
                    inout.Field7 = "600";
                    inout.Field10 = "已发货";

                    inout.status = "600";
                    inout.status_desc = "已发货";
                }
                string logisticsPath = "BOM/BO/UDIOOSOLogi/row/";
                inout.Field2 = ReadXml(logisticsPath + "LogiNo");
                // inout.Field18 = new  T_LogisticsCompanyBLL()
                inout.Field18 = GetLogisticsCompanyId(ReadXml(logisticsPath + "LogiServiceCode"));
                inoutBll.Update(inout);

                try
                {
                    inoutStatus.CustomerID = inout.customer_id;
                    inoutStatus.InoutStatusID = Guid.NewGuid();
                    inoutStatus.OrderID = inout.order_id;
                    inoutStatus.OrderStatus = TypeParse.ToInt(inout.status);
                    inoutStatus.Remark = "SAP同步";
                    inoutStatus.StatusRemark = "SAP消息：" + ReadXml(path + "StatusCode") + "，已经发货";
                    inoutStatus.unit_name = ReadXml(logisticsPath + "LogiServiceName");
                    inoutStatus.CreateBy = "SAP";
                    inoutStatus.CreateTime = DateTime.Now;
                    inoutStatus.LastUpdateBy = "SAP";
                    inoutStatus.LastUpdateTime = DateTime.Now;
                    inoutstatusBll.Update(inoutStatus);

                }
                catch (Exception ex)
                {
                }

                return true;
            }
            Msg = "未查询到订单：oBarCode：" + orderNo;
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logisticsName"></param>
        /// <returns></returns>
        private string GetLogisticsCompanyId(string logisticsCode)
        {
            var entity = new T_LogisticsCompanyBLL(loggingSessionInfo).QueryByEntity(new T_LogisticsCompanyEntity() { LogisticsShortName = logisticsCode }, null).FirstOrDefault();
            if (entity != null)
            {
                return entity.LogisticsID.Value.ToString();
            }
            return string.Empty;
        }
        #endregion
    }
}
