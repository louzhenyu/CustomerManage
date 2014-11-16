/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/22 14:59:47
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
    /// ��LVipAddup�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LVipAddupDAO : Base.BaseCPOSDAO, ICRUDable<LVipAddupEntity>, IQueryable<LVipAddupEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LVipAddupDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(LVipAddupEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(LVipAddupEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [LVipAddup](");
            strSql.Append("[YearMonth],[VipAddupCount],[VipMonthCount],[VipMonthMoM],[VipVisitantCount],[VipVisitantMonthCount],[VipVisitantMonthMoM],[VipWeiXinAddupCount],[VipWeiXinMonthCount],[VipWeiXinMonthMoM],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[AddupId])");
            strSql.Append(" values (");
            strSql.Append("@YearMonth,@VipAddupCount,@VipMonthCount,@VipMonthMoM,@VipVisitantCount,@VipVisitantMonthCount,@VipVisitantMonthMoM,@VipWeiXinAddupCount,@VipWeiXinMonthCount,@VipWeiXinMonthMoM,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@AddupId)");            

			string pkString = pEntity.AddupId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@YearMonth",SqlDbType.NVarChar),
					new SqlParameter("@VipAddupCount",SqlDbType.Int),
					new SqlParameter("@VipMonthCount",SqlDbType.Int),
					new SqlParameter("@VipMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipVisitantCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipWeiXinAddupCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@AddupId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.YearMonth;
			parameters[1].Value = pEntity.VipAddupCount;
			parameters[2].Value = pEntity.VipMonthCount;
			parameters[3].Value = pEntity.VipMonthMoM;
			parameters[4].Value = pEntity.VipVisitantCount;
			parameters[5].Value = pEntity.VipVisitantMonthCount;
			parameters[6].Value = pEntity.VipVisitantMonthMoM;
			parameters[7].Value = pEntity.VipWeiXinAddupCount;
			parameters[8].Value = pEntity.VipWeiXinMonthCount;
			parameters[9].Value = pEntity.VipWeiXinMonthMoM;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.AddupId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public LVipAddupEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LVipAddup] where AddupId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            LVipAddupEntity m = null;
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
        public LVipAddupEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LVipAddup] where isdelete=0");
            //��ȡ����
            List<LVipAddupEntity> list = new List<LVipAddupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LVipAddupEntity m;
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
        public void Update(LVipAddupEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LVipAddupEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AddupId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LVipAddup] set ");
            if (pIsUpdateNullField || pEntity.YearMonth!=null)
                strSql.Append( "[YearMonth]=@YearMonth,");
            if (pIsUpdateNullField || pEntity.VipAddupCount!=null)
                strSql.Append( "[VipAddupCount]=@VipAddupCount,");
            if (pIsUpdateNullField || pEntity.VipMonthCount!=null)
                strSql.Append( "[VipMonthCount]=@VipMonthCount,");
            if (pIsUpdateNullField || pEntity.VipMonthMoM!=null)
                strSql.Append( "[VipMonthMoM]=@VipMonthMoM,");
            if (pIsUpdateNullField || pEntity.VipVisitantCount!=null)
                strSql.Append( "[VipVisitantCount]=@VipVisitantCount,");
            if (pIsUpdateNullField || pEntity.VipVisitantMonthCount!=null)
                strSql.Append( "[VipVisitantMonthCount]=@VipVisitantMonthCount,");
            if (pIsUpdateNullField || pEntity.VipVisitantMonthMoM!=null)
                strSql.Append( "[VipVisitantMonthMoM]=@VipVisitantMonthMoM,");
            if (pIsUpdateNullField || pEntity.VipWeiXinAddupCount!=null)
                strSql.Append( "[VipWeiXinAddupCount]=@VipWeiXinAddupCount,");
            if (pIsUpdateNullField || pEntity.VipWeiXinMonthCount!=null)
                strSql.Append( "[VipWeiXinMonthCount]=@VipWeiXinMonthCount,");
            if (pIsUpdateNullField || pEntity.VipWeiXinMonthMoM!=null)
                strSql.Append( "[VipWeiXinMonthMoM]=@VipWeiXinMonthMoM,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where AddupId=@AddupId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@YearMonth",SqlDbType.NVarChar),
					new SqlParameter("@VipAddupCount",SqlDbType.Int),
					new SqlParameter("@VipMonthCount",SqlDbType.Int),
					new SqlParameter("@VipMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipVisitantCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthCount",SqlDbType.Int),
					new SqlParameter("@VipVisitantMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@VipWeiXinAddupCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthCount",SqlDbType.Int),
					new SqlParameter("@VipWeiXinMonthMoM",SqlDbType.Decimal),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@AddupId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.YearMonth;
			parameters[1].Value = pEntity.VipAddupCount;
			parameters[2].Value = pEntity.VipMonthCount;
			parameters[3].Value = pEntity.VipMonthMoM;
			parameters[4].Value = pEntity.VipVisitantCount;
			parameters[5].Value = pEntity.VipVisitantMonthCount;
			parameters[6].Value = pEntity.VipVisitantMonthMoM;
			parameters[7].Value = pEntity.VipWeiXinAddupCount;
			parameters[8].Value = pEntity.VipWeiXinMonthCount;
			parameters[9].Value = pEntity.VipWeiXinMonthMoM;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.AddupId;

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
        public void Update(LVipAddupEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LVipAddupEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LVipAddupEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(LVipAddupEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.AddupId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.AddupId, pTran);           
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
            sql.AppendLine("update [LVipAddup] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where AddupId=@AddupId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@AddupId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LVipAddupEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.AddupId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.AddupId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(LVipAddupEntity[] pEntities)
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
            sql.AppendLine("update [LVipAddup] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where AddupId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LVipAddupEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LVipAddup] where isdelete=0 ");
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
            List<LVipAddupEntity> list = new List<LVipAddupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LVipAddupEntity m;
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
        public PagedQueryResult<LVipAddupEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [AddupId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [LVipAddup] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [LVipAddup] where isdelete=0 ");
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
            PagedQueryResult<LVipAddupEntity> result = new PagedQueryResult<LVipAddupEntity>();
            List<LVipAddupEntity> list = new List<LVipAddupEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LVipAddupEntity m;
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
        public LVipAddupEntity[] QueryByEntity(LVipAddupEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LVipAddupEntity> PagedQueryByEntity(LVipAddupEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LVipAddupEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.AddupId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AddupId", Value = pQueryEntity.AddupId });
            if (pQueryEntity.YearMonth!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "YearMonth", Value = pQueryEntity.YearMonth });
            if (pQueryEntity.VipAddupCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipAddupCount", Value = pQueryEntity.VipAddupCount });
            if (pQueryEntity.VipMonthCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipMonthCount", Value = pQueryEntity.VipMonthCount });
            if (pQueryEntity.VipMonthMoM!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipMonthMoM", Value = pQueryEntity.VipMonthMoM });
            if (pQueryEntity.VipVisitantCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipVisitantCount", Value = pQueryEntity.VipVisitantCount });
            if (pQueryEntity.VipVisitantMonthCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipVisitantMonthCount", Value = pQueryEntity.VipVisitantMonthCount });
            if (pQueryEntity.VipVisitantMonthMoM!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipVisitantMonthMoM", Value = pQueryEntity.VipVisitantMonthMoM });
            if (pQueryEntity.VipWeiXinAddupCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipWeiXinAddupCount", Value = pQueryEntity.VipWeiXinAddupCount });
            if (pQueryEntity.VipWeiXinMonthCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipWeiXinMonthCount", Value = pQueryEntity.VipWeiXinMonthCount });
            if (pQueryEntity.VipWeiXinMonthMoM!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipWeiXinMonthMoM", Value = pQueryEntity.VipWeiXinMonthMoM });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
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
        protected void Load(SqlDataReader pReader, out LVipAddupEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new LVipAddupEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["AddupId"] != DBNull.Value)
			{
				pInstance.AddupId =  Convert.ToString(pReader["AddupId"]);
			}
			if (pReader["YearMonth"] != DBNull.Value)
			{
				pInstance.YearMonth =  Convert.ToString(pReader["YearMonth"]);
			}
			if (pReader["VipAddupCount"] != DBNull.Value)
			{
				pInstance.VipAddupCount =   Convert.ToInt32(pReader["VipAddupCount"]);
			}
			if (pReader["VipMonthCount"] != DBNull.Value)
			{
				pInstance.VipMonthCount =   Convert.ToInt32(pReader["VipMonthCount"]);
			}
			if (pReader["VipMonthMoM"] != DBNull.Value)
			{
				pInstance.VipMonthMoM =  Convert.ToDecimal(pReader["VipMonthMoM"]);
			}
			if (pReader["VipVisitantCount"] != DBNull.Value)
			{
				pInstance.VipVisitantCount =   Convert.ToInt32(pReader["VipVisitantCount"]);
			}
			if (pReader["VipVisitantMonthCount"] != DBNull.Value)
			{
				pInstance.VipVisitantMonthCount =   Convert.ToInt32(pReader["VipVisitantMonthCount"]);
			}
			if (pReader["VipVisitantMonthMoM"] != DBNull.Value)
			{
				pInstance.VipVisitantMonthMoM =  Convert.ToDecimal(pReader["VipVisitantMonthMoM"]);
			}
			if (pReader["VipWeiXinAddupCount"] != DBNull.Value)
			{
				pInstance.VipWeiXinAddupCount =   Convert.ToInt32(pReader["VipWeiXinAddupCount"]);
			}
			if (pReader["VipWeiXinMonthCount"] != DBNull.Value)
			{
				pInstance.VipWeiXinMonthCount =   Convert.ToInt32(pReader["VipWeiXinMonthCount"]);
			}
			if (pReader["VipWeiXinMonthMoM"] != DBNull.Value)
			{
				pInstance.VipWeiXinMonthMoM =  Convert.ToDecimal(pReader["VipWeiXinMonthMoM"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
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
