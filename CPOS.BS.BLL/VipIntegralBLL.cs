/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 11:13:49
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
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.CPOS.DTO.Base;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Log;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipIntegralBLL
    {
        #region Jermyn20130916 �����ն����ѣ�������֣�������Ϣ
        public bool SetPushIntegral(string orderId,string msgUrl, out string strError)
        {
            try
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPushIntegral--������ʶ:{0}", orderId)
                });

                VipBLL vipBLL = new VipBLL(this.CurrentUserInfo);
                IntegralRuleBLL integralRuleBLL = new IntegralRuleBLL(this.CurrentUserInfo);
                VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(this.CurrentUserInfo);
                VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(this.CurrentUserInfo);
                InoutInfo orderInfo = new InoutInfo();
                InoutService orderService = new InoutService(this.CurrentUserInfo);
                orderInfo = orderService.GetInoutInfoById(orderId);

                if (orderInfo == null || orderInfo.order_id == null)
                {
                    strError = "����������.";
                    return false;
                }

                if (orderInfo != null)
                {

                    string integralSourceId = "1";
                    int integralValue = 0;
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetPushIntegral --Order--��������:{0}", orderInfo.ToJSON())
                    });
                    if (orderInfo.vip_no == null || orderInfo.vip_no.Trim().Length == 0)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetPushIntegral--�����ϵ�:{0}", "vip_noΪ��")
                        });
                        strError = "vip_noΪ��";
                        return false;
                    }

                    #region ��ѯ��ԱID
                    VipEntity vipInfo = null;
                    var vipIdDataList = vipBLL.QueryByEntity(new VipEntity()
                    {
                        VIPID = orderInfo.vip_no
                    }, null);
                    if (vipIdDataList == null || vipIdDataList.Length == 0 || vipIdDataList[0] == null)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetPushIntegral:{0}", "���ݿⲻ���ڶ�Ӧ��vip��¼")
                        });
                        strError = "���ݿⲻ���ڶ�Ӧ��vip��¼";
                        return false;
                    }
                    else
                    {
                        vipInfo = vipIdDataList[0];
                    }
                    #endregion

                    #region �������
                    IntegralRuleEntity integralRuleData = null;
                    var integralRuleDataList = integralRuleBLL.QueryByEntity(new IntegralRuleEntity()
                    {
                        IntegralSourceID = integralSourceId
                    }, null);
                    if (integralRuleDataList == null || integralRuleDataList.Length == 0 || integralRuleDataList[0] == null)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetVipIntegral:{0}", "δ��ѯ�����ֹ���")
                        });
                        strError = "δ��ѯ�����ֹ���";
                        return false;
                    }
                    else
                    {
                        integralRuleData = integralRuleDataList[0];
                        integralValue = CPOS.Common.Utils.GetParseInt(integralRuleData.Integral) *
                            CPOS.Common.Utils.GetParseInt(orderInfo.total_amount);
                    }
                    #endregion

                    #region ���������ϸ
                    VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                    vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                    vipIntegralDetailEntity.VIPID = vipInfo.VIPID;
                    vipIntegralDetailEntity.FromVipID = vipInfo.VIPID;
                    vipIntegralDetailEntity.SalesAmount = orderInfo.total_amount;
                    vipIntegralDetailEntity.Integral = integralValue;
                    vipIntegralDetailEntity.IntegralSourceID = integralSourceId;
                    vipIntegralDetailEntity.IsAdd = 1;
                    //vipIntegralDetailBLL.Create(vipIntegralDetailEntity);

                    // ���»���
                    VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                    var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                        new VipIntegralEntity() { VipID = vipInfo.VIPID }, null);
                    if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 || vipIntegralDataList[0] == null)
                    {
                        vipIntegralEntity.VipID = vipInfo.VIPID;
                        vipIntegralEntity.BeginIntegral = 0; // �ڳ�����
                        vipIntegralEntity.InIntegral = 0; // ���ӻ���
                        vipIntegralEntity.OutIntegral = integralValue; //���ѻ���
                        vipIntegralEntity.EndIntegral = integralValue; //�������
                        vipIntegralEntity.InvalidIntegral = 0; // �ۼ�ʧЧ����
                        vipIntegralEntity.ValidIntegral = integralValue; // ��ǰ��Ч����
                        //vipIntegralBLL.Create(vipIntegralEntity);
                    }
                    else
                    {
                        vipIntegralEntity.VipID = vipInfo.VIPID;
                        //vipIntegralEntity.InIntegral = 0; // ���ӻ���
                        vipIntegralEntity.OutIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].OutIntegral) + integralValue; //���ѻ���
                        vipIntegralEntity.EndIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].EndIntegral) + integralValue; //�������
                        //vipIntegralEntity.InvalidIntegral = 0; // �ۼ�ʧЧ����
                        vipIntegralEntity.ValidIntegral = Common.Utils.GetDecimalVal(
                            vipIntegralDataList[0].ValidIntegral) + integralValue; // ��ǰ��Ч����
                        //vipIntegralBLL.Update(vipIntegralEntity, false);
                    }
                    #endregion

                    #region ����VIP
                    VipEntity vipEntity = new VipEntity();
                    var vipEntityDataList = vipBLL.QueryByEntity(
                        new VipEntity() { VIPID = vipInfo.VIPID}, null);
                    if (vipEntityDataList == null || vipEntityDataList.Length == 0 || vipEntityDataList[0] == null)
                    {
                        vipEntity.VIPID = vipInfo.VIPID;
                        //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        vipEntity.ClientID = this.CurrentUserInfo.CurrentUser.customer_id;
                        vipEntity.Status = 1;
                        vipBLL.Create(vipEntity);
                    }
                    else
                    {
                        vipEntity.VIPID = vipInfo.VIPID;
                        //vipEntity.Integration = vipIntegralEntity.ValidIntegral;
                        vipBLL.Update(vipEntity, false);
                    }
                    #endregion

                    #region ������Ϣ
                    if (vipInfo != null && vipInfo.WeiXinUserId != null && vipInfo.WeiXinUserId.Length > 15)
                    {
                        var strValidIntegral = string.Empty;
                        if (vipIntegralEntity.ValidIntegral == null)
                        {
                            strValidIntegral = "0";
                        }
                        else
                        {
                            decimal vd = (decimal)vipIntegralEntity.ValidIntegral;
                            strValidIntegral = Convert.ToString(decimal.Truncate(vd));
                        }

                        //string msgUrl = ConfigurationManager.AppSettings["push_weixin_msg_url"].Trim();
                        string msgText = string.Format("��л���������ѡ����ո����ѹ���{1}Ԫ����������{0}�������ۼ�Ϊ{2}����ӭ���´ι��١�",
                           Convert.ToString(integralValue), Convert.ToString(System.Math.Abs(double.Parse(orderInfo.total_amount.ToString()))), System.Math.Abs(CPOS.Common.Utils.GetParseInt(vipIntegralEntity.ValidIntegral)));
                        string msgData = "<xml><OpenID><![CDATA[" + vipInfo.WeiXinUserId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                        var msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                        #region ������־
                        MarketSendLogBLL logServer = new MarketSendLogBLL(CurrentUserInfo);
                        MarketSendLogEntity logInfo = new MarketSendLogEntity();
                        logInfo.LogId = BaseService.NewGuidPub();
                        logInfo.IsSuccess = 1;
                        logInfo.MarketEventId = orderInfo.order_id;
                        logInfo.SendTypeId = "2";
                        logInfo.TemplateContent = msgData;
                        logInfo.VipId = vipInfo.VIPID;
                        logInfo.WeiXinUserId = vipInfo.WeiXinUserId;
                        logInfo.CreateTime = System.DateTime.Now;
                        logServer.Create(logInfo);
                        #endregion
                        //���͹ν���Ϣ
                        msgText = "��Ӯ����һ�����߳齱���ᣬ<a href='http://o2oapi.aladingyidong.com/wap/weixin/luckyDraw.html'>�������ν�</a>";
                        msgData = "<xml><OpenID><![CDATA[" + vipInfo.WeiXinUserId + "]]></OpenID><Content><![CDATA[" + msgText + "]]></Content></xml>";

                        msgResult = Common.Utils.GetRemoteData(msgUrl, "POST", msgData);
                        #region ������־
                        logInfo.LogId = BaseService.NewGuidPub();
                        logInfo.IsSuccess = 1;
                        logInfo.MarketEventId = orderInfo.order_id;
                        logInfo.SendTypeId = "2";
                        logInfo.TemplateContent = msgData;
                        logInfo.VipId = vipInfo.VIPID;
                        logInfo.WeiXinUserId = vipInfo.WeiXinUserId;
                        logInfo.CreateTime = System.DateTime.Now;
                        logServer.Create(logInfo);
                        #endregion

                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("PushMsgResult:{0}", msgResult)
                        });
                    }
                    #endregion
                    //GetHightOpenInfo(data); //Jermyn20130517���ϼ���ӻ���
                    //respData.Data = vipIntegralEntity.ValidIntegral.ToString();
                }
                strError = "�ɹ�.";
                return true;
            }
            catch (Exception ex) {
                strError = ex.ToString();
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetPushIntegral--ʧ����ʾ:{0}", ex.ToString())
                });
                return false;
            }
        }
        #endregion

        #region ���ִ���    by Willie Yan
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sourceId">������Դ������ҵ�����á����롿</param>
        /// <param name="customerId">�ͻ���ʶ�����롿</param>
        /// <param name="vipId">�û���ʶ�����롿</param>
        /// <param name="objectId">�����ʶ�����롿</param>
        /// <param name="tran">�Ƿ�������</param>
        /// <param name="fromVipId">��Դ��Ա</param>
        /// <param name="point">����(���ָ������,��ʹ�ô˻��ָ���)</param>
        /// <param name="remark">����</param>
        /// <param name="updateBy">���ֲ����� �����롿</param>
        public void ProcessPoint(int sourceId, string customerId, string vipId, string objectId,System.Data.SqlClient.SqlTransaction tran=null, string fromVipId = null, decimal point = 0, string remark = null, string updateBy = null)
        {
            try
            {
                Loggers.Debug(new DebugLogInfo() { Message = string.Format("soureId: {0}, customerId: {1}, vipId: {2}, objectId: {3}, point: {4}", sourceId, customerId, vipId, objectId, point) });

                string result = "0";
                result = this._currentDAO.ProcessPoint(sourceId, customerId, vipId, objectId, tran, fromVipId, point, remark, updateBy) ?? "0";

                if (result == "0")
                    Loggers.Debug(new DebugLogInfo() { Message = string.Format("���ִ���ʧ�ܣ� soureId: {0}, customerId: {1}, vipId: {2}, objectId: {3}, point: {4}", sourceId, customerId, vipId, objectId, point) });
            }
            catch (Exception ex)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("ProcessPoint--ʧ����ʾ:{0}", ex.Message)
                });
            }
        }
        #endregion

        /// <summary>
        /// �������ʱ�������֣�����
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="userId">vipId</param>
        /// <param name="tran"></param>
        public void OrderReturnMoneyAndIntegral(string orderId, string userId, SqlTransaction tran)
        {

            #region ������
            var vipIntegralBll = new VipIntegralBLL(this.CurrentUserInfo);

            const int sourceId = 21;//����
            vipIntegralBll.ProcessPoint(sourceId, CurrentUserInfo.ClientID, userId, orderId, tran, null,
                0, null, userId);
            #endregion

            #region ����
            //1.Get All Order.skuId and Order.Qty 

            var orderDetail = new TInoutDetailBLL(this.CurrentUserInfo);

            var orderDetailList = orderDetail.QueryByEntity(new TInoutDetailEntity()
            {
                order_id = orderId
            }, null);

            if (orderDetailList == null || orderDetailList.Length == 0)
            {
                throw new APIException("�ö���û����Ʒ") { ErrorCode = 121 };
            }
            var str = orderDetailList.Aggregate("", (i, j) =>
            {
                i += string.Format("{0},{1};", j.SkuID, Convert.ToInt32(j.OrderQty));
                return i;
            });

            var bll = new VipBLL(CurrentUserInfo);
            //�����ܽ��
            var totalReturnAmount = bll.GetTotalReturnAmountBySkuId(str,tran);

            if (totalReturnAmount > 0)
            {
                //���¸����˻��Ŀ�ʹ����� 

                var vipAmountBll = new VipAmountBLL(CurrentUserInfo);

                var vipAmountEntity = vipAmountBll.GetByID(userId);

                if (vipAmountEntity == null)
                {
                    vipAmountEntity = new VipAmountEntity
                    {
                        VipId = userId,
                        BeginAmount = totalReturnAmount,
                        InAmount = totalReturnAmount,
                        EndAmount = totalReturnAmount,
                        IsLocking = 0
                    };

                    vipAmountBll.Create(vipAmountEntity, tran);


                    // throw new APIException("����δ��ͨ�����˻�") { ErrorCode = 121 };
                }
                else
                {
                    vipAmountEntity.EndAmount = vipAmountEntity.EndAmount + totalReturnAmount;
                    vipAmountEntity.InAmount = vipAmountEntity.InAmount + totalReturnAmount;

                    vipAmountBll.Update(vipAmountEntity, tran);
                }


                //Insert VipAmountDetail

                var vipamountDetailBll = new VipAmountDetailBLL(CurrentUserInfo);

                var vipAmountDetailEntity = new VipAmountDetailEntity
                {
                    AmountSourceId = "2",
                    Amount = totalReturnAmount,
                    VipAmountDetailId = Guid.NewGuid(),
                    VipId = userId,
                    ObjectId = orderId
                };

                vipamountDetailBll.Create(vipAmountDetailEntity, tran);
            }


            #endregion

        }

        /// <summary>
        /// ���ֱ��
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="points"></param>
        /// <param name="tran"></param>
        /// <param name="type"></param>
        /// <param name="objectId"></param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public bool AddIntegral(string vipId,int points, System.Data.SqlClient.SqlTransaction tran, string type, string objectId, LoggingSessionInfo loggingSessionInfo)
        {
            bool b = false;
            //���¸����˻��Ŀ�ʹ����� 
            try
            {
                var vipBLL = new VipBLL(loggingSessionInfo);
                var vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
                var vipIntegralDetailBLL = new VipIntegralDetailBLL(loggingSessionInfo);
                var vipInfo = vipBLL.GetByID(vipId);
                var vipIntegralInfo = vipIntegralBLL.GetByID(vipId);
                //�޸Ļ�Ա��Ϣʣ�����
                if (vipInfo != null)
                {
                    vipInfo.Integration=vipInfo.Integration==null?0:(vipInfo.Integration.Value+points);
                    vipBLL.Update(vipInfo, tran);
                }
                //������Ա���ּ�¼��Ϣ
                if (vipIntegralInfo == null)
                {
                    vipIntegralInfo = new VipIntegralEntity
                    {
                        VipID=vipId,
                        BeginIntegral=points,
                        InIntegral=points,
                        EndIntegral=points,
                        ValidIntegral=points
                    };
                    vipIntegralBLL.Create(vipIntegralInfo, tran);
                }
                else//�޸Ļ�Ա���ּ�¼��Ϣ
                {
                    vipIntegralInfo.EndIntegral = (vipIntegralInfo.EndIntegral == null ? 0 : vipIntegralInfo.EndIntegral.Value) + points;
                    vipIntegralInfo.ValidIntegral = (vipIntegralInfo.ValidIntegral == null ? 0 : vipIntegralInfo.ValidIntegral.Value) + points;
                    if (points > 0)
                        vipIntegralInfo.InIntegral = (vipIntegralInfo.InIntegral == null ? 0 : vipIntegralInfo.InIntegral.Value) + points;
                    else
                        vipIntegralInfo.OutIntegral = (vipIntegralInfo.OutIntegral == null ? 0 : vipIntegralInfo.OutIntegral.Value) + points;
                    vipIntegralBLL.Update(vipIntegralInfo, tran);
                }
                //���Ӽ�¼
                VipIntegralDetailEntity detail = new VipIntegralDetailEntity() { };
                detail.VipIntegralDetailID = Guid.NewGuid().ToString();
                detail.VIPID = vipId;
                detail.ObjectId = objectId;
                detail.Integral = points;
                detail.IntegralSourceID = type;
                vipIntegralDetailBLL.Create(detail,tran);
            }
            catch (Exception ex)
            {
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }
            finally
            {
                b = true;
            }

            return b;
        }
    }
}