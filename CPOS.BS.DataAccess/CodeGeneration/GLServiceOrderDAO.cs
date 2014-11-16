/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/6 16:00:15
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
    /// ��GL_ServiceOrder�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class GLServiceOrderDAO : Base.BaseCPOSDAO, ICRUDable<GLServiceOrderEntity>, IQueryable<GLServiceOrderEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public GLServiceOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(GLServiceOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(GLServiceOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into cpos_demo.dbo.[GL_ServiceOrder](");
            strSql.Append("[ServiceType],[ServiceDate],[ServiceDateEnd],[ServiceAddress],[Latitude],[Longitude],[CustomerMessage],[ProductOrderID],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ServiceOrderID])");
            strSql.Append(" values (");
            strSql.Append("@ServiceType,@ServiceDate,@ServiceDateEnd,@ServiceAddress,@Latitude,@Longitude,@CustomerMessage,@ProductOrderID,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ServiceOrderID)");            

			string pkString = pEntity.ServiceOrderID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ServiceType",SqlDbType.Int),
					new SqlParameter("@ServiceDate",SqlDbType.DateTime),
					new SqlParameter("@ServiceDateEnd",SqlDbType.DateTime),
					new SqlParameter("@ServiceAddress",SqlDbType.NVarChar),
					new SqlParameter("@Latitude",SqlDbType.Decimal),
					new SqlParameter("@Longitude",SqlDbType.Decimal),
					new SqlParameter("@CustomerMessage",SqlDbType.NVarChar),
					new SqlParameter("@ProductOrderID",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ServiceOrderID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ServiceType;
			parameters[1].Value = pEntity.ServiceDate;
			parameters[2].Value = pEntity.ServiceDateEnd;
			parameters[3].Value = pEntity.ServiceAddress;
			parameters[4].Value = pEntity.Latitude;
			parameters[5].Value = pEntity.Longitude;
			parameters[6].Value = pEntity.CustomerMessage;
			parameters[7].Value = pEntity.ProductOrderID;
			parameters[8].Value = pEntity.CustomerID;
			parameters[9].Value = pEntity.CreateBy;
			parameters[10].Value = pEntity.CreateTime;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ServiceOrderID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public GLServiceOrderEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from cpos_demo.dbo.[GL_ServiceOrder] where ServiceOrderID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            GLServiceOrderEntity m = null;
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
        public GLServiceOrderEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from cpos_demo.dbo.[GL_ServiceOrder] where isdelete=0");
            //��ȡ����
            List<GLServiceOrderEntity> list = new List<GLServiceOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    GLServiceOrderEntity m;
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
        public void Update(GLServiceOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(GLServiceOrderEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ServiceOrderID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update cpos_demo.dbo.[GL_ServiceOrder] set ");
            if (pIsUpdateNullField || pEntity.ServiceType!=null)
                strSql.Append( "[ServiceType]=@ServiceType,");
            if (pIsUpdateNullField || pEntity.ServiceDate!=null)
                strSql.Append( "[ServiceDate]=@ServiceDate,");
            if (pIsUpdateNullField || pEntity.ServiceDateEnd!=null)
                strSql.Append( "[ServiceDateEnd]=@ServiceDateEnd,");
            if (pIsUpdateNullField || pEntity.ServiceAddress!=null)
                strSql.Append( "[ServiceAddress]=@ServiceAddress,");
            if (pIsUpdateNullField || pEntity.Latitude!=null)
                strSql.Append( "[Latitude]=@Latitude,");
            if (pIsUpdateNullField || pEntity.Longitude!=null)
                strSql.Append( "[Longitude]=@Longitude,");
            if (pIsUpdateNullField || pEntity.CustomerMessage!=null)
                strSql.Append( "[CustomerMessage]=@CustomerMessage,");
            if (pIsUpdateNullField || pEntity.ProductOrderID!=null)
                strSql.Append( "[ProductOrderID]=@ProductOrderID,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ServiceOrderID=@ServiceOrderID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ServiceType",SqlDbType.Int),
					new SqlParameter("@ServiceDate",SqlDbType.DateTime),
					new SqlParameter("@ServiceDateEnd",SqlDbType.DateTime),
					new SqlParameter("@ServiceAddress",SqlDbType.NVarChar),
					new SqlParameter("@Latitude",SqlDbType.Decimal),
					new SqlParameter("@Longitude",SqlDbType.Decimal),
					new SqlParameter("@CustomerMessage",SqlDbType.NVarChar),
					new SqlParameter("@ProductOrderID",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ServiceOrderID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ServiceType;
			parameters[1].Value = pEntity.ServiceDate;
			parameters[2].Value = pEntity.ServiceDateEnd;
			parameters[3].Value = pEntity.ServiceAddress;
			parameters[4].Value = pEntity.Latitude;
			parameters[5].Value = pEntity.Longitude;
			parameters[6].Value = pEntity.CustomerMessage;
			parameters[7].Value = pEntity.ProductOrderID;
			parameters[8].Value = pEntity.CustomerID;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.ServiceOrderID;

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
        public void Update(GLServiceOrderEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(GLServiceOrderEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(GLServiceOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(GLServiceOrderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ServiceOrderID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ServiceOrderID, pTran);           
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
            sql.AppendLine("update cpos_demo.dbo.[GL_ServiceOrder] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ServiceOrderID=@ServiceOrderID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ServiceOrderID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(GLServiceOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ServiceOrderID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ServiceOrderID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(GLServiceOrderEntity[] pEntities)
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
            sql.AppendLine("update cpos_demo.dbo.[GL_ServiceOrder] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ServiceOrderID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public GLServiceOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from cpos_demo.dbo.[GL_ServiceOrder] where isdelete=0 ");
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
            List<GLServiceOrderEntity> list = new List<GLServiceOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    GLServiceOrderEntity m;
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
        public PagedQueryResult<GLServiceOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ServiceOrderID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [GLServiceOrder] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [GLServiceOrder] where isdelete=0 ");
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
            PagedQueryResult<GLServiceOrderEntity> result = new PagedQueryResult<GLServiceOrderEntity>();
            List<GLServiceOrderEntity> list = new List<GLServiceOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    GLServiceOrderEntity m;
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
        public GLServiceOrderEntity[] QueryByEntity(GLServiceOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<GLServiceOrderEntity> PagedQueryByEntity(GLServiceOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(GLServiceOrderEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ServiceOrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceOrderID", Value = pQueryEntity.ServiceOrderID });
            if (pQueryEntity.ServiceType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceType", Value = pQueryEntity.ServiceType });
            if (pQueryEntity.ServiceDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceDate", Value = pQueryEntity.ServiceDate });
            if (pQueryEntity.ServiceDateEnd!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceDateEnd", Value = pQueryEntity.ServiceDateEnd });
            if (pQueryEntity.ServiceAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ServiceAddress", Value = pQueryEntity.ServiceAddress });
            if (pQueryEntity.Latitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Latitude", Value = pQueryEntity.Latitude });
            if (pQueryEntity.Longitude!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Longitude", Value = pQueryEntity.Longitude });
            if (pQueryEntity.CustomerMessage!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerMessage", Value = pQueryEntity.CustomerMessage });
            if (pQueryEntity.ProductOrderID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProductOrderID", Value = pQueryEntity.ProductOrderID });
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
        protected void Load(SqlDataReader pReader, out GLServiceOrderEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new GLServiceOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ServiceOrderID"] != DBNull.Value)
			{
				pInstance.ServiceOrderID =  Convert.ToString(pReader["ServiceOrderID"]);
			}
			if (pReader["ServiceType"] != DBNull.Value)
			{
				pInstance.ServiceType =   Convert.ToInt32(pReader["ServiceType"]);
			}
			if (pReader["ServiceDate"] != DBNull.Value)
			{
				pInstance.ServiceDate =  Convert.ToDateTime(pReader["ServiceDate"]);
			}
			if (pReader["ServiceDateEnd"] != DBNull.Value)
			{
				pInstance.ServiceDateEnd =  Convert.ToDateTime(pReader["ServiceDateEnd"]);
			}
			if (pReader["ServiceAddress"] != DBNull.Value)
			{
				pInstance.ServiceAddress =  Convert.ToString(pReader["ServiceAddress"]);
			}
			if (pReader["Latitude"] != DBNull.Value)
			{
				pInstance.Latitude =  Convert.ToDecimal(pReader["Latitude"]);
			}
			if (pReader["Longitude"] != DBNull.Value)
			{
				pInstance.Longitude =  Convert.ToDecimal(pReader["Longitude"]);
			}
			if (pReader["CustomerMessage"] != DBNull.Value)
			{
				pInstance.CustomerMessage =  Convert.ToString(pReader["CustomerMessage"]);
			}
			if (pReader["ProductOrderID"] != DBNull.Value)
			{
				pInstance.ProductOrderID =  Convert.ToString(pReader["ProductOrderID"]);
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
