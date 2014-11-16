/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 10:41:51
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
    /// ��WXHouseOrder�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseOrderDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseOrderEntity>, IQueryable<WXHouseOrderEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseOrderDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXHouseOrderEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXHouseOrderEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXHouseOrder](");
            strSql.Append("[MappingID],[FeeID],[OrderNO],[OrderDate],[RealPay],[AssignbuyerID],[ThirdOrderNo],[Assbuyeridtp],[Assbuyeridno],[Assbuyername],[Assbuyermobile],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[PrePaymentID])");
            strSql.Append(" values (");
            strSql.Append("@MappingID,@FeeID,@OrderNO,@OrderDate,@RealPay,@AssignbuyerID,@ThirdOrderNo,@Assbuyeridtp,@Assbuyeridno,@Assbuyername,@Assbuyermobile,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@PrePaymentID)");            

			Guid? pkGuid;
			if (pEntity.PrePaymentID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.PrePaymentID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@MappingID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@FeeID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrderNO",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.DateTime),
					new SqlParameter("@RealPay",SqlDbType.Decimal),
					new SqlParameter("@AssignbuyerID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ThirdOrderNo",SqlDbType.VarChar),
					new SqlParameter("@Assbuyeridtp",SqlDbType.NVarChar),
					new SqlParameter("@Assbuyeridno",SqlDbType.NVarChar),
					new SqlParameter("@Assbuyername",SqlDbType.VarChar),
					new SqlParameter("@Assbuyermobile",SqlDbType.VarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@PrePaymentID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.MappingID;
			parameters[1].Value = pEntity.FeeID;
			parameters[2].Value = pEntity.OrderNO;
			parameters[3].Value = pEntity.OrderDate;
			parameters[4].Value = pEntity.RealPay;
			parameters[5].Value = pEntity.AssignbuyerID;
			parameters[6].Value = pEntity.ThirdOrderNo;
			parameters[7].Value = pEntity.Assbuyeridtp;
			parameters[8].Value = pEntity.Assbuyeridno;
			parameters[9].Value = pEntity.Assbuyername;
			parameters[10].Value = pEntity.Assbuyermobile;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.CreateBy;
			parameters[13].Value = pEntity.CreateTime;
			parameters[14].Value = pEntity.LastUpdateBy;
			parameters[15].Value = pEntity.LastUpdateTime;
			parameters[16].Value = pEntity.IsDelete;
			parameters[17].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.PrePaymentID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseOrderEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseOrder] where PrePaymentID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WXHouseOrderEntity m = null;
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
        public WXHouseOrderEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseOrder] where isdelete=0");
            //��ȡ����
            List<WXHouseOrderEntity> list = new List<WXHouseOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseOrderEntity m;
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
        public void Update(WXHouseOrderEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WXHouseOrderEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PrePaymentID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXHouseOrder] set ");
            if (pIsUpdateNullField || pEntity.MappingID!=null)
                strSql.Append( "[MappingID]=@MappingID,");
            if (pIsUpdateNullField || pEntity.FeeID!=null)
                strSql.Append( "[FeeID]=@FeeID,");
            if (pIsUpdateNullField || pEntity.OrderNO!=null)
                strSql.Append( "[OrderNO]=@OrderNO,");
            if (pIsUpdateNullField || pEntity.OrderDate!=null)
                strSql.Append( "[OrderDate]=@OrderDate,");
            if (pIsUpdateNullField || pEntity.RealPay!=null)
                strSql.Append( "[RealPay]=@RealPay,");
            if (pIsUpdateNullField || pEntity.AssignbuyerID!=null)
                strSql.Append( "[AssignbuyerID]=@AssignbuyerID,");
            if (pIsUpdateNullField || pEntity.ThirdOrderNo!=null)
                strSql.Append( "[ThirdOrderNo]=@ThirdOrderNo,");
            if (pIsUpdateNullField || pEntity.Assbuyeridtp!=null)
                strSql.Append( "[Assbuyeridtp]=@Assbuyeridtp,");
            if (pIsUpdateNullField || pEntity.Assbuyeridno!=null)
                strSql.Append( "[Assbuyeridno]=@Assbuyeridno,");
            if (pIsUpdateNullField || pEntity.Assbuyername!=null)
                strSql.Append( "[Assbuyername]=@Assbuyername,");
            if (pIsUpdateNullField || pEntity.Assbuyermobile!=null)
                strSql.Append( "[Assbuyermobile]=@Assbuyermobile,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where PrePaymentID=@PrePaymentID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@MappingID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@FeeID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@OrderNO",SqlDbType.NVarChar),
					new SqlParameter("@OrderDate",SqlDbType.DateTime),
					new SqlParameter("@RealPay",SqlDbType.Decimal),
					new SqlParameter("@AssignbuyerID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ThirdOrderNo",SqlDbType.VarChar),
					new SqlParameter("@Assbuyeridtp",SqlDbType.NVarChar),
					new SqlParameter("@Assbuyeridno",SqlDbType.NVarChar),
					new SqlParameter("@Assbuyername",SqlDbType.VarChar),
					new SqlParameter("@Assbuyermobile",SqlDbType.VarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@PrePaymentID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.MappingID;
			parameters[1].Value = pEntity.FeeID;
			parameters[2].Value = pEntity.OrderNO;
			parameters[3].Value = pEntity.OrderDate;
			parameters[4].Value = pEntity.RealPay;
			parameters[5].Value = pEntity.AssignbuyerID;
			parameters[6].Value = pEntity.ThirdOrderNo;
			parameters[7].Value = pEntity.Assbuyeridtp;
			parameters[8].Value = pEntity.Assbuyeridno;
			parameters[9].Value = pEntity.Assbuyername;
			parameters[10].Value = pEntity.Assbuyermobile;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.PrePaymentID;

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
        public void Update(WXHouseOrderEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WXHouseOrderEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXHouseOrderEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXHouseOrderEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.PrePaymentID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.PrePaymentID, pTran);           
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
            sql.AppendLine("update [WXHouseOrder] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where PrePaymentID=@PrePaymentID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@PrePaymentID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXHouseOrderEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.PrePaymentID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.PrePaymentID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXHouseOrderEntity[] pEntities)
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
            sql.AppendLine("update [WXHouseOrder] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where PrePaymentID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXHouseOrderEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseOrder] where isdelete=0 ");
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
            List<WXHouseOrderEntity> list = new List<WXHouseOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseOrderEntity m;
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
        public PagedQueryResult<WXHouseOrderEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [PrePaymentID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXHouseOrder] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXHouseOrder] where isdelete=0 ");
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
            PagedQueryResult<WXHouseOrderEntity> result = new PagedQueryResult<WXHouseOrderEntity>();
            List<WXHouseOrderEntity> list = new List<WXHouseOrderEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseOrderEntity m;
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
        public WXHouseOrderEntity[] QueryByEntity(WXHouseOrderEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXHouseOrderEntity> PagedQueryByEntity(WXHouseOrderEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXHouseOrderEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.PrePaymentID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "PrePaymentID", Value = pQueryEntity.PrePaymentID });
            if (pQueryEntity.MappingID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MappingID", Value = pQueryEntity.MappingID });
            if (pQueryEntity.FeeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "FeeID", Value = pQueryEntity.FeeID });
            if (pQueryEntity.OrderNO!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNO", Value = pQueryEntity.OrderNO });
            if (pQueryEntity.OrderDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderDate", Value = pQueryEntity.OrderDate });
            if (pQueryEntity.RealPay!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RealPay", Value = pQueryEntity.RealPay });
            if (pQueryEntity.AssignbuyerID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AssignbuyerID", Value = pQueryEntity.AssignbuyerID });
            if (pQueryEntity.ThirdOrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThirdOrderNo", Value = pQueryEntity.ThirdOrderNo });
            if (pQueryEntity.Assbuyeridtp!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Assbuyeridtp", Value = pQueryEntity.Assbuyeridtp });
            if (pQueryEntity.Assbuyeridno!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Assbuyeridno", Value = pQueryEntity.Assbuyeridno });
            if (pQueryEntity.Assbuyername!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Assbuyername", Value = pQueryEntity.Assbuyername });
            if (pQueryEntity.Assbuyermobile!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Assbuyermobile", Value = pQueryEntity.Assbuyermobile });
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
        protected void Load(SqlDataReader pReader, out WXHouseOrderEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXHouseOrderEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["PrePaymentID"] != DBNull.Value)
			{
				pInstance.PrePaymentID =  (Guid)pReader["PrePaymentID"];
			}
			if (pReader["MappingID"] != DBNull.Value)
			{
				pInstance.MappingID =  (Guid)pReader["MappingID"];
			}
			if (pReader["FeeID"] != DBNull.Value)
			{
				pInstance.FeeID =  (Guid)pReader["FeeID"];
			}
			if (pReader["OrderNO"] != DBNull.Value)
			{
				pInstance.OrderNO =  Convert.ToString(pReader["OrderNO"]);
			}
			if (pReader["OrderDate"] != DBNull.Value)
			{
				pInstance.OrderDate =  Convert.ToDateTime(pReader["OrderDate"]);
			}
			if (pReader["RealPay"] != DBNull.Value)
			{
				pInstance.RealPay =  Convert.ToDecimal(pReader["RealPay"]);
			}
			if (pReader["AssignbuyerID"] != DBNull.Value)
			{
				pInstance.AssignbuyerID =  (Guid)pReader["AssignbuyerID"];
			}
			if (pReader["ThirdOrderNo"] != DBNull.Value)
			{
				pInstance.ThirdOrderNo =  Convert.ToString(pReader["ThirdOrderNo"]);
			}
			if (pReader["Assbuyeridtp"] != DBNull.Value)
			{
				pInstance.Assbuyeridtp =  Convert.ToString(pReader["Assbuyeridtp"]);
			}
			if (pReader["Assbuyeridno"] != DBNull.Value)
			{
				pInstance.Assbuyeridno =  Convert.ToString(pReader["Assbuyeridno"]);
			}
			if (pReader["Assbuyername"] != DBNull.Value)
			{
				pInstance.Assbuyername =  Convert.ToString(pReader["Assbuyername"]);
			}
			if (pReader["Assbuyermobile"] != DBNull.Value)
			{
				pInstance.Assbuyermobile =  Convert.ToString(pReader["Assbuyermobile"]);
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
