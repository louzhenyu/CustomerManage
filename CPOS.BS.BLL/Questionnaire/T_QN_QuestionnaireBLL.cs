/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:36
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
    /// ҵ������  
    /// </summary>
    public partial class T_QN_QuestionnaireBLL
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
        /// ���ݻid����ȡʵ��
        /// </summary>
        /// <param name="AID">�id</param>
        /// <returns></returns>
        public T_QN_QuestionnaireEntity GetByAID(object AID)
        {
            return _currentDAO.GetByAID(AID);
        }

        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="QuestionnaireName">�ʾ�����</param>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<T_QN_QuestionnaireEntity> PagedQuery(string QuestionnaireName, IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            return _currentDAO.PagedQuery(QuestionnaireName, pWhereConditions, pOrderBys, pPageSize, pCurrentPageIndex);
        }
    }
}