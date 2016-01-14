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
    /// ��T_QN_QuestionnaireAnswerRecord�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_QN_QuestionnaireAnswerRecordDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionnaireAnswerRecordEntity>, IQueryable<T_QN_QuestionnaireAnswerRecordEntity>
    {
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <param name="AID">�id</param>
        /// <param name="QNID">�ʾ�id</param>
        /// <returns></returns>
        public T_QN_QuestionnaireAnswerRecordEntity[] GetModelList(object AID, object QNID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(string.Format("select * from [T_QN_QuestionnaireAnswerRecord] where 1=1  and QuestionnaireID='{0}' and  ActivityID='{1}' order by CreateTime desc ", QNID, AID));
            //��ȡ����
            List<T_QN_QuestionnaireAnswerRecordEntity> list = new List<T_QN_QuestionnaireAnswerRecordEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireAnswerRecordEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list.ToArray();
        }

        /// <summary>
        /// ��ȡ�����û�����
        /// </summary>
        /// <param name="AID">�id</param>
        /// <param name="QNID">�ʾ�id</param>
        /// <returns></returns>
        public string[] GetUserModelList(object AID, object QNID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(string.Format("SELECT VipID,max(CreateTime) as CreateTime  from T_QN_QuestionnaireAnswerRecord   where QuestionnaireID='{0}' and  ActivityID='{1}'  GROUP BY VipID ORDER BY CreateTime desc   ", QNID, AID));
            //��ȡ����

            List<string> list = new List<string>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    list.Add(rdr[0].ToString());
                }
            }
            //����
            return list.ToArray();
        }
    }
}
