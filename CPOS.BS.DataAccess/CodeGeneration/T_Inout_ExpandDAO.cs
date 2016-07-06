/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/26 11:20:39
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
using CPOS.BS.Entity;
using JIT.CPOS.BS.Entity;

namespace CPOS.BS.DataAccess
{
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��T_Inout_Expand�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class T_Inout_ExpandDAO : JIT.CPOS.BS.DataAccess.Base.BaseCPOSDAO, ICRUDable<T_Inout_ExpandEntity>, IQueryable<T_Inout_ExpandEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public T_Inout_ExpandDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(T_Inout_ExpandEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(T_Inout_ExpandEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [T_Inout_Expand](");
            strSql.Append("[OrderId],[DeliveryMode],[PackageRemark],[LogisticRemark],[TransType],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID],[ProvinceCode],[Province],[CityCode],[City],[AreaCode],[Area],[DiscRemarks],[IsCallBeDeli],[GoodsAndInvoice],[OrderExpandID])");
            strSql.Append(" values (");
            strSql.Append("@OrderId,@DeliveryMode,@PackageRemark,@LogisticRemark,@TransType,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID,@ProvinceCode,@Province,@CityCode,@City,@AreaCode,@Area,@DiscRemarks,@IsCallBeDeli,@GoodsAndInvoice,@OrderExpandID)");            

			string pkString = pEntity.OrderExpandID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryMode",SqlDbType.NVarChar),
					new SqlParameter("@PackageRemark",SqlDbType.NVarChar),
					new SqlParameter("@LogisticRemark",SqlDbType.NVarChar),
					new SqlParameter("@TransType",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@ProvinceCode",SqlDbType.NVarChar),
					new SqlParameter("@Province",SqlDbType.NVarChar),
					new SqlParameter("@CityCode",SqlDbType.NVarChar),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@AreaCode",SqlDbType.NVarChar),
					new SqlParameter("@Area",SqlDbType.NVarChar),
					new SqlParameter("@DiscRemarks",SqlDbType.NVarChar),
					new SqlParameter("@IsCallBeDeli",SqlDbType.NVarChar),
					new SqlParameter("@GoodsAndInvoice",SqlDbType.NVarChar),
					new SqlParameter("@OrderExpandID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.DeliveryMode;
			parameters[2].Value = pEntity.PackageRemark;
			parameters[3].Value = pEntity.LogisticRemark;
			parameters[4].Value = pEntity.TransType;
			parameters[5].Value = pEntity.CreateTime;
			parameters[6].Value = pEntity.CreateBy;
			parameters[7].Value = pEntity.LastUpdateTime;
			parameters[8].Value = pEntity.LastUpdateBy;
			parameters[9].Value = pEntity.IsDelete;
			parameters[10].Value = pEntity.CustomerID;
			parameters[11].Value = pEntity.ProvinceCode;
			parameters[12].Value = pEntity.Province;
			parameters[13].Value = pEntity.CityCode;
			parameters[14].Value = pEntity.City;
			parameters[15].Value = pEntity.AreaCode;
			parameters[16].Value = pEntity.Area;
			parameters[17].Value = pEntity.DiscRemarks;
			parameters[18].Value = pEntity.IsCallBeDeli;
			parameters[19].Value = pEntity.GoodsAndInvoice;
			parameters[20].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.OrderExpandID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public T_Inout_ExpandEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Expand] where OrderExpandID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            T_Inout_ExpandEntity m = null;
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
        public T_Inout_ExpandEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Expand] where 1=1  and isdelete=0");
            //��ȡ����
            List<T_Inout_ExpandEntity> list = new List<T_Inout_ExpandEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Inout_ExpandEntity m;
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
        public void Update(T_Inout_ExpandEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(T_Inout_ExpandEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderExpandID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [T_Inout_Expand] set ");
                        if (pIsUpdateNullField || pEntity.OrderId!=null)
                strSql.Append( "[OrderId]=@OrderId,");
            if (pIsUpdateNullField || pEntity.DeliveryMode!=null)
                strSql.Append( "[DeliveryMode]=@DeliveryMode,");
            if (pIsUpdateNullField || pEntity.PackageRemark!=null)
                strSql.Append( "[PackageRemark]=@PackageRemark,");
            if (pIsUpdateNullField || pEntity.LogisticRemark!=null)
                strSql.Append( "[LogisticRemark]=@LogisticRemark,");
            if (pIsUpdateNullField || pEntity.TransType!=null)
                strSql.Append( "[TransType]=@TransType,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.ProvinceCode!=null)
                strSql.Append( "[ProvinceCode]=@ProvinceCode,");
            if (pIsUpdateNullField || pEntity.Province!=null)
                strSql.Append( "[Province]=@Province,");
            if (pIsUpdateNullField || pEntity.CityCode!=null)
                strSql.Append( "[CityCode]=@CityCode,");
            if (pIsUpdateNullField || pEntity.City!=null)
                strSql.Append( "[City]=@City,");
            if (pIsUpdateNullField || pEntity.AreaCode!=null)
                strSql.Append( "[AreaCode]=@AreaCode,");
            if (pIsUpdateNullField || pEntity.Area!=null)
                strSql.Append( "[Area]=@Area,");
            if (pIsUpdateNullField || pEntity.DiscRemarks!=null)
                strSql.Append( "[DiscRemarks]=@DiscRemarks,");
            if (pIsUpdateNullField || pEntity.IsCallBeDeli!=null)
                strSql.Append( "[IsCallBeDeli]=@IsCallBeDeli,");
            if (pIsUpdateNullField || pEntity.GoodsAndInvoice!=null)
                strSql.Append( "[GoodsAndInvoice]=@GoodsAndInvoice");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderExpandID=@OrderExpandID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@OrderId",SqlDbType.NVarChar),
					new SqlParameter("@DeliveryMode",SqlDbType.NVarChar),
					new SqlParameter("@PackageRemark",SqlDbType.NVarChar),
					new SqlParameter("@LogisticRemark",SqlDbType.NVarChar),
					new SqlParameter("@TransType",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@ProvinceCode",SqlDbType.NVarChar),
					new SqlParameter("@Province",SqlDbType.NVarChar),
					new SqlParameter("@CityCode",SqlDbType.NVarChar),
					new SqlParameter("@City",SqlDbType.NVarChar),
					new SqlParameter("@AreaCode",SqlDbType.NVarChar),
					new SqlParameter("@Area",SqlDbType.NVarChar),
					new SqlParameter("@DiscRemarks",SqlDbType.NVarChar),
					new SqlParameter("@IsCallBeDeli",SqlDbType.NVarChar),
					new SqlParameter("@GoodsAndInvoice",SqlDbType.NVarChar),
					new SqlParameter("@OrderExpandID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.OrderId;
			parameters[1].Value = pEntity.DeliveryMode;
			parameters[2].Value = pEntity.PackageRemark;
			parameters[3].Value = pEntity.LogisticRemark;
			parameters[4].Value = pEntity.TransType;
			parameters[5].Value = pEntity.LastUpdateTime;
			parameters[6].Value = pEntity.LastUpdateBy;
			parameters[7].Value = pEntity.CustomerID;
			parameters[8].Value = pEntity.ProvinceCode;
			parameters[9].Value = pEntity.Province;
			parameters[10].Value = pEntity.CityCode;
			parameters[11].Value = pEntity.City;
			parameters[12].Value = pEntity.AreaCode;
			parameters[13].Value = pEntity.Area;
			parameters[14].Value = pEntity.DiscRemarks;
			parameters[15].Value = pEntity.IsCallBeDeli;
			parameters[16].Value = pEntity.GoodsAndInvoice;
			parameters[17].Value = pEntity.OrderExpandID;

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
        public void Update(T_Inout_ExpandEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(T_Inout_ExpandEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(T_Inout_ExpandEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderExpandID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderExpandID, pTran);           
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
            sql.AppendLine("update [T_Inout_Expand] set  isdelete=1 where OrderExpandID=@OrderExpandID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@OrderExpandID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(T_Inout_ExpandEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.OrderExpandID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.OrderExpandID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(T_Inout_ExpandEntity[] pEntities)
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
            sql.AppendLine("update [T_Inout_Expand] set  isdelete=1 where OrderExpandID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public T_Inout_ExpandEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [T_Inout_Expand] where 1=1  and isdelete=0 ");
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
            List<T_Inout_ExpandEntity> list = new List<T_Inout_ExpandEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Inout_ExpandEntity m;
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
        public PagedQueryResult<T_Inout_ExpandEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderExpandID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [T_Inout_Expand] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [T_Inout_Expand] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<T_Inout_ExpandEntity> result = new PagedQueryResult<T_Inout_ExpandEntity>();
            List<T_Inout_ExpandEntity> list = new List<T_Inout_ExpandEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    T_Inout_ExpandEntity m;
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
        public T_Inout_ExpandEntity[] QueryByEntity(T_Inout_ExpandEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<T_Inout_ExpandEntity> PagedQueryByEntity(T_Inout_ExpandEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(T_Inout_ExpandEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderExpandID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderExpandID", Value = pQueryEntity.OrderExpandID });
            if (pQueryEntity.OrderId!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderId", Value = pQueryEntity.OrderId });
            if (pQueryEntity.DeliveryMode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliveryMode", Value = pQueryEntity.DeliveryMode });
            if (pQueryEntity.PackageRemark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PackageRemark", Value = pQueryEntity.PackageRemark });
            if (pQueryEntity.LogisticRemark!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LogisticRemark", Value = pQueryEntity.LogisticRemark });
            if (pQueryEntity.TransType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransType", Value = pQueryEntity.TransType });
            if (pQueryEntity.CreateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateTime", Value = pQueryEntity.CreateTime });
            if (pQueryEntity.CreateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CreateBy", Value = pQueryEntity.CreateBy });
            if (pQueryEntity.LastUpdateTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateTime", Value = pQueryEntity.LastUpdateTime });
            if (pQueryEntity.LastUpdateBy!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LastUpdateBy", Value = pQueryEntity.LastUpdateBy });
            if (pQueryEntity.IsDelete!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsDelete", Value = pQueryEntity.IsDelete });
            if (pQueryEntity.CustomerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CustomerID", Value = pQueryEntity.CustomerID });
            if (pQueryEntity.ProvinceCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProvinceCode", Value = pQueryEntity.ProvinceCode });
            if (pQueryEntity.Province!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Province", Value = pQueryEntity.Province });
            if (pQueryEntity.CityCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CityCode", Value = pQueryEntity.CityCode });
            if (pQueryEntity.City!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "City", Value = pQueryEntity.City });
            if (pQueryEntity.AreaCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AreaCode", Value = pQueryEntity.AreaCode });
            if (pQueryEntity.Area!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Area", Value = pQueryEntity.Area });
            if (pQueryEntity.DiscRemarks!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DiscRemarks", Value = pQueryEntity.DiscRemarks });
            if (pQueryEntity.IsCallBeDeli!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsCallBeDeli", Value = pQueryEntity.IsCallBeDeli });
            if (pQueryEntity.GoodsAndInvoice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GoodsAndInvoice", Value = pQueryEntity.GoodsAndInvoice });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out T_Inout_ExpandEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new T_Inout_ExpandEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderExpandID"] != DBNull.Value)
			{
				pInstance.OrderExpandID =  Convert.ToString(pReader["OrderExpandID"]);
			}
			if (pReader["OrderId"] != DBNull.Value)
			{
				pInstance.OrderId =  Convert.ToString(pReader["OrderId"]);
			}
			if (pReader["DeliveryMode"] != DBNull.Value)
			{
				pInstance.DeliveryMode =  Convert.ToString(pReader["DeliveryMode"]);
			}
			if (pReader["PackageRemark"] != DBNull.Value)
			{
				pInstance.PackageRemark =  Convert.ToString(pReader["PackageRemark"]);
			}
			if (pReader["LogisticRemark"] != DBNull.Value)
			{
				pInstance.LogisticRemark =  Convert.ToString(pReader["LogisticRemark"]);
			}
			if (pReader["TransType"] != DBNull.Value)
			{
				pInstance.TransType =  Convert.ToString(pReader["TransType"]);
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
			if (pReader["IsDelete"] != DBNull.Value)
			{
				pInstance.IsDelete =   Convert.ToInt32(pReader["IsDelete"]);
			}
			if (pReader["CustomerID"] != DBNull.Value)
			{
				pInstance.CustomerID =  Convert.ToString(pReader["CustomerID"]);
			}
			if (pReader["ProvinceCode"] != DBNull.Value)
			{
				pInstance.ProvinceCode =  Convert.ToString(pReader["ProvinceCode"]);
			}
			if (pReader["Province"] != DBNull.Value)
			{
				pInstance.Province =  Convert.ToString(pReader["Province"]);
			}
			if (pReader["CityCode"] != DBNull.Value)
			{
				pInstance.CityCode =  Convert.ToString(pReader["CityCode"]);
			}
			if (pReader["City"] != DBNull.Value)
			{
				pInstance.City =  Convert.ToString(pReader["City"]);
			}
			if (pReader["AreaCode"] != DBNull.Value)
			{
				pInstance.AreaCode =  Convert.ToString(pReader["AreaCode"]);
			}
			if (pReader["Area"] != DBNull.Value)
			{
				pInstance.Area =  Convert.ToString(pReader["Area"]);
			}
			if (pReader["DiscRemarks"] != DBNull.Value)
			{
				pInstance.DiscRemarks =  Convert.ToString(pReader["DiscRemarks"]);
			}
			if (pReader["IsCallBeDeli"] != DBNull.Value)
			{
				pInstance.IsCallBeDeli =  Convert.ToString(pReader["IsCallBeDeli"]);
			}
			if (pReader["GoodsAndInvoice"] != DBNull.Value)
			{
				pInstance.GoodsAndInvoice =  Convert.ToString(pReader["GoodsAndInvoice"]);
			}

        }
        #endregion
    }
}
