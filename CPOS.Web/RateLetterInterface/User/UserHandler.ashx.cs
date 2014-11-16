using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.Web.ApplicationInterface;
using JIT.Utility.ExtensionMethod;
using JIT.Utility;
using JIT.Utility.Log;
using System.IO;
using System.Configuration;
using JIT.CPOS.BS.BLL.Product.Eclub.Module;
using CposCommon = JIT.CPOS.Common;
using System.Net;
using System.Globalization;
using JIT.CPOS.Web.RateLetterInterface.PG;
using JIT.CPOS.Web.ApplicationInterface.Project.HuaAn;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.Web.RateLetterInterface.Common;

namespace JIT.CPOS.Web.RateLetterInterface.User
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : BaseGateway
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pType"></param>
        /// <param name="pAction"></param>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            var rst = string.Empty;
            switch (pAction)
            {  //1.0宝洁登录
                case "PGLogin":
                    rst = PGLogin(pRequest);
                    break;
                //1.1登录
                case "Login":
                    rst = Login(pRequest);
                    break;
                //1.2获取用户列表
                case "GetUsers":
                    rst = GetUsers(pRequest);
                    break;
                //1.3获取用户信息
                case "GetUser":
                    rst = GetUser(pRequest);
                    break;
                //1.4密码修改
                case "ModifyPassword":
                    rst = ModifyPassword(pRequest);
                    break;
                //1.5用户信息修改
                case "ModifyUser":
                    rst = ModifyUser(pRequest);
                    break;
                //1.6头像修改
                case "ModifyHighImage":
                    rst = ModifyHighImage(pRequest);
                    break;
                //1.7获取通知
                case "GetNotices":
                    rst = GetNotices(pRequest);
                    break;
                //1.8提交用户反馈
                case "SubmitUserFeedback":
                    rst = SubmitUserFeedback(pRequest);
                    break;
                case "ExportUser"://导入用户
                    rst = ExportUser(pRequest);
                    break;
                case "ThirdPartyRegisterUser"://注册云通讯帐户
                    rst = ThirdPartyRegisterUser(pRequest);
                    break;
                case "SingleRegister"://单用户注册
                    rst = SingleRegister(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("未实现Action名为{0}的处理.", pAction)) { ErrorCode = 201 };
            }
            return rst;
        }

        #region 1.0宝洁登录
        private string PGLogin(string reqContent)
        {
            var rd = new APIResponse<PGUserInfoRD>();
            var rdData = new PGUserInfoRD();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<PGUserInfoRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

                #region token验证
                ////string token = "602B02EEB467C397F68D72FE014A89A3";
                //string token = rp.Parameters.PGToken;
                ////1.1调用宝洁服务器验证token的有效性
                //PGHelper pg = new PGHelper();
                //SsoVerify sso = pg.SsoVerify(token);

                ////记录日志
                //Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = sso.ToJSON() });

                //if (sso.error.err_code != "0")
                //{
                //    rd.Data = rdData;
                //    rd.ResultCode = 321;
                //    rd.Message = "宝洁系统登录验证失败";
                //    return rd.ToJSON();
                //}

                //#region 设置power hour cookie值
                ////设置power hour cookie值
                //SetPGCookie(token);
                //#endregion
                #endregion

                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                //DataSet ds = bll.GetUserInfoByEmail(rp.Parameters.Email);
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                DataSet ds = bll.LoginPGUser(rp.CustomerID, rp.Parameters.Email, string.Empty, typeID);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var user = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]).FirstOrDefault();
                    //if (user.user_password.Equals(rp.Parameters.Password))  //MD5Helper.Encryption()
                    //{
                    var userID = user.user_id;
                    //验证是否在第三方注册
                    TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(loggingSessionInfo);
                    TUserThirdPartyMappingEntity tutpmEntiy = tutpmBll.GetByID(userID);
                    UserViewModel userinfo = null;
                    if (tutpmEntiy == null)
                    {
                        ThirdUserViewModel userViewModel = null;
                        CloudRequestFactory factory = new CloudRequestFactory();
                        //调用云通讯创建子账户
                        //string retData = factory.CreateSubAccount("sandboxapp.cloopen.com", "8883", "ff8080813bbcae3f013bcc39c18a0022", "8f32e2023d804e1390a3b0b8b36d6e28", "aaf98f893e7df943013e8728b2b400c7", userID);
                        string retData = factory.CreateSubAccount(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, YunTongXunAppSetting.MainAccount, YunTongXunAppSetting.MainToken, YunTongXunAppSetting.AppID, userID);
                        //string jsonData = getDictionaryData(retData);
                        userViewModel = CWHelper.Deserialize<ThirdUserViewModel>(retData);
                        if (userViewModel.statusCode == MessageStatusCode.Success)
                        {
                            tutpmEntiy = CreateThirdUser(tutpmEntiy, userViewModel, userID);
                            tutpmBll.Create(tutpmEntiy);
                        }
                        userinfo = new UserViewModel
                        {
                            UserID = userID,
                            UserName = user.user_name,
                            HighImageUrl = user.HighImageUrl,
                            VoipAccount = userViewModel.SubAccount.voipAccount,
                            VoipPwd = userViewModel.SubAccount.voipPwd,
                            SubAccountSid = userViewModel.SubAccount.subAccountSid,
                            SubToken = userViewModel.SubAccount.subToken
                        };
                    }
                    else
                    {
                        userinfo = new UserViewModel
                        {
                            UserID = userID,
                            UserName = user.user_name,
                            HighImageUrl = user.HighImageUrl,
                            VoipAccount = tutpmEntiy.VoipAccount,
                            VoipPwd = tutpmEntiy.VoipPwd,
                            SubAccountSid = tutpmEntiy.SubAccountSid,
                            SubToken = tutpmEntiy.SubToken
                        };
                    }

                    //设置用户性别，邮箱，手机号，部门，职务，权限名称,生日,电话
                    userinfo.UserGender = user.user_gender;
                    userinfo.UserEmail = user.user_email;
                    userinfo.UserTelephone = user.user_telephone;
                    userinfo.Dept = user.Dept;
                    userinfo.JobFunc = user.JobFunc;
                    userinfo.RoleName = user.RoleName;
                    userinfo.UserBirthday = user.user_birthday;
                    userinfo.UserCellphone = user.user_cellphone;
                    //创建讨论组权限 key:VIP020000
                    DataSet dsUserRight = GetUserRights(userinfo.UserID, rp.CustomerID, loggingSessionInfo);
                    userinfo.CreateGroupRight = IsHasCreateGroupJuri(rp.UserID, rp.CustomerID, UserRightCode.USER_CREATE_GROUP_RIGHT_CODE, dsUserRight);
                    //上级邮箱
                    userinfo.SuperiorName = user.SuperiorName;

                    rdData.UserInfo = userinfo;
                    rd.Data = rdData;
                    rd.Message = "登录成功";
                    //}
                    //else
                    //{
                    //    rd.ResultCode = 300;
                    //    rd.Message = "密码错误";
                    //}
                }
                else
                {
                    rd.ResultCode = 300;
                    rd.Message = "邮箱不存在";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rd.ToJSON();
        }
        #endregion

        #region  1.1登录
        /// <summary>
        /// 登录Action。 
        ///1. 验证用户信息
        ///2. 登录 
        // 登录： 1)如果登录成功，验证则调用云通讯接口：创建子账号。 并将返回的信息保存在（2050用户与第三方用户关联表TUserThirdPartyMapping)TUserThirdPartyMapping中，
        // 
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string Login(string reqContent)
        {
            var rd = new APIResponse<UserInfoRD>();
            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<UserInfoRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                //DataSet ds = bll.GetUserInfoByEmail(rp.Parameters.Email);
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                //DataSet ds = bll.GetUserInfo(rp.CustomerID, rp.Parameters.Email, string.Empty, string.Empty, string.Empty, 0, 1000, typeID);
                DataSet ds = bll.LoginQiXinUser(rp.CustomerID, rp.Parameters.Email, string.Empty, typeID);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    var user = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]).FirstOrDefault();
                    if (user.user_password.Equals(rp.Parameters.Password))  //MD5Helper.Encryption()
                    {
                        var userID = user.user_id;

                        //验证是否在第三方注册
                        TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(loggingSessionInfo);
                        TUserThirdPartyMappingEntity tutpmEntiy = tutpmBll.GetByID(userID);
                        UserViewModel userinfo = null;
                        if (tutpmEntiy == null)
                        {
                            ThirdUserViewModel userViewModel = null;
                            CloudRequestFactory factory = new CloudRequestFactory();
                            //调用云通讯创建子账户
                            string retData = factory.CreateSubAccount(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, YunTongXunAppSetting.MainAccount, YunTongXunAppSetting.MainToken, YunTongXunAppSetting.AppID, userID);
                            //string jsonData = getDictionaryData(retData);
                            userViewModel = CWHelper.Deserialize<ThirdUserViewModel>(retData);
                            if (userViewModel.statusCode == MessageStatusCode.Success)
                            {
                                tutpmEntiy = CreateThirdUser(tutpmEntiy, userViewModel, userID);
                                tutpmBll.Create(tutpmEntiy);
                            }
                            userinfo = new UserViewModel
                            {
                                UserID = userID,
                                UserName = user.user_name,
                                HighImageUrl = user.HighImageUrl,
                                VoipAccount = userViewModel.SubAccount.voipAccount,
                                VoipPwd = userViewModel.SubAccount.voipPwd,
                                SubAccountSid = userViewModel.SubAccount.subAccountSid,
                                SubToken = userViewModel.SubAccount.subToken,
                            };
                        }
                        else
                        {
                            userinfo = new UserViewModel
                            {
                                UserID = userID,
                                UserName = user.user_name,
                                HighImageUrl = user.HighImageUrl,
                                VoipAccount = tutpmEntiy.VoipAccount,
                                VoipPwd = tutpmEntiy.VoipPwd,
                                SubAccountSid = tutpmEntiy.SubAccountSid,
                                SubToken = tutpmEntiy.SubToken
                            };
                        }

                        //设置用户性别，邮箱，手机号，部门，职务，权限名称,生日,电话
                        userinfo.UserGender = user.user_gender;
                        userinfo.UserEmail = user.user_email;
                        userinfo.UserTelephone = user.user_telephone;
                        userinfo.Dept = user.Dept;
                        userinfo.JobFunc = user.JobFunc;
                        userinfo.RoleName = user.RoleName;
                        userinfo.UserBirthday = user.user_birthday;
                        userinfo.UserCellphone = user.user_cellphone;
                        //创建讨论组权限 key:VIP020000
                        DataSet dsUserRight = GetUserRights(userinfo.UserID, rp.CustomerID, loggingSessionInfo);
                        userinfo.CreateGroupRight = IsHasCreateGroupJuri(rp.UserID, rp.CustomerID, UserRightCode.USER_CREATE_GROUP_RIGHT_CODE, dsUserRight);

                        var rdData = new UserInfoRD();
                        rdData.UserInfo = userinfo;
                        rd.Data = rdData;
                        rd.Message = "登录成功";

                    }
                    else
                    {
                        rd.ResultCode = 300;
                        rd.Message = "密码错误";
                    }
                }
                else
                {
                    rd.ResultCode = 300;
                    rd.Message = "邮箱不存在";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rd.ToJSON();
        }

        #endregion

        #region   1.2获取用户列表
        private string GetUsers(string reqContent)
        {
            //Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "开始获取数据" });
            var rd = new APIResponse<UserInfoListRD>();
            var rdData = new UserInfoListRD();

            try
            {
                var rp = reqContent.DeserializeJSONTo<EMAPIRequest<UserInfoListRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                //类别
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                DataSet ds = null;

                DateTime timeFir = DateTime.Now;
                if (string.IsNullOrEmpty(rp.Parameters.LastTimeStamp) || rp.Parameters.LastTimeStamp == "")
                {
                    ds = bll.GetSimpUserInfo(rp.CustomerID, rp.Parameters.Email, rp.Parameters.Phone, rp.Parameters.UserCode, rp.Parameters.UserName, rp.Parameters.PageIndex, rp.Parameters.PageSize, typeID);
                }
                else
                {
                    DateTime dtLast = ConvertToDateTime(rp.Parameters.LastTimeStamp);
                    DataSet dsLast = bll.GetUserInfo(rp.CustomerID, typeID, dtLast.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (dsLast != null && dsLast.Tables != null && dsLast.Tables.Count > 0 && dsLast.Tables[0] != null && dsLast.Tables[0].Rows.Count > 0)
                    {
                        ds = bll.GetSimpUserInfo(rp.CustomerID, rp.Parameters.Email, rp.Parameters.Phone, rp.Parameters.UserCode, rp.Parameters.UserName, rp.Parameters.PageIndex, rp.Parameters.PageSize, typeID);
                    }
                }
                //Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "用户列表访问数据库耗时：" + GetSeconds(timeFir, DateTime.Now) + "s" });

                DateTime dt1 = DateTime.Now;
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtData = ds.Tables[0];
                    DataTable dtLastData = dtData.Clone();
                    for (int i = 0; i < rp.Parameters.CycleNum; i++)
                    {
                        dtLastData.Merge(dtData);
                    }
                    rdData.UserInfo = DataTableToObject.ConvertToList<UserModel>(dtLastData);
                    rd.Data = rdData;
                    rd.Message = "获取成功";
                }
                else
                {
                    rd.Data = rdData;
                    rd.Message = "未查询到相关用户信息";
                }
                TimeSpan ts = DateTime.Now.Subtract(timeFir);

                //Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "用户列表返回数据耗时：" + +GetSeconds(dt1, DateTime.Now) + "s" });
            }
            catch (Exception)
            {
                throw;
            }

            return rd.ToJSON();
        }
        #endregion

        #region 1.3获取用户个人信息（包括积分）
        /// <summary>
        /// 获取用户个人信息（包括积分）
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetUser(string pRequest)
        {
            var rd = new APIResponse<GetUserInfoRD>();
            var rdData = new GetUserInfoRD();
            try
            {
                var rp = pRequest.DeserializeJSONTo<EMAPIRequest<GetUserInfoRP>>();
                if (rp.Parameters != null)
                    rp.Parameters.Validate();
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                //类别
                string typeID = System.Configuration.ConfigurationManager.AppSettings["TypeID"].ToString();
                var entity = bll.GetUserEntityByID(rp.UserID);
                if (entity != null)
                {
                    DataSet ds = bll.GetUserInfo(rp.CustomerID, entity.user_email, string.Empty, string.Empty, string.Empty, 0, 1000, typeID);
                    if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow row = ds.Tables[0].Rows[0];
                        UserViewModel user = new UserViewModel();
                        user.UserID = row["User_ID"].ToString();
                        user.UserName = row["User_Name"].ToString();
                        user.UserBirthday = row["User_Birthday"].ToString();
                        user.HighImageUrl = row["HighImageUrl"].ToString();
                        user.UserGender = row["User_Gender"].ToString();
                        user.UserEmail = row["User_Email"].ToString();
                        user.UserTelephone = row["User_Telephone"].ToString();
                        user.RoleName = row["RoleName"].ToString();
                        user.Dept = row["Dept"].ToString();
                        user.JobFunc = row["JobFunc"].ToString();
                        user.VoipAccount = row["VoipAccount"].ToString();
                        user.VoipPwd = row["VoipPwd"].ToString();
                        user.SubAccountSid = row["SubAccountSid"].ToString();
                        user.SubToken = row["SubToken"].ToString();

                        rdData.GetUserInfo = user;
                        rd.Data = rdData;
                    }
                }
                else
                    rd.Message = "未查询到相关用户信息";
            }
            catch (Exception)
            {
                throw;
            }
            return rd.ToJSON();
        }
        #endregion

        #region 1.4修改密码
        /// <summary>
        /// 修改密码。
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string ModifyPassword(string pRequest)
        {
            var rd = new APIResponse<ChangeUserPWDRD>();
            var rdData = new ChangeUserPWDRD();

            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<ChangeUserPWDRP>>();
            if (rp.Parameters != null)
                rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            #region 验证用户
            var bll = new T_UserBLL(loggingSessionInfo);
            var entity = bll.GetUserEntityByID(rp.UserID);
            if (entity != null)
            {
                if (entity.user_password != rp.Parameters.SourcePWD)//MD5Helper.Encryption(
                {
                    rd.Message = "原密码错误";
                    rd.ResultCode = 301;
                    rdData.IsSuccess = false;
                }
                else
                {
                    entity.user_password = rp.Parameters.NewPWD;
                    bll.Update(entity);
                    rd.Message = "修改成功";
                    rdData.IsSuccess = true;
                }
            }
            else
            {
                rd.Message = "用户不存在";
                rd.ResultCode = 302;
                rdData.IsSuccess = false;
            }
            #endregion
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region 1.5修改用户个人信息
        /// <summary>
        /// 修改用户个人信息
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string ModifyUser(string pRequest)
        {
            var rd = new APIResponse<ChangeUserInfoRD>();
            var rdData = new ChangeUserInfoRD();

            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<ChangeUserInfoRP>>();
            if (rp.Parameters != null)
                rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            #region 更新用户信息
            var bll = new T_UserBLL(loggingSessionInfo);
            var entity = bll.GetUserEntityByID(rp.UserID);
            if (entity != null)
            {
                entity.user_email = rp.Parameters.UserEmail;
                entity.user_telephone = rp.Parameters.UserTelephone;
                entity.user_cellphone = rp.Parameters.UserCellphone;
                bll.Update(entity);
                rd.Message = "修改成功";
                rdData.IsSuccess = true;
            }
            else
            {
                rd.Message = "用户不存在";
                rd.ResultCode = 302;
                rdData.IsSuccess = false;
            }
            #endregion

            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region 1.6 修改用户图像
        /// <summary>
        /// 修改用户图像。
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string ModifyHighImageDemo(string pRequest)
        {
            //日志记录req值
            Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "req参数值：" + pRequest });

            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<FileUploadRP>>();
            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            //rp.UserID = "4C2BCFC00D014ABB9BC5D8B5E9AB88C7";
            //rp.CustomerID = "e703dbedadd943abacf864531decdac1";
            //rp.Plat = "android";

            if (string.IsNullOrEmpty(rp.UserID))
                throw new APIException("UserID不能为空");

            FileUploadRD info = new FileUploadRD();
            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);

                #region 上传附件
                //图片完整路径
                string imgURL = "";
                if (rp.Plat.ToLower() == Plat.android.ToString())
                {
                    Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "Android图片处理" });

                    #region android图片处理
                    HttpPostedFile files = HttpContext.Current.Request.Files["FileUp"];

                    string filename = "";
                    string fileName = "";

                    HttpPostedFile postedFile = files;
                    if (postedFile != null && postedFile.FileName != null && postedFile.FileName != "")
                    {
                        filename = postedFile.FileName;
                        string suffixname = "";
                        if (filename != null)
                        {
                            suffixname = filename.Substring(filename.LastIndexOf(".")).ToLower();
                        }
                        string tempPath = string.IsNullOrEmpty(rp.Parameters.FilePath) ? "/HeadImage/qixin/images/" : rp.Parameters.FilePath.Trim();
                        fileName = (string.IsNullOrEmpty(rp.Parameters.ImageName) ? DateTime.Now.ToString("yyyy.MM.dd.mm.ss.ffffff") : rp.Parameters.ImageName.Trim()) + suffixname;
                        string savepath = HttpContext.Current.Server.MapPath(tempPath);
                        if (!Directory.Exists(savepath))
                        {
                            Directory.CreateDirectory(savepath);
                        }

                        postedFile.SaveAs(savepath + @"/" + fileName);//保存

                        imgURL = ConfigurationManager.AppSettings["customer_service_url"].ToString().TrimEnd('/') + tempPath + fileName;


                    }
                    else
                    {
                        throw new Exception("请上传.jpg,.png,.gif文件");
                    }
                    #endregion
                }
                else
                {
                    #region IOS图片处理
                    string downloadImageUrl = ConfigurationManager.AppSettings["customer_service_url"];
                    string localPath = string.Empty;
                    string headimgurl = DownloadFile(rp.Parameters.UploadImageUrl, downloadImageUrl.TrimEnd('/'), out localPath);
                    imgURL = headimgurl;
                    Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "IOS图片处理" });
                    #endregion
                }

                #endregion

                #region 修改数据库图片路径
                if (!string.IsNullOrEmpty(imgURL))
                {
                    if (rp.Parameters.IsUpdate != 0)
                    {
                        //string field = string.IsNullOrEmpty(rp.Parameters.Field) ? "ImageURL" : rp.Parameters.Field.Trim();
                        string sql = "SELECT ISNULL(MAX(DisplayIndex),0) AS DisplayIndex  FROM ObjectImages WHERE ObjectId='{0}'";
                        DataSet ds = bll.SearchSql(string.Format(sql, rp.UserID));
                        int displayIndex = 0;
                        if (ds != null)
                        {
                            displayIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["DisplayIndex"].ToString());
                            ++displayIndex;
                        }
                        sql = string.Format(@" update ObjectImages set IsDelete=1,LastUpdateBy='{0}',LastUpdateTime=GETDATE() 
                                             where ObjectId='{1}' ", rp.UserID, rp.UserID);
                        string isql = ";insert into ObjectImages (ImageId ,ObjectId ,ImageURL ,DisplayIndex,CreateTime,CreateBy,LastUpdateBy,LastUpdateTime,IsDelete,CustomerId,Title,Description)";
                        isql += " values ('{0}','{1}','{2}','{3}',GETDATE(),'{4}','{5}',GETDATE(),0,'{6}','{7}','{8}')";
                        isql = string.Format(isql, Guid.NewGuid().ToString(), rp.UserID, imgURL, displayIndex, rp.UserID, rp.UserID, rp.CustomerID, rp.Parameters.ImageName, "");
                        bll.SubmitSql(sql + isql);
                    }
                }
                #endregion

                //info.ImgUrl = imgURL;
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }

            //4.拼装响应结果
            var rd = new APIResponse<FileUploadRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }

        private string ModifyHighImage(string pRequest)
        {
            //日志记录req值
            Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "req参数值：" + pRequest });

            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<FileUploadRP>>();
            FileUploadRD info = new FileUploadRD();

            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            //rp.UserID = "2C2282DB10EB4447AB137C2576C2EF20";
            //rp.CustomerID = "e703dbedadd943abacf864531decdac1";
            //rp.Plat = "android";

            if (string.IsNullOrEmpty(rp.UserID))
                throw new APIException("UserID不能为空");
            try
            {
                var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);

                #region 上传附件
                UploadFileResp uploadFile = UploadImageData(HttpContext.Current, rp.Plat.ToString(), rp.Parameters.UploadImageUrl);
                info.UploadImageInfo = uploadFile;
                #endregion

                #region 修改数据库图片路径
                //if (uploadFile.thumbs.Count > 0)
                //{
                //    if (rp.Parameters.IsUpdate != 0)
                //    {
                #region 保存图片和缩略图url
                bll.SaveImageThumbs(uploadFile.file, uploadFile.thumbs, rp.UserID, rp.CustomerID, rp.UserID);
                #endregion
                //    }
                //}
                #endregion
            }
            catch (Exception e)
            {
                Loggers.Exception(new ExceptionLogInfo(e));
                throw new Exception(e.Message);
            }
            //4.拼装响应结果
            var rd = new APIResponse<FileUploadRD>();
            rd.Data = info;

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }
        #endregion

        #region 1.7通知
        /// <summary>
        /// TestDemo
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string GetNotices(string pRequest)
        {
            var rd = new APIResponse<NoticeRD>();
            var rdData = new NoticeRD();

            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<NoticeRP>>();
            if (rp.Parameters != null)
                rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            #region GetNotices
            List<NoticeModel> list = new List<NoticeModel>();
            NoticeModel model = null;
            model = new NoticeModel();
            model.NoticeID = "1";
            model.Title = "杰亦特隆重推出企业APP：管理通";
            model.Content = "2014年6月16日，杰亦特 隆重推出面向企业的移动应用：管理通。让企业内部沟通更加便捷。";
            model.Member = "";
            model.DT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            list.Add(model);
            model = new NoticeModel();
            model.NoticeID = "2";
            model.Title = "企信功能全面内测的通知";
            model.Content = "从6月1日开始，企信功能进入内部试用阶段，所有同事必须安装，并且在工作中充分利用，发现任何问题，可以随时给企信团队反馈。";
            model.Member = "";
            model.DT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            list.Add(model);
            model = new NoticeModel();
            model.NoticeID = "3";
            model.Title = "企信团队 端午节加班通知";
            model.Content = "因为项目时间紧张，为了能在6月1日发布内测版，决定企信团队所有员工，端午节三天 全体加班。请大家提前做好安排。";
            model.Member = "";
            model.DT = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            list.Add(model);
            #endregion

            rdData.ListNotices = list;
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region 1.8用户提交反馈
        public string SubmitUserFeedback(string pRequest)
        {
            var rd = new APIResponse<UserFeedbackRD>();
            var rdData = new UserFeedbackRD();

            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<UserFeedbackRP>>();
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            UserFeebackBLL bll = new UserFeebackBLL(loggingSessionInfo);
            UserFeebackEntity ufbe = new UserFeebackEntity();
            ufbe.FeedbackID = Guid.NewGuid();
            ufbe.FeedbackContent = rp.Parameters.FeedbackContent;
            ufbe.CustomerID = rp.CustomerID;
            ufbe.AppName = rp.Parameters.AppName;
            ufbe.Version = rp.Version;
            ufbe.IsDelete = 0;
            bll.Create(ufbe);
            rdData.IsSuccess = true;
            rdData.Msg = "非常感谢您提出的宝贵意见";
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #region Export UserInfo
        private string ExportUser(string pRequest)
        {
            var rd = new APIResponse<ExportUserRD>();
            var rdData = new ExportUserRD();

            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<ExportUserRP>>();
            if (rp.Parameters != null)
                rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            #region  Export UserInfo
            T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
            bll.ExportData(rp.CustomerID, rp.Parameters.PageIndex, rp.Parameters.PageSize, loggingSessionInfo);
            #endregion

            return rd.ToJSON();
        }
        #endregion

        #region 第三方用户注册
        private string ThirdPartyRegisterUser(string pRequest)
        {
            var rd = new APIResponse<ThirdRegisterUserRD>();
            var rdData = new ThirdRegisterUserRD();

            var rp = pRequest.DeserializeJSONTo<EMAPIRequest<ThirdRegisterUserRP>>();
            if (rp.Parameters != null)
                rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            try
            {
                #region  ThirdPartyRegisterUser
                T_UserBLL bll = new T_UserBLL(loggingSessionInfo);
                //string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY user_code ) rowid,* FROM  dbo.T_User WHERE customer_id = '{0}' AND user_code LIKE 'PG%')tt WHERE rowid BETWEEN {1} AND {2}";
                //string sql = "SELECT * FROM dbo.T_User WHERE create_time >'2014-06-28' AND customer_id='17fe67e2b69e4b50b67e725939586459'";

                string sql = "select TOP 120 * from [T_User] WHERE 1=1 AND customer_id='17fe67e2b69e4b50b67e725939586459' ";
                sql += " AND user_id NOT IN(SELECT userid FROM dbo.TUserThirdPartyMapping WHERE userid IN(";
                sql += " select user_id from [T_User]  WHERE 1=1 AND customer_id='17fe67e2b69e4b50b67e725939586459'))";

                int sRow = rp.Parameters.PageIndex * rp.Parameters.PageSize + 1;
                int eRow = (rp.Parameters.PageIndex + 1) * rp.Parameters.PageSize;
                sql = string.Format(sql, rp.CustomerID, sRow, eRow);
                DataSet ds = bll.SearchSql(sql);

                //验证是否在第三方注册
                TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(loggingSessionInfo);
                System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
                System.Text.StringBuilder strBuilderError = new System.Text.StringBuilder();
                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string itemUserID = row["user_id"].ToString();
                        //Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "userID:" + itemUserID });
                        try
                        {
                            TUserThirdPartyMappingEntity tutpmEntiy = tutpmBll.GetByID(itemUserID);
                            if (tutpmEntiy == null)
                            {
                                ThirdUserViewModel userViewModel = null;
                                CloudRequestFactory factory = new CloudRequestFactory();
                                //调用云通讯创建子账户
                                //string retData = factory.CreateSubAccount("sandboxapp.cloopen.com", "8883", "ff8080813bbcae3f013bcc39c18a0022", "8f32e2023d804e1390a3b0b8b36d6e28", "aaf98f893e7df943013e8728b2b400c7", itemUserID);
                                //string retData = factory.CreateSubAccount("sandboxapp.cloopen.com", "8883", "ff8080813bbcae3f013bcc39c18a0022", "8f32e2023d804e1390a3b0b8b36d6e28", "aaf98f893e7df943013e8728b2b400c7", itemUserID);
                                string retData = factory.CreateSubAccount(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, YunTongXunAppSetting.MainAccount, YunTongXunAppSetting.MainToken, YunTongXunAppSetting.AppID, itemUserID);
                                //string jsonData = getDictionaryData(retData);
                                strBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "create userID:" + itemUserID + "=====" + retData + "\r\n");
                                userViewModel = CWHelper.Deserialize<ThirdUserViewModel>(retData);
                                if (userViewModel.statusCode == MessageStatusCode.Success)
                                {
                                    tutpmEntiy = CreateThirdUser(tutpmEntiy, userViewModel, itemUserID);
                                    tutpmBll.Create(tutpmEntiy);
                                }
                            }
                            else
                            {
                                strBuilderError.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "error create userID:" + itemUserID + "\r\n");
                                //Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "云通讯已注册userID:" + itemUserID });
                            }
                        }
                        catch (Exception ex)
                        {
                            strBuilderError.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 云注册失败 error create userID:" + itemUserID + "\r\n");
                            rd.Message = ex.Message;
                            rd.ResultCode = 301;
                        }
                    }
                }
                #endregion
                Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = strBuilder.ToString() });
                Loggers.DEFAULT.Exception(new ExceptionLogInfo() { ErrorMessage = strBuilderError.ToString() });
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 301;
                Loggers.DEFAULT.Exception(new ExceptionLogInfo() { ErrorMessage = ex.Message });
            }
            return rd.ToJSON();
        }

        #region 单用户注册
        /// <summary>
        /// 单用户注册
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string SingleRegister(string pRequest)
        {
            var rd = new APIResponse<SingleRegisterRD>();
            var rdData = new SingleRegisterRD();

            var rp = pRequest.DeserializeJSONTo<APIRequest<SingleRegisterRP>>();
            if (rp.Parameters != null)
                rp.Parameters.Validate();
            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);

            try
            {
                //验证是否在第三方注册
                TUserThirdPartyMappingBLL tutpmBll = new TUserThirdPartyMappingBLL(loggingSessionInfo);
                TUserThirdPartyMappingEntity tutpmEntiy = tutpmBll.GetByID(rp.Parameters.RisUserID);

                Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = "userID:" + rp.Parameters.RisUserID + "开始注册" });

                if (tutpmEntiy == null)
                {
                    ThirdUserViewModel userViewModel = null;
                    CloudRequestFactory factory = new CloudRequestFactory();
                    //调用云通讯创建子账户
                    string retData = factory.CreateSubAccount(YunTongXunAppSetting.RestAddress, YunTongXunAppSetting.RestPort, YunTongXunAppSetting.MainAccount, YunTongXunAppSetting.MainToken, YunTongXunAppSetting.AppID, rp.Parameters.RisUserID);
                    //string jsonData = getDictionaryData(retData);
                    userViewModel = CWHelper.Deserialize<ThirdUserViewModel>(retData);
                    if (userViewModel.statusCode == MessageStatusCode.Success)
                    {
                        tutpmEntiy = CreateThirdUser(tutpmEntiy, userViewModel, rp.Parameters.RisUserID);
                        tutpmBll.Create(tutpmEntiy);
                        rdData.VoipAccount = userViewModel.SubAccount.voipAccount;
                        rdData.VoipPwd = userViewModel.SubAccount.voipPwd;
                    }
                    else
                    {
                        rd.ResultCode = 101;
                        rd.Message = userViewModel.statusMsg;
                        rdData.VoipAccount = "";
                        rdData.VoipPwd = "";
                    }
                    string str = "userID:" + rp.Parameters.RisUserID + "--------" + userViewModel.ToJSON();
                    Loggers.DEFAULT.Debug(new DebugLogInfo() { Message = str });
                }
                else
                {
                    rd.Message = "已在云通讯注册";
                }
            }
            catch (Exception ex)
            {
                rd.Message = ex.Message;
                rd.ResultCode = 102;
                Loggers.DEFAULT.Exception(new ExceptionLogInfo() { ErrorMessage = ex.Message });
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
        #endregion

        #endregion

        #region 工具方法
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (var item in data)
            {
                return item.Value.ToString();
            }
            return ret;
        }

        /// <summary>
        /// 创建第三方用户。
        /// </summary>
        /// <param name="tutpmEntiy"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private TUserThirdPartyMappingEntity CreateThirdUser(TUserThirdPartyMappingEntity tutpmEntiy, ThirdUserViewModel token, string userID)
        {
            tutpmEntiy = new TUserThirdPartyMappingEntity();
            tutpmEntiy.UserId = userID;
            tutpmEntiy.SubAccountSid = token.SubAccount.subAccountSid;
            tutpmEntiy.SubToken = token.SubAccount.subToken;
            tutpmEntiy.StatusCode = token.statusCode;

            tutpmEntiy.DateCreated = token.SubAccount.dateCreated;
            tutpmEntiy.VoipAccount = token.SubAccount.voipAccount;
            tutpmEntiy.VoipPwd = token.SubAccount.voipPwd;

            return tutpmEntiy;
        }

        #region 验证用户是否存在创建讨论组权限
        public DataSet GetUserRights(string pUserID, string pCustomerID, LoggingSessionInfo pLoggingSessionInfo)
        {
            //key:VIP020000 value:创建讨论组权限
            T_UserBLL bll = new T_UserBLL(pLoggingSessionInfo);
            DataSet ds = bll.GetUserRights(pUserID, pCustomerID);
            return ds;
        }

        public bool IsHasCreateGroupJuri(string pUserID, string pCustomerID, string pRightKey, DataSet pDsUserRight)
        {
            bool f = false;
            //key:VIP020000 value:创建讨论组权限
            if (pDsUserRight.Tables != null && pDsUserRight.Tables.Count > 0 && pDsUserRight.Tables[0] != null && pDsUserRight.Tables[0].Rows.Count > 0)
            {
                if (pDsUserRight.Tables[0].Select("Menu_Code='" + pRightKey + "'").Length > 0)
                    f = true;
            }
            return f;
        }
        #endregion

        #region 图片转换
        public string DownloadFile(string pAddress, string pDownloadUrl, out string pLocalAddress)
        {

            try
            {
                if (pDownloadUrl == null || pDownloadUrl.Equals(""))
                {
                    pDownloadUrl = "http://o2oapi.aladingyidong.com";
                }
                string host = pDownloadUrl + "/File/qixin/";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                WebClient webClient = new WebClient();

                //创建下载根文件夹
                //var dirPath = @"C:\DownloadFile\";
                var dirPath = System.AppDomain.CurrentDomain.BaseDirectory + "File\\qixin\\";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //根据年月日创建下载子文件夹
                var ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                dirPath += ymd + @"\";
                host += ymd + "/";
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                //下载到本地文件
                var fileExt = Path.GetExtension(pAddress).ToLower();
                var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt + ".jpg";
                var filePath = dirPath + newFileName;
                host += newFileName;
                webClient.DownloadFile(pAddress, filePath);
                pLocalAddress = filePath;
                return host;
            }
            catch (Exception ex)
            {
                //  BaseService.WriteLogWeixin("图片下载异常信息：  " + ex.Message);
                pLocalAddress = string.Empty;
                return string.Empty;
            }
        }
        #endregion

        #region 时间转换
        /// <summary>
        /// 时间戳转成时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public DateTime ConvertToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
        #endregion

        #region UploadImageData
        public UploadFileResp UploadImageData(HttpContext context, string pPlat, string pImgHttpUrl)
        {
            var respObj = new UploadFileResp();
            string folderPath = string.Empty;
            string fileLocation = string.Empty;
            string fileName = "";
            string extension = "";
            string savepath = "";
            string host = ConfigurationManager.AppSettings["customer_service_url"];

            string hostUrl = string.Empty;
            if (!host.EndsWith("/")) host += "/";

            if (pPlat == Plat.android.ToString())
            {
                HttpPostedFile postedFile = context.Request.Files["FileUp"];
                if (postedFile == null) postedFile = context.Request.Files[0];
                if (postedFile == null || postedFile.ContentLength == 0)
                {
                    respObj.success = false;
                    respObj.msg = "文件不能为空";
                    respObj.file = new FileData();
                }
                else
                {
                    folderPath = "File/qixin/Image/" + CposCommon.Utils.GetTodayString() + "/";
                    savepath = HttpContext.Current.Server.MapPath("~/" + folderPath);
                    extension = Path.GetExtension(postedFile.FileName).ToLower();
                    if (!Directory.Exists(savepath)) Directory.CreateDirectory(savepath);
                    fileName = CposCommon.Utils.NewGuid();
                    var fileFullName = fileName + extension;
                    fileLocation = string.Format("{0}/{1}", savepath, fileFullName);
                    postedFile.SaveAs(fileLocation);
                    hostUrl += host + folderPath + fileName + extension;
                }
            }
            else if (pPlat == Plat.iPhone.ToString())
            {
                string localPath = string.Empty;
                string headimgurl = DownloadFile(pImgHttpUrl, host.TrimEnd('/'), out localPath);
                fileLocation = localPath;
                hostUrl = headimgurl;
            }

            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(fileLocation);

            //生成缩略图
            respObj.thumbs = new List<FileData>();
            if (true)
            {
                int thumbWidth = 120;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 240;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 480;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            if (true)
            {
                int thumbWidth = 960;
                var thumbFullName = fileName + "_" + thumbWidth.ToString() + extension;
                var thumbLocation = string.Format("{0}/{1}_{2}{3}", savepath, fileName, thumbWidth, extension);
                if (MakeThumbnail(originalImage, thumbLocation, thumbWidth, thumbWidth, "W"))
                {
                    var thumbImage = new FileInfo(thumbLocation);
                    respObj.thumbs.Add(new FileData()
                    {
                        url = host + folderPath + thumbFullName,
                        name = thumbFullName,
                        extension = extension,
                        size = thumbImage.Length,
                        type = thumbWidth.ToString()
                    });
                }
            }
            respObj.o = true;
            respObj.success = true;
            respObj.msg = "";
            respObj.file = new FileData();
            respObj.file.url = hostUrl;
            //respObj.file.name = fileFullName;
            //respObj.file.extension = extension;
            //respObj.file.size = postedFile.ContentLength;
            if (originalImage != null) originalImage.Dispose();

            return respObj;
            //context.Response.Write(respObj.ToJSON());
            //context.Response.End();
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>
        public bool MakeThumbnail(System.Drawing.Image originalImage, string thumbnailPath, int width, int height, string mode)
        {
            //System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            //如果原图的尺寸比缩略图要求的尺寸小,则不进行任何处理
            if (originalImage.Width <= width && originalImage.Height <= height)
            {
                return false;
            }

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                return true;
            }
            catch (System.Exception e)
            {
                return false;
                throw e;

            }
            finally
            {
                //originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion

        #region  Token登录验证
        /// <summary>
        /// Power hour cookie key
        /// </summary>
        public const string POWER_HOUR_COOKIE_KEY = "POWER_HOUR_COOKIE_KEY";
        /// <summary>
        /// Power hour cookie expires
        /// 设置30分钟
        /// </summary>
        public const int POWER_HOUR_COOKIE_EXPIRES = 30;
        /// <summary>
        /// Token登录验证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool PowerHourTokenVerify(string token)
        {
            bool f = false;
            if (HttpContext.Current.Request.Cookies[POWER_HOUR_COOKIE_KEY + token] != null
                && HttpContext.Current.Request.Cookies[POWER_HOUR_COOKIE_KEY + token].Value != "")
            {
                f = true;
            }
            return f;
        }
        /// <summary>
        /// 设置宝洁cookie
        /// </summary>
        /// <param name="pToken"></param>
        public void SetPGCookie(string pToken)
        {
            HttpCookie hc = new HttpCookie(POWER_HOUR_COOKIE_KEY + pToken, Guid.NewGuid().ToString());
            hc.Expires = DateTime.Now.AddMinutes(POWER_HOUR_COOKIE_EXPIRES);
            HttpContext.Current.Response.Cookies.Add(hc);
        }
        #endregion

        #endregion

        public double GetSeconds(DateTime dtStart, DateTime dtEnd)
        {
            TimeSpan startTime = new TimeSpan(dtStart.Ticks);
            if (dtEnd == null)
                dtEnd = DateTime.Now;
            TimeSpan endTime = new TimeSpan(dtEnd.Ticks);
            TimeSpan t = startTime.Subtract(endTime).Duration();
            return t.TotalSeconds;
            //return t.TotalMilliseconds / 1000;
        }
    }

    #region 定义接口的请求参数及响应结果的数据结构

    #region 用户登录验证
    public class PGUserInfoRP : IAPIRequestParameter
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public string PGToken { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new APIException("请提供email地址");
            }

            //if (string.IsNullOrEmpty(Password))
            //{
            //    throw new APIException("请提供密码");
            //}

            if (string.IsNullOrEmpty(PGToken))
            {
                throw new APIException("请提供宝洁Token值");
            }
        }
    }

    public class PGUserInfoRD : IAPIResponseData
    {
        public UserViewModel UserInfo { set; get; }

    }
    #endregion

    #region 用户登录验证
    public class UserInfoRP : IAPIRequestParameter
    {
        public string Email { set; get; }
        public string Password { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new APIException("请输入邮件地址");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new APIException("请输入密码");
            }
        }
    }

    public class UserInfoRD : IAPIResponseData
    {
        public UserViewModel UserInfo { set; get; }

    }
    #endregion

    #region 用户列表
    public class UserInfoListRP : IAPIRequestParameter
    {
        public string Email { set; get; }
        public string Phone { set; get; }
        public string UserName { set; get; }
        public string UserCode { set; get; }
        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        /// <summary>
        /// 循环次数（大数量测试用）
        /// </summary>
        public int CycleNum { set; get; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string LastTimeStamp { set; get; }

        public void Validate()
        {
            if (PageSize == 0) PageSize = 15;
            if (CycleNum == 0) CycleNum = 1;
        }
    }
    public class UserInfoListRD : IAPIResponseData
    {
        public List<UserModel> UserInfo { set; get; }
    }

    public class UserModel
    {
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { set; get; }
        /// <summary>
        /// 头像url
        /// </summary>
        public string HighImageUrl { set; get; }
        /// <summary>
        /// 积分
        /// </summary>
        public string Integral { set; get; }
        /// <summary>
        /// 职务
        /// </summary>
        public string JobFunc { set; get; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        public string modify_time { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }
        /// <summary>
        /// 直接上级
        /// </summary>
        public string SuperiorName { set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string user_cellphone { set; get; }
        /// <summary>
        /// 用户Code
        /// </summary>
        public string user_code { set; get; }
        /// <summary>
        /// email
        /// </summary>
        public string user_email { set; get; }
        /// <summary>
        /// 性别 
        /// 1男 2女
        /// </summary>
        public string user_gender { set; get; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string user_id { set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string user_name { set; get; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string user_name_en { set; get; }
        /// <summary>
        /// 手机
        /// </summary>
        public string user_telephone { set; get; }
        /// <summary>
        /// 云通讯帐号
        /// </summary>
        public string VoipAccount { set; get; }
    }

    public class UserInfo : T_UserEntity
    {
        /// <summary>
        /// 云通讯ID
        /// </summary>
        public string VoipAccount { get; set; }

        /// <summary>
        /// 角色名称。
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 职位。
        /// </summary>
        public string JobFunc { get; set; }

        /// <summary>
        /// 直接上级名称
        /// </summary>
        public string SuperiorName { set; get; }

        /// <summary>
        /// 积分
        /// </summary>
        public string Integral { set; get; }
    }
    #endregion

    #region 密码修改
    public class ChangeUserPWDRP : IAPIRequestParameter
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string SourcePWD { set; get; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPWD { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(SourcePWD))
            {
                throw new APIException("请提供旧密码");
            }

            if (string.IsNullOrEmpty(NewPWD))
            {
                throw new APIException("请提供新密码");
            }
        }
    }
    public class ChangeUserPWDRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    #region 用户信息修改
    public class ChangeUserInfoRP : IAPIRequestParameter
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { set; get; }
        /// <summary>
        /// 手机
        /// </summary>
        public string UserTelephone { set; get; }
        /// <summary>
        /// 座机号
        /// </summary>
        public string UserCellphone { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(UserEmail))
            {
                throw new APIException("请提供邮箱");
            }

            if (string.IsNullOrEmpty(UserTelephone))
            {
                throw new APIException("请提供手机号");
            }
        }
    }
    public class ChangeUserInfoRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion

    #region 获取用户信息
    public class GetUserInfoRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetUserInfoRD : IAPIResponseData
    {
        public UserViewModel GetUserInfo { set; get; }
    }

    #endregion

    #region 上传图片
    /// <summary>
    /// 接口请求参数
    /// </summary>
    class FileUploadRP : IAPIRequestParameter
    {
        #region IAPIRequestParameter 成员
        /// <summary>
        /// 参数验证
        /// </summary>
        public void Validate()
        {
            if (HttpContext.Current.Request.Files.Count <= 0)
            {
                //throw new APIException("请提供上传文件");
            }
        }
        #endregion

        /// <summary>
        /// 上传路径：默认为 “/File/qixin/images/”， 如果为空，则为默认
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 上传图片的文件名“yyyy.MM.dd.mm.ss.ffffff”，如果为空，自动生成
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 是否修改数据，1是修改数据库，同时上传图片，0不修改数据库，直接上传图片，为空默认为1
        /// </summary>
        public int? IsUpdate { get; set; }
        /// <summary>
        /// 修改字段的名称，默认修改 “ImageURL”,传入其他字段，则修改其他字段，为空则修改HeadImgUrl
        /// </summary>
        //public string Field { get; set; }

        public string UploadImageUrl { set; get; }
    }
    /// <summary>
    /// 接口响应参数
    /// </summary>
    class FileUploadRD : IAPIResponseData
    {
        /// <summary>
        /// 返回图片路径
        /// </summary>
        //public string ImgUrl { get; set; }

        public UploadFileResp UploadImageInfo { set; get; }
    }
    #endregion

    #region 获取通知
    public class NoticeRP : IAPIRequestParameter
    {
        //public int Num { set; get; }
        //public string Title { set; get; }
        //public string Content { set; get; }
        //public string Member { set; get; }
        //public string ID { set; get; }
        public void Validate()
        {

        }
    }
    public class NoticeRD : IAPIResponseData
    {
        public List<NoticeModel> ListNotices { set; get; }
    }

    public class NoticeModel
    {
        public string Title { set; get; }
        public string Content { set; get; }
        public string DT { set; get; }
        public string Member { set; get; }
        public string NoticeID { set; get; }
    }
    #endregion

    #region 提交用户反馈
    public class UserFeedbackRP : IAPIRequestParameter
    {
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string FeedbackContent { set; get; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppName { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(FeedbackContent))
                throw new APIException("反馈内容不能为空");
            if (string.IsNullOrEmpty(AppName))
                throw new APIException("应用名称不能为空");
        }
    }
    public class UserFeedbackRD : IAPIResponseData
    {
        /// <summary>
        /// 提交状态
        /// </summary>
        public bool IsSuccess { set; get; }
        /// <summary>
        /// 提示
        /// </summary>
        public string Msg { set; get; }
    }
    #endregion

    //#region 密码修改
    //public class ChangeUserPWDRP : IAPIRequestParameter
    //{

    //}
    //public class ChangeUserPWDRD : IAPIResponseData
    //{

    //}
    //#endregion

    #region UploadFileResp
    public class UploadFileResp
    {
        public bool o { get; set; }
        public bool success { get; set; }
        public string msg { get; set; }
        public FileData file { get; set; }
        public IList<FileData> thumbs { get; set; }
    }
    //public class FileData
    //{
    //    public string url { get; set; }
    //    public string name { get; set; }
    //    public string extension { get; set; }
    //    public long size { get; set; }
    //    public string type { get; set; }
    //}
    #endregion

    #endregion

    #region Export UserInfo
    public class ExportUserRP : IAPIRequestParameter
    {
        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public void Validate()
        {

        }
    }
    public class ExportUserRD : IAPIResponseData
    {
    }
    #endregion

    #region ThirdRegisterUser
    public class ThirdRegisterUserRP : IAPIRequestParameter
    {
        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public void Validate()
        {

        }
    }
    public class ThirdRegisterUserRD : IAPIResponseData
    {
    }
    #endregion

    #region SingleRegister
    public class SingleRegisterRP : IAPIRequestParameter
    {
        public string RisUserID { set; get; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(RisUserID))
                throw new APIException("RisUserID不能为空");
        }
    }
    public class SingleRegisterRD : IAPIResponseData
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string VoipAccount { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string VoipPwd { set; get; }
    }
    #endregion
}