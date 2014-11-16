/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/13 18:00:57
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
    /// ��WXHouseBuy�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseBuyDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseBuyEntity>, IQueryable<WXHouseBuyEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseBuyDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXHouseBuyEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXHouseBuyEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
            pEntity.CreateTime = DateTime.Now;
            pEntity.CreateBy = CurrentUserInfo.UserID;
            pEntity.LastUpdateTime = pEntity.CreateTime;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;
            pEntity.IsDelete = 0;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [WXHouseBuy](");
            strSql.Append("[MappingID],[DeductPayment],[DeductPay],[ProfitPay],[RealPay],[TotalPay],[OrderDate],[OrderNo],[PayDate],[BillNo],[State],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[BuyingID])");
            strSql.Append(" values (");
            strSql.Append("@MappingID,@DeductPayment,@DeductPay,@ProfitPay,@RealPay,@TotalPay,@OrderDate,@OrderNo,@PayDate,@BillNo,@State,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@BuyingID)");            

			Guid? pkGuid;
			if (pEntity.BuyingID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.BuyingID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@MappingID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@DeductPayment",SqlDbType.Decimal),
					new SqlParameter("@DeductPay",SqlDbType.Decimal),
					new SqlParameter("@ProfitPay",SqlDbType.Decimal),
					new SqlParameter("@RealPay",SqlDbType.Decimal),
					new SqlParameter("@TotalPay",SqlDbType.Decimal),
					new SqlParameter("@OrderDate",SqlDbType.DateTime),
					new SqlParameter("@OrderNo",SqlDbType.DateTime),
					new SqlParameter("@PayDate",SqlDbType.DateTime),
					new SqlParameter("@BillNo",SqlDbType.NVarChar),
					new SqlParameter("@State",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@BuyingID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.MappingID;
			parameters[1].Value = pEntity.DeductPayment;
			parameters[2].Value = pEntity.DeductPay;
			parameters[3].Value = pEntity.ProfitPay;
			parameters[4].Value = pEntity.RealPay;
			parameters[5].Value = pEntity.TotalPay;
			parameters[6].Value = pEntity.OrderDate;
			parameters[7].Value = pEntity.OrderNo;
			parameters[8].Value = pEntity.PayDate;
			parameters[9].Value = pEntity.BillNo;
			parameters[10].Value = pEntity.State;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.CreateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.BuyingID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseBuyEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseBuy] where BuyingID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WXHouseBuyEntity m = null;
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
        public WXHouseBuyEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseBuy] where isdelete=0");
            //��ȡ����
            List<WXHouseBuyEntity> list = new List<WXHouseBuyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseBuyEntity m;
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
        public void Update(WXHouseBuyEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WXHouseBuyEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.BuyingID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXHouseBuy] set ");
            if (pIsUpdateNullField || pEntity.MappingID!=null)
                strSql.Append( "[MappingID]=@MappingID,");
            if (pIsUpdateNullField || pEntity.DeductPayment!=null)
                strSql.Append( "[DeductPayment]=@DeductPayment,");
            if (pIsUpdateNullField || pEntity.DeductPay!=null)
                strSql.Append( "[DeductPay]=@DeductPay,");
            if (pIsUpdateNullField || pEntity.ProfitPay!=null)
                strSql.Append( "[ProfitPay]=@ProfitPay,");
            if (pIsUpdateNullField || pEntity.RealPay!=null)
                strSql.Append( "[RealPay]=@RealPay,");
            if (pIsUpdateNullField || pEntity.TotalPay!=null)
                strSql.Append( "[TotalPay]=@TotalPay,");
            if (pIsUpdateNullField || pEntity.OrderDate!=null)
                strSql.Append( "[OrderDate]=@OrderDate,");
            if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.PayDate!=null)
                strSql.Append( "[PayDate]=@PayDate,");
            if (pIsUpdateNullField || pEntity.BillNo!=null)
                strSql.Append( "[BillNo]=@BillNo,");
            if (pIsUpdateNullField || pEntity.State!=null)
                strSql.Append( "[State]=@State,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where BuyingID=@BuyingID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@MappingID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@DeductPayment",SqlDbType.Decimal),
					new SqlParameter("@DeductPay",SqlDbType.Decimal),
					new SqlParameter("@ProfitPay",SqlDbType.Decimal),
					new SqlParameter("@RealPay",SqlDbType.Decimal),
					new SqlParameter("@TotalPay",SqlDbType.Decimal),
					new SqlParameter("@OrderDate",SqlDbType.DateTime),
					new SqlParameter("@OrderNo",SqlDbType.DateTime),
					new SqlParameter("@PayDate",SqlDbType.DateTime),
					new SqlParameter("@BillNo",SqlDbType.NVarChar),
					new SqlParameter("@State",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@BuyingID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.MappingID;
			parameters[1].Value = pEntity.DeductPayment;
			parameters[2].Value = pEntity.DeductPay;
			parameters[3].Value = pEntity.ProfitPay;
			parameters[4].Value = pEntity.RealPay;
			parameters[5].Value = pEntity.TotalPay;
			parameters[6].Value = pEntity.OrderDate;
			parameters[7].Value = pEntity.OrderNo;
			parameters[8].Value = pEntity.PayDate;
			parameters[9].Value = pEntity.BillNo;
			parameters[10].Value = pEntity.State;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.BuyingID;

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
        public void Update(WXHouseBuyEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WXHouseBuyEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXHouseBuyEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXHouseBuyEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.BuyingID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.BuyingID, pTran);           
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
            sql.AppendLine("update [WXHouseBuy] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where BuyingID=@BuyingID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@BuyingID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXHouseBuyEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.BuyingID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.BuyingID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXHouseBuyEntity[] pEntities)
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
            sql.AppendLine("update [WXHouseBuy] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where BuyingID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXHouseBuyEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseBuy] where isdelete=0 ");
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
            List<WXHouseBuyEntity> list = new List<WXHouseBuyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseBuyEntity m;
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
        public PagedQueryResult<WXHouseBuyEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [BuyingID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXHouseBuy] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXHouseBuy] where isdelete=0 ");
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
            PagedQueryResult<WXHouseBuyEntity> result = new PagedQueryResult<WXHouseBuyEntity>();
            List<WXHouseBuyEntity> list = new List<WXHouseBuyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseBuyEntity m;
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
        public WXHouseBuyEntity[] QueryByEntity(WXHouseBuyEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXHouseBuyEntity> PagedQueryByEntity(WXHouseBuyEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXHouseBuyEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.BuyingID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BuyingID", Value = pQueryEntity.BuyingID });
            if (pQueryEntity.MappingID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MappingID", Value = pQueryEntity.MappingID });
            if (pQueryEntity.DeductPayment!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeductPayment", Value = pQueryEntity.DeductPayment });
            if (pQueryEntity.DeductPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeductPay", Value = pQueryEntity.DeductPay });
            if (pQueryEntity.ProfitPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfitPay", Value = pQueryEntity.ProfitPay });
            if (pQueryEntity.RealPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RealPay", Value = pQueryEntity.RealPay });
            if (pQueryEntity.TotalPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalPay", Value = pQueryEntity.TotalPay });
            if (pQueryEntity.OrderDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDate", Value = pQueryEntity.OrderDate });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.PayDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayDate", Value = pQueryEntity.PayDate });
            if (pQueryEntity.BillNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BillNo", Value = pQueryEntity.BillNo });
            if (pQueryEntity.State!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "State", Value = pQueryEntity.State });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out WXHouseBuyEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXHouseBuyEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["BuyingID"] != DBNull.Value)
			{
				pInstance.BuyingID =  (Guid)pReader["BuyingID"];
			}
			if (pReader["MappingID"] != DBNull.Value)
			{
				pInstance.MappingID =  (Guid)pReader["MappingID"];
			}
			if (pReader["DeductPayment"] != DBNull.Value)
			{
				pInstance.DeductPayment =  Convert.ToDecimal(pReader["DeductPayment"]);
			}
			if (pReader["DeductPay"] != DBNull.Value)
			{
				pInstance.DeductPay =  Convert.ToDecimal(pReader["DeductPay"]);
			}
			if (pReader["ProfitPay"] != DBNull.Value)
			{
				pInstance.ProfitPay =  Convert.ToDecimal(pReader["ProfitPay"]);
			}
			if (pReader["RealPay"] != DBNull.Value)
			{
				pInstance.RealPay =  Convert.ToDecimal(pReader["RealPay"]);
			}
			if (pReader["TotalPay"] != DBNull.Value)
			{
				pInstance.TotalPay =  Convert.ToDecimal(pReader["TotalPay"]);
			}
			if (pReader["OrderDate"] != DBNull.Value)
			{
				pInstance.OrderDate =  Convert.ToDateTime(pReader["OrderDate"]);
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToDateTime(pReader["OrderNo"]);
			}
			if (pReader["PayDate"] != DBNull.Value)
			{
				pInstance.PayDate =  Convert.ToDateTime(pReader["PayDate"]);
			}
			if (pReader["BillNo"] != DBNull.Value)
			{
				pInstance.BillNo =  Convert.ToString(pReader["BillNo"]);
			}
			if (pReader["State"] != DBNull.Value)
			{
				pInstance.State =   Convert.ToInt32(pReader["State"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}

        }
        #endregion
    }
}
