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
    /// ҵ����  
    /// </summary>
    public partial class T_UserBLL
    {
        #region �����û���¼
        /// <summary>
        /// �����û���¼
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

        #region ����

        #region ����UserID��ȡ�û���Ϣ

        /// <summary>
        /// ����Email���Ϸ����û�ID���
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
        /// ����UserID��ȡ�û���Ϣ
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

        #region ����ָ���ֶεı����û��б���Ϣ
        /// <summary>
        /// ����ָ���ֶεı����û��б���Ϣ
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

        #region �������б����û���Ϣ�б�
        /// <summary>
        /// �������б����û���Ϣ�б�
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

        #region ���ر����û���Ϣ�䶯���û��б�
        /// <summary>
        /// ���ر����û���Ϣ�䶯���û��б�
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


        #region ����Channel
        public DataSet GetPgChannel()
        {
            return _currentDAO.GetPgChannel();
        }
        #endregion

        #endregion

        #region ���ŵ�¼
        /// <summary>
        /// ���ŵ�¼
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

        #region ����ָ���ֶε��û��б���Ϣ
        /// <summary>
        /// ����ָ���ֶε��û��б���Ϣ
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

        #region ���������û���Ϣ�б�
        /// <summary>
        /// ���������û���Ϣ�б�
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

        #region �����û���Ϣ�䶯���û��б�
        /// <summary>
        /// �����û���Ϣ�䶯���û��б�
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

        #region ��ȡ�û���ʵ��
        /// <summary>
        /// ��ȡ�û���ʵ��
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
        /// ��ȡͷ��
        /// </summary>
        /// <returns></returns>
        public DataSet GetObjectImages(string pObjectID)
        {
            return _currentDAO.GetObjectImages(pObjectID);
        }


        #region ִ���ύ����
        /// <summary>
        /// ִ���ύ����
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public int SubmitSql(string pSql)
        {
            return this._currentDAO.SubmitSql(pSql);
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="pSql"></param>
        /// <returns></returns>
        public DataSet SearchSql(string pSql)
        {
            return this._currentDAO.SearchSql(pSql);
        }
        #endregion

        #region ��ȡ��ɫȨ��
        public DataSet GetUserRights(string pUserID, string pCustomerID)
        {
            return this._currentDAO.GetUserRights(pUserID, pCustomerID);
        }
        /// <summary>
        /// ��RightCode��ȡ�û��Ƿ���Ȩ��
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

        #region ����PG�û�
        public void ExportData(string pCustomerID, int pPageIndex, int pPageSize, LoggingSessionInfo pLoggingSessionInfo)
        {
            _currentDAO.ExportData(pCustomerID, pPageIndex, pPageSize, pLoggingSessionInfo);
        }
        #endregion

        #region ����ͼƬ������ͼ
        /// <summary>
        /// ����ԭͼƬ������ͼ
        /// ͼƬ����FileData
        /// </summary>
        /// <param name="pOldFile">ԭͼ����</param>
        /// <param name="PThumbs">����ͼ���϶���</param>
        /// <param name="pObjectID">ͼƬ����ID</param>
        /// <param name="pCustomerID">�ͻ�ID</param>
        /// <param name="pUserID">�����޸�UserID</param>
        public void SaveImageThumbs(FileData pOldFile, IList<FileData> pThumbs, string pObjectID, string pCustomerID, string pUserID)
        {
            _currentDAO.SaveImageThumbs(pOldFile, pThumbs, pObjectID, pCustomerID, pUserID);
        }

        /// <summary>
        /// ����ԭͼƬ������ͼ
        /// ͼƬ����FileData
        /// pIsDel 1��0��
        /// </summary>
        /// <param name="pOldFile">ԭͼ����</param>
        /// <param name="PThumbs">����ͼ���϶���</param>
        /// <param name="pObjectID">ͼƬ����ID</param>
        /// <param name="pCustomerID">�ͻ�ID</param>
        /// <param name="pUserID">�����޸�UserID</param>
        /// <param name="pIsDel">�Ƿ��־ɾ��ObjectId��ǰ��ͼƬ</param>
        public void SaveImageThumbs(FileData pOldFile, IList<FileData> pThumbs, string pObjectID, string pCustomerID, string pUserID, string pIsDel)
        {
            _currentDAO.SaveImageThumbs(pOldFile, pThumbs, pObjectID, pCustomerID, pUserID, pIsDel);
        }
        #endregion

        public void InsertUserRole(TUserRoleEntity entity)
        {
            _currentDAO.InsertUserRole(entity);
        }


        #region �û��б�-qxht

        #region ��ȡ�û��б�
        /// <summary>
        /// ��ȡ�û��б�
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


        #region Ա���ֵ�
        /// <summary>
        /// Ա���ֵ�
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