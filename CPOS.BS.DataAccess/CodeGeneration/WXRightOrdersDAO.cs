/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/10 10:39:07
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
    /// ��WXRightOrders�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXRightOrdersDAO : BaseCPOSDAO, ICRUDable<WXRightOrdersEntity>, IQueryable<WXRightOrdersEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXRightOrdersDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXRightOrdersEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXRightOrdersEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXRightOrders](");
            strSql.Append("[VipId],[AppId],[OpenId],[FeedBackId],[TransId],[Reason],[Solution],[ExtInfo],[TimeStamp],[MsgType],[AppSignature],[Status],[HandleBy],[HandlePlan],[HandleTime],[AssignBy],[AssignPlan],[AssignTime],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[CustomerId],[ConfirmReason],[RightOrdersId])");
            strSql.Append(" values (");
            strSql.Append("@VipId,@AppId,@OpenId,@FeedBackId,@TransId,@Reason,@Solution,@ExtInfo,@TimeStamp,@MsgType,@AppSignature,@Status,@HandleBy,@HandlePlan,@HandleTime,@AssignBy,@AssignPlan,@AssignTime,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@CustomerId,@ConfirmReason,@RightOrdersId)");            

			Guid? pkGuid;
			if (pEntity.RightOrdersId == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.RightOrdersId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@AppId",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@FeedBackId",SqlDbType.NVarChar),
					new SqlParameter("@TransId",SqlDbType.NVarChar),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@Solution",SqlDbType.NVarChar),
					new SqlParameter("@ExtInfo",SqlDbType.NVarChar),
					new SqlParameter("@TimeStamp",SqlDbType.Int),
					new SqlParameter("@MsgType",SqlDbType.NVarChar),
					new SqlParameter("@AppSignature",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@HandleBy",SqlDbType.NVarChar),
					new SqlParameter("@HandlePlan",SqlDbType.NVarChar),
					new SqlParameter("@HandleTime",SqlDbType.DateTime),
					new SqlParameter("@AssignBy",SqlDbType.NVarChar),
					new SqlParameter("@AssignPlan",SqlDbType.NVarChar),
					new SqlParameter("@AssignTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@ConfirmReason",SqlDbType.NVarChar),
					new SqlParameter("@RightOrdersId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipId;
			parameters[1].Value = pEntity.AppId;
			parameters[2].Value = pEntity.OpenId;
			parameters[3].Value = pEntity.FeedBackId;
			parameters[4].Value = pEntity.TransId;
			parameters[5].Value = pEntity.Reason;
			parameters[6].Value = pEntity.Solution;
			parameters[7].Value = pEntity.ExtInfo;
			parameters[8].Value = pEntity.TimeStamp;
			parameters[9].Value = pEntity.MsgType;
			parameters[10].Value = pEntity.AppSignature;
			parameters[11].Value = pEntity.Status;
			parameters[12].Value = pEntity.HandleBy;
			parameters[13].Value = pEntity.HandlePlan;
			parameters[14].Value = pEntity.HandleTime;
			parameters[15].Value = pEntity.AssignBy;
			parameters[16].Value = pEntity.AssignPlan;
			parameters[17].Value = pEntity.AssignTime;
			parameters[18].Value = pEntity.CreateBy;
			parameters[19].Value = pEntity.CreateTime;
			parameters[20].Value = pEntity.LastUpdateBy;
			parameters[21].Value = pEntity.LastUpdateTime;
			parameters[22].Value = pEntity.IsDelete;
			parameters[23].Value = pEntity.CustomerId;
			parameters[24].Value = pEntity.ConfirmReason;
			parameters[25].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.RightOrdersId = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXRightOrdersEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXRightOrders] where RightOrdersId='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            WXRightOrdersEntity m = null;
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
        public WXRightOrdersEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXRightOrders] where 1=1  and isdelete=0");
            //��ȡ����
            List<WXRightOrdersEntity> list = new List<WXRightOrdersEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXRightOrdersEntity m;
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
        public void Update(WXRightOrdersEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(WXRightOrdersEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RightOrdersId.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXRightOrders] set ");
                        if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.AppId!=null)
                strSql.Append( "[AppId]=@AppId,");
            if (pIsUpdateNullField || pEntity.OpenId!=null)
                strSql.Append( "[OpenId]=@OpenId,");
            if (pIsUpdateNullField || pEntity.FeedBackId!=null)
                strSql.Append( "[FeedBackId]=@FeedBackId,");
            if (pIsUpdateNullField || pEntity.TransId!=null)
                strSql.Append( "[TransId]=@TransId,");
            if (pIsUpdateNullField || pEntity.Reason!=null)
                strSql.Append( "[Reason]=@Reason,");
            if (pIsUpdateNullField || pEntity.Solution!=null)
                strSql.Append( "[Solution]=@Solution,");
            if (pIsUpdateNullField || pEntity.ExtInfo!=null)
                strSql.Append( "[ExtInfo]=@ExtInfo,");
            if (pIsUpdateNullField || pEntity.TimeStamp!=null)
                strSql.Append( "[TimeStamp]=@TimeStamp,");
            if (pIsUpdateNullField || pEntity.MsgType!=null)
                strSql.Append( "[MsgType]=@MsgType,");
            if (pIsUpdateNullField || pEntity.AppSignature!=null)
                strSql.Append( "[AppSignature]=@AppSignature,");
            if (pIsUpdateNullField || pEntity.Status!=null)
                strSql.Append( "[Status]=@Status,");
            if (pIsUpdateNullField || pEntity.HandleBy!=null)
                strSql.Append( "[HandleBy]=@HandleBy,");
            if (pIsUpdateNullField || pEntity.HandlePlan!=null)
                strSql.Append( "[HandlePlan]=@HandlePlan,");
            if (pIsUpdateNullField || pEntity.HandleTime!=null)
                strSql.Append( "[HandleTime]=@HandleTime,");
            if (pIsUpdateNullField || pEntity.AssignBy!=null)
                strSql.Append( "[AssignBy]=@AssignBy,");
            if (pIsUpdateNullField || pEntity.AssignPlan!=null)
                strSql.Append( "[AssignPlan]=@AssignPlan,");
            if (pIsUpdateNullField || pEntity.AssignTime!=null)
                strSql.Append( "[AssignTime]=@AssignTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId,");
            if (pIsUpdateNullField || pEntity.ConfirmReason!=null)
                strSql.Append( "[ConfirmReason]=@ConfirmReason");
            strSql.Append(" where RightOrdersId=@RightOrdersId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@AppId",SqlDbType.NVarChar),
					new SqlParameter("@OpenId",SqlDbType.NVarChar),
					new SqlParameter("@FeedBackId",SqlDbType.NVarChar),
					new SqlParameter("@TransId",SqlDbType.NVarChar),
					new SqlParameter("@Reason",SqlDbType.NVarChar),
					new SqlParameter("@Solution",SqlDbType.NVarChar),
					new SqlParameter("@ExtInfo",SqlDbType.NVarChar),
					new SqlParameter("@TimeStamp",SqlDbType.Int),
					new SqlParameter("@MsgType",SqlDbType.NVarChar),
					new SqlParameter("@AppSignature",SqlDbType.NVarChar),
					new SqlParameter("@Status",SqlDbType.Int),
					new SqlParameter("@HandleBy",SqlDbType.NVarChar),
					new SqlParameter("@HandlePlan",SqlDbType.NVarChar),
					new SqlParameter("@HandleTime",SqlDbType.DateTime),
					new SqlParameter("@AssignBy",SqlDbType.NVarChar),
					new SqlParameter("@AssignPlan",SqlDbType.NVarChar),
					new SqlParameter("@AssignTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@ConfirmReason",SqlDbType.NVarChar),
					new SqlParameter("@RightOrdersId",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.VipId;
			parameters[1].Value = pEntity.AppId;
			parameters[2].Value = pEntity.OpenId;
			parameters[3].Value = pEntity.FeedBackId;
			parameters[4].Value = pEntity.TransId;
			parameters[5].Value = pEntity.Reason;
			parameters[6].Value = pEntity.Solution;
			parameters[7].Value = pEntity.ExtInfo;
			parameters[8].Value = pEntity.TimeStamp;
			parameters[9].Value = pEntity.MsgType;
			parameters[10].Value = pEntity.AppSignature;
			parameters[11].Value = pEntity.Status;
			parameters[12].Value = pEntity.HandleBy;
			parameters[13].Value = pEntity.HandlePlan;
			parameters[14].Value = pEntity.HandleTime;
			parameters[15].Value = pEntity.AssignBy;
			parameters[16].Value = pEntity.AssignPlan;
			parameters[17].Value = pEntity.AssignTime;
			parameters[18].Value = pEntity.LastUpdateBy;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.CustomerId;
			parameters[21].Value = pEntity.ConfirmReason;
			parameters[22].Value = pEntity.RightOrdersId;

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
        public void Update(WXRightOrdersEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXRightOrdersEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXRightOrdersEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.RightOrdersId.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.RightOrdersId.Value, pTran);           
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
            sql.AppendLine("update [WXRightOrders] set  isdelete=1 where RightOrdersId=@RightOrdersId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@RightOrdersId",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXRightOrdersEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.RightOrdersId.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.RightOrdersId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXRightOrdersEntity[] pEntities)
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
            sql.AppendLine("update [WXRightOrders] set  isdelete=1 where RightOrdersId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXRightOrdersEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXRightOrders] where 1=1  and isdelete=0 ");
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
            List<WXRightOrdersEntity> list = new List<WXRightOrdersEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXRightOrdersEntity m;
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
        public PagedQueryResult<WXRightOrdersEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [RightOrdersId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXRightOrders] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXRightOrders] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<WXRightOrdersEntity> result = new PagedQueryResult<WXRightOrdersEntity>();
            List<WXRightOrdersEntity> list = new List<WXRightOrdersEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXRightOrdersEntity m;
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
        public WXRightOrdersEntity[] QueryByEntity(WXRightOrdersEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXRightOrdersEntity> PagedQueryByEntity(WXRightOrdersEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXRightOrdersEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.RightOrdersId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RightOrdersId", Value = pQueryEntity.RightOrdersId });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.AppId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppId", Value = pQueryEntity.AppId });
            if (pQueryEntity.OpenId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OpenId", Value = pQueryEntity.OpenId });
            if (pQueryEntity.FeedBackId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FeedBackId", Value = pQueryEntity.FeedBackId });
            if (pQueryEntity.TransId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransId", Value = pQueryEntity.TransId });
            if (pQueryEntity.Reason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Reason", Value = pQueryEntity.Reason });
            if (pQueryEntity.Solution!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Solution", Value = pQueryEntity.Solution });
            if (pQueryEntity.ExtInfo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExtInfo", Value = pQueryEntity.ExtInfo });
            if (pQueryEntity.TimeStamp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TimeStamp", Value = pQueryEntity.TimeStamp });
            if (pQueryEntity.MsgType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MsgType", Value = pQueryEntity.MsgType });
            if (pQueryEntity.AppSignature!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AppSignature", Value = pQueryEntity.AppSignature });
            if (pQueryEntity.Status!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Status", Value = pQueryEntity.Status });
            if (pQueryEntity.HandleBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HandleBy", Value = pQueryEntity.HandleBy });
            if (pQueryEntity.HandlePlan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HandlePlan", Value = pQueryEntity.HandlePlan });
            if (pQueryEntity.HandleTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HandleTime", Value = pQueryEntity.HandleTime });
            if (pQueryEntity.AssignBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AssignBy", Value = pQueryEntity.AssignBy });
            if (pQueryEntity.AssignPlan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AssignPlan", Value = pQueryEntity.AssignPlan });
            if (pQueryEntity.AssignTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AssignTime", Value = pQueryEntity.AssignTime });
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
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });
            if (pQueryEntity.ConfirmReason!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ConfirmReason", Value = pQueryEntity.ConfirmReason });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out WXRightOrdersEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXRightOrdersEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["RightOrdersId"] != DBNull.Value)
			{
				pInstance.RightOrdersId =  (Guid)pReader["RightOrdersId"];
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["AppId"] != DBNull.Value)
			{
				pInstance.AppId =  Convert.ToString(pReader["AppId"]);
			}
			if (pReader["OpenId"] != DBNull.Value)
			{
				pInstance.OpenId =  Convert.ToString(pReader["OpenId"]);
			}
			if (pReader["FeedBackId"] != DBNull.Value)
			{
				pInstance.FeedBackId =  Convert.ToString(pReader["FeedBackId"]);
			}
			if (pReader["TransId"] != DBNull.Value)
			{
				pInstance.TransId =  Convert.ToString(pReader["TransId"]);
			}
			if (pReader["Reason"] != DBNull.Value)
			{
				pInstance.Reason =  Convert.ToString(pReader["Reason"]);
			}
			if (pReader["Solution"] != DBNull.Value)
			{
				pInstance.Solution =  Convert.ToString(pReader["Solution"]);
			}
			if (pReader["ExtInfo"] != DBNull.Value)
			{
				pInstance.ExtInfo =  Convert.ToString(pReader["ExtInfo"]);
			}
			if (pReader["TimeStamp"] != DBNull.Value)
			{
				pInstance.TimeStamp =   Convert.ToInt32(pReader["TimeStamp"]);
			}
			if (pReader["MsgType"] != DBNull.Value)
			{
				pInstance.MsgType =  Convert.ToString(pReader["MsgType"]);
			}
			if (pReader["AppSignature"] != DBNull.Value)
			{
				pInstance.AppSignature =  Convert.ToString(pReader["AppSignature"]);
			}
			if (pReader["Status"] != DBNull.Value)
			{
				pInstance.Status =   Convert.ToInt32(pReader["Status"]);
			}
			if (pReader["HandleBy"] != DBNull.Value)
			{
				pInstance.HandleBy =  Convert.ToString(pReader["HandleBy"]);
			}
			if (pReader["HandlePlan"] != DBNull.Value)
			{
				pInstance.HandlePlan =  Convert.ToString(pReader["HandlePlan"]);
			}
			if (pReader["HandleTime"] != DBNull.Value)
			{
				pInstance.HandleTime =  Convert.ToDateTime(pReader["HandleTime"]);
			}
			if (pReader["AssignBy"] != DBNull.Value)
			{
				pInstance.AssignBy =  Convert.ToString(pReader["AssignBy"]);
			}
			if (pReader["AssignPlan"] != DBNull.Value)
			{
				pInstance.AssignPlan =  Convert.ToString(pReader["AssignPlan"]);
			}
			if (pReader["AssignTime"] != DBNull.Value)
			{
				pInstance.AssignTime =  Convert.ToDateTime(pReader["AssignTime"]);
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
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}
			if (pReader["ConfirmReason"] != DBNull.Value)
			{
				pInstance.ConfirmReason =  Convert.ToString(pReader["ConfirmReason"]);
			}

        }
        #endregion
    }
}
