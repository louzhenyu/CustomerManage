/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/7/6 16:12:44
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
    /// ��T_Sku�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_SkuDAO : Base.BaseCPOSDAO, ICRUDable<T_SkuEntity>, IQueryable<T_SkuEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_SkuDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_SkuEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_SkuEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [T_Sku](");
            strSql.Append("[item_id],[sku_prop_id1],[sku_prop_id2],[sku_prop_id3],[sku_prop_id4],[sku_prop_id5],[barcode],[status],[create_user_id],[create_time],[modify_user_id],[modify_time],[bat_id],[if_flag],[sku_id])");
            strSql.Append(" values (");
            strSql.Append("@item_id,@sku_prop_id1,@sku_prop_id2,@sku_prop_id3,@sku_prop_id4,@sku_prop_id5,@barcode,@status,@create_user_id,@create_time,@modify_user_id,@modify_time,@bat_id,@if_flag,@sku_id)");            

			string pkString = pEntity.sku_id;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@item_id",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id1",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id2",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id3",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id4",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id5",SqlDbType.NVarChar),
					new SqlParameter("@barcode",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@sku_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.item_id;
			parameters[1].Value = pEntity.sku_prop_id1;
			parameters[2].Value = pEntity.sku_prop_id2;
			parameters[3].Value = pEntity.sku_prop_id3;
			parameters[4].Value = pEntity.sku_prop_id4;
			parameters[5].Value = pEntity.sku_prop_id5;
			parameters[6].Value = pEntity.barcode;
			parameters[7].Value = pEntity.status;
			parameters[8].Value = pEntity.create_user_id;
			parameters[9].Value = pEntity.create_time;
			parameters[10].Value = pEntity.modify_user_id;
			parameters[11].Value = pEntity.modify_time;
			parameters[12].Value = pEntity.bat_id;
			parameters[13].Value = pEntity.if_flag;
			parameters[14].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.sku_id = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_SkuEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Sku] where sku_id='{0}'  and status<>'-1' ", id.ToString());
            //��ȡ����
            T_SkuEntity m = null;
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
        public T_SkuEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Sku] where 1=1  and status<>'-1'");
            //��ȡ����
            List<T_SkuEntity> list = new List<T_SkuEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SkuEntity m;
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
        public void Update(T_SkuEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_SkuEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.sku_id == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Sku] set ");
                        if (pIsUpdateNullField || pEntity.item_id!=null)
                strSql.Append( "[item_id]=@item_id,");
            if (pIsUpdateNullField || pEntity.sku_prop_id1!=null)
                strSql.Append( "[sku_prop_id1]=@sku_prop_id1,");
            if (pIsUpdateNullField || pEntity.sku_prop_id2!=null)
                strSql.Append( "[sku_prop_id2]=@sku_prop_id2,");
            if (pIsUpdateNullField || pEntity.sku_prop_id3!=null)
                strSql.Append( "[sku_prop_id3]=@sku_prop_id3,");
            if (pIsUpdateNullField || pEntity.sku_prop_id4!=null)
                strSql.Append( "[sku_prop_id4]=@sku_prop_id4,");
            if (pIsUpdateNullField || pEntity.sku_prop_id5!=null)
                strSql.Append( "[sku_prop_id5]=@sku_prop_id5,");
            if (pIsUpdateNullField || pEntity.barcode!=null)
                strSql.Append( "[barcode]=@barcode,");
            if (pIsUpdateNullField || pEntity.status!=null)
                strSql.Append( "[status]=@status,");
            if (pIsUpdateNullField || pEntity.create_user_id!=null)
                strSql.Append( "[create_user_id]=@create_user_id,");
            if (pIsUpdateNullField || pEntity.create_time!=null)
                strSql.Append( "[create_time]=@create_time,");
            if (pIsUpdateNullField || pEntity.modify_user_id!=null)
                strSql.Append( "[modify_user_id]=@modify_user_id,");
            if (pIsUpdateNullField || pEntity.modify_time!=null)
                strSql.Append( "[modify_time]=@modify_time,");
            if (pIsUpdateNullField || pEntity.bat_id!=null)
                strSql.Append( "[bat_id]=@bat_id,");
            if (pIsUpdateNullField || pEntity.if_flag!=null)
                strSql.Append( "[if_flag]=@if_flag");
            strSql.Append(" where sku_id=@sku_id ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@item_id",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id1",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id2",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id3",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id4",SqlDbType.NVarChar),
					new SqlParameter("@sku_prop_id5",SqlDbType.NVarChar),
					new SqlParameter("@barcode",SqlDbType.NVarChar),
					new SqlParameter("@status",SqlDbType.NVarChar),
					new SqlParameter("@create_user_id",SqlDbType.NVarChar),
					new SqlParameter("@create_time",SqlDbType.NVarChar),
					new SqlParameter("@modify_user_id",SqlDbType.NVarChar),
					new SqlParameter("@modify_time",SqlDbType.NVarChar),
					new SqlParameter("@bat_id",SqlDbType.NVarChar),
					new SqlParameter("@if_flag",SqlDbType.NVarChar),
					new SqlParameter("@sku_id",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.item_id;
			parameters[1].Value = pEntity.sku_prop_id1;
			parameters[2].Value = pEntity.sku_prop_id2;
			parameters[3].Value = pEntity.sku_prop_id3;
			parameters[4].Value = pEntity.sku_prop_id4;
			parameters[5].Value = pEntity.sku_prop_id5;
			parameters[6].Value = pEntity.barcode;
			parameters[7].Value = pEntity.status;
			parameters[8].Value = pEntity.create_user_id;
			parameters[9].Value = pEntity.create_time;
			parameters[10].Value = pEntity.modify_user_id;
			parameters[11].Value = pEntity.modify_time;
			parameters[12].Value = pEntity.bat_id;
			parameters[13].Value = pEntity.if_flag;
			parameters[14].Value = pEntity.sku_id;

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
        public void Update(T_SkuEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_SkuEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_SkuEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.sku_id == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.sku_id, pTran);           
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
            sql.AppendLine("update [T_Sku] set status='-1' where sku_id=@sku_id;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@sku_id",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_SkuEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.sku_id == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.sku_id;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_SkuEntity[] pEntities)
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
            sql.AppendLine("update [T_Sku] set status='-1' where sku_id in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_SkuEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Sku] where 1=1  and status<>'-1' ");
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
            List<T_SkuEntity> list = new List<T_SkuEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SkuEntity m;
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
        public PagedQueryResult<T_SkuEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [sku_id] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Sku] where 1=1  and status<>'-1' ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_Sku] where 1=1  and status<>'-1' ");
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
            PagedQueryResult<T_SkuEntity> result = new PagedQueryResult<T_SkuEntity>();
            List<T_SkuEntity> list = new List<T_SkuEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_SkuEntity m;
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
        public T_SkuEntity[] QueryByEntity(T_SkuEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_SkuEntity> PagedQueryByEntity(T_SkuEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_SkuEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.sku_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sku_id", Value = pQueryEntity.sku_id });
            if (pQueryEntity.item_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "item_id", Value = pQueryEntity.item_id });
            if (pQueryEntity.sku_prop_id1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sku_prop_id1", Value = pQueryEntity.sku_prop_id1 });
            if (pQueryEntity.sku_prop_id2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sku_prop_id2", Value = pQueryEntity.sku_prop_id2 });
            if (pQueryEntity.sku_prop_id3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sku_prop_id3", Value = pQueryEntity.sku_prop_id3 });
            if (pQueryEntity.sku_prop_id4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sku_prop_id4", Value = pQueryEntity.sku_prop_id4 });
            if (pQueryEntity.sku_prop_id5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "sku_prop_id5", Value = pQueryEntity.sku_prop_id5 });
            if (pQueryEntity.barcode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "barcode", Value = pQueryEntity.barcode });
            if (pQueryEntity.status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "status", Value = pQueryEntity.status });
            if (pQueryEntity.create_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_user_id", Value = pQueryEntity.create_user_id });
            if (pQueryEntity.create_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "create_time", Value = pQueryEntity.create_time });
            if (pQueryEntity.modify_user_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_user_id", Value = pQueryEntity.modify_user_id });
            if (pQueryEntity.modify_time!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "modify_time", Value = pQueryEntity.modify_time });
            if (pQueryEntity.bat_id!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "bat_id", Value = pQueryEntity.bat_id });
            if (pQueryEntity.if_flag!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "if_flag", Value = pQueryEntity.if_flag });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_SkuEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_SkuEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["sku_id"] != DBNull.Value)
			{
				pInstance.sku_id =  Convert.ToString(pReader["sku_id"]);
			}
			if (pReader["item_id"] != DBNull.Value)
			{
				pInstance.item_id =  Convert.ToString(pReader["item_id"]);
			}
			if (pReader["sku_prop_id1"] != DBNull.Value)
			{
				pInstance.sku_prop_id1 =  Convert.ToString(pReader["sku_prop_id1"]);
			}
			if (pReader["sku_prop_id2"] != DBNull.Value)
			{
				pInstance.sku_prop_id2 =  Convert.ToString(pReader["sku_prop_id2"]);
			}
			if (pReader["sku_prop_id3"] != DBNull.Value)
			{
				pInstance.sku_prop_id3 =  Convert.ToString(pReader["sku_prop_id3"]);
			}
			if (pReader["sku_prop_id4"] != DBNull.Value)
			{
				pInstance.sku_prop_id4 =  Convert.ToString(pReader["sku_prop_id4"]);
			}
			if (pReader["sku_prop_id5"] != DBNull.Value)
			{
				pInstance.sku_prop_id5 =  Convert.ToString(pReader["sku_prop_id5"]);
			}
			if (pReader["barcode"] != DBNull.Value)
			{
				pInstance.barcode =  Convert.ToString(pReader["barcode"]);
			}
			if (pReader["status"] != DBNull.Value)
			{
				pInstance.status =  Convert.ToString(pReader["status"]);
			}
			if (pReader["create_user_id"] != DBNull.Value)
			{
				pInstance.create_user_id =  Convert.ToString(pReader["create_user_id"]);
			}
			if (pReader["create_time"] != DBNull.Value)
			{
				pInstance.create_time =  Convert.ToString(pReader["create_time"]);
			}
			if (pReader["modify_user_id"] != DBNull.Value)
			{
				pInstance.modify_user_id =  Convert.ToString(pReader["modify_user_id"]);
			}
			if (pReader["modify_time"] != DBNull.Value)
			{
				pInstance.modify_time =  Convert.ToString(pReader["modify_time"]);
			}
			if (pReader["bat_id"] != DBNull.Value)
			{
				pInstance.bat_id =  Convert.ToString(pReader["bat_id"]);
			}
			if (pReader["if_flag"] != DBNull.Value)
			{
				pInstance.if_flag =  Convert.ToString(pReader["if_flag"]);
			}

        }
        #endregion
    }
}
