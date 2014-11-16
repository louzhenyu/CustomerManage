/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/18 13:28:57
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
    /// ��EclubMicro�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubMicroDAO : Base.BaseCPOSDAO, ICRUDable<EclubMicroEntity>, IQueryable<EclubMicroEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public EclubMicroDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(EclubMicroEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(EclubMicroEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [EclubMicro](");
            strSql.Append("[MicroTypeID],[MicroNumberID],[MicroTitle],[Content],[ContentUrl],[PublishTime],[ImageUrl],[ThumbnailImageUrl],[Source],[SourceUrl],[Intro],[Sequence],[Clicks],[Goods],[Shares],[CustomerId],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[MicroID])");
            strSql.Append(" values (");
            strSql.Append("@MicroTypeID,@MicroNumberID,@MicroTitle,@Content,@ContentUrl,@PublishTime,@ImageUrl,@ThumbnailImageUrl,@Source,@SourceUrl,@Intro,@Sequence,@Clicks,@Goods,@Shares,@CustomerId,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@MicroID)");            

			Guid? pkGuid;
			if (pEntity.MicroID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.MicroID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@MicroTypeID",SqlDbType.NVarChar),
					new SqlParameter("@MicroNumberID",SqlDbType.NVarChar),
					new SqlParameter("@MicroTitle",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@PublishTime",SqlDbType.DateTime),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ThumbnailImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Source",SqlDbType.NVarChar),
					new SqlParameter("@SourceUrl",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@Clicks",SqlDbType.Int),
					new SqlParameter("@Goods",SqlDbType.Int),
					new SqlParameter("@Shares",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@MicroID",SqlDbType.UniqueIdentifier)
            };
			parameters[-1].Value = pEntity.MicroTypeID;
			parameters[0].Value = pEntity.MicroNumberID;
			parameters[1].Value = pEntity.MicroTitle;
			parameters[2].Value = pEntity.Content;
			parameters[3].Value = pEntity.ContentUrl;
			parameters[4].Value = pEntity.PublishTime;
			parameters[5].Value = pEntity.ImageUrl;
			parameters[6].Value = pEntity.ThumbnailImageUrl;
			parameters[7].Value = pEntity.Source;
			parameters[8].Value = pEntity.SourceUrl;
			parameters[9].Value = pEntity.Intro;
			parameters[10].Value = pEntity.Sequence;
			parameters[11].Value = pEntity.Clicks;
			parameters[12].Value = pEntity.Goods;
			parameters[13].Value = pEntity.Shares;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pEntity.CreateBy;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.IsDelete;
			parameters[21].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.MicroID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public EclubMicroEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EclubMicro] where MicroID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            EclubMicroEntity m = null;
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
        public EclubMicroEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EclubMicro] where isdelete=0");
            //��ȡ����
            List<EclubMicroEntity> list = new List<EclubMicroEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    EclubMicroEntity m;
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
        public void Update(EclubMicroEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(EclubMicroEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MicroID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [EclubMicro] set ");
            if (pIsUpdateNullField || pEntity.MicroTypeID!=null)
                strSql.Append( "[MicroTypeID]=@MicroTypeID,");
            if (pIsUpdateNullField || pEntity.MicroNumberID!=null)
                strSql.Append( "[MicroNumberID]=@MicroNumberID,");
            if (pIsUpdateNullField || pEntity.MicroTitle!=null)
                strSql.Append( "[MicroTitle]=@MicroTitle,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.ContentUrl!=null)
                strSql.Append( "[ContentUrl]=@ContentUrl,");
            if (pIsUpdateNullField || pEntity.PublishTime!=null)
                strSql.Append( "[PublishTime]=@PublishTime,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.ThumbnailImageUrl!=null)
                strSql.Append( "[ThumbnailImageUrl]=@ThumbnailImageUrl,");
            if (pIsUpdateNullField || pEntity.Source!=null)
                strSql.Append( "[Source]=@Source,");
            if (pIsUpdateNullField || pEntity.SourceUrl!=null)
                strSql.Append( "[SourceUrl]=@SourceUrl,");
            if (pIsUpdateNullField || pEntity.Intro!=null)
                strSql.Append( "[Intro]=@Intro,");
            if (pIsUpdateNullField || pEntity.Sequence!=null)
                strSql.Append( "[Sequence]=@Sequence,");
            if (pIsUpdateNullField || pEntity.Clicks!=null)
                strSql.Append( "[Clicks]=@Clicks,");
            if (pIsUpdateNullField || pEntity.Goods!=null)
                strSql.Append( "[Goods]=@Goods,");
            if (pIsUpdateNullField || pEntity.Shares!=null)
                strSql.Append( "[Shares]=@Shares,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where MicroID=@MicroID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@MicroTypeID",SqlDbType.NVarChar),
					new SqlParameter("@MicroNumberID",SqlDbType.NVarChar),
					new SqlParameter("@MicroTitle",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@PublishTime",SqlDbType.DateTime),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ThumbnailImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@Source",SqlDbType.NVarChar),
					new SqlParameter("@SourceUrl",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Sequence",SqlDbType.Int),
					new SqlParameter("@Clicks",SqlDbType.Int),
					new SqlParameter("@Goods",SqlDbType.Int),
					new SqlParameter("@Shares",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@MicroID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.MicroTypeID;
			parameters[1].Value = pEntity.MicroNumberID;
			parameters[2].Value = pEntity.MicroTitle;
			parameters[3].Value = pEntity.Content;
			parameters[4].Value = pEntity.ContentUrl;
			parameters[5].Value = pEntity.PublishTime;
			parameters[6].Value = pEntity.ImageUrl;
			parameters[7].Value = pEntity.ThumbnailImageUrl;
			parameters[8].Value = pEntity.Source;
			parameters[9].Value = pEntity.SourceUrl;
			parameters[10].Value = pEntity.Intro;
			parameters[11].Value = pEntity.Sequence;
			parameters[12].Value = pEntity.Clicks;
			parameters[13].Value = pEntity.Goods;
			parameters[14].Value = pEntity.Shares;
			parameters[15].Value = pEntity.CustomerId;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.MicroID;

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
        public void Update(EclubMicroEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(EclubMicroEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(EclubMicroEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(EclubMicroEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MicroID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.MicroID, pTran);           
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
            sql.AppendLine("update [EclubMicro] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where MicroID=@MicroID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@MicroID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(EclubMicroEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.MicroID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.MicroID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(EclubMicroEntity[] pEntities)
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
            sql.AppendLine("update [EclubMicro] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where MicroID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public EclubMicroEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [EclubMicro] where isdelete=0 ");
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
            List<EclubMicroEntity> list = new List<EclubMicroEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    EclubMicroEntity m;
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
        public PagedQueryResult<EclubMicroEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [MicroID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [EclubMicro] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [EclubMicro] where isdelete=0 ");
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
            PagedQueryResult<EclubMicroEntity> result = new PagedQueryResult<EclubMicroEntity>();
            List<EclubMicroEntity> list = new List<EclubMicroEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    EclubMicroEntity m;
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
        public EclubMicroEntity[] QueryByEntity(EclubMicroEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<EclubMicroEntity> PagedQueryByEntity(EclubMicroEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(EclubMicroEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.MicroTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MicroTypeID", Value = pQueryEntity.MicroTypeID });
            if (pQueryEntity.MicroNumberID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MicroNumberID", Value = pQueryEntity.MicroNumberID });
            if (pQueryEntity.MicroTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MicroTitle", Value = pQueryEntity.MicroTitle });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.ContentUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContentUrl", Value = pQueryEntity.ContentUrl });
            if (pQueryEntity.PublishTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PublishTime", Value = pQueryEntity.PublishTime });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.ThumbnailImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThumbnailImageUrl", Value = pQueryEntity.ThumbnailImageUrl });
            if (pQueryEntity.Source!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Source", Value = pQueryEntity.Source });
            if (pQueryEntity.SourceUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SourceUrl", Value = pQueryEntity.SourceUrl });
            if (pQueryEntity.Intro!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Intro", Value = pQueryEntity.Intro });
            if (pQueryEntity.Sequence!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Sequence", Value = pQueryEntity.Sequence });
            if (pQueryEntity.Clicks!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Clicks", Value = pQueryEntity.Clicks });
            if (pQueryEntity.Goods!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Goods", Value = pQueryEntity.Goods });
            if (pQueryEntity.Shares!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Shares", Value = pQueryEntity.Shares });
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
            if (pQueryEntity.MicroID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MicroID", Value = pQueryEntity.MicroID });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out EclubMicroEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new EclubMicroEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["MicroTypeID"] != DBNull.Value)
			{
				pInstance.MicroTypeID =  Convert.ToString(pReader["MicroTypeID"]);
			}
			if (pReader["MicroNumberID"] != DBNull.Value)
			{
				pInstance.MicroNumberID =  Convert.ToString(pReader["MicroNumberID"]);
			}
			if (pReader["MicroTitle"] != DBNull.Value)
			{
				pInstance.MicroTitle =  Convert.ToString(pReader["MicroTitle"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["ContentUrl"] != DBNull.Value)
			{
				pInstance.ContentUrl =  Convert.ToString(pReader["ContentUrl"]);
			}
			if (pReader["PublishTime"] != DBNull.Value)
			{
				pInstance.PublishTime =  Convert.ToDateTime(pReader["PublishTime"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["ThumbnailImageUrl"] != DBNull.Value)
			{
				pInstance.ThumbnailImageUrl =  Convert.ToString(pReader["ThumbnailImageUrl"]);
			}
			if (pReader["Source"] != DBNull.Value)
			{
				pInstance.Source =  Convert.ToString(pReader["Source"]);
			}
			if (pReader["SourceUrl"] != DBNull.Value)
			{
				pInstance.SourceUrl =  Convert.ToString(pReader["SourceUrl"]);
			}
			if (pReader["Intro"] != DBNull.Value)
			{
				pInstance.Intro =  Convert.ToString(pReader["Intro"]);
			}
			if (pReader["Sequence"] != DBNull.Value)
			{
				pInstance.Sequence =   Convert.ToInt32(pReader["Sequence"]);
			}
			if (pReader["Clicks"] != DBNull.Value)
			{
				pInstance.Clicks =   Convert.ToInt32(pReader["Clicks"]);
			}
			if (pReader["Goods"] != DBNull.Value)
			{
				pInstance.Goods =   Convert.ToInt32(pReader["Goods"]);
			}
			if (pReader["Shares"] != DBNull.Value)
			{
				pInstance.Shares =   Convert.ToInt32(pReader["Shares"]);
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
			if (pReader["MicroID"] != DBNull.Value)
			{
				pInstance.MicroID =  (Guid)pReader["MicroID"];
			}

        }
        #endregion
    }
}
