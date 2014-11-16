/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/18 13:22:52
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
    /// ��LItemAuth�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LItemAuthDAO : Base.BaseCPOSDAO, ICRUDable<LItemAuthEntity>, IQueryable<LItemAuthEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public LItemAuthDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(LItemAuthEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(LItemAuthEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [LItemAuth](");
            strSql.Append("[AuthCode],[CaptchaCode],[ItemName],[Norm],[Alcohol],[BaseWineYear],[AgePitPits],[Barcode],[IsAuthCode],[CategoryName],[CategoryId],[BrandName],[DealerName],[DealerId],[AuthCount],[CreateTime],[CreateBy],[LastUpdateBy],[LastUpdateTime],[IsDelete],[StoreCode],[VipId],[ItemAuthId])");
            strSql.Append(" values (");
            strSql.Append("@AuthCode,@CaptchaCode,@ItemName,@Norm,@Alcohol,@BaseWineYear,@AgePitPits,@Barcode,@IsAuthCode,@CategoryName,@CategoryId,@BrandName,@DealerName,@DealerId,@AuthCount,@CreateTime,@CreateBy,@LastUpdateBy,@LastUpdateTime,@IsDelete,@StoreCode,@VipId,@ItemAuthId)");            

			string pkString = pEntity.ItemAuthId;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@AuthCode",SqlDbType.NVarChar),
					new SqlParameter("@CaptchaCode",SqlDbType.NVarChar),
					new SqlParameter("@ItemName",SqlDbType.NVarChar),
					new SqlParameter("@Norm",SqlDbType.NVarChar),
					new SqlParameter("@Alcohol",SqlDbType.NVarChar),
					new SqlParameter("@BaseWineYear",SqlDbType.NVarChar),
					new SqlParameter("@AgePitPits",SqlDbType.NVarChar),
					new SqlParameter("@Barcode",SqlDbType.NVarChar),
					new SqlParameter("@IsAuthCode",SqlDbType.Int),
					new SqlParameter("@CategoryName",SqlDbType.NVarChar),
					new SqlParameter("@CategoryId",SqlDbType.NVarChar),
					new SqlParameter("@BrandName",SqlDbType.NVarChar),
					new SqlParameter("@DealerName",SqlDbType.NVarChar),
					new SqlParameter("@DealerId",SqlDbType.NVarChar),
					new SqlParameter("@AuthCount",SqlDbType.Int),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@StoreCode",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@ItemAuthId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AuthCode;
			parameters[1].Value = pEntity.CaptchaCode;
			parameters[2].Value = pEntity.ItemName;
			parameters[3].Value = pEntity.Norm;
			parameters[4].Value = pEntity.Alcohol;
			parameters[5].Value = pEntity.BaseWineYear;
			parameters[6].Value = pEntity.AgePitPits;
			parameters[7].Value = pEntity.Barcode;
			parameters[8].Value = pEntity.IsAuthCode;
			parameters[9].Value = pEntity.CategoryName;
			parameters[10].Value = pEntity.CategoryId;
			parameters[11].Value = pEntity.BrandName;
			parameters[12].Value = pEntity.DealerName;
			parameters[13].Value = pEntity.DealerId;
			parameters[14].Value = pEntity.AuthCount;
			parameters[15].Value = pEntity.CreateTime;
			parameters[16].Value = pEntity.CreateBy;
			parameters[17].Value = pEntity.LastUpdateBy;
			parameters[18].Value = pEntity.LastUpdateTime;
			parameters[19].Value = pEntity.IsDelete;
			parameters[20].Value = pEntity.StoreCode;
			parameters[21].Value = pEntity.VipId;
			parameters[22].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.ItemAuthId = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public LItemAuthEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LItemAuth] where ItemAuthId='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            LItemAuthEntity m = null;
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
        public LItemAuthEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LItemAuth] where isdelete=0");
            //��ȡ����
            List<LItemAuthEntity> list = new List<LItemAuthEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LItemAuthEntity m;
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
        public void Update(LItemAuthEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(LItemAuthEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ItemAuthId==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [LItemAuth] set ");
            if (pIsUpdateNullField || pEntity.AuthCode!=null)
                strSql.Append( "[AuthCode]=@AuthCode,");
            if (pIsUpdateNullField || pEntity.CaptchaCode!=null)
                strSql.Append( "[CaptchaCode]=@CaptchaCode,");
            if (pIsUpdateNullField || pEntity.ItemName!=null)
                strSql.Append( "[ItemName]=@ItemName,");
            if (pIsUpdateNullField || pEntity.Norm!=null)
                strSql.Append( "[Norm]=@Norm,");
            if (pIsUpdateNullField || pEntity.Alcohol!=null)
                strSql.Append( "[Alcohol]=@Alcohol,");
            if (pIsUpdateNullField || pEntity.BaseWineYear!=null)
                strSql.Append( "[BaseWineYear]=@BaseWineYear,");
            if (pIsUpdateNullField || pEntity.AgePitPits!=null)
                strSql.Append( "[AgePitPits]=@AgePitPits,");
            if (pIsUpdateNullField || pEntity.Barcode!=null)
                strSql.Append( "[Barcode]=@Barcode,");
            if (pIsUpdateNullField || pEntity.IsAuthCode!=null)
                strSql.Append( "[IsAuthCode]=@IsAuthCode,");
            if (pIsUpdateNullField || pEntity.CategoryName!=null)
                strSql.Append( "[CategoryName]=@CategoryName,");
            if (pIsUpdateNullField || pEntity.CategoryId!=null)
                strSql.Append( "[CategoryId]=@CategoryId,");
            if (pIsUpdateNullField || pEntity.BrandName!=null)
                strSql.Append( "[BrandName]=@BrandName,");
            if (pIsUpdateNullField || pEntity.DealerName!=null)
                strSql.Append( "[DealerName]=@DealerName,");
            if (pIsUpdateNullField || pEntity.DealerId!=null)
                strSql.Append( "[DealerId]=@DealerId,");
            if (pIsUpdateNullField || pEntity.AuthCount!=null)
                strSql.Append( "[AuthCount]=@AuthCount,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.StoreCode!=null)
                strSql.Append( "[StoreCode]=@StoreCode,");
            if (pIsUpdateNullField || pEntity.VipId!=null)
                strSql.Append( "[VipId]=@VipId");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ItemAuthId=@ItemAuthId ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AuthCode",SqlDbType.NVarChar),
					new SqlParameter("@CaptchaCode",SqlDbType.NVarChar),
					new SqlParameter("@ItemName",SqlDbType.NVarChar),
					new SqlParameter("@Norm",SqlDbType.NVarChar),
					new SqlParameter("@Alcohol",SqlDbType.NVarChar),
					new SqlParameter("@BaseWineYear",SqlDbType.NVarChar),
					new SqlParameter("@AgePitPits",SqlDbType.NVarChar),
					new SqlParameter("@Barcode",SqlDbType.NVarChar),
					new SqlParameter("@IsAuthCode",SqlDbType.Int),
					new SqlParameter("@CategoryName",SqlDbType.NVarChar),
					new SqlParameter("@CategoryId",SqlDbType.NVarChar),
					new SqlParameter("@BrandName",SqlDbType.NVarChar),
					new SqlParameter("@DealerName",SqlDbType.NVarChar),
					new SqlParameter("@DealerId",SqlDbType.NVarChar),
					new SqlParameter("@AuthCount",SqlDbType.Int),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@StoreCode",SqlDbType.NVarChar),
					new SqlParameter("@VipId",SqlDbType.NVarChar),
					new SqlParameter("@ItemAuthId",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.AuthCode;
			parameters[1].Value = pEntity.CaptchaCode;
			parameters[2].Value = pEntity.ItemName;
			parameters[3].Value = pEntity.Norm;
			parameters[4].Value = pEntity.Alcohol;
			parameters[5].Value = pEntity.BaseWineYear;
			parameters[6].Value = pEntity.AgePitPits;
			parameters[7].Value = pEntity.Barcode;
			parameters[8].Value = pEntity.IsAuthCode;
			parameters[9].Value = pEntity.CategoryName;
			parameters[10].Value = pEntity.CategoryId;
			parameters[11].Value = pEntity.BrandName;
			parameters[12].Value = pEntity.DealerName;
			parameters[13].Value = pEntity.DealerId;
			parameters[14].Value = pEntity.AuthCount;
			parameters[15].Value = pEntity.LastUpdateBy;
			parameters[16].Value = pEntity.LastUpdateTime;
			parameters[17].Value = pEntity.StoreCode;
			parameters[18].Value = pEntity.VipId;
			parameters[19].Value = pEntity.ItemAuthId;

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
        public void Update(LItemAuthEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(LItemAuthEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(LItemAuthEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(LItemAuthEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ItemAuthId==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ItemAuthId, pTran);           
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
            sql.AppendLine("update [LItemAuth] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ItemAuthId=@ItemAuthId;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ItemAuthId",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(LItemAuthEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ItemAuthId==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ItemAuthId;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(LItemAuthEntity[] pEntities)
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
            sql.AppendLine("update [LItemAuth] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ItemAuthId in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public LItemAuthEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [LItemAuth] where isdelete=0 ");
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
            List<LItemAuthEntity> list = new List<LItemAuthEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    LItemAuthEntity m;
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
        public PagedQueryResult<LItemAuthEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ItemAuthId] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [LItemAuth] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [LItemAuth] where isdelete=0 ");
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
            PagedQueryResult<LItemAuthEntity> result = new PagedQueryResult<LItemAuthEntity>();
            List<LItemAuthEntity> list = new List<LItemAuthEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    LItemAuthEntity m;
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
        public LItemAuthEntity[] QueryByEntity(LItemAuthEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<LItemAuthEntity> PagedQueryByEntity(LItemAuthEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(LItemAuthEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ItemAuthId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemAuthId", Value = pQueryEntity.ItemAuthId });
            if (pQueryEntity.AuthCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AuthCode", Value = pQueryEntity.AuthCode });
            if (pQueryEntity.CaptchaCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CaptchaCode", Value = pQueryEntity.CaptchaCode });
            if (pQueryEntity.ItemName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemName", Value = pQueryEntity.ItemName });
            if (pQueryEntity.Norm!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Norm", Value = pQueryEntity.Norm });
            if (pQueryEntity.Alcohol!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Alcohol", Value = pQueryEntity.Alcohol });
            if (pQueryEntity.BaseWineYear!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BaseWineYear", Value = pQueryEntity.BaseWineYear });
            if (pQueryEntity.AgePitPits!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AgePitPits", Value = pQueryEntity.AgePitPits });
            if (pQueryEntity.Barcode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Barcode", Value = pQueryEntity.Barcode });
            if (pQueryEntity.IsAuthCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsAuthCode", Value = pQueryEntity.IsAuthCode });
            if (pQueryEntity.CategoryName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CategoryName", Value = pQueryEntity.CategoryName });
            if (pQueryEntity.CategoryId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CategoryId", Value = pQueryEntity.CategoryId });
            if (pQueryEntity.BrandName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BrandName", Value = pQueryEntity.BrandName });
            if (pQueryEntity.DealerName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DealerName", Value = pQueryEntity.DealerName });
            if (pQueryEntity.DealerId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DealerId", Value = pQueryEntity.DealerId });
            if (pQueryEntity.AuthCount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AuthCount", Value = pQueryEntity.AuthCount });
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
            if (pQueryEntity.StoreCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "StoreCode", Value = pQueryEntity.StoreCode });
            if (pQueryEntity.VipId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipId", Value = pQueryEntity.VipId });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out LItemAuthEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new LItemAuthEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ItemAuthId"] != DBNull.Value)
			{
				pInstance.ItemAuthId =  Convert.ToString(pReader["ItemAuthId"]);
			}
			if (pReader["AuthCode"] != DBNull.Value)
			{
				pInstance.AuthCode =  Convert.ToString(pReader["AuthCode"]);
			}
			if (pReader["CaptchaCode"] != DBNull.Value)
			{
				pInstance.CaptchaCode =  Convert.ToString(pReader["CaptchaCode"]);
			}
			if (pReader["ItemName"] != DBNull.Value)
			{
				pInstance.ItemName =  Convert.ToString(pReader["ItemName"]);
			}
			if (pReader["Norm"] != DBNull.Value)
			{
				pInstance.Norm =  Convert.ToString(pReader["Norm"]);
			}
			if (pReader["Alcohol"] != DBNull.Value)
			{
				pInstance.Alcohol =  Convert.ToString(pReader["Alcohol"]);
			}
			if (pReader["BaseWineYear"] != DBNull.Value)
			{
				pInstance.BaseWineYear =  Convert.ToString(pReader["BaseWineYear"]);
			}
			if (pReader["AgePitPits"] != DBNull.Value)
			{
				pInstance.AgePitPits =  Convert.ToString(pReader["AgePitPits"]);
			}
			if (pReader["Barcode"] != DBNull.Value)
			{
				pInstance.Barcode =  Convert.ToString(pReader["Barcode"]);
			}
			if (pReader["IsAuthCode"] != DBNull.Value)
			{
				pInstance.IsAuthCode =   Convert.ToInt32(pReader["IsAuthCode"]);
			}
			if (pReader["CategoryName"] != DBNull.Value)
			{
				pInstance.CategoryName =  Convert.ToString(pReader["CategoryName"]);
			}
			if (pReader["CategoryId"] != DBNull.Value)
			{
				pInstance.CategoryId =  Convert.ToString(pReader["CategoryId"]);
			}
			if (pReader["BrandName"] != DBNull.Value)
			{
				pInstance.BrandName =  Convert.ToString(pReader["BrandName"]);
			}
			if (pReader["DealerName"] != DBNull.Value)
			{
				pInstance.DealerName =  Convert.ToString(pReader["DealerName"]);
			}
			if (pReader["DealerId"] != DBNull.Value)
			{
				pInstance.DealerId =  Convert.ToString(pReader["DealerId"]);
			}
			if (pReader["AuthCount"] != DBNull.Value)
			{
				pInstance.AuthCount =   Convert.ToInt32(pReader["AuthCount"]);
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
			if (pReader["StoreCode"] != DBNull.Value)
			{
				pInstance.StoreCode =  Convert.ToString(pReader["StoreCode"]);
			}
			if (pReader["VipId"] != DBNull.Value)
			{
				pInstance.VipId =  Convert.ToString(pReader["VipId"]);
			}

        }
        #endregion
    }
}
