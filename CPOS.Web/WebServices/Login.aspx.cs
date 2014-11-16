using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity.User;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.WebServices
{
    public partial class Login : System.Web.UI.Page
    {
       public string webUrl = ConfigurationManager.AppSettings["website_url3"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["UserCode"] != null && Request["Password"] != null)
            {
                string res = LoginEx("{UserCode:\"" + Request["UserCode"] + "\",Password:\"" + Request["Password"] + "\"}");
                Response.Clear();
                Response.Write(res);
                Response.End();
            }
        }
        public string LoginEx(string requestJSONStr)
        {
            var reqContent = requestJSONStr.DeserializeJSONTo<ReqContent>();

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["Conn"]))
            {
                conn.Open();
                SqlDataAdapter adapter =
                    new SqlDataAdapter(
                        string.Format("SELECT * FROM T_User where user_code='{0}' and user_password='{1}'",
                                      reqContent.UserCode, reqContent.Password), conn);
                adapter.Fill(ds);
                if (ds.Tables.Count > 0)
                {

                    List<UserInfo> userInfos = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]);
                    if (userInfos.Count == 1)
                    {
                        return new ResContent
                        {
                            code = "1",
                            description = "登录成功",
                            UserName = userInfos[0].User_Name,
                            UserID = userInfos[0].User_Id,
                            Password = userInfos[0].User_Password,
                            ClientID = userInfos[0].customer_id,
                            MediaPath = webUrl + ":8100/" + userInfos[0].customer_id + "/"
                        }.ToJSON();
                    }
                    return new ResContent { code = "-1", description = "登录失败" }.ToJSON();
                }
                conn.Close();
            }

            return new ResContent { code = "-1", description = "登录失败" }.ToJSON();
        }
    }
    public class ReqContent
    {
        public string UserCode;
        public string Password;
    }
    public class ResContent
    {
        public string code;
        public string description;
        public string UserName;
        public string Password;
        public string MediaPath;
        public string ClientID;
        public string UserID;

    }
}