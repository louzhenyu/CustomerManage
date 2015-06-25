/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/5/17 17:27:33
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
using System.Configuration;

namespace JIT.CPOS.BS.DataAccess
{




    /// <summary>
    /// ���ݷ��ʣ� �����̰����ŵ�͸��� 
    /// ��RetailTrader�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class RetailTraderDAO : Base.BaseCPOSDAO, ICRUDable<RetailTraderEntity>, IQueryable<RetailTraderEntity>
    {

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create2Ap(RetailTraderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");

            //��ʼ���̶��ֶ�
            pEntity.IsDelete = 0;
            pEntity.CreateTime = DateTime.Now;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [RetailTrader](");
            strSql.Append("[RetailTraderCode],[RetailTraderName],[RetailTraderLogin],[RetailTraderPass],[RetailTraderType],[RetailTraderMan],[RetailTraderPhone],[RetailTraderAddress],[CooperateType],[SellUserID],[UnitID],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[Status],[RetailTraderID])");
            strSql.Append(" values (");
            strSql.Append("@RetailTraderCode,@RetailTraderName,@RetailTraderLogin,@RetailTraderPass,@RetailTraderType,@RetailTraderMan,@RetailTraderPhone,@RetailTraderAddress,@CooperateType,@SellUserID,@UnitID,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@Status,@RetailTraderID)");

            string pkString = pEntity.RetailTraderID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@RetailTraderCode",SqlDbType.Int),
					new SqlParameter("@RetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderLogin",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPass",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderType",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderMan",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPhone",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderAddress",SqlDbType.NVarChar),
					new SqlParameter("@CooperateType",SqlDbType.NVarChar),
					new SqlParameter("@SellUserID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.RetailTraderCode;
            parameters[1].Value = pEntity.RetailTraderName;
            parameters[2].Value = pEntity.RetailTraderLogin;
            parameters[3].Value = pEntity.RetailTraderPass;
            parameters[4].Value = pEntity.RetailTraderType;
            parameters[5].Value = pEntity.RetailTraderMan;
            parameters[6].Value = pEntity.RetailTraderPhone;
            parameters[7].Value = pEntity.RetailTraderAddress;
            parameters[8].Value = pEntity.CooperateType;
            parameters[9].Value = pEntity.SellUserID;
            parameters[10].Value = pEntity.UnitID;
            parameters[11].Value = pEntity.CreateTime;
            parameters[12].Value = pEntity.CreateBy;
            parameters[13].Value = pEntity.LastUpdateBy;
            parameters[14].Value = pEntity.LastUpdateTime;
            parameters[15].Value = pEntity.IsDelete;
            parameters[16].Value = pEntity.CustomerId;
            parameters[17].Value = pEntity.Status;
            parameters[18].Value = pkString;


            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            //var result = sqlHelper.ExecuteScalar(sql);
            //return result == null || result == DBNull.Value ? string.Empty : result.ToString();

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = sqlHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = sqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.RetailTraderID = pkString;
        }


        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update2Ap(RetailTraderEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.RetailTraderID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [RetailTrader] set ");
            if (pIsUpdateNullField || pEntity.RetailTraderCode != null)
                strSql.Append("[RetailTraderCode]=@RetailTraderCode,");
            if (pIsUpdateNullField || pEntity.RetailTraderName != null)
                strSql.Append("[RetailTraderName]=@RetailTraderName,");
            if (pIsUpdateNullField || pEntity.RetailTraderLogin != null)
                strSql.Append("[RetailTraderLogin]=@RetailTraderLogin,");
            if (pIsUpdateNullField || pEntity.RetailTraderPass != null)
                strSql.Append("[RetailTraderPass]=@RetailTraderPass,");
            if (pIsUpdateNullField || pEntity.RetailTraderType != null)
                strSql.Append("[RetailTraderType]=@RetailTraderType,");
            if (pIsUpdateNullField || pEntity.RetailTraderMan != null)
                strSql.Append("[RetailTraderMan]=@RetailTraderMan,");
            if (pIsUpdateNullField || pEntity.RetailTraderPhone != null)
                strSql.Append("[RetailTraderPhone]=@RetailTraderPhone,");
            if (pIsUpdateNullField || pEntity.RetailTraderAddress != null)
                strSql.Append("[RetailTraderAddress]=@RetailTraderAddress,");
            if (pIsUpdateNullField || pEntity.CooperateType != null)
                strSql.Append("[CooperateType]=@CooperateType,");
            if (pIsUpdateNullField || pEntity.SellUserID != null)
                strSql.Append("[SellUserID]=@SellUserID,");
            if (pIsUpdateNullField || pEntity.UnitID != null)
                strSql.Append("[UnitID]=@UnitID,");

            if (pIsUpdateNullField || pEntity.CustomerId != null)
                strSql.Append("[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.Status != null)
                strSql.Append("[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy != null)
                strSql.Append("[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime != null)
                strSql.Append("[LastUpdateTime]=@LastUpdateTime");

            strSql.Append(" where RetailTraderID=@RetailTraderID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@RetailTraderCode",SqlDbType.Int),
					new SqlParameter("@RetailTraderName",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderLogin",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPass",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderType",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderMan",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderPhone",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderAddress",SqlDbType.NVarChar),
					new SqlParameter("@CooperateType",SqlDbType.NVarChar),
					new SqlParameter("@SellUserID",SqlDbType.NVarChar),
					new SqlParameter("@UnitID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.NVarChar),
					new SqlParameter("@RetailTraderID",SqlDbType.NVarChar)
            };
            parameters[0].Value = pEntity.RetailTraderCode;
            parameters[1].Value = pEntity.RetailTraderName;
            parameters[2].Value = pEntity.RetailTraderLogin;
            parameters[3].Value = pEntity.RetailTraderPass;
            parameters[4].Value = pEntity.RetailTraderType;
            parameters[5].Value = pEntity.RetailTraderMan;
            parameters[6].Value = pEntity.RetailTraderPhone;
            parameters[7].Value = pEntity.RetailTraderAddress;
            parameters[8].Value = pEntity.CooperateType;
            parameters[9].Value = pEntity.SellUserID;
            parameters[10].Value = pEntity.UnitID;
            parameters[11].Value = pEntity.LastUpdateBy;
            parameters[12].Value = pEntity.LastUpdateTime;
            parameters[13].Value = pEntity.CustomerId;
            parameters[14].Value = pEntity.Status;
            parameters[15].Value = pEntity.RetailTraderID;

            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            //ִ�����
            //int result = 0;
            //if (pTran != null)
            //    result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            //else
            //    result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            //ִ�в��������д
            int result;
            if (pTran != null)
                result = sqlHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = sqlHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
          //  pEntity.RetailTraderID = pkString;
        }

        public int getMaxRetailTraderCode(string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            string sql = "select isnull(max(RetailTraderCode),0) from RetailTrader where  CustomerId=@CustomerId";
            int maxCode = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql, ls.ToArray()));    //����������
            return maxCode;
        }


        public DataSet getRetailTraderInfoByLogin(string LoginName, string RetailTraderID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));

            string sql = @"select *,(select  unit_name from t_unit x where x.unit_id=a.UnitID) UnitName
		,(select  user_name from T_User y where y.user_id=a.SellUserID) UserName
		,(select top 1 ImageURL from ObjectImages z where z.ObjectId=convert(nvarchar(50), a.RetailTraderID) and z.IsDelete=0) ImageURL
    ,isnull((select COUNT(1) from vip where Col20=a.RetailTraderID),0) as VipCount
                                    from RetailTrader a where a.CustomerId=@CustomerId ";
            if (!string.IsNullOrEmpty(LoginName))
            {
                ls.Add(new SqlParameter("@LoginName", LoginName));
                sql += " and a.RetailTraderLogin=@LoginName";
            }

            if (!string.IsNullOrEmpty(RetailTraderID))
            {
                ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));
                sql += " and a.RetailTraderID=@RetailTraderID";
            }
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }
        public DataSet getRetailTraderInfoByLogin2(string LoginName, string RetailTraderID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
           // ls.Add(new SqlParameter("@CustomerId", CustomerID));

            string sql = @"select *
                                    from RetailTrader a where 1=1  ";//and a.CustomerId=@CustomerId
            if (!string.IsNullOrEmpty(LoginName))
            {
                ls.Add(new SqlParameter("@LoginName", LoginName));
                sql += " and a.RetailTraderLogin=@LoginName";
            }

            if (!string.IsNullOrEmpty(RetailTraderID))
            {
                ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));
                sql += " and a.RetailTraderID=@RetailTraderID";
            }
            //DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������          
            string conn = ConfigurationManager.AppSettings["APConn"];
            DefaultSQLHelper sqlHelper = new DefaultSQLHelper(conn);
            DataSet ds = sqlHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());
           return ds;
        }



        public DataSet GetRetailTradersBySellUser(string RetailTraderName, string UserID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));

            string sql = @"select *,(select  unit_name from t_unit x where x.unit_id=a.UnitID) UnitName
		,(select  user_name from T_User y where y.user_id=a.SellUserID) UserName
			,(select top 1 ImageURL from ObjectImages z where z.ObjectId=convert(nvarchar(50), a.RetailTraderID) and z.IsDelete=0 order by CreateTime desc) ImageURL
  
     ,isnull((select COUNT(1) from vip where Col20=a.RetailTraderID),0) as VipCount 
                                    from RetailTrader a where a.CustomerId=@CustomerId  AND    a.isdelete = 0  ";
            if (!string.IsNullOrEmpty(UserID))
            {
                ls.Add(new SqlParameter("@UserID", UserID));
                sql += " and a.SellUserID=@UserID";
            }
            if (!string.IsNullOrEmpty(RetailTraderName))
            {
                ls.Add(new SqlParameter("@RetailTraderName", "%" + RetailTraderName + "%"));
                sql += " and a.RetailTraderName like @RetailTraderName";
            }
            sql += "  order by VipCount desc";//���ջ�Ա����������
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }
        /// <summary>
        /// ��̨��ȡ��������Ϣ
        /// </summary>
        /// <param name="RetailTraderName"></param>
        /// <param name="UserID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public DataSet GetRetailTraders(string RetailTraderName, string RetailTraderAddress
