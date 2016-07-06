/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/14 9:46:51
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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using System.Configuration;
using JIT.Utility.Web;
using JIT.Utility.Log;
using System.Data.SqlClient;
using JIT.CPOS.BS.Entity.WX;
using System.Transactions;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.DTO.Module.Report.VipReport.Response;
using JIT.CPOS.DTO.Module.VIP.Dealer.Response;
using CPOS.Common;
using CPOS.BS.BLL;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class VipBLL
    {

        #region ��Ա���
        /// <summary>
        /// ���ݻ�Ա��Դ��ȡע���Ա����Ӫ��������Դ
        /// </summary>
        /// <param name="VipSourceID"></param>
        /// <returns></returns>
        public string GetSourceByVipSourceID(string VipSourceID)
        {
            string AmountSourceID = "5";

            if (VipSourceID.Equals("3")||VipSourceID.Equals("13"))    //����΢�ŵĻ�Ա
                AmountSourceID = "35";      //Ա������ע�ά��
            else if (VipSourceID.Equals("22") || VipSourceID.Equals("24") || VipSourceID.Equals("25"))
                AmountSourceID = "29";
            else if (VipSourceID.Equals("26") || VipSourceID.Equals("27"))
                AmountSourceID = "30";
            else if (VipSourceID.Equals("28") || VipSourceID.Equals("29"))
                AmountSourceID = "31";
            else if (VipSourceID.Equals("30") || VipSourceID.Equals("31"))
                AmountSourceID = "32";


            return AmountSourceID;
        }
        /// <summary>
        /// �������۽���
        /// </summary>
        public void SetOffActionSales(VipEntity vipInfo, T_InoutEntity orderInfo)
        {
            decimal actualAmount = orderInfo.actual_amount ?? 0;    //ʵ�����
            decimal deliveryAmount = orderInfo.DeliveryAmount;      //�˷�

            actualAmount = actualAmount - deliveryAmount;           //ʵ�����-�˷�

            //�����ж�
            var SetoffEvent = new SetoffEventBLL(CurrentUserInfo).QueryByEntity(new SetoffEventEntity() { Status = "10", CustomerId = CurrentUserInfo.ClientID }, null).ToList();
            if (SetoffEvent.Count > 0)
            {
                foreach (var item in SetoffEvent)
                {
                    //���ͽ�������
                    var IincentiveRuleData = new IincentiveRuleBLL(CurrentUserInfo).QueryByEntity(new IincentiveRuleEntity() { Status = "10", SetoffEventID = item.SetoffEventID, CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                    decimal ResultPer = IincentiveRuleData.SetoffOrderPer ?? 0;
                    if (IincentiveRuleData != null)
                    {
                        if (item.SetoffType == 1)
                        {
                            if (!string.IsNullOrEmpty(vipInfo.HigherVipID))
                            {
                                #region ��Ա����
                                //��ȡ�����Ļ�Ա
                                var VipData = this._currentDAO.GetByID(vipInfo.HigherVipID);
                                switch (IincentiveRuleData.SetoffRegAwardType)
                                {
                                    case 1:
                                        #region �ֽ���
                                        

                                        if (ResultPer > 0)
                                        {
                                            //�����Ľ������
                                            decimal ResultMonery = (actualAmount * ResultPer) / 100;

                                            if (IincentiveRuleData.SetoffOrderTimers == 0)
                                            {
                                                #region ������Ч
                                                AddVipGoldAmount(ResultMonery, vipInfo.HigherVipID, orderInfo.order_id, "20", "�����ж����۽���", VipData.VipCode);
                                                #endregion
                                            }
                                            if (IincentiveRuleData.SetoffOrderTimers == 1)
                                            {
                                                #region �׵���Ч
                                                var OrderResult = new T_InoutBLL(CurrentUserInfo).QueryByEntity(new T_InoutEntity() { vip_no = orderInfo.vip_no, status = "700" }, null).ToList();
                                                if (OrderResult.Count() > 0)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    AddVipGoldAmount(ResultMonery, vipInfo.HigherVipID, orderInfo.order_id, "20", "�����ж����۽���", VipData.VipCode);
                                                }
                                                #endregion
                                            }
                                        }
                                        #endregion

                                        break;
                                    case 2:
                                        #region ���ֽ���
                                        if (ResultPer > 0)
                                        {
                                            int ResultIntergral = Convert.ToInt32((actualAmount * ResultPer) / 100);

                                            if (IincentiveRuleData.SetoffOrderTimers == 0)
                                            {//������Ч
                                                AddVipGoldIntegral(ResultIntergral, vipInfo.HigherVipID, orderInfo.order_id, "25", "�����ж����۽���",VipData.VipCode);
                                            }
                                            if (IincentiveRuleData.SetoffOrderTimers == 1)
                                            {//�׵���Ч
                                                var OrderResult = new T_InoutBLL(CurrentUserInfo).QueryByEntity(new T_InoutEntity() { vip_no = orderInfo.vip_no, status = "700" }, null).ToList();
                                                if (OrderResult.Count() > 0)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    AddVipGoldIntegral(ResultIntergral, vipInfo.HigherVipID, orderInfo.order_id, "25", "�����ж����۽���", VipData.VipCode);
                                                }
                                            }
                                        }
                                        #endregion
                                        break;
                                }
                                #endregion

                            }
                        }
                        else if (item.SetoffType == 2)
                        {
                            if (!string.IsNullOrEmpty(vipInfo.SetoffUserId))
                            {
                                #region Ա������
                                #region �ֽ���

                                if (ResultPer > 0)
                                {
                                    //�����Ľ������
                                    decimal ResultMonery = (actualAmount * ResultPer) / 100;

                                    if (IincentiveRuleData.SetoffOrderTimers == 0)
                                    {
                                        #region ������Ч
                                        AddVipGoldAmount(ResultMonery, vipInfo.SetoffUserId, orderInfo.order_id, "20", "�����ж����۽���", "");
                                        #endregion
                                    }
                                    if (IincentiveRuleData.SetoffOrderTimers == 1)
                                    {
                                        #region �׵���Ч
                                        var OrderResult = new T_InoutBLL(CurrentUserInfo).QueryByEntity(new T_InoutEntity() { vip_no = orderInfo.vip_no, status = "700" }, null).ToList();
                                        if (OrderResult.Count() > 0)
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            AddVipGoldAmount(ResultMonery, vipInfo.SetoffUserId, orderInfo.order_id, "20", "�����ж����۽���", "");
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                                #endregion
                            }
                        }
                    }
                }

            }
        }
        /// <summary>
        /// ����ע�ά�� 
        /// </summary>
        public void SetOffActionReg(VipEntity vipInfo)
        {
            //��ȡ������Դ
            string SysAmountSourceID = GetSourceByVipSourceID(vipInfo.VipSourceId);
            //�����ж�
            var SetoffEvent = new SetoffEventBLL(CurrentUserInfo).QueryByEntity(new SetoffEventEntity() { Status = "10", CustomerId = CurrentUserInfo.ClientID }, null).ToList();
            if (SetoffEvent.Count > 0)
            {
                foreach (var item in SetoffEvent)
                {
                    //���ͽ�������
                    var IincentiveRuleData = new IincentiveRuleBLL(CurrentUserInfo).QueryByEntity(new IincentiveRuleEntity() { Status = "10", SetoffEventID = item.SetoffEventID, CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
                    if (IincentiveRuleData != null)
                    {
                        //����ע�ά��
                        int SetoffRegPrize = Convert.ToInt32(IincentiveRuleData.SetoffRegPrize ?? 0);
                        if (item.SetoffType == 1)
                        {
                            if (!string.IsNullOrEmpty(vipInfo.HigherVipID))
                            {
                                #region ��Ա����
                                //��ȡ�����Ļ�Ա
                                var VipData = this._currentDAO.GetByID(vipInfo.HigherVipID);
                                switch (IincentiveRuleData.SetoffRegAwardType)
                                {
                                    case 1:
                                        #region �ֽ���
                                        AddVipGoldAmount(SetoffRegPrize, vipInfo.HigherVipID, vipInfo.VIPID, SysAmountSourceID, "�����ж�ע�ά��", VipData.VipCode);
                                        #endregion

                                        break;
                                    case 2:
                                        #region ���ֽ���
                                        AddVipGoldIntegral(SetoffRegPrize, vipInfo.HigherVipID, vipInfo.VIPID, SysAmountSourceID, "�����ж�ע�ά��", VipData.VipCode);
                                        #endregion
                                        break;
                                }
                                #endregion

                            }
                        }
                        else if (item.SetoffType == 2)
                        {
                            if (!string.IsNullOrEmpty(vipInfo.SetoffUserId))
                            {
                                #region Ա������
                                AddVipGoldAmount(SetoffRegPrize, vipInfo.SetoffUserId, vipInfo.VIPID, SysAmountSourceID, "�����ж�ע�ά��", "");
                                #endregion

                            }
                        }
                    }
                }

            }
        }
        /// <summary>
        /// ��Ա����ֽ���
        /// </summary>
        /// <param name="ResultMonery"></param>
        /// <param name="HigherVipID"></param>
        /// <param name="OrderId"></param>
        private void AddVipGoldAmount(decimal ResultMonery, string HigherVipID, string ObjectId, string SysAmountSourceID, string Remark,string VipCode)
        {
            var vipAmountBll = new VipAmountBLL(CurrentUserInfo);

            //��ϸ
            var VipAmountDetail = new VipAmountDetailEntity();
            VipAmountDetail.VipAmountDetailId = System.Guid.NewGuid();
            VipAmountDetail.VipId = HigherVipID;
            VipAmountDetail.Amount = ResultMonery;
            VipAmountDetail.Reason = Remark;
            VipAmountDetail.AmountSourceId = SysAmountSourceID;
            VipAmountDetail.ObjectId = ObjectId;
            VipAmountDetail.Remark = Remark;
            VipAmountDetail.IsValid = 1;
            VipAmountDetail.IsWithdrawCash = 1;
            VipAmountDetail.CustomerID = CurrentUserInfo.ClientID;
            VipAmountDetail.UsedReturnAmount = 0;
            new VipAmountDetailBLL(CurrentUserInfo).Create(VipAmountDetail);

            var VipAmount = vipAmountBll.GetByID(HigherVipID);
            if (VipAmount != null)
            {
                //����
                VipAmount.ReturnAmount = VipAmount.ReturnAmount ?? 0;
                VipAmount.ReturnAmount += ResultMonery;//���
                VipAmount.ValidReturnAmount = VipAmount.ValidReturnAmount ?? 0;
                VipAmount.ValidReturnAmount += ResultMonery;//�ۼƽ��
                VipAmount.TotalReturnAmount = VipAmount.TotalReturnAmount ?? 0;
                VipAmount.TotalReturnAmount += ResultMonery;//�ۼƽ��
                VipAmount.VipCardCode = VipCode;
                vipAmountBll.Update(VipAmount);
            }
            else
            {
                //����
                var NewVipAmountData = new VipAmountEntity();
                NewVipAmountData.VipId = HigherVipID;
                NewVipAmountData.VipCardCode = VipCode;
                NewVipAmountData.BeginAmount = 0;
                NewVipAmountData.InAmount = 0;
                NewVipAmountData.OutAmount = 0;
                NewVipAmountData.EndAmount = 0;
                NewVipAmountData.TotalAmount = 0;
                NewVipAmountData.BeginReturnAmount = ResultMonery;
                NewVipAmountData.InReturnAmount = ResultMonery;
                NewVipAmountData.OutReturnAmount = 0;
                NewVipAmountData.ReturnAmount = ResultMonery;
                NewVipAmountData.ImminentInvalidRAmount = 0;
                NewVipAmountData.InvalidReturnAmount = 0;
                NewVipAmountData.ValidReturnAmount = ResultMonery;
                NewVipAmountData.TotalReturnAmount = ResultMonery;
                NewVipAmountData.PayPassword = "";
                NewVipAmountData.IsLocking = 0;
                NewVipAmountData.CustomerID = CurrentUserInfo.ClientID;
                vipAmountBll.Create(NewVipAmountData);//
            }
        }
        /// <summary>
        /// ��Ա�����ֽ���
        /// </summary>
        /// <param name="ResultIntegral"></param>
        /// <param name="HigherVipID"></param>
        /// <param name="OrderId"></param>
        private void AddVipGoldIntegral(int ResultIntegral, string HigherVipID, string ObjectId, string SysIntergralSourceID, string Remark, string VipCode)
        {
            var vipBLL = new VipBLL(CurrentUserInfo);
            var VipIntegralDeatil = new VipIntegralDetailEntity();
            VipIntegralDeatil.VipIntegralDetailID = System.Guid.NewGuid().ToString();
            VipIntegralDeatil.VIPID = HigherVipID;
            VipIntegralDeatil.VipCardCode = VipCode;
            VipIntegralDeatil.Integral = ResultIntegral;
            VipIntegralDeatil.UsedIntegral = ResultIntegral;
            VipIntegralDeatil.Reason = Remark;
            VipIntegralDeatil.IntegralSourceID = SysIntergralSourceID;
            VipIntegralDeatil.IsAdd = 1;
            VipIntegralDeatil.ObjectId = ObjectId;
            VipIntegralDeatil.Remark = Remark;
            VipIntegralDeatil.CustomerID = CurrentUserInfo.ClientID;
            //new VipIntegralDetailBLL(CurrentUserInfo).Create(VipIntegralDeatil);//
            var vipinfo = vipBLL.QueryByEntity(new VipEntity() { ClientID=CurrentUserInfo.ClientID,VIPID=HigherVipID},null).FirstOrDefault();
            var VipIntegral = new VipIntegralBLL(CurrentUserInfo).GetByID(HigherVipID); //vipAmountBll.GetByID(HigherVipID);
            if (VipIntegral != null)
            {
                //����
                VipIntegral.InIntegral = VipIntegral.InIntegral ?? 0;
                VipIntegral.EndIntegral = VipIntegral.EndIntegral ?? 0;
                VipIntegral.ValidIntegral = VipIntegral.ValidIntegral ?? 0;
                VipIntegral.CumulativeIntegral = VipIntegral.CumulativeIntegral ?? 0;
                //��ֵ
                VipIntegral.InIntegral += ResultIntegral;
                VipIntegral.EndIntegral += ResultIntegral;
                VipIntegral.ValidIntegral += ResultIntegral;
                VipIntegral.CumulativeIntegral += ResultIntegral;
                VipIntegral.VipCardCode = VipCode;
                //new VipIntegralBLL(CurrentUserInfo).Update(VipIntegral);//
            }
            else
            {
                var NewVipIntegral = new VipIntegralEntity();
                NewVipIntegral.VipID = HigherVipID;
                NewVipIntegral.VipCardCode = VipCode;
                NewVipIntegral.BeginIntegral = 0;
                NewVipIntegral.InIntegral = ResultIntegral;
                NewVipIntegral.OutIntegral = 0;
                NewVipIntegral.EndIntegral = ResultIntegral;
                NewVipIntegral.ImminentInvalidIntegral = 0;
                NewVipIntegral.InvalidIntegral = 0;
                NewVipIntegral.ValidIntegral = ResultIntegral;
                NewVipIntegral.CumulativeIntegral = ResultIntegral;
                NewVipIntegral.ValidNotIntegral = 0;
                NewVipIntegral.CustomerID = CurrentUserInfo.ClientID;
                //new VipIntegralBLL(CurrentUserInfo).Create(NewVipIntegral);//
            }
            var VipIntegralbll = new VipIntegralBLL(CurrentUserInfo);
            VipIntegralbll.AddIntegral(ref vipinfo, null, VipIntegralDeatil,null,CurrentUserInfo);
        }

        #endregion

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipEntity NewGetByID(object pID)
        {
            return _currentDAO.NewGetByID(pID);
        }

        #region �ȹ�����
        /// <summary>
        /// �жϻ�Ա�Ƿ���Գ�Ϊ������
        /// </summary>
        /// <param name="VipID"></param>
        /// <returns></returns>
        public bool IsSetVipDealer(string VipID)
        {
            bool Flag = false;

            //��ȡ��Ϊ�����������׼�
            string Dsql = string.Format("select MustBuyAmount from T_VipMultiLevelSalerConfig where IsDelete=0 and CustomerId='{0}'", CurrentUserInfo.ClientID);
            decimal MustBuyAmount = this._currentDAO.GetAmount(Dsql);

            if (!string.IsNullOrWhiteSpace(VipID))
            {
                string sql = string.Format(@"
                                            SELECT c.Prices FROM VipCardVipMapping as a 
                                            inner join VipCard as b on a.vipcardid=b.VipCardID and b.IsDelete =0 and b.VipCardStatusId=1 
                                            left join SysVipCardType as c on b.VipCardTypeID=c.VipCardTypeID and c.IsDelete=0 
                                            where a.IsDelete=0 and a.CustomerId='{0}' and a.VIPID='{1}'", CurrentUserInfo.ClientID, VipID);
                decimal Prices = this._currentDAO.GetAmount(sql);

                if (MustBuyAmount > 0)
                    if (Prices >= MustBuyAmount)
                        Flag = true;
            }

            return Flag;
        }
        /// <summary>
        /// ��ȡ��Ա��Ϊ�����̵׼�
        /// </summary>
        /// <returns></returns>
        public Decimal GetSetVipDealerUpset()
        {
            //��ȡ��Ϊ�����������׼�
            string Dsql = string.Format("select MustBuyAmount from T_VipMultiLevelSalerConfig where IsDelete=0 and CustomerId='{0}'", CurrentUserInfo.ClientID);
            return this._currentDAO.GetAmount(Dsql);
        }
        /// <summary>
        /// ��ȡ��Ա�������˻���ϸ
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public GetVipDealerAccountDetailRD GetVipDealerAccountDetail(string VipId)
        {
            var ReturenData = new GetVipDealerAccountDetailRD();

            DataSet ds = this._currentDAO.GetVipDealerAccountDetail(VipId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ReturenData.VipID = VipId;
                if (ds.Tables[0].Rows[0]["DayIncome"] != DBNull.Value)
                    ReturenData.DayIncome = ds.Tables[0].Rows[0]["DayIncome"].ToString();
                else
                    ReturenData.DayIncome = "0.00";

                if (ds.Tables[0].Rows[0]["DayFans"] != DBNull.Value)
                    ReturenData.DayFans = ds.Tables[0].Rows[0]["DayFans"].ToString();
                else
                    ReturenData.DayFans = "0";

                if (ds.Tables[0].Rows[0]["MounthIncome"] != DBNull.Value)
                    ReturenData.MounthIncome = ds.Tables[0].Rows[0]["MounthIncome"].ToString();
                else
                    ReturenData.MounthIncome = "0.00";

                if (ds.Tables[0].Rows[0]["SumIncome"] != DBNull.Value)
                    ReturenData.SumIncome = ds.Tables[0].Rows[0]["SumIncome"].ToString();
                else
                    ReturenData.SumIncome = "0.00";

                if (ds.Tables[0].Rows[0]["MounthFans"] != DBNull.Value)
                    ReturenData.MounthFans = ds.Tables[0].Rows[0]["MounthFans"].ToString();
                else
                    ReturenData.MounthFans = "0";

                if (ds.Tables[0].Rows[0]["SumFans"] != DBNull.Value)
                    ReturenData.SumFans = ds.Tables[0].Rows[0]["SumFans"].ToString();
                else
                    ReturenData.SumFans = "0";

                if (ds.Tables[0].Rows[0]["WithdrawSumMoney"] != DBNull.Value)
                    ReturenData.WithdrawSumMoney = ds.Tables[0].Rows[0]["WithdrawSumMoney"].ToString();
                else
                    ReturenData.WithdrawSumMoney = "0.00";
            }

            return ReturenData;
        }
        /// <summary>
        /// ��ȡ��ǰ��Ա��������¼��˿�б�
        /// </summary>
        /// <param name="HigherVipID"></param>
        /// <returns></returns>

        public List<VipFansInfo> GetVipFansList(string Col20, string Code, string VipName, int StartPage, int EndPage)
        {
            var ReturnList = new List<VipFansInfo>();
            DataSet ds = this._currentDAO.GetVipFansList(Col20, Code, VipName, StartPage, EndPage);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var Data = new VipFansInfo();
                    if (ds.Tables[0].Rows[i]["vipid"] != DBNull.Value)
                        Data.VipId = ds.Tables[0].Rows[i]["vipid"].ToString();

                    if (ds.Tables[0].Rows[i]["HeadImgUrl"] != DBNull.Value)
                        Data.HeadImgUrl = ds.Tables[0].Rows[i]["HeadImgUrl"].ToString();

                    if (ds.Tables[0].Rows[i]["vipname"] != DBNull.Value)
                        Data.VipName = ds.Tables[0].Rows[i]["vipname"].ToString();

                    if (ds.Tables[0].Rows[i]["prices"] != DBNull.Value)
                        Data.CardAmount = ds.Tables[0].Rows[i]["prices"].ToString();
                    else
                        Data.CardAmount = "0.00";

                    if (ds.Tables[0].Rows[i]["Phone"] != DBNull.Value)
                        Data.Phone = ds.Tables[0].Rows[i]["Phone"].ToString();

                    if (ds.Tables[0].Rows[i]["col48"] != DBNull.Value)
                    {
                        string str = ds.Tables[0].Rows[i]["col48"].ToString();
                        if (str.Equals("1"))
                            Data.CodeDes = "�ѳɽ�";
                        else
                            Data.CodeDes = "�ѹ�עδ�ɽ�";
                    }
                    else
                    {
                        Data.CodeDes = "�ѹ�עδ�ɽ�";
                    }

                    ReturnList.Add(Data);
                }

            }
            return ReturnList;
        }
        /// <summary>
        /// ��ȡ��Ա�����̷�˿���ݼ�����
        /// </summary>
        /// <param name="Col20"></param>
        /// <param name="Code"></param>
        /// <param name="VipName"></param>
        /// <returns></returns>
        public int GetVipFansListCount(string Col20, string Code, string VipName)
        {
            return this._currentDAO.GetVipFansListCount(Col20, Code, VipName);
        }
        #endregion



        #region ��Աͳ�Ʊ���
        /// <summary>
        /// ��Ա����ͳ�Ʊ���
        /// </summary>
        /// <param name="Month"></param>
        /// <param name="UnitID"></param>
        /// <param name="Gender"></param>
        /// <param name="CardStatusID"></param>
        /// <param name="StarDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public DataSet GetVipBirthdayCount(string Month, string UnitID, string Gender, int? CardStatusID, string StarDate, string EndDate, int? PageSize, int? PageIndex)
        {
            //VipBirthdayRD Data = null;
            //int? ThisCardStatusID = null;
            //if (CardStatusID != null) {
            //    ThisCardStatusID = CardStatusID.Value;
            //}
            DataSet ds = this._currentDAO.VipBirthdayCount(Month, UnitID, Gender, CardStatusID, StarDate, EndDate, PageSize, PageIndex);



            return ds;
        }
        #endregion

        #region ��Ա��ѯ
        /// <summary>
        /// ��Ա��ѯ Jermyn20130514+
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity SearchVipInfo(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                int iCount = _currentDAO.SearchVipInfoCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.SearchVipInfo(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);

                    foreach (VipEntity vipInfo1 in vipInfoList)
                    {
                        IList<TagsEntity> tagsList = new List<TagsEntity>();
                        tagsList = GetVipTags(vipInfo1.VIPID);
                        foreach (TagsEntity tagInfo in tagsList)
                        {
                            if (vipInfo1.VipTagsShort == null || vipInfo1.VipTagsShort.Equals(""))
                            {
                                vipInfo1.VipTagsShort = tagInfo.TagsName;
                                vipInfo1.VipTagsLong = tagInfo.TagsName;
                            }
                            else
                            {
                                if (vipInfo1.VipTagsShort.Length < 20)
                                {
                                    vipInfo1.VipTagsShort = vipInfo1.VipTagsShort + "," + tagInfo.TagsName;
                                }
                                vipInfo1.VipTagsLong = vipInfo1.VipTagsLong + "<br/>" + tagInfo.TagsName;
                            }
                        }
                        vipInfo1.VipTagsCount = tagsList.Count;
                    }
                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region ��ȡ��Ա����
        public VipEntity GetIntegralByVip(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                int iCount = _currentDAO.GetIntegralByVipCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.GetIntegralByVipList(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);


                }

                vipInfo.ICount = iCount;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region ��Ա��ע
        public bool SetVipInfo(VipEntity vipInfo)
        {
            /*1.�����ע��־��
              2.�ж����»�Ա���Ǵ��ڵĻ�Ա
              3.�����»�Ա
              4.�����Ѵ��ڵĻ�Ա
            */

            return true;
        }
        #endregion

        #region ��ȡ��Ա��ϸ��Ϣ
        /// <summary>
        /// ����΢��OpenID��ȡ��Ա��ϸ��Ϣ
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public VipEntity GetVipDetail(string OpenID)
        {
            VipEntity vipInfo = new VipEntity();

            DataSet ds = _currentDAO.GetVipDetail(OpenID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }

            return vipInfo;
        }

        #region ��ȡ��ʼ������Ϣ

        /// <summary>
        /// ��ȡ��ʼ������Ϣ��ǰ̨�ã�
        /// </summary>
        /// <param name="OpenID">΢��ID</param>
        /// <returns></returns>
        public VipInfoRD GetVipInitInfo(string OpenID)
        {
            VipInfoRD vipInfo = new VipInfoRD();

            DataSet ds = _currentDAO.GetVipDetail(OpenID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipInfoRD>(ds.Tables[0].Rows[0]);
            }
            return vipInfo;
        }

        #endregion

        public VipEntity GetVipDetailByVipID(string vipID)
        {
            VipEntity vipInfo = new VipEntity();

            DataSet ds = _currentDAO.GetVipDetailByVipID(vipID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }

            return vipInfo;
        }
        #endregion


        /// <summary>
        /// ���ݺ���ģ����ѯ��Ա
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public List<VipEntity> GetVipByPhone(string phone)
        {
            List<VipEntity> vipInfo = new List<VipEntity>();

            DataSet ds = _currentDAO.GetVipByPhone(phone);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }

            return vipInfo;
        }


        #region ��ȡ��Ա��
        /// <summary>
        /// ��ȡvip����
        /// </summary>
        /// <returns></returns>
        public string GetVipCode(string pre = "Vip")
        {
            return new AppSysService(this.CurrentUserInfo).GetNo(pre);
        }
        #endregion

        #region ��ȡ��ע������
        /// <summary>
        /// ��ȡ���ڹ�ע�Ŀͻ�
        /// </summary>
        /// <param name="Timestamp"></param>
        /// <param name="NewTimestamp"></param>
        /// <returns></returns>
        public int GetShowCount(long Timestamp, out long NewTimestamp)
        {
            int icount = 0;
            DataSet ds = _currentDAO.GetShowCount(Timestamp);
            if (ds != null && ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString()) > 0)
            {
                NewTimestamp = Convert.ToInt64(ds.Tables[0].Rows[0]["NewTimestamp"].ToString());
                icount = Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString());
            }
            else
            {
                NewTimestamp = _currentDAO.GetNowTimestamp();
            }
            return icount;
        }
        /// <summary>
        /// ��ȡ������Դ��Ӧ�Ļ�Ա����VipSourceId Ϊ NULL ʱ �����绰�ͷ���Դ�Դ�
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public DataSet GetShowCount2(string userId, string clientId)
        {
            return _currentDAO.GetShowCount2(userId, clientId);
        }
        public JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity GetShowCountBySource(long Timestamp)
        {
            DataSet ds = _currentDAO.GetShowCountBySource(Timestamp);
            JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity info = new Entity.Interface.VIPAttentionEntity();
            IList<JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity> infoList = new List<JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                infoList = DataTableToObject.ConvertToList<JIT.CPOS.BS.Entity.Interface.VIPAttentionEntity>(ds.Tables[0]);
            }
            info.VipAttentionInfoList = infoList;
            return info;
        }
        #endregion

        #region GetLjVipInfo
        /// <summary>
        /// ��ȡlj VIP��Ϣ
        /// </summary>
        /// <param name="OpenID"></param>
        /// <returns></returns>
        public VipEntity GetLjVipInfo(VipEntity entity)
        {
            VipEntity vipInfo = new VipEntity();
            DataSet ds = _currentDAO.GetLjVipInfo(entity);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                vipInfo = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }
            return vipInfo;
        }
        #endregion

        #region GetVipItemList
        /// <summary>
        /// ��ȡVIP�һ���Ʒ�б���Ϣ
        /// </summary>
        public ItemInfo GetVipItemList(VipEntity entity)
        {
            try
            {
                ItemInfo obj = new ItemInfo();

                IList<ItemInfo> list = new List<ItemInfo>();
                DataSet ds = _currentDAO.GetVipItemList(entity);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<ItemInfo>(ds.Tables[0]);
                }

                obj.ICount = list.Count;
                obj.ItemInfoList = list;

                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GetVipOrderList
        /// <summary>
        /// ��ȡVIP�����б���Ϣ
        /// </summary>
        public InoutInfo GetVipOrderList(VipEntity entity, int page, int pageSize)
        {
            try
            {
                InoutService inoutService = new InoutService(this.CurrentUserInfo);
                InoutInfo obj = new InoutInfo();

                IList<InoutInfo> list = new List<InoutInfo>();
                DataSet ds = _currentDAO.GetVipOrderList(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<InoutInfo>(ds.Tables[0]);
                }

                foreach (var item in list)
                {
                    item.InoutDetailList = inoutService.GetInoutDetailInfoByOrderId(item.order_id);
                }

                obj.ICount = _currentDAO.GetVipOrdersCount(entity);
                obj.InoutInfoList = list;

                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region GetVipSkuPropList
        /// <summary>
        /// ��ȡVIP�һ���ƷSKU�б���Ϣ
        /// </summary>
        public SkuInfo GetVipSkuPropList(VipEntity entity, string itemId)
        {
            try
            {
                SkuInfo obj = new SkuInfo();

                IList<SkuInfo> list = new List<SkuInfo>();
                DataSet ds = _currentDAO.GetVipSkuPropList(entity, itemId);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<SkuInfo>(ds.Tables[0]);
                }

                obj.SkuInfoList = list;

                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ��ȡ��Ա��ע����(�����Ͻ�)
        public int GetHasVipCount(string WeiXinId)
        {
            return _currentDAO.GetHasVipCount(WeiXinId);
        }
        #endregion

        #region ��ȡ�²ɼ���Ա����(�����Ͻ�)
        public int GetNewVipCount(string WeiXinId)
        {
            return _currentDAO.GetNewVipCount(WeiXinId);
        }
        #endregion

        #region getVipMonthAddup
        /// <summary>
        /// ��Ա�����ۼ�
        /// </summary>
        public LVipAddupEntity getVipMonthAddup(LVipAddupEntity entity, int page, int pageSize)
        {
            try
            {
                var result = new LVipAddupEntity();

                IList<LVipAddupEntity> list = new List<LVipAddupEntity>();
                DataSet ds = _currentDAO.getVipMonthAddup(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<LVipAddupEntity>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getVipMonthAddupCount(entity);
                result.EntityList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region getEventMonthEventAddup
        /// <summary>
        /// ��Ա�»����ͳ��
        /// </summary>
        public LEventAddupEntity getEventMonthEventAddup(LEventAddupEntity entity, int page, int pageSize)
        {
            try
            {
                var result = new LEventAddupEntity();

                IList<LEventAddupEntity> list = new List<LEventAddupEntity>();
                DataSet ds = _currentDAO.getEventMonthEventAddup(entity, page, pageSize);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<LEventAddupEntity>(ds.Tables[0]);
                }

                result.ICount = _currentDAO.getEventMonthEventAddupCount(entity);
                result.EntityList = list;
                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region ���ܲ���ȡvip��Ϣ Jermyn20130911
        public bool GetVipInfoFromApByOpenId(string OpenId, string VipId)
        {
            return _currentDAO.GetVipInfoFromApByOpenId(OpenId, VipId);
        }
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<VipEntity> GetLotteryVipList()
        {
            IList<VipEntity> list = new List<VipEntity>();
            DataSet ds = _currentDAO.GetLotteryVipList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion

        #region ��ȡ��Ա��ǩ����
        /// <summary>
        /// ��ȡ��Ա��ǩ����
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public IList<TagsEntity> GetVipTags(string VipId)
        {
            IList<TagsEntity> tagsList = new List<TagsEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipTags(VipId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tagsList = DataTableToObject.ConvertToList<TagsEntity>(ds.Tables[0]);
            }
            return tagsList;
        }
        #endregion

        #region ��ȡ��Ա��ǩӳ�伯��
        /// <summary>
        /// ��ȡ��Ա��ǩӳ�伯��
        /// </summary>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public IList<VipTagsMappingEntity> GetVipMappingTags(string VipId)
        {
            IList<VipTagsMappingEntity> tagsList = new List<VipTagsMappingEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipTagsMapping(VipId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tagsList = DataTableToObject.ConvertToList<VipTagsMappingEntity>(ds.Tables[0]);
            }
            return tagsList;
        }
        #endregion

        #region Jermyn20131207 �ϲ��û���ʶ
        /// <summary>
        /// �����û��ϲ�����Ҫ����Ϊ֮ǰ���û�����ע���û���ע��֮����������ʺţ���Ҫ�ϲ�
        /// </summary>
        /// <param name="UserId">������Cookie�е�</param>
        /// <param name="VipId"></param>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public bool SetMergerVipInfo(string UserId, string VipId, string OpenId)
        {
            return _currentDAO.SetMergerVipInfo(UserId, VipId, OpenId);
        }
        #endregion

        #region Jermyn20131219�ŵ꽱����ѯ
        /// <summary>
        /// �ŵ꽱����ѯ
        /// </summary>
        /// <param name="strError">������Ϣ</param>
        /// <returns>�ŵ�MembershipShop������SearchIntegral����Ա����UnitCount�����۽��UnitSalesAmount��ICount������</returns>
        public VipEntity GetUnitIntegral(VipEntity vipSearchInfo, out string strError)
        {
            VipEntity vipInfo = new VipEntity();
            try
            {
                vipInfo.ICount = _currentDAO.GetUnitIntegralCount(vipSearchInfo);
                IList<VipEntity> list = new List<VipEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetUnitIntegral(vipSearchInfo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }
                vipInfo.vipInfoList = list;
                strError = "ok";

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion

        #region Jermyn20131219����Ա����
        /// <summary>
        /// ����Ա����
        /// </summary>
        /// <param name="vipSearchInfo">��ѯ��������:�ŵ꣬�������ͣ���ѡ����Ӧ��SysIntegralSource ������ʼ���ڣ���������</param>
        /// <param name="strError"></param>
        /// <returns>����Ա���� UserName �ŵ� MembershipShop������SearchIntegral����Ա����VipCount���������۽��UnitSalesAmount</returns>
        public VipEntity GetPurchasingGuideIntegral(VipEntity vipSearchInfo, out string strError)
        {
            VipEntity vipInfo = new VipEntity();
            try
            {
                vipInfo.ICount = _currentDAO.GetPurchasingGuideIntegralCount(vipSearchInfo);
                IList<VipEntity> list = new List<VipEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetPurchasingGuideIntegral(vipSearchInfo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }
                vipInfo.vipInfoList = list;
                strError = "ok";

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion

        #region Jermyn20131219��Ա����
        /// <summary>
        /// ��Ա����
        /// </summary>
        /// <param name="vipSearchInfo">��ѯ��������:�ŵ�UnitId����Ա��VipName���������ͣ���ѡ����Ӧ��SysIntegralSource ������ʼ���ڣ���������</param>
        /// <param name="strError"></param>
        /// <returns>��Ա��VipName���Ἦ��MembershipShop������SearchIntegral����ԴVipSourceName����ǩVipTagsShort������ʱ��CreateTime���ȼ�VipLevelDesc���ƽ��Ա��VipCount��������PurchaseAmount</returns>
        public VipEntity GetVipIntegral(VipEntity vipSearchInfo, out string strError)
        {
            VipEntity vipInfo = new VipEntity();
            try
            {
                vipInfo.ICount = _currentDAO.GetVipIntegralCount(vipSearchInfo);
                IList<VipEntity> list = new List<VipEntity>();
                DataSet ds = new DataSet();
                ds = _currentDAO.GetVipIntegral(vipSearchInfo);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                    #region ��Ա��ǩ
                    foreach (VipEntity vipInfo1 in list)
                    {
                        IList<TagsEntity> tagsList = new List<TagsEntity>();
                        tagsList = GetVipTags(vipInfo1.VIPID);
                        foreach (TagsEntity tagInfo in tagsList)
                        {
                            if (vipInfo1.VipTagsShort == null || vipInfo1.VipTagsShort.Equals(""))
                            {
                                vipInfo1.VipTagsShort = tagInfo.TagsName;
                                vipInfo1.VipTagsLong = tagInfo.TagsName;
                            }
                            else
                            {
                                if (vipInfo1.VipTagsShort.Length < 20)
                                {
                                    vipInfo1.VipTagsShort = vipInfo1.VipTagsShort + "," + tagInfo.TagsName;
                                }
                                vipInfo1.VipTagsLong = vipInfo1.VipTagsLong + "<br/>" + tagInfo.TagsName;
                            }
                        }
                    }
                    #endregion
                }
                vipInfo.vipInfoList = list;
                strError = "ok";

            }
            catch (Exception ex)
            {
                strError = ex.ToString();
            }
            return vipInfo;
        }
        #endregion

        #region DataTable paging
        /// <summary>
        /// DataTable��ҳ
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="PageIndex">ҳ����,ע�⣺��1��ʼ</param>
        /// <param name="PageSize">ÿҳ��С</param>
        /// <returns>�ֺ�ҳ��DataTable����</returns>
        private DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (PageIndex == 0) { return dt; }
            DataTable newdt = dt.Copy();
            newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
            { return newdt; }

            if (rowend > dt.Rows.Count)
            { rowend = dt.Rows.Count; }
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
        /// <summary>
        /// ���ط�ҳ��ҳ��
        /// </summary>
        /// <param name="count">������</param>
        /// <param name="pageSize">ÿҳ��ʾ������</param>
        /// <returns>��� ��βΪ0���򷵻�1</returns>
        private int PageCount(int count, int pageSize)
        {
            int page = 0;
            if (count % pageSize == 0) { page = count / pageSize; }
            else { page = (count / pageSize) + 1; }
            if (page == 0) { page += 1; }
            return page;
        }
        #endregion

        public DataSet GetSaleFunnelData()
        {
            DataSet naviSalesData = new DataSet();
            naviSalesData = _currentDAO.GetSaleFunnelData();
            return naviSalesData;
        }

        #region ��ȡ��Ա�ĸ���״̬��������Jermyn201223
        /// <summary>
        /// ��ȡ��Ա�ĸ���״̬��������
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public VipEntity GetVipOrderByStatus(string OpenId)
        {
            VipEntity vipInfo = new VipEntity();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipOrderByStatus(OpenId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    switch (dr["Status"].ToString().Trim())
                    {
                        case "0":
                            vipInfo.Status0 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                        case "1":
                            vipInfo.Status1 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                        case "2":
                            vipInfo.Status1 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                        case "3":
                            vipInfo.Status1 = Convert.ToInt32(dr["ICount"].ToString().Trim());
                            break;
                    }
                }
            }
            else
            {
                vipInfo.Status0 = 0;
                vipInfo.Status1 = 1;
                vipInfo.Status2 = 2;
                vipInfo.Status3 = 3;
            }
            return vipInfo;
        }
        #endregion

        public string Register(string userID, string mobile, string name, string code, string customerID)
        {
            string result = "200";

            RegisterValidationCodeDAO registerValidationCodeDAO = new RegisterValidationCodeDAO(this.CurrentUserInfo);
            var validationCode = registerValidationCodeDAO.Query(new IWhereCondition[] {
                new EqualsCondition(){ FieldName = "Mobile", Value = mobile}
            }, new OrderBy[] { new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc } }).FirstOrDefault();

            Loggers.Debug(new DebugLogInfo()
            {
                Message = string.Format("validationCode: {0}", validationCode.ToJSON())
            });

            if (validationCode != null && validationCode.Code == code && validationCode.IsValidated.Value == 0)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("validationCode.Code: {0}", validationCode.Code)
                });

                validationCode.IsValidated = 1;
                validationCode.LastUpdateTime = DateTime.Now;
                registerValidationCodeDAO.Update(validationCode, false, null);

                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("validationCode Updated: {0}", validationCode.Code)
                });

                VipBLL vipBLL = new VipBLL(this.CurrentUserInfo);
                var vip = vipBLL.GetByID(userID);
                if (vip != null)
                {
                    vip.Phone = mobile;
                    vip.Status = 2;
                    vip.VipRealName = name;

                    vipBLL.Update(vip, true);

                    //added by zhangwei ע����200����
                    #region ������� ��ֹӰ��ע�ᣬ����try catch �����Դ���

                    try
                    {
                        //�µĻ��ַ��� zhangwei2013-2-13
                        new VipIntegralBLL(CurrentUserInfo).ProcessPoint(2, customerID, vip.VIPID, null);
                    }
                    catch
                    {

                    }

                    #endregion

                    result = "200";
                }
                else
                    result = "102";
            }
            else
                result = "101";

            return result;
        }

        public VipEntity GetVipByPhone(string phone, string vipId, string status)
        {
            VipEntity tagsList = null;
            DataSet ds = new DataSet();
            ds = _currentDAO.GetVipByPhone(phone, vipId, status);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                tagsList = DataTableToObject.ConvertToObject<VipEntity>(ds.Tables[0].Rows[0]);
            }
            return tagsList;
        }

        #region ��ȡ��Ա����
        /// <summary>
        /// GetList
        /// </summary>
        public IList<VipEntity> GetList_Emba(string keyword, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<VipEntity> list = new List<VipEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetList_Emba(keyword, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// GetListCount
        /// </summary>
        public int GetListCount_Emba(string keyword)
        {
            return _currentDAO.GetListCount_Emba(keyword);
        }
        #endregion


        #region ������
        public void ModifyPWD(string pCustomerID, string phone, string sPwd, string newPWD)
        {
            var temp = this._currentDAO.GetByPhone(phone, pCustomerID);
            if (temp.Length > 0)
            {
                var entity = temp[0];
                if (entity.VipPasswrod == sPwd)
                {
                    entity.VipPasswrod = newPWD;
                    this._currentDAO.Update(entity);
                }
                else
                {
                    throw new Exception("������֤ʧ��");
                }
            }
            else
            {
                throw new Exception("δ�ҵ����û�");
            }
        }
        #endregion

        #region ��Ա��ѯLocation
        /// <summary>
        /// ��Ա��ѯ Jermyn20130514+
        /// </summary>
        /// <param name="vipSearchInfo"></param>
        /// <returns></returns>
        public VipEntity SearchVipInfoLocation(VipSearchEntity vipSearchInfo)
        {
            try
            {
                VipEntity vipInfo = new VipEntity();
                //int iCount = _currentDAO.SearchVipInfoCount(vipSearchInfo);

                IList<VipEntity> vipInfoList = new List<VipEntity>();
                DataSet ds = _currentDAO.SearchVipInfoLocation(vipSearchInfo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    vipInfoList = DataTableToObject.ConvertToList<VipEntity>(ds.Tables[0]);
                }

                vipInfo.ICount = 1;
                vipInfo.vipInfoList = vipInfoList;

                return vipInfo;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion


        #region ��ȡ��Ա�ŵ�
        public string GetUnitByUserId(string userId)
        {
            return this._currentDAO.GetUnitByUserId(userId);
        }
        #endregion

        #region ���ݻ�Ա��ɫ��ȡAppȨ�� add by Henry 2015-3-26
        public DataSet GetAppMenuByUserId(string userId)
        {
            return this._currentDAO.GetAppMenuByUserId(userId);
        }
        #endregion

        #region ͬ����������Ա��Ϣ

        /// <summary>
        /// ͬ����������Ա��Ϣ
        /// </summary>
        /// <param name="vipId">��ԱID</param>
        /// <returns></returns>
        public void SyncAladingUserInfo(string vipId, string customerId)
        {
            try
            {
                var vipEntity = this._currentDAO.GetByID(vipId);

                if (vipEntity == null)
                {
                    List<Guid> vipList = new List<Guid>();
                    vipList.Add(Guid.Parse(vipId));

                    var aldRequest = new ALDOrderRequest();
                    aldRequest.BusinessZoneID = 1;
                    aldRequest.Locale = 1;
                    aldRequest.Parameters = new { MemberIDs = vipList };

                    //���������û���Ϣ
                    var url = ConfigurationManager.AppSettings["ALDGatewayURL"];  //��ʽ
                    //var url = "http://121.199.42.125:5012/Gateway.ashx";        //����

                    if (string.IsNullOrEmpty(url))
                        throw new Exception("δ���ð�����ƽ̨�ӿ�URL:ALDGatewayURL");
                    var postContent = string.Format("Action=GetMemberList4PushMessage&ReqContent={0}", aldRequest.ToJSON());
                    var strAldRsp = HttpWebClient.DoHttpRequest(url, postContent);
                    var aldRsp = strAldRsp.DeserializeJSONTo<ALDResponse>();

                    if (aldRsp != null && aldRsp.Data != null && aldRsp.Data.Count != 0 && aldRsp.ResultCode == 200)
                    {
                        var entity = aldRsp.Data.FirstOrDefault();
                        //ͬ���û���Ϣ
                        this._currentDAO.SyncAladingUserInfo(customerId, entity.MemberID, entity.MemberNo, entity.MemberNo,
                            entity.MemberNo, entity.IOSDeviceToken, entity.BaiduChannelID, "",
                            entity.BaiduUserID, entity.Platform, entity.ChannelID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public class ALDOrderRequest
        {
            public int? Locale { get; set; }
            public Guid? UserID { get; set; }
            public int? BusinessZoneID { get; set; }
            public string Token { get; set; }
            public object Parameters { get; set; }
        }

        public class ALDResponse
        {
            public int? ResultCode { get; set; }
            public string Message { get; set; }
            public List<MemberEntity> Data { get; set; }
        }

        public class MemberEntity
        {
            public string MemberID { get; set; }    //��ԱID
            public string MemberNo { get; set; }    //��¼�ʺ�
            public string Platform { get; set; }    //�ͻ���ƽ̨��1=Android,2=IOS,3=����
            public string IOSDeviceToken { get; set; }  //IOS���豸��
            public string BaiduChannelID { get; set; }  //�ٶ���Ϣ���͵�����ID
            public string BaiduUserID { get; set; }     //�ٶ���Ϣ���͵��û�ID
            public string ChannelID { get; set; }       //����ID
        }

        #endregion

        #region �ͷ���¼

        /// <summary>
        /// �ͷ���¼
        /// </summary>
        /// <param name="userName">�û���</param>
        /// <param name="password">����</param>
        /// <param name="customerId">�ͻ�ID</param>
        /// <returns></returns>
        public DataSet SetSignIn(string userName, string password, string customerId)
        {
            return this._currentDAO.SetSignIn(userName, password, customerId);
        }

        public DataSet GetRoleCodeByUserId(string userId, string customerId)
        {
            return this._currentDAO.GetRoleCodeByUserId(userId, customerId);
        }
        #region �ж��û��Ƿ����

        public bool JudgeUserExist(string userName, string customerId)
        {
            return this._currentDAO.JudgeUserExist(userName, customerId);
        }
        #endregion

        #region �ж������Ƿ���ȷ
        public bool JudgeUserPasswordExist(string userName, string customerId, string password)
        {
            return this._currentDAO.JudgeUserPasswordExist(userName, customerId, password);
        }
        #endregion

        public DataSet GetUserIdByUserNameAndPassword(string userName, string customerId, string password)
        {
            return this._currentDAO.GetUserIdByUserNameAndPassword(userName, customerId, password);
        }

        #region �жϸÿͷ���Ա�Ƿ��пͷ������������Ȩ��
        public bool JudgeUserRoleExist(string userName, string customerId, string password)
        {
            return this._currentDAO.JudgeUserRoleExist(userName, customerId, password);
        }
        #endregion

        #endregion

        public VipEntity GetByMobile(string pMobile, string pCustomerID)
        {
            var temp = this._currentDAO.GetByPhone(pMobile, pCustomerID);
            if (temp.Length > 0)
            {
                return temp[0];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// ��Ա��¼ Add by Alex Tian 2014-04-11
        /// </summary>
        /// <param name="VipNo"></param>
        /// <param name="mobile"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public VipEntity GetLoginInfo(string VipNo, string mobile, string password)
        {
            var temp = this._currentDAO.GetLoginInfo(VipNo, mobile, password);
            if (temp.Length > 0)
            {
                return temp[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ���ݻ�Ա��š��ֻ��ţ��ϲ���Ա��Ϣ�� �����ɹ�����1��ʧ�ܷ���0
        /// </summary>
        /// <param name="pCustomerID">�ͻ�ID</param>
        /// <param name="pVipID">��ԱID</param>
        /// <param name="pPhone">�ֻ���</param>
        /// <returns></returns>
        public bool MergeVipInfo(string pCustomerID, string pVipID, string pPhone)
        {
            return this._currentDAO.MergeVipInfo(pCustomerID, pVipID, pPhone);
        }

        /// <summary>
        /// ��ȡһ���µĻ�Ա����
        /// </summary>
        /// <returns></returns>
        public string GetNewVipCode(string pCustomerID)
        {
            return this._currentDAO.GetVipCode(pCustomerID);
        }

        public int GetVipCoupon(string vipId)
        {
            return this._currentDAO.GetVipCoupon(vipId);
        }


        public string GetSettingValue(string customerId)
        {
            return this._currentDAO.GetSettingValue(customerId);
        }

        public DataSet GetVipColumnInfo(string eventCode, string customerId)
        {
            return this._currentDAO.GetVipColumnInfo(customerId, eventCode);
        }

        public DataSet GetVipInfo(string vipId)
        {
            return this._currentDAO.GetVipInfo(vipId);
        }

        public string GetVipLeave(string vipId)
        {
            return this._currentDAO.GetVipLeave(vipId);
        }

        public decimal GetIntegralBySkuId(string skuIdList)
        {
            return this._currentDAO.GetIntegralBySkuId(skuIdList);
        }

        public decimal GetTotalSaleAmountBySkuId(string skuIdList)
        {
            return this._currentDAO.GetTotalSaleAmountBySkuId(skuIdList);
        }
        /// <summary>
        /// ��ȡ���ֶһ��������
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public decimal GetIntegralAmountPre(string customerId)
        {
            return this._currentDAO.GetIntegralAmountPre(customerId);
        }
        /// <summary>
        /// ���ݿͻ����ֶһ�������úͻ��֣�������
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <param name="amount">���</param>
        /// <returns></returns>
        public decimal GetAmountByIntegralPer(string customerId, decimal integral)
        {
            decimal integralAmountPre = this._currentDAO.GetIntegralAmountPre(customerId);
            integralAmountPre = integralAmountPre > 0 ? integralAmountPre : 0.01M;
            return Math.Round(integral / integralAmountPre, 2);
        }
        public decimal GetTotalReturnAmountBySkuId(string skuIdList, SqlTransaction tran)
        {
            return this._currentDAO.GetTotalReturnAmountBySkuId(skuIdList, tran);
        }

        public decimal GetVipEndAmount(string vipId)
        {
            return this._currentDAO.GetVipEndAmount(vipId);
        }
        /// <summary>
        /// ��ȡ��Ա�Ż�ȯ����
        /// </summary>
        /// <param name="vipId">��ԱID</param>
        /// <param name="totalPayAmount">֧�����</param>
        /// <param name="usableRange">���÷�Χ(1=����ȯ��2=����ȯ)</param>
        /// <param name="objectID">�Ż�ȯʹ���ŵ�/������ID</param>
        /// <param name="type">�Ƿ��������ȯ��0=��������ȯ��1=����������ȯ��</param>
        /// <returns></returns>
        public DataSet GetVipCouponDataSet(string vipId, decimal totalPayAmount, int usableRange, string objectID, int type)
        {
            return this._currentDAO.GetVipCouponDataSet(vipId, totalPayAmount, usableRange, objectID, type);
        }

        public void ProcSetCancelOrder(string customerId, string orderId, string vipId)
        {
            this._currentDAO.ProcSetCancelOrder(customerId, orderId, vipId);
        }

        public DataSet VipLandingPage(string customerId)
        {
            return this._currentDAO.VipLandingPage(customerId);
        }
        public DataSet GetVipIntegralDetail(string vipId, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.GetVipIntegralDetail(vipId, pPageIndex, pPageSize);
        }
        public DataSet GetVipEndAmountDetail(string vipId, int pPageIndex, int pPageSize)
        {
            return this._currentDAO.GetVipEndAmountDetail(vipId, pPageIndex, pPageSize);
        }

        #region ��ֵ��ģ�飬���һ�Ա
        public DataSet GetCardVip(string criterion, string couponCode, int pageSize, int pageIndex)
        {
            return this._currentDAO.GetCardVip(criterion, couponCode, pageSize, pageIndex);
        }
        #endregion

        public string GetMaxVipCode()
        {
            return this._currentDAO.GetMaxVipCode();
        }
        public void AddVipWXDownload(UserInfoEntity item)
        {
            this._currentDAO.AddVipWXDownload(item);
        }
        public int WXToVip(string BatNo)
        {
            return this._currentDAO.WXToVip(BatNo);

        }

        public string GetVipSearchPropList(string customerId, string tableName, string unitId)
        {
            return this._currentDAO.GetVipSearchPropList(customerId, tableName, unitId);
        }
        /// <summary>
        /// ��ȡ���»�Ա��������Ϣ�Լ��ж�Ӧ��ֵ
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public DataSet GetExistVipInfo(string customerId, string userId, string vipId)
        {
            return this._currentDAO.GetExistVipInfo(customerId, userId, vipId);
        }
        /// <summary>
        /// ��ȡ������Ա��̬��������
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <returns></returns>
        public string GetCreateVipPropList(string customerId, string userId)
        {
            return this._currentDAO.GetCreateVipPropList(customerId, userId);
        }
        public DataSet GetVipTagTypeList()
        {
            return this._currentDAO.GetVipTagTypeList();
        }
        public DataSet GetVipTagList(string customerId)
        {
            return this._currentDAO.GetVipTagList(customerId);
        }
        public DataSet GetVipDetailInfo(string vipId, string customerId)
        {
            return this._currentDAO.GetVipDetailInfo(vipId, customerId);
        }
        public void UpdateVipInfo(string vipId, SearchColumn[] columns)
        {
            var vipLogBll = new VipLogBLL(this.CurrentUserInfo);
            var snapShotBll = new VipSnapshotBLL(this.CurrentUserInfo);
            using (TransactionScope scope = new TransactionScope())
            {
                var vip = this.GetVipDetailByVipID(vipId);
                var viplog = new VipLogEntity()
                {
                    LogID = Guid.NewGuid().ToString("N"),
                    Action = "�޸���Ϣ",
                    VipID = vipId,
                    CreateBy = this.CurrentUserInfo.UserID,
                };
                vipLogBll.Create(viplog);
                snapShotBll.InsertSnapshotByVip(vip, this.CurrentUserInfo.UserID);
                this._currentDAO.UpdateVipInfo(vipId, columns);
                scope.Complete();
            }
        }
        public DataSet GetVipIntegralList(string vipId, int pageIndex, int pageSize, string sortType)
        {
            return this._currentDAO.GetVipIntegralList(vipId, pageIndex, pageSize, sortType);
        }

        public DataSet GetVipOrderList(string vipId, string customerId, int pageIndex, int pageSize, string sortType)
        {
            return this._currentDAO.GetVipOrderList(vipId, customerId, pageIndex, pageSize, sortType);
        }
        public DataSet GetVipConsumeCardList(string vipId, int pageIndex, int pageSize, string sortType)
        {
            return this._currentDAO.GetVipConsumeCardList(vipId, pageIndex, pageSize, sortType);
        }

        public DataSet GetVipAmountList(string vipid, int pageIndex, int pageSize, string sortType)
        {
            return this._currentDAO.GetVipAmountList(vipid, pageIndex, pageSize, sortType);
        }
        public DataSet GetVipOnlineOffline(string vipId, int pageIndex, int pageSize, string sortType)
        {
            return this._currentDAO.GetVipOnlineOffline(vipId, pageIndex, pageSize, sortType);
        }
        /// <summary>
        /// ���������ͱ�ǩ��ѯVIP�б�
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <param name="userId">�û�ID</param>
        /// <param name="pageIndex">�ڼ�ҳ</param>
        /// <param name="pageSize">ÿҳ��¼��</param>
        /// <param name="orderBy">�����ֶ���</param>
        /// <param name="isDesc">�Ƿ�Ϊ����</param>
        /// <param name="searchColumns">�в�ѯ��������</param>
        /// <param name="vipSearchTags">VIP��ǩ��������</param>
        /// <returns></returns>
        public DataSet SearchVipList(string customerId, string userId, int pageIndex, int pageSize, string orderBy,
                                        string sortType, SearchColumn[] searchColumns, VipSearchTag[] vipSearchTags)
        {
            return this._currentDAO.SearchVipList(customerId, userId, pageIndex, pageSize, orderBy,
                                         sortType, searchColumns, vipSearchTags);
        }
        /// <summary>
        /// ɾ����Ա
        /// </summary>
        /// <param name="vipIds">��Աid�б�</param>
        public void DeleteVip(string[] vipIds)
        {
            var vipLogBll = new VipLogBLL(this.CurrentUserInfo);
            var snapShotBll = new VipSnapshotBLL(this.CurrentUserInfo);
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (var id in vipIds)
                {
                    var vip = this.GetVipDetailByVipID(id);
                    var viplog = new VipLogEntity()
                    {
                        LogID = Guid.NewGuid().ToString("N"),
                        Action = "ɾ����Ա",
                        VipID = id,
                        CreateBy = this.CurrentUserInfo.UserID,
                    };
                    vipLogBll.Create(viplog);
                    snapShotBll.InsertSnapshotByVip(vip, this.CurrentUserInfo.UserID);
                }
                this._currentDAO.DeleteVips(vipIds, this.CurrentUserInfo.UserID);
                scope.Complete();
            }
        }
        /// <summary>
        /// ��������ӻ�Ա
        /// </summary>
        /// <param name="columns"></param>
        public void InsertVipEntity(SearchColumn[] columns, string clientId)
        {
            var bll = new VipBLL(this.CurrentUserInfo);
            var vipLogBll = new VipLogBLL(this.CurrentUserInfo);
            using (TransactionScope scope = new TransactionScope())
            {
                string vipCode = GetVipCode();
                var vipId = this._currentDAO.InsertVipEntity(columns, clientId, vipCode);
                var vip = bll.GetVipDetailByVipID(vipId);
                var viplog = new VipLogEntity()
                {
                    LogID = Guid.NewGuid().ToString("N"),
                    Action = "������Ա",
                    VipID = vipId,
                    CreateBy = this.CurrentUserInfo.UserID,
                };
                vipLogBll.Create(viplog);
                scope.Complete();
            }
        }
        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public VipEntity[] QueryByEntityAbsolute(VipEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            return _currentDAO.QueryByEntityAbsolute(pQueryEntity, pOrderBys);
        }
        private string newVipNotificationTemplateId = "eV8Sk890yc4tVtJUj43kOfdfpN1H4_QG6_Trb3V6jpM";
        /// <summary>
        /// ����ע���Աʱ������ ����Ϊ��Ա֪ͨ�� ��΢��ģ����Ϣ
        /// </summary>
        /// <param name="vipId"></param>
        public void SendNotification2NewVip(VipEntity vip)
        {
            var customerBll = new t_customerBLL(this.CurrentUserInfo);
            var customer = customerBll.GetByCustomerID(vip.ClientID);
            if (null == customer) return;
            var address = customer.customer_address;
            var remark = string.Format("��ע���������ʣ�����ѯ{0}��", customer.customer_tel);
            var first = string.Format("���ã����Ѿ���Ϊ{0}��Ա��", customer.customer_name);
            var message = new WeixinTemplateMessage()
            {
                touser = vip.WeiXinUserId,
                template_id = newVipNotificationTemplateId,
                url = "",
                topcolor = "#FF0000",
                data = new Dictionary<string, WeixinTemplateMessageData>()
            };
            message.Add("first", first, "#cccccc");
            message.Add("cardNumber", vip.VipCode, "#cccccc");
            message.Add("type", "�̻�", "#000000");
            message.Add("address", address, "#cccccc");
            message.Add("VIPName", vip.VipName, "#cccccc");
            message.Add("VIPPhone", vip.Phone, "#cccccc");
            message.Add("expDate", DateTime.MaxValue.Date.ToSQLFormatString(), "#cccccc");
            message.Add("remark", remark, "#cccccc");
            var jsonContent = message.ToJSON();
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("zk weixin template message interface json result:{0}", jsonContent) });
            #region simple format template message for new vip
            /******* 
            var content = new
            {
                touser = vip.WeiXinUserId,
                template_id = newVipNotificationTemplateId,
                url = "",
                topcolor = "#FF0000",
                data = new
                {
                    first = new
                    {
                        value = first,
                        color = "#cccccc"
                    },
                    cardNumber = new
                    {
                        value = vip.VipCode,
                        color = "#cccccc"
                    },
                    type = new
                    {
                        value = "�̻�",
                        color = "#cccccc"
                    },
                    address = new
                    {
                        value = address,
                        color = "#cccccc"
                    },
                    VIPName = new
                    {
                        value = vip.VipName,
                        color = "#cccccc"
                    },
                    VIPPhone = new
                    {
                        value = vip.Phone,
                        color = "#cccccc"
                    },
                    expDate = new
                    {
                        vaue = DateTime.MaxValue,
                        color = "#cccccc"
                    },
                    remark = new
                    {
                        value = remark,
                        color = "#cccccc"
                    }
                }
            };
            ******************************/
            #endregion
            var appService = new WApplicationInterfaceBLL(this.CurrentUserInfo);
            var appList = appService.QueryByEntity(new WApplicationInterfaceEntity { CustomerId = vip.ClientID }, null);
            if (appList == null || appList.Length == 0) return;
            var app = appList.FirstOrDefault();
            var commonBll = new CommonBLL();
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("��ʼ���ͳ�Ϊ��Ա֪ͨģ����Ϣ:{0}", jsonContent) });
            var result = commonBll.SendTemplateMessage(app.WeiXinID, jsonContent);
            Loggers.Debug(new DebugLogInfo() { Message = string.Format("���ͳ�Ϊ��Ա֪ͨģ����Ϣ���ؽ����{0}", result) });
        }

        #region RequestParameter 2014-10-16

        /// <summary>
        /// �û���ʼ��Ϣ
        /// </summary>
        public class VipInfoRP : IAPIRequestParameter
        {
            /// <summary>
            /// ΢��ID
            /// </summary>
            public string WeiXinUserID { get; set; }

            public void Validate()
            {
                //if (string.IsNullOrEmpty(WeiXinUserID))
                //    throw new APIException(201, "΢��ID����Ϊ�գ�");
            }
        }

        /// <summary>
        /// �û���ʼ��Ϣ
        /// </summary>
        public class VipInfoRD : IAPIResponseData
        {
            /// <summary>
            /// �û�ID
            /// </summary>
            public string VIPID { get; set; }
            /// <summary>
            /// �û�����
            /// </summary>
            public string VipName { get; set; }
            /// <summary>
            /// ��ν
            /// </summary>
            public string Gender { get; set; }
            /// <summary>
            /// �û��绰
            /// </summary>
            public string Phone { get; set; }
            /// <summary>
            /// ΢��ID
            /// </summary>
            public string WeiXinUserID { get; set; }
            /// <summary>
            /// �ͻ�ID
            /// </summary>
            public string ClientID { get; set; }
            /// <summary>
            /// �ŵ��б�
            /// </summary>
            public IList<JIT.CPOS.BS.BLL.UnitService.UnitInfoRD> UnitList { get; set; }

            /// <summary>
            /// ����Ϣ�б�
            /// </summary>
            public IList<VipExpandEntityInfoRD> CarInfoList { get; set; }

            /// <summary>
            /// ԤԼ����
            /// </summary>
            //public IList<JIT.CPOS.BS.BLL.ReserveTypeBLL.ReserveTypeInfoRD> ReserveTypeList { get; set; }

            ///// <summary>
            ///// ���ƺ�
            ///// </summary>
            //public string LicensePlateNo { get; set; }

            ///// <summary>
            ///// �û���ʵ����
            ///// </summary>
            public string VipRealName { get; set; }
        }

        public class VipExpandEntityInfoRD : IAPIResponseData
        {
            /// <summary>
            /// ����ID
            /// </summary>
            public String VipExpandID { get; set; }
            /// <summary>
            /// ���ƺ���
            /// </summary>
            public String LicensePlateNo { get; set; }
        }

        #endregion

        #region ���ݻ�ԱID��ȡ��Ա�ۿ���Ϣ 2014-11-5

        public decimal GetVipSale(string vipID)
        {
            DataSet ds = _currentDAO.GetVipSale(vipID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SalesPreferentiaAmount"] is DBNull)
                {
                    return 100;
                }
                else
                {
                    return (Convert.ToDecimal(ds.Tables[0].Rows[0]["SalesPreferentiaAmount"]) * 100);
                }
            }
            else
            {
                //�Ҳ�����Ϣ�򲻴��ۿ�
                return 100;
            }
        }

        #endregion

        #region ��ȡ�Ƶ��Ա����
        public DataSet GetCardBag(string weixinUserId, string cloudCustomerId)
        {
            return this._currentDAO.GetCardBag(weixinUserId, cloudCustomerId);
        }
        #endregion

        /// <summary>
        /// ��Ա����ͳ��
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int GetInviteCount(string userID)
        {
            return _currentDAO.GetInviteCount(userID);
        }
        /// <summary>
        /// ��Ա����Ϣ
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="idnumber"></param>
        /// <param name="vipcardcode"></param>
        /// <returns></returns>
        public DataSet GetVipCardInfo(string phone, string idnumber, string vipcardcode, string vipcardisn)
        {
            return this._currentDAO.GetVipCardInfo(phone, idnumber, vipcardcode, vipcardisn);
        }
        /// <summary>
        /// ��Ա������
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="idnumber"></param>
        /// <param name="vipcardcode"></param>
        /// <returns></returns>
        public DataSet GetVipCardDetail(string phone, string idnumber, string vipcardcode, string vipcardisn)
        {
            return this._currentDAO.GetVipCardDetail(phone, idnumber, vipcardcode, vipcardisn);
        }
        /// <summary>
        /// ��Ա�����
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="idnumber"></param>
        /// <param name="vipcardcode"></param>
        /// <returns></returns>
        public DataSet GetVipCardBalance(string phone, string idnumber, string vipcardcode, string vipcardisn)
        {
            return this._currentDAO.GetVipCardBalance(phone, idnumber, vipcardcode, vipcardisn);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        /// <summary>
        /// ���ݻ�Ա��ȡ��Ա����Ϣ
        /// </summary>
        /// <param name="vipid"></param>
        /// <param name="vipcode"></param>
        /// <returns></returns>
        public DataSet GetVipCardByVip(string vipid, string vipcode)
        {
            return this._currentDAO.GetVipCardByVip(vipid, vipcode);
        }
        /// <summary>
        /// ��ȡ��Ա�б�-�°�
        /// </summary>
        /// <param name="pWhereConditions"></param>
        /// <param name="pOrderBys"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pCurrentPageIndex"></param>
        /// <returns></returns>
        public PagedQueryResult<VipEntity> GetVipList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int type, int pPageSize, int pCurrentPageIndex)
        {
            return this._currentDAO.GetVipList(pWhereConditions, pOrderBys, type, pPageSize, pCurrentPageIndex);
        }

        /// <summary>
        /// ����ҳ�õ����̵�ǰ��Ա�б�
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="unitId"></param>
        /// <param name="dateCode"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<VipEntity> PagedQueryForCurrentVip(string customerId, string unitId, int pageIndex, int pageSize)
        {
            //��ѯ����
            List<IWhereCondition> lstWhere = new List<IWhereCondition> { };
            lstWhere.Add(new EqualsCondition() { FieldName = "ClientID", Value = customerId });
            lstWhere.Add(new EqualsCondition() { FieldName = "CouponInfo", Value = unitId });
            lstWhere.Add(new DirectCondition("Status = 2"));

            //�������
            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = "VipName", Direction = OrderByDirections.Asc });
            lstOrder.Add(new OrderBy() { FieldName = "Phone", Direction = OrderByDirections.Asc });

            //��ѯ
            return PagedQuery(lstWhere.ToArray(), lstOrder.ToArray(), pageSize, pageIndex + 1);
        }

        public DataSet ExcelToDb(string strPath, LoggingSessionInfo CurrentUserInfo)
        {
            DataSet ds; //Ҫ���������  
            DataSet dsResult = new DataSet(); //Ҫ���������  
            DataTable dt;

            DataTable table = new DataTable("Error");
            //��ȡ�м���,�����  
            DataColumnCollection columns = table.Columns;
            columns.Add("ErrMsg", typeof(String));



            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + strPath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn); //����excel              
            if (conn.State.ToString() == "Open")
            {
                conn.Close();
            }
            conn.Open();    //�ⲿ����Ԥ�ڸ�ʽ��������2010��excel��ṹ  
            string s = conn.State.ToString();
            OleDbDataAdapter myCommand = null;
            ds = null;

            string strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, conn);
            ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();


            try
            {


                dt = ds.Tables[0];
                string connString = CurrentUserInfo.CurrentLoggingManager.Connection_String; //System.Configuration.ConfigurationManager.AppSettings["Conn_alading"]; //@"user id=dev;password=JtLaxT7668;data source=182.254.219.83,3433;database=cpos_bs_alading;";   //�������ݿ��·������  
                SqlConnection connSql = new SqlConnection(connString);
                connSql.Open();
                DataRow dr = null;
                int C_Count = dt.Columns.Count;//��ȡ����  
                if (C_Count == 12)
                {
                    DataTable dtVip = new DataTable();
                    dtVip.Columns.Add("id", typeof(Int32));
                    dtVip.Columns.Add("VipName", typeof(string));
                    dtVip.Columns.Add("VipCardType", typeof(string));
                    dtVip.Columns.Add("VipTel", typeof(string));
                    dtVip.Columns.Add("OpenId", typeof(string));
                    dtVip.Columns.Add("Birthday", typeof(DateTime));
                    dtVip.Columns.Add("VipGender", typeof(string));
                    dtVip.Columns.Add("IDCardNum", typeof(string));
                    dtVip.Columns.Add("PointBalance", typeof(decimal));
                    dtVip.Columns.Add("CashBalance", typeof(decimal));
                    dtVip.Columns.Add("VipCard", typeof(string));
                    dtVip.Columns.Add("CreateVipDate", typeof(DateTime));
                    dtVip.Columns.Add("CreateVipUnit", typeof(string));
                    dtVip.Columns.Add("CreateUserId", typeof(string));
                    dtVip.Columns.Add("CustomerId", typeof(string));

                    for (int i = 0; i < dt.Rows.Count; i++)  //��¼���е�������ѭ������  
                    {
                        dr = dt.Rows[i];
                        DataRow dr_Vip = dtVip.NewRow();
                        if (dr[0].ToString() != "" && dr[1].ToString() != "")
                        {
                            //this._currentDAO.insertToSql(dr, C_Count, connSql, CurrentUserInfo.ClientID, CurrentUserInfo.UserID);
                            dr_Vip["id"] = 0;
                            dr_Vip["VipName"] = dr[0].ToString();
                            dr_Vip["VipCardType"] = dr[1].ToString();
                            dr_Vip["VipTel"] = dr[2].ToString();
                            dr_Vip["OpenId"] = dr[3].ToString();
                            dr_Vip["Birthday"] = dr[4].ToString() == "" ? DateTime.Now : Convert.ToDateTime(dr[4].ToString());
                            dr_Vip["VipGender"] = dr[5].ToString();
                            dr_Vip["IDCardNum"] = dr[6].ToString();
                            dr_Vip["PointBalance"] = dr[7].ToString() == "" ? 0 : Convert.ToDecimal(dr[7].ToString());
                            dr_Vip["CashBalance"] = dr[8].ToString() == "" ? 0 : Convert.ToDecimal(dr[8].ToString());
                            dr_Vip["VipCard"] = dr[9].ToString();
                            dr_Vip["CreateVipDate"] = dr[10].ToString() == "" ? DateTime.Now : Convert.ToDateTime(dr[10].ToString());
                            dr_Vip["CreateVipUnit"] = dr[11].ToString();
                            dr_Vip["CreateUserId"] = CurrentUserInfo.UserID;
                            dr_Vip["CustomerId"] = CurrentUserInfo.ClientID;

                            dtVip.Rows.Add(dr_Vip);
                        }
                    }
                    Utils.SqlBulkCopy(connString, dtVip, "ImportVipTemp");
                    connSql.Close();
                    //��ʱ������ʽ��
                    dsResult = this._currentDAO.ExcelImportToDB(CurrentUserInfo.ClientID);
                }
                else
                {

                    DataRow row = table.NewRow();
                    row["ErrMsg"] = "ģ����������" + C_Count.ToString(); ;
                    table.Rows.Add(row);
                    dsResult.Tables.Add(table);

                    DataTable tableCount = new DataTable("Count");
                    DataColumnCollection columns1 = tableCount.Columns;
                    columns1.Add("TotalCount", typeof(Int16));
                    columns1.Add("ErrCount", typeof(Int16));
                    row = tableCount.NewRow();
                    row["TotalCount"] = "0";
                    row["ErrCount"] = dt.Rows.Count.ToString();
                    tableCount.Rows.Add(row);
                    dsResult.Tables.Add(tableCount);

                }
            }
            catch (Exception ex)
            {
                dsResult = new DataSet();
                DataRow row = table.NewRow();
                row["ErrMsg"] = ex.Message.ToString();
                table.Rows.Add(row);
                dsResult.Tables.Add(table);

                DataTable tableCount = new DataTable("Count");
                DataColumnCollection columns1 = tableCount.Columns;
                columns1.Add("TotalCount", typeof(Int16));
                columns1.Add("ErrCount", typeof(Int16));
                row = tableCount.NewRow();
                row["TotalCount"] = "0";
                row["ErrCount"] = "0";
                tableCount.Rows.Add(row);
                dsResult.Tables.Add(tableCount);
            }

            return dsResult;
        }

        /// <summary>
        /// ��ȡVIPID   ���ݶ���ID���ɹ���
        /// </summary>
        /// <returns></returns>
        public string GetVipId(LoggingSessionInfo CurrentUserInfo)
        {
            string id = "";
            string result = HttpHelper.GetData(string.Empty, string.Format("{0}{1}", ConfigHelper.GetAppSetting("QueueAddr", "http://182.254.242.12:8011"), "/keyvalue/GetIdentity"));

            if (!string.IsNullOrEmpty(result) && !result.Equals("-1"))
            {
                id = result;
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "��ȡ�����û�ID�ɹ�,ID:" + result
                });
            }
            else if (result.Equals("-1"))
            {
                var pabll = new PA_UserInfoBLL(CurrentUserInfo);
                id = pabll.GetMaxVipId();
                id = (Convert.ToInt64(id) + 1).ToString();
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "��ʼ�������û�ID�ɹ�,ID:" + id
                });
                HttpHelper.GetData(string.Empty, string.Format("{0}{1}", ConfigHelper.GetAppSetting("QueueAddr", "http://182.254.242.12:8011"), "/keyvalue/set/Identity/" + id));
            }
            else
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = "���ɶ����û�IDʧ��,���ؽ��:" + result
                });
            }
            return id;
        }
    }
}