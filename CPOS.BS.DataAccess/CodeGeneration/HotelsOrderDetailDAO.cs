/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 18:19:53
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
    /// ��HotelsOrderDetail�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class HotelsOrderDetailDAO : Base.BaseCPOSDAO, ICRUDable<HotelsOrderDetailEntity>, IQueryable<HotelsOrderDetailEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public HotelsOrderDetailDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(HotelsOrderDetailEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(HotelsOrderDetailEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [HotelsOrderDetail](");
            strSql.Append("[OrderId],[RoomId],[CurrencyType],[CheckInDate],[RoomQty],[CheckInPeople],[InStatus],[StdPrice],[SalesAmount],[SalesTotalAmount],[DiscountRate],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[OrderDetailId])");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@RoomId,@CurrencyType,@CheckInDate,@RoomQty,@CheckInPeople,@InStatus,@StdPrice,@SalesAmount,@SalesTotalAmount,@DiscountRate,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@OrderDetailId)");            

			string pkString = pEntity.OrderDetailId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@RoomId",SqlDbType.NVarChar),
					new SqlParameter("@CurrencyType",SqlDbType.NVarChar),
					new SqlParameter("@CheckInDate",SqlDbType.DateTime),
					new SqlParameter("@RoomQty",SqlDbType.Int),
					new SqlParameter("@CheckInPeople",SqlDbType.NVarChar),
					new SqlParameter("@InStatus",SqlDbType.Int),
					new SqlParameter("@StdPrice",SqlDbType.Decimal),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@OrderDetailId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.RoomId;
			parameters[2].Value = pEntity.CurrencyType;
			parameters[3].Value = pEntity.CheckInDate;
			parameters[4].Value = pEntity.RoomQty;
			parameters[5].Value = pEntity.CheckInPeople;
			parameters[6].Value = pEntity.InStatus;
			parameters[7].Value = pEntity.StdPrice;
			parameters[8].Value = pEntity.SalesAmount;
			parameters[9].Value = pEntity.SalesTotalAmount;
			parameters[10].Value = pEntity.DiscountRate;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderDetailId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public HotelsOrderDetailEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelsOrderDetail] where OrderDetailId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            HotelsOrderDetailEntity m = null;
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
        public HotelsOrderDetailEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelsOrderDetail] where isdelete=0");
            //��ȡ����
            List<HotelsOrderDetailEntity> list = new List<HotelsOrderDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelsOrderDetailEntity m;
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
        public void Update(HotelsOrderDetailEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(HotelsOrderDetailEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderDetailId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [HotelsOrderDetail] set ");
            if (pIsUpdateNullField || pEntity.OrderId!=null)
                strSql.Append( "[OrderId]=@OrderId,");
            if (pIsUpdateNullField || pEntity.RoomId!=null)
                strSql.Append( "[RoomId]=@RoomId,");
            if (pIsUpdateNullField || pEntity.CurrencyType!=null)
                strSql.Append( "[CurrencyType]=@CurrencyType,");
            if (pIsUpdateNullField || pEntity.CheckInDate!=null)
                strSql.Append( "[CheckInDate]=@CheckInDate,");
            if (pIsUpdateNullField || pEntity.RoomQty!=null)
                strSql.Append( "[RoomQty]=@RoomQty,");
            if (pIsUpdateNullField || pEntity.CheckInPeople!=null)
                strSql.Append( "[CheckInPeople]=@CheckInPeople,");
            if (pIsUpdateNullField || pEntity.InStatus!=null)
                strSql.Append( "[InStatus]=@InStatus,");
            if (pIsUpdateNullField || pEntity.StdPrice!=null)
                strSql.Append( "[StdPrice]=@StdPrice,");
            if (pIsUpdateNullField || pEntity.SalesAmount!=null)
                strSql.Append( "[SalesAmount]=@SalesAmount,");
            if (pIsUpdateNullField || pEntity.SalesTotalAmount!=null)
                strSql.Append( "[SalesTotalAmount]=@SalesTotalAmount,");
            if (pIsUpdateNullField || pEntity.DiscountRate!=null)
                strSql.Append( "[DiscountRate]=@DiscountRate,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderDetailId=@OrderDetailId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@RoomId",SqlDbType.NVarChar),
					new SqlParameter("@CurrencyType",SqlDbType.NVarChar),
					new SqlParameter("@CheckInDate",SqlDbType.DateTime),
					new SqlParameter("@RoomQty",SqlDbType.Int),
					new SqlParameter("@CheckInPeople",SqlDbType.NVarChar),
					new SqlParameter("@InStatus",SqlDbType.Int),
					new SqlParameter("@StdPrice",SqlDbType.Decimal),
					new SqlParameter("@SalesAmount",SqlDbType.Decimal),
					new SqlParameter("@SalesTotalAmount",SqlDbType.Decimal),
					new SqlParameter("@DiscountRate",SqlDbType.Decimal),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@OrderDetailId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.RoomId;
			parameters[2].Value = pEntity.CurrencyType;
			parameters[3].Value = pEntity.CheckInDate;
			parameters[4].Value = pEntity.RoomQty;
			parameters[5].Value = pEntity.CheckInPeople;
			parameters[6].Value = pEntity.InStatus;
			parameters[7].Value = pEntity.StdPrice;
			parameters[8].Value = pEntity.SalesAmount;
			parameters[9].Value = pEntity.SalesTotalAmount;
			parameters[10].Value = pEntity.DiscountRate;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.OrderDetailId;

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
        public void Update(HotelsOrderDetailEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(HotelsOrderDetailEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(HotelsOrderDetailEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(HotelsOrderDetailEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderDetailId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderDetailId, pTran);           
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
            sql.AppendLine("update [HotelsOrderDetail] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderDetailId=@OrderDetailId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderDetailId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(HotelsOrderDetailEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.OrderDetailId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.OrderDetailId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(HotelsOrderDetailEntity[] pEntities)
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
            sql.AppendLine("update [HotelsOrderDetail] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where OrderDetailId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public HotelsOrderDetailEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelsOrderDetail] where isdelete=0 ");
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
            List<HotelsOrderDetailEntity> list = new List<HotelsOrderDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelsOrderDetailEntity m;
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
        public PagedQueryResult<HotelsOrderDetailEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderDetailId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [HotelsOrderDetail] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [HotelsOrderDetail] where isdelete=0 ");
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
            PagedQueryResult<HotelsOrderDetailEntity> result = new PagedQueryResult<HotelsOrderDetailEntity>();
            List<HotelsOrderDetailEntity> list = new List<HotelsOrderDetailEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelsOrderDetailEntity m;
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
        public HotelsOrderDetailEntity[] QueryByEntity(HotelsOrderDetailEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<HotelsOrderDetailEntity> PagedQueryByEntity(HotelsOrderDetailEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(HotelsOrderDetailEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderDetailId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDetailId", Value = pQueryEntity.OrderDetailId });
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.RoomId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomId", Value = pQueryEntity.RoomId });
            if (pQueryEntity.CurrencyType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CurrencyType", Value = pQueryEntity.CurrencyType });
            if (pQueryEntity.CheckInDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CheckInDate", Value = pQueryEntity.CheckInDate });
            if (pQueryEntity.RoomQty!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomQty", Value = pQueryEntity.RoomQty });
            if (pQueryEntity.CheckInPeople!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CheckInPeople", Value = pQueryEntity.CheckInPeople });
            if (pQueryEntity.InStatus!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InStatus", Value = pQueryEntity.InStatus });
            if (pQueryEntity.StdPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StdPrice", Value = pQueryEntity.StdPrice });
            if (pQueryEntity.SalesAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesAmount", Value = pQueryEntity.SalesAmount });
            if (pQueryEntity.SalesTotalAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SalesTotalAmount", Value = pQueryEntity.SalesTotalAmount });
            if (pQueryEntity.DiscountRate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscountRate", Value = pQueryEntity.DiscountRate });
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
        protected void Load(SqlDataReader pReader, out HotelsOrderDetailEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new HotelsOrderDetailEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderDetailId"] != DBNull.Value)
			{
				pInstance.OrderDetailId =  Convert.ToString(pReader["OrderDetailId"]);
			}
			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["RoomId"] != DBNull.Value)
			{
				pInstance.RoomId =  Convert.ToString(pReader["RoomId"]);
			}
			if (pReader["CurrencyType"] != DBNull.Value)
			{
				pInstance.CurrencyType =  Convert.ToString(pReader["CurrencyType"]);
			}
			if (pReader["CheckInDate"] != DBNull.Value)
			{
				pInstance.CheckInDate =  Convert.ToDateTime(pReader["CheckInDate"]);
			}
			if (pReader["RoomQty"] != DBNull.Value)
			{
				pInstance.RoomQty =   Convert.ToInt32(pReader["RoomQty"]);
			}
			if (pReader["CheckInPeople"] != DBNull.Value)
			{
				pInstance.CheckInPeople =  Convert.ToString(pReader["CheckInPeople"]);
			}
			if (pReader["InStatus"] != DBNull.Value)
			{
				pInstance.InStatus =   Convert.ToInt32(pReader["InStatus"]);
			}
			if (pReader["StdPrice"] != DBNull.Value)
			{
				pInstance.StdPrice =  Convert.ToDecimal(pReader["StdPrice"]);
			}
			if (pReader["SalesAmount"] != DBNull.Value)
			{
				pInstance.SalesAmount =  Convert.ToDecimal(pReader["SalesAmount"]);
			}
			if (pReader["SalesTotalAmount"] != DBNull.Value)
			{
				pInstance.SalesTotalAmount =  Convert.ToDecimal(pReader["SalesTotalAmount"]);
			}
			if (pReader["DiscountRate"] != DBNull.Value)
			{
				pInstance.DiscountRate =  Convert.ToDecimal(pReader["DiscountRate"]);
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
