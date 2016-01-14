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
    /// ��T_QN_QuestionnaireOptionCount�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_QN_QuestionnaireOptionCountDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_QuestionnaireOptionCountEntity>, IQueryable<T_QN_QuestionnaireOptionCountEntity>
    {
        /// <summary>
        /// ����ѡ��id�ͻid��ѯ�Ƿ��������
        /// </summary>
        /// <param name="OptionID"></param>
        /// <param name="ActivityID"></param>
        public T_QN_QuestionnaireOptionCountEntity isExist(string OptionID, string ActivityID)
        {
            //�������
            if (OptionID == null || ActivityID==null)
                return null;
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_QuestionnaireOptionCount] where ActivityID='{0}' and OptionID='{1}'  and isdelete=0 ",ActivityID, OptionID);
            //��ȡ����
            T_QN_QuestionnaireOptionCountEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

        /// <summary>
        /// ����ѡ��id�ͻid��ѯ����
        /// </summary>
        /// <param name="OptionID"></param>
        /// <param name="ActivityID"></param>
        public T_QN_QuestionnaireOptionCountEntity[] GetList(string QuestionnaireID, string ActivityID)
        {
            //�������
            if (QuestionnaireID == null || ActivityID == null)
                return null;
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_QN_QuestionnaireOptionCount] where ActivityID='{0}' and QuestionnaireID='{1}'  and isdelete=0 ", ActivityID, QuestionnaireID);
            //��ȡ����
            List<T_QN_QuestionnaireOptionCountEntity> list = new List<T_QN_QuestionnaireOptionCountEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_QuestionnaireOptionCountEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list.ToArray();
        }
    }
}
