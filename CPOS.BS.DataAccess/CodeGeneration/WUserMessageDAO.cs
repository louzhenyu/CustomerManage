/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/13 15:34:22
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
    /// ��WUserMessage�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WUserMessageDAO : Base.BaseCPOSDAO, ICRUDable<WUserMessageEntity>, IQueryable<WUserMessageEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WUserMessageDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WUserMessageEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WUserMessageEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WUserMessage](");
            strSql.Append("[VipId],[MaterialTypeId],[Text],[ImageUrl],[VoiceUrl],[VideoUrl],[OpenId],[WeiXinId],[ParentMessageId],[DataFrom],[IsPushWX],[IsPushSuccess],[FailureReason],[PushWXTime],[Title],[OriUrl],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ToVipType],[MessageId])");
            strSql.Append(" values (");
            strSql.Append("@VipId,@MaterialTypeId,@Text,@ImageUrl,@VoiceUrl,@VideoUrl,@OpenId,@WeiXinId,@ParentMessageId,@DataFrom,@IsPushWX,@IsPushSuccess,@FailureReason,@PushWXTime,@Title,@OriUrl,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ToVipType,@MessageId)");            

			string pkString = pEntity.MessageId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@MaterialTypeId",SqlDbType.NVarChar),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@VoiceUrl",SqlDbType.NVarChar),
					new SqlParameter("@VideoUrl",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinId",SqlDbType.NVarChar),
					new SqlParameter("@ParentMessageId",SqlDbType.NVarChar),
					new SqlParameter("@DataFrom",SqlDbType.Int),
					new SqlParameter("@IsPushWX",SqlDbType.Int),
					new SqlParameter("@IsPushSuccess",SqlDbType.Int),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@PushWXTime",SqlDbType.DateTime),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@OriUrl",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ToVipType",SqlDbType.Int),
					new SqlParameter("@MessageId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipId;
			parameters[1].Value = pEntity.MaterialTypeId;
			parameters[2].Value = pEntity.Text;
			parameters[3].Value = pEntity.ImageUrl;
			parameters[4].Value = pEntity.VoiceUrl;
			parameters[5].Value = pEntity.VideoUrl;
			parameters[6].Value = pEntity.OpenId;
			parameters[7].Value = pEntity.WeiXinId;
			parameters[8].Value = pEntity.ParentMessageId;
			parameters[9].Value = pEntity.DataFrom;
			parameters[10].Value = pEntity.IsPushWX;
			parameters[11].Value = pEntity.IsPushSuccess;
			parameters[12].Value = pEntity.FailureReason;
			parameters[13].Value = pEntity.PushWXTime;
			parameters[14].Value = pEntity.Title;
			parameters[15].Value = pEntity.OriUrl;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.IsDelete;
			parameters[21].Value = pEntity.ToVipType;
			parameters[22].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.MessageId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WUserMessageEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WUserMessage] where MessageId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WUserMessageEntity m = null;
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
        public WUserMessageEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WUserMessage] where isdelete=0");
            //��ȡ����
            List<WUserMessageEntity> list = new List<WUserMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WUserMessageEntity m;
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
        public void Update(WUserMessageEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WUserMessageEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MessageId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WUserMessage] set ");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.MaterialTypeId!=null)
                strSql.Append( "[MaterialTypeId]=@MaterialTypeId,");
            if (pIsUpdateNullField || pEntity.Text!=null)
                strSql.Append( "[Text]=@Text,");
            if (pIsUpdateNullField || pEntity.ImageUrl!=null)
                strSql.Append( "[ImageUrl]=@ImageUrl,");
            if (pIsUpdateNullField || pEntity.VoiceUrl!=null)
                strSql.Append( "[VoiceUrl]=@VoiceUrl,");
            if (pIsUpdateNullField || pEntity.VideoUrl!=null)
                strSql.Append( "[VideoUrl]=@VideoUrl,");
            if (pIsUpdateNullField || pEntity.OpenId!=null)
                strSql.Append( "[OpenId]=@OpenId,");
            if (pIsUpdateNullField || pEntity.WeiXinId!=null)
                strSql.Append( "[WeiXinId]=@WeiXinId,");
            if (pIsUpdateNullField || pEntity.ParentMessageId!=null)
                strSql.Append( "[ParentMessageId]=@ParentMessageId,");
            if (pIsUpdateNullField || pEntity.DataFrom!=null)
                strSql.Append( "[DataFrom]=@DataFrom,");
            if (pIsUpdateNullField || pEntity.IsPushWX!=null)
                strSql.Append( "[IsPushWX]=@IsPushWX,");
            if (pIsUpdateNullField || pEntity.IsPushSuccess!=null)
                strSql.Append( "[IsPushSuccess]=@IsPushSuccess,");
            if (pIsUpdateNullField || pEntity.FailureReason!=null)
                strSql.Append( "[FailureReason]=@FailureReason,");
            if (pIsUpdateNullField || pEntity.PushWXTime!=null)
                strSql.Append( "[PushWXTime]=@PushWXTime,");
            if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.OriUrl!=null)
                strSql.Append( "[OriUrl]=@OriUrl,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.ToVipType!=null)
                strSql.Append( "[ToVipType]=@ToVipType");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where MessageId=@MessageId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@MaterialTypeId",SqlDbType.NVarChar),
					new SqlParameter("@Text",SqlDbType.NVarChar),
					new SqlParameter("@ImageUrl",SqlDbType.NVarChar),
					new SqlParameter("@VoiceUrl",SqlDbType.NVarChar),
					new SqlParameter("@VideoUrl",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@WeiXinId",SqlDbType.NVarChar),
					new SqlParameter("@ParentMessageId",SqlDbType.NVarChar),
					new SqlParameter("@DataFrom",SqlDbType.Int),
					new SqlParameter("@IsPushWX",SqlDbType.Int),
					new SqlParameter("@IsPushSuccess",SqlDbType.Int),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@PushWXTime",SqlDbType.DateTime),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@OriUrl",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ToVipType",SqlDbType.Int),
					new SqlParameter("@MessageId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipId;
			parameters[1].Value = pEntity.MaterialTypeId;
			parameters[2].Value = pEntity.Text;
			parameters[3].Value = pEntity.ImageUrl;
			parameters[4].Value = pEntity.VoiceUrl;
			parameters[5].Value = pEntity.VideoUrl;
			parameters[6].Value = pEntity.OpenId;
			parameters[7].Value = pEntity.WeiXinId;
			parameters[8].Value = pEntity.ParentMessageId;
			parameters[9].Value = pEntity.DataFrom;
			parameters[10].Value = pEntity.IsPushWX;
			parameters[11].Value = pEntity.IsPushSuccess;
			parameters[12].Value = pEntity.FailureReason;
			parameters[13].Value = pEntity.PushWXTime;
			parameters[14].Value = pEntity.Title;
			parameters[15].Value = pEntity.OriUrl;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.ToVipType;
			parameters[19].Value = pEntity.MessageId;

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
        public void Update(WUserMessageEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WUserMessageEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WUserMessageEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WUserMessageEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MessageId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.MessageId, pTran);           
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
            sql.AppendLine("update [WUserMessage] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where MessageId=@MessageId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@MessageId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WUserMessageEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.MessageId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.MessageId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WUserMessageEntity[] pEntities)
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
            sql.AppendLine("update [WUserMessage] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where MessageId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WUserMessageEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WUserMessage] where isdelete=0 ");
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
            List<WUserMessageEntity> list = new List<WUserMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WUserMessageEntity m;
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
        public PagedQueryResult<WUserMessageEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [MessageId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WUserMessage] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WUserMessage] where isdelete=0 ");
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
            PagedQueryResult<WUserMessageEntity> result = new PagedQueryResult<WUserMessageEntity>();
            List<WUserMessageEntity> list = new List<WUserMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WUserMessageEntity m;
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
        public WUserMessageEntity[] QueryByEntity(WUserMessageEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WUserMessageEntity> PagedQueryByEntity(WUserMessageEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WUserMessageEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.MessageId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageId", Value = pQueryEntity.MessageId });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.MaterialTypeId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaterialTypeId", Value = pQueryEntity.MaterialTypeId });
            if (pQueryEntity.Text!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Text", Value = pQueryEntity.Text });
            if (pQueryEntity.ImageUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImageUrl", Value = pQueryEntity.ImageUrl });
            if (pQueryEntity.VoiceUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VoiceUrl", Value = pQueryEntity.VoiceUrl });
            if (pQueryEntity.VideoUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VideoUrl", Value = pQueryEntity.VideoUrl });
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });
            if (pQueryEntity.WeiXinId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinId", Value = pQueryEntity.WeiXinId });
            if (pQueryEntity.ParentMessageId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ParentMessageId", Value = pQueryEntity.ParentMessageId });
            if (pQueryEntity.DataFrom!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DataFrom", Value = pQueryEntity.DataFrom });
            if (pQueryEntity.IsPushWX!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPushWX", Value = pQueryEntity.IsPushWX });
            if (pQueryEntity.IsPushSuccess!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsPushSuccess", Value = pQueryEntity.IsPushSuccess });
            if (pQueryEntity.FailureReason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FailureReason", Value = pQueryEntity.FailureReason });
            if (pQueryEntity.PushWXTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PushWXTime", Value = pQueryEntity.PushWXTime });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.OriUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OriUrl", Value = pQueryEntity.OriUrl });
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
            if (pQueryEntity.ToVipType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ToVipType", Value = pQueryEntity.ToVipType });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out WUserMessageEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WUserMessageEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["MessageId"] != DBNull.Value)
			{
				pInstance.MessageId =  Convert.ToString(pReader["MessageId"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["MaterialTypeId"] != DBNull.Value)
			{
				pInstance.MaterialTypeId =  Convert.ToString(pReader["MaterialTypeId"]);
			}
			if (pReader["Text"] != DBNull.Value)
			{
				pInstance.Text =  Convert.ToString(pReader["Text"]);
			}
			if (pReader["ImageUrl"] != DBNull.Value)
			{
				pInstance.ImageUrl =  Convert.ToString(pReader["ImageUrl"]);
			}
			if (pReader["VoiceUrl"] != DBNull.Value)
			{
				pInstance.VoiceUrl =  Convert.ToString(pReader["VoiceUrl"]);
			}
			if (pReader["VideoUrl"] != DBNull.Value)
			{
				pInstance.VideoUrl =  Convert.ToString(pReader["VideoUrl"]);
			}
			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}
			if (pReader["WeiXinId"] != DBNull.Value)
			{
				pInstance.WeiXinId =  Convert.ToString(pReader["WeiXinId"]);
			}
			if (pReader["ParentMessageId"] != DBNull.Value)
			{
				pInstance.ParentMessageId =  Convert.ToString(pReader["ParentMessageId"]);
			}
			if (pReader["DataFrom"] != DBNull.Value)
			{
				pInstance.DataFrom =   Convert.ToInt32(pReader["DataFrom"]);
			}
			if (pReader["IsPushWX"] != DBNull.Value)
			{
				pInstance.IsPushWX =   Convert.ToInt32(pReader["IsPushWX"]);
			}
			if (pReader["IsPushSuccess"] != DBNull.Value)
			{
				pInstance.IsPushSuccess =   Convert.ToInt32(pReader["IsPushSuccess"]);
			}
			if (pReader["FailureReason"] != DBNull.Value)
			{
				pInstance.FailureReason =  Convert.ToString(pReader["FailureReason"]);
			}
			if (pReader["PushWXTime"] != DBNull.Value)
			{
				pInstance.PushWXTime =  Convert.ToDateTime(pReader["PushWXTime"]);
			}
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}
			if (pReader["OriUrl"] != DBNull.Value)
			{
				pInstance.OriUrl =  Convert.ToString(pReader["OriUrl"]);
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
			if (pReader["ToVipType"] != DBNull.Value)
			{
				pInstance.ToVipType =   Convert.ToInt32(pReader["ToVipType"]);
			}

        }
        #endregion
    }
}
