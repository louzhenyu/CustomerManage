/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/2/28 17:54:09
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��ObjectEvaluation�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ObjectEvaluationDAO : JIT.CPOS.BS.DataAccess.Base.BaseCPOSDAO, ICRUDable<ObjectEvaluationEntity>, IQueryable<ObjectEvaluationEntity>
    {
        //pClientID�ǿͻ�ID��pMemberID��ָ��ԱID
        public ObjectEvaluationEntity[] GetByVIPAndObject(string pClientID,string pMemberID, string pObjectID, int page, int pagesize)
        {
            return null;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }
        /// <summary>
        /// ��ȡ��Ʒ���۸���
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="starLevel">���۵ȼ�</param>
        /// <returns></returns>
        public int GetEvaluationCount(string objectID, int starLevel)
        {
            string sql =string.Format("SELECT COUNT(1) FROM [ObjectEvaluation] WHERE [ObjectID]='{0}' AND StarLevel={1} AND IsDelete=0",objectID,starLevel);
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));  
        }
    }
}
