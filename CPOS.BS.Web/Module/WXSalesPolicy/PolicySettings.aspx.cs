using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.BS.Web.Module.WXSalesPolicy
{
    public partial class PolicySettings : JIT.CPOS.BS.Web.PageBase.JITPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            string content = string.Empty;
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
                return;
            try
            {
                action = action.Trim().ToLower();
                switch (action)
                {
                    case "save":
                        content = SaveItems();
                        break;
                    case "load":
                        content = LoadItems();
                        break;
                    default: 
                        break;
                }
            }
            catch (Exception ex)
            {
                content = ex.Message;
            }
            Response.ContentEncoding = Encoding.UTF8;
            Response.Write(content);
            Response.End();
        }

        #region fields
        private WXSalesPolicyRateBLL controller = null;
        private LoggingSessionInfo logInfo = null;
        #endregion

        #region properties

        protected LoggingSessionInfo LogInfo
        {
            get
            {
                if (null == logInfo)
                {
                    logInfo = new SessionManager().CurrentUserLoginInfo;
                }
                return logInfo;
            }
        }
       
        protected WXSalesPolicyRateBLL Controller
        {
            get
            {
                if (controller == null)
                {
                    controller = new WXSalesPolicyRateBLL(LogInfo);
                }
                return controller;
            }
        }
        #endregion

        #region methods
        private string LoadItems()
        {
            var items = Controller.QueryByEntity(new WXSalesPolicyRateEntity { CustomerId = LogInfo.ClientID, IsDelete = 0 }, null);
            foreach (var e in items)
            {
                e.Coefficient = e.Coefficient * 100;
            }
            items = items.OrderBy(x => x.AmountBegin).ToArray();
            return items.ToJSON();
        }
        private string SaveItems()
        {
            var json = Request["data"];
            var items = json.DeserializeJSONTo<WXSalesPolicyRateEntity[]>();
            ProcItems(items);
            try
            {
                Controller.BatchProcess(LogInfo.ClientID,LogInfo.UserID,items);
                var rlt = new { IsSuccess = 1, Message = "保存成功" };
                return rlt.ToJSON();
            }
            catch (Exception ex)
            {
                var exRlt = new { IsSuccess =  0, Message = string.Format("保存失败：{0}",ex.Message) };
                return exRlt.ToJSON();
            }
            
        }
        /// <summary>
        /// 计算每个规则的基数
        /// </summary>
        /// <param name="items"></param>
        private void ProcItems(WXSalesPolicyRateEntity[] items)
        {
            var len = items.Length;
            for (var i = 0; i < len; i++)
            {
                var o = items[i];
                o.CustomerId = LogInfo.ClientID;
                if (!o.RateId.HasValue || string.IsNullOrEmpty(o.RateId.ToString()))
                {
                    o.RateId = Guid.NewGuid();
                    o.CreateBy = LogInfo.UserID;
                    o.LastUpdateBy = LogInfo.UserID;
                }
                else
                {
                    o.LastUpdateBy = LogInfo.UserID;
                }
                if (i == 0)
                {
                    if (o.AmountBegin > 0)
                    {
                        //(前一个区间的结束值-0) * 前一个区间的系数 +　基数
                        o.CardinalNumber = (o.AmountBegin - 0) * (0*0.01m) + 0;
                    }
                    //如果从0开始，基数定为0
                    else if (o.AmountBegin == 0)
                    {
                        o.CardinalNumber = 0;
                    }
                    continue;
                }
                //如果不是第一个区间，则基数为上一个区间的基数+（上一个区间的结束值-上一个区间的起始值）* 上一个区间的系数
                var prev = items[i-1];
                o.CardinalNumber = (prev.AmountEnd - prev.AmountBegin) * (prev.Coefficient*0.01m) 
                    + prev.CardinalNumber;
            }
        }
        #endregion
    }
}