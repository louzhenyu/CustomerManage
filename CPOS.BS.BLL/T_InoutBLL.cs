/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 20:18:26
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
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Reflection;
using System.Web;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.DTO.Base;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.Order.Response;
using JIT.CPOS.DTO.Module.Order.Order.Response;
using JIT.Utility.Log;
using JIT.CPOS.Common;
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_InoutBLL
    {
        /// <summary>
        /// ȡ������(Api�ͺ�̨ͨ��)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userType">0=��Ա��1=��̨�û�</param>
        /// <param name="loggingSessionInfo"></param>
        public void SetCancelOrder(string orderId, int userType, LoggingSessionInfo loggingSessionInfo)
        {
            var vipBll = new VipBLL(loggingSessionInfo);                    //��Աҵ��ʵ����
            var inoutDetailBLL = new Inout3Service(loggingSessionInfo);     //����ҵ��ʵ����
            var refundOrderBll = new T_RefundOrderBLL(loggingSessionInfo);  //�˻�ҵ��ʵ����
            var inoutBll = new T_InoutBLL(loggingSessionInfo);              //����ҵ��ʵ����

            //��ȡ��������
            var inoutInfo = inoutBll.GetInoutInfo(orderId, loggingSessionInfo);

            //������֡������ֺ��Ż�ȯ
            vipBll.ProcSetCancelOrder(loggingSessionInfo.ClientID, inoutInfo.order_id, inoutInfo.vip_no);
            //��ȡ������ϸ
            var inoutDetailInfo = inoutDetailBLL.GetInoutDetailInfoByOrderId(inoutInfo.order_id, loggingSessionInfo.ClientID).FirstOrDefault();

            #region �����˿�ҵ��
            if (inoutInfo != null)
            {
                if (inoutInfo.Field1 == "1" && (inoutInfo.actual_amount - inoutInfo.DeliveryAmount) > 0)//�Ѹ���,����ʵ����-�˷�>0
                {

                    #region �����˻���(Ĭ��״̬Ϊȷ���ջ�)
                    var salesReturnBLL = new T_SalesReturnBLL(loggingSessionInfo);
                    var historyBLL = new T_SalesReturnHistoryBLL(loggingSessionInfo);
                    T_SalesReturnEntity salesReturnEntity = null;
                    T_SalesReturnHistoryEntity historyEntity = null;

                    var userBll = new T_UserBLL(loggingSessionInfo);    //��ԱBLLʵ����
                    VipEntity vipEntity = null;                         //��Ա��Ϣ

                    salesReturnEntity = new T_SalesReturnEntity();
                    salesReturnEntity.SalesReturnNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    salesReturnEntity.VipID = loggingSessionInfo.UserID;
                    salesReturnEntity.ServicesType = 1;//�˻�
                    salesReturnEntity.DeliveryType = 1;//����ͻ�;
                    salesReturnEntity.OrderID = inoutInfo.order_id;
                    if (inoutDetailInfo != null)
                    {
                        salesReturnEntity.ItemID = inoutDetailInfo.item_id;
                        salesReturnEntity.SkuID = inoutDetailInfo.sku_id;
                    }
                    salesReturnEntity.Qty = 0;
                    salesReturnEntity.ActualQty = 0;
                    if (inoutInfo != null)
                    {
                        salesReturnEntity.UnitID = inoutInfo.sales_unit_id;
                        //salesReturnEntity.UnitName = para.UnitName;
                        //salesReturnEntity.UnitTel = para.UnitTel;
                        salesReturnEntity.Address = inoutInfo.Field4;
                        salesReturnEntity.Contacts = inoutInfo.Field14 != null ? inoutInfo.Field14 : "";
                        salesReturnEntity.Phone = inoutInfo.Field6 != null ? inoutInfo.Field6 : "";
                    }
                    salesReturnEntity.Reason = "ȡ������";
                    if (inoutInfo.actual_amount - inoutInfo.DeliveryAmount > 0)
                        salesReturnEntity.Status = 6; //����ɣ����˿
                    else
                        salesReturnEntity.Status = 7; //����ɣ����˿
                    salesReturnEntity.CustomerID = loggingSessionInfo.ClientID;
                    salesReturnBLL.Create(salesReturnEntity);

                    string userName = string.Empty;//����������
                    if (userType == 0)//��Ա����
                    {
                        vipEntity = vipBll.GetByID(loggingSessionInfo.UserID);
                        userName = vipEntity != null ? vipEntity.VipName : "";
                    }
                    else//��̨�û�����
                        userName = loggingSessionInfo.CurrentUser.User_Name;
                    historyEntity = new T_SalesReturnHistoryEntity()
                    {
                        SalesReturnID = salesReturnEntity.SalesReturnID,
                        OperationType = 14,
                        OperationDesc = "ȡ������",
                        OperatorID = loggingSessionInfo.UserID,
                        HisRemark = "ȡ������",
                        OperatorName = userName,
                        OperatorType = userType   //0=��Ա;1=�����û�
                    };
                    historyBLL.Create(historyEntity);
                    #endregion

                    #region �����˿
                    if (inoutInfo.actual_amount - inoutInfo.DeliveryAmount > 0)
                    {
                        T_RefundOrderEntity refundOrderEntity = new T_RefundOrderEntity()
                        {
                            RefundNo = DateTime.Now.ToString("yyyyMMddhhmmfff"),
                            VipID = inoutInfo.vip_no,
                            SalesReturnID = salesReturnEntity.SalesReturnID,
                            //ServicesType = 1,//�˻�
                            DeliveryType = 1,//����ͻ�
                            ItemID = inoutDetailInfo.item_id,
                            SkuID = inoutDetailInfo.sku_id,
                            Qty = 0,
                            ActualQty = 0,
                            UnitID = inoutInfo.sales_unit_id,
                            //salesReturnEntity.UnitName = para.UnitName;
                            //salesReturnEntity.UnitTel = para.UnitTel;
                            Address = inoutInfo.Field4,
                            Contacts = inoutInfo.Field14,
                            Phone = inoutInfo.Field6,
                            OrderID = inoutInfo.order_id,
                            PayOrderID = inoutInfo.paymentcenter_id,
                            RefundAmount = inoutInfo.total_amount,
                            ConfirmAmount = inoutInfo.total_amount,
                            ActualRefundAmount = inoutInfo.actual_amount - inoutInfo.DeliveryAmount,//ʵ�˽��=ʵ����-�˷�
                            Points = inoutInfo.pay_points == null ? 0 : Convert.ToInt32(inoutInfo.pay_points),
                            ReturnAmount = inoutInfo.ReturnAmount,
                            Amount = inoutInfo.VipEndAmount,
                            Status = 1,//���˿�
                            CustomerID = loggingSessionInfo.ClientID
                        };
                        refundOrderBll.Create(refundOrderEntity);
                    }
                    #endregion

                }
            }
            #endregion
        }
    }
}