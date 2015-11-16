/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-8-14 21:54:11
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
    /// ��VipCardTransLogHis�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardTransLogHisDAO : Base.BaseCPOSDAO, ICRUDable<VipCardTransLogHisEntity>, IQueryable<VipCardTransLogHisEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public VipCardTransLogHisDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(VipCardTransLogHisEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(VipCardTransLogHisEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [VipCardTransLogHis](");
            strSql.Append("[TransID],[VipCode],[VipName],[VipCardCode],[VipCardTypeCode],[UnitCode],[UnitName],[OrderNo],[BillNo],[TransType],[TransTerminalType],[TransTerminalCode],[TransAddress],[TransContent],[TransTime],[TransAmount],[TransBalance],[RequestJSON],[CheckCode],[CreateTime],[CreateBy],[LastUpdateTime],[LastUpdateBy],[IsDelete],[CustomerID])");
            strSql.Append(" values (");
            strSql.Append("@TransID,@VipCode,@VipName,@VipCardCode,@VipCardTypeCode,@UnitCode,@UnitName,@OrderNo,@BillNo,@TransType,@TransTerminalType,@TransTerminalCode,@TransAddress,@TransContent,@TransTime,@TransAmount,@TransBalance,@RequestJSON,@CheckCode,@CreateTime,@CreateBy,@LastUpdateTime,@LastUpdateBy,@IsDelete,@CustomerID)");            
			strSql.AppendFormat("{0}select SCOPE_IDENTITY();", Environment.NewLine);


            SqlParameter[] parameters = 
            {
					new SqlParameter("@TransID",SqlDbType.Int),
					new SqlParameter("@VipCode",SqlDbType.NVarChar),
					new SqlParameter("@VipName",SqlDbType.NVarChar),
					new SqlParameter("@VipCardCode",SqlDbType.NVarChar),
					new SqlParameter("@VipCardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@BillNo",SqlDbType.NVarChar),
					new SqlParameter("@TransType",SqlDbType.NVarChar),
					new SqlParameter("@TransTerminalType",SqlDbType.NVarChar),
					new SqlParameter("@TransTerminalCode",SqlDbType.NVarChar),
					new SqlParameter("@TransAddress",SqlDbType.NVarChar),
					new SqlParameter("@TransContent",SqlDbType.NVarChar),
					new SqlParameter("@TransTime",SqlDbType.DateTime),
					new SqlParameter("@TransAmount",SqlDbType.Decimal),
					new SqlParameter("@TransBalance",SqlDbType.Decimal),
					new SqlParameter("@RequestJSON",SqlDbType.NVarChar),
					new SqlParameter("@CheckCode",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar)
            };
			parameters[0].Value = pEntity.TransID;
			parameters[1].Value = pEntity.VipCode;
			parameters[2].Value = pEntity.VipName;
			parameters[3].Value = pEntity.VipCardCode;
			parameters[4].Value = pEntity.VipCardTypeCode;
			parameters[5].Value = pEntity.UnitCode;
			parameters[6].Value = pEntity.UnitName;
			parameters[7].Value = pEntity.OrderNo;
			parameters[8].Value = pEntity.BillNo;
			parameters[9].Value = pEntity.TransType;
			parameters[10].Value = pEntity.TransTerminalType;
			parameters[11].Value = pEntity.TransTerminalCode;
			parameters[12].Value = pEntity.TransAddress;
			parameters[13].Value = pEntity.TransContent;
			parameters[14].Value = pEntity.TransTime;
			parameters[15].Value = pEntity.TransAmount;
			parameters[16].Value = pEntity.TransBalance;
			parameters[17].Value = pEntity.RequestJSON;
			parameters[18].Value = pEntity.CheckCode;
			parameters[19].Value = pEntity.CreateTime;
			parameters[20].Value = pEntity.CreateBy;
			parameters[21].Value = pEntity.LastUpdateTime;
			parameters[22].Value = pEntity.LastUpdateBy;
			parameters[23].Value = pEntity.IsDelete;
			parameters[24].Value = pEntity.CustomerID;

            //ִ�в��������д
            object result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteScalar((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.TransHisID = Convert.ToInt32(result);
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public VipCardTransLogHisEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardTransLogHis] where TransHisID='{0}'  and isdelete=0 ", id.ToString());
            //��ȡ����
            VipCardTransLogHisEntity m = null;
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
        public VipCardTransLogHisEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardTransLogHis] where 1=1  and isdelete=0");
            //��ȡ����
            List<VipCardTransLogHisEntity> list = new List<VipCardTransLogHisEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardTransLogHisEntity m;
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
        public void Update(VipCardTransLogHisEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity , pTran,true);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Update(VipCardTransLogHisEntity pEntity , IDbTransaction pTran,bool pIsUpdateNullField)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.TransHisID.HasValue)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
			pEntity.LastUpdateTime=DateTime.Now;
			pEntity.LastUpdateBy=CurrentUserInfo.UserID;


            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [VipCardTransLogHis] set ");
                        if (pIsUpdateNullField || pEntity.TransID!=null)
                strSql.Append( "[TransID]=@TransID,");
            if (pIsUpdateNullField || pEntity.VipCode!=null)
                strSql.Append( "[VipCode]=@VipCode,");
            if (pIsUpdateNullField || pEntity.VipName!=null)
                strSql.Append( "[VipName]=@VipName,");
            if (pIsUpdateNullField || pEntity.VipCardCode!=null)
                strSql.Append( "[VipCardCode]=@VipCardCode,");
            if (pIsUpdateNullField || pEntity.VipCardTypeCode!=null)
                strSql.Append( "[VipCardTypeCode]=@VipCardTypeCode,");
            if (pIsUpdateNullField || pEntity.UnitCode!=null)
                strSql.Append( "[UnitCode]=@UnitCode,");
            if (pIsUpdateNullField || pEntity.UnitName!=null)
                strSql.Append( "[UnitName]=@UnitName,");
            if (pIsUpdateNullField || pEntity.OrderNo!=null)
                strSql.Append( "[OrderNo]=@OrderNo,");
            if (pIsUpdateNullField || pEntity.BillNo!=null)
                strSql.Append( "[BillNo]=@BillNo,");
            if (pIsUpdateNullField || pEntity.TransType!=null)
                strSql.Append( "[TransType]=@TransType,");
            if (pIsUpdateNullField || pEntity.TransTerminalType!=null)
                strSql.Append( "[TransTerminalType]=@TransTerminalType,");
            if (pIsUpdateNullField || pEntity.TransTerminalCode!=null)
                strSql.Append( "[TransTerminalCode]=@TransTerminalCode,");
            if (pIsUpdateNullField || pEntity.TransAddress!=null)
                strSql.Append( "[TransAddress]=@TransAddress,");
            if (pIsUpdateNullField || pEntity.TransContent!=null)
                strSql.Append( "[TransContent]=@TransContent,");
            if (pIsUpdateNullField || pEntity.TransTime!=null)
                strSql.Append( "[TransTime]=@TransTime,");
            if (pIsUpdateNullField || pEntity.TransAmount!=null)
                strSql.Append( "[TransAmount]=@TransAmount,");
            if (pIsUpdateNullField || pEntity.TransBalance!=null)
                strSql.Append( "[TransBalance]=@TransBalance,");
            if (pIsUpdateNullField || pEntity.RequestJSON!=null)
                strSql.Append( "[RequestJSON]=@RequestJSON,");
            if (pIsUpdateNullField || pEntity.CheckCode!=null)
                strSql.Append( "[CheckCode]=@CheckCode,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID");
            strSql.Append(" where TransHisID=@TransHisID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TransID",SqlDbType.Int),
					new SqlParameter("@VipCode",SqlDbType.NVarChar),
					new SqlParameter("@VipName",SqlDbType.NVarChar),
					new SqlParameter("@VipCardCode",SqlDbType.NVarChar),
					new SqlParameter("@VipCardTypeCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitCode",SqlDbType.NVarChar),
					new SqlParameter("@UnitName",SqlDbType.NVarChar),
					new SqlParameter("@OrderNo",SqlDbType.NVarChar),
					new SqlParameter("@BillNo",SqlDbType.NVarChar),
					new SqlParameter("@TransType",SqlDbType.NVarChar),
					new SqlParameter("@TransTerminalType",SqlDbType.NVarChar),
					new SqlParameter("@TransTerminalCode",SqlDbType.NVarChar),
					new SqlParameter("@TransAddress",SqlDbType.NVarChar),
					new SqlParameter("@TransContent",SqlDbType.NVarChar),
					new SqlParameter("@TransTime",SqlDbType.DateTime),
					new SqlParameter("@TransAmount",SqlDbType.Decimal),
					new SqlParameter("@TransBalance",SqlDbType.Decimal),
					new SqlParameter("@RequestJSON",SqlDbType.NVarChar),
					new SqlParameter("@CheckCode",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@TransHisID",SqlDbType.Int)
            };
			parameters[0].Value = pEntity.TransID;
			parameters[1].Value = pEntity.VipCode;
			parameters[2].Value = pEntity.VipName;
			parameters[3].Value = pEntity.VipCardCode;
			parameters[4].Value = pEntity.VipCardTypeCode;
			parameters[5].Value = pEntity.UnitCode;
			parameters[6].Value = pEntity.UnitName;
			parameters[7].Value = pEntity.OrderNo;
			parameters[8].Value = pEntity.BillNo;
			parameters[9].Value = pEntity.TransType;
			parameters[10].Value = pEntity.TransTerminalType;
			parameters[11].Value = pEntity.TransTerminalCode;
			parameters[12].Value = pEntity.TransAddress;
			parameters[13].Value = pEntity.TransContent;
			parameters[14].Value = pEntity.TransTime;
			parameters[15].Value = pEntity.TransAmount;
			parameters[16].Value = pEntity.TransBalance;
			parameters[17].Value = pEntity.RequestJSON;
			parameters[18].Value = pEntity.CheckCode;
			parameters[19].Value = pEntity.LastUpdateTime;
			parameters[20].Value = pEntity.LastUpdateBy;
			parameters[21].Value = pEntity.CustomerID;
			parameters[22].Value = pEntity.TransHisID;

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
        public void Update(VipCardTransLogHisEntity pEntity )
        {
            this.Update(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(VipCardTransLogHisEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(VipCardTransLogHisEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (!pEntity.TransHisID.HasValue)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.TransHisID.Value, pTran);           
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
            sql.AppendLine("update [VipCardTransLogHis] set  isdelete=1 where TransHisID=@TransHisID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@TransHisID",SqlDbType=SqlDbType.VarChar,Value=pID}
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
        public void Delete(VipCardTransLogHisEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var pEntity = pEntities[i];
                //����У��
                if (pEntity == null)
                    throw new ArgumentNullException("pEntity");
                if (!pEntity.TransHisID.HasValue)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = pEntity.TransHisID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(VipCardTransLogHisEntity[] pEntities)
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
                primaryKeys.AppendFormat("{0},",item.ToString());
            }
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("update [VipCardTransLogHis] set  isdelete=1 where TransHisID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public VipCardTransLogHisEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipCardTransLogHis] where 1=1  and isdelete=0 ");
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
            List<VipCardTransLogHisEntity> list = new List<VipCardTransLogHisEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardTransLogHisEntity m;
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
        public PagedQueryResult<VipCardTransLogHisEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [TransHisID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [VipCardTransLogHis] where 1=1  and isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [VipCardTransLogHis] where 1=1  and isdelete=0 ");
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
            PagedQueryResult<VipCardTransLogHisEntity> result = new PagedQueryResult<VipCardTransLogHisEntity>();
            List<VipCardTransLogHisEntity> list = new List<VipCardTransLogHisEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    VipCardTransLogHisEntity m;
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
        public VipCardTransLogHisEntity[] QueryByEntity(VipCardTransLogHisEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<VipCardTransLogHisEntity> PagedQueryByEntity(VipCardTransLogHisEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(VipCardTransLogHisEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.TransHisID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransHisID", Value = pQueryEntity.TransHisID });
            if (pQueryEntity.TransID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransID", Value = pQueryEntity.TransID });
            if (pQueryEntity.VipCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCode", Value = pQueryEntity.VipCode });
            if (pQueryEntity.VipName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipName", Value = pQueryEntity.VipName });
            if (pQueryEntity.VipCardCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardCode", Value = pQueryEntity.VipCardCode });
            if (pQueryEntity.VipCardTypeCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "VipCardTypeCode", Value = pQueryEntity.VipCardTypeCode });
            if (pQueryEntity.UnitCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitCode", Value = pQueryEntity.UnitCode });
            if (pQueryEntity.UnitName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "UnitName", Value = pQueryEntity.UnitName });
            if (pQueryEntity.OrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OrderNo", Value = pQueryEntity.OrderNo });
            if (pQueryEntity.BillNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "BillNo", Value = pQueryEntity.BillNo });
            if (pQueryEntity.TransType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransType", Value = pQueryEntity.TransType });
            if (pQueryEntity.TransTerminalType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransTerminalType", Value = pQueryEntity.TransTerminalType });
            if (pQueryEntity.TransTerminalCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransTerminalCode", Value = pQueryEntity.TransTerminalCode });
            if (pQueryEntity.TransAddress!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransAddress", Value = pQueryEntity.TransAddress });
            if (pQueryEntity.TransContent!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransContent", Value = pQueryEntity.TransContent });
            if (pQueryEntity.TransTime!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransTime", Value = pQueryEntity.TransTime });
            if (pQueryEntity.TransAmount!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransAmount", Value = pQueryEntity.TransAmount });
            if (pQueryEntity.TransBalance!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TransBalance", Value = pQueryEntity.TransBalance });
            if (pQueryEntity.RequestJSON!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "RequestJSON", Value = pQueryEntity.RequestJSON });
            if (pQueryEntity.CheckCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CheckCode", Value = pQueryEntity.CheckCode });
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

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(IDataReader pReader, out VipCardTransLogHisEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new VipCardTransLogHisEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["TransHisID"] != DBNull.Value)
			{
				pInstance.TransHisID =   Convert.ToInt32(pReader["TransHisID"]);
			}
			if (pReader["TransID"] != DBNull.Value)
			{
				pInstance.TransID =   Convert.ToInt32(pReader["TransID"]);
			}
			if (pReader["VipCode"] != DBNull.Value)
			{
				pInstance.VipCode =  Convert.ToString(pReader["VipCode"]);
			}
			if (pReader["VipName"] != DBNull.Value)
			{
				pInstance.VipName =  Convert.ToString(pReader["VipName"]);
			}
			if (pReader["VipCardCode"] != DBNull.Value)
			{
				pInstance.VipCardCode =  Convert.ToString(pReader["VipCardCode"]);
			}
			if (pReader["VipCardTypeCode"] != DBNull.Value)
			{
				pInstance.VipCardTypeCode =  Convert.ToString(pReader["VipCardTypeCode"]);
			}
			if (pReader["UnitCode"] != DBNull.Value)
			{
				pInstance.UnitCode =  Convert.ToString(pReader["UnitCode"]);
			}
			if (pReader["UnitName"] != DBNull.Value)
			{
				pInstance.UnitName =  Convert.ToString(pReader["UnitName"]);
			}
			if (pReader["OrderNo"] != DBNull.Value)
			{
				pInstance.OrderNo =  Convert.ToString(pReader["OrderNo"]);
			}
			if (pReader["BillNo"] != DBNull.Value)
			{
				pInstance.BillNo =  Convert.ToString(pReader["BillNo"]);
			}
			if (pReader["TransType"] != DBNull.Value)
			{
				pInstance.TransType =  Convert.ToString(pReader["TransType"]);
			}
			if (pReader["TransTerminalType"] != DBNull.Value)
			{
				pInstance.TransTerminalType =  Convert.ToString(pReader["TransTerminalType"]);
			}
			if (pReader["TransTerminalCode"] != DBNull.Value)
			{
				pInstance.TransTerminalCode =  Convert.ToString(pReader["TransTerminalCode"]);
			}
			if (pReader["TransAddress"] != DBNull.Value)
			{
				pInstance.TransAddress =  Convert.ToString(pReader["TransAddress"]);
			}
			if (pReader["TransContent"] != DBNull.Value)
			{
				pInstance.TransContent =  Convert.ToString(pReader["TransContent"]);
			}
			if (pReader["TransTime"] != DBNull.Value)
			{
				pInstance.TransTime =  Convert.ToDateTime(pReader["TransTime"]);
			}
			if (pReader["TransAmount"] != DBNull.Value)
			{
				pInstance.TransAmount =  Convert.ToDecimal(pReader["TransAmount"]);
			}
			if (pReader["TransBalance"] != DBNull.Value)
			{
				pInstance.TransBalance =  Convert.ToDecimal(pReader["TransBalance"]);
			}
			if (pReader["RequestJSON"] != DBNull.Value)
			{
				pInstance.RequestJSON =  Convert.ToString(pReader["RequestJSON"]);
			}
			if (pReader["CheckCode"] != DBNull.Value)
			{
				pInstance.CheckCode =  Convert.ToString(pReader["CheckCode"]);
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

        }
        #endregion
    }
}
