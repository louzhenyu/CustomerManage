/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/15 18:13:03
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
    /// ��WNewsPush�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WNewsPushDAO : Base.BaseCPOSDAO, ICRUDable<WNewsPushEntity>, IQueryable<WNewsPushEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WNewsPushDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WNewsPushEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WNewsPushEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WNewsPush](");
            strSql.Append("[WeiXinID],[MsgType],[Content],[PicUrl],[Location_X],[Location_Y],[Scale],[Title],[Description],[Url],[AnswerMsgType],[AnswerContent],[AnswerMusicUrl],[AnswerHQMusicUrl],[AnswerArticleCount],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[NewsPushID])");
            strSql.Append(" values (");
            strSql.Append("@WeiXinID,@MsgType,@Content,@PicUrl,@LocationX,@LocationY,@Scale,@Title,@Description,@Url,@AnswerMsgType,@AnswerContent,@AnswerMusicUrl,@AnswerHQMusicUrl,@AnswerArticleCount,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@NewsPushID)");            

			string pkString = pEntity.NewsPushID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@MsgType",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar),
					new SqlParameter("@LocationX",SqlDbType.NVarChar),
					new SqlParameter("@LocationY",SqlDbType.NVarChar),
					new SqlParameter("@Scale",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Url",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMsgType",SqlDbType.NVarChar),
					new SqlParameter("@AnswerContent",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerHQMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerArticleCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@NewsPushID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.WeiXinID;
			parameters[1].Value = pEntity.MsgType;
			parameters[2].Value = pEntity.Content;
			parameters[3].Value = pEntity.PicUrl;
			parameters[4].Value = pEntity.LocationX;
			parameters[5].Value = pEntity.LocationY;
			parameters[6].Value = pEntity.Scale;
			parameters[7].Value = pEntity.Title;
			parameters[8].Value = pEntity.Description;
			parameters[9].Value = pEntity.Url;
			parameters[10].Value = pEntity.AnswerMsgType;
			parameters[11].Value = pEntity.AnswerContent;
			parameters[12].Value = pEntity.AnswerMusicUrl;
			parameters[13].Value = pEntity.AnswerHQMusicUrl;
			parameters[14].Value = pEntity.AnswerArticleCount;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.IsDelete;
			parameters[20].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.NewsPushID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WNewsPushEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WNewsPush] where NewsPushID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WNewsPushEntity m = null;
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
        public WNewsPushEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WNewsPush] where isdelete=0");
            //��ȡ����
            List<WNewsPushEntity> list = new List<WNewsPushEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WNewsPushEntity m;
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
        public void Update(WNewsPushEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WNewsPushEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsPushID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WNewsPush] set ");
            if (pIsUpdateNullField || pEntity.WeiXinID!=null)
                strSql.Append( "[WeiXinID]=@WeiXinID,");
            if (pIsUpdateNullField || pEntity.MsgType!=null)
                strSql.Append( "[MsgType]=@MsgType,");
            if (pIsUpdateNullField || pEntity.Content!=null)
                strSql.Append( "[Content]=@Content,");
            if (pIsUpdateNullField || pEntity.PicUrl!=null)
                strSql.Append( "[PicUrl]=@PicUrl,");
            if (pIsUpdateNullField || pEntity.LocationX!=null)
                strSql.Append( "[Location_X]=@LocationX,");
            if (pIsUpdateNullField || pEntity.LocationY!=null)
                strSql.Append( "[Location_Y]=@LocationY,");
            if (pIsUpdateNullField || pEntity.Scale!=null)
                strSql.Append( "[Scale]=@Scale,");
            if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title,");
            if (pIsUpdateNullField || pEntity.Description!=null)
                strSql.Append( "[Description]=@Description,");
            if (pIsUpdateNullField || pEntity.Url!=null)
                strSql.Append( "[Url]=@Url,");
            if (pIsUpdateNullField || pEntity.AnswerMsgType!=null)
                strSql.Append( "[AnswerMsgType]=@AnswerMsgType,");
            if (pIsUpdateNullField || pEntity.AnswerContent!=null)
                strSql.Append( "[AnswerContent]=@AnswerContent,");
            if (pIsUpdateNullField || pEntity.AnswerMusicUrl!=null)
                strSql.Append( "[AnswerMusicUrl]=@AnswerMusicUrl,");
            if (pIsUpdateNullField || pEntity.AnswerHQMusicUrl!=null)
                strSql.Append( "[AnswerHQMusicUrl]=@AnswerHQMusicUrl,");
            if (pIsUpdateNullField || pEntity.AnswerArticleCount!=null)
                strSql.Append( "[AnswerArticleCount]=@AnswerArticleCount,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where NewsPushID=@NewsPushID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinID",SqlDbType.NVarChar),
					new SqlParameter("@MsgType",SqlDbType.NVarChar),
					new SqlParameter("@Content",SqlDbType.NVarChar),
					new SqlParameter("@PicUrl",SqlDbType.NVarChar),
					new SqlParameter("@LocationX",SqlDbType.NVarChar),
					new SqlParameter("@LocationY",SqlDbType.NVarChar),
					new SqlParameter("@Scale",SqlDbType.NVarChar),
					new SqlParameter("@Title",SqlDbType.NVarChar),
					new SqlParameter("@Description",SqlDbType.NVarChar),
					new SqlParameter("@Url",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMsgType",SqlDbType.NVarChar),
					new SqlParameter("@AnswerContent",SqlDbType.NVarChar),
					new SqlParameter("@AnswerMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerHQMusicUrl",SqlDbType.NVarChar),
					new SqlParameter("@AnswerArticleCount",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@NewsPushID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.WeiXinID;
			parameters[1].Value = pEntity.MsgType;
			parameters[2].Value = pEntity.Content;
			parameters[3].Value = pEntity.PicUrl;
			parameters[4].Value = pEntity.LocationX;
			parameters[5].Value = pEntity.LocationY;
			parameters[6].Value = pEntity.Scale;
			parameters[7].Value = pEntity.Title;
			parameters[8].Value = pEntity.Description;
			parameters[9].Value = pEntity.Url;
			parameters[10].Value = pEntity.AnswerMsgType;
			parameters[11].Value = pEntity.AnswerContent;
			parameters[12].Value = pEntity.AnswerMusicUrl;
			parameters[13].Value = pEntity.AnswerHQMusicUrl;
			parameters[14].Value = pEntity.AnswerArticleCount;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.LastUpdateTime;
			parameters[17].Value = pEntity.NewsPushID;

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
        public void Update(WNewsPushEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WNewsPushEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WNewsPushEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WNewsPushEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.NewsPushID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.NewsPushID, pTran);           
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
            sql.AppendLine("update [WNewsPush] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where NewsPushID=@NewsPushID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@NewsPushID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WNewsPushEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.NewsPushID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.NewsPushID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WNewsPushEntity[] pEntities)
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
            sql.AppendLine("update [WNewsPush] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where NewsPushID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WNewsPushEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WNewsPush] where isdelete=0 ");
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
            List<WNewsPushEntity> list = new List<WNewsPushEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WNewsPushEntity m;
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
        public PagedQueryResult<WNewsPushEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [NewsPushID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WNewsPush] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WNewsPush] where isdelete=0 ");
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
            PagedQueryResult<WNewsPushEntity> result = new PagedQueryResult<WNewsPushEntity>();
            List<WNewsPushEntity> list = new List<WNewsPushEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WNewsPushEntity m;
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
        public WNewsPushEntity[] QueryByEntity(WNewsPushEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WNewsPushEntity> PagedQueryByEntity(WNewsPushEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WNewsPushEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.NewsPushID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewsPushID", Value = pQueryEntity.NewsPushID });
            if (pQueryEntity.WeiXinID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinID", Value = pQueryEntity.WeiXinID });
            if (pQueryEntity.MsgType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MsgType", Value = pQueryEntity.MsgType });
            if (pQueryEntity.Content!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Content", Value = pQueryEntity.Content });
            if (pQueryEntity.PicUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PicUrl", Value = pQueryEntity.PicUrl });
            if (pQueryEntity.LocationX!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LocationX", Value = pQueryEntity.LocationX });
            if (pQueryEntity.LocationY!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LocationY", Value = pQueryEntity.LocationY });
            if (pQueryEntity.Scale!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Scale", Value = pQueryEntity.Scale });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });
            if (pQueryEntity.Description!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Description", Value = pQueryEntity.Description });
            if (pQueryEntity.Url!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Url", Value = pQueryEntity.Url });
            if (pQueryEntity.AnswerMsgType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerMsgType", Value = pQueryEntity.AnswerMsgType });
            if (pQueryEntity.AnswerContent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerContent", Value = pQueryEntity.AnswerContent });
            if (pQueryEntity.AnswerMusicUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerMusicUrl", Value = pQueryEntity.AnswerMusicUrl });
            if (pQueryEntity.AnswerHQMusicUrl!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerHQMusicUrl", Value = pQueryEntity.AnswerHQMusicUrl });
            if (pQueryEntity.AnswerArticleCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AnswerArticleCount", Value = pQueryEntity.AnswerArticleCount });
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
        protected void Load(SqlDataReader pReader, out WNewsPushEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WNewsPushEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["NewsPushID"] != DBNull.Value)
			{
				pInstance.NewsPushID =  Convert.ToString(pReader["NewsPushID"]);
			}
			if (pReader["WeiXinID"] != DBNull.Value)
			{
				pInstance.WeiXinID =  Convert.ToString(pReader["WeiXinID"]);
			}
			if (pReader["MsgType"] != DBNull.Value)
			{
				pInstance.MsgType =  Convert.ToString(pReader["MsgType"]);
			}
			if (pReader["Content"] != DBNull.Value)
			{
				pInstance.Content =  Convert.ToString(pReader["Content"]);
			}
			if (pReader["PicUrl"] != DBNull.Value)
			{
				pInstance.PicUrl =  Convert.ToString(pReader["PicUrl"]);
			}
			if (pReader["Location_X"] != DBNull.Value)
			{
				pInstance.LocationX =  Convert.ToString(pReader["Location_X"]);
			}
			if (pReader["Location_Y"] != DBNull.Value)
			{
				pInstance.LocationY =  Convert.ToString(pReader["Location_Y"]);
			}
			if (pReader["Scale"] != DBNull.Value)
			{
				pInstance.Scale =  Convert.ToString(pReader["Scale"]);
			}
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}
			if (pReader["Description"] != DBNull.Value)
			{
				pInstance.Description =  Convert.ToString(pReader["Description"]);
			}
			if (pReader["Url"] != DBNull.Value)
			{
				pInstance.Url =  Convert.ToString(pReader["Url"]);
			}
			if (pReader["AnswerMsgType"] != DBNull.Value)
			{
				pInstance.AnswerMsgType =  Convert.ToString(pReader["AnswerMsgType"]);
			}
			if (pReader["AnswerContent"] != DBNull.Value)
			{
				pInstance.AnswerContent =  Convert.ToString(pReader["AnswerContent"]);
			}
			if (pReader["AnswerMusicUrl"] != DBNull.Value)
			{
				pInstance.AnswerMusicUrl =  Convert.ToString(pReader["AnswerMusicUrl"]);
			}
			if (pReader["AnswerHQMusicUrl"] != DBNull.Value)
			{
				pInstance.AnswerHQMusicUrl =  Convert.ToString(pReader["AnswerHQMusicUrl"]);
			}
			if (pReader["AnswerArticleCount"] != DBNull.Value)
			{
				pInstance.AnswerArticleCount =   Convert.ToInt32(pReader["AnswerArticleCount"]);
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
