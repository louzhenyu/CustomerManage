using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Collections;

using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.Common;
using System.Configuration;
using System.Web;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.DTO.Base;


namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 用户管理类
    /// </summary>
    public class cUserService: BaseService
    {
        JIT.CPOS.BS.DataAccess.UserService userService = null;
        public cUserService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            userService = new JIT.CPOS.BS.DataAccess.UserService(loggingSessionInfo);
        }

        

        #region 用户
        

        /// <summary>
        /// 用户查询,根据条件返回用户查询信息与查询结果总记录数
        /// </summary>
        /// <param name="User_Code">用户工号</param>
        /// <param name="User_Name">用户名</param>
        /// <param name="CellPhone">手机</param>
        /// <param name="User_Status">状态</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns></returns>
        public UserInfo SearchUserList(string User_Code, string User_Name, string CellPhone, string User_Status, int maxRowCount, int startRowIndex)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("UserCode", User_Code);
            hashtable.Add("UserName", User_Name);
            hashtable.Add("CellPhone", CellPhone);
            hashtable.Add("UserStatus", User_Status);
            hashtable.Add("LoginUserId", loggingSessionInfo.CurrentUser.User_Id.ToString());
            hashtable.Add("StartRow", startRowIndex);
            hashtable.Add("EndRow", startRowIndex + maxRowCount);
            hashtable.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);

            

            IList<UserInfo> userInfoList = new List<UserInfo>();
            DataSet ds = userService.SearchUserListByUnitID(hashtable);//根据unitid获取会员信息
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                userInfoList = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]);
            }

            UserInfo userInfo = new UserInfo();
            userInfo.ICount = userService.SearchUserCount(hashtable); //获取会员数量
            userInfo.UserInfoList = userInfoList;

            return userInfo;
        }
        /// <summary>
        /// 用户查询,根据条件返回用户查询信息与查询结果总记录数
        /// </summary>
        /// <param name="User_Code">用户工号</param>
        /// <param name="User_Name">用户名</param>
        /// <param name="CellPhone">手机</param>
        /// <param name="User_Status">状态</param>
        /// <param name="maxRowCount">每页所占行数</param>
        /// <param name="startRowIndex">当前页的起始行数</param>
        /// <returns></returns>
        public UserInfo SearchUserListByUnitID(string User_Code, string User_Name, string CellPhone, string User_Status, int maxRowCount, int startRowIndex, string UnitID, string para_unit_id, string role_id)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("UserCode", User_Code);
            hashtable.Add("UserName", User_Name);
            hashtable.Add("CellPhone", CellPhone);
            hashtable.Add("UserStatus", User_Status);
            hashtable.Add("LoginUserId", loggingSessionInfo.CurrentUser.User_Id.ToString());
            hashtable.Add("StartRow", startRowIndex);
            hashtable.Add("EndRow", startRowIndex + maxRowCount);
            hashtable.Add("CustomerId", loggingSessionInfo.CurrentLoggingManager.Customer_Id);
            hashtable.Add("UnitID", UnitID);
            hashtable.Add("para_unit_id", para_unit_id);
            hashtable.Add("role_id", role_id);
            IList<UserInfo> userInfoList = new List<UserInfo>();
            DataSet ds = userService.SearchUserListByUnitID(hashtable);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                userInfoList = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]);
            }

            UserInfo userInfo = new UserInfo();
            userInfo.ICount = userService.SearchUserCount(hashtable);
            userInfo.UserInfoList = userInfoList;

            return userInfo;
        }

        public DataSet GetUserList(int pageIndex, int pageSize, string OrderBy, string sortType, string UserID, string CustomerID,string PhoneList,string UnitID)
        {
            return userService.GetUserList(pageIndex, pageSize, OrderBy, sortType, UserID, CustomerID, PhoneList, UnitID);
        }
        /// <summary>
        /// 获取用户登陆密码
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetPasswordFromAP(string customerId, string userId)
        {
            return userService.GetPasswordFromAP(customerId, userId);
        }
        /// <summary>
        /// 根据用户的Id获取用户信息
        /// </summary>
        /// <param name="LoggingSessionInfo">用户登录信息类</param>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        public UserInfo GetUserById(LoggingSessionInfo LoggingSessionInfo, string user_id)
        {
            UserInfo userInfo = new UserInfo();
            DataTable dt = userService.GetUserById(user_id);
            if (dt != null && dt.Rows.Count > 0)
            {
                userInfo = DataTableToObject.ConvertToObject<UserInfo>(dt.Rows[0]);
            }
            return userInfo;
        }

        #region 根据CusID获取销售人员列表 2014-10-16

        /// <summary>
        /// 获取门店服务顾问人员列表（后台用）
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="unitID">门店ID</param>
        /// <param name="roleCode">人员code 销售人员为：xsgw </param>
        /// <returns></returns>
        public IList<UserInfoRD> GetUserByCustomerID(string customerID, string unitID, string roleCode)
        {
            DataTable dt = userService.GetUserByCustomerID(customerID, unitID, roleCode);

            IList<UserInfoRD> userList = new List<UserInfoRD>();
            if (dt != null && dt.Rows.Count > 0)
            {
                userList = DataTableToObject.ConvertToList<UserInfoRD>(dt);
            }
            return userList;
        }

        #endregion

        /// <summary>
        /// 获取用户在某种角色下的缺省的单位
        /// </summary>
        /// <param name="userId">用户的Id</param>
        /// <param name="roleId">角色的Id</param>
        /// <returns>单位Id</returns>
        public string GetDefaultUnitByUserIdAndRoleId(string userId, string roleId)
        {
            DataSet ds = userService.GetDefaultUnitByUserIdAndRoleId(userId, roleId);

            IList<UserRoleInfo> userRoleList = new List<UserRoleInfo>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                userRoleList = DataTableToObject.ConvertToList<UserRoleInfo>(ds.Tables[0]);
            }
            if (userRoleList == null || userRoleList.Count == 0)
            {
                return null;
            }
            else
            {
                return userRoleList[0].UnitId;
            }
        }

        #region 判断用户是否存在
        /// <summary>
        /// 判断用户标识是否存在
        /// </summary>
        /// <param name="loggingManager">登录model</param>
        /// <returns></returns>
        public bool IsExistUser(LoggingSessionInfo loggingSessionInfo)
        {
            //int n = cSqlMapper.Instance(loggingManager).QueryForObject<int>("User.IsExsitUser", loggingManager.User_Id);
            //return n > 0 ? true : false;
            return userService.IsExistUser() > 0 ? true : false;
        }

        /// <summary>
        /// 判断用户号码是否存在
        /// </summary>
        /// <param name="user_code">用户号码</param>
        /// <param name="user_id">用户标识</param>
        /// <returns></returns>
        public bool IsExistUserCode(string user_code, string user_id)
        {
            try
            {
                int n = userService.IsExistUserCode(user_code,user_id);
                return n > 0 ? false : true;
            }
            catch (Exception ex) {
                throw (ex); 
            }
        }
        #endregion

        #region 用户修改新建保存
        /// <summary>
        /// 用户保存
        /// </summary>
        /// <param name="loggingSessionInfo">登录用户信息</param>
        /// <param name="userInfo">处理的用户类</param>
        /// <param name="userRoleInfos">用户角色组织关系类集合</param>
        /// <param name="strError">错误信息</param>
        /// <returns></returns>
        public bool SetUserInfo( UserInfo userInfo, IList<UserRoleInfo> userRoleInfos,out string strError,bool IsTran=true) 
        {
            string strResult = string.Empty;
            bool bReturn = true;
            //事物信息
            //cSqlMapper.Instance().BeginTransaction();
            try
            {
                userInfo.customer_id = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                //处理是新建还是修改
                string strDo = string.Empty;

                if (userInfo.User_Password != null)
                {
                    userInfo.User_Password = EncryptManager.Hash(userInfo.User_Password, HashProviderType.MD5);
                }
                else { 
                 var   userInfoOld=this.GetUserById( loggingSessionInfo,userInfo.User_Id);
                    if(userInfoOld!=null)
                    {
                        userInfo.User_Password = userInfoOld.User_Password; //如果是修改的时候没有密码，就用原来老的密码
                     }
                }
                if (userInfo.strDo == null)
                {
                    if (userInfo.User_Id == null || userInfo.User_Id.Equals(""))
                    {
                        userInfo.User_Id = NewGuid();
                        strDo = "Create";
                    }
                    else
                    {
                        strDo = "Modify";
                    }
                }
                else {
                    strDo = "Create";
                }
                //1 判断用户号码是否唯一
                //if (!IsExistUserCode( userInfo.User_Code, userInfo.User_Id))
                //{
                //    strError = "用户号码已经存在。";
                //    bReturn = false;
                //    return bReturn;
                //}
                //2.提交用户信息至表单
                if (strDo.Equals("Create"))
                {
                    if (!SetUserInsertBill(userInfo))
                    {
                        strError = "用户表单提交失败。";
                        bReturn = false;
                        return bReturn;
                    }
                }

                //3.获取用户表单信息,设置用户状态与状态描述
                BillModel billInfo = new cBillService(loggingSessionInfo).GetBillById( userInfo.User_Id);
                userInfo.User_Status = billInfo.Status;
                userInfo.User_Status_Desc = billInfo.BillStatusDescription;

                if (userInfo.User_Status == null || userInfo.User_Status.Equals(""))
                {
                    userInfo.User_Status = "1";
                    userInfo.User_Status_Desc = "正常";
                }

                //提交后台
                userService.SetUserInfo(userInfo, userRoleInfos, IsTran, strDo, out strError);
                if (IsTran)
                {
                    //#if SYN_AP
                    // 提交管理平台

                    if (!SetManagerExchangeUserInfo(loggingSessionInfo, userInfo, strDo == "Create" ? 1 : 2))
                    {
                        strError = "提交管理平台失败";
                        bReturn = false;
                        return bReturn;
                    }
                    //#endif

                    // 提交接口
                    strResult = SetDexUserCertificate(loggingSessionInfo, userInfo);
                    if (!(strResult.Equals("True") || strResult.Equals("true")))
                    {
                        //cSqlMapper.Instance().RollBackTransaction();
                        strError = "提交接口失败";
                        bReturn = false;
                        return bReturn;
                    }
                }
                //                cSqlMapper.Instance().CommitTransaction();
//                strError = "保存成功!";
//                return bReturn;
            }
            catch (Exception ex)
            {
                //cSqlMapper.Instance().RollBackTransaction();
                strError = ex.ToString();
                throw (ex);
            }
            strError = "保存成功!";
            return true;
        }

        /// <summary>
        /// 用户表单提交
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="userInfo">用户model</param>
        /// <returns></returns>
        private bool SetUserInsertBill(UserInfo userInfo)
        {
            try
            {
                BillModel bill = new BillModel();
                cBillService bs = new cBillService(loggingSessionInfo);

                bill.Id = userInfo.User_Id;//order_id
                string order_type_id = bs.GetBillKindByCode( "USERMANAGER").Id.ToString(); //loggingSession, OrderType
                bill.Code = bs.GetBillNextCode( BillKindModel.CODE_USER_NEW); //BillKindCode
                bill.KindId = order_type_id;
                bill.UnitId = loggingSessionInfo.CurrentUserRole.UnitId;
                bill.AddUserId = loggingSessionInfo.CurrentUser.User_Id;

                BillOperateStateService state = bs.InsertBill( bill);

                if (state == BillOperateStateService.CreateSuccessful)
                {
                    return true;
                }
                else
                {
                    throw (new System.Exception(state.ToString()));
                }
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        /// <summary>
        /// 提交用户表信息
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private bool SetUserTableInfo(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo)
        {
            try
            {
                if (userInfo != null)
                {
                    //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("User.InsertOrUpdate", userInfo);
                    return false;
                }
                return true;
            }
            catch (Exception ex) {
                return false;
                throw (ex);
            }
        }

        /// <summary>
        /// 设置用户与角色与组织关系
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userRoleInfos"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool SetUserRoleTableInfo(LoggingSessionInfo loggingSessionInfo, IList<UserRoleInfo> userRoleInfos,UserInfo userInfo)
        {
            try
            {
                bool bReturn = true;
                
                foreach (UserRoleInfo userRoleInfo in userRoleInfos)
                {
                    if (userRoleInfo.UserId == null || userRoleInfo.UserId.Equals(""))
                        userRoleInfo.UserId = userInfo.User_Id;
                    if (userRoleInfo.Id == null || userRoleInfo.Id.Equals(""))
                        userRoleInfo.Id = NewGuid();
                }
                UserInfo us = new UserInfo();
                us.User_Id = loggingSessionInfo.CurrentUser.User_Id;
                us.Modify_Time = GetCurrentDateTime();
                us.Create_Time = GetCurrentDateTime();
                us.userRoleInfoList = userRoleInfos;

                if (userRoleInfos != null)
                {
                    IDbTransaction pTran = null;
                    foreach (UserRoleInfo userRoleInfo in userRoleInfos)
                    {
                        bReturn = userService.InsertUserRole(userRoleInfo, pTran);
                    }
                }

                //cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("UserRole.InsertOrUpdate", us);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw (ex);
            }
        }

        /// <summary>
        /// 用户上传（到管理平台）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userInfo"></param>
        /// <param name="TypeId">1:新建;2:修改;3:启用;4:停用;5:修改密码</param>
        /// <returns></returns>
        public bool SetManagerExchangeUserInfo(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo,int TypeId)
        {
            try
            {
                UserInfo usInfo = new UserInfo();
                if (TypeId.ToString().Equals("1") || TypeId.ToString().Equals("2"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Code = userInfo.User_Code;
                    usInfo.User_Name = userInfo.User_Name;
                    usInfo.User_Password = userInfo.User_Password;
                    usInfo.Fail_Date = userInfo.Fail_Date;
                    usInfo.User_Status_Desc = userInfo.User_Status_Desc;
                    //usInfo.user_expired_date = "";
                }
                if (TypeId.ToString().Equals("3") || TypeId.ToString().Equals("4"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Status_Desc = userInfo.User_Status_Desc;
                }
                if (TypeId.ToString().Equals("5"))
                {
                    usInfo.User_Id = userInfo.User_Id;
                    usInfo.User_Password = userInfo.User_Password;
                }
                if (usInfo.Fail_Date == null || usInfo.Fail_Date.Equals(""))
                {
                    usInfo.Fail_Date = "9999-12-31";
                }
                string strUserInfo = cXMLService.Serialiaze(usInfo);

                CPOS.BS.WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService cdxService = new WebServices.CustomerDataExchangeWebService.CustomerDataExchangeService();
                cdxService.Url = System.Configuration.ConfigurationManager.AppSettings["ap_url"] + "/customer/CustomerDataExchangeService.asmx";
                //cdxService.SynUser(loggingSessionInfo.CurrentLoggingManager.Customer_Id, 5, strUserInfo);
                return cdxService.SynUser(loggingSessionInfo.CurrentLoggingManager.Customer_Id, TypeId, strUserInfo);//
                //return false;
                
            }
            catch (Exception ex) {
                throw (ex);
            }
        }

        #region 与接口交互
        /// <summary>
        /// 申请用户凭证方法
        /// </summary>
        /// <param name="loggingSessionInfo">登录用户信息</param>
        /// <param name="userInfo">用户集合</param>
        /// <returns></returns>
        public static string SetDexUserCertificate(LoggingSessionInfo loggingSessionInfo, UserInfo userInfo)
        {
            try
            {
                Hashtable certInfo = new Hashtable();
                certInfo["CertId"] = Utils.NewGuid();
                certInfo["UserId"] = userInfo.User_Id;
                certInfo["UserCode"] = userInfo.User_Code;
                certInfo["CustomerId"] = loggingSessionInfo.CurrentLoggingManager.Customer_Id;
                certInfo["CustomerCode"] = loggingSessionInfo.CurrentLoggingManager.Customer_Code;
                certInfo["CertPwd"] = userInfo.User_Password;
                certInfo["CreateUserId"] = loggingSessionInfo.CurrentUser.User_Id;
                certInfo["CreateTime"] = Utils.GetNow();
                certInfo["ModifyUserId"] = loggingSessionInfo.CurrentUser.User_Id;
                certInfo["ModifyTime"] = Utils.GetNow();
                certInfo["ModifyPassword"] = userInfo.ModifyPassword ? "1" : "0";

                UserService service = new UserService(GetLoginUser("dex"));
                bool checkFlag = service.CheckUserToDex(certInfo);
                Hashtable _ht = new Hashtable();
                if (checkFlag)
                {
                    _ht = service.UpdateUserToDex(certInfo);
                }
                else
                {
                    _ht = service.AddUserToDex(certInfo);
                }

                if (!_ht["status"].ToString().Equals("True"))
                {
                    return _ht["error_code"].ToString();
                }
                else
                {
                    return "True";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static LoggingSessionInfo GetLoginUser(string name)
        {
            var obj = new LoggingSessionInfo();
            obj.CurrentLoggingManager = new LoggingManager();
            obj.CurrentLoggingManager.Connection_String = GetConn(name);
            obj.CurrentUser = new BS.Entity.User.UserInfo();
            obj.CurrentUser.User_Id = "1";
            return obj;
        }

        public static string GetConn(string name)
        {
            switch (name)
            {
                case "dex":
                    return ConfigurationManager.AppSettings["Conn_dex"].Trim();
                case "ap":
                    return ConfigurationManager.AppSettings["Conn_ap"].Trim();
            }
            return string.Empty;
        }

        #endregion
        #endregion 用户修改新建保存


        #region 修改用户密码
        /// <summary>
        /// 修改用户密码 Jermyn20140103
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool SetUserPwd(LoggingSessionInfo loggingSessionInfo, string pwd,out string strError)
        {
            try
            {
                bool bReturn = true;
                UserInfo userInfo = new UserInfo();
                userInfo.User_Id = loggingSessionInfo.CurrentUser.User_Id;
                userInfo.User_Password = pwd;
                UpdateUserPassword(userInfo.User_Id, userInfo.User_Password);
                //提交管理平台
                if (!SetManagerExchangeUserInfo(loggingSessionInfo, userInfo, 5))
                {
                    strError = "修改密码失败.提交管理平台失败";
                    bReturn = false;
                    return bReturn;
                }

                // 提交接口
                userInfo.ModifyPassword = true;
                string strResult = SetDexUserCertificate(loggingSessionInfo, userInfo);
                if (!(strResult.Equals("True") || strResult.Equals("true")))
                {
                    strError = "修改密码失败.提交接口失败";
                    bReturn = false;
                    return bReturn;
                }

                strError = "修改密码成功.";
                return bReturn;
            }
            catch (Exception ex) {
                strError = "修改密码失败."+ex.ToString();
                return false;
            }
        }
        #endregion

        #endregion 用户

        #region 用户的状态处理
        /// <summary>
        /// 用户停用/启用方法
        /// </summary>
        /// <param name="user_id">用户标识</param>
        /// <param name="iStatus">用户状态</param>
        /// <param name="LoggingSessionInfo">登录用户Session类</param>
        /// <returns></returns>
        public bool SetUserStatus(string user_id, string iStatus, LoggingSessionInfo LoggingSessionInfo)
        {
            //cSqlMapper.Instance().BeginTransaction();
            try
            {
                cBillService bs = new cBillService(LoggingSessionInfo);
                UserInfo userInfo = new UserInfo();
                userInfo.User_Id = user_id;
                string strError = string.Empty;
                BillActionType billActionType;
                if (iStatus.Equals("1"))
                {
                    billActionType = BillActionType.Open;
                    userInfo.User_Status_Desc = "正常";
                }
                else
                {
                    billActionType = BillActionType.Stop;
                    userInfo.User_Status_Desc = "停用";
                }
                BillOperateStateService state = bs.ApproveBill(user_id, "", billActionType, out strError);
                if (state == BillOperateStateService.ApproveSuccessful)
                {
//                    if (SetUserTableStatus(LoggingSessionInfo, user_id))
//                    {
//#if SYN_AP
//                        // 提交管理平台
//                        if (!SetManagerExchangeUserInfo(LoggingSessionInfo, userInfo, 3))
//                        {
//                            cSqlMapper.Instance().RollBackTransaction();
//                            return false;
//                        }
//#endif
//                        //cSqlMapper.Instance().CommitTransaction();
//                        return true;
//                    }
//                    else
//                    {
//                        //cSqlMapper.Instance().RollBackTransaction();
//                        return false;
//                    }
                    return true;
                }
                else
                {
                    //cSqlMapper.Instance().RollBackTransaction();
                    //设置要改变的用户信息
                    UserInfo userInfo1 = new UserInfo();
                    userInfo1.User_Status = iStatus;
                    userInfo1.User_Status_Desc = userInfo.User_Status_Desc;
                    userInfo1.User_Id = user_id;
                    userInfo1.Modify_User_Id = LoggingSessionInfo.CurrentUser.User_Id;
                    userInfo1.Modify_Time = GetCurrentDateTime(); //获取当前时间
                    //提交

                    return userService.SetUpdateUserStatus(userInfo1);
                }
            }
            catch (Exception ex)
            {
                //cSqlMapper.Instance().RollBackTransaction();
                throw (ex);
            }
            //return false;
        }


        public bool physicalDeleteUser(string unitId)
        {
            return userService.physicalDeleteUser(unitId);
        }



        /// <summary>
        /// 设置用户表的用户状态
        /// </summary>
        /// <param name="loggingSession"></param>
        /// <param name="user_id"></param>
        /// <returns></returns>
        private bool SetUserTableStatus(LoggingSessionInfo loggingSession, string user_id)
        {
            try
            {
                //获取要改变的表单信息
                BillModel billInfo = new cBillService(loggingSessionInfo).GetBillById( user_id);
                //设置要改变的用户信息
                UserInfo userInfo = new UserInfo();
                userInfo.User_Status = billInfo.Status;
                userInfo.User_Status_Desc = billInfo.BillStatusDescription;
                userInfo.User_Id = user_id;
                userInfo.Modify_User_Id = loggingSession.CurrentUser.User_Id;
                userInfo.Modify_Time = GetCurrentDateTime(); //获取当前时间
                //提交
                
                return userService.SetUpdateUserStatus(userInfo);
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region 用户的角色

        /// <summary>
        /// 获取用户在某种角色下能够登录的单位列表
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="userId">用户Id</param>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        public IList<UnitInfo> GetUnitsByUserIdAndRoleId(LoggingSessionInfo loggingSession, string userId, string roleId)
        {
            //Hashtable hashTable = new Hashtable();
            //hashTable.Add("LanguageKindId", loggingSession.CurrentLanguageKindId);
            //hashTable.Add("UserId", userId);
            //hashTable.Add("RoleId", roleId);

            //return cSqlMapper.Instance().QueryForList<UnitInfo>("Unit.SelectByUserIdAndRoleId", hashTable);

            IList<UnitInfo> unitInfoList = new List<UnitInfo>();
            return unitInfoList;
        }

        /// <summary>
        /// 获取用户在某个应用系统下的角色和单位
        /// </summary>
        /// <param name="loggingSession">当前登录用户的Session信息</param>
        /// <param name="userId">用户Id</param>
        /// <param name="applicationId">应用系统Id</param>
        /// <returns></returns>
        public IList<UserRoleInfo> GetUserRoles(string userId, string applicationId)
        {
            IList<UserRoleInfo> userRoleInfoList = new List<UserRoleInfo>();
            DataSet ds = userService.GetUserRoles(userId, applicationId);
            if(ds != null && ds.Tables[0].Rows.Count> 0)
            {
                userRoleInfoList = DataTableToObject.ConvertToList<UserRoleInfo>(ds.Tables[0]);
            }
            
            return userRoleInfoList;
        }

        public IList<UserRoleInfo> GetUserRoles(string userId)
        {
            IList<UserRoleInfo> userRoleInfoList = new List<UserRoleInfo>();
            DataSet ds = userService.GetUserRoles(userId, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                userRoleInfoList = DataTableToObject.ConvertToList<UserRoleInfo>(ds.Tables[0]);
            }

            return userRoleInfoList;
        }

        /// <summary>
        /// 查询用户角色信息
        /// </summary>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public bool CheckUserRole(UserRoleInfo userRole)
        {
            ////将角色和单位的ID拼出来作为角色ID,这样按角色只能查询出一个单位
            //userRole.RoleId = userRole.RoleId + "," + userRole.UnitId;
            //userRole.DefaultFlag = 1;
            //userRole.Id = NewGuid();

            //bool existed = cSqlMapper.Instance().QueryForObject<int>("UserRole.CountByUserRoleUnit", userRole) == 1;
            //if (!existed)
            //{
            //    cSqlMapper.Instance().Insert("UserRole.Insert", userRole);
            //}
            //return existed;
            return false;
        }
        #endregion

        #region 用户密码处理
        /// <summary>
        /// 判断用户输入的密码是否有效
        /// </summary>
        /// <param name="loggingSession">登录model</param>
        /// <param name="user">用户</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public bool IsValidPassword(LoggingSessionInfo loggingSession, UserInfo user, string pwd)
        {
            if (string.IsNullOrEmpty(user.User_Code))
                return false;
            if (string.IsNullOrEmpty(user.User_Code.Trim()))
                return false;

            if (string.IsNullOrEmpty(pwd))
                return false;
            if (string.IsNullOrEmpty(pwd.Trim()))
                return false;

            ////保留
            //if (pwd == "361-" + user.User_Code)
            //    return false;

            //新旧密码一样
            if (!string.IsNullOrEmpty(user.User_Password) && EncryptManager.Hash(pwd, HashProviderType.MD5) == user.User_Password)
                return false;

            string s = pwd;
            char[] s1 = s.ToCharArray();
            if (s.Length < 8)
                return false;
            if (s.Length != s1.Length)
                return false;
            bool b_char = false;
            bool b_number = false;
            bool b_other = false;
            foreach (char ch in s1)
            {
                if (ch >= '0' && ch <= '9')
                    b_number = true;
                else
                {
                    if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                        b_char = true;
                    else
                        b_other = true;
                }
            }

            if (!b_char || !b_number || !b_other)
                return false;
            return true;

        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="userId">用户Id</param>
        /// <param name="userPassword">用户密码</param>
        /// <returns></returns>
        public bool ModifyUserPassword(LoggingSessionInfo loggingSessionInfo,string userId, string userPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(userPassword))
                {
                    return false;
                }
                var count = userService.ModifyUserPassword(userId,EncryptManager.Hash(userPassword, HashProviderType.MD5));
                return count==1;

            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        /// <summary>
        /// 从接口过来的修改用户密码（不需要加密）
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="userId"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public bool ModifyUserPassword_JK(LoggingSessionInfo loggingSessionInfo, string userId, string userPassword,out string strError)
        {
//            cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).BeginTransaction();
//            try
//            {
//                if (string.IsNullOrEmpty(userPassword))
//                {
//                    strError = "密码为空";
//                    return false;
//                }

//                Hashtable hashTable = new Hashtable();
//                hashTable.Add("UserId", userId);
//                hashTable.Add("UserPassword", userPassword);
//                int count = cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).Update("User.ModifyPassword2", hashTable);
//                //return count == 1;

//                UserInfo userInfo = new UserInfo();
//                userInfo.User_Id = userId;
//                userInfo.User_Password = userPassword;
//                userInfo.userRoleInfoList = new UserRoleService().GetUserRoleListByUserId(loggingSessionInfo, userId);
//#if SYN_AP
//                // 提交管理平台
//                if (!SetManagerExchangeUserInfo(loggingSessionInfo, userInfo, 5))
//                {
//                    cSqlMapper.Instance().RollBackTransaction();
//                    strError = "提交管理平台失败";
//                    return false;
//                }
//#endif

////#if SYN_DEX
////                // 提交接口
////                string strError = string.Empty;
////                bool bResult = SetDexUpdateUserCertificate(loggingSessionInfo, userInfo, out strError);
////                if (!bResult)
////                {
////                    cSqlMapper.Instance().RollBackTransaction();
////                    strError = "提交接口失败";
////                    return false;
////                }
////#endif
//                cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).CommitTransaction();
//                strError = "ok";
//                return true;
//            }
//            catch (Exception ex)
//            {
//                cSqlMapper.Instance().RollBackTransaction();
//                strError = ex.ToString();
//                throw (ex);
//            }
            strError = "ok";
            return false;
        }
        #endregion

        #region 根据组织获取用户集合
        /// <summary>
        /// 根据组织获取用户集合
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId">组织标识</param>
        /// <returns></returns>
        public IList<UserInfo> GetUserListByUnitId(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            //Hashtable _ht = new Hashtable();
            //_ht.Add("UnitId", unitId);
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserInfo>("User.SelectByUnitId", _ht);
            IList<UserInfo> userInfoList = new List<UserInfo>();
            return userInfoList;
        }
        #endregion

        #region 根据组织角色获取用户集合
        /// <summary>
        /// 根据组织与角色获取用户集合
        /// </summary>
        /// <param name="loggingSessionInfo">用户登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public IList<UserInfo> GetUserListByUnitIdsAndRoleId(LoggingSessionInfo loggingSessionInfo, string unitId, string roleId)
        {
            //Hashtable _ht = new Hashtable();
            //_ht.Add("UnitId", unitId);
            //_ht.Add("RoleId", roleId);
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserInfo>("User.SelectByUnitIdRoleId", _ht);
            IList<UserInfo> userInfoList = new List<UserInfo>();
            return userInfoList;
        }
        #endregion

        /// <summary>
        /// 获取每个组织的开班人员
        /// </summary>
        /// <param name="loggingSessionInfo">登录model</param>
        /// <param name="unitId">组织标识</param>
        /// <param name="roleId">角色标识</param>
        /// <returns></returns>
        public IList<UserInfo> GetSalesUserByUnitIdsAndRoleId(LoggingSessionInfo loggingSessionInfo, string unitId, string roleId)
        {
            //Hashtable _ht = new Hashtable();
            //_ht.Add("UnitId", unitId);
            //_ht.Add("RoleId", roleId);
            //return cSqlMapper.Instance(loggingSessionInfo.CurrentLoggingManager).QueryForList<UserInfo>("User.SelectSalesUserByUnitIdRoleId", _ht);
            IList<UserInfo> userInfoList = new List<UserInfo>();
            return userInfoList;
        }
        
        #region 上传到中间层
        /// <summary>
        /// 根据用户,获取终端需要的基础信息
        /// </summary>
        /// <param name="User_id">用户标识</param>
        /// <param name="Customer_Id">客户标识</param>
        /// <param name="Unit_Id">门店标识</param>
        /// <returns></returns>
        public BaseInfo GetUserBaseInfoByUserId(string User_id, string Customer_Id,string Unit_Id)
        {
            try
            {
                //根据客户标识,获取数据库连接字符串
                LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
                loggingSessionInfo = GetLoggingSessionInfoByCustomerId(Customer_Id);

                BaseInfo baseInfo = new BaseInfo();
                //1.人员集合
                //baseInfo.CurrSalesUserInfoList = GetUserListByUnitId(loggingSessionInfo, Unit_Id);
                ////2.角色集合
                //baseInfo.CurrRoleInfoList = new RoleService().GetRoleListByUnitId(loggingSessionInfo, Unit_Id);
                ////3.人员与角色关系
                //baseInfo.CurrSalesUserRoleInfoList = new UserRoleService().GetUserRoleListByUnitId(loggingSessionInfo, Unit_Id);
                ////4.菜单集合
                //baseInfo.CurrMenuInfoList = new cAppSysServices().GetAllMenusByAppSysCode(loggingSessionInfo, "CT");
                ////5.角色与菜单关系
                //baseInfo.CurrRoleMenuInfoList = new RoleMenuService().GetRoleMenuListByUnitIdAndAppCode(loggingSessionInfo, Unit_Id, "CT");
                return baseInfo;
            }
            catch (Exception ex) {
                throw (ex);
            }
        }
        #endregion

        #region Dashboard登陆
        /// <summary>
        /// Dashboard登陆
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public UserInfo GetSignInDashboard(UserInfo userInfo)
        {
            UserInfo obj = new UserInfo();
            DataSet ds = userService.GetSignInDashboard(userInfo);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj = DataTableToObject.ConvertToObject<UserInfo>(ds.Tables[0].Rows[0]);
            }
            return obj;
        }
        #endregion

        #region IsExistUserCode
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        public bool IsExistUserCode(string userCode)
        {
            return userService.IsExistUserCode(userCode);
        }
        #endregion

        #region IsExistEmail
        /// <summary>
        /// 判断email是否存在
        /// </summary>
        public bool IsExistEmail(string email)
        {
            return userService.IsExistEmail(email);
        }
        #endregion

        #region GetUserNameByEmail
        /// <summary>
        /// GetUserNameByEmail
        /// </summary>
        public string GetUserNameByEmail(string email)
        {
            return userService.GetUserNameByEmail(email);
        }
        #endregion

        #region GetUserInfoByEmail
        /// <summary>
        /// GetUserInfoByEmail
        /// </summary>
        public UserInfo GetUserInfoByEmail(string email)
        {
            UserInfo userInfo = new UserInfo();
            DataSet ds = userService.GetUserInfoByEmail(email);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                userInfo = DataTableToObject.ConvertToObject<UserInfo>(ds.Tables[0].Rows[0]);
            }
            return userInfo;
        }
        #endregion

        #region UpdateUserPassword
        /// <summary>
        /// UpdateUserPassword
        /// </summary>
        public bool UpdateUserPassword(string userId, string password)
        {
            return userService.UpdateUserPassword(userId, password);
        }
        #endregion

        /// <summary>
        /// 获取发送订单消息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IList<OrderMsgInfo> GetOrderMessage(string orderId)
        {
            DataSet ds = userService.GetOrderMessage(orderId);
            IList<OrderMsgInfo> list = new List<OrderMsgInfo>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<OrderMsgInfo>(ds.Tables[0]);
            }
            return list;
        }
        public IList<OrderMsgInfo> GetOrderMessage2(string orderId)
        {
            DataSet ds = userService.GetOrderMessage2(orderId);
            IList<OrderMsgInfo> list = new List<OrderMsgInfo>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<OrderMsgInfo>(ds.Tables[0]);
            }
            return list;
        }

        /// <summary>
        /// 发送订单消息
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool SendOrderMessage(string orderId)
        {
            var msgList = GetOrderMessage(orderId);
            OrderMessageLogBLL orderMessageLogBLL = new OrderMessageLogBLL(loggingSessionInfo);
            InoutService inoutService = new InoutService(loggingSessionInfo);
            foreach (var msg in msgList)
            {
                if (msg.IsCallEmailPush == 1)
                {
                    string userName = msg.CallUserName;
                    string mailto = msg.CallUserEmail;
                    string mailsubject = "订单信息";
                    var timeText = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

                    //string mailBody = "<div>亲爱的 " + userName + " ，您好！</div><br/>" +
                    //    "<div>您有一条新的订单信息</div><br/>" +
                    //    "<div> 订单号：" + msg.OrderNo + " </div><br/>" +
                    //    "<div> 订单状态：" + msg.OrderStatus + " </div><br/>" +
                    //    "<div> 会员名：" + msg.VipName + " </div><br/>" +
                    //    "<div> 会员手机：" + msg.Phone + " </div><br/>" +
                    //    "<div> 订单金额：" + msg.Amount + " </div><br/>" +
                    //    "<br/>";
                    string mailBody = userName + "您好，您的店铺刚刚收到一笔订单，下单时间：" + timeText
                        + "，单号：" + msg.OrderNo + "，订单金额：" + msg.Amount.ToString("f2") + "元，实付金额：" + msg.ActualAmount.ToString("f2") + "元，货到付款，请及时关注并发货。";
                    if (msg.OrderStatusCode == "2")
                    {
                        mailBody = userName + "您好，您的店铺刚刚收到一笔订单，下单时间：" + timeText
                            + "，单号：" + msg.OrderNo + "，订单金额：" + msg.Amount.ToString("f2") + "元，实付金额：" + msg.ActualAmount.ToString("f2") + "元，支付宝已付款，请及时确认收款并发货。";
                    }
                    mailsubject = mailBody;

                    var success = JIT.Utility.Notification.Mail.SendMail(
                        mailto, mailsubject, mailBody);
                    OrderMessageLogEntity logObj = new OrderMessageLogEntity();
                    logObj.LogId = Utils.NewGuid();
                    logObj.VipId = msg.UserId;
                    logObj.OrderId = msg.OrderNo;
                    logObj.IsCallEmailPush = "1";
                    logObj.MsgBody = mailBody;
                    logObj.CreateTime = DateTime.Now;
                    logObj.CreateBy = loggingSessionInfo.CurrentUser.User_Id;
                    logObj.LastUpdateTime = DateTime.Now;
                    logObj.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
                    if (success)
                    {
                        logObj.MsgStatus = 1;
                        orderMessageLogBLL.Create(logObj);
                    }
                    else
                    {
                        logObj.MsgStatus = 2;
                        logObj.MsgError = "邮件发送失败";
                        orderMessageLogBLL.Create(logObj);
                    }
                }
                if (msg.IsCallSMSPush == 1)
                {
                    string userName = msg.CallUserName;
                    string mailto = msg.CallUserPhone;
                    var timeText = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

                    //string mailBody = "亲爱的 " + userName + " ，您有一条新的订单！";
                    string mailBody = userName + "您好，您的店铺刚刚收到一笔订单，下单时间：" + timeText
                        + "，单号：" + msg.OrderNo + "，订单金额：" + msg.Amount.ToString("f2") + "元，实付金额：" + msg.ActualAmount.ToString("f2") + "元，货到付款，请及时关注并发货。";
                    if (msg.OrderStatusCode == "2")
                    {
                        mailBody = userName + "您好，您的店铺刚刚收到一笔订单，下单时间：" + timeText
                            + "，单号：" + msg.OrderNo + "，订单金额：" + msg.Amount.ToString("f2") + "元，实付金额：" + msg.ActualAmount.ToString("f2") + "元，支付宝已付款，请及时确认收款并发货。";
                    }

                    var success = SMSSend(mailto, mailBody);
                    OrderMessageLogEntity logObj = new OrderMessageLogEntity();
                    logObj.LogId = Utils.NewGuid();
                    logObj.VipId = msg.UserId;
                    logObj.OrderId = msg.OrderNo;
                    logObj.IsCallSMSPush = "1";
                    logObj.MsgBody = mailBody;
                    logObj.CreateTime = DateTime.Now;
                    logObj.CreateBy = loggingSessionInfo.CurrentUser.User_Id;
                    logObj.LastUpdateTime = DateTime.Now;
                    logObj.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
                    if (success)
                    {
                        logObj.MsgStatus = 1;
                        orderMessageLogBLL.Create(logObj);
                    }
                    else
                    {
                        logObj.MsgStatus = 2;
                        logObj.MsgError = "短信发送失败";
                        orderMessageLogBLL.Create(logObj);
                    }
                }
            }

            var msgList2 = GetOrderMessage2(orderId);
            foreach (var msg in msgList2)
            {
                if (msg.Email != null && msg.Email.Trim().Length > 0)
                {
                    string userName = msg.VipName;
                    string mailto = msg.Email;
                    string mailsubject = "订单信息";
                    var timeText = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

                    //string mailBody = "<div></div><br/>" +
                    //    "<div>有一条新的订单信息</div><br/>" +
                    //    "<div> 订单号：" + msg.OrderNo + " </div><br/>" +
                    //    "<div> 订单状态：" + msg.OrderStatus + " </div><br/>" +
                    //    "<div> 订单状态编码：" + msg.OrderStatusCode + " </div><br/>" +
                    //    "<div> 会员名：" + msg.VipName + " </div><br/>" +
                    //    "<div> 订单金额：" + msg.Amount + " </div><br/>" +
                    //    "<br/>";
                    var detailList = inoutService.GetInoutDetailInfoByOrderId(msg.OrderId);
                    var orderNames = "";
                    if (detailList != null && detailList.Count > 0)
                    {
                        foreach (var detailItem in detailList)
                        {
                            orderNames += " \"" + detailItem.item_name + "\", ";
                        }
                        if (orderNames.EndsWith(", "))
                        {
                            orderNames = orderNames.Substring(0, orderNames.Length - 2);
                        }
                    }

                    //string mailBody = userName + "您购买" + orderNames + "的订单我们已经收到，我们会尽快为您安排发货，欢迎您再次光临。";
                    string mailBody = userName + "您购买" + orderNames + "的订单我们已经收到，您支付后我们会尽快发货，更多惊喜，请关注“微讯网O2OMarketing”微信公众号，我们期待您的光临。";
                    if (msg.OrderStatusCode == "2")
                    {
                        //mailBody = userName + "您购买" + orderNames + "的商品钱款我们已经收到，我们会尽快为您安排发货，欢迎您再次光临。";
                        mailBody = userName + "您购买" + orderNames + "的商品钱款我们已经收到，我们会尽快为您安排发货，更多惊喜，请关注“微讯网O2OMarketing”微信公众号，我们期待您的光临。";
                    }
                    mailsubject = mailBody;

                    var success = JIT.Utility.Notification.Mail.SendMail(
                        mailto, mailsubject, mailBody);
                    OrderMessageLogEntity logObj = new OrderMessageLogEntity();
                    logObj.LogId = Utils.NewGuid();
                    logObj.VipId = msg.VipId;
                    logObj.OrderId = msg.OrderNo;
                    logObj.IsCallEmailPush = "1";
                    logObj.MsgBody = mailBody;
                    logObj.CreateTime = DateTime.Now;
                    logObj.CreateBy = loggingSessionInfo.CurrentUser.User_Id;
                    logObj.LastUpdateTime = DateTime.Now;
                    logObj.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
                    if (success)
                    {
                        logObj.MsgStatus = 1;
                        orderMessageLogBLL.Create(logObj);
                    }
                    else
                    {
                        logObj.MsgStatus = 2;
                        logObj.MsgError = "邮件发送失败";
                        orderMessageLogBLL.Create(logObj);
                    }
                }
                if (msg.Phone != null && msg.Phone.Trim().Length > 0)
                {
                    string userName = msg.VipName;
                    string mailto = msg.Phone;
                    var timeText = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

                    //string mailBody = "亲爱的 " + userName + " ，您有一条新的订单！";
                    var detailList = inoutService.GetInoutDetailInfoByOrderId(msg.OrderId);
                    var orderNames = "";
                    if (detailList != null && detailList.Count > 0)
                    {
                        foreach (var detailItem in detailList)
                        {
                            orderNames += " \"" + detailItem.item_name + "\", ";
                        }
                        if (orderNames.EndsWith(", "))
                        {
                            orderNames = orderNames.Substring(0, orderNames.Length - 2);
                        }
                    }

                    //string mailBody = userName + "您购买" + orderNames + "的订单我们已经收到，我们会尽快为您安排发货，欢迎您再次光临。";
                    string mailBody = userName + "您购买" + orderNames + "的订单我们已经收到，您支付后我们会尽快发货，更多惊喜，请关注“微讯网O2OMarketing”微信公众号，我们期待您的光临。";
                    if (msg.OrderStatusCode == "2")
                    {
                        //mailBody = userName + "您购买" + orderNames + "的商品钱款我们已经收到，我们会尽快为您安排发货，欢迎您再次光临。";
                        mailBody = userName + "您购买" + orderNames + "的商品钱款我们已经收到，我们会尽快为您安排发货，更多惊喜，请关注“微讯网O2OMarketing”微信公众号，我们期待您的光临。";
                    }

                    var success = SMSSend(mailto, mailBody);
                    OrderMessageLogEntity logObj = new OrderMessageLogEntity();
                    logObj.LogId = Utils.NewGuid();
                    logObj.VipId = msg.VipId;
                    logObj.OrderId = msg.OrderNo;
                    logObj.IsCallSMSPush = "1";
                    logObj.MsgBody = mailBody;
                    logObj.CreateTime = DateTime.Now;
                    logObj.CreateBy = loggingSessionInfo.CurrentUser.User_Id;
                    logObj.LastUpdateTime = DateTime.Now;
                    logObj.LastUpdateBy = loggingSessionInfo.CurrentUser.User_Id;
                    if (success)
                    {
                        logObj.MsgStatus = 1;
                        orderMessageLogBLL.Create(logObj);
                    }
                    else
                    {
                        logObj.MsgStatus = 2;
                        logObj.MsgError = "短信发送失败";
                        orderMessageLogBLL.Create(logObj);
                    }
                }
            }

            return true;
        }

        public static bool SMSSend(string mobileNO, string SMSContent)
        {
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            string smsContentEncode = HttpUtility.UrlEncode(SMSContent, gb2312);

            string uri = string.Format("http://www.jitmarketing.cn/SendSMS.asp?code=jit2010sms&mobilelist={0}&content={1}",
                mobileNO, smsContentEncode);

            string method = "GET";
            string cotent = "";
            string data = Utils.GetRemoteData(uri, method, cotent);

            return true;
        }

        public IList<UserInfo> GetUserListByRoleCode(string roleCode)
        {
            DataSet ds = userService.GetUserListByRoleCode(roleCode);

            IList<UserInfo> userList = new List<UserInfo>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                userList = DataTableToObject.ConvertToList<UserInfo>(ds.Tables[0]);
            }
            return userList;
        }

        #region CreateQrcode
        /// <summary>
        /// 生成二维码
        /// 
        /// name 姓名
        /// email 邮箱
        /// tel 联系电话
        /// cell 移动电话
        /// address 联系地址
        /// company 公司名称
        /// title 职位
        /// page_url 主页地址
        /// remark 备注
        /// qrImageUrl 二维码地址
        /// </summary>
        public static bool CreateQrcode(string name, string email, string tel, string cell, string address,
            string company, string title, string page_url, string remark, ref string qrImageUrl)
        {
            string host = ConfigurationManager.AppSettings["original_url"].ToString();
            if (!host.EndsWith("/")) host += "/";
            string uri = string.Format(host + "OnlineShopping/data/Data.aspx?Action=createQrcode&name={0}&email={1}&tel={2}&cell={3}&address={4}&company={5}&title={6}&page_url={7}&remark={8}",
                HttpUtility.UrlEncode(name ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(email ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(tel ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(cell ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(address ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(company ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(title ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(page_url ?? "", Encoding.UTF8),
                HttpUtility.UrlEncode(remark ?? "", Encoding.UTF8));
            string method = "GET";
            string cotent = "";
            string data = Utils.GetRemoteData(uri, method, cotent);
            var obj = data.DeserializeJSONTo<CreateQrcodeEntity>();
            if (obj.Code == "200")
            {
                qrImageUrl = obj.qrImageUrl;
                return true;
            }
            return false;
        }
        public class CreateQrcodeEntity
        {
            public string Code;
            public string Description;
            public string Exception;
            public string Data;
            public string qrImageUrl;
        }
        #endregion

        #region RequestParameter 2014-10-16

        /// <summary>
        /// 销售人员信息
        /// </summary>
        public class UserInfoRP : IAPIRequestParameter
        {
            /// <summary>
            /// 门店ID
            /// </summary>
            public string UnitID { get; set; }

            public string ReserveType { get; set; }

            public void Validate()
            {
                if (string.IsNullOrEmpty(UnitID))
                    throw new APIException(201, "门店不能为空！");
            }
        }

        /// <summary>
        /// 销售人员列表
        /// </summary>
        public class UserInfoListRD : IAPIResponseData
        {
            /// <summary>
            /// 销售人员列表信息
            /// </summary>
            public IList<UserInfoRD> SalesmanList { get; set; }

            /// <summary>
            /// 工位列表信息
            /// </summary>
            public IList<ObjectInfoRD> ObjectList { get; set; }
        }

        /// <summary>
        /// 销售人员
        /// </summary>
        public class ObjectInfoRD : IAPIResponseData
        {
            public string ObjectID { get; set; }
            public string ObjectName { get; set; }
        }

        /// <summary>
        /// 销售人员
        /// </summary>
        public class UserInfoRD : IAPIResponseData
        {
            public string SalesmanID { get; set; }
            public string SName { get; set; }
        }

        #endregion


        /// <summary>
        /// excel导入数据库
        /// </summary>
        /// <param name="strPath"></param>
        /// <param name="CurrentUserInfo"></param>
        public DataSet ExcelToDb(string strPath, LoggingSessionInfo CurrentUserInfo)
        {
            DataSet ds; //要插入的数据  
            DataSet dsResult=new DataSet(); //要插入的数据  
            DataTable dt;

            DataTable table = new DataTable("Error");
            //获取列集合,添加列  
            DataColumnCollection columns = table.Columns;
            columns.Add("ErrMsg", typeof(String));



            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + strPath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn); //连接excel              
            if (conn.State.ToString() == "Open")
            {
                conn.Close();
            }
            conn.Open();    //外部表不是预期格式，不兼容2010的excel表结构  
            string s = conn.State.ToString();
            OleDbDataAdapter myCommand = null;
            ds = null;

            string strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, conn);
            ds = new DataSet();
            myCommand.Fill(ds);
            conn.Close();


            try
            {
         

                dt = ds.Tables[0];
                string connString = System.Configuration.ConfigurationManager.AppSettings["Conn_alading"]; //@"user id=dev;password=JtLaxT7668;data source=182.254.219.83,3433;database=cpos_bs_alading;";   //连接数据库的路径方法  
                SqlConnection connSql = new SqlConnection(connString);
                connSql.Open();
                DataRow dr = null;
                int C_Count = dt.Columns.Count;//获取列数  
                if (C_Count == 7)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)  //记录表中的行数，循环插入  
                    {
                        dr = dt.Rows[i];
                        this.userService.insertToSql(dr, C_Count, connSql, CurrentUserInfo.ClientID, CurrentUserInfo.UserID);
                    }

                    connSql.Close();
                    //临时表导入正式表
                    dsResult = this.userService.ExcelImportToDB();
                }
                else
                {

                    DataRow row = table.NewRow();
                    row["ErrMsg"] = "模板列数不对";
                    table.Rows.Add(row);
                    dsResult.Tables.Add(table);

                    DataTable tableCount = new DataTable("Count");
                    DataColumnCollection columns1 = tableCount.Columns;
                    columns1.Add("TotalCount", typeof(Int16));
                    columns1.Add("ErrCount", typeof(Int16));
                    row = tableCount.NewRow();
                    row["TotalCount"] = "0";
                    row["ErrCount"] = "0";
                    tableCount.Rows.Add(row);
                    dsResult.Tables.Add(tableCount);

                }
            }
            catch (Exception ex)
            {
                DataRow row = table.NewRow();
                row["ErrMsg"] = ex.Message.ToString();
                table.Rows.Add(row);
                dsResult.Tables.Add(table); 
            }

            return dsResult;
        }

    }
}
