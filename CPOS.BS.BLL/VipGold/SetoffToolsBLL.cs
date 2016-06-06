/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 13:59:40
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class SetoffToolsBLL
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// ��ȡ���͹�������
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ApplicationType"></param>
        /// <param name="pBeShareVipID"></param>
        /// <returns></returns>
        public int GeSetoffToolsListCount(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, string pSetoffEventID)
        {
            return _currentDAO.GeSetoffToolsListCount(entity, ApplicationType, pBeShareVipID, pSetoffEventID);
        }
        /// <summary>
        /// ��ȡ���͹����б�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ApplicationType"></param>
        /// <param name="pBeShareVipID"></param>
        /// <param name="Page"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public DataSet GetSetoffToolsList(SetoffToolsEntity entity, string ApplicationType, string pBeShareVipID, int Page, int PageSize, string pSetoffEventID)
        {
            return _currentDAO.GetSetoffToolsList(entity, ApplicationType, pBeShareVipID, Page, PageSize, pSetoffEventID);
        }
        /// <summary>
        /// ��ȡδ���ͻ��������
        /// </summary>
        /// <param name="ShareVipType">������ID</param>
        /// <param name="BeShareVipID">��������ID</param>
        /// <param name="BusTypeCode">��������</param>
        /// <returns></returns>
        public int GetIsPushCount(string ShareVipType, string BeShareVipID, string BusTypeCode, string SetOffEventID)
        {
            return _currentDAO.GetIsPushCount(ShareVipType, BeShareVipID, BusTypeCode, SetOffEventID);
        }

        public DataSet GetToolsDetails(string SetoffEventID)
        {
            return this._currentDAO.GetToolsDetails(SetoffEventID);
        }

        /// <summary>
        /// ��ȡδ�����͹��߸���
        /// </summary>
        /// <param name="UserId">�û����</param>
        /// <param name="CustomerId">�̻����</param>
        /// <param name="NoticePlatformTypeId">ƽ̨���1=΢���û� 2=APPԱ��</param>
        /// <param name="SetoffTypeId">1=��Ա���� 2=Ա������ 3=����������</param>
        /// <returns></returns>
        public int GetUnReadSetoffToolsCount(string UserId, string CustomerId, int NoticePlatformTypeId, int SetoffTypeId)
        {
            return _currentDAO.GetUnReadSetoffToolsCount(UserId, CustomerId, NoticePlatformTypeId, SetoffTypeId);
        }
    }
}