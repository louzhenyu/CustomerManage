using JIT.CPOS.BS.Web.MapKpiService;
using JIT.CPOS.BS.Web.Module.MapAnalysis.Common.Handler;
//using JIT.TenantPlatform.BLL;
//using JIT.TenantPlatform.BLL.Control;
//using JIT.TenantPlatform.Entity;
//using JIT.TenantPlatform.Web.MapKpiService;
//using JIT.TenantPlatform.Web.PageBase;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.BS.Web.Module.MapAnalysis.Common
{
    public partial class Common : System.Web.UI.Page
    {
        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.CreateKPIMenu();
                this.CreateBrandList();
                this.CreateCategoryList();
                this.CreateSKUList();
                this.GetChannelList();
                this.GetChainList();
            }
        }
        #endregion

        protected string GetCurrentClientID()
        { 
            return "27";
        }

        //protected TenantPlatform.Utility.TenantPlatformUserInfo GetCurrentUserInfo()
        //{
        //    var userInfo = new TenantPlatform.Utility.TenantPlatformUserInfo();
        //    userInfo.AppModel = this.CurrentUserInfo.AppModel;
        //    userInfo.ClientDistributorID = this.CurrentUserInfo.ClientDistributorID;
        //    userInfo.ClientID = "27"; 
        //    userInfo.ClientUserRealName = this.CurrentUserInfo.ClientUserRealName;
        //    userInfo.ConnectionString = this.CurrentUserInfo.ConnectionString;
        //    userInfo.ImgPath = this.CurrentUserInfo.ImgPath;
        //    userInfo.Lang = this.CurrentUserInfo.Lang;
        //    userInfo.StructureLevel = this.CurrentUserInfo.StructureLevel;
        //    userInfo.UserID = this.CurrentUserInfo.UserID;
        //    userInfo.UserOPRight = this.CurrentUserInfo.UserOPRight;

        //    return userInfo;
        //}


        #region 生成菜单
        /// <summary>
        /// 生成菜单
        /// </summary>
        private void CreateKPIMenu()
        {
            MapKpiServiceClient service = new MapKpiServiceClient();
            var kpiCategories = service.GetKpiByClientId(this.GetCurrentClientID());
            if (kpiCategories != null && kpiCategories.Length > 0)
            {
                List<KPI> kpis = new List<KPI>();
                //生成KPI菜单的HTML
                StringBuilder html = new StringBuilder();
                html.AppendLine("<ul class='level1-ul' style='padding-top:5px'>");
                foreach (var category in kpiCategories)
                {
                    html.AppendFormat("{0}<li>{1}", Keyboard.TAB, Environment.NewLine);
                    html.AppendFormat("{0}{0}<a href=\"#\" ><div style='height: 22px;width:140px;*width:120px;'><div style='float:left;width:100px;'>{2}</div><div style='float:right; width:10px;'>></div></div></a>{1}", Keyboard.TAB, Environment.NewLine, category.CategoryName);
                    if (category.KPIs != null && category.KPIs.Length > 0)
                    {
                        html.AppendFormat("{0}{0}<ul>{1}", Keyboard.TAB, Environment.NewLine);
                        foreach (var kpi in category.KPIs)
                        {
                            html.AppendFormat("{0}{0}{0}<li>{1}", Keyboard.TAB, Environment.NewLine);
                            html.AppendFormat("{0}{0}{0}{0}<a href=\"#\" id=\"{3}\" onclick=\"javascript:changeKPI(this);\">{2}</a>{1}", Keyboard.TAB, Environment.NewLine, kpi.KPIText, kpi.KPIID);
                            html.AppendFormat("{0}{0}{0}</li>{1}", Keyboard.TAB, Environment.NewLine);
                        }
                        html.AppendFormat("{0}{0}</ul>{1}", Keyboard.TAB, Environment.NewLine);
                        //
                        kpis.AddRange(category.KPIs);
                    }
                    html.AppendFormat("{0}</li>{1}", Keyboard.TAB, Environment.NewLine);
                }
                html.AppendLine("</ul>");
                //生成KPI对象脚本
                StringBuilder script = new StringBuilder();
                script.AppendLine("<script type=\"text/javascript\">");
                script.AppendFormat("{0}var mapKPIs=eval({2});{1}", Keyboard.TAB, Environment.NewLine, kpis.ToArray().ToJSON());
                script.AppendLine("</script>");
                //
                this.ltMenuKPI.Mode = LiteralMode.PassThrough;
                this.ltMenuKPI.Text += html.ToString();
                this.ltMenuKPI.Text += script.ToString();
            }
        }
        #endregion     

        #region 生成品牌列表
        /// <summary>
        /// 生成品牌列表
        /// </summary>
        private void CreateBrandList()
        {
            //BrandBLL bll = new BrandBLL(this.GetCurrentUserInfo());
            BrandEntity brandQueryEntity = new BrandEntity();
            brandQueryEntity.IsLeaf=-1;
            //var list = bll.QueryByEntity(brandQueryEntity, null);
            //var list = bll.GetAll();
            var list = BrandSKUTreeHandler.GetBrandList(brandQueryEntity);
            if (list != null && list.Count > 0)
            {               
                StringBuilder html = new StringBuilder();
                html.AppendLine("<p  class=\"pText\"><input id=\"chkBrandAll\" type=\"checkbox\" value=\"-1\" onclick=\"brandSelectAll(this);\"/><label for=\"chkBrandAll\">全部</label></p>");
                foreach (var item in list)
                {
                    html.AppendFormat("<p id=\"p_chkBrand_{1}\" title=\"{3}\"  class=\"pText\"><input id=\"chkBrand_{1}\" name=\"chkBrand\" type=\"checkbox\" onclick=\"fnCheckChange(1,{1})\" value=\"{1}\" /><label for=\"chkBrand_{1}\">{2}</label></p>{0}", Environment.NewLine, item.BrandID.Value, item.BrandName.Length > 7 ? item.BrandName.Substring(0, 7) : item.BrandName, item.BrandName);
                }
                var brandJSON = list.ToJSON();
                html.AppendLine("<script type=\"text/javascript\"> var __brands=" + brandJSON + "; </script>");
                //
                this.ltBrandList.Mode = LiteralMode.PassThrough;
                this.ltBrandList.Text = html.ToString();
            }
        }
        #endregion

        #region 生成品类列表
        private void CreateCategoryList()
        {
            //CategoryBLL bll = new CategoryBLL(this.GetCurrentUserInfo());
            CategoryEntity categoryQueryEntity = new CategoryEntity();
            categoryQueryEntity.IsLeaf = -1;
            //var list = bll.QueryByEntity(categoryQueryEntity, null);
            var list = BrandSKUTreeHandler.GetCategoryList(categoryQueryEntity);
            if (list != null && list.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.AppendLine("<p  class=\"pText\"><input id=\"chkCategoryAll\" type=\"checkbox\" value=\"-1\"  onclick=\"categorySelectAll(this);\"/><label for=\"chkCategoryAll\">全部</label></p>");
                foreach (var item in list)
                {
                    html.AppendFormat("<p id=\"p_chkCategory_{1}\" title=\"{3}\"   class=\"pText\"><input id=\"chkCategory_{1}\" onclick=\"fnCheckChange(2,{1})\" name=\"chkCategory\" type=\"checkbox\" value=\"{1}\" /><label for=\"chkCategory_{1}\">{2}</label></p>{0}", Environment.NewLine, item.CategoryID.Value, item.CategoryName.Length > 7 ? item.CategoryName.Substring(0, 7) : item.CategoryName, item.CategoryName);
                }
                var categoryJSON = list.ToJSON();
                html.AppendLine("<script type=\"text/javascript\"> var __categories=" + categoryJSON + "; </script>"); 
                //
                this.ltCategoryList.Mode = LiteralMode.PassThrough;
                this.ltCategoryList.Text = html.ToString();
            }
        }
        #endregion

        #region 生成产品列表
        private void CreateSKUList()
        {
            //SKUBLL bll = new SKUBLL(this.GetCurrentUserInfo(), true);
            SKUEntity skuQueryEntity = new SKUEntity();
            skuQueryEntity.IsMain = -1;
            //var list = bll.QueryByEntity(skuQueryEntity, null);
            var list = BrandSKUTreeHandler.GetSKUList(skuQueryEntity);
            if (list != null && list.Count > 0)
            {
                StringBuilder html = new StringBuilder();
                html.AppendLine("<p  class=\"pText\"><input id=\"chkSKUAll\" type=\"checkbox\" value=\"-1\"  onclick=\"skuSelectAll(this);\"/><label for=\"chkSKUAll\">全部</label></p>");
                foreach (var item in list)
                {
                    html.AppendFormat("<p id=\"p_chkSKU_{1}\" title=\"{3}\"   class=\"pText\"><input id=\"chkSKU_{1}\" onclick=\"fnCheckChange(3,{1})\" name=\"chkSKU\" type=\"checkbox\" value=\"{1}\" /><label for=\"chkSKU_{1}\">{2}</label></p>{0}", Environment.NewLine, item.SKUID.Value, item.SKUName.Length > 7 ? item.SKUName.Substring(0, 7) : item.SKUName, item.SKUName);
                }
                var skuJSON = list.ToJSON();
                html.AppendLine("<script type=\"text/javascript\"> var __skus=" + skuJSON + "; </script>");  
                //__skus.length;
                //
                this.ltSKUList.Mode = LiteralMode.PassThrough;
                this.ltSKUList.Text = html.ToString();
            }
        }
        #endregion

        #region 渠道信息
        private void GetChannelList()
        {
            //ControlChannelEntity[] entity = new ControlBLL(this.GetCurrentUserInfo()).GetChannelByClientID("", -1);
            var entity = BrandSKUTreeHandler.GetChannelByClientID("", -1);
            List<ControlChannelEntity> lstChannel = new List<ControlChannelEntity>();
            foreach (var channelItem in entity)
            {
                if (channelItem.IsLeaf.HasValue && channelItem.IsLeaf.Value==-1)
                    lstChannel.Add(channelItem);
            }
            var channelJSON = lstChannel.ToJSON();
            StringBuilder html = new StringBuilder();
            html.AppendLine("<script type=\"text/javascript\"> var __channels=" + channelJSON + "; </script>");
            //__skus.length;
            //
            this.ltChannel.Mode = LiteralMode.PassThrough;
            this.ltChannel.Text = html.ToString();

            //drpChannel.DataSource = lstChannel.ToArray() ;            
            //drpChannel.DataTextField = "ChannelName";
            //drpChannel.DataValueField = "ChannelID";
            //drpChannel.DataBind();
            //drpChannel.Items.Insert(0,new ListItem("所有渠道",""));
        }
        #endregion

        #region 连锁信息
        private void GetChainList()
        {
            //ControlChainEntity[] entity = new ControlBLL(this.GetCurrentUserInfo()).GetChainByClientID("", -1);
            var entity = BrandSKUTreeHandler.GetChainByClientID("", -1);
            drpChain.DataSource = entity;
            drpChain.DataTextField = "ChainName";
            drpChain.DataValueField = "ChainID";
            drpChain.DataBind();
            drpChain.Items.Insert(0, new ListItem("--请选择--", ""));
        }
        #endregion
    }
}