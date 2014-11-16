/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 16:16:32
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
    /// ��EclubInfoCollect�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubInfoCollectDAO : Base.BaseCPOSDAO, ICRUDable<EclubInfoCollectEntity>, IQueryable<EclubInfoCollectEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EclubInfoCollectDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(EclubInfoCollectEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(EclubInfoCollectEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [EclubInfoCollect](");
            strSql.Append("[Intro],[Substance],[ClassInfoID],[Col1],[Col2],[Col3],[Col4],[Col5],[Col6],[Col7],[Col8],[Col9],[Col10],[Description],[Remark],[CustomerId],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[InfoCollectID])");
            strSql.Append(" values (");
            strSql.Append("@Intro,@Substance,@ClassInfoID,@Col1,@Col2,@Col3,@Col4,@Col5,@Col6,@Col7,@Col8,@Col9,@Col10,@Description,@Remark,@CustomerId,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@InfoCollectID)");            

			Guid? pkGuid;
			if (pEntity.InfoCollectID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.InfoCollectID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Substance",SqlDbType.NVarChar),
					new SqlParameter("@ClassInfoID",SqlDbType.NVarChar),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@InfoCollectID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.Intro;
			parameters[1].Value = pEntity.Substance;
			parameters[2].Value = pEntity.ClassInfoID;
			parameters[3].Value = pEntity.Col1;
			parameters[4].Value = pEntity.Col2;
			parameters[5].Value = pEntity.Col3;
			parameters[6].Value = pEntity.Col4;
			parameters[7].Value = pEntity.Col5;
			parameters[8].Value = pEntity.Col6;
			parameters[9].Value = pEntity.Col7;
			parameters[10].Value = pEntity.Col8;
			parameters[11].Value = pEntity.Col9;
			parameters[12].Value = pEntity.Col10;
			parameters[13].Value = pEntity.Description;
			parameters[14].Value = pEntity.Remark;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.CreateTime;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.IsDelete;
			parameters[21].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.InfoCollectID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public EclubInfoCollectEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EclubInfoCollect] where InfoCollectID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            EclubInfoCollectEntity m = null;
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
        public EclubInfoCollectEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EclubInfoCollect] where isdelete=0");
            //��ȡ����
            List<EclubInfoCollectEntity> list = new List<EclubInfoCollectEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    EclubInfoCollectEntity m;
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
        public void Update(EclubInfoCollectEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(EclubInfoCollectEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.InfoCollectID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [EclubInfoCollect] set ");
            if (pIsUpdateNullField || pEntity.Intro!=null)
                strSql.Append( "[Intro]=@Intro,");
            if (pIsUpdateNullField || pEntity.Substance!=null)
                strSql.Append( "[Substance]=@Substance,");
            if (pIsUpdateNullField || pEntity.ClassInfoID!=null)
                strSql.Append( "[ClassInfoID]=@ClassInfoID,");
            if (pIsUpdateNullField || pEntity.Col1!=null)
                strSql.Append( "[Col1]=@Col1,");
            if (pIsUpdateNullField || pEntity.Col2!=null)
                strSql.Append( "[Col2]=@Col2,");
            if (pIsUpdateNullField || pEntity.Col3!=null)
                strSql.Append( "[Col3]=@Col3,");
            if (pIsUpdateNullField || pEntity.Col4!=null)
                strSql.Append( "[Col4]=@Col4,");
            if (pIsUpdateNullField || pEntity.Col5!=null)
                strSql.Append( "[Col5]=@Col5,");
            if (pIsUpdateNullField || pEntity.Col6!=null)
                strSql.Append( "[Col6]=@Col6,");
            if (pIsUpdateNullField || pEntity.Col7!=null)
                strSql.Append( "[Col7]=@Col7,");
            if (pIsUpdateNullField || pEntity.Col8!=null)
                strSql.Append( "[Col8]=@Col8,");
            if (pIsUpdateNullField || pEntity.Col9!=null)
                strSql.Append( "[Col9]=@Col9,");
            if (pIsUpdateNullField || pEntity.Col10!=null)
                strSql.Append( "[Col10]=@Col10,");
            if (pIsUpdateNullField || pEntity.Description!=null)
                strSql.Append( "[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.Remark!=null)
                strSql.Append( "[Remark]=@Remark,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where InfoCollectID=@InfoCollectID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Substance",SqlDbType.NVarChar),
					new SqlParameter("@ClassInfoID",SqlDbType.NVarChar),
					new SqlParameter("@Col1",SqlDbType.NVarChar),
					new SqlParameter("@Col2",SqlDbType.NVarChar),
					new SqlParameter("@Col3",SqlDbType.NVarChar),
					new SqlParameter("@Col4",SqlDbType.NVarChar),
					new SqlParameter("@Col5",SqlDbType.NVarChar),
					new SqlParameter("@Col6",SqlDbType.NVarChar),
					new SqlParameter("@Col7",SqlDbType.NVarChar),
					new SqlParameter("@Col8",SqlDbType.NVarChar),
					new SqlParameter("@Col9",SqlDbType.NVarChar),
					new SqlParameter("@Col10",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Remark",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@InfoCollectID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.Intro;
			parameters[1].Value = pEntity.Substance;
			parameters[2].Value = pEntity.ClassInfoID;
			parameters[3].Value = pEntity.Col1;
			parameters[4].Value = pEntity.Col2;
			parameters[5].Value = pEntity.Col3;
			parameters[6].Value = pEntity.Col4;
			parameters[7].Value = pEntity.Col5;
			parameters[8].Value = pEntity.Col6;
			parameters[9].Value = pEntity.Col7;
			parameters[10].Value = pEntity.Col8;
			parameters[11].Value = pEntity.Col9;
			parameters[12].Value = pEntity.Col10;
			parameters[13].Value = pEntity.Description;
			parameters[14].Value = pEntity.Remark;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.InfoCollectID;

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
        public void Update(EclubInfoCollectEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(EclubInfoCollectEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(EclubInfoCollectEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(EclubInfoCollectEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.InfoCollectID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.InfoCollectID, pTran);           
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
            sql.AppendLine("update [EclubInfoCollect] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where InfoCollectID=@InfoCollectID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@InfoCollectID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(EclubInfoCollectEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.InfoCollectID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.InfoCollectID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(EclubInfoCollectEntity[] pEntities)
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
            sql.AppendLine("update [EclubInfoCollect] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where InfoCollectID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public EclubInfoCollectEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EclubInfoCollect] where isdelete=0 ");
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
            List<EclubInfoCollectEntity> list = new List<EclubInfoCollectEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    EclubInfoCollectEntity m;
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
        public PagedQueryResult<EclubInfoCollectEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [InfoCollectID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [EclubInfoCollect] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [EclubInfoCollect] where isdelete=0 ");
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
            PagedQueryResult<EclubInfoCollectEntity> result = new PagedQueryResult<EclubInfoCollectEntity>();
            List<EclubInfoCollectEntity> list = new List<EclubInfoCollectEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    EclubInfoCollectEntity m;
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
        public EclubInfoCollectEntity[] QueryByEntity(EclubInfoCollectEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<EclubInfoCollectEntity> PagedQueryByEntity(EclubInfoCollectEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(EclubInfoCollectEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.InfoCollectID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InfoCollectID", Value = pQueryEntity.InfoCollectID });
            if (pQueryEntity.Intro!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Intro", Value = pQueryEntity.Intro });
            if (pQueryEntity.Substance!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Substance", Value = pQueryEntity.Substance });
            if (pQueryEntity.ClassInfoID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ClassInfoID", Value = pQueryEntity.ClassInfoID });
            if (pQueryEntity.Col1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col1", Value = pQueryEntity.Col1 });
            if (pQueryEntity.Col2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col2", Value = pQueryEntity.Col2 });
            if (pQueryEntity.Col3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col3", Value = pQueryEntity.Col3 });
            if (pQueryEntity.Col4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col4", Value = pQueryEntity.Col4 });
            if (pQueryEntity.Col5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col5", Value = pQueryEntity.Col5 });
            if (pQueryEntity.Col6!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col6", Value = pQueryEntity.Col6 });
            if (pQueryEntity.Col7!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col7", Value = pQueryEntity.Col7 });
            if (pQueryEntity.Col8!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col8", Value = pQueryEntity.Col8 });
            if (pQueryEntity.Col9!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col9", Value = pQueryEntity.Col9 });
            if (pQueryEntity.Col10!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Col10", Value = pQueryEntity.Col10 });
            if (pQueryEntity.Description!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.Remark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Remark", Value = pQueryEntity.Remark });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
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
        protected void Load(SqlDataReader pReader, out EclubInfoCollectEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new EclubInfoCollectEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["InfoCollectID"] != DBNull.Value)
			{
				pInstance.InfoCollectID =  (Guid)pReader["InfoCollectID"];
			}
			if (pReader["Intro"] != DBNull.Value)
			{
				pInstance.Intro =  Convert.ToString(pReader["Intro"]);
			}
			if (pReader["Substance"] != DBNull.Value)
			{
				pInstance.Substance =  Convert.ToString(pReader["Substance"]);
			}
			if (pReader["ClassInfoID"] != DBNull.Value)
			{
				pInstance.ClassInfoID =  Convert.ToString(pReader["ClassInfoID"]);
			}
			if (pReader["Col1"] != DBNull.Value)
			{
				pInstance.Col1 =  Convert.ToString(pReader["Col1"]);
			}
			if (pReader["Col2"] != DBNull.Value)
			{
				pInstance.Col2 =  Convert.ToString(pReader["Col2"]);
			}
			if (pReader["Col3"] != DBNull.Value)
			{
				pInstance.Col3 =  Convert.ToString(pReader["Col3"]);
			}
			if (pReader["Col4"] != DBNull.Value)
			{
				pInstance.Col4 =  Convert.ToString(pReader["Col4"]);
			}
			if (pReader["Col5"] != DBNull.Value)
			{
				pInstance.Col5 =  Convert.ToString(pReader["Col5"]);
			}
			if (pReader["Col6"] != DBNull.Value)
			{
				pInstance.Col6 =  Convert.ToString(pReader["Col6"]);
			}
			if (pReader["Col7"] != DBNull.Value)
			{
				pInstance.Col7 =  Convert.ToString(pReader["Col7"]);
			}
			if (pReader["Col8"] != DBNull.Value)
			{
				pInstance.Col8 =  Convert.ToString(pReader["Col8"]);
			}
			if (pReader["Col9"] != DBNull.Value)
			{
				pInstance.Col9 =  Convert.ToString(pReader["Col9"]);
			}
			if (pReader["Col10"] != DBNull.Value)
			{
				pInstance.Col10 =  Convert.ToString(pReader["Col10"]);
			}
			if (pReader["Description"] != DBNull.Value)
			{
				pInstance.Description =  Convert.ToString(pReader["Description"]);
			}
			if (pReader["Remark"] != DBNull.Value)
			{
				pInstance.Remark =  Convert.ToString(pReader["Remark"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
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
