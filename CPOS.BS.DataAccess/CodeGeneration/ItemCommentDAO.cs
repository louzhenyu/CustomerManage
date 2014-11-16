/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:33
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
    /// ��ItemComment�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ItemCommentDAO : Base.BaseCPOSDAO, ICRUDable<ItemCommentEntity>, IQueryable<ItemCommentEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public ItemCommentDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(ItemCommentEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(ItemCommentEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [ItemComment](");
            strSql.Append("[ItemId],[CustomerId],[CommentType],[CommentImageUrl],[CommentVideoUrl],[CommentContent],[CommentTime],[CommentUserName],[CommentUserImageUrl],[IsVip],[VipId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ObjectId],[ProductMatch],[ItemType],[Topic],[Star],[ItemCommentId])");
            strSql.Append(" values (");
            strSql.Append("@ItemId,@CustomerId,@CommentType,@CommentImageUrl,@CommentVideoUrl,@CommentContent,@CommentTime,@CommentUserName,@CommentUserImageUrl,@IsVip,@VipId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ObjectId,@ProductMatch,@ItemType,@Topic,@Star,@ItemCommentId)");            

			string pkString = pEntity.ItemCommentId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@CommentType",SqlDbType.Int),
					new SqlParameter("@CommentImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@CommentVideoUrl",SqlDbType.NVarChar),
					new SqlParameter("@CommentContent",SqlDbType.NVarChar),
					new SqlParameter("@CommentTime",SqlDbType.DateTime),
					new SqlParameter("@CommentUserName",SqlDbType.NVarChar),
					new SqlParameter("@CommentUserImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@IsVip",SqlDbType.Int),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ProductMatch",SqlDbType.Int),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@Topic",SqlDbType.NVarChar),
					new SqlParameter("@Star",SqlDbType.Int),
					new SqlParameter("@ItemCommentId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ItemId;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.CommentType;
			parameters[3].Value = pEntity.CommentImageUrl;
			parameters[4].Value = pEntity.CommentVideoUrl;
			parameters[5].Value = pEntity.CommentContent;
			parameters[6].Value = pEntity.CommentTime;
			parameters[7].Value = pEntity.CommentUserName;
			parameters[8].Value = pEntity.CommentUserImageUrl;
			parameters[9].Value = pEntity.IsVip;
			parameters[10].Value = pEntity.VipId;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.ObjectId;
			parameters[17].Value = pEntity.ProductMatch;
			parameters[18].Value = pEntity.ItemType;
			parameters[19].Value = pEntity.Topic;
			parameters[20].Value = pEntity.Star;
			parameters[21].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ItemCommentId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public ItemCommentEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ItemComment] where ItemCommentId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            ItemCommentEntity m = null;
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
        public ItemCommentEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ItemComment] where isdelete=0");
            //��ȡ����
            List<ItemCommentEntity> list = new List<ItemCommentEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ItemCommentEntity m;
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
        public void Update(ItemCommentEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(ItemCommentEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ItemCommentId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ItemComment] set ");
            if (pIsUpdateNullField || pEntity.ItemId!=null)
                strSql.Append( "[ItemId]=@ItemId,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.CommentType!=null)
                strSql.Append( "[CommentType]=@CommentType,");
            if (pIsUpdateNullField || pEntity.CommentImageUrl!=null)
                strSql.Append( "[CommentImageUrl]=@CommentImageUrl,");
            if (pIsUpdateNullField || pEntity.CommentVideoUrl!=null)
                strSql.Append( "[CommentVideoUrl]=@CommentVideoUrl,");
            if (pIsUpdateNullField || pEntity.CommentContent!=null)
                strSql.Append( "[CommentContent]=@CommentContent,");
            if (pIsUpdateNullField || pEntity.CommentTime!=null)
                strSql.Append( "[CommentTime]=@CommentTime,");
            if (pIsUpdateNullField || pEntity.CommentUserName!=null)
                strSql.Append( "[CommentUserName]=@CommentUserName,");
            if (pIsUpdateNullField || pEntity.CommentUserImageUrl!=null)
                strSql.Append( "[CommentUserImageUrl]=@CommentUserImageUrl,");
            if (pIsUpdateNullField || pEntity.IsVip!=null)
                strSql.Append( "[IsVip]=@IsVip,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.ObjectId!=null)
                strSql.Append( "[ObjectId]=@ObjectId,");
            if (pIsUpdateNullField || pEntity.ProductMatch!=null)
                strSql.Append( "[ProductMatch]=@ProductMatch,");
            if (pIsUpdateNullField || pEntity.ItemType!=null)
                strSql.Append( "[ItemType]=@ItemType,");
            if (pIsUpdateNullField || pEntity.Topic!=null)
                strSql.Append( "[Topic]=@Topic,");
            if (pIsUpdateNullField || pEntity.Star!=null)
                strSql.Append( "[Star]=@Star");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ItemCommentId=@ItemCommentId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ItemId",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@CommentType",SqlDbType.Int),
					new SqlParameter("@CommentImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@CommentVideoUrl",SqlDbType.NVarChar),
					new SqlParameter("@CommentContent",SqlDbType.NVarChar),
					new SqlParameter("@CommentTime",SqlDbType.DateTime),
					new SqlParameter("@CommentUserName",SqlDbType.NVarChar),
					new SqlParameter("@CommentUserImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@IsVip",SqlDbType.Int),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ObjectId",SqlDbType.NVarChar),
					new SqlParameter("@ProductMatch",SqlDbType.Int),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@Topic",SqlDbType.NVarChar),
					new SqlParameter("@Star",SqlDbType.Int),
					new SqlParameter("@ItemCommentId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ItemId;
			parameters[1].Value = pEntity.CustomerId;
			parameters[2].Value = pEntity.CommentType;
			parameters[3].Value = pEntity.CommentImageUrl;
			parameters[4].Value = pEntity.CommentVideoUrl;
			parameters[5].Value = pEntity.CommentContent;
			parameters[6].Value = pEntity.CommentTime;
			parameters[7].Value = pEntity.CommentUserName;
			parameters[8].Value = pEntity.CommentUserImageUrl;
			parameters[9].Value = pEntity.IsVip;
			parameters[10].Value = pEntity.VipId;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.ObjectId;
			parameters[14].Value = pEntity.ProductMatch;
			parameters[15].Value = pEntity.ItemType;
			parameters[16].Value = pEntity.Topic;
			parameters[17].Value = pEntity.Star;
			parameters[18].Value = pEntity.ItemCommentId;

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
        public void Update(ItemCommentEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(ItemCommentEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(ItemCommentEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(ItemCommentEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ItemCommentId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ItemCommentId, pTran);           
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
            sql.AppendLine("update [ItemComment] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ItemCommentId=@ItemCommentId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ItemCommentId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(ItemCommentEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ItemCommentId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ItemCommentId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(ItemCommentEntity[] pEntities)
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
            sql.AppendLine("update [ItemComment] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ItemCommentId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public ItemCommentEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [ItemComment] where isdelete=0 ");
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
            List<ItemCommentEntity> list = new List<ItemCommentEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    ItemCommentEntity m;
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
        public PagedQueryResult<ItemCommentEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ItemCommentId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [ItemComment] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [ItemComment] where isdelete=0 ");
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
            PagedQueryResult<ItemCommentEntity> result = new PagedQueryResult<ItemCommentEntity>();
            List<ItemCommentEntity> list = new List<ItemCommentEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    ItemCommentEntity m;
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
        public ItemCommentEntity[] QueryByEntity(ItemCommentEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<ItemCommentEntity> PagedQueryByEntity(ItemCommentEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(ItemCommentEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ItemCommentId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemCommentId", Value = pQueryEntity.ItemCommentId });
            if (pQueryEntity.ItemId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemId", Value = pQueryEntity.ItemId });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.CommentType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommentType", Value = pQueryEntity.CommentType });
            if (pQueryEntity.CommentImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommentImageUrl", Value = pQueryEntity.CommentImageUrl });
            if (pQueryEntity.CommentVideoUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommentVideoUrl", Value = pQueryEntity.CommentVideoUrl });
            if (pQueryEntity.CommentContent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommentContent", Value = pQueryEntity.CommentContent });
            if (pQueryEntity.CommentTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommentTime", Value = pQueryEntity.CommentTime });
            if (pQueryEntity.CommentUserName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommentUserName", Value = pQueryEntity.CommentUserName });
            if (pQueryEntity.CommentUserImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CommentUserImageUrl", Value = pQueryEntity.CommentUserImageUrl });
            if (pQueryEntity.IsVip!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsVip", Value = pQueryEntity.IsVip });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
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
            if (pQueryEntity.ObjectId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ObjectId", Value = pQueryEntity.ObjectId });
            if (pQueryEntity.ProductMatch!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProductMatch", Value = pQueryEntity.ProductMatch });
            if (pQueryEntity.ItemType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemType", Value = pQueryEntity.ItemType });
            if (pQueryEntity.Topic!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Topic", Value = pQueryEntity.Topic });
            if (pQueryEntity.Star!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Star", Value = pQueryEntity.Star });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out ItemCommentEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new ItemCommentEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ItemCommentId"] != DBNull.Value)
			{
				pInstance.ItemCommentId =  Convert.ToString(pReader["ItemCommentId"]);
			}
			if (pReader["ItemId"] != DBNull.Value)
			{
				pInstance.ItemId =  Convert.ToString(pReader["ItemId"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["CommentType"] != DBNull.Value)
			{
				pInstance.CommentType =   Convert.ToInt32(pReader["CommentType"]);
			}
			if (pReader["CommentImageUrl"] != DBNull.Value)
			{
				pInstance.CommentImageUrl =  Convert.ToString(pReader["CommentImageUrl"]);
			}
			if (pReader["CommentVideoUrl"] != DBNull.Value)
			{
				pInstance.CommentVideoUrl =  Convert.ToString(pReader["CommentVideoUrl"]);
			}
			if (pReader["CommentContent"] != DBNull.Value)
			{
				pInstance.CommentContent =  Convert.ToString(pReader["CommentContent"]);
			}
			if (pReader["CommentTime"] != DBNull.Value)
			{
				pInstance.CommentTime =  Convert.ToDateTime(pReader["CommentTime"]);
			}
			if (pReader["CommentUserName"] != DBNull.Value)
			{
				pInstance.CommentUserName =  Convert.ToString(pReader["CommentUserName"]);
			}
			if (pReader["CommentUserImageUrl"] != DBNull.Value)
			{
				pInstance.CommentUserImageUrl =  Convert.ToString(pReader["CommentUserImageUrl"]);
			}
			if (pReader["IsVip"] != DBNull.Value)
			{
				pInstance.IsVip =   Convert.ToInt32(pReader["IsVip"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
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
			if (pReader["ObjectId"] != DBNull.Value)
			{
				pInstance.ObjectId =  Convert.ToString(pReader["ObjectId"]);
			}
			if (pReader["ProductMatch"] != DBNull.Value)
			{
				pInstance.ProductMatch =   Convert.ToInt32(pReader["ProductMatch"]);
			}
			if (pReader["ItemType"] != DBNull.Value)
			{
				pInstance.ItemType =   Convert.ToInt32(pReader["ItemType"]);
			}
			if (pReader["Topic"] != DBNull.Value)
			{
				pInstance.Topic =  Convert.ToString(pReader["Topic"]);
			}
			if (pReader["Star"] != DBNull.Value)
			{
				pInstance.Star =   Convert.ToInt32(pReader["Star"]);
			}

        }
        #endregion
    }
}
