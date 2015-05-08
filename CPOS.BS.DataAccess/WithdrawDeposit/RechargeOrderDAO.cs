/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-4-16 17:36:32
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
    /// ��RechargeOrder�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class RechargeOrderDAO : Base.BaseCPOSDAO, ICRUDable<RechargeOrderEntity>, IQueryable<RechargeOrderEntity>
    {
        /// <summary>
        /// �������󣬷���ID
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public Guid CreateReturnID(RechargeOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [RechargeOrder](");
            strSql.Append("[OrderNo],[OrderDesc],[VipID],[TotalAmount],[ActuallyPaid],[ReturnAmount],[PayPoints],[ReceivePoints],[PayerID],[PayID],[Status],[CustomerID],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[OrderID])");
            strSql.Append(" values (");
            strSql.Append("@OrderNo,@OrderDesc,@VipID,@TotalAmount,@ActuallyPaid,@ReturnAmount,@PayPoints,@ReceivePoints,@PayerID,@PayID,@Status,@CustomerID,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@OrderID)");

            Guid? pkGuid;
            if (pEntity.OrderID == null)
                pkGuid = Guid.NewGuid();
            else
                pkGuid = pEntity.OrderID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderNo",SqlDbType.VarChar),
					new SqlParameter("@OrderDesc",SqlDbType.NVarChar),
					new SqlParameter("@VipID",SqlDbType.VarChar),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@ActuallyPaid",SqlDbType.Decimal),
					new SqlParameter("@ReturnAmount",SqlDbType.Decimal),
					new SqlParameter("@PayPoints",SqlDbType.Decimal),
					new SqlParameter("@ReceivePoints",SqlDbType.Decimal),
					new SqlParameter("@PayerID",SqlDbType.VarChar),
					new SqlParameter("@PayID",SqlDbType.VarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@OrderID",SqlDbType.UniqueIdentifier)
            };
            parameters[0].Value = pEntity.OrderNo;
            parameters[1].Value = pEntity.OrderDesc;
            parameters[2].Value = pEntity.VipID;
            parameters[3].Value = pEntity.TotalAmount;
            parameters[4].Value = pEntity.ActuallyPaid;
            parameters[5].Value = pEntity.ReturnAmount;
            parameters[6].Value = pEntity.PayPoints;
            parameters[7].Value = pEntity.ReceivePoints;
            parameters[8].Value = pEntity.PayerID;
            parameters[9].Value = pEntity.PayID;
            parameters[10].Value = pEntity.Status;
            parameters[11].Value = pEntity.CustomerID;
            parameters[12].Value = pEntity.CreateTime;
            parameters[13].Value = pEntity.CreateBy;
            parameters[14].Value = pEntity.LastUpdateTime;
            parameters[15].Value = pEntity.LastUpdateBy;
            parameters[16].Value = pEntity.IsDelete;
            parameters[17].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            pEntity.OrderID = pkGuid;
            return pkGuid.Value;
        }
    }
}
