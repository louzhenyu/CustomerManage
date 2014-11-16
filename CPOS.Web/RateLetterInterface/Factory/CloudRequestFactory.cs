using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace JIT.CPOS.Web.RateLetterInterface
{
    enum EBodyType : uint
    {
        EType_XML = 0,
        EType_JSON
    };

    /// <summary>
    /// 云通讯第三方请求逻辑处理类。 
    /// </summary>
    public class CloudRequestFactory
    {
        #region  初始化
        /// <summary>
        /// 初始化云通讯第三方请求逻辑处理类。
        /// </summary>
        public CloudRequestFactory()
        {
        }
        #endregion

        /// <summary>
        /// 服务器api版本
        /// </summary>
        const string m_softVer = "2013-12-26";

        #region  构造post请求
        /// <summary>
        /// 云通讯HttpPost接口封装。
        /// </summary>
        /// <param name="url">请求路径</param>
        /// <param name="date">日期</param>
        /// <param name="account">账号：子账号或者主账号</param>
        /// <param name="data">请求数据
        /// </param>
        /// <returns></returns>
        private string CreateHttpPost(string url, string date, string account, string data, EBodyType eType)
        {
            try
            {
                Uri address = new Uri(url);

                // 创建网络请求  
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                setCertificateValidationCallBack();

                // 构建Head
                request.Method = "POST";

                Encoding myEncoding = Encoding.GetEncoding("utf-8");
                byte[] myByte = myEncoding.GetBytes(account + ":" + date);
                string authStr = Convert.ToBase64String(myByte);
                request.Headers.Add("Authorization", authStr);


                // 构建Body
                if (eType == EBodyType.EType_XML)
                {
                    request.Accept = "application/xml";
                    request.ContentType = "application/xml;charset=utf-8";
                }
                else
                {
                    request.Accept = "application/json";
                    request.ContentType = "application/json;charset=utf-8";
                }

                byte[] byteData = UTF8Encoding.UTF8.GetBytes(data);

                // 开始请求
                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                }

                // 获取请求
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string responseStr = reader.ReadToEnd();

                    if (responseStr != null && responseStr.Length > 0)
                    {
                        return responseStr;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return "";
        }
        #endregion

        #region  创建子账号。
        /// <summary>
        /// 创建子帐号
        /// </summary>
        /// <param name="restAddress">服务器地址</param>
        /// <param name="restPort">服务器端口</param>
        /// <param name="accountSid">主帐号</param>
        /// <param name="accountToken">主帐号令牌</param>
        /// <param name="friendlyName">关联用户</param>
        /// <returns>包体内容</returns>
        public string CreateSubAccount(string restAddress, string restPort, string mainAccount, string mainToken, string appId, string friendlyName)
        {
            if (friendlyName == null)
                throw new ArgumentNullException("friendlyName");

            try
            {
                string date = DateTime.Now.ToString("yyyyMMddhhmmss");

                // 构建URL内容
                string sigstr = MD5Encrypt(mainAccount + mainToken + date);
                string uriStr = string.Format("https://{0}:{1}/{2}/Accounts/{3}/SubAccounts?sig={4}", restAddress, restPort, m_softVer, mainAccount, sigstr);

                StringBuilder data = new StringBuilder();
                data.Append("{");
                data.Append("\"appId\":\"").Append(appId).Append("\"");
                data.Append(",\"friendlyName\":\"").Append(friendlyName).Append("\"");
                data.Append("}");

                string responseStr = CreateHttpPost(uriStr, date, mainAccount, data.ToString(), EBodyType.EType_JSON);

                return responseStr;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

        #region  用户申请加入群组
        /// <summary>
        /// 用户申请加入群组
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="groupId">群组Id</param>
        /// <param name="declared">申请理由</param>
        /// <returns>
        /// 请求状态码，取值000000（成功）
        /// 
        /// </returns>
        public FoundationsViewModel JoinGroup(string restAddress, string restPort, string subAccountSid, string subToken, string groupId, string declared)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            ///{SoftVersion}/SubAccounts/{subAccountSid}/Group/JoinGroup
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/JoinGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            sb.Append("<Request>");
            sb.Append("<groupId>").Append(groupId).Append("</groupId>");
            sb.Append("<declared>").Append(declared).Append("</declared>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            FoundationsViewModel viewModel = XMLDeserialize(typeof(FoundationsViewModel), responseStr) as FoundationsViewModel;

            return viewModel;
        }

        #endregion

        #region  群组管理员删除成员（群主踢人）
        /// <summary>
        /// 群组管理员删除成员
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="groupId">群组ID</param>
        /// <param name="members">被邀请成员列表</param>
        /// <param name="member">被邀请成员</param>
        /// <returns>
        /// statusCode:请求状态码，取值000000（成功）
        /// </returns>
        public FoundationsViewModel DeleteGroupMember(string restAddress, string restPort, string subAccountSid, string subToken, string groupId, List<string> members)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            ///{SoftVersion}/SubAccounts/{subAccountSid}/Group/DeleteGroupMember
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/DeleteGroupMember?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<groupId>").Append(groupId).Append("</groupId>");
            sb.Append(" <members>");  //member 填写：VoIP号码
            if (members != null && members.Count > 0)
            {
                foreach (var item in members)
                {
                    sb.Append("<member>").Append(item).Append("</member>");
                }
            }
            sb.Append("</members>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            FoundationsViewModel viewModel = XMLDeserialize(typeof(FoundationsViewModel), responseStr) as FoundationsViewModel;

            return viewModel;
        }

        #endregion

        #region  成员主动退出群组
        /// <summary>
        /// 成员主动退出群组
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="groupId">群组ID</param>
        /// <returns>
        /// statusCode:请求状态码，取值000000（成功）
        /// </returns>
        public FoundationsViewModel LogoutGroup(string restAddress, string restPort, string subAccountSid, string subToken, string groupId)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/LogoutGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<groupId>").Append(groupId).Append("</groupId>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            FoundationsViewModel viewModel = XMLDeserialize(typeof(FoundationsViewModel), responseStr) as FoundationsViewModel;

            return viewModel;
        }

        #endregion

        #region  群组管理员邀请用户加入群组(群主拉人)
        /// <summary>
        /// 群组管理员邀请用户加入群组(用户加人)
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="groupId">群组ID</param>
        /// <param name="members">被邀请成员列表</param>
        /// <param name="member">被邀请成员</param>
        /// <param name="confirm">是否需要被邀请人确认 0 ：需要 1：不需要（自动加入群组）</param>
        /// <param name="declared">邀请理由</param>
        /// <returns></returns>
        public FoundationsViewModel InviteJoinGroup(string restAddress, string restPort, string subAccountSid, string subToken, string groupId, List<string> members, string confirm, string declared)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            ///{SoftVersion}/SubAccounts/{subAccountSid}/Group/InviteJoinGroup
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/InviteJoinGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<groupId>").Append(groupId).Append("</groupId>");
            sb.Append(" <members>");  //member 填写：VoIP号码
            if (members != null && members.Count > 0)
            {
                foreach (var item in members)
                {
                    sb.Append("<member>").Append(item).Append("</member>");
                }
            }
            sb.Append("</members>");
            sb.Append("<confirm>").Append(confirm).Append("</confirm>");
            sb.Append("<declared>").Append(declared).Append("</declared>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            FoundationsViewModel viewModel = XMLDeserialize(typeof(FoundationsViewModel), responseStr) as FoundationsViewModel;

            return viewModel;
        }

        #endregion

        #region  查询群组属性
        /// <summary>
        /// 查询群组属性
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="groupId">群组ID</param>
        /// <returns>
        /// 群组的详细信息。
        /// 
        /// </returns>
        public ResponseViewModel QueryGroupDetail(string restAddress, string restPort, string subAccountSid, string subToken, string groupId)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            ///{SoftVersion}/SubAccounts/{subAccountSid}/Group/QueryGroupDetail
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/QueryGroupDetail?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<groupId>").Append(groupId).Append("</groupId>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            var viewModel = (ResponseViewModel)XMLDeserialize(typeof(ResponseViewModel), responseStr);

            return viewModel;
        }

        #endregion

        #region  删除群组
        /// <summary>
        /// 删除群组
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="groupId">群组ID</param>
        /// <returns>
        /// 请求状态码，取值000000（成功）
        /// </returns>
        public FoundationsViewModel DeleteGroup(string restAddress, string restPort, string subAccountSid, string subToken, string groupId)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            ///{SoftVersion}/SubAccounts/{subAccountSid}/Group/DeleteGroup
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/DeleteGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<groupId>").Append(groupId).Append("</groupId>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            FoundationsViewModel viewModel = XMLDeserialize(typeof(FoundationsViewModel), responseStr) as FoundationsViewModel;

            return viewModel;
        }

        #endregion

        #region  修改群组属性
        /// <summary>
        /// 修改群组属性
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="groupId">群组ID</param>
        /// <param name="name">群组名字，最长50个字符</param>
        /// <param name="permission">申请加入模式 0：默认直接加入1：需要身份验证 2:私有群组</param>
        /// <param name="declared">群组公告，最长为200个字符</param>
        /// <returns></returns>
        public FoundationsViewModel ModifyGroup(string restAddress, string restPort, string subAccountSid, string subToken, string groupId, string name, string permission, string declared)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/ModifyGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<groupId>").Append(groupId).Append("</groupId>");
            sb.Append("<name>").Append(name).Append("</name>");
            sb.Append("<declared>").Append(declared).Append("</declared>");
            sb.Append("<permission>").Append(permission).Append("</permission>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            FoundationsViewModel viewModel = XMLDeserialize(typeof(FoundationsViewModel), responseStr) as FoundationsViewModel;

            return viewModel;
        }

        #endregion

        #region  创建群组
        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="restAddress">服务器地址</param>
        /// <param name="restPort">端口</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="mainToken">子账号的授权令牌</param>
        /// <param name="name">群组名字，最长为50个字符</param>
        /// <param name="type">群组类型 0：临时组(上限100人)  1：普通组(上限300人)  2：VIP组 (上限500人)</param>
        /// <param name="permission">申请加入模式 0：默认直接加入1：需要身份验证 2:私有群组</param>
        /// <param name="declared">群组公告，最长为200个字符</param>
        /// <returns></returns>
        public CreateGroupViewModel CreateGroup(string restAddress, string restPort, string subAccountSid, string subToken, string name, string type, string permission, string declared)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/CreateGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<name>").Append(name).Append("</name>");
            sb.Append("<type>").Append(type).Append("</type>");
            sb.Append("<permission>").Append(permission).Append("</permission>");
            sb.Append("<declared>").Append(declared).Append("</declared>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            CreateGroupViewModel viewModel = XMLDeserialize(typeof(CreateGroupViewModel), responseStr) as CreateGroupViewModel;

            return viewModel;
        }
        #endregion

        #region 查询成员所加入的组
        /// <summary>
        /// 查询成员所加入的组
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号: 当前用户的子账号</param>
        /// <param name="subToken">子账号Token：当前用户</param>
        /// <returns></returns>
        public QueryGroupViewModel QueryGroup(string restAddress, string restPort, string subAccountSid, string subToken)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            //{SoftVersion}/SubAccounts/{subAccountSid}/Member/QueryGroup
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Member/QueryGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);
            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, "", EBodyType.EType_XML);
            QueryGroupViewModel viewModel = XMLDeserialize(typeof(QueryGroupViewModel), responseStr) as QueryGroupViewModel;

            return viewModel;
        }
        #endregion

        #region   修改用户群名片信息（修改在群中的名片）  未完成
        /// <summary>
        /// 修改群名片信息
        /// </summary>
        /// <param name="restAddress">地址</param>
        /// <param name="restPort">端口号</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="subToken">子账号Token</param>
        /// <param name="display">用户名字，上限为20字符</param>
        /// <param name="belong">用户所属的群组ID</param>
        /// <param name="mail">用户邮箱</param>
        /// <param name="tel">用户电话</param>
        /// <param name="remark">用户备注Token</param>
        /// <returns>
        /// 请求状态码，取值000000（成功）
        /// </returns>
        public string ModifyCard(string restAddress, string restPort, string subAccountSid, string subToken, string display, string belong, string mail, string tel, string remark)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            //{SoftVersion}/SubAccounts/{subAccountSid}/Member/QueryGroup
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Member/QueryGroup?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);
            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, "", EBodyType.EType_XML);
            // QueryGroupViewModel viewModel = XMLDeserialize(typeof(QueryGroupViewModel), responseStr) as QueryGroupViewModel;

            return responseStr;
        }
        #endregion

        #region  按条件搜索公共群组  未完成
        /// <summary>
        /// 按条件搜索公共群组
        /// </summary>
        /// <param name="restAddress">服务器地址</param>
        /// <param name="restPort">端口</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="mainToken">子账号的授权令牌</param>
        /// <param name="groupId">根据群组ID查找（模糊查询，同时具备两个条件，查询以此为先）</param>
        /// <param name="name">根据群组名查找（模糊查询，结果集中不包含私有群组）</param>
        /// <returns></returns>
        public string SearchPublicGroups(string restAddress, string restPort, string subAccountSid, string subToken, string groupId, string name)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            //{SoftVersion}/SubAccounts/{subAccountSid}/Group/SearchPublicGroups
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/SearchPublicGroups?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            //sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            //sb.Append("<name>").Append(name).Append("</name>");
            //sb.Append("<type>").Append(type).Append("</type>");
            //sb.Append("<permission>").Append(permission).Append("</permission>");
            //sb.Append("<declared>").Append(declared).Append("</declared>");
            //sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            FoundationsViewModel viewModel = XMLDeserialize(typeof(FoundationsViewModel), responseStr) as FoundationsViewModel;
            //if (viewModel != null)
            //{
            //    if (viewModel.statusCode == MessageStatusCode.Success)
            //    {
            //        return true;
            //    }
            //}

            //return false;

            return "";
        }

        #endregion

        #region 获取所有公共群组
        /// <summary>
        /// 获取所有公共群组
        /// </summary>
        /// <param name="restAddress">服务器地址</param>
        /// <param name="restPort">端口</param>
        /// <param name="subAccountSid">子账号</param>
        /// <param name="mainToken">子账号的授权令牌</param>
        /// <param name="lastUpdateTime">上一次更新的时间戳 ms（用于分页，最大返回50条数据）</param>
        /// <returns>
        /// 
        /// </returns>
        public PublicGroups GetPublicGroups(string restAddress, string restPort, string subAccountSid, string subToken, string lastUpdateTime)
        {
            string date = DateTime.Now.ToString("yyyyMMddhhmmss");

            // 构建URL内容
            string sigstr = MD5Encrypt(subAccountSid + subToken + date);
            string uriStr = string.Format("https://{0}:{1}/{2}/SubAccounts/{3}/Group/GetPublicGroups?sig={4}", restAddress, restPort, m_softVer, subAccountSid, sigstr);

            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version='1.0' encoding='UTF-8'?> <Request>");
            sb.Append("<lastUpdateTime>").Append(lastUpdateTime).Append("</lastUpdateTime>");
            sb.Append("</Request> ");

            string responseStr = CreateHttpPost(uriStr, date, subAccountSid, sb.ToString(), EBodyType.EType_XML);
            PublicGroups viewModel = XMLDeserialize(typeof(PublicGroups), responseStr) as PublicGroups;

            return viewModel;
        }
        #endregion



        #region  辅助方法
        /// <summary>  
        /// 将XML序列化为对象。
        /// </summary>  
        /// <param name="type">类型</param>  
        /// <param name="strXml">XML字符串</param>  
        /// <returns></returns>  
        private object XMLDeserialize(Type type, string strXml)
        {
            try
            {
                using (StringReader sr = new StringReader(strXml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">原内容</param>
        /// <returns>加密后内容</returns>
        public static string MD5Encrypt(string source)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(source));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// 设置服务器证书验证回调
        /// </summary>
        public void setCertificateValidationCallBack()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = CertificateValidationResult;
        }


        /// <summary>
        ///  证书验证回调函数  
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cer"></param>
        /// <param name="chain"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool CertificateValidationResult(object obj, System.Security.Cryptography.X509Certificates.X509Certificate cer, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            return true;
        }

        #endregion
    }
}