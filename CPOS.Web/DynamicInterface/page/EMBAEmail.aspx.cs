using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.DynamicInterface.page
{
    public partial class EMBAEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var loggingSessionInfo = Default.GetBSLoggingSession("75a232c2cf064b45b1b6393823d2431e", "1");
            if (!string.IsNullOrEmpty(Request["UserID"]))
            {
                string UserID = Request["UserID"].ToString();
                DataSet ds = new DynamicInterfaceBLL(loggingSessionInfo).GetUserInfo(UserID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    lt_Col1.Text = dr["VipSchool"].ToString();               
                    lt_Col2.Text = dr["VipCourse"].ToString();
                    lt_Col3.Text = dr["Col3"].ToString();
                    lt_Col4.Text = dr["Col4"].ToString();
                    lt_Col5.Text = dr["Col5"].ToString();
                    lt_Col6.Text = dr["Col6"].ToString();
                    lt_Col7.Text = dr["Col7"].ToString();
                    lt_Col8.Text = dr["Col8"].ToString();
                    lt_Col9.Text = dr["IsMarital"].ToString();
                    lt_Col10.Text = dr["Col10"].ToString();
                    lt_Col11.Text = dr["Col11"].ToString();
                    lt_Col12.Text = dr["Col2"].ToString();
                    lt_Col13.Text = dr["Col3"].ToString();
                    lt_Phone.Text = dr["Phone"].ToString();
                    lt_Birthday.Text = dr["Birthday"].ToString();
                    lt_Email.Text = dr["Email"].ToString();
                    lt_VipName.Text = dr["VipName"].ToString();
                    lt_SinaMBlog.Text = dr["SinaMBlog"].ToString();
                    lt_WeiXin.Text = dr["WeiXin"].ToString();
                }
            }
        }
    }
}