, string RetailTraderMan, string Status, string CooperateType, string UnitID, string UserID, string CustomerID, string loginUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = "a.CREATETIME";
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC";

            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@loginUserID", loginUserID));

            string sqlCon = "";
            //if (!string.IsNullOrEmpty(UserID))  //����ԭ���ķ����̵�
            //{
            //    ls.Add(new SqlParameter("@UserID", UserID));
            //    sqlCon += " and a.SellUserID=@UserID";
            //}
            if (!string.IsNullOrEmpty(RetailTraderName))
            {
                ls.Add(new SqlParameter("@RetailTraderName", "%" + RetailTraderName + "%"));
                sqlCon += " and a.RetailTraderName like @RetailTraderName";
            }
            if (!string.IsNullOrEmpty(RetailTraderAddress))
            {
                ls.Add(new SqlParameter("@RetailTraderAddress", "%" + RetailTraderAddress + "%"));
                sqlCon += " and a.RetailTraderAddress like @RetailTraderAddress";
            }
            if (!string.IsNullOrEmpty(RetailTraderMan))
            {
                ls.Add(new SqlParameter("@RetailTraderMan", "%" + RetailTraderMan + "%"));
                sqlCon += " and a.RetailTraderMan like @RetailTraderMan";
            }
            if (!string.IsNullOrEmpty(Status) && Status != "-1")  //1��������0��ͣ��.��ѯʱ���������-1����ѡ�����е�
            {
                ls.Add(new SqlParameter("@Status", Status));
                sqlCon += " and a.Status = @Status";
            }
            if (!string.IsNullOrEmpty(CooperateType))
            {
                ls.Add(new SqlParameter("@CooperateType", CooperateType));
                sqlCon += " and a.CooperateType =@CooperateType";
            }
            if (!string.IsNullOrEmpty(UnitID))
            {
                ls.Add(new SqlParameter("@UnitID", UnitID));
                sqlCon += " and a.UnitID =@UnitID";
            }






            string sql = @"----������Ҫ���û���Ȩ���ܿ��������ݼ���
