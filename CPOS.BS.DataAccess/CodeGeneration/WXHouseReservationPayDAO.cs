/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/24 17:30:30
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
    /// ��WXHouseReservationPay�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseReservationPayDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseReservationPayEntity>, IQueryable<WXHouseReservationPayEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseReservationPayDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXHouseReservationPayEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXHouseReservationPayEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXHouseReservationPay](");
            strSql.Append("[PrePaymentID],[SeqNO],[Fundtype],[FundState],[Merchantdate],[Hatradedt],[HaorderNO],[Retcode],[Retmsg],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ReservationPayID])");
            strSql.Append(" values (");
            strSql.Append("@PrePaymentID,@SeqNO,@Fundtype,@FundState,@Merchantdate,@Hatradedt,@HaorderNO,@Retcode,@Retmsg,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ReservationPayID)");            

			Guid? pkGuid;
			if (pEntity.ReservationPayID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ReservationPayID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrePaymentID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SeqNO",SqlDbType.NVarChar),
					new SqlParameter("@Fundtype",SqlDbType.Int),
					new SqlParameter("@FundState",SqlDbType.Int),
					new SqlParameter("@Merchantdate",SqlDbType.VarChar),
					new SqlParameter("@Hatradedt",SqlDbType.Int),
					new SqlParameter("@HaorderNO",SqlDbType.NVarChar),
					new SqlParameter("@Retcode",SqlDbType.VarChar),
					new SqlParameter("@Retmsg",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ReservationPayID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.PrePaymentID;
			parameters[1].Value = pEntity.SeqNO;
			parameters[2].Value = pEntity.Fundtype;
			parameters[3].Value = pEntity.FundState;
			parameters[4].Value = pEntity.Merchantdate;
			parameters[5].Value = pEntity.Hatradedt;
			parameters[6].Value = pEntity.HaorderNO;
			parameters[7].Value = pEntity.Retcode;
			parameters[8].Value = pEntity.Retmsg;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.IsDelete;
			parameters[15].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ReservationPayID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseReservationPayEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseReservationPay] where ReservationPayID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WXHouseReservationPayEntity m = null;
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
        public WXHouseReservationPayEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseReservationPay] where isdelete=0");
            //��ȡ����
            List<WXHouseReservationPayEntity> list = new List<WXHouseReservationPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseReservationPayEntity m;
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
        public void Update(WXHouseReservationPayEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WXHouseReservationPayEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReservationPayID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXHouseReservationPay] set ");
            if (pIsUpdateNullField || pEntity.PrePaymentID!=null)
                strSql.Append( "[PrePaymentID]=@PrePaymentID,");
            if (pIsUpdateNullField || pEntity.SeqNO!=null)
                strSql.Append( "[SeqNO]=@SeqNO,");
            if (pIsUpdateNullField || pEntity.Fundtype!=null)
                strSql.Append( "[Fundtype]=@Fundtype,");
            if (pIsUpdateNullField || pEntity.FundState!=null)
                strSql.Append( "[FundState]=@FundState,");
            if (pIsUpdateNullField || pEntity.Merchantdate!=null)
                strSql.Append( "[Merchantdate]=@Merchantdate,");
            if (pIsUpdateNullField || pEntity.Hatradedt!=null)
                strSql.Append( "[Hatradedt]=@Hatradedt,");
            if (pIsUpdateNullField || pEntity.HaorderNO!=null)
                strSql.Append( "[HaorderNO]=@HaorderNO,");
            if (pIsUpdateNullField || pEntity.Retcode!=null)
                strSql.Append( "[Retcode]=@Retcode,");
            if (pIsUpdateNullField || pEntity.Retmsg!=null)
                strSql.Append( "[Retmsg]=@Retmsg,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ReservationPayID=@ReservationPayID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@PrePaymentID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@SeqNO",SqlDbType.NVarChar),
					new SqlParameter("@Fundtype",SqlDbType.Int),
					new SqlParameter("@FundState",SqlDbType.Int),
					new SqlParameter("@Merchantdate",SqlDbType.VarChar),
					new SqlParameter("@Hatradedt",SqlDbType.Int),
					new SqlParameter("@HaorderNO",SqlDbType.NVarChar),
					new SqlParameter("@Retcode",SqlDbType.VarChar),
					new SqlParameter("@Retmsg",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ReservationPayID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.PrePaymentID;
			parameters[1].Value = pEntity.SeqNO;
			parameters[2].Value = pEntity.Fundtype;
			parameters[3].Value = pEntity.FundState;
			parameters[4].Value = pEntity.Merchantdate;
			parameters[5].Value = pEntity.Hatradedt;
			parameters[6].Value = pEntity.HaorderNO;
			parameters[7].Value = pEntity.Retcode;
			parameters[8].Value = pEntity.Retmsg;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.ReservationPayID;

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
        public void Update(WXHouseReservationPayEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WXHouseReservationPayEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXHouseReservationPayEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXHouseReservationPayEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ReservationPayID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ReservationPayID, pTran);           
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
            sql.AppendLine("update [WXHouseReservationPay] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ReservationPayID=@ReservationPayID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ReservationPayID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXHouseReservationPayEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ReservationPayID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ReservationPayID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXHouseReservationPayEntity[] pEntities)
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
            sql.AppendLine("update [WXHouseReservationPay] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ReservationPayID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXHouseReservationPayEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseReservationPay] where isdelete=0 ");
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
            List<WXHouseReservationPayEntity> list = new List<WXHouseReservationPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseReservationPayEntity m;
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
        public PagedQueryResult<WXHouseReservationPayEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ReservationPayID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXHouseReservationPay] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXHouseReservationPay] where isdelete=0 ");
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
            PagedQueryResult<WXHouseReservationPayEntity> result = new PagedQueryResult<WXHouseReservationPayEntity>();
            List<WXHouseReservationPayEntity> list = new List<WXHouseReservationPayEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseReservationPayEntity m;
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
        public WXHouseReservationPayEntity[] QueryByEntity(WXHouseReservationPayEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXHouseReservationPayEntity> PagedQueryByEntity(WXHouseReservationPayEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXHouseReservationPayEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ReservationPayID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ReservationPayID", Value = pQueryEntity.ReservationPayID });
            if (pQueryEntity.PrePaymentID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrePaymentID", Value = pQueryEntity.PrePaymentID });
            if (pQueryEntity.SeqNO!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SeqNO", Value = pQueryEntity.SeqNO });
            if (pQueryEntity.Fundtype!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Fundtype", Value = pQueryEntity.Fundtype });
            if (pQueryEntity.FundState!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FundState", Value = pQueryEntity.FundState });
            if (pQueryEntity.Merchantdate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Merchantdate", Value = pQueryEntity.Merchantdate });
            if (pQueryEntity.Hatradedt!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Hatradedt", Value = pQueryEntity.Hatradedt });
            if (pQueryEntity.HaorderNO!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HaorderNO", Value = pQueryEntity.HaorderNO });
            if (pQueryEntity.Retcode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Retcode", Value = pQueryEntity.Retcode });
            if (pQueryEntity.Retmsg!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Retmsg", Value = pQueryEntity.Retmsg });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out WXHouseReservationPayEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXHouseReservationPayEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ReservationPayID"] != DBNull.Value)
			{
				pInstance.ReservationPayID =  (Guid)pReader["ReservationPayID"];
			}
			if (pReader["PrePaymentID"] != DBNull.Value)
			{
				pInstance.PrePaymentID =  (Guid)pReader["PrePaymentID"];
			}
			if (pReader["SeqNO"] != DBNull.Value)
			{
				pInstance.SeqNO =  Convert.ToString(pReader["SeqNO"]);
			}
			if (pReader["Fundtype"] != DBNull.Value)
			{
				pInstance.Fundtype =   Convert.ToInt32(pReader["Fundtype"]);
			}
			if (pReader["FundState"] != DBNull.Value)
			{
				pInstance.FundState =   Convert.ToInt32(pReader["FundState"]);
			}
			if (pReader["Merchantdate"] != DBNull.Value)
			{
				pInstance.Merchantdate =  Convert.ToString(pReader["Merchantdate"]);
			}
			if (pReader["Hatradedt"] != DBNull.Value)
			{
				pInstance.Hatradedt =   Convert.ToInt32(pReader["Hatradedt"]);
			}
			if (pReader["HaorderNO"] != DBNull.Value)
			{
				pInstance.HaorderNO =  Convert.ToString(pReader["HaorderNO"]);
			}
			if (pReader["Retcode"] != DBNull.Value)
			{
				pInstance.Retcode =  Convert.ToString(pReader["Retcode"]);
			}
			if (pReader["Retmsg"] != DBNull.Value)
			{
				pInstance.Retmsg =  Convert.ToString(pReader["Retmsg"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
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

        }
        #endregion
    }
}
