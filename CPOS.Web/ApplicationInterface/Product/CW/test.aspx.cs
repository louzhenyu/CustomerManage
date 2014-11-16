using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JIT.CPOS.Web.ApplicationInterface.Product.CW;

namespace JIT.CPOS.Web.ApplicationInterface.Product.CW
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ret = null;   //返回的值。
            CloudRequestFactory factory = new CloudRequestFactory();
            //bool isInit = factory.init("sandboxapp.cloopen.com", "8883");
            //factory.setAccount("852054485b3211e3b2e4d89d672afa5c", "6962cc5c44eac08bad74e6a8d496bede");
            // factory.setAppId("80147000000011");

            try
            {
                Dictionary<string, object> retData = factory.CreateSubAccount("sandboxapp.cloopen.com", "8883", "ff8080813bbcae3f013bcc39c18a0022", "8f32e2023d804e1390a3b0b8b36d6e28", "aaf98f893e7df943013e8728b2b400c7", "kongfanjun1234567");
                ret = getDictionaryData(retData);
            }
            catch (Exception exc)
            {
                ret = exc.Message;
            }
            finally
            {
                Response.Write(ret);
            }
        }

        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }
    }
}