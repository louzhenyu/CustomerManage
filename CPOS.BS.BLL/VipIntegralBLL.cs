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
using System.Collections;
using JIT.CPOS.BS.BLL.WX;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipIntegralBLL
    {
        #region Jermyn20130916 �����ն����ѣ�������֣�������Ϣ
        public bool SetPushIntegral(string orderId, string msgUrl, out string strError)
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
                        vipIntegralEntity.OutIntegral = vipIntegralDataList[0].OutIntegral + integralValue; //���ѻ���
                        vipIntegralEntity.EndIntegral = vipIntegralDataList[0].EndIntegral + integralValue; //�������
                        //vipIntegralEntity.InvalidIntegral = 0; // �ۼ�ʧЧ����
                        vipIntegralEntity.ValidIntegral = vipIntegralDataList[0].ValidIntegral + integralValue; // ��ǰ��Ч����
                        //vipIntegralBLL.Update(vipIntegralEntity, false);
                    }
                    #endregion

                    #region ����VIP
                    VipEntity vipEntity = new VipEntity();
                    var vipEntityDataList = vipBLL.QueryByEntity(
                        new VipEntity() { VIPID = vipInfo.VIPID }, null);
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
            catch (Exception ex)
            {
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
        public void ProcessPoint(int sourceId, string customerId, string vipId, string objectId, System.Data.SqlClient.SqlTransaction tran = null, string fromVipId = null, decimal point = 0, string remark = null, string updateBy = null)
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
        /// �������ʱ�������֣����� Willie Yan
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
            var totalReturnAmount = bll.GetTotalReturnAmountBySkuId(str, tran);

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
        /// ȷ���ջ�ʱ������֡����֡�Ӷ��
        /// </summary>
        /// <param name="orderInfo">������Ϣ</param>
        /// <param name="tran">����</param>
        /// <param name="dataFromId">16=��ԱС��;17=Ա��С��;3=΢�̳��µ�</param>
        public void OrderReward(T_InoutEntity orderInfo, SqlTransaction tran)
        {
            var vipBll = new VipBLL(this.CurrentUserInfo);
            var unitBLL = new t_unitBLL(CurrentUserInfo);
            var basicSettingBll = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            var vipAmountBll = new VipAmountBLL(CurrentUserInfo);

            var vipCardVipMappingBLL = new VipCardVipMappingBLL(CurrentUserInfo);
            var vipCardBLL = new VipCardBLL(CurrentUserInfo);
            var vipCardRuleBLL = new VipCardRuleBLL(CurrentUserInfo);

            var userBLL = new T_UserBLL(CurrentUserInfo);

            //��ȡ��Ա��Ϣ
            var vipInfo = vipBll.GetByID(orderInfo.vip_no);
            //��ȡ�ŵ���Ϣ
            t_unitEntity unitInfo = null;
            if (!string.IsNullOrEmpty(orderInfo.sales_unit_id))
                unitInfo = unitBLL.GetByID(orderInfo.sales_unit_id);

            //��ȡ��ữ�������úͻ��ַ�������
            Hashtable htSetting = basicSettingBll.GetSocialSetting();

            //��ȡ��������Ķһ�����
            var integralAmountPre = vipBll.GetIntegralAmountPre(this.CurrentUserInfo.ClientID);
            if (integralAmountPre == 0)
                integralAmountPre = (decimal)0.01;

            decimal actualAmount = orderInfo.actual_amount ?? 0;    //ʵ�����
            decimal deliveryAmount = orderInfo.DeliveryAmount;      //�˷�

            actualAmount = actualAmount - deliveryAmount;           //ʵ�����-�˷�

            //�˻����ͷ���
            var vipAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();

            #region ��ȡ������

            VipCardRuleEntity vipCardRuleInfo = null;
            var vipCardMappingInfo = vipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VIPID = vipInfo.VIPID }, null).FirstOrDefault();
            if (vipCardMappingInfo != null)
            {
                var vipCardInfo = vipCardBLL.QueryByEntity(new VipCardEntity() { VipCardID = vipCardMappingInfo.VipCardID }, null).FirstOrDefault();
                if (vipCardInfo != null)
                {
                    vipCardRuleInfo = vipCardRuleBLL.QueryByEntity(new VipCardRuleEntity() { VipCardTypeID = vipCardInfo.VipCardTypeID }, null).FirstOrDefault();
                }
            }
            #endregion

            #region ������ update by Henry 2015-4-17

            if (int.Parse(htSetting["enableIntegral"].ToString()) == 1)
            {
                var vipIntegralBll = new VipIntegralBLL(this.CurrentUserInfo);
                var vipIntegralDetailBll = new VipIntegralDetailBLL(this.CurrentUserInfo);
                if (int.Parse(htSetting["rewardsType"].ToString()) == 1)//����Ʒ[�ݲ�֧��]
                {
                    //const int sourceId = 21;//����
                    //vipIntegralBll.ProcessPoint(sourceId, CurrentUserInfo.ClientID, userId, orderId, tran, null,0, null, userId);
                }
                else//������
                {
                    if (vipCardRuleInfo != null)
                    {
                        decimal paidGivePoints = vipCardRuleInfo.PaidGivePoints != null ? vipCardRuleInfo.PaidGivePoints.Value : 0;
                        if (paidGivePoints > 0)
                        {
                            //int points = (int)Math.Round(actualAmount * (decimal.Parse(htSetting["rewardPointsPer"].ToString()) / 100) / integralAmountPre, 1);
                            int points = (int)Math.Round(actualAmount / paidGivePoints, 1);
                            int pointsOrderUpLimit = int.Parse(htSetting["pointsOrderUpLimit"].ToString());
                            if (pointsOrderUpLimit > 0)
                                points = points > pointsOrderUpLimit ? pointsOrderUpLimit : points; //����ÿ�����ͻ�������
                            if (points > 0)
                            {
                                //���ֱ��
                                var IntegralDetail = new VipIntegralDetailEntity()
                                {
                                    Integral = points,
                                    IntegralSourceID = "1",
                                    ObjectId = orderInfo.order_id
                                };
                                //�䶯ǰ����
                                string OldIntegral = (vipInfo.Integration ?? 0).ToString();
                                //�䶯����
                                string ChangeIntegral = (IntegralDetail.Integral ?? 0).ToString();
                                String vipIntegralDetailId = this.AddIntegral(ref vipInfo, unitInfo,IntegralDetail, tran, this.CurrentUserInfo);
                                //����΢�Ż��ֱ䶯֪ͨģ����Ϣ
                                if (!string.IsNullOrWhiteSpace(vipIntegralDetailId))
                                {
                                    var CommonBLL = new CommonBLL();
                                    CommonBLL.PointsChangeMessage(OldIntegral, vipInfo, ChangeIntegral, vipInfo.WeiXinUserId, this.CurrentUserInfo);
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region ����

            if (int.Parse(htSetting["enableRewardCash"].ToString()) == 1)
            {
                if (vipCardRuleInfo != null)
                {
                    decimal returnAmountPer = vipCardRuleInfo.ReturnAmountPer != null ? vipCardRuleInfo.ReturnAmountPer.Value : 0;
                    if (returnAmountPer > 0)
                    {
                        var orderDetail = new TInoutDetailBLL(this.CurrentUserInfo);
                        var orderDetailList = orderDetail.QueryByEntity(new TInoutDetailEntity() { order_id = orderInfo.order_id }, null);

                        if (orderDetailList == null || orderDetailList.Length == 0)
                        {
                            throw new APIException("�ö���û����Ʒ") { ErrorCode = 121 };
                        }
                        var str = orderDetailList.Aggregate("", (i, j) =>
                        {
                            i += string.Format("{0},{1};", j.SkuID, Convert.ToInt32(j.OrderQty));
                            return i;
                        });

                        decimal totalReturnAmount = 0;//�����ܽ��

                        if (int.Parse(htSetting["rewardsType"].ToString()) == 1)//����Ʒ[�ݲ�֧��]
                            totalReturnAmount = vipBll.GetTotalReturnAmountBySkuId(str, tran);
                        else//������
                            totalReturnAmount = actualAmount * (returnAmountPer / 100);
                        //totalReturnAmount = actualAmount * (decimal.Parse(htSetting["rewardCashPer"].ToString()) / 100);

                        decimal cashOrderUpLimit = int.Parse(htSetting["cashOrderUpLimit"].ToString());
                        if (cashOrderUpLimit > 0)
                            totalReturnAmount = totalReturnAmount > cashOrderUpLimit ? cashOrderUpLimit : totalReturnAmount; //����ÿ����������

                        if (totalReturnAmount > 0)
                        {
                            //���¸����˻��Ŀ�ʹ����� 
                            var detailInfo = new VipAmountDetailEntity()
                            {
                                Amount = totalReturnAmount,
                                ObjectId = orderInfo.order_id,
                                AmountSourceId = "2"
                            };
                            var vipAmountDetailId= vipAmountBll.AddReturnAmount(vipInfo, unitInfo, vipAmountEntity,ref detailInfo, tran, CurrentUserInfo);
                            if (!string.IsNullOrWhiteSpace(vipAmountDetailId))
                            {//���ͷ��ֵ���֪ͨ΢��ģ����Ϣ
                                var CommonBLL = new CommonBLL();
                                CommonBLL.CashBackMessage(orderInfo.order_no, detailInfo.Amount, vipInfo.WeiXinUserId, vipInfo.VIPID, CurrentUserInfo);

                            }
                        }
                    }
                }
            }
            #endregion

            #region Ӷ���� add by Henry 2015-6-10

            decimal totalAmount = 0; //������Ӷ��
            if (int.Parse(htSetting["socialSalesType"].ToString()) == 1)     //����Ʒ���ü���
            {
                //ȷ���ջ�ʱ�����������(sales_user)��Ϊ��,����ƷӶ��*������������浽������
                if (!string.IsNullOrEmpty(orderInfo.sales_user))
                {
                    var skuPriceBll = new SkuPriceService(this.CurrentUserInfo);              //sku�۸�
                    var inoutService = new InoutService(this.CurrentUserInfo);
                    List<OrderDetail> orderDetailList = skuPriceBll.GetSkuPrice(orderInfo.order_id);
                    if (orderDetailList.Count > 0)
                    {
                        foreach (var detail in orderDetailList)
                        {
                            totalAmount += decimal.Parse(detail.salesPrice) * decimal.Parse(detail.qty);
                        }
                    }
                }
            }
            else//���������
            {
                if (orderInfo.data_from_id == "16")     //��ԱС��
                {
                    if (int.Parse(htSetting["enableVipSales"].ToString()) > 0)//���û�ԱС��
                        totalAmount += actualAmount * (decimal.Parse(htSetting["vOrderCommissionPer"].ToString()) / 100);

                    if (totalAmount > 0)
                    {
                        var detailInfo = new VipAmountDetailEntity()
                        {
                            Amount = totalAmount,
                            AmountSourceId = "10",
                            ObjectId = orderInfo.order_id
                        };

                        var vipSalesVipInfo = vipBll.GetByID(orderInfo.sales_user);
                        //�˻����ͷ���
                        var vipSalesAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipSalesVipInfo.VIPID, VipCardCode = vipSalesVipInfo.VipCode }, null).FirstOrDefault();
                        var vipAmountDetailId = vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipSalesAmountEntity, detailInfo, tran, this.CurrentUserInfo);
                        if (!string.IsNullOrWhiteSpace(vipAmountDetailId) && orderInfo.data_from_id == "16")
                        {//����΢���˻����䶯ģ����Ϣ

                            var CommonBLL = new CommonBLL();
                            CommonBLL.BalanceChangedMessage(orderInfo.order_no, vipSalesAmountEntity, detailInfo, vipSalesVipInfo.WeiXinUserId, orderInfo.vip_no, this.CurrentUserInfo);
                        }
                    }

                }
                else if (orderInfo.data_from_id == "17") //Ա��С��
                {
                    if (int.Parse(htSetting["enableEmployeeSales"].ToString()) > 0)//����Ա��С��
                        totalAmount += actualAmount * (decimal.Parse(htSetting["eOrderCommissionPer"].ToString()) / 100);

                    if (totalAmount > 0)
                    {
                        var detailInfo = new VipAmountDetailEntity()
                        {
                            Amount = totalAmount,
                            AmountSourceId = "10",
                            ObjectId = orderInfo.order_id
                        };

                        var employeeSalesUserInfo= userBLL.GetByID(orderInfo.sales_user);
                        vipInfo.VIPID = employeeSalesUserInfo.user_id;
                        vipInfo.VipCode = employeeSalesUserInfo.user_code;
                        //�˻����ͷ���
                        var vipSalesAmountEntity = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = employeeSalesUserInfo.user_id }, null).FirstOrDefault();
                        vipAmountBll.AddVipAmount(vipInfo, unitInfo, ref vipSalesAmountEntity, detailInfo, tran, this.CurrentUserInfo);
                    }

                }
            }
            #endregion
        }
        /// <summary>
        /// �˻���-ȷ���ջ�ʱ�˻ض����������֡����ֺ�Ӷ��
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <param name="tran"></param>
        public void CancelReward(T_InoutEntity orderInfo, VipEntity vipInfo, SqlTransaction tran)
        {
            //ȡ����������
            var vipIntegralBll = new VipIntegralBLL(this.CurrentUserInfo);
            var vipIntegralDetailBll = new VipIntegralDetailBLL(this.CurrentUserInfo);

            var integralDetailInfo = vipIntegralDetailBll.QueryByEntity(new VipIntegralDetailEntity() { VIPID = orderInfo.vip_no, ObjectId = orderInfo.order_id, IntegralSourceID = "1" }, null).FirstOrDefault();
            if (integralDetailInfo != null)
            {
                var vipIntegralInfo = vipIntegralBll.QueryByEntity(new VipIntegralEntity() { VipID = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();

                vipIntegralInfo.InIntegral -= integralDetailInfo.Integral;
                vipIntegralInfo.EndIntegral -= integralDetailInfo.Integral;
                vipIntegralInfo.ValidIntegral -= integralDetailInfo.Integral;
                vipIntegralInfo.CumulativeIntegral -= integralDetailInfo.Integral;
                vipIntegralBll.Update(vipIntegralInfo, tran);

                vipIntegralDetailBll.Delete(integralDetailInfo, tran);
            }
            //ȡ����������
            var vipAmountBll = new VipAmountBLL(this.CurrentUserInfo);
            var vipAmountDetailBll = new VipAmountDetailBLL(this.CurrentUserInfo);

            var vipAmountDetailInfo = vipAmountDetailBll.QueryByEntity(new VipAmountDetailEntity() { VipId = orderInfo.vip_no, ObjectId = orderInfo.order_id, AmountSourceId = "2" }, null).FirstOrDefault();
            if (vipAmountDetailInfo != null)
            {
                var vipAmountInfo = vipAmountBll.QueryByEntity(new VipAmountEntity() { VipId = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();

                vipAmountInfo.InReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountInfo.ReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountInfo.ValidReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountInfo.TotalReturnAmount -= vipAmountDetailInfo.Amount;
                vipAmountBll.Update(vipAmountInfo, tran);

                vipAmountDetailBll.Delete(vipAmountDetailInfo, tran);
            }
            //ȡ������Ӷ��
        }
        /// <summary>
        /// ���ֱ��
        /// </summary>
        /// <param name="vipInfo">��Ա��Ϣ</param>
        /// <param name="unitInfo">�ŵ���Ϣ</param>
        /// <param name="detailInfo">�����ϸ��Ϣ</param>
        /// <param name="tran">����</param>
        /// <param name="loggingSessionInfo">��¼��Ϣ</param>
        /// <returns></returns>
        public string AddIntegral(ref VipEntity vipInfo, t_unitEntity unitInfo,VipIntegralDetailEntity detailInfo, SqlTransaction tran, LoggingSessionInfo loggingSessionInfo)
        {
            string vipIntegralDetailId = string.Empty;//�����ϸID
            //���¸����˻��Ŀ�ʹ����� 
            try
            {
                var vipBLL = new VipBLL(loggingSessionInfo);
                var vipIntegralBLL = new VipIntegralBLL(loggingSessionInfo);
                var vipIntegralDetailBLL = new VipIntegralDetailBLL(loggingSessionInfo);
                var customerBasicSettingBLL = new CustomerBasicSettingBLL(loggingSessionInfo);

                //��ȡ������Ч��
                int pointsValidPeriod = 2;  //Ĭ��Ϊ1��ҵ����ʱ���ȥ1
                var pointsValidPeriodInfo = customerBasicSettingBLL.QueryByEntity(new CustomerBasicSettingEntity() { SettingCode = "PointsValidPeriod", CustomerID = loggingSessionInfo.ClientID }, null).FirstOrDefault();
                if (pointsValidPeriodInfo != null)
                    pointsValidPeriod = int.Parse(pointsValidPeriodInfo.SettingValue);

                //��ȡ��Ա����������Ϣ
                //var vipIntegralInfo = vipIntegralBLL.GetByID(vipId);
                var vipIntegralInfo = vipIntegralBLL.QueryByEntity(new VipIntegralEntity() { VipID = vipInfo.VIPID, VipCardCode = vipInfo.VipCode }, null).FirstOrDefault();
                //�޸Ļ�Ա��Ϣʣ�����
                if (vipInfo != null)
                {
                    vipInfo.Integration = (vipInfo.Integration == null ? 0 : vipInfo.Integration.Value) + detailInfo.Integral;
                    vipBLL.Update(vipInfo, tran);
                }
                //������Ա���ּ�¼��Ϣ
                if (vipIntegralInfo == null)
                {
                    vipIntegralInfo = new VipIntegralEntity
                    {
                        VipID = vipInfo.VIPID,
                        VipCardCode = vipInfo.VipCode,
                        BeginIntegral = 0,
                        InIntegral = detailInfo.Integral,
                        OutIntegral = 0,
                        EndIntegral = detailInfo.Integral,
                        ImminentInvalidIntegral = 0,
                        InvalidIntegral = 0,
                        ValidIntegral = detailInfo.Integral,
                        CumulativeIntegral = detailInfo.Integral,
                        CustomerID = loggingSessionInfo.ClientID
                    };
                    vipIntegralBLL.Create(vipIntegralInfo, tran);
                }
                else//�޸Ļ�Ա���ּ�¼��Ϣ
                {
                    if (detailInfo.Integral > 0)
                    {
                        vipIntegralInfo.InIntegral = (vipIntegralInfo.InIntegral == null ? 0 : vipIntegralInfo.InIntegral.Value) + detailInfo.Integral;
                        vipIntegralInfo.CumulativeIntegral = (vipIntegralInfo.CumulativeIntegral == null ? 0 : vipIntegralInfo.CumulativeIntegral.Value) + detailInfo.Integral;
                    }
                    else
                        vipIntegralInfo.OutIntegral = (vipIntegralInfo.OutIntegral == null ? 0 : vipIntegralInfo.OutIntegral.Value) + Math.Abs(detailInfo.Integral.Value);
                    vipIntegralInfo.EndIntegral = (vipIntegralInfo.EndIntegral == null ? 0 : vipIntegralInfo.EndIntegral.Value) + detailInfo.Integral;
                    vipIntegralInfo.ValidIntegral = (vipIntegralInfo.ValidIntegral == null ? 0 : vipIntegralInfo.ValidIntegral.Value) + detailInfo.Integral;

                    vipIntegralBLL.Update(vipIntegralInfo, tran);
                }
                //���Ӽ�¼
                VipIntegralDetailEntity detail = new VipIntegralDetailEntity() { };
                detail.VipIntegralDetailID = Guid.NewGuid().ToString();
                detail.VIPID = vipInfo.VIPID;
                detail.VipCardCode = vipInfo.VipCode;
                detail.UnitID = unitInfo != null ? unitInfo.unit_id : "";
                detail.UnitName = unitInfo != null ? unitInfo.unit_name : "";
                detail.ObjectId = detailInfo.ObjectId;
                detail.Integral = detailInfo.Integral;
                detail.UsedIntegral = 0;
                detail.IntegralSourceID = detailInfo.IntegralSourceID;
                detail.Reason = detailInfo.Reason;
                detail.Remark = detailInfo.Remark;
                detail.EffectiveDate = DateTime.Now;
                detail.DeadlineDate = Convert.ToDateTime((DateTime.Now.Year + pointsValidPeriod - 1) + "-12-31 23:59:59 ");//ʧЧʱ��
                detail.CustomerID = loggingSessionInfo.ClientID;
                vipIntegralDetailBLL.Create(detail, tran);

                vipIntegralDetailId = detail.VipIntegralDetailID;
            }
            catch (Exception ex)
            {
                throw new APIException(ex.ToString()) { ErrorCode = 121 };
            }
            return vipIntegralDetailId;
        }
    }
}