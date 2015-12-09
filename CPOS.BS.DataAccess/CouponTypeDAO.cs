/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
    /// ��CouponType�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class CouponTypeDAO : Base.BaseCPOSDAO, ICRUDable<CouponTypeEntity>, IQueryable<CouponTypeEntity>
    {
        #region ��ȡ�Ż݄����
        /// <summary>
        /// ��ȡ�Ż݄��б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetCouponType()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select convert(nvarchar(50),CouponTypeID),CoupontypeName from CouponType where IsDelete='0' and (CustomerID is null or CustomerID = '");
            strb.Append(CurrentUserInfo.ClientID);
            strb.Append("')");
            return this.SQLHelper.ExecuteDataset(strb.ToString());
        }
        #endregion

        #region ��ȡ�Ż�ȯ���
        public DataSet GetCouponTypeList()
        {
            //updated by Willie: CustomerId is null Ϊͨ������ on 2014-09-17
            string sql = @"SELECT 
                         C.CouponTypeID
                        , C.CouponTypeName
                        , SUM(c.[IssuedQty]) IssuedQty
                        ,SUM(CountTotal) CountTotal
                         FROM  CouponType c
                        LEFT JOIN PrizeCouponTypeMapping p ON CAST(c.CouponTypeID AS NVARCHAR(200)) = p.CouponTypeID 
                        LEFT JOIN dbo.LPrizes l ON l.PrizesID = p.PrizesID AND [PrizeTypeId] ='Coupon'  
                        where  C.IsDelete='0' and   C.CustomerId='" + this.CurrentUserInfo.ClientID + "' AND ((EndTime IS NULL AND ServiceLife IS NOT NULL) OR (EndTime IS NOT NULL AND EndTime >getdate())) GROUP BY c.CouponTypeID,c.CouponTypeName";
             return this.SQLHelper.ExecuteDataset(sql);

        }
        #endregion

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public Guid CreateReturnID(CouponTypeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [CouponType](");
            strSql.Append("[CouponTypeName],[ParValue],[Discount],[ConditionValue],[IsRepeatable],[IsMixable],[CouponSourceID],[ValidPeriod],[LastUpdateTime],[LastUpdateBy],[CreateTime],[CreateBy],[IsDelete],[CustomerId],[IssuedQty],[IsVoucher],[UsableRange],[ServiceLife],[SuitableForStore],[BeginTime],[EndTime],[CouponTypeDesc],[CouponTypeID])");
            strSql.Append(" values (");
            strSql.Append("@CouponTypeName,@ParValue,@Discount,@ConditionValue,@IsRepeatable,@IsMixable,@CouponSourceID,@ValidPeriod,@LastUpdateTime,@LastUpdateBy,@CreateTime,@CreateBy,@IsDelete,@CustomerId,@IssuedQty,@IsVoucher,@UsableRange,@ServiceLife,@SuitableForStore,@BeginTime,@EndTime,@CouponTypeDesc,@CouponTypeID)");

            Guid? pkGuid;
            if (pEntity.CouponTypeID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.CouponTypeID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@CouponTypeName",SqlDbType.NVarChar),
					new SqlParameter("@ParValue",SqlDbType.Decimal),
					new SqlParameter("@Discount",SqlDbType.Decimal),
					new SqlParameter("@ConditionValue",SqlDbType.Decimal),
					new SqlParameter("@IsRepeatable",SqlDbType.Int),
					new SqlParameter("@IsMixable",SqlDbType.Int),
					new SqlParameter("@CouponSourceID",SqlDbType.NVarChar),
					new SqlParameter("@ValidPeriod",SqlDbType.Int),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@IssuedQty",SqlDbType.Int),
					new SqlParameter("@IsVoucher",SqlDbType.Int),
					new SqlParameter("@UsableRange",SqlDbType.Int),
					new SqlParameter("@ServiceLife",SqlDbType.Int),
					new SqlParameter("@SuitableForStore",SqlDbType.Int),
					new SqlParameter("@BeginTime",SqlDbType.DateTime),
					new SqlParameter("@EndTime",SqlDbType.DateTime),
					new SqlParameter("@CouponTypeDesc",SqlDbType.NVarChar),
					new SqlParameter("@CouponTypeID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.CouponTypeName;
            parameters[1].Value = pEntity.ParValue;
            parameters[2].Value = pEntity.Discount;
            parameters[3].Value = pEntity.ConditionValue;
            parameters[4].Value = pEntity.IsRepeatable;
            parameters[5].Value = pEntity.IsMixable;
            parameters[6].Value = pEntity.CouponSourceID;
            parameters[7].Value = pEntity.ValidPeriod;
            parameters[8].Value = pEntity.LastUpdateTime;
            parameters[9].Value = pEntity.LastUpdateBy;
            parameters[10].Value = pEntity.CreateTime;
            parameters[11].Value = pEntity.CreateBy;
            parameters[12].Value = pEntity.IsDelete;
            parameters[13].Value = pEntity.CustomerId;
            parameters[14].Value = pEntity.IssuedQty;
            parameters[15].Value = pEntity.IsVoucher;
            parameters[16].Value = pEntity.UsableRange;
            parameters[17].Value = pEntity.ServiceLife;
            parameters[18].Value = pEntity.SuitableForStore;
            parameters[19].Value = pEntity.BeginTime;
            parameters[20].Value = pEntity.EndTime;
            parameters[21].Value = pEntity.CouponTypeDesc;
            parameters[22].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.CouponTypeID = pkGuid;
            return pkGuid.Value;
        }

        #region ��ȡ����Ż�ȯʹ�����
        /// <summary>
        /// ��ȡ��Ʒ��Ա�б�
        /// </summary>
        /// <param name="EventID"></param>
        /// <returns></returns>
        public DataSet GetCouponTypeCount()
        {
            string sql = "SELECT  c.CouponTypeID,SUM(c.[IssuedQty])[IssuedQty],SUM(CountTotal) CountTotal  "
                        + " FROM [dbo].[CouponType] C "
                        + " LEFT JOIN PrizeCouponTypeMapping  p ON CAST(C.CouponTypeID AS NVARCHAR(200))=p.CouponTypeID  "

                        + " INNER JOIN dbo.LPrizes l ON l.PrizesID=p.PrizesID  AND  [PrizeTypeId]=1   "
                        + " WHERE  c.IsDelete=0 "
                        + " GROUP BY c.CouponTypeID ";
            DataSet ds = new DataSet();
            return this.SQLHelper.ExecuteDataset(sql);
        }
        #endregion
    }
}
