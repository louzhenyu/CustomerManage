/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/17 9:39:12
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
    /// ��MVipShow�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MVipShowDAO : Base.BaseCPOSDAO, ICRUDable<MVipShowEntity>, IQueryable<MVipShowEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MVipShowDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(MVipShowEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(MVipShowEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MVipShow](");
            strSql.Append("[VipId],[UnitId],[Experience],[PraiseCount],[HairStylistId],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[IsCheck],[IsLottery],[LotteryCode],[CustomerId],[VipShowId])");
            strSql.Append(" values (");
            strSql.Append("@VipId,@UnitId,@Experience,@PraiseCount,@HairStylistId,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@IsCheck,@IsLottery,@LotteryCode,@CustomerId,@VipShowId)");            

			string pkString = pEntity.VipShowId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@Experience",SqlDbType.NVarChar),
					new SqlParameter("@PraiseCount",SqlDbType.Int),
					new SqlParameter("@HairStylistId",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@IsCheck",SqlDbType.Int),
					new SqlParameter("@IsLottery",SqlDbType.Int),
					new SqlParameter("@LotteryCode",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@VipShowId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipId;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.Experience;
			parameters[3].Value = pEntity.PraiseCount;
			parameters[4].Value = pEntity.HairStylistId;
			parameters[5].Value = pEntity.CreateTime;
			parameters[6].Value = pEntity.CreateBy;
			parameters[7].Value = pEntity.LastUpdateBy;
			parameters[8].Value = pEntity.LastUpdateTime;
			parameters[9].Value = pEntity.IsDelete;
			parameters[10].Value = pEntity.IsCheck;
			parameters[11].Value = pEntity.IsLottery;
			parameters[12].Value = pEntity.LotteryCode;
			parameters[13].Value = pEntity.CustomerId;
			parameters[14].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipShowId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public MVipShowEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MVipShow] where VipShowId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            MVipShowEntity m = null;
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
        public MVipShowEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MVipShow] where isdelete=0");
            //��ȡ����
            List<MVipShowEntity> list = new List<MVipShowEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MVipShowEntity m;
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
        public void Update(MVipShowEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(MVipShowEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipShowId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MVipShow] set ");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId,");
            if (pIsUpdateNullField || pEntity.UnitId!=null)
                strSql.Append( "[UnitId]=@UnitId,");
            if (pIsUpdateNullField || pEntity.Experience!=null)
                strSql.Append( "[Experience]=@Experience,");
            if (pIsUpdateNullField || pEntity.PraiseCount!=null)
                strSql.Append( "[PraiseCount]=@PraiseCount,");
            if (pIsUpdateNullField || pEntity.HairStylistId!=null)
                strSql.Append( "[HairStylistId]=@HairStylistId,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.IsCheck!=null)
                strSql.Append( "[IsCheck]=@IsCheck,");
            if (pIsUpdateNullField || pEntity.IsLottery!=null)
                strSql.Append( "[IsLottery]=@IsLottery,");
            if (pIsUpdateNullField || pEntity.LotteryCode!=null)
                strSql.Append( "[LotteryCode]=@LotteryCode,");
            if (pIsUpdateNullField || pEntity.CustomerId!=null)
                strSql.Append( "[CustomerId]=@CustomerId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where VipShowId=@VipShowId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@UnitId",SqlDbType.NVarChar),
					new SqlParameter("@Experience",SqlDbType.NVarChar),
					new SqlParameter("@PraiseCount",SqlDbType.Int),
					new SqlParameter("@HairStylistId",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsCheck",SqlDbType.Int),
					new SqlParameter("@IsLottery",SqlDbType.Int),
					new SqlParameter("@LotteryCode",SqlDbType.NVarChar),
					new SqlParameter("@CustomerId",SqlDbType.NVarChar),
					new SqlParameter("@VipShowId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.VipId;
			parameters[1].Value = pEntity.UnitId;
			parameters[2].Value = pEntity.Experience;
			parameters[3].Value = pEntity.PraiseCount;
			parameters[4].Value = pEntity.HairStylistId;
			parameters[5].Value = pEntity.LastUpdateBy;
			parameters[6].Value = pEntity.LastUpdateTime;
			parameters[7].Value = pEntity.IsCheck;
			parameters[8].Value = pEntity.IsLottery;
			parameters[9].Value = pEntity.LotteryCode;
			parameters[10].Value = pEntity.CustomerId;
			parameters[11].Value = pEntity.VipShowId;

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
        public void Update(MVipShowEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(MVipShowEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MVipShowEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(MVipShowEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipShowId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.VipShowId, pTran);           
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
            sql.AppendLine("update [MVipShow] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where VipShowId=@VipShowId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@VipShowId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(MVipShowEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.VipShowId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.VipShowId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(MVipShowEntity[] pEntities)
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
            sql.AppendLine("update [MVipShow] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where VipShowId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MVipShowEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MVipShow] where isdelete=0 ");
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
            List<MVipShowEntity> list = new List<MVipShowEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MVipShowEntity m;
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
        public PagedQueryResult<MVipShowEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipShowId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [MVipShow] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [MVipShow] where isdelete=0 ");
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
            PagedQueryResult<MVipShowEntity> result = new PagedQueryResult<MVipShowEntity>();
            List<MVipShowEntity> list = new List<MVipShowEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MVipShowEntity m;
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
        public MVipShowEntity[] QueryByEntity(MVipShowEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MVipShowEntity> PagedQueryByEntity(MVipShowEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MVipShowEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipShowId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipShowId", Value = pQueryEntity.VipShowId });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });
            if (pQueryEntity.UnitId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitId", Value = pQueryEntity.UnitId });
            if (pQueryEntity.Experience!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Experience", Value = pQueryEntity.Experience });
            if (pQueryEntity.PraiseCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PraiseCount", Value = pQueryEntity.PraiseCount });
            if (pQueryEntity.HairStylistId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HairStylistId", Value = pQueryEntity.HairStylistId });
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
            if (pQueryEntity.IsCheck!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCheck", Value = pQueryEntity.IsCheck });
            if (pQueryEntity.IsLottery!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsLottery", Value = pQueryEntity.IsLottery });
            if (pQueryEntity.LotteryCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LotteryCode", Value = pQueryEntity.LotteryCode });
            if (pQueryEntity.CustomerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerId", Value = pQueryEntity.CustomerId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out MVipShowEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new MVipShowEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VipShowId"] != DBNull.Value)
			{
				pInstance.VipShowId =  Convert.ToString(pReader["VipShowId"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}
			if (pReader["UnitId"] != DBNull.Value)
			{
				pInstance.UnitId =  Convert.ToString(pReader["UnitId"]);
			}
			if (pReader["Experience"] != DBNull.Value)
			{
				pInstance.Experience =  Convert.ToString(pReader["Experience"]);
			}
			if (pReader["PraiseCount"] != DBNull.Value)
			{
				pInstance.PraiseCount =   Convert.ToInt32(pReader["PraiseCount"]);
			}
			if (pReader["HairStylistId"] != DBNull.Value)
			{
				pInstance.HairStylistId =  Convert.ToString(pReader["HairStylistId"]);
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
			if (pReader["IsCheck"] != DBNull.Value)
			{
				pInstance.IsCheck =   Convert.ToInt32(pReader["IsCheck"]);
			}
			if (pReader["IsLottery"] != DBNull.Value)
			{
				pInstance.IsLottery =   Convert.ToInt32(pReader["IsLottery"]);
			}
			if (pReader["LotteryCode"] != DBNull.Value)
			{
				pInstance.LotteryCode =  Convert.ToString(pReader["LotteryCode"]);
			}
			if (pReader["CustomerId"] != DBNull.Value)
			{
				pInstance.CustomerId =  Convert.ToString(pReader["CustomerId"]);
			}

        }
        #endregion
    }
}