DECLARE @AllUnit NVARCHAR(200)

CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
 INSERT #UnitSET (UnitID)                  
   SELECT DISTINCT R.UnitID                   
   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  (@CustomerId,UR.unit_id,205)  R                  
   WHERE user_id=@loginUserID          ---�����˻��Ľ�ɫȥ���ɫ��Ӧ��  unit_id

   SELECT @AllUnit=unit_id FROM t_unit WHERE customer_id=@CustomerId  AND type_id='2F35F85CF7FF4DF087188A7FB05DED1D'
   ----��������˸ÿͻ�@CustomerId ���ܵ��unit_id  

    SELECT * INTO #TmpTBL FROM  RetailTrader where CustomerId=@CustomerId  AND ISNULL(CustomerId,'')!=''  -----	 ��Ҫû��CustomerId��
   UPDATE #TmpTBL SET UnitID=@AllUnit WHERE ISNULL(UnitID,'')=''----��CouponInfoΪ�յģ��ŵ��Ϊ�ܲ�  
----ȡ�����������Ϊ��Ա��Ĵ���
select RetailTraderID  into #TempTable	 from  #TmpTBL L , #UnitSET R where L.IsDelete=0
	 AND  L.UnitID=R.UnitID    ----ֻȡ�����˺��ܿ����Ļ�Ա��Ϣ

";
            //�����ݱ�
            sql += @"select a.*  from RetailTrader a
 inner join #TempTable f on a.RetailTraderID=f.RetailTraderID
                                 WHERE 1 = 1 AND    a.isdelete = 0 
   {4}
                and  a.CustomerId=@CustomerId  ";
            //ȡ��ĳһҳ��
            sql += @"select * from ( select  ROW_NUMBER()over(order by {0} {3}) _row,a.*
        ,(select  unit_name from t_unit x where x.unit_id=a.UnitID) UnitName
		,(select  user_name from T_User y where y.user_id=a.SellUserID) UserName
		,(select top 1  ImageURL from ObjectImages z where z.ObjectId=convert(nvarchar(50), a.RetailTraderID) and z.IsDelete=0) ImageURL
     ,isnull((select COUNT(1) from vip where Col20=a.RetailTraderID),0) as VipCount 
    ,(case status when '1' then '����' else 'ͣ��' end)   StatusDesc
