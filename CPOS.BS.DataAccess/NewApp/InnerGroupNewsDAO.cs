/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-7-13 14:00:31
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
    /// ��InnerGroupNews�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class InnerGroupNewsDAO : Base.BaseCPOSDAO, ICRUDable<InnerGroupNewsEntity>, IQueryable<InnerGroupNewsEntity>
    {

        public DataSet GetInnerGroupNewsList(int pageIndex, int pageSize, string OrderBy, string sortType, string UserID, string CustomerID, string DeptID)
        {
            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = "a.CREATETIME";
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC";
            var sqlCon = "";
            if (!string.IsNullOrEmpty(CustomerID))
            {
                sqlCon += " and a.CustomerID = '" + CustomerID + "'";
            }
            if (!string.IsNullOrEmpty(DeptID) && DeptID!="-1")
            {
                sqlCon += " and a.DeptID = '" + DeptID + "'";
            }
            List<SqlParameter> ls = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(UserID))
            {
                //and���������ǰ���Ѿ���false�ˣ�������жϾͲ�ִ����
                sqlCon += " and exists ( select 1 from newsusermapping where groupnewsid=a.groupnewsid  and USERID=@UserID )";//�������ڵ��ڵģ�����ǰ���ڼ��������¾ʹ��ڹ����յ�
                ls.Add(new SqlParameter("@UserID", UserID));
            }

            //if (!string.IsNullOrEmpty(ContinueExpensesStatus))//֧��״̬
            //{
            //    sqlCon += " and (case  when a.haspay=0 then 'δ����' when haspay=1 then '�Ѹ���' end)= '" + PayStatus + "'";

            //}
          ;
            //ls.Add(new SqlParameter("@UserID", UserID));
        //   ls.Add(new SqlParameter("@CustomerID", customerID));

            //�쿨����vip����
            var sql = @" 
select a.*  from InnerGroupNews a 
                                 WHERE 1 = 1 AND    a.isdelete = 0 
   {4}
                 ";  //�����ݵı�tab[0]
            sql = sql + @"select * from ( select ROW_NUMBER()over(order by {0} {3}) _row,a.*,b.DeptName
                                    , isnull(( select count(1) from newsusermapping where groupnewsid=a.GroupNewsID and isdelete=0 ),0)  as NewsUserCount
                                    , isnull(( select count(1) from newsusermapping where groupnewsid=a.GroupNewsID and isdelete=0 and hasread=1 ),0)  as ReadUserCount
                                    , CONVERT(varchar(50),a.CreateTime,23) CreateTimeStr
                                    , CONVERT(varchar(100), a.CreateTime, 120) spanNowStr
                                    ,isnull((select user_name from t_user where user_id=a.CreateBy  ),'') CreateByName
                                    from InnerGroupNews a left join T_dept b on a.deptID=b.deptID 
                                 WHERE 1 = 1 AND    a.isdelete = 0 
   {4}  
                                ) t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}
";

            sql = string.Format(sql, OrderBy, pageIndex - 1, pageSize, sortType, sqlCon);
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql.ToString(), ls.ToArray());
        }

        public void DeleteInnerGroupNews(string groupnewsid)
        {
            string sql = @"update  InnerGroupNews set isdelete=1 where groupnewsid=@groupnewsid
                update  newsusermapping set isdelete=1 where groupnewsid=@groupnewsid ";
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@groupnewsid", groupnewsid));

            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());

        }


    }
}
