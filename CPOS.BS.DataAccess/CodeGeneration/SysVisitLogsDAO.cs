/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/31 16:47:21
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
    /// ��SysVisitLogs�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysVisitLogsDAO : Base.BaseCPOSDAO, ICRUDable<SysVisitLogsEntity>, IQueryable<SysVisitLogsEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public SysVisitLogsDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(SysVisitLogsEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(SysVisitLogsEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [SysVisitLogs](");
            strSql.Append("[UserID],[LogType],[Plat],[Version],[IpAddress],[ChannelId],[DeviceToken],[SessionID],[Locale],[OSInfo],[ItemID],[SpecialParams],[ResultCode],[ResultDescription],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[WeiXinId],[OpenId],[LogsID])");
            strSql.Append(" values (");
            strSql.Append("@UserID,@LogType,@Plat,@Version,@IpAddress,@ChannelId,@DeviceToken,@SessionID,@Locale,@OSInfo,@ItemID,@SpecialParams,@ResultCode,@ResultDescription,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@WeiXinId,@OpenId,@LogsID)");            

			string pkString = pEntity.LogsID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@LogType",SqlDbType.NVarChar),
					new SqlParameter("@Plat",SqlDbType.NVarChar),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@IpAddress",SqlDbType.NVarChar),
					new SqlParameter("@ChannelId",SqlDbType.NVarChar),
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@SessionID",SqlDbType.NVarChar),
					new SqlParameter("@Locale",SqlDbType.NVarChar),
					new SqlParameter("@OSInfo",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SpecialParams",SqlDbType.NVarChar),
					new SqlParameter("@ResultCode",SqlDbType.NVarChar),
					new SqlParameter("@ResultDescription",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@WeiXinId",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@LogsID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UserID;
			parameters[1].Value = pEntity.LogType;
			parameters[2].Value = pEntity.Plat;
			parameters[3].Value = pEntity.Version;
			parameters[4].Value = pEntity.IpAddress;
			parameters[5].Value = pEntity.ChannelId;
			parameters[6].Value = pEntity.DeviceToken;
			parameters[7].Value = pEntity.SessionID;
			parameters[8].Value = pEntity.Locale;
			parameters[9].Value = pEntity.OSInfo;
			parameters[10].Value = pEntity.ItemID;
			parameters[11].Value = pEntity.SpecialParams;
			parameters[12].Value = pEntity.ResultCode;
			parameters[13].Value = pEntity.ResultDescription;
			parameters[14].Value = pEntity.CreateTime;
			parameters[15].Value = pEntity.CreateBy;
			parameters[16].Value = pEntity.LastUpdateBy;
			parameters[17].Value = pEntity.LastUpdateTime;
			parameters[18].Value = pEntity.IsDelete;
			parameters[19].Value = pEntity.WeiXinId;
			parameters[20].Value = pEntity.OpenId;
			parameters[21].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.LogsID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public SysVisitLogsEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVisitLogs] where LogsID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            SysVisitLogsEntity m = null;
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
        public SysVisitLogsEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVisitLogs] where isdelete=0");
            //��ȡ����
            List<SysVisitLogsEntity> list = new List<SysVisitLogsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVisitLogsEntity m;
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
        public void Update(SysVisitLogsEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(SysVisitLogsEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.LogsID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [SysVisitLogs] set ");
            if (pIsUpdateNullField || pEntity.UserID!=null)
                strSql.Append( "[UserID]=@UserID,");
            if (pIsUpdateNullField || pEntity.LogType!=null)
                strSql.Append( "[LogType]=@LogType,");
            if (pIsUpdateNullField || pEntity.Plat!=null)
                strSql.Append( "[Plat]=@Plat,");
            if (pIsUpdateNullField || pEntity.Version!=null)
                strSql.Append( "[Version]=@Version,");
            if (pIsUpdateNullField || pEntity.IpAddress!=null)
                strSql.Append( "[IpAddress]=@IpAddress,");
            if (pIsUpdateNullField || pEntity.ChannelId!=null)
                strSql.Append( "[ChannelId]=@ChannelId,");
            if (pIsUpdateNullField || pEntity.DeviceToken!=null)
                strSql.Append( "[DeviceToken]=@DeviceToken,");
            if (pIsUpdateNullField || pEntity.SessionID!=null)
                strSql.Append( "[SessionID]=@SessionID,");
            if (pIsUpdateNullField || pEntity.Locale!=null)
                strSql.Append( "[Locale]=@Locale,");
            if (pIsUpdateNullField || pEntity.OSInfo!=null)
                strSql.Append( "[OSInfo]=@OSInfo,");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.SpecialParams!=null)
                strSql.Append( "[SpecialParams]=@SpecialParams,");
            if (pIsUpdateNullField || pEntity.ResultCode!=null)
                strSql.Append( "[ResultCode]=@ResultCode,");
            if (pIsUpdateNullField || pEntity.ResultDescription!=null)
                strSql.Append( "[ResultDescription]=@ResultDescription,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.WeiXinId!=null)
                strSql.Append( "[WeiXinId]=@WeiXinId,");
            if (pIsUpdateNullField || pEntity.OpenId!=null)
                strSql.Append( "[OpenId]=@OpenId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where LogsID=@LogsID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@LogType",SqlDbType.NVarChar),
					new SqlParameter("@Plat",SqlDbType.NVarChar),
					new SqlParameter("@Version",SqlDbType.NVarChar),
					new SqlParameter("@IpAddress",SqlDbType.NVarChar),
					new SqlParameter("@ChannelId",SqlDbType.NVarChar),
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@SessionID",SqlDbType.NVarChar),
					new SqlParameter("@Locale",SqlDbType.NVarChar),
					new SqlParameter("@OSInfo",SqlDbType.NVarChar),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@SpecialParams",SqlDbType.NVarChar),
					new SqlParameter("@ResultCode",SqlDbType.NVarChar),
					new SqlParameter("@ResultDescription",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@WeiXinId",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@LogsID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.UserID;
			parameters[1].Value = pEntity.LogType;
			parameters[2].Value = pEntity.Plat;
			parameters[3].Value = pEntity.Version;
			parameters[4].Value = pEntity.IpAddress;
			parameters[5].Value = pEntity.ChannelId;
			parameters[6].Value = pEntity.DeviceToken;
			parameters[7].Value = pEntity.SessionID;
			parameters[8].Value = pEntity.Locale;
			parameters[9].Value = pEntity.OSInfo;
			parameters[10].Value = pEntity.ItemID;
			parameters[11].Value = pEntity.SpecialParams;
			parameters[12].Value = pEntity.ResultCode;
			parameters[13].Value = pEntity.ResultDescription;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.WeiXinId;
			parameters[17].Value = pEntity.OpenId;
			parameters[18].Value = pEntity.LogsID;

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
        public void Update(SysVisitLogsEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(SysVisitLogsEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(SysVisitLogsEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(SysVisitLogsEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.LogsID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.LogsID, pTran);           
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
            sql.AppendLine("update [SysVisitLogs] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where LogsID=@LogsID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@LogsID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(SysVisitLogsEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.LogsID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.LogsID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(SysVisitLogsEntity[] pEntities)
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
            sql.AppendLine("update [SysVisitLogs] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where LogsID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public SysVisitLogsEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [SysVisitLogs] where isdelete=0 ");
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
            List<SysVisitLogsEntity> list = new List<SysVisitLogsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVisitLogsEntity m;
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
        public PagedQueryResult<SysVisitLogsEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [LogsID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [SysVisitLogs] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [SysVisitLogs] where isdelete=0 ");
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
            PagedQueryResult<SysVisitLogsEntity> result = new PagedQueryResult<SysVisitLogsEntity>();
            List<SysVisitLogsEntity> list = new List<SysVisitLogsEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVisitLogsEntity m;
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
        public SysVisitLogsEntity[] QueryByEntity(SysVisitLogsEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<SysVisitLogsEntity> PagedQueryByEntity(SysVisitLogsEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(SysVisitLogsEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.LogsID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogsID", Value = pQueryEntity.LogsID });
            if (pQueryEntity.UserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
            if (pQueryEntity.LogType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogType", Value = pQueryEntity.LogType });
            if (pQueryEntity.Plat!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Plat", Value = pQueryEntity.Plat });
            if (pQueryEntity.Version!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Version", Value = pQueryEntity.Version });
            if (pQueryEntity.IpAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IpAddress", Value = pQueryEntity.IpAddress });
            if (pQueryEntity.ChannelId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ChannelId", Value = pQueryEntity.ChannelId });
            if (pQueryEntity.DeviceToken!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeviceToken", Value = pQueryEntity.DeviceToken });
            if (pQueryEntity.SessionID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SessionID", Value = pQueryEntity.SessionID });
            if (pQueryEntity.Locale!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Locale", Value = pQueryEntity.Locale });
            if (pQueryEntity.OSInfo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OSInfo", Value = pQueryEntity.OSInfo });
            if (pQueryEntity.ItemID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.SpecialParams!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SpecialParams", Value = pQueryEntity.SpecialParams });
            if (pQueryEntity.ResultCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ResultCode", Value = pQueryEntity.ResultCode });
            if (pQueryEntity.ResultDescription!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ResultDescription", Value = pQueryEntity.ResultDescription });
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
            if (pQueryEntity.WeiXinId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "WeiXinId", Value = pQueryEntity.WeiXinId });
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out SysVisitLogsEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new SysVisitLogsEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["LogsID"] != DBNull.Value)
			{
				pInstance.LogsID =  Convert.ToString(pReader["LogsID"]);
			}
			if (pReader["UserID"] != DBNull.Value)
			{
				pInstance.UserID =  Convert.ToString(pReader["UserID"]);
			}
			if (pReader["LogType"] != DBNull.Value)
			{
				pInstance.LogType =  Convert.ToString(pReader["LogType"]);
			}
			if (pReader["Plat"] != DBNull.Value)
			{
				pInstance.Plat =  Convert.ToString(pReader["Plat"]);
			}
			if (pReader["Version"] != DBNull.Value)
			{
				pInstance.Version =  Convert.ToString(pReader["Version"]);
			}
			if (pReader["IpAddress"] != DBNull.Value)
			{
				pInstance.IpAddress =  Convert.ToString(pReader["IpAddress"]);
			}
			if (pReader["ChannelId"] != DBNull.Value)
			{
				pInstance.ChannelId =  Convert.ToString(pReader["ChannelId"]);
			}
			if (pReader["DeviceToken"] != DBNull.Value)
			{
				pInstance.DeviceToken =  Convert.ToString(pReader["DeviceToken"]);
			}
			if (pReader["SessionID"] != DBNull.Value)
			{
				pInstance.SessionID =  Convert.ToString(pReader["SessionID"]);
			}
			if (pReader["Locale"] != DBNull.Value)
			{
				pInstance.Locale =  Convert.ToString(pReader["Locale"]);
			}
			if (pReader["OSInfo"] != DBNull.Value)
			{
				pInstance.OSInfo =  Convert.ToString(pReader["OSInfo"]);
			}
			if (pReader["ItemID"] != DBNull.Value)
			{
				pInstance.ItemID =  Convert.ToString(pReader["ItemID"]);
			}
			if (pReader["SpecialParams"] != DBNull.Value)
			{
				pInstance.SpecialParams =  Convert.ToString(pReader["SpecialParams"]);
			}
			if (pReader["ResultCode"] != DBNull.Value)
			{
				pInstance.ResultCode =  Convert.ToString(pReader["ResultCode"]);
			}
			if (pReader["ResultDescription"] != DBNull.Value)
			{
				pInstance.ResultDescription =  Convert.ToString(pReader["ResultDescription"]);
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
			if (pReader["WeiXinId"] != DBNull.Value)
			{
				pInstance.WeiXinId =  Convert.ToString(pReader["WeiXinId"]);
			}
			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}

        }
        #endregion
    }
}
