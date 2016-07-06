using CPOS.Common;
using JIT.CPOS.BS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.SapMessage
{
    public class SapUDSORDERMsg : BaseSapMsg
    {
        public T_InoutBLL inoutBll { get; set; }
        public TInoutStatusEntity inoutStatus { get; set; }
        public TInoutStatusBLL inoutstatusBll { get; set; }

        public SapUDSORDERMsg()
            : base()
        {
            inoutBll = new T_InoutBLL(loggingSessionInfo);


            inoutStatus = new TInoutStatusEntity();
            inoutstatusBll = new TInoutStatusBLL(loggingSessionInfo);
        }

        #region 更新订单信息
        /// <summary>
        /// 更新订单信息
        /// </summary>
        public override bool Update()
        {
            if (string.IsNullOrEmpty(MsgObjRD.Msg1.Content) && MsgObjRD.Omsg.Status > 0)
            {
                Msg = "Content 为空，无消息,status=" + MsgObjRD.Omsg.Status;
                return true;
            }

            if (string.IsNullOrEmpty(MsgObjRD.Msg1.Content))
            {
                Msg = "Content 为空，无消息";
                return false;
            }


            string path = "BOM/BO/UDSORDER/row/";
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

                inoutBll.Update(inout);

                try
                {
                    inoutStatus.CustomerID = inout.customer_id;
                    inoutStatus.InoutStatusID = Guid.NewGuid();
                    inoutStatus.OrderID = inout.order_id;
                    inoutStatus.OrderStatus = TypeParse.ToInt(inout.status);
                    inoutStatus.Remark = "SAP同步";
                    inoutStatus.StatusRemark = "SAP消息：" + ReadXml(path + "StatusCode") + "，已经发货";
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
            Msg = "未找到对应的订单信息:" + orderNo;
            return false;
        }
        #endregion
    }
}
