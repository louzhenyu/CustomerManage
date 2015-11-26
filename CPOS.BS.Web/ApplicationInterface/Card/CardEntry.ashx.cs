using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using JIT.CPOS.BS.Web.Base.Excel;
using Aspose.Cells;
using JIT.Utility;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Card
{
    /// <summary>
    /// VipGateway 的摘要说明
    /// </summary>
    public class CardEntry : BaseGateway
    {
        #region

        public string GetChannel(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new GetChannelRD();
            var channelBll = new CardChannelInfoBLL(loggingSessionInfo);

            var ds = channelBll.Query(
                new Utility.DataAccess.Query.IWhereCondition[] { 
                    new Utility.DataAccess.Query.EqualsCondition() { 
                        FieldName = "CustomerId", Value = loggingSessionInfo.ClientID
                    }
                }
                , new Utility.DataAccess.Query.OrderBy[] { 
                    new Utility.DataAccess.Query.OrderBy() { 
                        Direction = Utility.DataAccess.Query.OrderByDirections.Asc, FieldName="DisplayIndex" 
                    }
                 });

            if (ds.Length > 0)
            {
                ChannelInfo channelInfo = new ChannelInfo();

                var channelList = from d in ds
                                  select new ChannelInfo()
                                  {
                                      ChannelID = d.ChannelId.ToString(),
                                      ChannelTitle = d.ChannelTitle
                                  };

                rd.ChannelList = channelList.ToArray();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string MakeCard(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rp = pRequest.DeserializeJSONTo<APIRequest<MakeCardRP>>();
            rp.Parameters.Validate();

            var rd = new MakeCardRD();

            string result = cardDepositBLL.BulkInsertCard(rp.Parameters.ChannelID, rp.Parameters.Amount, rp.Parameters.Bonus, rp.Parameters.Qty, loggingSessionInfo.UserID, loggingSessionInfo.ClientID);

            rd.BatchID = result;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            if (string.IsNullOrEmpty(result))
            {
                rsp.ResultCode = 204;
                rsp.Message = "生成失败!";
            }

            return rsp.ToJSON();
        }

        public string GetCard(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rd = new GetCardRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCardRP>>();
            rp.Parameters.Validate();

            var ds = cardDepositBLL.PagedSearch(rp.Parameters, loggingSessionInfo.ClientID);

            if (ds.Tables.Count == 2)
            {
                var cardInfoList = (from d in ds.Tables[0].AsEnumerable()
                                    select new BLL.Card()
                                   {
                                       CardID = d["CardDepositId"].ToString(),
                                       CardNo = d["CardNo"].ToString(),
                                       ChannelTitle = d["ChannelTitle"].ToString(),
                                       Amount = decimal.Parse(d["Amount"].ToString()),
                                       Bonus = decimal.Parse(d["Bonus"].ToString()),
                                       CardStatus = int.Parse(d["CardStatus"].ToString()),
                                       UseStatus = int.Parse(d["UseStatus"].ToString())
                                   });

                rd.CardList = cardInfoList.ToArray();
                rd.TotalPage = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                rd.TotalCount = int.Parse(ds.Tables[1].Rows[0][1].ToString());
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        public string SetCardStatus(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rp = pRequest.DeserializeJSONTo<APIRequest<SetCardStatusRP>>();
            rp.Parameters.Validate();

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            int result = cardDepositBLL.SetCardStatus(rp);

            if (result >= 0)
            {
                rsp.ResultCode = 0;
                rsp.Message = result.ToString();
            }
            else
            {
                rsp.ResultCode = 202;
                rsp.Message = "更新失败!";
            }
            return rsp.ToJSON();
        }

        private string ExportCard(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rd = new GetCardRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCardRP>>();
            rp.Parameters.Validate();

            DataSet ds = new DataSet();

            if (rp.Parameters.CardIDs != null && rp.Parameters.CardIDs.Length > 0)
                ds = cardDepositBLL.GetCardByIDs(rp);
            else if (!string.IsNullOrEmpty(rp.Parameters.BatchID))
                ds = cardDepositBLL.GetCardByBatchID(rp);
            else
            {
                rp.Parameters.PageSize = 1000000;
                rp.Parameters.PageIndex = 0;
                ds = cardDepositBLL.PagedSearch(rp.Parameters, loggingSessionInfo.ClientID);
            }

            if (ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                if (ds.Tables[0] != null)
                {
                    ds.Tables[0].Columns.Add(new DataColumn() { ColumnName = "CardPasswordDecrypt", DataType = typeof(string) });

                    #region 解密密码
                    foreach (DataRow item in ds.Tables[0].Rows)
                        item["CardPasswordDecrypt"] = System.Text.Encoding.UTF8.GetString(cardDepositBLL.DecryptCardPassword((byte[])item["CardPassword"]));
                    #endregion

                    Dictionary<string, string> exportFields = new Dictionary<string, string>();
                    exportFields.Add("ChannelTitle", "渠道");
                    exportFields.Add("CardNo", "卡号");
                    exportFields.Add("CardPasswordDecrypt", "密码");
                    exportFields.Add("Amount", "金额");
                    exportFields.Add("Bonus", "赠送金额");

                    #region 替换标题信息
                    DataColumn dc = new DataColumn();
                    dt = ds.Tables[0].DefaultView.ToTable(false, exportFields.Keys.ToArray());

                    foreach (DataColumn c in dt.Columns)
                    {
                        string title;
                        exportFields.TryGetValue(c.ColumnName, out title);
                        c.ColumnName = title;
                    }
                    #endregion
                }

                //数据获取
                Workbook wb = DataTableExporter.WriteXLS(dt, 0);
                string savePath = HttpContext.Current.Server.MapPath(@"~/File/Excel");
                if (!System.IO.Directory.Exists(savePath))
                {
                    System.IO.Directory.CreateDirectory(savePath);
                }
                savePath = savePath + "\\卡数据导出" + DateTime.Now.ToFileTime() + ".xls";
                wb.Save(savePath);//保存Excel文件
                new ExcelCommon().OutPutExcel(HttpContext.Current, savePath);
                HttpContext.Current.Response.End();
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }

        /// <summary>
        /// 这个方法在couponWriteOff.js已经不用,Sun@2015-11-03标注
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetCardVip(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var vipBLL = new VipBLL(loggingSessionInfo);

            var rd = new GetCardVipRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetCardVipRP>>();
            rp.Parameters.Validate();

            var ds = vipBLL.GetCardVip(rp.Parameters.Criterion, rp.Parameters.CouponCode, rp.Parameters.PageSize, rp.Parameters.PageIndex);

            if (ds.Tables.Count == 2)
            {
                var vipList = (from d in ds.Tables[0].AsEnumerable()
                                    select new BS.Entity.VipEntity()
                                    {
                                        VIPID = d["VIPID"].ToString(),
                                        VipCode = d["VipCode"].ToString(),
                                        VipRealName = d["VipRealName"].ToString(),
                                        VipName = d["VipName"].ToString(),
                                        Phone = d["Phone"].ToString(),
                                        Col2 = d["Col2"].ToString(),
                                        Col4 = d["EndAmount"].ToString()
                                    }).ToList();

                rd.VipList = vipList.ToArray();
                rd.TotalPage = int.Parse(ds.Tables[1].Rows[0][0].ToString());
                rd.TotalCount = int.Parse(ds.Tables[1].Rows[0][1].ToString());
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            //获取优惠券
            var couponBll = new CouponBLL(loggingSessionInfo);                //优惠券BLL实例化
            var couponEntity = couponBll.QueryByEntity(new CouponEntity()
            {
                CouponCode = rp.Parameters.CouponCode
            }, null);

            if (couponEntity == null || couponEntity.Length == 0)
            {
                rsp.ResultCode = 103;
                rsp.Message = "优惠券无效";
                return rsp.ToJSON();
            }
            if (couponEntity[0].Status == 1)
            {
                rsp.ResultCode = 103;
                rsp.Message = "优惠券已核销";
                return rsp.ToJSON();
            }

            return rsp.ToJSON();
        }

        public string ActiveCard(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rp = pRequest.DeserializeJSONTo<APIRequest<ActiveCardRP>>();
            rp.Parameters.Validate();

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            int result = cardDepositBLL.ActiveCard(rp);

            if (result >= 1)
            {
                rsp.ResultCode = 0;
                rsp.Message = result.ToString();
            }
            else
            {
                rsp.ResultCode = 300;
                rsp.Message = "密码错误或卡已被使用!";
            }

            return rsp.ToJSON();
        }

        private string VipConsume(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rp = pRequest.DeserializeJSONTo<APIRequest<VipConsumeRP>>();
            rp.Parameters.Validate();

            SuccessResponse<IAPIResponseData> result = cardDepositBLL.VipConsume(rp);

            return result.ToJSON();
        }

        public string CardSummary(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);
            
            var rp = pRequest.DeserializeJSONTo<APIRequest<CardSummaryRP>>();

            var rsp = cardDepositBLL.CardSummary(rp);

            return rsp.ToJSON();
        }

        public string TransactionList(string pRequest)
        {
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var cardDepositBLL = new CardDepositBLL(loggingSessionInfo);

            var rp = pRequest.DeserializeJSONTo<APIRequest<TransactionListRP>>();

            var rsp = cardDepositBLL.TransactionList(rp);

            return rsp.ToJSON();
        }

        #endregion

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetChannel":
                    rst = this.GetChannel(pRequest);
                    break;
                case "MakeCard":
                    rst = this.MakeCard(pRequest);
                    break;
                case "GetCard":
                    rst = this.GetCard(pRequest);
                    break;
                case "SetCardStatus":
                    rst = this.SetCardStatus(pRequest);
                    break;
                case "ValidateCard":
                    rst = this.GetCard(pRequest);
                    break;
                case "ExportCard":
                    rst = this.ExportCard(pRequest);
                    break;
                case "GetCardVip":
                    rst = this.GetCardVip(pRequest);
                    break;
                case "ActiveCard":
                    rst = this.ActiveCard(pRequest);
                    break;
                case "VipConsume":
                    rst = this.VipConsume(pRequest);
                    break;
                case "CardSummary":
                    rst = this.CardSummary(pRequest);
                    break;
                case "TransactionList":
                    rst = this.TransactionList(pRequest);
                    break; 
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            return rst;
        }
    }
}