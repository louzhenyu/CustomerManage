using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JIT.CPOS.Web.Lj
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            SaturnServiceReference.SaturnServiceSoapClient client = new SaturnServiceReference.SaturnServiceSoapClient();
            var result = client.getAuthenticationCode("{\"common\":{\"userId\":\"admin\",\"version\":\"3.1\"},\"special\":{\"authCode\":\"123\",\"captchaCode\":\"321\"}}");
            Response.Write(result);

            //"{\"code\":\"200\",\"description\":\"操作成功\",\"content\":{\"itemName\":\"1573\",\"norm\":\"2.5L\",\"alcohol\":\"68度\",\"baseWineYear\":\"3年\",\"agePitPits\":\"300年\",\"barcode\":\"LJ2013112823132\",\"isAuthCode\":\"1\",\"categoryName\":\"白酒\",\"categoryId\":\"大师酒\",\"brandName\":\"国窖1573\",\"dealerName\":\"\",\"dealerId\":\"\",\"imageList\":{\"imageURL\":\"\",\"displayIndex\":\"\"},\"wineDescList\":{\"wineDesc\":\"介绍信息\",\"displayIndex\":\"\"}}}"
        }

        private void GetAuthenticationInfo(string UserId, string authCode, string captchaCode)
        {
            string para = "{\"common\":{\"userId\":\"" + UserId + "\",\"version\":\"3.1\"},\"special\":{\"authCode\":\"" + authCode + "\",\"captchaCode\":\"" + captchaCode + "\"}}";
            
        }
    }
}