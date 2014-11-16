using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Extension;
using System.Globalization;
using System.Threading;

namespace JIT.CPOS.BS.Web.Module.VisitingPlan.CallDayPlanning
{
    public partial class CallDayPlanningUserDate : JIT.CPOS.BS.Web.PageBase.JITChildPage
    {
        CallDayPlanningViewEntity_UserDate[] list = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    return;
            //}

            if (!string.IsNullOrEmpty(Request.QueryString["btncode"]))
            {
                switch (Request.QueryString["btncode"])
                {
                    case "search":
                        GetUserCDP();
                        break;
                    case "update":
                        GetUserCDP();
                        break;
                }
            }            
        }

        #region GetUserCDP
        protected void GetUserCDP()
        {
            CallDayPlanningViewEntity_UserDate entity = new CallDayPlanningViewEntity_UserDate();
            entity.ClientUserID = Request.QueryString["ClientUserID"].ToInt();
            entity.CallDate = Request.QueryString["CallDate"].ToDateTime();
            if (!string.IsNullOrEmpty(Request.QueryString["ClientStructureID"]))
            {
                entity.ClientStructureID = Guid.Parse(Request.QueryString["ClientStructureID"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ClientPositionID"]))
            {
                entity.ClientPositionID = Request.QueryString["ClientPositionID"].ToInt();
            }

            userCalendar.VisibleDate = entity.CallDate.Value;
            list = new CallDayPlanningBLL(CurrentUserInfo).GetUserCDP(entity);

        }
        #endregion

        #region userCalendar_OnPreRender
        protected void userCalendar_OnPreRender(object sender, EventArgs e)
        {
            //setting the Calendar culture!
            CultureInfo culture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = culture;
        }
        #endregion
        
        #region userCalendar_DayRender
        /// <summary>
        /// 日期渲染
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void userCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            
            if (list != null && list.Length > 0)
            {
                var query = list.Where(i => 
                    i.CallDate.Value.Year == e.Day.Date.Year 
                    && i.CallDate.Value.Month == e.Day.Date.Month 
                    && i.CallDate.Value.Day == e.Day.Date.Day);
                if (query.ToArray().Length == 1)
                {
                    e.Cell.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Cell.ToolTip += "(" + query.ToArray()[0].POPCount.ToString() + ")";
                    e.Cell.Attributes.Add("onclick", "javascript:fnViewUserDetail('" + e.Day.Date.ToString("yyyy-MM-dd") + "')");
                    e.Cell.Attributes.Add("style", "cursor:pointer;");
                    //e.Cell.Font.Bold = true; 
                    e.Cell.Text = e.Day.Date.Day + "<br>(" + query.ToArray()[0].POPCount.ToString() + ")";
                    return;
                }
            }

            DateTime callDate = Request.QueryString["CallDate"].ToDateTime();
            if (callDate.Year == e.Day.Date.Year
                   && callDate.Month == e.Day.Date.Month)
            {
                e.Cell.Attributes.Add("onclick", "javascript:;");
                e.Cell.Text = e.Day.Date.Day.ToString();
            }
            else
            {
                e.Cell.Attributes.Add("onclick", "javascript:;");
                e.Cell.Text = e.Day.Date.Day.ToString();
                e.Cell.Attributes.Add("class", "monthother");
            }
            
        }
        #endregion

    }
}