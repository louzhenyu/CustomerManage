/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/4/22 18:54:19
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
    /// ��LNews�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LNewsDAO : Base.BaseCPOSDAO, ICRUDable<LNewsEntity>, IQueryable<LNewsEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LNewsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(LNewsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(LNewsEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [LNews](");
            strSql.Append("[NewsType],[NewsTitle],[NewsSubTitle],[Intro],[Content],[PublishTime],[ContentUrl],[ImageUrl],[ThumbnailImageUrl],[APPId],[NewsLevel],[ParentNewsId],[IsDefault],[IsTop],[Author],[BrowseCount],[PraiseCount],[CollCount],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[DisplayIndex],[NewsId])");
            strSql.Append(" values (");
            strSql.Append("@NewsType,@NewsTitle,@NewsSubTitle,@Intro,@Content,@PublishTime,@ContentUrl,@ImageUrl,@ThumbnailImageUrl,@APPId,@NewsLevel,@ParentNewsId,@IsDefault,@IsTop,@Author,@BrowseCount,@PraiseCount,@CollCount,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@DisplayIndex,@NewsId)");            

			string pkString = pEntity.NewsId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@NewsType",SqlDbType.NVarChar),
					new SqlParameter("@NewsTitle",SqlDbType.NVarChar),
					new SqlParameter("@NewsSubTitle",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PublishTime",SqlDbType.DateTime),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ThumbnailImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@APPId",SqlDbType.NVarChar),
					new SqlParameter("@NewsLevel",SqlDbType.Int),
					new SqlParameter("@ParentNewsId",SqlDbType.NVarChar),
					new SqlParameter("@IsDefault",SqlDbType.Int),
					new SqlParameter("@IsTop",SqlDbType.Int),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@BrowseCount",SqlDbType.Int),
					new SqlParameter("@PraiseCount",SqlDbType.Int),
					new SqlParameter("@CollCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@NewsId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.NewsType;
			parameters[1].Value = pEntity.NewsTitle;
			parameters[2].Value = pEntity.NewsSubTitle;
			parameters[3].Value = pEntity.Intro;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.PublishTime;
			parameters[6].Value = pEntity.ContentUrl;
			parameters[7].Value = pEntity.ImageUrl;
			parameters[8].Value = pEntity.ThumbnailImageUrl;
			parameters[9].Value = pEntity.APPId;
			parameters[10].Value = pEntity.NewsLevel;
			parameters[11].Value = pEntity.ParentNewsId;
			parameters[12].Value = pEntity.IsDefault;
			parameters[13].Value = pEntity.IsTop;
			parameters[14].Value = pEntity.Author;
			parameters[15].Value = pEntity.BrowseCount;
			parameters[16].Value = pEntity.PraiseCount;
			parameters[17].Value = pEntity.CollCount;
			parameters[18].Value = pEntity.CreateTime;
			parameters[19].Value = pEntity.CreateBy;
			parameters[20].Value = pEntity.LastUpdateBy;
			parameters[21].Value = pEntity.LastUpdateTime;
			parameters[22].Value = pEntity.IsDelete;
			parameters[23].Value = pEntity.CustomerId;
			parameters[24].Value = pEntity.DisplayIndex;
			parameters[25].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.NewsId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public LNewsEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LNews] where NewsId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            LNewsEntity m = null;
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
        public LNewsEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LNews] where isdelete=0");
            //��ȡ����
            List<LNewsEntity> list = new List<LNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LNewsEntity m;
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
        public void Update(LNewsEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LNewsEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LNews] set ");
            if (pIsUpdateNullField || pEntity.NewsType!=null)
                strSql.Append( "[NewsType]=@NewsType,");
            if (pIsUpdateNullField || pEntity.NewsTitle!=null)
                strSql.Append( "[NewsTitle]=@NewsTitle,");
            if (pIsUpdateNullField || pEntity.NewsSubTitle!=null)
                strSql.Append( "[NewsSubTitle]=@NewsSubTitle,");
            if (pIsUpdateNullField || pEntity.Intro!=null)
                strSql.Append( "[Intro]=@Intro,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.PublishTime!=null)
                strSql.Append( "[PublishTime]=@PublishTime,");
            if (pIsUpdateNullField || pEntity.ContentUrl!=null)
                strSql.Append( "[ContentUrl]=@ContentUrl,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.ThumbnailImageUrl!=null)
                strSql.Append( "[ThumbnailImageUrl]=@ThumbnailImageUrl,");
            if (pIsUpdateNullField || pEntity.APPId!=null)
                strSql.Append( "[APPId]=@APPId,");
            if (pIsUpdateNullField || pEntity.NewsLevel!=null)
                strSql.Append( "[NewsLevel]=@NewsLevel,");
            if (pIsUpdateNullField || pEntity.ParentNewsId!=null)
                strSql.Append( "[ParentNewsId]=@ParentNewsId,");
            if (pIsUpdateNullField || pEntity.IsDefault!=null)
                strSql.Append( "[IsDefault]=@IsDefault,");
            if (pIsUpdateNullField || pEntity.IsTop!=null)
                strSql.Append( "[IsTop]=@IsTop,");
            if (pIsUpdateNullField || pEntity.Author!=null)
                strSql.Append( "[Author]=@Author,");
            if (pIsUpdateNullField || pEntity.BrowseCount!=null)
                strSql.Append( "[BrowseCount]=@BrowseCount,");
            if (pIsUpdateNullField || pEntity.PraiseCount!=null)
                strSql.Append( "[PraiseCount]=@PraiseCount,");
            if (pIsUpdateNullField || pEntity.CollCount!=null)
                strSql.Append( "[CollCount]=@CollCount,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.DisplayIndex!=null)
                strSql.Append( "[DisplayIndex]=@DisplayIndex");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where NewsId=@NewsId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@NewsType",SqlDbType.NVarChar),
					new SqlParameter("@NewsTitle",SqlDbType.NVarChar),
					new SqlParameter("@NewsSubTitle",SqlDbType.NVarChar),
					new SqlParameter("@Intro",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PublishTime",SqlDbType.DateTime),
					new SqlParameter("@ContentUrl",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@ThumbnailImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@APPId",SqlDbType.NVarChar),
					new SqlParameter("@NewsLevel",SqlDbType.Int),
					new SqlParameter("@ParentNewsId",SqlDbType.NVarChar),
					new SqlParameter("@IsDefault",SqlDbType.Int),
					new SqlParameter("@IsTop",SqlDbType.Int),
					new SqlParameter("@Author",SqlDbType.NVarChar),
					new SqlParameter("@BrowseCount",SqlDbType.Int),
					new SqlParameter("@PraiseCount",SqlDbType.Int),
					new SqlParameter("@CollCount",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@DisplayIndex",SqlDbType.Int),
					new SqlParameter("@NewsId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.NewsType;
			parameters[1].Value = pEntity.NewsTitle;
			parameters[2].Value = pEntity.NewsSubTitle;
			parameters[3].Value = pEntity.Intro;
			parameters[4].Value = pEntity.Content;
			parameters[5].Value = pEntity.PublishTime;
			parameters[6].Value = pEntity.ContentUrl;
			parameters[7].Value = pEntity.ImageUrl;
			parameters[8].Value = pEntity.ThumbnailImageUrl;
			parameters[9].Value = pEntity.APPId;
			parameters[10].Value = pEntity.NewsLevel;
			parameters[11].Value = pEntity.ParentNewsId;
			parameters[12].Value = pEntity.IsDefault;
			parameters[13].Value = pEntity.IsTop;
			parameters[14].Value = pEntity.Author;
			parameters[15].Value = pEntity.BrowseCount;
			parameters[16].Value = pEntity.PraiseCount;
			parameters[17].Value = pEntity.CollCount;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.CustomerId;
			parameters[21].Value = pEntity.DisplayIndex;
			parameters[22].Value = pEntity.NewsId;

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
        public void Update(LNewsEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LNewsEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LNewsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(LNewsEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.NewsId, pTran);           
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
            sql.AppendLine("update [LNews] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where NewsId=@NewsId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@NewsId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LNewsEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.NewsId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.NewsId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(LNewsEntity[] pEntities)
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
            sql.AppendLine("update [LNews] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where NewsId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LNewsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LNews] where isdelete=0 ");
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
            List<LNewsEntity> list = new List<LNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LNewsEntity m;
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
        public PagedQueryResult<LNewsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [NewsId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [LNews] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [LNews] where isdelete=0 ");
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
            PagedQueryResult<LNewsEntity> result = new PagedQueryResult<LNewsEntity>();
            List<LNewsEntity> list = new List<LNewsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LNewsEntity m;
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
        public LNewsEntity[] QueryByEntity(LNewsEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LNewsEntity> PagedQueryByEntity(LNewsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LNewsEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.NewsId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsId", Value = pQueryEntity.NewsId });
            if (pQueryEntity.NewsType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsType", Value = pQueryEntity.NewsType });
            if (pQueryEntity.NewsTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsTitle", Value = pQueryEntity.NewsTitle });
            if (pQueryEntity.NewsSubTitle!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsSubTitle", Value = pQueryEntity.NewsSubTitle });
            if (pQueryEntity.Intro!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Intro", Value = pQueryEntity.Intro });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.PublishTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PublishTime", Value = pQueryEntity.PublishTime });
            if (pQueryEntity.ContentUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ContentUrl", Value = pQueryEntity.ContentUrl });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.ThumbnailImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThumbnailImageUrl", Value = pQueryEntity.ThumbnailImageUrl });
            if (pQueryEntity.APPId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "APPId", Value = pQueryEntity.APPId });
            if (pQueryEntity.NewsLevel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsLevel", Value = pQueryEntity.NewsLevel });
            if (pQueryEntity.ParentNewsId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentNewsId", Value = pQueryEntity.ParentNewsId });
            if (pQueryEntity.IsDefault!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDefault", Value = pQueryEntity.IsDefault });
            if (pQueryEntity.IsTop!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTop", Value = pQueryEntity.IsTop });
            if (pQueryEntity.Author!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Author", Value = pQueryEntity.Author });
            if (pQueryEntity.BrowseCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BrowseCount", Value = pQueryEntity.BrowseCount });
            if (pQueryEntity.PraiseCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PraiseCount", Value = pQueryEntity.PraiseCount });
            if (pQueryEntity.CollCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CollCount", Value = pQueryEntity.CollCount });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.DisplayIndex!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DisplayIndex", Value = pQueryEntity.DisplayIndex });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out LNewsEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new LNewsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["NewsId"] != DBNull.Value)
			{
				pInstance.NewsId =  Convert.ToString(pReader["NewsId"]);
			}
			if (pReader["NewsType"] != DBNull.Value)
			{
				pInstance.NewsType =  Convert.ToString(pReader["NewsType"]);
			}
			if (pReader["NewsTitle"] != DBNull.Value)
			{
				pInstance.NewsTitle =  Convert.ToString(pReader["NewsTitle"]);
			}
			if (pReader["NewsSubTitle"] != DBNull.Value)
			{
				pInstance.NewsSubTitle =  Convert.ToString(pReader["NewsSubTitle"]);
			}
			if (pReader["Intro"] != DBNull.Value)
			{
				pInstance.Intro =  Convert.ToString(pReader["Intro"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["PublishTime"] != DBNull.Value)
			{
				pInstance.PublishTime =  Convert.ToDateTime(pReader["PublishTime"]);
			}
			if (pReader["ContentUrl"] != DBNull.Value)
			{
				pInstance.ContentUrl =  Convert.ToString(pReader["ContentUrl"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["ThumbnailImageUrl"] != DBNull.Value)
			{
				pInstance.ThumbnailImageUrl =  Convert.ToString(pReader["ThumbnailImageUrl"]);
			}
			if (pReader["APPId"] != DBNull.Value)
			{
				pInstance.APPId =  Convert.ToString(pReader["APPId"]);
			}
			if (pReader["NewsLevel"] != DBNull.Value)
			{
				pInstance.NewsLevel =   Convert.ToInt32(pReader["NewsLevel"]);
			}
			if (pReader["ParentNewsId"] != DBNull.Value)
			{
				pInstance.ParentNewsId =  Convert.ToString(pReader["ParentNewsId"]);
			}
			if (pReader["IsDefault"] != DBNull.Value)
			{
				pInstance.IsDefault =   Convert.ToInt32(pReader["IsDefault"]);
			}
			if (pReader["IsTop"] != DBNull.Value)
			{
				pInstance.IsTop =   Convert.ToInt32(pReader["IsTop"]);
			}
			if (pReader["Author"] != DBNull.Value)
			{
				pInstance.Author =  Convert.ToString(pReader["Author"]);
			}
			if (pReader["BrowseCount"] != DBNull.Value)
			{
				pInstance.BrowseCount =   Convert.ToInt32(pReader["BrowseCount"]);
			}
			if (pReader["PraiseCount"] != DBNull.Value)
			{
				pInstance.PraiseCount =   Convert.ToInt32(pReader["PraiseCount"]);
			}
			if (pReader["CollCount"] != DBNull.Value)
			{
				pInstance.CollCount =   Convert.ToInt32(pReader["CollCount"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["DisplayIndex"] != DBNull.Value)
			{
				pInstance.DisplayIndex =   Convert.ToInt32(pReader["DisplayIndex"]);
			}

        }
        #endregion
    }
}
