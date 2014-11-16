using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using JIT.CPOS.BS.Web.Module.MapAnalysis.Common.Handler;

//using JIT.TenantPlatform.Web.PageBase;
//using JIT.TenantPlatform.BLL;
//using JIT.TenantPlatform.Entity;
//using JIT.TenantPlatform.Web.Session;

namespace JIT.CPOS.BS.Web.Module.MapAnalysis.Common
{
    public partial class StoreInfo : Page
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ShowStoreContent();
            }
        }
        #endregion

        protected void ShowStoreContent()
        {
            var storeID = this.Request.QueryString["sid"];
            //
            this.StoreName = string.Empty;
            //SessionManager rSession = new SessionManager();
            //
            if (!string.IsNullOrWhiteSpace(storeID))
            {
                //var bll = new StoreBLL(rSession.CurrentUserLoginInfo);
                var storeInfo = BrandSKUTreeHandler.GetStoreByID(storeID);
                this.StoreName = storeInfo.StoreName;
                bool hasStorePhoto = (!string.IsNullOrWhiteSpace(storeInfo.BannerPhoto));
                //
                StringBuilder html = new StringBuilder();
                //
                if (hasStorePhoto)
                {
                    html.AppendFormat("<div class=\"mobile_shop\">{0}", Environment.NewLine);
                    html.AppendFormat(" <img class=\"view\" src=\"{1}\" alt=\"\" border=\"0\"/>{0}", Environment.NewLine, this.getBannerPhotoUrl(storeInfo));
                    
                }
                else
                {
                    html.AppendFormat("<div class=\"mobile_shop_non\">{0}", Environment.NewLine);
                    html.AppendFormat(" <div class=\"addView\"  style='border:1px dashed #CCCCCC;'>{0}", Environment.NewLine);
                    html.AppendFormat("     <a href=\"###\" onclick=\"alert('添加终端图片')\">{0}", Environment.NewLine);
                    html.AppendFormat("         <span class=\"hLine\"></span>{0}", Environment.NewLine);
                    html.AppendFormat("         <span class=\"vLine\"></span>{0}", Environment.NewLine);
                    html.AppendFormat("         <h2>添加终端图片</h2>{0}", Environment.NewLine);
                    html.AppendFormat("     </a>{0}", Environment.NewLine);
                    html.AppendFormat(" </div>{0}", Environment.NewLine);
                }
                html.AppendFormat(" <ul class=\"desc\">{0}", Environment.NewLine);
                //
                string kpitext = this.Request.QueryString["kpitext"];
                string kpilabel = this.Request.QueryString["kpilabel"];
                html.AppendFormat("     <li><label for=\"\">{1}:</label>{2}</li>{0}", Environment.NewLine, kpitext, kpilabel);
                //
                if (storeInfo.ChannelID.HasValue)
                {
                    //var channelBll = new ChannelBLL(rSession.CurrentUserLoginInfo);
                    var channelInfo = BrandSKUTreeHandler.GetChannelByID(storeInfo.ChannelID.Value.ToString());
                    html.AppendFormat("     <li><label for=\"\">渠道类型:</label>{1}</li>{0}", Environment.NewLine, channelInfo.ChannelName);
                }
                else
                {
                    html.AppendFormat("     <li><label for=\"\">渠道类型:</label></li>{0}", Environment.NewLine);
                }
                //
                html.AppendFormat("     <li><label for=\"\">地&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;址:</label>{1}</li>{0}", Environment.NewLine, storeInfo.Addr);
                html.AppendFormat("     <li><label for=\"\">联&nbsp;&nbsp;系&nbsp;人:</label>{1}</li>{0}", Environment.NewLine,storeInfo.Manager);
                html.AppendFormat("     <li><label for=\"\">联系方式:</label>{1}</li>{0}", Environment.NewLine,storeInfo.Tel);
                html.AppendFormat("     <li><label for=\"\">营业状态:</label>正常</li>{0}", Environment.NewLine);
                html.AppendFormat(" </ul>{0}", Environment.NewLine);
                //
                this.ltStoreContent.Mode = LiteralMode.PassThrough;
                this.ltStoreContent.Text = html.ToString();
            }
        }

        private string getBannerPhotoUrl(StoreEntity pEntity)
        {
            return pEntity.BannerPhoto;
            //return string.Format("/File/Photo/{0}/{1}/Store/{2}",this.CurrentUserInfo.ClientID,this.CurrentUserInfo.ClientDistributorID,pEntity.BannerPhoto);
        }

        public string StoreName { get; set; }
    }
}