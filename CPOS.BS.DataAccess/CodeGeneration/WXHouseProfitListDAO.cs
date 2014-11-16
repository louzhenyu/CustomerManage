/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/25 22:14:04
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
    /// ��WXHouseProfitList�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseProfitListDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseProfitListEntity>, IQueryable<WXHouseProfitListEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseProfitListDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXHouseProfitListEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXHouseProfitListEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXHouseProfitList](");
            strSql.Append("[AssetsDate],[Assignbuyer],[ThirdOrderNo],[TotalAssetsMoney],[AvailableMoney],[GrandProfit],[NewProfit],[OtherProfit],[NoteInformation],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[ProfitID])");
            strSql.Append(" values (");
            strSql.Append("@AssetsDate,@Assignbuyer,@ThirdOrderNo,@TotalAssetsMoney,@AvailableMoney,@GrandProfit,@NewProfit,@OtherProfit,@NoteInformation,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@ProfitID)");            

			Guid? pkGuid;
			if (pEntity.ProfitID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.ProfitID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@AssetsDate",SqlDbType.VarChar),
					new SqlParameter("@Assignbuyer",SqlDbType.NVarChar),
					new SqlParameter("@ThirdOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@TotalAssetsMoney",SqlDbType.Decimal),
					new SqlParameter("@AvailableMoney",SqlDbType.Decimal),
					new SqlParameter("@GrandProfit",SqlDbType.Decimal),
					new SqlParameter("@NewProfit",SqlDbType.Decimal),
					new SqlParameter("@OtherProfit",SqlDbType.Decimal),
					new SqlParameter("@NoteInformation",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@ProfitID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.AssetsDate;
			parameters[1].Value = pEntity.Assignbuyer;
			parameters[2].Value = pEntity.ThirdOrderNo;
			parameters[3].Value = pEntity.TotalAssetsMoney;
			parameters[4].Value = pEntity.AvailableMoney;
			parameters[5].Value = pEntity.GrandProfit;
			parameters[6].Value = pEntity.NewProfit;
			parameters[7].Value = pEntity.OtherProfit;
			parameters[8].Value = pEntity.NoteInformation;
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
            pEntity.ProfitID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseProfitListEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseProfitList] where ProfitID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WXHouseProfitListEntity m = null;
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
        public WXHouseProfitListEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseProfitList] where isdelete=0");
            //��ȡ����
            List<WXHouseProfitListEntity> list = new List<WXHouseProfitListEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseProfitListEntity m;
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
        public void Update(WXHouseProfitListEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WXHouseProfitListEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ProfitID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXHouseProfitList] set ");
            if (pIsUpdateNullField || pEntity.AssetsDate!=null)
                strSql.Append( "[AssetsDate]=@AssetsDate,");
            if (pIsUpdateNullField || pEntity.Assignbuyer!=null)
                strSql.Append( "[Assignbuyer]=@Assignbuyer,");
            if (pIsUpdateNullField || pEntity.ThirdOrderNo!=null)
                strSql.Append( "[ThirdOrderNo]=@ThirdOrderNo,");
            if (pIsUpdateNullField || pEntity.TotalAssetsMoney!=null)
                strSql.Append( "[TotalAssetsMoney]=@TotalAssetsMoney,");
            if (pIsUpdateNullField || pEntity.AvailableMoney!=null)
                strSql.Append( "[AvailableMoney]=@AvailableMoney,");
            if (pIsUpdateNullField || pEntity.GrandProfit!=null)
                strSql.Append( "[GrandProfit]=@GrandProfit,");
            if (pIsUpdateNullField || pEntity.NewProfit!=null)
                strSql.Append( "[NewProfit]=@NewProfit,");
            if (pIsUpdateNullField || pEntity.OtherProfit!=null)
                strSql.Append( "[OtherProfit]=@OtherProfit,");
            if (pIsUpdateNullField || pEntity.NoteInformation!=null)
                strSql.Append( "[NoteInformation]=@NoteInformation,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where ProfitID=@ProfitID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@AssetsDate",SqlDbType.VarChar),
					new SqlParameter("@Assignbuyer",SqlDbType.NVarChar),
					new SqlParameter("@ThirdOrderNo",SqlDbType.NVarChar),
					new SqlParameter("@TotalAssetsMoney",SqlDbType.Decimal),
					new SqlParameter("@AvailableMoney",SqlDbType.Decimal),
					new SqlParameter("@GrandProfit",SqlDbType.Decimal),
					new SqlParameter("@NewProfit",SqlDbType.Decimal),
					new SqlParameter("@OtherProfit",SqlDbType.Decimal),
					new SqlParameter("@NoteInformation",SqlDbType.NVarChar),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@ProfitID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.AssetsDate;
			parameters[1].Value = pEntity.Assignbuyer;
			parameters[2].Value = pEntity.ThirdOrderNo;
			parameters[3].Value = pEntity.TotalAssetsMoney;
			parameters[4].Value = pEntity.AvailableMoney;
			parameters[5].Value = pEntity.GrandProfit;
			parameters[6].Value = pEntity.NewProfit;
			parameters[7].Value = pEntity.OtherProfit;
			parameters[8].Value = pEntity.NoteInformation;
			parameters[9].Value = pEntity.CustomerID;
			parameters[10].Value = pEntity.LastUpdateBy;
			parameters[11].Value = pEntity.LastUpdateTime;
			parameters[12].Value = pEntity.ProfitID;

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
        public void Update(WXHouseProfitListEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WXHouseProfitListEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXHouseProfitListEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXHouseProfitListEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.ProfitID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.ProfitID, pTran);           
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
            sql.AppendLine("update [WXHouseProfitList] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where ProfitID=@ProfitID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@ProfitID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXHouseProfitListEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.ProfitID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.ProfitID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXHouseProfitListEntity[] pEntities)
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
            sql.AppendLine("update [WXHouseProfitList] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where ProfitID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXHouseProfitListEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseProfitList] where isdelete=0 ");
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
            List<WXHouseProfitListEntity> list = new List<WXHouseProfitListEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseProfitListEntity m;
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
        public PagedQueryResult<WXHouseProfitListEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [ProfitID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXHouseProfitList] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXHouseProfitList] where isdelete=0 ");
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
            PagedQueryResult<WXHouseProfitListEntity> result = new PagedQueryResult<WXHouseProfitListEntity>();
            List<WXHouseProfitListEntity> list = new List<WXHouseProfitListEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseProfitListEntity m;
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
        public WXHouseProfitListEntity[] QueryByEntity(WXHouseProfitListEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXHouseProfitListEntity> PagedQueryByEntity(WXHouseProfitListEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXHouseProfitListEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.ProfitID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ProfitID", Value = pQueryEntity.ProfitID });
            if (pQueryEntity.AssetsDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AssetsDate", Value = pQueryEntity.AssetsDate });
            if (pQueryEntity.Assignbuyer!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Assignbuyer", Value = pQueryEntity.Assignbuyer });
            if (pQueryEntity.ThirdOrderNo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ThirdOrderNo", Value = pQueryEntity.ThirdOrderNo });
            if (pQueryEntity.TotalAssetsMoney!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TotalAssetsMoney", Value = pQueryEntity.TotalAssetsMoney });
            if (pQueryEntity.AvailableMoney!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AvailableMoney", Value = pQueryEntity.AvailableMoney });
            if (pQueryEntity.GrandProfit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "GrandProfit", Value = pQueryEntity.GrandProfit });
            if (pQueryEntity.NewProfit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NewProfit", Value = pQueryEntity.NewProfit });
            if (pQueryEntity.OtherProfit!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "OtherProfit", Value = pQueryEntity.OtherProfit });
            if (pQueryEntity.NoteInformation!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "NoteInformation", Value = pQueryEntity.NoteInformation });
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
        protected void Load(SqlDataReader pReader, out WXHouseProfitListEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXHouseProfitListEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["ProfitID"] != DBNull.Value)
			{
				pInstance.ProfitID =  (Guid)pReader["ProfitID"];
			}
			if (pReader["AssetsDate"] != DBNull.Value)
			{
				pInstance.AssetsDate =  Convert.ToString(pReader["AssetsDate"]);
			}
			if (pReader["Assignbuyer"] != DBNull.Value)
			{
				pInstance.Assignbuyer =  Convert.ToString(pReader["Assignbuyer"]);
			}
			if (pReader["ThirdOrderNo"] != DBNull.Value)
			{
				pInstance.ThirdOrderNo =  Convert.ToString(pReader["ThirdOrderNo"]);
			}
			if (pReader["TotalAssetsMoney"] != DBNull.Value)
			{
				pInstance.TotalAssetsMoney =  Convert.ToDecimal(pReader["TotalAssetsMoney"]);
			}
			if (pReader["AvailableMoney"] != DBNull.Value)
			{
				pInstance.AvailableMoney =  Convert.ToDecimal(pReader["AvailableMoney"]);
			}
			if (pReader["GrandProfit"] != DBNull.Value)
			{
				pInstance.GrandProfit =  Convert.ToDecimal(pReader["GrandProfit"]);
			}
			if (pReader["NewProfit"] != DBNull.Value)
			{
				pInstance.NewProfit =  Convert.ToDecimal(pReader["NewProfit"]);
			}
			if (pReader["OtherProfit"] != DBNull.Value)
			{
				pInstance.OtherProfit =  Convert.ToDecimal(pReader["OtherProfit"]);
			}
			if (pReader["NoteInformation"] != DBNull.Value)
			{
				pInstance.NoteInformation =  Convert.ToString(pReader["NoteInformation"]);
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
