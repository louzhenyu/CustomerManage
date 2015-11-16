/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:10
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
    /// ��VipCardBalanceChange�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardBalanceChangeDAO : Base.BaseCPOSDAO, ICRUDable<VipCardBalanceChangeEntity>, IQueryable<VipCardBalanceChangeEntity>
    {
        #region �������¼
        /// <summary>
        /// ��ȡ���������¼
        /// </summary>
        /// <param name="VipCardCode">����</param>
        /// <param name="pPageSize">Ҷ��С</param>
        /// <param name="pCurrentPageIndex">Ҷ����</param>
        /// <returns>����</returns>
        public PagedQueryResult<VipCardBalanceChangeEntity> GetVipCardBalanceChangeList(string VipCardCode, int pPageSize, int pCurrentPageIndex)
        {
            #region ��ѯsql
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top {0} * from (  ", pPageSize);
            strSql.Append("select ROW_NUMBER () OVER (ORDER BY  a.CreateTime desc");
            strSql.Append(") AS RowNumber,a.*,b.unit_name,c.user_name,oi.ImageURL from VipCardBalanceChange as a ");
            strSql.Append("left join t_unit as b on a.UnitID=b.unit_id ");
            strSql.Append("left join T_User as c on a.CreateBy=c.user_id ");
            strSql.Append("left join ObjectImages oi on oi.ObjectID=a.ChangeID and oi.IsDelete=0 ");
            strSql.AppendFormat("where a.IsDelete = 0 and  a.VipCardCode='{0}' ", VipCardCode);
            strSql.AppendFormat(") as h where RowNumber >{0}*({1}-1 ) ", pPageSize, pCurrentPageIndex);

            #region ��ҳsql
            //��ҳ
            StringBuilder totalCountSql = new StringBuilder();
            totalCountSql.AppendFormat("select count(1) from VipCardBalanceChange where IsDelete = 0 and VipCardCode='{0}' ", VipCardCode);
            #endregion
            #endregion
            #region ִ��,ת��ҵ��ʵ�壬��ҳ���Ը�ֵ
            PagedQueryResult<VipCardBalanceChangeEntity> result = new PagedQueryResult<VipCardBalanceChangeEntity>();
            List<VipCardBalanceChangeEntity> list = new List<VipCardBalanceChangeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(strSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardBalanceChangeEntity m;
                    this.Load(rdr, out m);
                    if (rdr["unit_name"] != DBNull.Value)
                    {
                        m.UnitName = rdr["unit_name"].ToString();
                    }
                    if (rdr["user_name"] != DBNull.Value)
                    {
                        m.CreateByName = rdr["user_name"].ToString();
                    }
                    if (rdr["ImageURL"] != DBNull.Value)
                    {

                        m.ImageURL=rdr["ImageURL"].ToString();
                    }
                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(totalCountSql.ToString()));    //����������

            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pPageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            #endregion

            return result;
        }
        #endregion
    }
}
