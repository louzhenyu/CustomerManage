/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/21 11:26:41
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
    /// ��PushIOSMessage�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PushIOSMessageDAO : Base.BaseCPOSDAO, ICRUDable<PushIOSMessageEntity>, IQueryable<PushIOSMessageEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public PushIOSMessageDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(PushIOSMessageEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(PushIOSMessageEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [PushIOSMessage](");
            strSql.Append("[DeviceToken],[UserID],[ConnUserID],[ItemType],[ItemID],[MessageText],[SendCount],[Status],[FailureReason],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[MessagePushType],[IsVersion1],[IsVersion2],[IOSMessageID])");
            strSql.Append(" values (");
            strSql.Append("@DeviceToken,@UserID,@ConnUserID,@ItemType,@ItemID,@MessageText,@SendCount,@Status,@FailureReason,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@MessagePushType,@IsVersion1,@IsVersion2,@IOSMessageID)");            

			string pkString = pEntity.IOSMessageID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@ConnUserID",SqlDbType.NVarChar),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@MessageText",SqlDbType.NVarChar),
					new SqlParameter("@SendCount",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@MessagePushType",SqlDbType.Int),
					new SqlParameter("@IsVersion1",SqlDbType.Int),
					new SqlParameter("@IsVersion2",SqlDbType.Int),
					new SqlParameter("@IOSMessageID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.DeviceToken;
			parameters[1].Value = pEntity.UserID;
			parameters[2].Value = pEntity.ConnUserID;
			parameters[3].Value = pEntity.ItemType;
			parameters[4].Value = pEntity.ItemID;
			parameters[5].Value = pEntity.MessageText;
			parameters[6].Value = pEntity.SendCount;
			parameters[7].Value = pEntity.Status;
			parameters[8].Value = pEntity.FailureReason;
			parameters[9].Value = pEntity.CreateTime;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.IsDelete;
			parameters[14].Value = pEntity.MessagePushType;
			parameters[15].Value = pEntity.IsVersion1;
			parameters[16].Value = pEntity.IsVersion2;
			parameters[17].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.IOSMessageID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public PushIOSMessageEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushIOSMessage] where IOSMessageID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            PushIOSMessageEntity m = null;
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
        public PushIOSMessageEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushIOSMessage] where isdelete=0");
            //��ȡ����
            List<PushIOSMessageEntity> list = new List<PushIOSMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PushIOSMessageEntity m;
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
        public void Update(PushIOSMessageEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(PushIOSMessageEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.IOSMessageID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [PushIOSMessage] set ");
            if (pIsUpdateNullField || pEntity.DeviceToken!=null)
                strSql.Append( "[DeviceToken]=@DeviceToken,");
            if (pIsUpdateNullField || pEntity.UserID!=null)
                strSql.Append( "[UserID]=@UserID,");
            if (pIsUpdateNullField || pEntity.ConnUserID!=null)
                strSql.Append( "[ConnUserID]=@ConnUserID,");
            if (pIsUpdateNullField || pEntity.ItemType!=null)
                strSql.Append( "[ItemType]=@ItemType,");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.MessageText!=null)
                strSql.Append( "[MessageText]=@MessageText,");
            if (pIsUpdateNullField || pEntity.SendCount!=null)
                strSql.Append( "[SendCount]=@SendCount,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.FailureReason!=null)
                strSql.Append( "[FailureReason]=@FailureReason,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.MessagePushType!=null)
                strSql.Append( "[MessagePushType]=@MessagePushType,");
            if (pIsUpdateNullField || pEntity.IsVersion1!=null)
                strSql.Append( "[IsVersion1]=@IsVersion1,");
            if (pIsUpdateNullField || pEntity.IsVersion2!=null)
                strSql.Append( "[IsVersion2]=@IsVersion2");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where IOSMessageID=@IOSMessageID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@DeviceToken",SqlDbType.NVarChar),
					new SqlParameter("@UserID",SqlDbType.NVarChar),
					new SqlParameter("@ConnUserID",SqlDbType.NVarChar),
					new SqlParameter("@ItemType",SqlDbType.Int),
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
					new SqlParameter("@MessageText",SqlDbType.NVarChar),
					new SqlParameter("@SendCount",SqlDbType.Int),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@FailureReason",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@MessagePushType",SqlDbType.Int),
					new SqlParameter("@IsVersion1",SqlDbType.Int),
					new SqlParameter("@IsVersion2",SqlDbType.Int),
					new SqlParameter("@IOSMessageID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.DeviceToken;
			parameters[1].Value = pEntity.UserID;
			parameters[2].Value = pEntity.ConnUserID;
			parameters[3].Value = pEntity.ItemType;
			parameters[4].Value = pEntity.ItemID;
			parameters[5].Value = pEntity.MessageText;
			parameters[6].Value = pEntity.SendCount;
			parameters[7].Value = pEntity.Status;
			parameters[8].Value = pEntity.FailureReason;
			parameters[9].Value = pEntity.LastUpdateBy;
			parameters[10].Value = pEntity.LastUpdateTime;
			parameters[11].Value = pEntity.MessagePushType;
			parameters[12].Value = pEntity.IsVersion1;
			parameters[13].Value = pEntity.IsVersion2;
			parameters[14].Value = pEntity.IOSMessageID;

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
        public void Update(PushIOSMessageEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(PushIOSMessageEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(PushIOSMessageEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(PushIOSMessageEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.IOSMessageID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.IOSMessageID, pTran);           
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
            sql.AppendLine("update [PushIOSMessage] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where IOSMessageID=@IOSMessageID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@IOSMessageID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(PushIOSMessageEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.IOSMessageID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.IOSMessageID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(PushIOSMessageEntity[] pEntities)
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
            sql.AppendLine("update [PushIOSMessage] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where IOSMessageID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public PushIOSMessageEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PushIOSMessage] where isdelete=0 ");
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
            List<PushIOSMessageEntity> list = new List<PushIOSMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    PushIOSMessageEntity m;
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
        public PagedQueryResult<PushIOSMessageEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [IOSMessageID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [PushIOSMessage] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [PushIOSMessage] where isdelete=0 ");
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
            PagedQueryResult<PushIOSMessageEntity> result = new PagedQueryResult<PushIOSMessageEntity>();
            List<PushIOSMessageEntity> list = new List<PushIOSMessageEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    PushIOSMessageEntity m;
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
        public PushIOSMessageEntity[] QueryByEntity(PushIOSMessageEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<PushIOSMessageEntity> PagedQueryByEntity(PushIOSMessageEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(PushIOSMessageEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.IOSMessageID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IOSMessageID", Value = pQueryEntity.IOSMessageID });
            if (pQueryEntity.DeviceToken!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeviceToken", Value = pQueryEntity.DeviceToken });
            if (pQueryEntity.UserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UserID", Value = pQueryEntity.UserID });
            if (pQueryEntity.ConnUserID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConnUserID", Value = pQueryEntity.ConnUserID });
            if (pQueryEntity.ItemType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemType", Value = pQueryEntity.ItemType });
            if (pQueryEntity.ItemID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.MessageText!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessageText", Value = pQueryEntity.MessageText });
            if (pQueryEntity.SendCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SendCount", Value = pQueryEntity.SendCount });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.FailureReason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FailureReason", Value = pQueryEntity.FailureReason });
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
            if (pQueryEntity.MessagePushType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MessagePushType", Value = pQueryEntity.MessagePushType });
            if (pQueryEntity.IsVersion1!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsVersion1", Value = pQueryEntity.IsVersion1 });
            if (pQueryEntity.IsVersion2!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsVersion2", Value = pQueryEntity.IsVersion2 });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out PushIOSMessageEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new PushIOSMessageEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["IOSMessageID"] != DBNull.Value)
			{
				pInstance.IOSMessageID =  Convert.ToString(pReader["IOSMessageID"]);
			}
			if (pReader["DeviceToken"] != DBNull.Value)
			{
				pInstance.DeviceToken =  Convert.ToString(pReader["DeviceToken"]);
			}
			if (pReader["UserID"] != DBNull.Value)
			{
				pInstance.UserID =  Convert.ToString(pReader["UserID"]);
			}
			if (pReader["ConnUserID"] != DBNull.Value)
			{
				pInstance.ConnUserID =  Convert.ToString(pReader["ConnUserID"]);
			}
			if (pReader["ItemType"] != DBNull.Value)
			{
				pInstance.ItemType =   Convert.ToInt32(pReader["ItemType"]);
			}
			if (pReader["ItemID"] != DBNull.Value)
			{
				pInstance.ItemID =  Convert.ToString(pReader["ItemID"]);
			}
			if (pReader["MessageText"] != DBNull.Value)
			{
				pInstance.MessageText =  Convert.ToString(pReader["MessageText"]);
			}
			if (pReader["SendCount"] != DBNull.Value)
			{
				pInstance.SendCount =   Convert.ToInt32(pReader["SendCount"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["FailureReason"] != DBNull.Value)
			{
				pInstance.FailureReason =  Convert.ToString(pReader["FailureReason"]);
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
			if (pReader["MessagePushType"] != DBNull.Value)
			{
				pInstance.MessagePushType =   Convert.ToInt32(pReader["MessagePushType"]);
			}
			if (pReader["IsVersion1"] != DBNull.Value)
			{
				pInstance.IsVersion1 =   Convert.ToInt32(pReader["IsVersion1"]);
			}
			if (pReader["IsVersion2"] != DBNull.Value)
			{
				pInstance.IsVersion2 =   Convert.ToInt32(pReader["IsVersion2"]);
			}

        }
        #endregion
    }
}
