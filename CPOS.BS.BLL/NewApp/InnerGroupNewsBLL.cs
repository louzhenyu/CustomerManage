/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-13 14:00:31
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class InnerGroupNewsBLL
    {

        public DataSet GetInnerGroupNewsList(int pageIndex, int pageSize, string OrderBy, string sortType, string UserID, string CustomerID, string DeptID)
        {
            return _currentDAO.GetInnerGroupNewsList(pageIndex, pageSize, OrderBy, sortType, UserID, CustomerID, DeptID);
        }


        public void DeleteInnerGroupNews(string GroupNewsID)
        {
            _currentDAO.DeleteInnerGroupNews(GroupNewsID);
        }

        #region ��Ա���վ����Ϣ�б�
        /// <summary>
        /// ��ȡ �̻���Ϣ �б� ���շ�ҳ��ѯ��Ϣ
        /// </summary>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="pageSize">úҵ��ʾ����</param>
        /// <param name="UserID">�û����</param>
        /// <param name="CustomerID">�̻����</param>
        /// <param name="NoticePlatformType">ƽ̨��ţ�1=΢���û�2=APPԱ����</param>
        /// <returns></returns>
        public PagedQueryResult<InnerGroupNewsEntity> GetVipInnerGroupNewsList(int pageIndex, int pageSize, string UserID, string CustomerID, int NoticePlatformType, int? BusType, DateTime BeginTime)
        {
            return _currentDAO.GetVipInnerGroupNewsList(pageIndex, pageSize, UserID, CustomerID, NoticePlatformType, BusType, BeginTime);
        }

        /// <summary>
        /// ��ȡδ��վ������Ϣ���� Ĭ�Ϲ���֮ǰ����Ϣ
        /// </summary>
        /// <param name="UserID">�û����</param>
        /// <param name="CustomerID">�̻����</param>
        /// <param name="NoticePlatformType">ƽ̨��ţ�1=΢���û�2=APPԱ����</param>
        /// <param name="CreateTime">��ǰ�û�ע��ʱ��</param>
        /// <returns></returns>
        public int GetVipInnerGroupNewsUnReadCount(string UserID, string CustomerID, int NoticePlatformType, string NewsGroupId, DateTime CreateTime)
        {
            return _currentDAO.GetVipInnerGroupNewsUnReadCount(UserID, CustomerID, NoticePlatformType, NewsGroupId, CreateTime);
        }

        /// <summary>
        /// ���� �̻����| ƽ̨��� ��ȡ�̻�����Ϣ����
        /// </summary>
        /// <param name="customerId">�̻����</param>
        /// <param name="loggingSessionInfo"></param>
        /// <returns></returns>
        public List<InnerGroupNewsEntity> GetMessageByCustomerId(string customerId, int NoticePlatformTypeId, LoggingSessionInfo loggingSessionInfo)
        {
            //���� TotalCount ��  TotalPages
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = customerId });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = 0 });
            lstWhereCondition.Add(new EqualsCondition() { FieldName = "NoticePlatformType", Value = NoticePlatformTypeId });
            InnerGroupNewsBLL newslist = new InnerGroupNewsBLL(loggingSessionInfo);
            List<OrderBy> lstOrderByCondition = new List<OrderBy>();
            lstOrderByCondition.Add(new OrderBy() { FieldName = "CreateTime", Direction = OrderByDirections.Desc });
            var list = newslist.Query(lstWhereCondition.ToArray(), lstOrderByCondition.ToArray()).ToList();//��ȡ��ǰ�̻� ��Ч��Ϣ ����
            return list;
        }

        /// <summary>
        /// ��ȡ��һ����Ϣ/��һ����Ϣ ���ߵ�ǰ ��Ϣ
        /// </summary>
        /// <param name="CustomerId">�̻����</param>
        /// <param name="model">operationtype��0=��ǰ��Ϣ 1=��һ����Ϣ 2=��һ����Ϣ��</param>
        /// <param name="NoticePlatformType">ƽ̨��ţ�1=΢���û� 2=APPԱ����</param>
        /// <param name="CreateTime">��Աע����Ϣ</param>
        /// 
        /// <returns>
        /// </returns>
        public InnerGroupNewsEntity GetVipInnerGroupNewsDetailsByPaging(int operationtype, string CustomerID, int NoticePlatformType, string GroupNewsId, DateTime CreateTime)
        {
            return _currentDAO.GetVipInnerGroupNewsDetailsByPaging(CustomerID, operationtype, NoticePlatformType, GroupNewsId, CreateTime);
        }
        /// <summary>
        /// �鿴ĳ���û���ĳ����Ϣʱ�� ��ȡ����
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customerId"></param>
        /// <param name="KeyId"></param>
        /// <param name="newsusermappingService"></param>
        /// <returns></returns>
        public bool CheckUserIsReadMessage(string userId, string customerId, string GroupNewsId)
        {
            return this._currentDAO.GetVipInnerGroupNewsUnReadCount(userId, customerId, null, GroupNewsId,null) > 0;
        }
        #endregion

    }
}