/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/24 11:37:23
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
    /// ��vwOnlineOrderHeader�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VwOnlineOrderHeaderDAO : Base.BaseCPOSDAO, ICRUDable<VwOnlineOrderHeaderEntity>, IQueryable<VwOnlineOrderHeaderEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VwOnlineOrderHeaderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VwOnlineOrderHeaderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VwOnlineOrderHeaderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
         
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
     

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [vwOnlineOrderHeader](");
            strSql.Append("[OrderCode],[OrderDate],[StoreId],[TotalQty],[TotalAmount],[DisCountRate],[CarrierId],[CarrierName],[Mobile],[Email],[OpenId],[Remark],[STATUS],[StatusDesc],[PaymentTypeId],[DeliveryId],[DeliveryTime],[DeliveryAddress],[DeliveryCode],[UserName],[DeliveryName],[VipId],[ClinchTime],[ReceiptTime],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[OrderId])");
            strSql.Append(" values (");
            strSql.Append("@OrderCode,@OrderDate,@StoreId,@TotalQty,@TotalAmount,@DisCountRate,@CarrierId,@CarrierName,@Mobile,@Email,@OpenId,@Remark,@STATUS,@StatusDesc,@PaymentTypeId,@DeliveryId,@DeliveryTime,@DeliveryAddress,@DeliveryCode,@UserName,@DeliveryName,@VipId,@ClinchTime,@ReceiptTime,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@OrderId)");            

			string pkString = pEntity.OrderId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderCode",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.NVarChar),
					new SqlParameter("@StoreId",SqlDbType.NVarChar),
					new SqlParameter("@TotalQty",SqlDbType.Decimal),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DisCountRate",SqlDbType.Decimal),
					new SqlParameter("@CarrierId",SqlDbType.NVarChar),
					new SqlParameter("@CarrierName",SqlDbType.NVarChar),
					new SqlParameter("@Mobile",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@STATUS",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTypeId",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryId",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryTime",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryAddress",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryCode",SqlDbType.NVarChar),
					new SqlParameter("@UserName",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryName",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@ClinchTime",SqlDbType.NVarChar),
					new SqlParameter("@ReceiptTime",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.VarChar),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.VarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderCode;
			parameters[1].Value = pEntity.OrderDate;
			parameters[2].Value = pEntity.StoreId;
			parameters[3].Value = pEntity.TotalQty;
			parameters[4].Value = pEntity.TotalAmount;
			parameters[5].Value = pEntity.DisCountRate;
			parameters[6].Value = pEntity.CarrierId;
			parameters[7].Value = pEntity.CarrierName;
			parameters[8].Value = pEntity.Mobile;
			parameters[9].Value = pEntity.Email;
			parameters[10].Value = pEntity.OpenId;
			parameters[11].Value = pEntity.Remark;
			parameters[12].Value = pEntity.STATUS;
			parameters[13].Value = pEntity.StatusDesc;
			parameters[14].Value = pEntity.PaymentTypeId;
			parameters[15].Value = pEntity.DeliveryId;
			parameters[16].Value = pEntity.DeliveryTime;
			parameters[17].Value = pEntity.DeliveryAddress;
			parameters[18].Value = pEntity.DeliveryCode;
			parameters[19].Value = pEntity.UserName;
			parameters[20].Value = pEntity.DeliveryName;
			parameters[21].Value = pEntity.VipId;
			parameters[22].Value = pEntity.ClinchTime;
			parameters[23].Value = pEntity.ReceiptTime;
			parameters[24].Value = pEntity.CreateTime;
			parameters[25].Value = pEntity.CreateBy;
			parameters[26].Value = pEntity.LastUpdateTime;
			parameters[27].Value = pEntity.LastUpdateBy;
			parameters[28].Value = pEntity.IsDelete;
			parameters[29].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VwOnlineOrderHeaderEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwOnlineOrderHeader] where OrderId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            VwOnlineOrderHeaderEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

        /// <summary>
        /// ��ȡ����ʵ��
        /// </summary>
        /// <returns></returns>
        public VwOnlineOrderHeaderEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwOnlineOrderHeader] where isdelete=0");
            //��ȡ����
            List<VwOnlineOrderHeaderEntity> list = new List<VwOnlineOrderHeaderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwOnlineOrderHeaderEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list.ToArray();
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VwOnlineOrderHeaderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(VwOnlineOrderHeaderEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [vwOnlineOrderHeader] set ");
            if (pIsUpdateNullField || pEntity.OrderCode!=null)
                strSql.Append( "[OrderCode]=@OrderCode,");
            if (pIsUpdateNullField || pEntity.OrderDate!=null)
                strSql.Append( "[OrderDate]=@OrderDate,");
            if (pIsUpdateNullField || pEntity.StoreId!=null)
                strSql.Append( "[StoreId]=@StoreId,");
            if (pIsUpdateNullField || pEntity.TotalQty!=null)
                strSql.Append( "[TotalQty]=@TotalQty,");
            if (pIsUpdateNullField || pEntity.TotalAmount!=null)
                strSql.Append( "[TotalAmount]=@TotalAmount,");
            if (pIsUpdateNullField || pEntity.DisCountRate!=null)
                strSql.Append( "[DisCountRate]=@DisCountRate,");
            if (pIsUpdateNullField || pEntity.CarrierId!=null)
                strSql.Append( "[CarrierId]=@CarrierId,");
            if (pIsUpdateNullField || pEntity.CarrierName!=null)
                strSql.Append( "[CarrierName]=@CarrierName,");
            if (pIsUpdateNullField || pEntity.Mobile!=null)
                strSql.Append( "[Mobile]=@Mobile,");
            if (pIsUpdateNullField || pEntity.Email!=null)
                strSql.Append( "[Email]=@Email,");
            if (pIsUpdateNullField || pEntity.OpenId!=null)
                strSql.Append( "[OpenId]=@OpenId,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.STATUS!=null)
                strSql.Append( "[STATUS]=@STATUS,");
            if (pIsUpdateNullField || pEntity.StatusDesc!=null)
                strSql.Append( "[StatusDesc]=@StatusDesc,");
            if (pIsUpdateNullField || pEntity.PaymentTypeId!=null)
                strSql.Append( "[PaymentTypeId]=@PaymentTypeId,");
            if (pIsUpdateNullField || pEntity.DeliveryId!=null)
                strSql.Append( "[DeliveryId]=@DeliveryId,");
            if (pIsUpdateNullField || pEntity.DeliveryTime!=null)
                strSql.Append( "[DeliveryTime]=@DeliveryTime,");
            if (pIsUpdateNullField || pEntity.DeliveryAddress!=null)
                strSql.Append( "[DeliveryAddress]=@DeliveryAddress,");
            if (pIsUpdateNullField || pEntity.DeliveryCode!=null)
                strSql.Append( "[DeliveryCode]=@DeliveryCode,");
            if (pIsUpdateNullField || pEntity.UserName!=null)
                strSql.Append( "[UserName]=@UserName,");
            if (pIsUpdateNullField || pEntity.DeliveryName!=null)
                strSql.Append( "[DeliveryName]=@DeliveryName,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.ClinchTime!=null)
                strSql.Append( "[ClinchTime]=@ClinchTime,");
            if (pIsUpdateNullField || pEntity.ReceiptTime!=null)
                strSql.Append( "[ReceiptTime]=@ReceiptTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderId=@OrderId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderCode",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.NVarChar),
					new SqlParameter("@StoreId",SqlDbType.NVarChar),
					new SqlParameter("@TotalQty",SqlDbType.Decimal),
					new SqlParameter("@TotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DisCountRate",SqlDbType.Decimal),
					new SqlParameter("@CarrierId",SqlDbType.NVarChar),
					new SqlParameter("@CarrierName",SqlDbType.NVarChar),
					new SqlParameter("@Mobile",SqlDbType.NVarChar),
					new SqlParameter("@Email",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@STATUS",SqlDbType.NVarChar),
					new SqlParameter("@StatusDesc",SqlDbType.NVarChar),
					new SqlParameter("@PaymentTypeId",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryId",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryTime",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryAddress",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryCode",SqlDbType.NVarChar),
					new SqlParameter("@UserName",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryName",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@ClinchTime",SqlDbType.NVarChar),
					new SqlParameter("@ReceiptTime",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@OrderId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderCode;
			parameters[1].Value = pEntity.OrderDate;
			parameters[2].Value = pEntity.StoreId;
			parameters[3].Value = pEntity.TotalQty;
			parameters[4].Value = pEntity.TotalAmount;
			parameters[5].Value = pEntity.DisCountRate;
			parameters[6].Value = pEntity.CarrierId;
			parameters[7].Value = pEntity.CarrierName;
			parameters[8].Value = pEntity.Mobile;
			parameters[9].Value = pEntity.Email;
			parameters[10].Value = pEntity.OpenId;
			parameters[11].Value = pEntity.Remark;
			parameters[12].Value = pEntity.STATUS;
			parameters[13].Value = pEntity.StatusDesc;
			parameters[14].Value = pEntity.PaymentTypeId;
			parameters[15].Value = pEntity.DeliveryId;
			parameters[16].Value = pEntity.DeliveryTime;
			parameters[17].Value = pEntity.DeliveryAddress;
			parameters[18].Value = pEntity.DeliveryCode;
			parameters[19].Value = pEntity.UserName;
			parameters[20].Value = pEntity.DeliveryName;
			parameters[21].Value = pEntity.VipId;
			parameters[22].Value = pEntity.ClinchTime;
			parameters[23].Value = pEntity.ReceiptTime;
			parameters[24].Value = pEntity.LastUpdateTime;
			parameters[25].Value = pEntity.LastUpdateBy;
			parameters[26].Value = pEntity.OrderId;

            //ִ�����
            int result = 0;
            if (pTran != null)
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Update(VwOnlineOrderHeaderEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(VwOnlineOrderHeaderEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VwOnlineOrderHeaderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VwOnlineOrderHeaderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderId, pTran);           
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object pID, IDbTransaction pTran)
        {
            if (pID == null)
                return ;   
            //��֯������SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [vwOnlineOrderHeader] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderId=@OrderId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderId",SqlDbType=SqlDbType.VarChar,Value=pID}
            };
            //ִ�����
            int result = 0;
            if (pTran != null)
                result=this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), parameters);
            else
                result=this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), parameters);
            return ;
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VwOnlineOrderHeaderEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.OrderId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.OrderId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VwOnlineOrderHeaderEntity[] pEntities)
        { 
            Delete(pEntities, null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        public void Delete(object[] pIDs)
        {
            Delete(pIDs,null);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pIDs">��ʶ��ֵ����</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(object[] pIDs, IDbTransaction pTran) 
        {
            if (pIDs == null || pIDs.Length==0)
                return ;
            //��֯������SQL
            StringBuilder primaryKeys = new StringBuilder();
            foreach (object item in pIDs)
            {
                primaryKeys.AppendFormat("'{0}',",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [vwOnlineOrderHeader] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where OrderId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
            //ִ�����
            int result = 0;   
            if (pTran == null)
                result = this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql.ToString(), null);
            else
                result = this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran,CommandType.Text, sql.ToString());       
        }
        #endregion

        #region IQueryable ��Ա
        /// <summary>
        /// ִ�в�ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public VwOnlineOrderHeaderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [vwOnlineOrderHeader] where isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //ִ��SQL
            List<VwOnlineOrderHeaderEntity> list = new List<VwOnlineOrderHeaderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VwOnlineOrderHeaderEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        /// <summary>
        /// ִ�з�ҳ��ѯ
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <param name="pPageSize">ÿҳ�ļ�¼��</param>
        /// <param name="pCurrentPageIndex">��0��ʼ�ĵ�ǰҳ��</param>
        /// <returns></returns>
        public PagedQueryResult<VwOnlineOrderHeaderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            //��֯SQL
            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //��ҳSQL
            pagedSql.AppendFormat("select * from (select row_number()over( order by ");
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                foreach (var item in pOrderBys)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" {0} {1},", StringUtils.WrapperSQLServerObject(item.FieldName), item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                    }
                }
                pagedSql.Remove(pagedSql.Length - 1, 1);
            }
            else
            {
                pagedSql.AppendFormat(" [OrderId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VwOnlineOrderHeader] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VwOnlineOrderHeader] where isdelete=0 ");
            //��������
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    if(item!=null)
                    {
                        pagedSql.AppendFormat(" and {0}", item.GetExpression());
                        totalCountSql.AppendFormat(" and {0}", item.GetExpression());
                    }
                }
            }
            pagedSql.AppendFormat(") as A ");
            //ȡָ��ҳ������
            pagedSql.AppendFormat(" where ___rn >{0} and ___rn <={1}", pPageSize * (pCurrentPageIndex-1), pPageSize * (pCurrentPageIndex));
            //ִ����䲢���ؽ��
            PagedQueryResult<VwOnlineOrderHeaderEntity> result = new PagedQueryResult<VwOnlineOrderHeaderEntity>();
            List<VwOnlineOrderHeaderEntity> list = new List<VwOnlineOrderHeaderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VwOnlineOrderHeaderEntity m;
                    this.Load(rdr, out m);
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
            return result;
        }

        /// <summary>
        /// ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public VwOnlineOrderHeaderEntity[] QueryByEntity(VwOnlineOrderHeaderEntity pQueryEntity, OrderBy[] pOrderBys)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity(pQueryEntity);
            return Query(queryWhereCondition,  pOrderBys);            
        }

        /// <summary>
        /// ��ҳ����ʵ��������ѯʵ��
        /// </summary>
        /// <param name="pQueryEntity">��ʵ����ʽ����Ĳ���</param>
        /// <param name="pOrderBys">�������</param>
        /// <returns>����������ʵ�弯</returns>
        public PagedQueryResult<VwOnlineOrderHeaderEntity> PagedQueryByEntity(VwOnlineOrderHeaderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
        {
            IWhereCondition[] queryWhereCondition = GetWhereConditionByEntity( pQueryEntity);
            return PagedQuery(queryWhereCondition, pOrderBys, pPageSize, pCurrentPageIndex);
        }

        #endregion

        #region ���߷���
        /// <summary>
        /// ����ʵ���Null�������ɲ�ѯ������
        /// </summary>
        /// <returns></returns>
        protected IWhereCondition[] GetWhereConditionByEntity(VwOnlineOrderHeaderEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.OrderCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderCode", Value = pQueryEntity.OrderCode });
            if (pQueryEntity.OrderDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDate", Value = pQueryEntity.OrderDate });
            if (pQueryEntity.StoreId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreId", Value = pQueryEntity.StoreId });
            if (pQueryEntity.TotalQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalQty", Value = pQueryEntity.TotalQty });
            if (pQueryEntity.TotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAmount", Value = pQueryEntity.TotalAmount });
            if (pQueryEntity.DisCountRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisCountRate", Value = pQueryEntity.DisCountRate });
            if (pQueryEntity.CarrierId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CarrierId", Value = pQueryEntity.CarrierId });
            if (pQueryEntity.CarrierName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CarrierName", Value = pQueryEntity.CarrierName });
            if (pQueryEntity.Mobile!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Mobile", Value = pQueryEntity.Mobile });
            if (pQueryEntity.Email!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Email", Value = pQueryEntity.Email });
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.STATUS!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "STATUS", Value = pQueryEntity.STATUS });
            if (pQueryEntity.StatusDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StatusDesc", Value = pQueryEntity.StatusDesc });
            if (pQueryEntity.PaymentTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PaymentTypeId", Value = pQueryEntity.PaymentTypeId });
            if (pQueryEntity.DeliveryId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryId", Value = pQueryEntity.DeliveryId });
            if (pQueryEntity.DeliveryTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryTime", Value = pQueryEntity.DeliveryTime });
            if (pQueryEntity.DeliveryAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryAddress", Value = pQueryEntity.DeliveryAddress });
            if (pQueryEntity.DeliveryCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryCode", Value = pQueryEntity.DeliveryCode });
            if (pQueryEntity.UserName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserName", Value = pQueryEntity.UserName });
            if (pQueryEntity.DeliveryName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryName", Value = pQueryEntity.DeliveryName });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.ClinchTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClinchTime", Value = pQueryEntity.ClinchTime });
            if (pQueryEntity.ReceiptTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReceiptTime", Value = pQueryEntity.ReceiptTime });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out VwOnlineOrderHeaderEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VwOnlineOrderHeaderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["OrderCode"] != DBNull.Value)
			{
				pInstance.OrderCode =  Convert.ToString(pReader["OrderCode"]);
			}
			if (pReader["OrderDate"] != DBNull.Value)
			{
				pInstance.OrderDate =  Convert.ToString(pReader["OrderDate"]);
			}
			if (pReader["StoreId"] != DBNull.Value)
			{
				pInstance.StoreId =  Convert.ToString(pReader["StoreId"]);
			}
			if (pReader["TotalQty"] != DBNull.Value)
			{
				pInstance.TotalQty =  Convert.ToDecimal(pReader["TotalQty"]);
			}
			if (pReader["TotalAmount"] != DBNull.Value)
			{
				pInstance.TotalAmount =  Convert.ToDecimal(pReader["TotalAmount"]);
			}
			if (pReader["DisCountRate"] != DBNull.Value)
			{
				pInstance.DisCountRate =  Convert.ToDecimal(pReader["DisCountRate"]);
			}
			if (pReader["CarrierId"] != DBNull.Value)
			{
				pInstance.CarrierId =  Convert.ToString(pReader["CarrierId"]);
			}
			if (pReader["CarrierName"] != DBNull.Value)
			{
				pInstance.CarrierName =  Convert.ToString(pReader["CarrierName"]);
			}
			if (pReader["Mobile"] != DBNull.Value)
			{
				pInstance.Mobile =  Convert.ToString(pReader["Mobile"]);
			}
			if (pReader["Email"] != DBNull.Value)
			{
				pInstance.Email =  Convert.ToString(pReader["Email"]);
			}
			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["STATUS"] != DBNull.Value)
			{
				pInstance.STATUS =  Convert.ToString(pReader["STATUS"]);
			}
			if (pReader["StatusDesc"] != DBNull.Value)
			{
				pInstance.StatusDesc =  Convert.ToString(pReader["StatusDesc"]);
			}
			if (pReader["PaymentTypeId"] != DBNull.Value)
			{
				pInstance.PaymentTypeId =  Convert.ToString(pReader["PaymentTypeId"]);
			}
			if (pReader["DeliveryId"] != DBNull.Value)
			{
				pInstance.DeliveryId =  Convert.ToString(pReader["DeliveryId"]);
			}
			if (pReader["DeliveryTime"] != DBNull.Value)
			{
				pInstance.DeliveryTime =  Convert.ToString(pReader["DeliveryTime"]);
			}
			if (pReader["DeliveryAddress"] != DBNull.Value)
			{
				pInstance.DeliveryAddress =  Convert.ToString(pReader["DeliveryAddress"]);
			}
			if (pReader["DeliveryCode"] != DBNull.Value)
			{
				pInstance.DeliveryCode =  Convert.ToString(pReader["DeliveryCode"]);
			}
			if (pReader["UserName"] != DBNull.Value)
			{
				pInstance.UserName =  Convert.ToString(pReader["UserName"]);
			}
			if (pReader["DeliveryName"] != DBNull.Value)
			{
				pInstance.DeliveryName =  Convert.ToString(pReader["DeliveryName"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["ClinchTime"] != DBNull.Value)
			{
				pInstance.ClinchTime =  Convert.ToString(pReader["ClinchTime"]);
			}
			if (pReader["ReceiptTime"] != DBNull.Value)
			{
				pInstance.ReceiptTime =  Convert.ToString(pReader["ReceiptTime"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToString(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToString(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =  Convert.ToString(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
