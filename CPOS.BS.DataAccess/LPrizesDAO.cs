/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 14:52:46
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��LPrizes�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LPrizesDAO : Base.BaseCPOSDAO, ICRUDable<LPrizesEntity>, IQueryable<LPrizesEntity>
    {
        #region ���Ʒ�б�
        /// <summary>
        /// ���ݱ�ʶ��ȡ��Ʒ����
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int GetEventPrizesCount(string EventID)
        {
            string sql = GetEventPrizesSql(EventID);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ���ݱ�ʶ��ȡ��Ʒ��Ϣ
        /// </summary>
        public DataSet GetEventPrizesList(string EventID, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventPrizesSql(EventID);
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndexLast between '" + beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetEventPrizesSql(string EventID)
        {
            BasicUserInfo pUserInfo = new BasicUserInfo();

            string sql = "";
            sql += "SELECT a.* "
                + " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex ) "
                + " into #tmp FROM LPrizes a "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            //sql += " order by a.DisplayIndex ";
            return sql;
        }
        #endregion

        #region
        /// <summary>
        /// ����Ʒ�Ʒ��飬��ȡ��Ϣ
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetLPrizesGroupBrand(string EventID,string RoundId)
        {
            string sql = "SELECT DISTINCT a.PrizeName,a.LogoURL,a.PrizeShortDesc,a.DisplayIndex,a.PrizeDesc "
                        + " FROM dbo.LPrizes a inner join LEventRountPrizesMapping b on(a.PrizesID = b.PrizesID) "
                        + " inner join LEventRound c on(b.RoundId = c.RoundId and a.EventId = c.EventId) "
                        + " WHERE 1=1 "
                        + " and a.EventID = '" + EventID + "' "
                        + " AND a.IsDelete = '0' "
                        + " and b.IsDelete = '0' "
                        + " and c.IsDelete = '0' "
                        + " and c.RoundId = '" + RoundId + "' order by a.DisplayIndex  ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region �ҵ��н�����
        /// <summary>
        /// �ҵ��н�����
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetEventPrizesByVipId(string EventId, string VipId)
        {
            string sql = "select a.* From LPrizes a "
                        + " inner join LPrizeWinner b on(a.PrizesID = b.PrizeID) "
                        + " where a.IsDelete = '0' "
                        + " and b.IsDelete = '0' "
                        + " and b.VipID = '" + VipId + "' "
                        + " and a.EventId = case when '" + EventId + "' = ''  then a.EventId else '" + EventId + "' end "
                        + " order by CreateTime desc ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region ��ȡ��Ʒ�б�
        /// <summary>
        /// ��ȡ��Ʒ�б�
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetPrizesByEventId(string EventId)
        {
            string sql = "select top 1 a.*,p.CouponTypeID From LPrizes a with(nolock)"
                        + "LEFT JOIN dbo.PrizeCouponTypeMapping p WITH(NOLOCK)  ON a.PrizesID=p.PrizesID "
                        + " where a.IsDelete = '0' "
                        + " and a.EventId = '" + EventId + "' "
                        + " order by a.displayIndex desc ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region ��ȡ��Ʒ��Ա�б�
        /// <summary>
        /// ��ȡ��Ʒ��Ա�б�
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetPrizeWinnerByPrizeId(string PrizeId)
        {
            string sql = "select a.* From LPrizeWinner a "
                        + " where a.IsDelete = '0' "
                        + " and a.PrizeId = '" + PrizeId + "' "
                        + " order by a.CreateTime asc ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion

        #region ��ִν�Ʒ�б�
        /// <summary>
        /// ���ݱ�ʶ��ȡ��Ʒ����
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public int GetEventRoundPrizesCount(string EventID, string RoundId)
        {
            string sql = GetEventRoundPrizesSql(EventID, RoundId);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ���ݱ�ʶ��ȡ��Ʒ��Ϣ
        /// </summary>
        public DataSet GetEventRoundPrizesList(string EventID, string RoundId, int Page, int PageSize)
        {
            int beginSize = Page * PageSize + 1;
            int endSize = Page * PageSize + PageSize;
            DataSet ds = new DataSet();
            string sql = GetEventRoundPrizesSql(EventID, RoundId);
            sql = sql + "select * From #tmp a where 1=1 and a.DisplayIndexLast between '" + beginSize + "' and '" + endSize + "' order by  a.DisplayIndexLast ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetEventRoundPrizesSql(string EventID, string RoundId)
        {
            BasicUserInfo pUserInfo = new BasicUserInfo();

            string sql = "";
            sql += " select a.*  ";
            sql += " ,DisplayIndexLast = row_number() over(order by a.DisplayIndex )  ";
            sql += " into #tmp from ( ";
            sql += " SELECT distinct a.PrizesID, a.PrizeName "
                + " ,a.DisplayIndex "
                + " ,(select c.PrizesCount from LEventRountPrizesMapping c where c.isDelete='0' and a.PrizesID=c.PrizesID and c.roundId=b.roundId and b.roundId='" + RoundId + "') PrizesCount "
                + " ,(select c.WinnerCount from LEventRountPrizesMapping c where c.isDelete='0' and a.PrizesID=c.PrizesID and c.roundId=b.roundId and b.roundId='" + RoundId + "') WinnerCount "
                + "  FROM LPrizes a "
                + " left join LEventRound b on (a.EventId=b.EventId and b.isDelete='0' ) "
                + " where a.IsDelete='0' and a.EventID = '" + EventID + "' ";
            if (RoundId != null && RoundId.Length > 0)
            {
                sql += " and b.RoundId='" + RoundId + "' ";
            }
            sql += " ) a ";
            return sql;
        }
        #endregion

        #region ���潱Ʒ
        public int SavePrize(LPrizesEntity entity)
        {
            
             SqlParameter[] parameters = new SqlParameter[11] 
            { 
                new SqlParameter{ParameterName="@EventId",SqlDbType=SqlDbType.NVarChar,Value=entity.EventId},
                new SqlParameter{ParameterName="@PrizesID",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizesID},
                new SqlParameter{ParameterName="@PrizeLevel",SqlDbType=SqlDbType.Int,Value=entity.PrizeLevel},
                new SqlParameter{ParameterName="@PrizeTypeId",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizeTypeId},
                new SqlParameter{ParameterName="@Point",SqlDbType=SqlDbType.Int,Value=entity.Point},
                new SqlParameter{ParameterName="@CouponTypeID",SqlDbType=SqlDbType.NVarChar,Value=entity.CouponTypeID},
                new SqlParameter{ParameterName="@PrizeName",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizeName},
                new SqlParameter{ParameterName="@CountTotal",SqlDbType=SqlDbType.Int,Value=entity.CountTotal},
                new SqlParameter{ParameterName="@CustomerId",SqlDbType=SqlDbType.NVarChar,Value=this.CurrentUserInfo.ClientID},
                //new SqlParameter{ParameterName="@Probability",SqlDbType=SqlDbType.Decimal,Value=entity.Probability},
                new SqlParameter{ParameterName="@ImageUrl",SqlDbType=SqlDbType.NVarChar,Value=entity.ImageUrl},
                new SqlParameter{ParameterName="@Creator",SqlDbType=SqlDbType.NVarChar,Value=entity.CreateBy}
            };
             return this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_AddPrizes", parameters);
        }
        #endregion
        #region ׷�ӽ�Ʒ
        public int AppendPrize(LPrizesEntity entity)
        {
            
             SqlParameter[] parameters = new SqlParameter[6] 
            { 
                new SqlParameter{ParameterName="@CustomerId",SqlDbType=SqlDbType.NVarChar,Value=this.CurrentUserInfo.ClientID},
                new SqlParameter{ParameterName="@EventId",SqlDbType=SqlDbType.NVarChar,Value=entity.EventId},
                new SqlParameter{ParameterName="@PrizeId",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizesID},
                new SqlParameter{ParameterName="@PrizeType",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizeTypeId},
                new SqlParameter{ParameterName="@Qty",SqlDbType=SqlDbType.Int,Value=entity.CountTotal},      
                new SqlParameter{ParameterName="@UpdateBy",SqlDbType=SqlDbType.NVarChar,Value=entity.LastUpdateBy}
            };
             return this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_AppendPrizes", parameters);
        }
        #endregion
        #region ɾ����Ʒ
        public int DeletePrize(LPrizesEntity entity)
        {

            SqlParameter[] parameters = new SqlParameter[3] 
            { 
                new SqlParameter{ParameterName="@EventId",SqlDbType=SqlDbType.NVarChar,Value=entity.EventId},
                new SqlParameter{ParameterName="@PrizesID",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizesID},
                new SqlParameter{ParameterName="@UpdateBy",SqlDbType=SqlDbType.NVarChar,Value=entity.LastUpdateBy}
            };
            return this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_DeletePrizes", parameters);
        }
        #endregion
        #region ��Ʒ�б�

        public DataSet GetPirzeList(string strEventId)
        {
            string sql = "select l.PrizesID ,l.EventId ,l.PrizeName,l.PrizeLevel,o.OptionText PrizeLevelName,l.CountTotal,l.CountTotal PrizeCount ,l.Probability ,c.CouponTypeName,c.CouponTypeID ,c.IssuedQty,l.ImageUrl,pool.RemainCount,l.PrizeTypeId ,l.Point"
                        +"From dbo.LPrizes l "
                        +"LEFT JOIN PrizeCouponTypeMapping p ON l.PrizesID = p.PrizesID "
                        +"LEFT JOIN CouponType c ON p.CouponTypeID = CAST(c.CouponTypeID AS NVARCHAR(200)) "
                        + "LEFT JOIN Options o ON l.PrizeLevel=o.OptionValue "
                        + "LEFT JOIN (SELECT PrizeID,COUNT(1) RemainCount FROM dbo.LPrizePools WITH(NOLOCK) WHERE Status=1 and EventId = '" + strEventId + "'  GROUP BY PrizeID) pool ON pool.PrizeID=l.PrizesID "
                        + " where l.IsDelete = '0'  AND o.OptionName='PrizeGrade'"
                        + " and l.EventId = '" + strEventId + "' "
                        + " order by l.CreateTime asc ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        public DataSet GetPirzeListForCTW(string strEventId)
        {
            string sql = "select l.PrizesID ,l.EventId ,l.PrizeName,l.PrizeLevel,o.OptionText PrizeLevelName,l.CountTotal,l.CountTotal PrizeCount ,l.Probability ,c.CouponTypeName,c.CouponTypeID ,c.IssuedQty,l.ImageUrl,pool.RemainCount,l.PrizeTypeId "
                        + "From dbo.LPrizes l "
                        + "LEFT JOIN PrizeCouponTypeMapping p ON l.PrizesID = p.PrizesID "
                        + "LEFT JOIN CouponType c ON p.CouponTypeID = CAST(c.CouponTypeID AS NVARCHAR(200)) "
                        + "LEFT JOIN Options o ON l.PrizeLevel=o.OptionValue "
                        + "LEFT JOIN (SELECT PrizeID,COUNT(1) RemainCount FROM dbo.LPrizePools WITH(NOLOCK) WHERE Status=1 and EventId = '" + strEventId + "'  GROUP BY PrizeID) pool ON pool.PrizeID=l.PrizesID "
                        + " where l.IsDelete = '0' "
                        + " and l.EventId = '" + strEventId + "' "
                        + " order by l.CreateTime asc ";
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion
        public DataSet GetCouponTypeIDByPrizeId(string strPrizesID)
        {
            string strSql = string.Format("SELECT top 1 l.*,CouponTypeID,Location FROM LPrizes l WITH(NOLOCK) LEFT JOIN dbo.PrizeCouponTypeMapping p WITH(NOLOCK)  ON l.PrizesID=p.PrizesID LEFT JOIN dbo.LPrizeLocation lc WITH(NOLOCK) ON l.PrizesID=lc.PrizeID where l.PrizesID='{0}' ", strPrizesID);
            return this.SQLHelper.ExecuteDataset(strSql);

        }
        /// <summary>
        /// ���ݻid����δ�н���λ��
        /// </summary>
        /// <param name="strEventID"></param>
        /// <returns></returns>
        public int GetLocationByEventID(string strEventID)
        {
            string strSql = string.Format("SELECT top 1 Location FROM dbo.LPrizeLocation lc WITH(NOLOCK) where EventID='{0}' AND PrizeID='' order by newid()", strEventID);
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql) == null ? "0" : this.SQLHelper.ExecuteScalar(strSql).ToString());

        }
 
    }
}
