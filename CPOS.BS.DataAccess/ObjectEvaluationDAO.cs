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
using JIT.CPOS.Entity;

namespace JIT.CPOS.DataAccess
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
            StringBuilder sub = new StringBuilder();
            if (!string.IsNullOrEmpty(pMemberID))
                sub.AppendLine(string.Format(" and a.ClientID='{0}'", pClientID));
            if (!string.IsNullOrEmpty(pMemberID))
                sub.AppendLine(string.Format(" and a.MemberID='{0}'", pMemberID));
            if (!string.IsNullOrEmpty(pObjectID))
                sub.AppendLine(string.Format(" and a.objectid='{0}'", pObjectID));
            List<ObjectEvaluationEntity> list = new List<ObjectEvaluationEntity> { };
            StringBuilder sql = new StringBuilder(string.Format(@"select row_number() over(order by createtime desc) _row, a.*
                                                                  from ObjectEvaluation a 
                                                                  where a.isdelete=0 {0}", sub));//����row_number
            //�����������sql��ƴװ
            sql = new StringBuilder(string.Format("select * from ({0}) t where t._row>{1}*{2} and t.row<=({1}+1)*{2}", sql, page, pagesize));
            sql.AppendLine(string.Format(@"select count(*) from from ObjectEvaluation a
                                            where a.isdelete=0 
                                            {0}", sub));//��һ�仰�Ǽ�����������
            DataSet ds;
            ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            var count = Convert.ToInt32(ds.Tables[1].Rows[0][0]);//�ڶ�����������������
            using (var rd = ds.Tables[0].CreateDataReader())//��reader��ȡ���ٶȿ�
            {
                while (rd.Read())
                {
                    ObjectEvaluationEntity m;//���m
                    this.Load(rd, out m);
                    m.MemberName = rd["vipname"].ToString();
                    m.Count = count;//ÿ���ﶼ��ֵ������
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
