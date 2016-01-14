/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:37
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
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��T_QN_ScoreRecoveryInformation�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_QN_ScoreRecoveryInformationDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_ScoreRecoveryInformationEntity>, IQueryable<T_QN_ScoreRecoveryInformationEntity>
    {
        /// <summary>
        /// �����ʾ�id��ȡ���ݼ���
        /// </summary>
        /// <param name="QuestionnaireID">�ʾ�id</param>
        /// <returns></returns>
        public T_QN_ScoreRecoveryInformationEntity[] getList(string QuestionnaireID)
        {
            string sql = string.Empty;
            sql += " select * from [T_QN_ScoreRecoveryInformation] where 1=1  and isdelete=0  ";
            sql += " and   QuestionnaireID ='"+ QuestionnaireID.Replace("\"", "\'") + "' ";

            //ִ��SQL
            List<T_QN_ScoreRecoveryInformationEntity> list = new List<T_QN_ScoreRecoveryInformationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_ScoreRecoveryInformationEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }

        /// <summary>
        /// �����ʾ�id��ȡ���ݼ���
        /// </summary>
        /// <param name="QuestionnaireID">�ʾ�id</param>
        /// <returns></returns>
        public T_QN_ScoreRecoveryInformationEntity[] getScoreRecoveryInformationByScore(string QuestionnaireID, string sumScore)
        {
            string sql = string.Empty;
            sql += " select * from [T_QN_ScoreRecoveryInformation] where 1=1  and isdelete=0  ";
            sql += " and   QuestionnaireID ='" + QuestionnaireID.Replace("\"", "\'") + "'  and " + sumScore + ">=MinScore and " + sumScore + "<=MaxScore ";

            //ִ��SQL
            List<T_QN_ScoreRecoveryInformationEntity> list = new List<T_QN_ScoreRecoveryInformationEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_ScoreRecoveryInformationEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
    }
}
