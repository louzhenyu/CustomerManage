/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/30 15:25:27
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
    /// ��VipIntegral�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipIntegralDAO : Base.BaseCPOSDAO, ICRUDable<VipIntegralEntity>, IQueryable<VipIntegralEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipIntegralDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipIntegralEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipIntegralEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipIntegral](");
            strSql.Append("[VipCardCode],[BeginIntegral],[InIntegral],[OutIntegral],[EndIntegral],[ImminentInvalidIntegral],[InvalidIntegral],[ValidIntegral],[CumulativeIntegral],[ValidNotIntegral],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[VipID])");
            strSql.Append(" values (");
            strSql.Append("@VipCardCode,@BeginIntegral,@InIntegral,@OutIntegral,@EndIntegral,@ImminentInvalidIntegral,@InvalidIntegral,@ValidIntegral,@CumulativeIntegral,@ValidNotIntegral,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@VipID)");            

			string pkString = pEntity.VipID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardCode",SqlDbType.VarChar),
					new SqlParameter("@BeginIntegral",SqlDbType.Int),
					new SqlParameter("@InIntegral",SqlDbType.Int),
					new SqlParameter("@OutIntegral",SqlDbType.Int),
					new SqlParameter("@EndIntegral",SqlDbType.Int),
					new SqlParameter("@ImminentInvalidIntegral",SqlDbType.Int),
					new SqlParameter("@InvalidIntegral",SqlDbType.Int),
					new SqlParameter("@ValidIntegral",SqlDbType.Int),
					new SqlParameter("@CumulativeIntegral",SqlDbType.Int),
					new SqlParameter("@ValidNotIntegral",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@CreateBy",SqlDbType.VarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@VipID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.VipCardCode;
			parameters[1].Value = pEntity.BeginIntegral;
			parameters[2].Value = pEntity.InIntegral;
			parameters[3].Value = pEntity.OutIntegral;
			parameters[4].Value = pEntity.EndIntegral;
			parameters[5].Value = pEntity.ImminentInvalidIntegral;
			parameters[6].Value = pEntity.InvalidIntegral;
			parameters[7].Value = pEntity.ValidIntegral;
			parameters[8].Value = pEntity.CumulativeIntegral;
			parameters[9].Value = pEntity.ValidNotIntegral;
			parameters[10].Value = pEntity.CustomerID;
			parameters[11].Value = pEntity.CreateBy;
			parameters[12].Value = pEntity.CreateTime;
			parameters[13].Value = pEntity.LastUpdateBy;
			parameters[14].Value = pEntity.LastUpdateTime;
			parameters[15].Value = pEntity.IsDelete;
			parameters[16].Value = pkString;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.VipID = pkString;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipIntegralEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipIntegral] where VipID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipIntegralEntity m = null;
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
        public VipIntegralEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipIntegral] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipIntegralEntity> list = new List<VipIntegralEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipIntegralEntity m;
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
        public void Update(VipIntegralEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipIntegralEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipID == null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipIntegral] set ");
                        if (pIsUpdateNullField || pEntity.VipCardCode!=null)
                strSql.Append( "[VipCardCode]=@VipCardCode,");
            if (pIsUpdateNullField || pEntity.BeginIntegral!=null)
                strSql.Append( "[BeginIntegral]=@BeginIntegral,");
            if (pIsUpdateNullField || pEntity.InIntegral!=null)
                strSql.Append( "[InIntegral]=@InIntegral,");
            if (pIsUpdateNullField || pEntity.OutIntegral!=null)
                strSql.Append( "[OutIntegral]=@OutIntegral,");
            if (pIsUpdateNullField || pEntity.EndIntegral!=null)
                strSql.Append( "[EndIntegral]=@EndIntegral,");
            if (pIsUpdateNullField || pEntity.ImminentInvalidIntegral!=null)
                strSql.Append( "[ImminentInvalidIntegral]=@ImminentInvalidIntegral,");
            if (pIsUpdateNullField || pEntity.InvalidIntegral!=null)
                strSql.Append( "[InvalidIntegral]=@InvalidIntegral,");
            if (pIsUpdateNullField || pEntity.ValidIntegral!=null)
                strSql.Append( "[ValidIntegral]=@ValidIntegral,");
            if (pIsUpdateNullField || pEntity.CumulativeIntegral!=null)
                strSql.Append( "[CumulativeIntegral]=@CumulativeIntegral,");
            if (pIsUpdateNullField || pEntity.ValidNotIntegral!=null)
                strSql.Append( "[ValidNotIntegral]=@ValidNotIntegral,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            strSql.Append(" where VipID=@VipID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@VipCardCode",SqlDbType.VarChar),
					new SqlParameter("@BeginIntegral",SqlDbType.Int),
					new SqlParameter("@InIntegral",SqlDbType.Int),
					new SqlParameter("@OutIntegral",SqlDbType.Int),
					new SqlParameter("@EndIntegral",SqlDbType.Int),
					new SqlParameter("@ImminentInvalidIntegral",SqlDbType.Int),
					new SqlParameter("@InvalidIntegral",SqlDbType.Int),
					new SqlParameter("@ValidIntegral",SqlDbType.Int),
					new SqlParameter("@CumulativeIntegral",SqlDbType.Int),
					new SqlParameter("@ValidNotIntegral",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.VarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@VipID",SqlDbType.VarChar)
            };
			parameters[0].Value = pEntity.VipCardCode;
			parameters[1].Value = pEntity.BeginIntegral;
			parameters[2].Value = pEntity.InIntegral;
			parameters[3].Value = pEntity.OutIntegral;
			parameters[4].Value = pEntity.EndIntegral;
			parameters[5].Value = pEntity.ImminentInvalidIntegral;
			parameters[6].Value = pEntity.InvalidIntegral;
			parameters[7].Value = pEntity.ValidIntegral;
			parameters[8].Value = pEntity.CumulativeIntegral;
			parameters[9].Value = pEntity.ValidNotIntegral;
			parameters[10].Value = pEntity.CustomerID;
			parameters[11].Value = pEntity.LastUpdateBy;
			parameters[12].Value = pEntity.LastUpdateTime;
			parameters[13].Value = pEntity.VipID;

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
        public void Update(VipIntegralEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipIntegralEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipIntegralEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.VipID == null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.VipID, pTran);           
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
            sql.AppendLine("update [VipIntegral] set  isdelete=1 where VipID=@VipID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@VipID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipIntegralEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (pEntity.VipID == null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.VipID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipIntegralEntity[] pEntities)
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
            sql.AppendLine("update [VipIntegral] set  isdelete=1 where VipID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipIntegralEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipIntegral] where 1=1  and isdelete=0 ");
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
            List<VipIntegralEntity> list = new List<VipIntegralEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipIntegralEntity m;
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
        public PagedQueryResult<VipIntegralEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [VipID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipIntegral] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipIntegral] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipIntegralEntity> result = new PagedQueryResult<VipIntegralEntity>();
            List<VipIntegralEntity> list = new List<VipIntegralEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipIntegralEntity m;
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
        public VipIntegralEntity[] QueryByEntity(VipIntegralEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipIntegralEntity> PagedQueryByEntity(VipIntegralEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipIntegralEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.VipID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipID", Value = pQueryEntity.VipID });
            if (pQueryEntity.VipCardCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardCode", Value = pQueryEntity.VipCardCode });
            if (pQueryEntity.BeginIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BeginIntegral", Value = pQueryEntity.BeginIntegral });
            if (pQueryEntity.InIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InIntegral", Value = pQueryEntity.InIntegral });
            if (pQueryEntity.OutIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OutIntegral", Value = pQueryEntity.OutIntegral });
            if (pQueryEntity.EndIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EndIntegral", Value = pQueryEntity.EndIntegral });
            if (pQueryEntity.ImminentInvalidIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ImminentInvalidIntegral", Value = pQueryEntity.ImminentInvalidIntegral });
            if (pQueryEntity.InvalidIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "InvalidIntegral", Value = pQueryEntity.InvalidIntegral });
            if (pQueryEntity.ValidIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ValidIntegral", Value = pQueryEntity.ValidIntegral });
            if (pQueryEntity.CumulativeIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CumulativeIntegral", Value = pQueryEntity.CumulativeIntegral });
            if (pQueryEntity.ValidNotIntegral!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ValidNotIntegral", Value = pQueryEntity.ValidNotIntegral });
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
        protected void Load(IDataReader pReader, out VipIntegralEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipIntegralEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["VipID"] != DBNull.Value)
			{
				pInstance.VipID =  Convert.ToString(pReader["VipID"]);
			}
			if (pReader["VipCardCode"] != DBNull.Value)
			{
				pInstance.VipCardCode =  Convert.ToString(pReader["VipCardCode"]);
			}
			if (pReader["BeginIntegral"] != DBNull.Value)
			{
				pInstance.BeginIntegral =   Convert.ToInt32(pReader["BeginIntegral"]);
			}
			if (pReader["InIntegral"] != DBNull.Value)
			{
				pInstance.InIntegral =   Convert.ToInt32(pReader["InIntegral"]);
			}
			if (pReader["OutIntegral"] != DBNull.Value)
			{
				pInstance.OutIntegral =   Convert.ToInt32(pReader["OutIntegral"]);
			}
			if (pReader["EndIntegral"] != DBNull.Value)
			{
				pInstance.EndIntegral =   Convert.ToInt32(pReader["EndIntegral"]);
			}
			if (pReader["ImminentInvalidIntegral"] != DBNull.Value)
			{
				pInstance.ImminentInvalidIntegral =   Convert.ToInt32(pReader["ImminentInvalidIntegral"]);
			}
			if (pReader["InvalidIntegral"] != DBNull.Value)
			{
				pInstance.InvalidIntegral =   Convert.ToInt32(pReader["InvalidIntegral"]);
			}
			if (pReader["ValidIntegral"] != DBNull.Value)
			{
				pInstance.ValidIntegral =   Convert.ToInt32(pReader["ValidIntegral"]);
			}
			if (pReader["CumulativeIntegral"] != DBNull.Value)
			{
				pInstance.CumulativeIntegral =   Convert.ToInt32(pReader["CumulativeIntegral"]);
			}
			if (pReader["ValidNotIntegral"] != DBNull.Value)
			{
				pInstance.ValidNotIntegral =   Convert.ToInt32(pReader["ValidNotIntegral"]);
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
