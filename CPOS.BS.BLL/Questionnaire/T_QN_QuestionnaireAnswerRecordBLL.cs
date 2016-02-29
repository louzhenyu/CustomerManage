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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class T_QN_QuestionnaireAnswerRecordBLL
    {

        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="AID">�id</param>
        /// <param name="QNID">�ʾ�id</param>
        /// <returns></returns>
        public T_QN_QuestionnaireAnswerRecordEntity[] GetModelList(object AID, object QNID)
        {
            return _currentDAO.GetModelList(AID,QNID);
        }


        /// <summary>
        /// ��ȡ�����û�����
        /// </summary>
        /// <param name="AID">�id</param>
        /// <param name="QNID">�ʾ�id</param>
        /// <returns></returns>
        public string[] GetUserModelList(object AID, object QNID)
        {
            return _currentDAO.GetUserModelList(AID, QNID);
        }

        /// <summary>
        /// ����ÿ���û������ʶ����ɾ��
        /// </summary>
        /// <param name="vipIDs">�û������ʶ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void DeletevipIDs(object[] vipIDs)
        {
            _currentDAO.DeletevipIDs(vipIDs, null);
            
        }

    }
}