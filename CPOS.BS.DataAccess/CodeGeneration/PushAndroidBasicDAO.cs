/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/21 14:24:47
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
    /// ��PushAndroidBasic�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PushAndroidBasicDAO : Base.BaseCPOSDAO, ICRUDable<PushAndroidBasicEntity>, IQueryable<PushAndroidBasicEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PushAndroidBasicDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(PushAndroidBasicEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(PushAndroidBasicEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [PushAndroidBasic](");
            strSql.Append("[Locale],[Version],[SessionID],[Plat],[DeviceToken],[OsInfo],[Channel],[Phone],[ChannelIDBaiDu],[BaiduPushAppID],[UserIDBaiDu],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[UserID])");
            strSql.Append(" values (");
            strSql.Append("@Locale,@Version,@SessionID,@Plat,@DeviceToken,@OsInfo,@Channel,@Phone,@ChannelIDBaiDu,@BaiduPushAppID,@UserIDBaiDu,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@UserID)");            

			string pkString = pEntity.UserID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@Locale",SqlDbType.NVarChar),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@SessionID",SqlDbType.NVarChar),
					new SqlParameter("@Plat",SqlDbType.NVarChar),
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@OsInfo",SqlDbType.NVarChar),
					new SqlParameter("@Channel",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@ChannelIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@BaiduPushAppID",SqlDbType.NVarChar),
					new SqlParameter("@UserIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Locale;
			parameters[1].Value = pEntity.Version;
			parameters[2].Value = pEntity.SessionID;
			parameters[3].Value = pEntity.Plat;
			parameters[4].Value = pEntity.DeviceToken;
			parameters[5].Value = pEntity.OsInfo;
			parameters[6].Value = pEntity.Channel;
			parameters[7].Value = pEntity.Phone;
			parameters[8].Value = pEntity.ChannelIDBaiDu;
			parameters[9].Value = pEntity.BaiduPushAppID;
			parameters[10].Value = pEntity.UserIDBaiDu;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pEntity.CustomerId;
			parameters[17].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.UserID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public PushAndroidBasicEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushAndroidBasic] where UserID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            PushAndroidBasicEntity m = null;
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
        public PushAndroidBasicEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushAndroidBasic] where isdelete=0");
            //��ȡ����
            List<PushAndroidBasicEntity> list = new List<PushAndroidBasicEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PushAndroidBasicEntity m;
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
        public void Update(PushAndroidBasicEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(PushAndroidBasicEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UserID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PushAndroidBasic] set ");
            if (pIsUpdateNullField || pEntity.Locale!=null)
                strSql.Append( "[Locale]=@Locale,");
            if (pIsUpdateNullField || pEntity.Version!=null)
                strSql.Append( "[Version]=@Version,");
            if (pIsUpdateNullField || pEntity.SessionID!=null)
                strSql.Append( "[SessionID]=@SessionID,");
            if (pIsUpdateNullField || pEntity.Plat!=null)
                strSql.Append( "[Plat]=@Plat,");
            if (pIsUpdateNullField || pEntity.DeviceToken!=null)
                strSql.Append( "[DeviceToken]=@DeviceToken,");
            if (pIsUpdateNullField || pEntity.OsInfo!=null)
                strSql.Append( "[OsInfo]=@OsInfo,");
            if (pIsUpdateNullField || pEntity.Channel!=null)
                strSql.Append( "[Channel]=@Channel,");
            if (pIsUpdateNullField || pEntity.Phone!=null)
                strSql.Append( "[Phone]=@Phone,");
            if (pIsUpdateNullField || pEntity.ChannelIDBaiDu!=null)
                strSql.Append( "[ChannelIDBaiDu]=@ChannelIDBaiDu,");
            if (pIsUpdateNullField || pEntity.BaiduPushAppID!=null)
                strSql.Append( "[BaiduPushAppID]=@BaiduPushAppID,");
            if (pIsUpdateNullField || pEntity.UserIDBaiDu!=null)
                strSql.Append( "[UserIDBaiDu]=@UserIDBaiDu,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where UserID=@UserID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@Locale",SqlDbType.NVarChar),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@SessionID",SqlDbType.NVarChar),
					new SqlParameter("@Plat",SqlDbType.NVarChar),
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@OsInfo",SqlDbType.NVarChar),
					new SqlParameter("@Channel",SqlDbType.NVarChar),
					new SqlParameter("@Phone",SqlDbType.NVarChar),
					new SqlParameter("@ChannelIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@BaiduPushAppID",SqlDbType.NVarChar),
					new SqlParameter("@UserIDBaiDu",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.Locale;
			parameters[1].Value = pEntity.Version;
			parameters[2].Value = pEntity.SessionID;
			parameters[3].Value = pEntity.Plat;
			parameters[4].Value = pEntity.DeviceToken;
			parameters[5].Value = pEntity.OsInfo;
			parameters[6].Value = pEntity.Channel;
			parameters[7].Value = pEntity.Phone;
			parameters[8].Value = pEntity.ChannelIDBaiDu;
			parameters[9].Value = pEntity.BaiduPushAppID;
			parameters[10].Value = pEntity.UserIDBaiDu;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pEntity.UserID;

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
        public void Update(PushAndroidBasicEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(PushAndroidBasicEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PushAndroidBasicEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(PushAndroidBasicEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.UserID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.UserID, pTran);           
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
            sql.AppendLine("update [PushAndroidBasic] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where UserID=@UserID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@UserID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(PushAndroidBasicEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.UserID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.UserID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(PushAndroidBasicEntity[] pEntities)
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
            sql.AppendLine("update [PushAndroidBasic] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where UserID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PushAndroidBasicEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushAndroidBasic] where isdelete=0 ");
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
            List<PushAndroidBasicEntity> list = new List<PushAndroidBasicEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PushAndroidBasicEntity m;
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
        public PagedQueryResult<PushAndroidBasicEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [UserID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [PushAndroidBasic] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [PushAndroidBasic] where isdelete=0 ");
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
            PagedQueryResult<PushAndroidBasicEntity> result = new PagedQueryResult<PushAndroidBasicEntity>();
            List<PushAndroidBasicEntity> list = new List<PushAndroidBasicEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PushAndroidBasicEntity m;
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
        public PushAndroidBasicEntity[] QueryByEntity(PushAndroidBasicEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PushAndroidBasicEntity> PagedQueryByEntity(PushAndroidBasicEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PushAndroidBasicEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.UserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
            if (pQueryEntity.Locale!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Locale", Value = pQueryEntity.Locale });
            if (pQueryEntity.Version!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Version", Value = pQueryEntity.Version });
            if (pQueryEntity.SessionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SessionID", Value = pQueryEntity.SessionID });
            if (pQueryEntity.Plat!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Plat", Value = pQueryEntity.Plat });
            if (pQueryEntity.DeviceToken!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeviceToken", Value = pQueryEntity.DeviceToken });
            if (pQueryEntity.OsInfo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OsInfo", Value = pQueryEntity.OsInfo });
            if (pQueryEntity.Channel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Channel", Value = pQueryEntity.Channel });
            if (pQueryEntity.Phone!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Phone", Value = pQueryEntity.Phone });
            if (pQueryEntity.ChannelIDBaiDu!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelIDBaiDu", Value = pQueryEntity.ChannelIDBaiDu });
            if (pQueryEntity.BaiduPushAppID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BaiduPushAppID", Value = pQueryEntity.BaiduPushAppID });
            if (pQueryEntity.UserIDBaiDu!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserIDBaiDu", Value = pQueryEntity.UserIDBaiDu });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out PushAndroidBasicEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new PushAndroidBasicEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["UserID"] != DBNull.Value)
			{
				pInstance.UserID =  Convert.ToString(pReader["UserID"]);
			}
			if (pReader["Locale"] != DBNull.Value)
			{
				pInstance.Locale =  Convert.ToString(pReader["Locale"]);
			}
			if (pReader["Version"] != DBNull.Value)
			{
				pInstance.Version =  Convert.ToString(pReader["Version"]);
			}
			if (pReader["SessionID"] != DBNull.Value)
			{
				pInstance.SessionID =  Convert.ToString(pReader["SessionID"]);
			}
			if (pReader["Plat"] != DBNull.Value)
			{
				pInstance.Plat =  Convert.ToString(pReader["Plat"]);
			}
			if (pReader["DeviceToken"] != DBNull.Value)
			{
				pInstance.DeviceToken =  Convert.ToString(pReader["DeviceToken"]);
			}
			if (pReader["OsInfo"] != DBNull.Value)
			{
				pInstance.OsInfo =  Convert.ToString(pReader["OsInfo"]);
			}
			if (pReader["Channel"] != DBNull.Value)
			{
				pInstance.Channel =  Convert.ToString(pReader["Channel"]);
			}
			if (pReader["Phone"] != DBNull.Value)
			{
				pInstance.Phone =  Convert.ToString(pReader["Phone"]);
			}
			if (pReader["ChannelIDBaiDu"] != DBNull.Value)
			{
				pInstance.ChannelIDBaiDu =  Convert.ToString(pReader["ChannelIDBaiDu"]);
			}
			if (pReader["BaiduPushAppID"] != DBNull.Value)
			{
				pInstance.BaiduPushAppID =  Convert.ToString(pReader["BaiduPushAppID"]);
			}
			if (pReader["UserIDBaiDu"] != DBNull.Value)
			{
				pInstance.UserIDBaiDu =  Convert.ToString(pReader["UserIDBaiDu"]);
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

        }
        #endregion
    }
}
