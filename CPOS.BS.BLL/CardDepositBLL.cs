/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/9 15:19:33
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Common;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class CardDepositBLL
    {
        string key = Utils.GetEncryptKey(Config.OriginalKey);

        public string BulkInsertCard(string channelID, decimal amount, decimal bonus, int qty, string userID, string clientID)
        {
            string result = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Column1", typeof(System.Byte[]));
            Random rd = new Random();

            for (int i = 0; i < qty; i++)
            {
                int p = rd.Next(100000, 999999);
                byte[] password = Utils.TripleDESEncrypt(key, p.ToString());

                DataRow dr = dt.NewRow();
                dr["ID"] = i + 1;
                dr["Column1"] = password;
                dt.Rows.Add(dr);
            }

            result = this._currentDAO.BulkInsertCard(channelID, amount, bonus, qty, userID, clientID, dt);
            return result;
        }

        public byte[] DecryptCardPassword(byte[] encryptPassword)
        {
            return Utils.TripleDESDecrypt(key, encryptPassword);
        }

        public DataSet PagedSearch(GetCardRP getCardInfoRP, string customerID)
        {
            DataSet ds = new DataSet();

            ds = this._currentDAO.PagedSearch(getCardInfoRP.ChannelID, getCardInfoRP.CardNoStart, getCardInfoRP.CardNoEnd, getCardInfoRP.CardStatus, getCardInfoRP.UseStatus, getCardInfoRP.Amount, getCardInfoRP.DateRange, getCardInfoRP.CreateTimeStart, getCardInfoRP.CreateTimeEnd, customerID, getCardInfoRP.PageIndex, getCardInfoRP.PageSize, getCardInfoRP.CardNo);

            return ds;
        }

        public int SetCardStatus(APIRequest<SetCardStatusRP> rp)
        {
            int result = this._currentDAO.SetCardStatus(rp.Parameters.CardIDs.ToJoinString(',', '\''), rp.Parameters.CardStatus, this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID);

            return result;
        }

        public DataSet GetCardByIDs(APIRequest<GetCardRP> rp)
        {
            List<JIT.Utility.DataAccess.Query.IWhereCondition> lwc = new List<JIT.Utility.DataAccess.Query.IWhereCondition>();
            lwc.Add(new JIT.Utility.DataAccess.Query.InCondition<string>() { FieldName = "CardDepositID", Values = rp.Parameters.CardIDs });
            lwc.Add(new JIT.Utility.DataAccess.Query.EqualsCondition() { FieldName = "CardDeposit.CustomerId", Value = this.CurrentUserInfo.ClientID });

            return this._currentDAO.QueryDataSet(lwc.ToArray(), new JIT.Utility.DataAccess.Query.OrderBy[] { new JIT.Utility.DataAccess.Query.OrderBy() { FieldName = "CardNo", Direction = JIT.Utility.DataAccess.Query.OrderByDirections.Asc } });
        }

        public DataSet GetCardByBatchID(APIRequest<GetCardRP> rp)
        {
            List<JIT.Utility.DataAccess.Query.IWhereCondition> lwc = new List<JIT.Utility.DataAccess.Query.IWhereCondition>();
            lwc.Add(new JIT.Utility.DataAccess.Query.EqualsCondition() { FieldName = "BatchID", Value = rp.Parameters.BatchID });
            lwc.Add(new JIT.Utility.DataAccess.Query.EqualsCondition() { FieldName = "CardDeposit.CustomerId", Value = this.CurrentUserInfo.ClientID });

            return this._currentDAO.QueryDataSet(lwc.ToArray(), new JIT.Utility.DataAccess.Query.OrderBy[] { new JIT.Utility.DataAccess.Query.OrderBy() { FieldName = "CardNo", Direction = JIT.Utility.DataAccess.Query.OrderByDirections.Asc } });
        }

        public int ActiveCard(APIRequest<ActiveCardRP> rp)
        {
            var cardDepositBLL = new CardDepositBLL(CurrentUserInfo);
            var cardDeposit = PagedSearch(new GetCardRP() { CardNo = rp.Parameters.CardNo, UseStatus = "0" }, CurrentUserInfo.ClientID);

            int result = 0;

            if (cardDeposit != null && cardDeposit.Tables.Count > 0 && cardDeposit.Tables[0].Rows.Count > 0)
            {
                DataRow dr = cardDeposit.Tables[0].Rows[0];
                if (rp.Parameters.CardPassword == System.Text.Encoding.UTF8.GetString(cardDepositBLL.DecryptCardPassword((byte[])dr["CardPassword"])).Replace("\0", ""))
                {
                    System.Data.SqlClient.SqlTransaction tran = GetTran();
                    using (tran.Connection)
                    {
                        string errorMessage = "";
                        VipAmountBLL vipAmountBLL = new VipAmountBLL(CurrentUserInfo);
                        vipAmountBLL.SetVipAmountChange(CurrentUserInfo.ClientID, 4, rp.Parameters.VipID, decimal.Parse(dr["Amount"].ToString()) + decimal.Parse(dr["Bonus"].ToString()), dr["CardDepositId"].ToString(), "Prepaid card charge", "In", out errorMessage, tran);
                        result = this._currentDAO.ActiveCard(rp.Parameters.CardNo, rp.Parameters.VipID, this.CurrentUserInfo.UserID, this.CurrentUserInfo.ClientID, tran);

                        tran.Commit();
                    }
                }
            }

            return result;
        }

        public System.Data.SqlClient.SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }

        public SuccessResponse<IAPIResponseData> VipConsume(APIRequest<VipConsumeRP> rp)
        {
            var cardDepositBLL = new CardDepositBLL(CurrentUserInfo);
            var registerValidationCodeBLL = new RegisterValidationCodeBLL(CurrentUserInfo);
            var vipBLL = new VipBLL(CurrentUserInfo);
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            string phone = "";
            
            var vip = vipBLL.Query(new IWhereCondition[] { new EqualsCondition() { FieldName = "VipID", Value = rp.Parameters.VipID } }, null);
            if (vip.Length > 0)
            {
                phone = vip[0].Phone;

                if (string.IsNullOrEmpty(phone))
                {
                    rsp.ResultCode = 202;
                    rsp.Message = "会员未注册手机号！";
                }
                else if (string.IsNullOrEmpty(rp.Parameters.SMSCode))
                {
                    //发送验证码
                    registerValidationCodeBLL.SendCode(phone);
                }
                else
                {
                    //验证验证码
                    var codeEntity = registerValidationCodeBLL.Query(new IWhereCondition[] { 
                        new EqualsCondition() { FieldName="Mobile", Value=phone }
                        , new EqualsCondition() { FieldName="Code", Value=rp.Parameters.SMSCode }
                        , new EqualsCondition() { FieldName="IsValidated", Value=0 }
                        , new EqualsCondition() { FieldName="IsDelete", Value=0 }
                    }, new OrderBy[] { 
                        new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc }
                    });

                    if (codeEntity != null && codeEntity.Length > 0)
                    {
                        System.Data.SqlClient.SqlTransaction tran = GetTran();
                        using (tran.Connection)
                        {
                            registerValidationCodeBLL.DeleteByMobile(phone, 1, tran);

                            string errorMessage = "";
                            VipAmountBLL vipAmountBLL = new VipAmountBLL(CurrentUserInfo);
                            vipAmountBLL.SetVipAmountChange(CurrentUserInfo.ClientID, 5, rp.Parameters.VipID, rp.Parameters.Amount, CurrentUserInfo.CurrentUserRole.UnitId, "Prepaid card consumption" + "~" + (rp.Parameters.DocumentCode ?? ""), "Out", out errorMessage, tran);

                            tran.Commit();
                        }
                    }
                    else
                    {
                        rsp.ResultCode = 203;
                        rsp.Message = "请先获取验证码！";
                    }
                }
            }
            else
            {
                rsp.ResultCode = 201;
                rsp.Message = "会员不存在！";
            }

            return rsp;
        }

        public SuccessResponse<IAPIResponseData> CardSummary(APIRequest<CardSummaryRP> rp)
        {
            var cardDepositBLL = new CardDepositBLL(CurrentUserInfo);
            var rd = new CardSummaryRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            DataSet ds = _currentDAO.CardSummary(rp.Parameters.PageSize, rp.Parameters.PageIndex);

            if (ds.Tables.Count == 2)
            {
                var cardSummaryList = (from d in ds.Tables[0].AsEnumerable()
                                       select new CardSummary()
                                       {
                                           ChannelTitle = d["ChannelTitle"].ToString(),
                                           Amount = d["Amount"].ToString(),
                                           ActivatedAmount = d["ActivatedAmount"].ToString()
                                       });

                rd.CardSummaryArray = cardSummaryList.ToArray();
                rd.TotalPage = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                rd.TotalCount = int.Parse(ds.Tables[1].Rows[0][1].ToString());
            }

            return rsp;
        }

        public SuccessResponse<IAPIResponseData> TransactionList(APIRequest<TransactionListRP> rp)
        {
            var cardDepositBLL = new CardDepositBLL(CurrentUserInfo);
            var rd = new TransactionListRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            DataSet ds = _currentDAO.TransactionList(rp.Parameters.PageSize, rp.Parameters.PageIndex);

            if (ds.Tables.Count == 2)
            {
                var transactionList = (from d in ds.Tables[0].AsEnumerable()
                                       select new Transaction()
                                       {
                                         Amount = d["Amount"].ToString(),
                                         AmountSource = d["AmountSource"].ToString(),
                                         CreateTime = d["CreateTime"].ToString(),
                                         Phone = d["Phone"].ToString(),
                                         UnitName = d["UnitName"].ToString(),
                                         VipCode = d["VipCode"].ToString(),
                                         VipName = d["VipName"].ToString(),
                                         VipRealName = d["VipRealName"].ToString(),
                                         RoomType = d["Remark"].ToString(),
                                         IDCard = d["Col2"].ToString()
                                       });

                rd.TransactionArray = transactionList.ToArray();
                rd.TotalPage = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                rd.TotalCount = int.Parse(ds.Tables[1].Rows[0][1].ToString());
            }

            return rsp;
        }
    }

    #region ResponseData

    public class GetChannelRD : IAPIResponseData
    {
        public ChannelInfo[] ChannelList { get; set; }
    }

    public class ChannelInfo
    {
        public string ChannelID { get; set; }
        public string ChannelTitle { get; set; }
    }

    public class MakeCardRD : IAPIResponseData
    {
        public string BatchID { get; set; }
    }

    public class EmptyRD : IAPIResponseData
    {

    }

    public class GetCardRD : IAPIResponseData
    {
        public Card[] CardList { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class Card
    {
        public string CardID { get; set; }
        public string CardNo { get; set; }
        public string ChannelTitle { get; set; }
        public string CardPassword { get; set; }
        public decimal Amount { get; set; }
        public decimal Bonus { get; set; }
        public decimal ConsumedAmount { get; set; }
        public string VipId { get; set; }
        public int CardStatus { get; set; }
        public int UseStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateBy { get; set; }
    }

    public class GetCardVipRD : IAPIResponseData
    {
        public VipEntity[] VipList { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class CardSummaryRD : IAPIResponseData
    {
        public CardSummary[] CardSummaryArray { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class CardSummary
    {
        public string ChannelTitle { get; set; }
        public string Amount { get; set; }
        public string ActivatedAmount { get; set; }
    }

    public class TransactionListRD : IAPIResponseData
    {
        public Transaction[] TransactionArray { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class Transaction
    {
        public string VipCode { get; set; }
        public string VipName { get; set; }
        public string VipRealName { get; set; }
        public string Phone { get; set; }
        public string Amount { get; set; }
        public string AmountSource { get; set; }
        public string UnitName { get; set; }
        public string CreateTime { get; set; }
        public string RoomType { get; set; }
        public string IDCard { get; set; }
    }

    #endregion

    #region RequestParameter
    public class MakeCardRP : IAPIRequestParameter
    {
        public string ChannelID { get; set; }
        public decimal Amount { get; set; }
        public decimal Bonus { get; set; }
        public int Qty { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(ChannelID))
                throw new APIException(201, "渠道不能为空！");

            if (!(Amount >= 0))
                throw new APIException(202, "卡金额不能为负数！");

            if (!(Bonus >= 0))
                throw new APIException(203, "赠送金额不能为负数！");

            if (!(Qty > 0))
                throw new APIException(204, "卡数量必须大于0！");
        }
    }

    public class GetCardRP : IAPIRequestParameter
    {
        public string ChannelID { get; set; }
        public string CardNoStart { get; set; }
        public string CardNoEnd { get; set; }
        public string CardStatus { get; set; }
        public string UseStatus { get; set; }
        public string Amount { get; set; }
        public string DateRange { get; set; }
        public DateTime? CreateTimeStart { get; set; }
        public DateTime? CreateTimeEnd { get; set; }
        public string CardNo { get; set; }
        public string[] CardIDs { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string BatchID { get; set; }

        public void Validate()
        {
            if(string.IsNullOrEmpty(ChannelID) && (!string.IsNullOrEmpty(CardNoStart) || !string.IsNullOrEmpty(CardNoEnd)))
                throw new APIException(201, "以卡序号查询时必须选择渠道！");
        }
    }

    public class SetCardStatusRP : IAPIRequestParameter
    {
        public string[] CardIDs { get; set; }
        public int CardStatus { get; set; }

        public void Validate()
        {
            if (CardIDs.Length < 0)
                throw new APIException(201, "请至少选中一条记录！");

            if (!(CardStatus >= 0))
                throw new APIException(202, "设置卡状态错误！");
        }
    }

    public class GetCardVipRP : IAPIRequestParameter
    {
        public string Criterion { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string CouponCode { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Criterion) && string.IsNullOrEmpty(CouponCode))
                throw new APIException(201, "搜索条件不能为空！");
        }
    }

    public class ActiveCardRP : IAPIRequestParameter
    {
        public string CardNo { get; set; }
        public string VipID { get; set; }
        public string Mobile { get; set; }
        public string CardPassword { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CardNo))
                throw new APIException(201, "卡号不能为空！");

            if (string.IsNullOrEmpty(VipID))
                throw new APIException(202, "会员不能为空！");

            if (string.IsNullOrEmpty(Mobile))
                throw new APIException(203, "手机号不能为空！");

            if (string.IsNullOrEmpty(CardPassword))
                throw new APIException(204, "密码不能为空！");
        }
    }

    public class VipConsumeRP : IAPIRequestParameter
    {
        public decimal Amount { get; set; }
        public string SMSCode { get; set; }
        public string VipID { get; set; }
        public string DocumentCode { get; set; }

        public void Validate()
        {
            if (Amount <=0)
                throw new APIException(201, "消费额必须大于0！");

            if (string.IsNullOrEmpty(VipID))
                throw new APIException(202, "会员ID不能为空！");
        }
    }

    public class CardSummaryRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public void Validate()
        {

        }
    }

    public class TransactionListRP : IAPIRequestParameter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public void Validate()
        {

        }
    }
    #endregion
}