,( case CooperateType when 'OneWay' then '���򼯿�' else '���༯��' end ) CooperateTypeDesc
,( select  top 1 isnull(EndAmount,0) from VipAmount where VipId=a.RetailTraderID ) as EndAmount
,isnull((select ImageUrl from WQRCodeManager x where x.ObjectId=a.RetailTraderID),'') as QRImageUrl
                                    from RetailTrader a 
 inner join #TempTable f on a.RetailTraderID=f.RetailTraderID

where a.CustomerId=@CustomerId  AND    a.isdelete = 0   {4} ";


            sql += @") t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}";
            //ʹ������ʱ��Ҫɾ��
            sql += @" drop table #UnitSET
                  drop table #TmpTBL
                  drop table #TempTable";
            sql = string.Format(sql, OrderBy, pageIndex - 1, pageSize, sortType, sqlCon);

            //   sql += "  order by VipCount ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }
        public int GetVipCountBySellUser(string UserID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));

            string sql = @"
          select isnull(count(1),0) from vip a where col20 in 
         (select RetailTraderID from RetailTrader x where x.SellUserID=@UserID and x.CustomerId=@CustomerId)
          ";
            int vipCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql, ls.ToArray()));    //����������
            return vipCount;
        }

        //ĳ���·ݣ�����·�Ϊ-1�����ȡ����
        public DataSet GetMonthVipList(string UserID, string CustomerID, int month, int year)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));


            string sql = @" select * from vip a where 1=1
             and col20 in 
            (select RetailTraderID from RetailTrader x where x.SellUserID=@UserID and x.CustomerId=@CustomerId)";
            if (year != -1)
            {
                ls.Add(new SqlParameter("@year", year));
                sql += " and CONVERT(int, Datename(year,a.CreateTime))=@year";
            }
            if (month != -1)
            {
                ls.Add(new SqlParameter("@month", month));
                sql += " and CONVERT(int, Datename(MONTH,a.CreateTime))=@month";
            }
            sql += " order by createtime desc";//����lastupdatetime��ԭ���������Ա������ܻ����ܶ����飬Ӱ�����ĸ��ġ�
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }

        public int GetMonthTradeCount(string UserID, string CustomerID, int month, int year)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));

            string sql = @" select isnull(count(1),0) from  T_Inout b inner join  vip a on b.vip_no=a.vipid
where 1=1

and b.status=700
and
col20 in 
 (select RetailTraderID from RetailTrader x where x.SellUserID=@UserID and x.CustomerId=@CustomerId)
          ";

            if (month != -1)
            {
                ls.Add(new SqlParameter("@month", month));
                sql += " and CONVERT(int, Datename(MONTH,b.create_time))=@month";
            }
            if (year != -1)
            {
                ls.Add(new SqlParameter("@year", year));
                sql += " and CONVERT(int, Datename(year,b.create_time))=@year";
            }

            int vipCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, sql, ls.ToArray()));    //����������
            return vipCount;
        }




        public DataSet MonthVipRiseTrand(string UserID, string CustomerID, int year)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));


            string sql = @" select convert(int,datename(year,CreateTime)) as Year,convert(int,datename(month,CreateTime)) as Month,COUNT(1) as VipCount
                                from vip a where 1=1
             and col20 in 
            (select RetailTraderID from RetailTrader x where x.SellUserID=@UserID and x.CustomerId=@CustomerId)";
            if (year != -1)
            {
                ls.Add(new SqlParameter("@year", year));
                sql += " and CONVERT(int, Datename(year,a.CreateTime))=@year";
            }
            sql += @"   group by datename(YEAR,CreateTime),datename(month,CreateTime) 
               order by year,month ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }




        /// <summary>
        /// �����̺�����Ա�Ķ������
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="AmountSourceID"></param>
        /// <returns></returns>
        public decimal RetailRewardByAmountSource(string UserID, string CustomerID, int year, int month, int day, string AmountSourceID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();

            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@UserID", UserID));
            string sql = @" select isnull(sum(Amount),0) from  VipAmountDetail b 
where 1=1
and b.vipid=@UserID  and isdelete=0
          ";

            if (year != -1)
            {
                ls.Add(new SqlParameter("@year", year));
                sql += " and CONVERT(int, Datename(year,b.CreateTime))=@year";
            }
            if (month != -1)
            {
                ls.Add(new SqlParameter("@month", month));
                sql += " and CONVERT(int, Datename(MONTH,b.CreateTime))=@month";
            }
            if (day != -1)
            {
                ls.Add(new SqlParameter("@day", day));
                sql += " and CONVERT(int, Datename(day,b.CreateTime))=@day";
            }

            if (!string.IsNullOrEmpty(AmountSourceID))
            {
                ls.Add(new SqlParameter("@AmountSourceID", AmountSourceID));
                sql += " and b.AmountSourceID=@AmountSourceID";
            }

            //            sql += @"   group by datename(YEAR,CreateTime),datename(month,CreateTime) 
            //               ";
            decimal amount = Convert.ToDecimal(this.SQLHelper.ExecuteScalar(CommandType.Text, sql, ls.ToArray()));    //����������
            return amount;
        }





        public DataSet MonthRewards(string UserID, string CustomerID, int year)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));


            string sql = @" select convert(int,datename(year,CreateTime)) as Year
