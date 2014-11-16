/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/13 16:48:24
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
    /// ��TInOutStatusNode�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TInOutStatusNodeDAO : Base.BaseCPOSDAO, ICRUDable<TInOutStatusNodeEntity>, IQueryable<TInOutStatusNodeEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public TInOutStatusNodeDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(TInOutStatusNodeEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(TInOutStatusNodeEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [TInOutStatusNode](");
            strSql.Append("[NodeCode],[NodeValue],[PreviousValue],[NextValue],[PayMethod],[Sequence],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[DeliveryMethod],[ActionDesc],[ActionDescEn],[NodeID])");
            strSql.Append(" values (");
            strSql.Append("@NodeCode,@NodeValue,@PreviousValue,@NextValue,@PayMethod,@Sequence,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@DeliveryMethod,@ActionDesc,@ActionDescEn,@NodeID)");           

			Guid? pkGuid;
			if (pEntity.NodeID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.NodeID;

            SqlParameter[] parameters = 
            {
                    new SqlParameter("@NodeCode",SqlDbType.NVarChar,100),
					new SqlParameter("@NodeValue",SqlDbType.NVarChar,100),
					new SqlParameter("@PreviousValue",SqlDbType.NVarChar,100),
					new SqlParameter("@NextValue",SqlDbType.NVarChar,100),
					new SqlParameter("@PayMethod",SqlDbType.NVarChar,100),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar,100),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar,100),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@DeliveryMethod",SqlDbType.NVarChar,100),
					new SqlParameter("@ActionDesc",SqlDbType.NVarChar,100),
					new SqlParameter("@ActionDescEn",SqlDbType.NVarChar,100),
					new SqlParameter("@NodeID",SqlDbType.UniqueIdentifier,16)
            };
            parameters[0].Value = pEntity.NodeCode;
            parameters[1].Value = pEntity.NodeValue;
            parameters[2].Value = pEntity.PreviousValue;
            parameters[3].Value = pEntity.NextValue;
            parameters[4].Value = pEntity.PayMethod;
            parameters[5].Value = pEntity.Sequence;
            parameters[6].Value = pEntity.CustomerID;
            parameters[7].Value = pEntity.CreateBy;
            parameters[8].Value = pEntity.CreateTime;
            parameters[9].Value = pEntity.LastUpdateBy;
            parameters[10].Value = pEntity.LastUpdateTime;
            parameters[11].Value = pEntity.IsDelete;
            parameters[12].Value = pEntity.DeliveryMethod;
            parameters[13].Value = pEntity.ActionDesc;
            parameters[14].Value = pEntity.ActionDescEn;
            parameters[15].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.NodeID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public TInOutStatusNodeEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInOutStatusNode] where NodeID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            TInOutStatusNodeEntity m = null;
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
        public TInOutStatusNodeEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInOutStatusNode] where isdelete=0");
            //��ȡ����
            List<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInOutStatusNodeEntity m;
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
        public void Update(TInOutStatusNodeEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(TInOutStatusNodeEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NodeID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [TInOutStatusNode] set ");
            if (pIsUpdateNullField || pEntity.NodeCode!=null)
                strSql.Append( "[NodeCode]=@NodeCode,");
            if (pIsUpdateNullField || pEntity.NodeValue!=null)
                strSql.Append( "[NodeValue]=@NodeValue,");
            if (pIsUpdateNullField || pEntity.PreviousValue!=null)
                strSql.Append( "[PreviousValue]=@PreviousValue,");
            if (pIsUpdateNullField || pEntity.NextValue!=null)
                strSql.Append( "[NextValue]=@NextValue,");
            if (pIsUpdateNullField || pEntity.PayMethod!=null)
                strSql.Append( "[PayMethod]=@PayMethod,");
            if (pIsUpdateNullField || pEntity.Sequence!=null)
                strSql.Append( "[Sequence]=@Sequence,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");

            if (pIsUpdateNullField || pEntity.DeliveryMethod != null)
                strSql.Append("[DeliveryMethod]=@DeliveryMethod,");
            if (pIsUpdateNullField || pEntity.ActionDesc != null)
                strSql.Append("[ActionDesc]=@ActionDesc,");
            if (pIsUpdateNullField || pEntity.ActionDescEn != null)
                strSql.Append("[ActionDescEn]=@ActionDescEn,");

            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where NodeID=@NodeID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@NodeCode",SqlDbType.NVarChar),
					new SqlParameter("@NodeValue",SqlDbType.NVarChar),
					new SqlParameter("@PreviousValue",SqlDbType.NVarChar),
					new SqlParameter("@NextValue",SqlDbType.NVarChar),
					new SqlParameter("@PayMethod",SqlDbType.NVarChar),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),

                    new SqlParameter("@DeliveryMethod",SqlDbType.NVarChar),
                    new SqlParameter("@ActionDesc",SqlDbType.NVarChar),
                    new SqlParameter("@ActionDescEn",SqlDbType.NVarChar),

					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@NodeID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.NodeCode;
			parameters[1].Value = pEntity.NodeValue;
			parameters[2].Value = pEntity.PreviousValue;
			parameters[3].Value = pEntity.NextValue;
			parameters[4].Value = pEntity.PayMethod;
			parameters[5].Value = pEntity.Sequence;
			parameters[6].Value = pEntity.CustomerID;
			parameters[7].Value = pEntity.LastUpdateBy;

            parameters[8].Value = pEntity.DeliveryMethod;
            parameters[9].Value = pEntity.ActionDesc;
            parameters[10].Value = pEntity.ActionDescEn;

			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.NodeID;

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
        public void Update(TInOutStatusNodeEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(TInOutStatusNodeEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(TInOutStatusNodeEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(TInOutStatusNodeEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NodeID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.NodeID, pTran);           
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
            sql.AppendLine("update [TInOutStatusNode] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where NodeID=@NodeID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@NodeID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(TInOutStatusNodeEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.NodeID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.NodeID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(TInOutStatusNodeEntity[] pEntities)
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
            sql.AppendLine("update [TInOutStatusNode] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where NodeID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public TInOutStatusNodeEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [TInOutStatusNode] where isdelete=0 ");
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
            List<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    TInOutStatusNodeEntity m;
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
        public PagedQueryResult<TInOutStatusNodeEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [NodeID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [TInOutStatusNode] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [TInOutStatusNode] where isdelete=0 ");
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
            PagedQueryResult<TInOutStatusNodeEntity> result = new PagedQueryResult<TInOutStatusNodeEntity>();
            List<TInOutStatusNodeEntity> list = new List<TInOutStatusNodeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    TInOutStatusNodeEntity m;
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
        public TInOutStatusNodeEntity[] QueryByEntity(TInOutStatusNodeEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<TInOutStatusNodeEntity> PagedQueryByEntity(TInOutStatusNodeEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(TInOutStatusNodeEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.NodeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NodeID", Value = pQueryEntity.NodeID });
            if (pQueryEntity.NodeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NodeCode", Value = pQueryEntity.NodeCode });
            if (pQueryEntity.NodeValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NodeValue", Value = pQueryEntity.NodeValue });
            if (pQueryEntity.PreviousValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PreviousValue", Value = pQueryEntity.PreviousValue });
            if (pQueryEntity.NextValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NextValue", Value = pQueryEntity.NextValue });
            if (pQueryEntity.PayMethod!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PayMethod", Value = pQueryEntity.PayMethod });
            if (pQueryEntity.Sequence!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sequence", Value = pQueryEntity.Sequence });
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
            if (pQueryEntity.DeliveryMethod != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryMethod", Value = pQueryEntity.DeliveryMethod });
            if (pQueryEntity.ActionDesc != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActionDesc", Value = pQueryEntity.ActionDesc });
            if (pQueryEntity.ActionDescEn != null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ActionDescEn", Value = pQueryEntity.ActionDescEn });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out TInOutStatusNodeEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new TInOutStatusNodeEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["NodeID"] != DBNull.Value)
			{
				pInstance.NodeID =  (Guid)pReader["NodeID"];
			}
			if (pReader["NodeCode"] != DBNull.Value)
			{
				pInstance.NodeCode =  Convert.ToString(pReader["NodeCode"]);
			}
			if (pReader["NodeValue"] != DBNull.Value)
			{
				pInstance.NodeValue =  Convert.ToString(pReader["NodeValue"]);
			}
			if (pReader["PreviousValue"] != DBNull.Value)
			{
				pInstance.PreviousValue =  Convert.ToString(pReader["PreviousValue"]);
			}
			if (pReader["NextValue"] != DBNull.Value)
			{
				pInstance.NextValue =  Convert.ToString(pReader["NextValue"]);
			}
			if (pReader["PayMethod"] != DBNull.Value)
			{
				pInstance.PayMethod =  Convert.ToString(pReader["PayMethod"]);
			}
			if (pReader["Sequence"] != DBNull.Value)
			{
				pInstance.Sequence =   Convert.ToInt32(pReader["Sequence"]);
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
            if (pReader["DeliveryMethod"] != DBNull.Value)
            {
                pInstance.DeliveryMethod = Convert.ToString(pReader["DeliveryMethod"]);
            }
            if (pReader["ActionDesc"] != DBNull.Value)
            {
                pInstance.ActionDesc = Convert.ToString(pReader["ActionDesc"]);
            }
            if (pReader["ActionDescEn"] != DBNull.Value)
            {
                pInstance.ActionDescEn = Convert.ToString(pReader["ActionDescEn"]);
            }

        }
        #endregion
    }
}
