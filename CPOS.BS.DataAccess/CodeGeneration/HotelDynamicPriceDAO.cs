/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 11:41:01
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
    /// ��HotelDynamicPrice�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class HotelDynamicPriceDAO : Base.BaseCPOSDAO, ICRUDable<HotelDynamicPriceEntity>, IQueryable<HotelDynamicPriceEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public HotelDynamicPriceDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(HotelDynamicPriceEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(HotelDynamicPriceEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [HotelDynamicPrice](");
            strSql.Append("[RoomId],[FloatPrice],[MaxIntegralToCurrency],[DonateIntegral],[ReturnCurrency],[EffectiveBeginTime],[EffectiveEndTime],[EffectiveBeginDate],[EffectiveEndDate],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[HotelDynamicPriceId])");
            strSql.Append(" values (");
            strSql.Append("@RoomId,@FloatPrice,@MaxIntegralToCurrency,@DonateIntegral,@ReturnCurrency,@EffectiveBeginTime,@EffectiveEndTime,@EffectiveBeginDate,@EffectiveEndDate,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@HotelDynamicPriceId)");            

			Guid? pkGuid;
			if (pEntity.HotelDynamicPriceId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.HotelDynamicPriceId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@RoomId",SqlDbType.NVarChar),
					new SqlParameter("@FloatPrice",SqlDbType.Decimal),
					new SqlParameter("@MaxIntegralToCurrency",SqlDbType.Int),
					new SqlParameter("@DonateIntegral",SqlDbType.Int),
					new SqlParameter("@ReturnCurrency",SqlDbType.Decimal),
					new SqlParameter("@EffectiveBeginTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveBeginDate",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndDate",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@HotelDynamicPriceId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.RoomId;
			parameters[1].Value = pEntity.FloatPrice;
			parameters[2].Value = pEntity.MaxIntegralToCurrency;
			parameters[3].Value = pEntity.DonateIntegral;
			parameters[4].Value = pEntity.ReturnCurrency;
			parameters[5].Value = pEntity.EffectiveBeginTime;
			parameters[6].Value = pEntity.EffectiveEndTime;
			parameters[7].Value = pEntity.EffectiveBeginDate;
			parameters[8].Value = pEntity.EffectiveEndDate;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.HotelDynamicPriceId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public HotelDynamicPriceEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelDynamicPrice] where HotelDynamicPriceId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            HotelDynamicPriceEntity m = null;
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
        public HotelDynamicPriceEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelDynamicPrice] where isdelete=0");
            //��ȡ����
            List<HotelDynamicPriceEntity> list = new List<HotelDynamicPriceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelDynamicPriceEntity m;
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
        public void Update(HotelDynamicPriceEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(HotelDynamicPriceEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.HotelDynamicPriceId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [HotelDynamicPrice] set ");
            if (pIsUpdateNullField || pEntity.RoomId!=null)
                strSql.Append( "[RoomId]=@RoomId,");
            if (pIsUpdateNullField || pEntity.FloatPrice!=null)
                strSql.Append( "[FloatPrice]=@FloatPrice,");
            if (pIsUpdateNullField || pEntity.MaxIntegralToCurrency!=null)
                strSql.Append( "[MaxIntegralToCurrency]=@MaxIntegralToCurrency,");
            if (pIsUpdateNullField || pEntity.DonateIntegral!=null)
                strSql.Append( "[DonateIntegral]=@DonateIntegral,");
            if (pIsUpdateNullField || pEntity.ReturnCurrency!=null)
                strSql.Append( "[ReturnCurrency]=@ReturnCurrency,");
            if (pIsUpdateNullField || pEntity.EffectiveBeginTime!=null)
                strSql.Append( "[EffectiveBeginTime]=@EffectiveBeginTime,");
            if (pIsUpdateNullField || pEntity.EffectiveEndTime!=null)
                strSql.Append( "[EffectiveEndTime]=@EffectiveEndTime,");
            if (pIsUpdateNullField || pEntity.EffectiveBeginDate!=null)
                strSql.Append( "[EffectiveBeginDate]=@EffectiveBeginDate,");
            if (pIsUpdateNullField || pEntity.EffectiveEndDate!=null)
                strSql.Append( "[EffectiveEndDate]=@EffectiveEndDate,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where HotelDynamicPriceId=@HotelDynamicPriceId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@RoomId",SqlDbType.NVarChar),
					new SqlParameter("@FloatPrice",SqlDbType.Decimal),
					new SqlParameter("@MaxIntegralToCurrency",SqlDbType.Int),
					new SqlParameter("@DonateIntegral",SqlDbType.Int),
					new SqlParameter("@ReturnCurrency",SqlDbType.Decimal),
					new SqlParameter("@EffectiveBeginTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndTime",SqlDbType.DateTime),
					new SqlParameter("@EffectiveBeginDate",SqlDbType.DateTime),
					new SqlParameter("@EffectiveEndDate",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@HotelDynamicPriceId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.RoomId;
			parameters[1].Value = pEntity.FloatPrice;
			parameters[2].Value = pEntity.MaxIntegralToCurrency;
			parameters[3].Value = pEntity.DonateIntegral;
			parameters[4].Value = pEntity.ReturnCurrency;
			parameters[5].Value = pEntity.EffectiveBeginTime;
			parameters[6].Value = pEntity.EffectiveEndTime;
			parameters[7].Value = pEntity.EffectiveBeginDate;
			parameters[8].Value = pEntity.EffectiveEndDate;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.CustomerId;
			parameters[12].Value = pEntity.HotelDynamicPriceId;

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
        public void Update(HotelDynamicPriceEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(HotelDynamicPriceEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(HotelDynamicPriceEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(HotelDynamicPriceEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.HotelDynamicPriceId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.HotelDynamicPriceId, pTran);           
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
            sql.AppendLine("update [HotelDynamicPrice] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where HotelDynamicPriceId=@HotelDynamicPriceId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@HotelDynamicPriceId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(HotelDynamicPriceEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.HotelDynamicPriceId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.HotelDynamicPriceId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(HotelDynamicPriceEntity[] pEntities)
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
            sql.AppendLine("update [HotelDynamicPrice] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where HotelDynamicPriceId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public HotelDynamicPriceEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [HotelDynamicPrice] where isdelete=0 ");
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
            List<HotelDynamicPriceEntity> list = new List<HotelDynamicPriceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelDynamicPriceEntity m;
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
        public PagedQueryResult<HotelDynamicPriceEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [HotelDynamicPriceId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [HotelDynamicPrice] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [HotelDynamicPrice] where isdelete=0 ");
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
            PagedQueryResult<HotelDynamicPriceEntity> result = new PagedQueryResult<HotelDynamicPriceEntity>();
            List<HotelDynamicPriceEntity> list = new List<HotelDynamicPriceEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    HotelDynamicPriceEntity m;
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
        public HotelDynamicPriceEntity[] QueryByEntity(HotelDynamicPriceEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<HotelDynamicPriceEntity> PagedQueryByEntity(HotelDynamicPriceEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(HotelDynamicPriceEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.HotelDynamicPriceId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HotelDynamicPriceId", Value = pQueryEntity.HotelDynamicPriceId });
            if (pQueryEntity.RoomId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RoomId", Value = pQueryEntity.RoomId });
            if (pQueryEntity.FloatPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FloatPrice", Value = pQueryEntity.FloatPrice });
            if (pQueryEntity.MaxIntegralToCurrency!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxIntegralToCurrency", Value = pQueryEntity.MaxIntegralToCurrency });
            if (pQueryEntity.DonateIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DonateIntegral", Value = pQueryEntity.DonateIntegral });
            if (pQueryEntity.ReturnCurrency!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReturnCurrency", Value = pQueryEntity.ReturnCurrency });
            if (pQueryEntity.EffectiveBeginTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveBeginTime", Value = pQueryEntity.EffectiveBeginTime });
            if (pQueryEntity.EffectiveEndTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveEndTime", Value = pQueryEntity.EffectiveEndTime });
            if (pQueryEntity.EffectiveBeginDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveBeginDate", Value = pQueryEntity.EffectiveBeginDate });
            if (pQueryEntity.EffectiveEndDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EffectiveEndDate", Value = pQueryEntity.EffectiveEndDate });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out HotelDynamicPriceEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new HotelDynamicPriceEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["HotelDynamicPriceId"] != DBNull.Value)
			{
				pInstance.HotelDynamicPriceId =  (Guid)pReader["HotelDynamicPriceId"];
			}
			if (pReader["RoomId"] != DBNull.Value)
			{
				pInstance.RoomId =  Convert.ToString(pReader["RoomId"]);
			}
			if (pReader["FloatPrice"] != DBNull.Value)
			{
				pInstance.FloatPrice =  Convert.ToDecimal(pReader["FloatPrice"]);
			}
			if (pReader["MaxIntegralToCurrency"] != DBNull.Value)
			{
				pInstance.MaxIntegralToCurrency =   Convert.ToInt32(pReader["MaxIntegralToCurrency"]);
			}
			if (pReader["DonateIntegral"] != DBNull.Value)
			{
				pInstance.DonateIntegral =   Convert.ToInt32(pReader["DonateIntegral"]);
			}
			if (pReader["ReturnCurrency"] != DBNull.Value)
			{
				pInstance.ReturnCurrency =  Convert.ToDecimal(pReader["ReturnCurrency"]);
			}
			if (pReader["EffectiveBeginTime"] != DBNull.Value)
			{
				pInstance.EffectiveBeginTime =  Convert.ToDateTime(pReader["EffectiveBeginTime"]);
			}
			if (pReader["EffectiveEndTime"] != DBNull.Value)
			{
				pInstance.EffectiveEndTime =  Convert.ToDateTime(pReader["EffectiveEndTime"]);
			}
			if (pReader["EffectiveBeginDate"] != DBNull.Value)
			{
				pInstance.EffectiveBeginDate =  Convert.ToDateTime(pReader["EffectiveBeginDate"]);
			}
			if (pReader["EffectiveEndDate"] != DBNull.Value)
			{
				pInstance.EffectiveEndDate =  Convert.ToDateTime(pReader["EffectiveEndDate"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
