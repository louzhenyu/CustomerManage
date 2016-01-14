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
    /// ��T_QN_Question�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_QN_QuestionDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionEntity>, IQueryable<T_QN_QuestionEntity>
    {
        #region ���¶���������

        /// <summary>
        /// �����ʾ�id��ȡ���ݼ���
        /// </summary>
        /// <param name="QuestionnaireID">�ʾ�id</param>
        /// <returns></returns>
        public T_QN_QuestionEntity[] getList(string QuestionnaireID)
        {
            string sql = string.Empty;
            sql += " select * from [T_QN_Question] where 1=1  and isdelete=0  ";
            sql += " and   CONVERT(VARCHAR(500), Questionid) in (select QuestionID from T_QN_QuestionNaireQuestionMapping where  isdelete=0  and QuestionnaireID = '" + QuestionnaireID.Replace("\"", "\'") + "' ) order by sort ";

            //ִ��SQL
            List<T_QN_QuestionEntity> list = new List<T_QN_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }

        /// <summary>
        /// �����ʾ�id��ȡ��ѡ�͸�ѡ���ݼ���
        /// </summary>
        /// <param name="QuestionnaireID">�ʾ�id</param>
        /// <returns></returns>
        public T_QN_QuestionEntity[] getOptionQuestionList(string QuestionnaireID)
        {
            string sql = string.Empty;
            sql += " select * from [T_QN_Question] where 1=1  and isdelete=0 and (QuestionidType=3 or QuestionidType=4 or QuestionidType=9 or QuestionidType=10 ) ";
            sql += " and   CONVERT(VARCHAR(500), Questionid) in (select QuestionID from T_QN_QuestionNaireQuestionMapping where  isdelete=0  and QuestionnaireID = '" + QuestionnaireID.Replace("\"", "\'") + "' ) order by sort ";

            //ִ��SQL
            List<T_QN_QuestionEntity> list = new List<T_QN_QuestionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }

        #endregion   
    }
}
