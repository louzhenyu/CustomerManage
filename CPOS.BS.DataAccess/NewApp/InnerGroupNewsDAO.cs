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
            if (!string.IsNullOrEmpty(DeptID) && DeptID != "-1")
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



        #region ��Ա���һ����Ϣҵ��{����}
        /// <summary>
        /// �ҵ��̻�������Ϣ ��ҳ��ʾ
        /// </summary>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="pageSize">ÿҳ����</param>
        /// <param name="UserID">�û����</param>
        /// <param name="CustomerID">�̻����</param>
        /// <param name="NoticePlatformType">ƽ̨���{1=΢���û� 2=APPԱ��}</param>
        /// <returns></returns>
        public PagedQueryResult<InnerGroupNewsEntity> GetVipInnerGroupNewsList(int pageIndex, int pageSize, string UserID, string CustomerID, int NoticePlatformType, int? BusType)
        {
            #region ��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            pagedSql.Append(@"SELECT * FROM (
                    select  ROW_NUMBER()over(order by a.CreateTime desc) _row,
                        a.GroupNewsId AS 'GroupNewsId',
						a.Title  AS 'Title',
						a.Text AS 'Text',
						a.CreateTime AS 'CreateTime',
                        a.CreateTime AS 'MsgTime',
					    a.CreateBy AS 'CreateBy',
						a.LastUpdateBy AS 'LastUpdateBy',
						a.IsDelete,
						a.DeptID,
						a.SentType,
						IsRead=ISNULL(mum.HasRead,0), --�Ƿ��Ѷ���־
						a.BusType,
                        a.LastUpdateTime,
                        a.CustomerID,
                        a.NoticePlatformType,
                        a.ObjectId
                        from InnerGroupNews a 
                        LEFT JOIN newsusermapping mum ON mum.GroupNewsId=a.GroupNewsId  AND mum.UserId=@UserId
                        WHERE 1 = 1 AND a.isdelete = 0 
                        and a.CustomerID = @CustomerID
                        AND a.NoticePlatformType=@NoticePlatformTypeId AND a.IsDelete=0 --1=΢���û� 2=APPԱ��
                         ) AS T WHERE T._row> @LimitPageCount AND _row<= @MaxPageCount ");

            if (BusType != null && BusType != 0)
            {
                pagedSql.Append(" AND BusType='" + BusType + "'");
            }
            SqlParameter[] pagedParameter = new SqlParameter[5]{
                            new SqlParameter("@UserId",UserID),
                            new SqlParameter("@CustomerID",CustomerID),
                            new SqlParameter("@NoticePlatformTypeId",NoticePlatformType),
                            new SqlParameter("@LimitPageCount",pageIndex*pageSize),
                            new SqlParameter("@MaxPageCount",(pageIndex+1)*pageSize)
            };

            #region ��¼����sql
            totalCountSql.Append(@"select count(1) from InnerGroupNews as a WHERE  a.CustomerID =@CustomerID
                                AND a.NoticePlatformType=@NoticePlatformTypeId AND a.IsDelete=0");

            if (BusType != null && BusType != 0)
            {
                totalCountSql.Append(" AND BusType='" + BusType + "'");
            }


            SqlParameter[] totalParameter = new SqlParameter[2]{
                            new SqlParameter("@CustomerID",CustomerID),
                            new SqlParameter("@NoticePlatformTypeId",NoticePlatformType)
                        };
            #endregion
            #endregion
            #region ִ��,ת��ʵ��,��ҳ���Ը�ֵ
            PagedQueryResult<InnerGroupNewsEntity> result = new PagedQueryResult<InnerGroupNewsEntity>();
            List<InnerGroupNewsEntity> list = new List<InnerGroupNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), pagedParameter))
            {
                while (rdr.Read())
                {
                    InnerGroupNewsEntity m;
                    this.Load(rdr, out m);
                    if (rdr["IsRead"] != DBNull.Value)
                    {
                        m.IsRead = Convert.ToString(rdr["IsRead"]);
                    }
                    list.Add(m);
                }
            }

            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, totalCountSql.ToString(), totalParameter));    //����������
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            #endregion
            return result;
        }

        /// <summary>
        /// �ҵ��̻�δ����Ϣ
        /// </summary>
        /// <param name="UserID">�û����</param>
        /// <param name="CustomerID">�̻����</param>
        /// <param name="NoticePlatformType">ƽ̨���{1=΢���û� 2=APPԱ��}</param>
        /// <returns></returns>
        public int GetVipInnerGroupNewsUnReadCount(string UserID, string CustomerID, int? NoticePlatformType, string NewsGroupId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
                    select COUNT(*) AS 'UnReadCount' from InnerGroupNews a 
                    LEFT JOIN newsusermapping mum ON mum.GroupNewsId=a.GroupNewsId  AND mum.UserId=@UserId
                    WHERE a.isdelete = 0 
                    and a.CustomerID =@CustomerID
                  ");
            if (!String.IsNullOrEmpty(NewsGroupId))
            {
                sb.Append(" AND a.GroupNewsId='" + NewsGroupId + "'");
            }

            if (NoticePlatformType != null)
            {
                sb.Append(" AND a.NoticePlatformType='" + NoticePlatformType + "'");

            }

            sb.Append("  AND (mum.HasRead IS NULL OR mum.HasRead=0)");

            SqlParameter[] parameter = new SqlParameter[2];
            parameter[0] = new SqlParameter("@CustomerID", CustomerID);
            parameter[1] = new SqlParameter("@UserId", UserID);

            var ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sb.ToString(), parameter);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["UnReadCount"]);
            }
            return 0;
        }

        /// <summary>
        /// ��ȡ��һ����Ϣ/��һ����Ϣ ���ߵ�ǰ ��Ϣ
        /// </summary>
        /// <param name="CustomerId">�̻����</param>
        /// <param name="model">operationtype��0=��ǰ��Ϣ 1=��һ����Ϣ 2=��һ����Ϣ��</param>
        /// <param name="NoticePlatformType">ƽ̨��ţ�1=΢���û� 2=APPԱ����</param>
        /// <returns>
        /// </returns>
        public InnerGroupNewsEntity GetVipInnerGroupNewsDetailsByPaging(string CustomerId, int operationtype, int NoticePlatformTypeId, string GroupNewsId)
        {

            string expression = string.Empty;
            string SortOrder = string.Empty;
            if (operationtype == 1)
            {
                expression = " < ";
                SortOrder = "DESC";
            }
            else if (operationtype == 0)
            {
                expression = " >= ";
                SortOrder = "ASC";
            }
            else
            {
                expression = " > ";
                SortOrder = "ASC";
            }
            StringBuilder sbentitysql = new StringBuilder();


            sbentitysql.Append(@"select COUNT(*) AS 'PageIndex'
                    from InnerGroupNews a  
                    WHERE a.NoticePlatformType=@NoticePlatformType AND a.createtime {0} (SELECT CreateTime FROM InnerGroupNews WHERE GroupNewsId=@GroupNewsId)  AND CustomerId=@CustomerId"); //��ȡ��ǰ�̻�����һ����Ϣ����һ����Ϣ ͬʱ��ȡ��ǰ����

            sbentitysql.Append(@" select TOP 1 a.GroupNewsId AS 'GroupNewsId' ,a.Title AS 'Title',a.Text AS 'Text',a.ObjectId,a.CreateTime
                    from InnerGroupNews a  
                    WHERE a.NoticePlatformType=@NoticePlatformType AND a.createtime {0} (SELECT CreateTime FROM InnerGroupNews WHERE GroupNewsId=@GroupNewsId)  AND CustomerId=@CustomerId
                    ORDER BY a.CreateTime {1}"); //��ȡ��ǰ�̻�����һ����Ϣ����һ����Ϣ ͬʱ��ȡ��ǰ����
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@NoticePlatformType",NoticePlatformTypeId),
                new SqlParameter("@GroupNewsId",GroupNewsId),
                new SqlParameter("@CustomerId",CustomerId)
            };

            var sql = string.Format(sbentitysql.ToString(), expression, SortOrder);
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);

            InnerGroupNewsEntity entity = new InnerGroupNewsEntity();

            entity.PageIndex = -1;

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0] != null)
                        {
                            entity.PageIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["PageIndex"]);
                        }
                    }
                }

                if (ds.Tables[1] != null)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (ds.Tables[1].Rows[0] != null)
                        {
                            entity.Title = ds.Tables[1].Rows[0]["Title"] + "";
                            entity.Text = ds.Tables[1].Rows[0]["Text"] + "";
                            entity.ObjectId = ds.Tables[1].Rows[0]["ObjectId"] + "";
                            entity.GroupNewsId = ds.Tables[1].Rows[0]["GroupNewsId"] + "";
                            entity.CreateTime = Convert.ToDateTime(ds.Tables[1].Rows[0]["CreateTime"]);
                        }
                    }
                }
            }
            return entity;
        }
        #endregion

    }
}
