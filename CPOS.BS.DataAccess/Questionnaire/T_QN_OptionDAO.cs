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
    /// ��T_QN_Option�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_QN_OptionDAO : Base.BaseCPOSDAO, ICRUDable<T_QN_OptionEntity>, IQueryable<T_QN_OptionEntity>
    {
        /// <summary>
        /// ��ȡ������Ŀ��ѡ���
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        public T_QN_OptionEntity[] GetListByQuestionID(string QuestionID)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(string.Format("select * from [T_QN_Option] where QuestionID='{0}' and isdelete=0 order by sort ", QuestionID));
            //��ȡ����
            List<T_QN_OptionEntity> list = new List<T_QN_OptionEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_QN_OptionEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list.ToArray();
        }
    }
}