,convert(int,datename(month,CreateTime)) as Month
, isnull(sum(case AmountSourceID when 17 then Amount when 14 then Amount when 15 then Amount else 0 end ),0)  as MonthAmount
                ,isnull(sum(case AmountSourceID when 17 then Amount else 0 end ),0) as MonthVipAmount
              ,  isnull(sum(case AmountSourceID when 14 then Amount when 15 then Amount else 0 end ),0) as MonthTradeAmount
                from  VipAmountDetail b 
                where 1=1  
                and b.vipid=@UserID ";
            if (year != -1)
            {
                ls.Add(new SqlParameter("@year", year));
                sql += " and CONVERT(int, Datename(year,b.CreateTime))=@year";
            }
            sql += @"   group by datename(YEAR,CreateTime),datename(month,CreateTime) 
               order by year,month ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }



        public DataSet MonthDayRewards(string UserID, string CustomerID, int year, int month)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));


            string sql = @" select convert(int,datename(year,CreateTime)) as Year
,convert(int,datename(month,CreateTime)) as Month
,convert(int,datename(day,CreateTime)) as Day
,CONVERT(varchar(100), b.CreateTime, 23) as formatDate
, isnull(sum(case AmountSourceID when 17 then Amount when 14 then Amount when 15 then Amount else 0 end ),0)  as DayAmount
                ,isnull(sum(case AmountSourceID when 17 then Amount else 0 end ),0) as DayVipAmount
              ,  isnull(sum(case AmountSourceID when 14 then Amount when 15 then Amount else 0 end ),0) as DayTradeAmount
                from  VipAmountDetail b 
                where 1=1  
                and b.vipid=@UserID ";
            if (year != -1)
            {
                ls.Add(new SqlParameter("@year", year));
                sql += " and CONVERT(int, Datename(year,b.CreateTime))=@year";
            }
            if (month != -1)
            {
                ls.Add(new SqlParameter("@month", month));
                sql += " and CONVERT(int, Datename(month,b.CreateTime))=@month";
            }
            sql += @"   group by datename(YEAR,CreateTime),datename(month,CreateTime) 
,datename(day,CreateTime) ,CONVERT(varchar(100), b.CreateTime, 23) 
               order by year,month,day ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }



        /// <summary>
        /// ��ʼʱ��ͽ���ʱ��Ŀǰ�Ǳ����е�
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetRewardsDayRiseList(string UserID, string CustomerID, DateTime beginDate, DateTime endDate)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@UserID", UserID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));


            string sql = @" select convert(int,datename(year,CreateTime)) as Year
,convert(int,datename(month,CreateTime)) as Month
,convert(int,datename(day,CreateTime)) as Day
,CONVERT(varchar(100), b.CreateTime, 23) as formatDate
, isnull(sum(case AmountSourceID when 17 then Amount when 14 then Amount when 15 then Amount else 0 end ),0)  as DayAmount
                ,isnull(sum(case AmountSourceID when 17 then Amount else 0 end ),0) as DayVipAmount
              ,  isnull(sum(case AmountSourceID when 14 then Amount when 15 then Amount else 0 end ),0) as DayTradeAmount
                from  VipAmountDetail b 
                where 1=1  
                and b.vipid=@UserID ";

            if (beginDate != null)
            {
                ls.Add(new SqlParameter("@beginDate", beginDate));
                sql += " and b.CreateTime>=@beginDate";
            }
            if (endDate != null)
            {
                ls.Add(new SqlParameter("@endDate", endDate));
                sql += " and b.CreateTime <=@endDate";
            }
            sql += @"   group by datename(YEAR,CreateTime),datename(month,CreateTime) 
,datename(day,CreateTime) ,CONVERT(varchar(100), b.CreateTime, 23) 
               order by year,month,day ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }




        /// <summary>
        /// ��ʼʱ��ͽ���ʱ��Ŀǰ�Ǳ����е�
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet GetVipDayRiseList(string RetailTraderID, string CustomerID, DateTime beginDate, DateTime endDate)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));


            string sql = @" select convert(int,datename(year,CreateTime)) as Year
,convert(int,datename(month,CreateTime)) as Month
,convert(int,datename(day,CreateTime)) as Day
,CONVERT(varchar(100), b.CreateTime, 23) as formatDate
, isnull(count(1),0)  as vipCount
                   from  vip b 
                where 1=1  
                   and b.ClientID=@CustomerId
                and b.Col20=@RetailTraderID ";

            if (beginDate != null)
            {
                ls.Add(new SqlParameter("@beginDate", beginDate));
                sql += " and b.CreateTime>=@beginDate";
            }
            if (endDate != null)
            {
                ls.Add(new SqlParameter("@endDate", endDate));
                sql += " and b.CreateTime <=@endDate";
            }
            sql += @"   group by datename(YEAR,CreateTime),datename(month,CreateTime) 
