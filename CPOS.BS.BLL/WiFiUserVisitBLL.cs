/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/10 12:44:22
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
    public partial class WiFiUserVisitBLL
    {

        #region  ��ѯ��Ӧʱ���ڵ�����
        /// <summary>
        /// ��ѯ��Ӧʱ���ڵ�����
        /// </summary>
        /// <param name="unitID">�ŵ�ID</param>
        /// <param name="minMinute">��С����</param>
        /// <param name="maxMinute">������(ֵ-1�������������)</param>
        /// <returns></returns>
        public int GetVipNum(string unitID, int minMinute, int maxMinute)
        {
            return _currentDAO.GetVipNum(unitID, minMinute, maxMinute);
        }
        #endregion


        #region  ��ѯȫ����ǰ����
        /// <summary>
        /// ��ѯȫ����ǰ����
        /// </summary>
        /// <param name="unitID">�ŵ�ID</param>
        /// <param name="isNowCount">�Ƿ��Ǽ��㵱ǰ����</param>
        /// <returns></returns>
        public int GetVipNumAllOrNow(string unitID, bool isNowCount)
        {
            return _currentDAO.GetVipNumAllOrNow(unitID, isNowCount);
        }
        #endregion


        #region  ��ҳ��ѯ��ǰ������Ϣ
        /// <summary>
        /// ��ҳ��ѯ��ǰ������Ϣ
        /// </summary>
        /// <param name="unitID">�ŵ�ID</param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public DataSet GetVipListByPage(string unitID, int PageIndex, int PageSize)
        {
            return _currentDAO.GetVipListByPage(unitID, PageIndex, PageSize);
        }
        #endregion


        #region  �����ŵ�ĳ����Ա��ϸ
        /// <summary>
        /// �����ŵ�ĳ����Ա��ϸ
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetVipDetailList(string strWhere)
        {
            return _currentDAO.GetVipDetailList(strWhere);
        }
        #endregion


        #region �ж������Ƿ��������
        /// <summary>
        /// �ж������Ƿ��������
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool IsExists(string strWhere)
        {
            return _currentDAO.IsExists(strWhere);
        }
        #endregion


        #region  ����������ȡʵ��
        /// <summary>
        /// ����������ȡʵ��
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public WiFiUserVisitEntity GetByWhere(string strWhere)
        {
            return _currentDAO.GetByWhere(strWhere);
        }
        #endregion

    }
}