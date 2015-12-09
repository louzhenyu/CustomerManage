/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/11/26 19:56:00
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
    /// ��WXTMConfig�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXTMConfigDAO : Base.BaseCPOSDAO, ICRUDable<WXTMConfigEntity>, IQueryable<WXTMConfigEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXTMConfigDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXTMConfigEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXTMConfigEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            
            //��ʼ���̶��ֶ�
			pEntity.IsDelete=0;
			pEntity.CreateTime=DateTime.Now;
			pEntity.LastUpdateTime=pEntity.CreateTime;
			pEntity.CreateBy=CurrentUserInfo.UserID;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [WXTMConfig](");
            strSql.Append("[WeiXinId],[TemplateIdShort],[AppId],[FirstText],[RemarkText],[FirstColour],[RemarkColour],[AmountColour],[Colour1],[Colour2],[Colour3],[Colour4],[Colour5],[Colour6],[CustomerId],[IsDelete],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[Title],[TemplateID])");
            strSql.Append(" values (");
            strSql.Append("@WeiXinId,@TemplateIdShort,@AppId,@FirstText,@RemarkText,@FirstColour,@RemarkColour,@AmountColour,@Colour1,@Colour2,@Colour3,@Colour4,@Colour5,@Colour6,@CustomerId,@IsDelete,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@Title,@TemplateID)");            

			string pkString = pEntity.TemplateID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinId",SqlDbType.VarChar),
					new SqlParameter("@TemplateIdShort",SqlDbType.VarChar),
					new SqlParameter("@AppId",SqlDbType.VarChar),
					new SqlParameter("@FirstText",SqlDbType.VarChar),
					new SqlParameter("@RemarkText",SqlDbType.VarChar),
					new SqlParameter("@FirstColour",SqlDbType.VarChar),
					new SqlParameter("@RemarkColour",SqlDbType.VarChar),
					new SqlParameter("@AmountColour",SqlDbType.VarChar),
					new SqlParameter("@Colour1",SqlDbType.VarChar),
					new SqlParameter("@Colour2",SqlDbType.VarChar),
					new SqlParameter("@Colour3",SqlDbType.VarChar),
					new SqlParameter("@Colour4",SqlDbType.VarChar),
					new SqlParameter("@Colour5",SqlDbType.VarChar),
					new SqlParameter("@Colour6",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@Title",SqlDbType.VarChar),
					new SqlParameter("@TemplateID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.WeiXinId;
			parameters[1].Value = pEntity.TemplateIdShort;
			parameters[2].Value = pEntity.AppId;
			parameters[3].Value = pEntity.FirstText;
			parameters[4].Value = pEntity.RemarkText;
			parameters[5].Value = pEntity.FirstColour;
			parameters[6].Value = pEntity.RemarkColour;
			parameters[7].Value = pEntity.AmountColour;
			parameters[8].Value = pEntity.Colour1;
			parameters[9].Value = pEntity.Colour2;
			parameters[10].Value = pEntity.Colour3;
			parameters[11].Value = pEntity.Colour4;
			parameters[12].Value = pEntity.Colour5;
			parameters[13].Value = pEntity.Colour6;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CreateTime;
			parameters[17].Value = pEntity.CreateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.LastUpdateBy;
			parameters[20].Value = pEntity.Title;
			parameters[21].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.TemplateID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXTMConfigEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXTMConfig] where TemplateID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            WXTMConfigEntity m = null;
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
        public WXTMConfigEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXTMConfig] where 1=1  and isdelete=0");
            //��ȡ����
            List<WXTMConfigEntity> list = new List<WXTMConfigEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXTMConfigEntity m;
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
        public void Update(WXTMConfigEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(WXTMConfigEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TemplateID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXTMConfig] set ");
                        if (pIsUpdateNullField || pEntity.WeiXinId!=null)
                strSql.Append( "[WeiXinId]=@WeiXinId,");
            if (pIsUpdateNullField || pEntity.TemplateIdShort!=null)
                strSql.Append( "[TemplateIdShort]=@TemplateIdShort,");
            if (pIsUpdateNullField || pEntity.AppId!=null)
                strSql.Append( "[AppId]=@AppId,");
            if (pIsUpdateNullField || pEntity.FirstText!=null)
                strSql.Append( "[FirstText]=@FirstText,");
            if (pIsUpdateNullField || pEntity.RemarkText!=null)
                strSql.Append( "[RemarkText]=@RemarkText,");
            if (pIsUpdateNullField || pEntity.FirstColour!=null)
                strSql.Append( "[FirstColour]=@FirstColour,");
            if (pIsUpdateNullField || pEntity.RemarkColour!=null)
                strSql.Append( "[RemarkColour]=@RemarkColour,");
            if (pIsUpdateNullField || pEntity.AmountColour!=null)
                strSql.Append( "[AmountColour]=@AmountColour,");
            if (pIsUpdateNullField || pEntity.Colour1!=null)
                strSql.Append( "[Colour1]=@Colour1,");
            if (pIsUpdateNullField || pEntity.Colour2!=null)
                strSql.Append( "[Colour2]=@Colour2,");
            if (pIsUpdateNullField || pEntity.Colour3!=null)
                strSql.Append( "[Colour3]=@Colour3,");
            if (pIsUpdateNullField || pEntity.Colour4!=null)
                strSql.Append( "[Colour4]=@Colour4,");
            if (pIsUpdateNullField || pEntity.Colour5!=null)
                strSql.Append( "[Colour5]=@Colour5,");
            if (pIsUpdateNullField || pEntity.Colour6!=null)
                strSql.Append( "[Colour6]=@Colour6,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.Title!=null)
                strSql.Append( "[Title]=@Title");
            strSql.Append(" where TemplateID=@TemplateID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@WeiXinId",SqlDbType.VarChar),
					new SqlParameter("@TemplateIdShort",SqlDbType.VarChar),
					new SqlParameter("@AppId",SqlDbType.VarChar),
					new SqlParameter("@FirstText",SqlDbType.VarChar),
					new SqlParameter("@RemarkText",SqlDbType.VarChar),
					new SqlParameter("@FirstColour",SqlDbType.VarChar),
					new SqlParameter("@RemarkColour",SqlDbType.VarChar),
					new SqlParameter("@AmountColour",SqlDbType.VarChar),
					new SqlParameter("@Colour1",SqlDbType.VarChar),
					new SqlParameter("@Colour2",SqlDbType.VarChar),
					new SqlParameter("@Colour3",SqlDbType.VarChar),
					new SqlParameter("@Colour4",SqlDbType.VarChar),
					new SqlParameter("@Colour5",SqlDbType.VarChar),
					new SqlParameter("@Colour6",SqlDbType.VarChar),
					new SqlParameter("@CustomerId",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@Title",SqlDbType.VarChar),
					new SqlParameter("@TemplateID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.WeiXinId;
			parameters[1].Value = pEntity.TemplateIdShort;
			parameters[2].Value = pEntity.AppId;
			parameters[3].Value = pEntity.FirstText;
			parameters[4].Value = pEntity.RemarkText;
			parameters[5].Value = pEntity.FirstColour;
			parameters[6].Value = pEntity.RemarkColour;
			parameters[7].Value = pEntity.AmountColour;
			parameters[8].Value = pEntity.Colour1;
			parameters[9].Value = pEntity.Colour2;
			parameters[10].Value = pEntity.Colour3;
			parameters[11].Value = pEntity.Colour4;
			parameters[12].Value = pEntity.Colour5;
			parameters[13].Value = pEntity.Colour6;
			parameters[14].Value = pEntity.CustomerId;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.Title;
			parameters[18].Value = pEntity.TemplateID;

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
        public void Update(WXTMConfigEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXTMConfigEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXTMConfigEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.TemplateID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.TemplateID, pTran);           
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
            sql.AppendLine("update [WXTMConfig] set  isdelete=1 where TemplateID=@TemplateID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@TemplateID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(WXTMConfigEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.TemplateID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.TemplateID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXTMConfigEntity[] pEntities)
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
            sql.AppendLine("update [WXTMConfig] set  isdelete=1 where TemplateID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXTMConfigEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXTMConfig] where 1=1  and isdelete=0 ");
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
            List<WXTMConfigEntity> list = new List<WXTMConfigEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXTMConfigEntity m;
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
        public PagedQueryResult<WXTMConfigEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TemplateID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXTMConfig] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXTMConfig] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<WXTMConfigEntity> result = new PagedQueryResult<WXTMConfigEntity>();
            List<WXTMConfigEntity> list = new List<WXTMConfigEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXTMConfigEntity m;
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
        public WXTMConfigEntity[] QueryByEntity(WXTMConfigEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXTMConfigEntity> PagedQueryByEntity(WXTMConfigEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXTMConfigEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TemplateID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateID", Value = pQueryEntity.TemplateID });
            if (pQueryEntity.WeiXinId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinId", Value = pQueryEntity.WeiXinId });
            if (pQueryEntity.TemplateIdShort!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TemplateIdShort", Value = pQueryEntity.TemplateIdShort });
            if (pQueryEntity.AppId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppId", Value = pQueryEntity.AppId });
            if (pQueryEntity.FirstText!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstText", Value = pQueryEntity.FirstText });
            if (pQueryEntity.RemarkText!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RemarkText", Value = pQueryEntity.RemarkText });
            if (pQueryEntity.FirstColour!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FirstColour", Value = pQueryEntity.FirstColour });
            if (pQueryEntity.RemarkColour!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RemarkColour", Value = pQueryEntity.RemarkColour });
            if (pQueryEntity.AmountColour!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AmountColour", Value = pQueryEntity.AmountColour });
            if (pQueryEntity.Colour1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour1", Value = pQueryEntity.Colour1 });
            if (pQueryEntity.Colour2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour2", Value = pQueryEntity.Colour2 });
            if (pQueryEntity.Colour3!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour3", Value = pQueryEntity.Colour3 });
            if (pQueryEntity.Colour4!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour4", Value = pQueryEntity.Colour4 });
            if (pQueryEntity.Colour5!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour5", Value = pQueryEntity.Colour5 });
            if (pQueryEntity.Colour6!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Colour6", Value = pQueryEntity.Colour6 });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.Title!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Title", Value = pQueryEntity.Title });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out WXTMConfigEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXTMConfigEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["TemplateID"] != DBNull.Value)
			{
				pInstance.TemplateID =  Convert.ToString(pReader["TemplateID"]);
			}
			if (pReader["WeiXinId"] != DBNull.Value)
			{
				pInstance.WeiXinId =  Convert.ToString(pReader["WeiXinId"]);
			}
			if (pReader["TemplateIdShort"] != DBNull.Value)
			{
				pInstance.TemplateIdShort =  Convert.ToString(pReader["TemplateIdShort"]);
			}
			if (pReader["AppId"] != DBNull.Value)
			{
				pInstance.AppId =  Convert.ToString(pReader["AppId"]);
			}
			if (pReader["FirstText"] != DBNull.Value)
			{
				pInstance.FirstText =  Convert.ToString(pReader["FirstText"]);
			}
			if (pReader["RemarkText"] != DBNull.Value)
			{
				pInstance.RemarkText =  Convert.ToString(pReader["RemarkText"]);
			}
			if (pReader["FirstColour"] != DBNull.Value)
			{
				pInstance.FirstColour =  Convert.ToString(pReader["FirstColour"]);
			}
			if (pReader["RemarkColour"] != DBNull.Value)
			{
				pInstance.RemarkColour =  Convert.ToString(pReader["RemarkColour"]);
			}
			if (pReader["AmountColour"] != DBNull.Value)
			{
				pInstance.AmountColour =  Convert.ToString(pReader["AmountColour"]);
			}
			if (pReader["Colour1"] != DBNull.Value)
			{
				pInstance.Colour1 =  Convert.ToString(pReader["Colour1"]);
			}
			if (pReader["Colour2"] != DBNull.Value)
			{
				pInstance.Colour2 =  Convert.ToString(pReader["Colour2"]);
			}
			if (pReader["Colour3"] != DBNull.Value)
			{
				pInstance.Colour3 =  Convert.ToString(pReader["Colour3"]);
			}
			if (pReader["Colour4"] != DBNull.Value)
			{
				pInstance.Colour4 =  Convert.ToString(pReader["Colour4"]);
			}
			if (pReader["Colour5"] != DBNull.Value)
			{
				pInstance.Colour5 =  Convert.ToString(pReader["Colour5"]);
			}
			if (pReader["Colour6"] != DBNull.Value)
			{
				pInstance.Colour6 =  Convert.ToString(pReader["Colour6"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CreateTime"] != DBNull.Value)
			{
				pInstance.CreateTime =  Convert.ToDateTime(pReader["CreateTime"]);
			}
			if (pReader["CreateBy"] != DBNull.Value)
			{
				pInstance.CreateBy =  Convert.ToString(pReader["CreateBy"]);
			}
			if (pReader["LastUpdateTime"] != DBNull.Value)
			{
				pInstance.LastUpdateTime =  Convert.ToDateTime(pReader["LastUpdateTime"]);
			}
			if (pReader["LastUpdateBy"] != DBNull.Value)
			{
				pInstance.LastUpdateBy =  Convert.ToString(pReader["LastUpdateBy"]);
			}
			if (pReader["Title"] != DBNull.Value)
			{
				pInstance.Title =  Convert.ToString(pReader["Title"]);
			}

        }
        #endregion
    }
}