,datename(day,CreateTime) ,CONVERT(varchar(100), b.CreateTime, 23) 
               order by year,month,day ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }


        //ĳ���·ݣ�����·�Ϊ-1�����ȡ����
        public DataSet GetRetailVipInfos(string RetailTraderID, string CustomerID, int year, int month, int day)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));
            ls.Add(new SqlParameter("@CustomerId", CustomerID));


            string sql = @" select * from vip a where 1=1   and a.ClientID=@CustomerId
             and col20=@RetailTraderID";
            if (year != -1)
            {
                ls.Add(new SqlParameter("@year", year));
                sql += " and CONVERT(int, Datename(year,a.CreateTime))=@year";
            }
            if (month != -1)
            {
                ls.Add(new SqlParameter("@month", month));
                sql += " and CONVERT(int, Datename(MONTH,a.CreateTime))=@month";
            }
            if (day != -1)
            {
                ls.Add(new SqlParameter("@day", day));
                sql += " and CONVERT(int, Datename(day,a.CreateTime))=@day";
            }
            sql += " order by createtime desc ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }

        /// <summary>
        /// ����Ա��ȡ
        /// </summary>
        /// <param name="UnitID"></param>
        /// <param name="SellerOrRetailName"></param>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="UserID"></param>
        /// <param name="CustomerID"></param>
        /// <param name="loginUserID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        public DataSet GetSellerMonthRewardList(string UnitID, string SellerOrRetailName
