/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/1/14 19:49:54
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
    /// ��OrderIntegral�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class OrderIntegralDAO : Base.BaseCPOSDAO, ICRUDable<OrderIntegralEntity>, IQueryable<OrderIntegralEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public OrderIntegralDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(OrderIntegralEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(OrderIntegralEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [OrderIntegral](");
            strSql.Append("[ItemID],[OrderNo],[Quantity],[Integral],[IntegralAmmount],[VIPID],[LinkMan],[LinkTel],[CityID],[Address],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete])");
            strSql.Append(" values (");
            strSql.Append("@ItemID,@OrderNo,@Quantity,@Integral,@IntegralAmmount,@VIPID,@LinkMan,@LinkTel,@CityID,@Address,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete)");            

            SqlParameter[] parameters = 
            {
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
                    new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@Quantity",SqlDbType.Int),
					new SqlParameter("@Integral",SqlDbType.Decimal),
					new SqlParameter("@IntegralAmmount",SqlDbType.Decimal),
					new SqlParameter("@VIPID",SqlDbType.NVarChar),
					new SqlParameter("@LinkMan",SqlDbType.NVarChar),
					new SqlParameter("@LinkTel",SqlDbType.NVarChar),
					new SqlParameter("@CityID",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.ItemID;
            parameters[1].Value = pEntity.OrderNo;
			parameters[2].Value = pEntity.Quantity;
			parameters[3].Value = pEntity.Integral;
			parameters[4].Value = pEntity.IntegralAmmount;
			parameters[5].Value = pEntity.VIPID;
			parameters[6].Value = pEntity.LinkMan;
			parameters[7].Value = pEntity.LinkTel;
			parameters[8].Value = pEntity.CityID;
			parameters[9].Value = pEntity.Address;
			parameters[10].Value = pEntity.CreateBy;
			parameters[11].Value = pEntity.CreateTime;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.IsDelete;


            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 

        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public OrderIntegralEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [OrderIntegral] where OrderIntegralID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            OrderIntegralEntity m = null;
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
        public OrderIntegralEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [OrderIntegral] where isdelete=0");
            //��ȡ����
            List<OrderIntegralEntity> list = new List<OrderIntegralEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    OrderIntegralEntity m;
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
        public void Update(OrderIntegralEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(OrderIntegralEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderIntegralID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [OrderIntegral] set ");
            if (pIsUpdateNullField || pEntity.ItemID!=null)
                strSql.Append( "[ItemID]=@ItemID,");
            if (pIsUpdateNullField || pEntity.OrderNo != null)
                strSql.Append("[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.Quantity!=null)
                strSql.Append( "[Quantity]=@Quantity,");
            if (pIsUpdateNullField || pEntity.Integral!=null)
                strSql.Append( "[Integral]=@Integral,");
            if (pIsUpdateNullField || pEntity.IntegralAmmount!=null)
                strSql.Append( "[IntegralAmmount]=@IntegralAmmount,");
            if (pIsUpdateNullField || pEntity.VIPID!=null)
                strSql.Append( "[VIPID]=@VIPID,");
            if (pIsUpdateNullField || pEntity.LinkMan!=null)
                strSql.Append( "[LinkMan]=@LinkMan,");
            if (pIsUpdateNullField || pEntity.LinkTel!=null)
                strSql.Append( "[LinkTel]=@LinkTel,");
            if (pIsUpdateNullField || pEntity.CityID!=null)
                strSql.Append( "[CityID]=@CityID,");
            if (pIsUpdateNullField || pEntity.Address!=null)
                strSql.Append( "[Address]=@Address,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where OrderIntegralID=@OrderIntegralID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@ItemID",SqlDbType.NVarChar),
                    new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@Quantity",SqlDbType.Int),
					new SqlParameter("@Integral",SqlDbType.Decimal),
					new SqlParameter("@IntegralAmmount",SqlDbType.Decimal),
					new SqlParameter("@VIPID",SqlDbType.NVarChar),
					new SqlParameter("@LinkMan",SqlDbType.NVarChar),
					new SqlParameter("@LinkTel",SqlDbType.NVarChar),
					new SqlParameter("@CityID",SqlDbType.NVarChar),
					new SqlParameter("@Address",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@OrderIntegralID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.ItemID;
            parameters[1].Value = pEntity.OrderNo;
			parameters[2].Value = pEntity.Quantity;
			parameters[3].Value = pEntity.Integral;
			parameters[4].Value = pEntity.IntegralAmmount;
			parameters[5].Value = pEntity.VIPID;
			parameters[6].Value = pEntity.LinkMan;
			parameters[7].Value = pEntity.LinkTel;
			parameters[8].Value = pEntity.CityID;
			parameters[9].Value = pEntity.Address;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.OrderIntegralID;

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
        public void Update(OrderIntegralEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(OrderIntegralEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(OrderIntegralEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(OrderIntegralEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.OrderIntegralID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.OrderIntegralID, pTran);           
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
            sql.AppendLine("update [OrderIntegral] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where OrderIntegralID=@OrderIntegralID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@OrderIntegralID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(OrderIntegralEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.OrderIntegralID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.OrderIntegralID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(OrderIntegralEntity[] pEntities)
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
            sql.AppendLine("update [OrderIntegral] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where OrderIntegralID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public OrderIntegralEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [OrderIntegral] where isdelete=0 ");
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
            List<OrderIntegralEntity> list = new List<OrderIntegralEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    OrderIntegralEntity m;
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
        public PagedQueryResult<OrderIntegralEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [OrderIntegralID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [OrderIntegral] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [OrderIntegral] where isdelete=0 ");
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
            PagedQueryResult<OrderIntegralEntity> result = new PagedQueryResult<OrderIntegralEntity>();
            List<OrderIntegralEntity> list = new List<OrderIntegralEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    OrderIntegralEntity m;
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
        public OrderIntegralEntity[] QueryByEntity(OrderIntegralEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<OrderIntegralEntity> PagedQueryByEntity(OrderIntegralEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(OrderIntegralEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.OrderIntegralID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderIntegralID", Value = pQueryEntity.OrderIntegralID });
            if (pQueryEntity.ItemID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ItemID", Value = pQueryEntity.ItemID });
            if (pQueryEntity.Quantity!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Quantity", Value = pQueryEntity.Quantity });
            if (pQueryEntity.Integral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Integral", Value = pQueryEntity.Integral });
            if (pQueryEntity.IntegralAmmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IntegralAmmount", Value = pQueryEntity.IntegralAmmount });
            if (pQueryEntity.VIPID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VIPID", Value = pQueryEntity.VIPID });
            if (pQueryEntity.LinkMan!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LinkMan", Value = pQueryEntity.LinkMan });
            if (pQueryEntity.LinkTel!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LinkTel", Value = pQueryEntity.LinkTel });
            if (pQueryEntity.CityID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CityID", Value = pQueryEntity.CityID });
            if (pQueryEntity.Address!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Address", Value = pQueryEntity.Address });
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
        protected void Load(SqlDataReader pReader, out OrderIntegralEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new OrderIntegralEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["OrderIntegralID"] != DBNull.Value)
			{
				pInstance.OrderIntegralID =  Convert.ToString(pReader["OrderIntegralID"]);
			}
			if (pReader["ItemID"] != DBNull.Value)
			{
				pInstance.ItemID =  Convert.ToString(pReader["ItemID"]);
			}
			if (pReader["Quantity"] != DBNull.Value)
			{
				pInstance.Quantity =   Convert.ToInt32(pReader["Quantity"]);
			}
			if (pReader["Integral"] != DBNull.Value)
			{
				pInstance.Integral =  Convert.ToDecimal(pReader["Integral"]);
			}
			if (pReader["IntegralAmmount"] != DBNull.Value)
			{
				pInstance.IntegralAmmount =  Convert.ToDecimal(pReader["IntegralAmmount"]);
			}
			if (pReader["VIPID"] != DBNull.Value)
			{
				pInstance.VIPID =  Convert.ToString(pReader["VIPID"]);
			}
			if (pReader["LinkMan"] != DBNull.Value)
			{
				pInstance.LinkMan =  Convert.ToString(pReader["LinkMan"]);
			}
			if (pReader["LinkTel"] != DBNull.Value)
			{
				pInstance.LinkTel =  Convert.ToString(pReader["LinkTel"]);
			}
			if (pReader["CityID"] != DBNull.Value)
			{
				pInstance.CityID =  Convert.ToString(pReader["CityID"]);
			}
			if (pReader["Address"] != DBNull.Value)
			{
				pInstance.Address =  Convert.ToString(pReader["Address"]);
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
