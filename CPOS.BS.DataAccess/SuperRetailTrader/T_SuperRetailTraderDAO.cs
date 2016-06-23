/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 9:08:39
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
    /// ��T_SuperRetailTrader�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_SuperRetailTraderDAO : Base.BaseCPOSDAO, ICRUDable<T_SuperRetailTraderEntity>, IQueryable<T_SuperRetailTraderEntity>
    {
        /// <summary>
        ///  ��ȡ��������Ϣ{ע���Ϊ������֮ǰ����ϢҪĬ�Ϲ��˵�}
        /// </summary>
        /// <param name="customerId">�̻�����</param>
        /// <param name="PageIndex">��ǰҳ</param>
        /// <param name="PageSize">ҳ��</param>
        /// <param name="HigheSuperRetailTraderID">���ڵ���Ϣ</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int PageIndex, int PageSize, string customerId)
        {

            #region ��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            pagedSql.Append(" SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY  ");

            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" a.JoinTime desc"); //Ĭ��Ϊ����ֵ����
            }


            pagedSql.Append(@") AS RowNumber,a.SuperRetailTraderID,
                                            a.SuperRetailTraderCode,
                                            a.SuperRetailTraderName,
                                            a.SuperRetailTraderLogin,
                                            a.SuperRetailTraderPass,
                                            a.SuperRetailTraderMan,
                                            a.SuperRetailTraderPhone,
                                            a.SuperRetailTraderAddress,
                                            a.SuperRetailTraderFrom,
                                            a.SuperRetailTraderFromId,
                                            a.HigheSuperRetailTraderID,
                                            a.JoinTime,a.CreateTime,a.CreateBy,a.LastUpdateBy,a.LastUpdateTime,a.IsDelete,a.CustomerId,a.Status,
                                            WithdrawCount=(SELECT COUNT(*) FROM VipWithdrawDepositApply WHERE VipId=a.SuperRetailTraderFromId),
                                            OrderCount=(SELECT COUNT(*) FROM T_inout WHERE sales_user=cast(a.SuperRetailTraderID AS VARCHAR(500))),
                                            WithdrawTotalMoney=(SELECT SUM(Amount) FROM VipWithdrawDepositApply WHERE VipId=a.SuperRetailTraderFromId),
                                            NumberOffline=(SELECT COUNT(*) FROM T_SuperRetailTrader WHERE HigheSuperRetailTraderID=a.SuperRetailTraderID AND Status='10')
                                 from T_SuperRetailTrader as a  ");
            //pagedSql.Append(" left JOIN T_SuperRetailTrader AS b ON b.HigheSuperRetailTraderID=a.SuperRetailTraderID"); //����� 
            // pagedSql.Append(" left JOIN VipWithdrawDepositApply AS vwda ON vwda.VipID=a.SuperRetailTraderFromId AND vwda.Status=2 AND vwda.IsDelete=0  AND vwda.ApplyDate>=a.JoinTime"); //����� 
            // pagedSql.Append(" left JOIN T_inout AS ti ON ti.sales_user=a.SuperRetailTraderID AND ti.customer_id=@CustomerId  AND  ti.order_date>=a.JoinTime "); //�ɽ� ��δ�ɽ� ���� �Ѻ� ���� ȷ�������������ж�������
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",customerId)
            };
            pagedSql.Append("  Where 1=1 ");

            #region ��¼����sql
            totalCountSql.Append("select count(1) from T_SuperRetailTrader as a ");

            totalCountSql.Append("Where 1=1 ");
            #endregion

            //��������
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
//            pagedSql.Append(@"
//                                GROUP BY a.SuperRetailTraderID,
//                                a.SuperRetailTraderCode,
//                                a.SuperRetailTraderName,
//                                a.SuperRetailTraderLogin,
//                                a.SuperRetailTraderPass,
//                                a.SuperRetailTraderMan,
//                                a.SuperRetailTraderPhone,
//                                a.SuperRetailTraderAddress,
//                                a.SuperRetailTraderFrom,
//                                a.SuperRetailTraderFromId,
//                                a.HigheSuperRetailTraderID,
//                                a.JoinTime,
//                                a.CreateTime,a.CreateBy,a.LastUpdateBy,a.LastUpdateTime,a.IsDelete,a.CustomerId,a.Status");
            pagedSql.AppendFormat(") as h where RowNumber >{0}*({1}-1) AND RowNumber<={0}*{1} ", PageSize, PageIndex);
            #endregion


            #region ִ��,ת��ʵ��,��ҳ���Ը�ֵ
            PagedQueryResult<T_SuperRetailTraderEntity> result = new PagedQueryResult<T_SuperRetailTraderEntity>();
            List<T_SuperRetailTraderEntity> list = new List<T_SuperRetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameter))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);

                    if (rdr["OrderCount"] != DBNull.Value)
                    {
                        m.OrderCount = Convert.ToInt32(rdr["OrderCount"] == null ? "0" : rdr["OrderCount"]);
                    }

                    if (rdr["WithdrawCount"] != DBNull.Value)
                    {
                        m.WithdrawCount = Convert.ToInt32(rdr["WithdrawCount"] == null ? "0" : rdr["WithdrawCount"]);
                    }

                    if (rdr["WithdrawTotalMoney"] != DBNull.Value)
                    {
                        m.WithdrawTotalMoney = Convert.ToDecimal(rdr["WithdrawTotalMoney"] == null ? "0" : rdr["WithdrawTotalMoney"]);
                    }

                    if (rdr["NumberOffline"] != DBNull.Value)
                    {
                        m.NumberOffline = Convert.ToInt32(rdr["NumberOffline"] == null ? "0" : rdr["NumberOffline"]);
                    }
                }
            }
            result.Entities = list.ToArray();

            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, PageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            #endregion
            return result;
        }
        /// <summary>
        ///  ��ȡ��������Ϣ{ע���Ϊ������֮ǰ����ϢҪĬ�Ϲ��˵�} ���� ����Ҫ��ҳ
        /// </summary>
        /// <param name="customerId">�̻�����</param>
        /// <param name="HigheSuperRetailTraderID">���ڵ���Ϣ</param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderEntity> FindListByCustomerId(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, string customerId)
        {

            #region ��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            pagedSql.Append(" SELECT * FROM (select ROW_NUMBER() OVER (ORDER BY  ");

            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" a.JoinTime desc"); //Ĭ��Ϊ����ֵ����
            }


            pagedSql.Append(@") AS RowNumber,a.SuperRetailTraderID,
                                            a.SuperRetailTraderCode,
                                            a.SuperRetailTraderName,
                                            a.SuperRetailTraderLogin,
                                            a.SuperRetailTraderPass,
                                            a.SuperRetailTraderMan,
                                            a.SuperRetailTraderPhone,
                                            a.SuperRetailTraderAddress,
                                            a.SuperRetailTraderFrom,
                                            a.SuperRetailTraderFromId,
                                            a.HigheSuperRetailTraderID,
                                            a.JoinTime,a.CreateTime,a.CreateBy,a.LastUpdateBy,a.LastUpdateTime,a.IsDelete,a.CustomerId,a.Status,
                                            WithdrawCount=(SELECT COUNT(*) FROM VipWithdrawDepositApply WHERE VipId=a.SuperRetailTraderFromId),
                                            OrderCount=(SELECT COUNT(*) FROM T_inout WHERE sales_user=cast(a.SuperRetailTraderID as varchar(500))),
                                            WithdrawTotalMoney=(SELECT SUM(Amount) FROM VipWithdrawDepositApply WHERE VipId=a.SuperRetailTraderFromId),
                                           NumberOffline=(SELECT COUNT(*) FROM T_SuperRetailTrader WHERE HigheSuperRetailTraderID=a.SuperRetailTraderID AND Status='10')
                                 from T_SuperRetailTrader as a  ");
            //pagedSql.Append(" left JOIN T_SuperRetailTrader AS b ON b.HigheSuperRetailTraderID=a.SuperRetailTraderID"); //����� 
            // pagedSql.Append(" left JOIN VipWithdrawDepositApply AS vwda ON vwda.VipID=a.SuperRetailTraderFromId AND vwda.Status=2 AND vwda.IsDelete=0  AND vwda.ApplyDate>=a.JoinTime"); //����� 
            // pagedSql.Append(" left JOIN T_inout AS ti ON ti.sales_user=a.SuperRetailTraderID AND ti.customer_id=@CustomerId  AND  ti.order_date>=a.JoinTime "); //�ɽ� ��δ�ɽ� ���� �Ѻ� ���� ȷ�������������ж�������
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",customerId)
            };
            pagedSql.Append("  Where 1=1 ");

            #region ��¼����sql
            totalCountSql.Append("select count(1) from T_SuperRetailTrader as a ");

            totalCountSql.Append("Where 1=1 ");
            #endregion

            //��������
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if (item != null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.Append(@"
                                GROUP BY a.SuperRetailTraderID,
                                a.SuperRetailTraderCode,
                                a.SuperRetailTraderName,
                                a.SuperRetailTraderLogin,
                                a.SuperRetailTraderPass,
                                a.SuperRetailTraderMan,
                                a.SuperRetailTraderPhone,
                                a.SuperRetailTraderAddress,
                                a.SuperRetailTraderFrom,
                                a.SuperRetailTraderFromId,
                                a.HigheSuperRetailTraderID,
                                a.JoinTime,
                                a.CreateTime,a.CreateBy,a.LastUpdateBy,a.LastUpdateTime,a.IsDelete,a.CustomerId,a.Status");
            pagedSql.AppendFormat(") as h  ");
            #endregion


            #region ִ��,ת��ʵ��,��ҳ���Ը�ֵ
            PagedQueryResult<T_SuperRetailTraderEntity> result = new PagedQueryResult<T_SuperRetailTraderEntity>();
            List<T_SuperRetailTraderEntity> list = new List<T_SuperRetailTraderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameter))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);

                    if (rdr["OrderCount"] != DBNull.Value)
                    {
                        m.OrderCount = Convert.ToInt32(rdr["OrderCount"] == null ? "0" : rdr["OrderCount"]);
                    }

                    if (rdr["WithdrawCount"] != DBNull.Value)
                    {
                        m.WithdrawCount = Convert.ToInt32(rdr["WithdrawCount"] == null ? "0" : rdr["WithdrawCount"]);
                    }

                    if (rdr["WithdrawTotalMoney"] != DBNull.Value)
                    {
                        m.WithdrawTotalMoney = Convert.ToDecimal(rdr["WithdrawTotalMoney"] == null ? "0" : rdr["WithdrawTotalMoney"]);
                    }

                    if (rdr["NumberOffline"] != DBNull.Value)
                    {
                        m.NumberOffline = Convert.ToInt32(rdr["NumberOffline"] == null ? "0" : rdr["NumberOffline"]);
                    }
                }
            }
            result.Entities = list.ToArray();

            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������
            result.RowCount = totalCount;

            #endregion
            return result;
        }

        /// <summary>
        /// ��ȡ���и���
        /// </summary>
        /// <param name="strSuperRetailTraderId"></param>
        /// <returns></returns>
        public DataSet GetAllFather(string strSuperRetailTraderId)
        {
            string strSql = string.Format(@"  WITH SuperRetailTraderFather([SuperRetailTraderID], [HigheSuperRetailTraderID],level)
                                              AS
                                              ( 
                                                SELECT [SuperRetailTraderID],[HigheSuperRetailTraderID],1 
                                                FROM [T_SuperRetailTrader]
                                                WHERE [SuperRetailTraderID] = '{0}'
 
                                                UNION all
                                                SELECT e.[SuperRetailTraderID],e.[HigheSuperRetailTraderID],es.level+1
                                                FROM [T_SuperRetailTrader] AS e
                                                  JOIN SuperRetailTraderFather AS es
                                                    ON es.[HigheSuperRetailTraderID] = e.[SuperRetailTraderID]
                                              )
                                                SELECT * FROM SuperRetailTraderFather", strSuperRetailTraderId);
            return this.SQLHelper.ExecuteDataset(strSql);
        }
    }
}
