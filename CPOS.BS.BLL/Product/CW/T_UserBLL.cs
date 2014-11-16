/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/23 14:33:03
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_UserBLL
    {
        #region 宝洁用户登录
        /// <summary>
        /// 宝洁用户登录
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet LoginPGUser(string customerID, string email, string userName, string pTypeID)
        {
            return _currentDAO.LoginPGUser(customerID, email, userName, pTypeID);
        }
        #endregion

        #region 宝洁

        #region 根据UserID获取用户信息

        /// <summary>
        /// 根据Email集合返回用户ID结合
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pEmailList"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetUserByEmailList(string pCustomerID, List<string> pEmailList, string pTypeID)
        {
            return _currentDAO.GetUserByEmailList(pCustomerID, pEmailList, pTypeID);
        }

        /// <summary>
        /// 根据UserID获取用户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="pUserID"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetPgUserByID(string pCustomerID, string pUserID, string pTypeID)
        {
            return _currentDAO.GetPgUserByID(pCustomerID, pUserID, pTypeID);
        }

        #endregion

        #region 返回指定字段的宝洁用户列表信息
        /// <summary>
        /// 返回指定字段的宝洁用户列表信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetPGSimpUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            return this._currentDAO.GetPGSimpUserInfo(customerID, email, phone, userCode, userName, pageIndex, pageSize, pTypeID);
        }
        #endregion

        #region 返回所有宝洁用户信息列表
        /// <summary>
        /// 返回所有宝洁用户信息列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetPGUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            return this._currentDAO.GetPGUserInfo(customerID, email, phone, userCode, userName, pageIndex, pageSize, pTypeID);
        }
        #endregion

        #region 返回宝洁用户信息变动的用户列表
        /// <summary>
        /// 返回宝洁用户信息变动的用户列表
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pTypeID"></param>
        /// <param name="pLastUpdateTime"></param>
        /// <returns></returns>
        public DataSet GetPGTestingChangeUserInfo(string pCustomerID, string pTypeID, string pLastUpdateTime)
        {
            return this._currentDAO.GetPGTestingChangeUserInfo(pCustomerID, pTypeID, pLastUpdateTime);
        }
        #endregion


        #region 宝洁Channel
        public DataSet GetPgChannel()
        {
            return _currentDAO.GetPgChannel();
        }
        #endregion

        #endregion

        #region 企信登录
        /// <summary>
        /// 企信登录
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pEmail"></param>
        /// <param name="pUserName"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet LoginQiXinUser(string pCustomerID, string pEmail, string pUserName, string pTypeID)
        {
            return _currentDAO.LoginQiXinUser(pCustomerID, pEmail, pUserName, pTypeID);
        }
        #endregion

        #region 返回指定字段的用户列表信息
        /// <summary>
        /// 返回指定字段的用户列表信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetSimpUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            return this._currentDAO.GetSimpUserInfo(customerID, email, phone, userCode, userName, pageIndex, pageSize, pTypeID);
        }
        #endregion

        #region 返回所有用户信息列表
        /// <summary>
        /// 返回所有用户信息列表
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="pTypeID"></param>
        /// <returns></returns>
        public DataSet GetUserInfo(string customerID, string email, string phone, string userCode, string userName, int pageIndex, int pageSize, string pTypeID)
        {
            return this._currentDAO.GetUserInfo(customerID, email, phone, userCode, userName, pageIndex, pageSize, pTypeID);
        }
        #endregion

        #region 返回用户信息变动的用户列表
        /// <summary>
        /// 返回用户信息变动的用户列表
        /// </summary>
        /// <param name="pCustomerID"></param>
        /// <param name="pTypeID"></param>
        /// <param name="pLastUpdateTime"></param>
        /// <returns></returns>
        public DataSet GetUserInfo(string pCustomerID, string pTypeID, string pLastUpdateTime)
        {
            return this._currentDAO.GetUserInfo(pCustomerID, pTypeID, pLastUpdateTime);
        }
        #endregion

        #region 获取用户单实例
        /// <summary>
        /// 获取用户单实例
        /// </summary>
        /// <param name="pID">user_id</param>
        public T_UserEntity GetUserEntityByID(object pID)
        {
            return _currentDAO.GetUserEntityByID(pID);
        }

        public T_UserEntity GetUserEntityByEmail(string email, string customerId)
        {
            return _currentDAO.GetUserEntityByEmail(email, customerId);
        }

        #endregion

        /// <summary>
        /// 获取头像
        /// </summary>
        /// <returns></returns>
        public DataSet GetObjectImages(string pObjectID)
        {
            return _currentDAO.GetObjectImages(pObjectID);
        }


        #region 执行提交数据
        /// <summary>
        /// 执行提交数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public int SubmitSql(string pSql)
        {
            return this._currentDAO.SubmitSql(pSql);
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public DataSet SearchSql(string pSql)
        {
            return this._currentDAO.SearchSql(pSql);
        }
        #endregion

        #region 获取角色权限
        public DataSet GetUserRights(string pUserID, string pCustomerID)
        {
            return this._currentDAO.GetUserRights(pUserID, pCustomerID);
        }
        /// <summary>
        /// 据RightCode获取用户是否有权限
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pCustomerID"></param>
        /// <param name="pRightCode"></param>
        /// <returns></returns>
        public bool IsHasPower(string pUserID, string pRightCode)
        {
            return this._currentDAO.IsHasPower(pUserID, pRightCode);
        }
        #endregion

        #region 导入PG用户
        public void ExportData(string pCustomerID, int pPageIndex, int pPageSize, LoggingSessionInfo pLoggingSessionInfo)
        {
            _currentDAO.ExportData(pCustomerID, pPageIndex, pPageSize, pLoggingSessionInfo);
        }
        #endregion

        #region 保存图片和缩略图
        /// <summary>
        /// 保存原图片和缩略图
        /// 图片对象FileData
        /// </summary>
        /// <param name="pOldFile">原图对象</param>
        /// <param name="PThumbs">缩略图集合对象</param>
        /// <param name="pObjectID">图片对象ID</param>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pUserID">创建修改UserID</param>
        public void SaveImageThumbs(FileData pOldFile, IList<FileData> pThumbs, string pObjectID, string pCustomerID, string pUserID)
        {
            _currentDAO.SaveImageThumbs(pOldFile, pThumbs, pObjectID, pCustomerID, pUserID);
        }

        /// <summary>
        /// 保存原图片和缩略图
        /// 图片对象FileData
        /// pIsDel 1是0否
        /// </summary>
        /// <param name="pOldFile">原图对象</param>
        /// <param name="PThumbs">缩略图集合对象</param>
        /// <param name="pObjectID">图片对象ID</param>
        /// <param name="pCustomerID">客户ID</param>
        /// <param name="pUserID">创建修改UserID</param>
        /// <param name="pIsDel">是否标志删除ObjectId以前的图片</param>
        public void SaveImageThumbs(FileData pOldFile, IList<FileData> pThumbs, string pObjectID, string pCustomerID, string pUserID, string pIsDel)
        {
            _currentDAO.SaveImageThumbs(pOldFile, pThumbs, pObjectID, pCustomerID, pUserID, pIsDel);
        }
        #endregion

        public void InsertUserRole(TUserRoleEntity entity)
        {
            _currentDAO.InsertUserRole(entity);
        }


        #region 用户列表-qxht

        #region 获取用户列表
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pUserId"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pPageSize"></param>
        /// <param name="totalPage"></param>
        /// <returns></returns>
        public DataTable GetUserList(string pUserId, int pPageIndex, int pPageSize, out int totalPage, QueryUserEntity entity)
        {
            return this._currentDAO.GetUserList(pUserId, pPageIndex, pPageSize, out totalPage, entity);
        }
        #endregion


        #region 员工字典
        /// <summary>
        /// 员工字典
        /// </summary>
        /// <param name="pUserID"></param>
        /// <param name="pUserName"></param>
        /// <returns></returns>
        public DataTable GetStaffDict(string pUserID, string pUserName)
        {
            return this._currentDAO.GetStaffDict(pUserID, pUserName);
        }
        #endregion

        #endregion
    }
}