, int Year, int Month,  string CustomerID, string loginUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {

            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = "a.CREATE_TIME";
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC";

            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@loginUserID", loginUserID));
            ls.Add(new SqlParameter("@Year", Year));
            ls.Add(new SqlParameter("@Month", Month));

            string sqlCon = "";
            //if (!string.IsNullOrEmpty(UserID))  //����ԭ���ķ����̵�
            //{
            //    ls.Add(new SqlParameter("@UserID", UserID));
            //    sqlCon += " and a.SellUserID=@UserID";
            //}
            if (!string.IsNullOrEmpty(SellerOrRetailName))
            {
                ls.Add(new SqlParameter("@User_Name", "%" + SellerOrRetailName + "%"));
                sqlCon += " and a.User_Name like @User_Name";
            }
          
            if (!string.IsNullOrEmpty(UnitID))
            {
                ls.Add(new SqlParameter("@UnitID", UnitID));
                sqlCon += " and exists (select 1 from T_User_Role x where x.user_id=a.user_id and x.default_flag=1 and x.unit_id=@UnitID)";
            }






            string sql = @"----������Ҫ���û���Ȩ���ܿ��������ݼ���
DECLARE @AllUnit NVARCHAR(200)

CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
 INSERT #UnitSET (UnitID)                  
   SELECT DISTINCT R.UnitID                   
   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  (@CustomerId,UR.unit_id,205)  R                  
   WHERE user_id=@loginUserID          ---�����˻��Ľ�ɫȥ���ɫ��Ӧ��  unit_id

   SELECT @AllUnit=unit_id FROM t_unit WHERE customer_id=@CustomerId  AND type_id='2F35F85CF7FF4DF087188A7FB05DED1D'
   ----��������˸ÿͻ�@CustomerId ���ܵ��unit_id  

    SELECT distinct x.user_id,y.unit_id INTO #TmpTBL FROM  t_user x inner join T_User_Role y  on x.user_id=y.user_id where  default_flag=1 and x.customer_id=@CustomerId  AND ISNULL(x.customer_id,'')!=''  -----	 ��Ҫû��CustomerId��
   UPDATE #TmpTBL SET Unit_id=@AllUnit WHERE ISNULL(Unit_id,'')=''----��CouponInfoΪ�յģ��ŵ��Ϊ�ܲ�  
----ȡ�����������Ϊ��Ա��Ĵ���
select user_id  into #TempTable	 from  #TmpTBL L , #UnitSET R where 
	 L.Unit_ID=R.UnitID    ----ֻȡ�����˺��ܿ����Ļ�Ա��Ϣ

";
            //�����ݱ�
            sql += @"select a.*  from t_user a
 inner join #TempTable f on a.user_id=f.user_id
                                 WHERE 1 = 1
   {4}
                and  a.customer_id=@CustomerId  ";
            //ȡ��ĳһҳ��
            sql += @"select * from ( select  ROW_NUMBER()over(order by {0} {3}) _row,a.*
         ,isnull((select top 1 y.unit_name from T_User_Role x inner join t_unit y on x.unit_id=y.unit_id
 where default_flag=1 and x.user_id=a.user_id),'') as UnitName	
---��/��
 ,convert(varchar(4),@Year)+'-'+ convert(varchar(2),@Month) as YearAndMonth
---ĳ���µĽ��� 
 ,isnull((select SUM(Amount) from vipamountdetail where IsDelete=0 and AmountSourceID in ('17','14','15') 
 and year(CreateTime)=@Year and MONTH(CreateTime)=@Month and VipId=a.user_id)  ,0) as MonthAmount

                                    from T_User a 
 inner join #TempTable f on a.user_id=f.user_id

where a.customer_id=@CustomerId    {4} ";


            sql += @") t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}";
            //ʹ������ʱ��Ҫɾ��
            sql += @" drop table #UnitSET
                  drop table #TmpTBL
                  drop table #TempTable";
            sql = string.Format(sql, OrderBy, pageIndex - 1, pageSize, sortType, sqlCon);

            //   sql += "  order by VipCount ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }


        public DataSet GetRetailMonthRewardList(string UnitID, string SellerOrRetailName
, int Year, int Month, string CustomerID, string loginUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {

            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = "a.CREATETIME";
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC";

            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@loginUserID", loginUserID));
            ls.Add(new SqlParameter("@Year", Year));
            ls.Add(new SqlParameter("@Month", Month));

            string sqlCon = "";
            //if (!string.IsNullOrEmpty(UserID))  //����ԭ���ķ����̵�
            //{
            //    ls.Add(new SqlParameter("@UserID", UserID));
            //    sqlCon += " and a.SellUserID=@UserID";
            //}
            if (!string.IsNullOrEmpty(SellerOrRetailName))
            {
                ls.Add(new SqlParameter("@RetailTraderName", "%" + SellerOrRetailName + "%"));
                sqlCon += " and a.RetailTraderName like @RetailTraderName";
            }

            if (!string.IsNullOrEmpty(UnitID))
            {
                ls.Add(new SqlParameter("@UnitID", UnitID));
                sqlCon += " and a.UnitID =@UnitID";
            }



            



            string sql = @"----������Ҫ���û���Ȩ���ܿ��������ݼ���
DECLARE @AllUnit NVARCHAR(200)

CREATE TABLE #UnitSET  (UnitID NVARCHAR(100))   
 INSERT #UnitSET (UnitID)                  
   SELECT DISTINCT R.UnitID                   
   FROM T_User_Role  UR CROSS APPLY dbo.FnGetUnitList  (@CustomerId,UR.unit_id,205)  R                  
   WHERE user_id=@loginUserID          ---�����˻��Ľ�ɫȥ���ɫ��Ӧ��  unit_id

   SELECT @AllUnit=unit_id FROM t_unit WHERE customer_id=@CustomerId  AND type_id='2F35F85CF7FF4DF087188A7FB05DED1D'
   ----��������˸ÿͻ�@CustomerId ���ܵ��unit_id  

    SELECT * INTO #TmpTBL FROM  RetailTrader where CustomerId=@CustomerId  AND ISNULL(CustomerId,'')!=''  -----	 ��Ҫû��CustomerId��
   UPDATE #TmpTBL SET UnitID=@AllUnit WHERE ISNULL(UnitID,'')=''----��CouponInfoΪ�յģ��ŵ��Ϊ�ܲ�  
----ȡ�����������Ϊ��Ա��Ĵ���
select RetailTraderID  into #TempTable	 from  #TmpTBL L , #UnitSET R where L.IsDelete=0
	 AND  L.UnitID=R.UnitID    ----ֻȡ�����˺��ܿ����Ļ�Ա��Ϣ

";
            //�����ݱ�
            sql += @"select a.*  from RetailTrader a
 inner join #TempTable f on a.RetailTraderID=f.RetailTraderID
                                 WHERE 1 = 1 AND    a.isdelete = 0 
   {4}
                and  a.CustomerId=@CustomerId  ";
            //ȡ��ĳһҳ��
            sql += @"select * from ( select  ROW_NUMBER()over(order by {0} {3}) _row,a.*
        ,(select  unit_name from t_unit x where x.unit_id=a.UnitID) UnitName
		,(select  user_name from T_User y where y.user_id=a.SellUserID) UserName
		,(select top 1 ImageURL from ObjectImages z where z.ObjectId=convert(nvarchar(50), a.RetailTraderID) and z.IsDelete=0) ImageURL
     ,isnull((select COUNT(1) from vip where Col20=a.RetailTraderID),0) as VipCount 
    ,(case status when '1' then '����' else 'ͣ��' end)   StatusDesc
,( case CooperateType when 'OneWay' then '���򼯿�' else '���༯��' end ) CooperateTypeDesc
,( select  top 1 isnull(EndAmount,0) from VipAmount where VipId=a.RetailTraderID ) as EndAmount
---ĳ���µĽ��� 
 ,isnull((select SUM(Amount) from vipamountdetail where IsDelete=0 and AmountSourceID in ('17','14','15') 
 and year(CreateTime)=@Year and MONTH(CreateTime)=@Month and VipId=a.RetailTraderID)  ,0) as MonthAmount
---��/��
 ,convert(varchar(4),@Year)+'-'+ convert(varchar(2),@Month) as YearAndMonth
----ĳ���µļ��ͻ�Ա��
,isnull(( select COUNT(1) from vip where IsDelete=0 and Col20=a.RetailTraderID
  and year(CreateTime)=@Year and MONTH(CreateTime)=@Month)  ,0)  as MonthVipCount
----ĳ���µĽ��ױ���
,isnull(  ( select COUNT(1) from T_Inout x inner join vip y on x.vip_no=y.VIPID  
 where x.status=700  ---״̬������700�ģ��Ѿ��ջ�����˵Ķ���
 and y.IsDelete=0 and y.Col20=a.RetailTraderID
 and  year(x.Create_Time)=@Year and MONTH(x.Create_Time)=@Month)    ,0)   as MonthOrderCount

                                    from RetailTrader a 
 inner join #TempTable f on a.RetailTraderID=f.RetailTraderID

where a.CustomerId=@CustomerId  AND    a.isdelete = 0   {4} ";


            sql += @") t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}";
            //ʹ������ʱ��Ҫɾ��
            sql += @" drop table #UnitSET
                  drop table #TmpTBL
                  drop table #TempTable";
            sql = string.Format(sql, OrderBy, pageIndex - 1, pageSize, sortType, sqlCon);

            //   sql += "  order by VipCount ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }




        public DataSet GetRetailCoupon(string RetailTraderID, string CustomerID, int Status, int pageIndex, int pageSize, string OrderBy, string sortType)
        {

            if (string.IsNullOrEmpty(OrderBy))
                OrderBy = "a.CreateTime";
            if (string.IsNullOrEmpty(sortType))
                sortType = "DESC"; List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));

            string sqlCon = "";          
            if (Status!=-1)
            {
                ls.Add(new SqlParameter("@Status",Status));
                sqlCon += " and a.Status=@Status";
            }





          
            //�����ݱ�
            string sql = @"  select c.HeadImgUrl, c.VipName,c.VipRealName,a.CoupnName,a.CouponCode,CONVERT(varchar(50),f.CreateTime,23) as UseTime,a.Status
  ,(case a.Status when 1 then '�Ѻ���' else 'δ����' end) as StatusDesc
  from Coupon a inner join VipCouponMapping b on a.CouponID=b.CouponID
  inner join vip c on b.VIPID=c.VIPID
  left join CouponUse f on a.CouponID=f.CouponID
  inner join CouponType d on a.CouponTypeID=d.CouponTypeID
  left join CouponTypeUnitMapping e on  ( a.CouponTypeID=e.CouponTypeID and e.ObjectID=@RetailTraderID)  -----�����left join ����Ϊ�����SuitableForStore=3����û�����Ź����� 
  where 1=1
  and c.Col20=@RetailTraderID
  and (d.SuitableForStore=3 or (d.SuitableForStore=2 and e.ObjectID=@RetailTraderID))  ----SuitableForStore�����ŵ�(1=�����ŵꡢ2=�����ŵ�/�����̡�3=���з�����)
    {4}
                and  a.CustomerID=@CustomerId  ";
            //ȡ��ĳһҳ��
            sql += @"select * from (  select  ROW_NUMBER()over(order by {0} {3}) _row, c.HeadImgUrl, c.VipName,c.VipRealName,a.CoupnName,a.CouponCode,CONVERT(varchar(50),f.CreateTime,23) as UseTime,a.Status
  ,(case a.Status when 1 then '�Ѻ���' else 'δ����' end) as StatusDesc
  from Coupon a inner join VipCouponMapping b on a.CouponID=b.CouponID
  inner join vip c on b.VIPID=c.VIPID
  left join CouponUse f on a.CouponID=f.CouponID
  inner join CouponType d on a.CouponTypeID=d.CouponTypeID
  left join CouponTypeUnitMapping e on  ( a.CouponTypeID=e.CouponTypeID and e.ObjectID=@RetailTraderID)    -----�����left join ����Ϊ�����SuitableForStore=3����û�����Ź����� 
  where 1=1
  and c.Col20=@RetailTraderID
  and (d.SuitableForStore=3 or (d.SuitableForStore=2 and e.ObjectID=@RetailTraderID))  ----SuitableForStore�����ŵ�(1=�����ŵꡢ2=�����ŵ�/�����̡�3=���з�����)
    {4}
                and  a.CustomerID=@CustomerId";


            sql += @") t
                                    where t._row>{1}*{2} and t._row<=({1}+1)*{2}";
        
            sql = string.Format(sql, OrderBy, pageIndex - 1, pageSize, sortType, sqlCon);

            //   sql += "  order by VipCount ";
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, sql, ls.ToArray());    //����������
            return ds;
        }


    }
}
