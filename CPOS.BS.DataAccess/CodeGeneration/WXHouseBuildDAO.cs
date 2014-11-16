/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/6 13:13:26
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
    /// ��WXHouseBuild�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class WXHouseBuildDAO : Base.BaseCPOSDAO, ICRUDable<WXHouseBuildEntity>, IQueryable<WXHouseBuildEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public WXHouseBuildDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(WXHouseBuildEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(WXHouseBuildEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [WXHouseBuild](");
            strSql.Append("[HouseCode],[HouseName],[HouseImgURL],[Coordinate],[Hotline],[HouseAddr],[LowestPrice],[SaleHoseNum],[HouseOpenDate],[DeliverDate],[HouseState],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[HouseID])");
            strSql.Append(" values (");
            strSql.Append("@HouseCode,@HouseName,@HouseImgURL,@Coordinate,@Hotline,@HouseAddr,@LowestPrice,@SaleHoseNum,@HouseOpenDate,@DeliverDate,@HouseState,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@HouseID)");            

			Guid? pkGuid;
			if (pEntity.HouseID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.HouseID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@HouseCode",SqlDbType.NVarChar),
					new SqlParameter("@HouseName",SqlDbType.NVarChar),
					new SqlParameter("@HouseImgURL",SqlDbType.NVarChar),
					new SqlParameter("@Coordinate",SqlDbType.NVarChar),
					new SqlParameter("@Hotline",SqlDbType.NVarChar),
					new SqlParameter("@HouseAddr",SqlDbType.NVarChar),
					new SqlParameter("@LowestPrice",SqlDbType.Decimal),
					new SqlParameter("@SaleHoseNum",SqlDbType.Int),
					new SqlParameter("@HouseOpenDate",SqlDbType.DateTime),
					new SqlParameter("@DeliverDate",SqlDbType.DateTime),
					new SqlParameter("@HouseState",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@HouseID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.HouseCode;
			parameters[1].Value = pEntity.HouseName;
			parameters[2].Value = pEntity.HouseImgURL;
			parameters[3].Value = pEntity.Coordinate;
			parameters[4].Value = pEntity.Hotline;
			parameters[5].Value = pEntity.HouseAddr;
			parameters[6].Value = pEntity.LowestPrice;
			parameters[7].Value = pEntity.SaleHoseNum;
			parameters[8].Value = pEntity.HouseOpenDate;
			parameters[9].Value = pEntity.DeliverDate;
			parameters[10].Value = pEntity.HouseState;
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
            pEntity.HouseID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public WXHouseBuildEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseBuild] where HouseID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            WXHouseBuildEntity m = null;
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
        public WXHouseBuildEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseBuild] where isdelete=0");
            //��ȡ����
            List<WXHouseBuildEntity> list = new List<WXHouseBuildEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseBuildEntity m;
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
        public void Update(WXHouseBuildEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(WXHouseBuildEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.HouseID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [WXHouseBuild] set ");
            if (pIsUpdateNullField || pEntity.HouseCode!=null)
                strSql.Append( "[HouseCode]=@HouseCode,");
            if (pIsUpdateNullField || pEntity.HouseName!=null)
                strSql.Append( "[HouseName]=@HouseName,");
            if (pIsUpdateNullField || pEntity.HouseImgURL!=null)
                strSql.Append( "[HouseImgURL]=@HouseImgURL,");
            if (pIsUpdateNullField || pEntity.Coordinate!=null)
                strSql.Append( "[Coordinate]=@Coordinate,");
            if (pIsUpdateNullField || pEntity.Hotline!=null)
                strSql.Append( "[Hotline]=@Hotline,");
            if (pIsUpdateNullField || pEntity.HouseAddr!=null)
                strSql.Append( "[HouseAddr]=@HouseAddr,");
            if (pIsUpdateNullField || pEntity.LowestPrice!=null)
                strSql.Append( "[LowestPrice]=@LowestPrice,");
            if (pIsUpdateNullField || pEntity.SaleHoseNum!=null)
                strSql.Append( "[SaleHoseNum]=@SaleHoseNum,");
            if (pIsUpdateNullField || pEntity.HouseOpenDate!=null)
                strSql.Append( "[HouseOpenDate]=@HouseOpenDate,");
            if (pIsUpdateNullField || pEntity.DeliverDate!=null)
                strSql.Append( "[DeliverDate]=@DeliverDate,");
            if (pIsUpdateNullField || pEntity.HouseState!=null)
                strSql.Append( "[HouseState]=@HouseState,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where HouseID=@HouseID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@HouseCode",SqlDbType.NVarChar),
					new SqlParameter("@HouseName",SqlDbType.NVarChar),
					new SqlParameter("@HouseImgURL",SqlDbType.NVarChar),
					new SqlParameter("@Coordinate",SqlDbType.NVarChar),
					new SqlParameter("@Hotline",SqlDbType.NVarChar),
					new SqlParameter("@HouseAddr",SqlDbType.NVarChar),
					new SqlParameter("@LowestPrice",SqlDbType.Decimal),
					new SqlParameter("@SaleHoseNum",SqlDbType.Int),
					new SqlParameter("@HouseOpenDate",SqlDbType.DateTime),
					new SqlParameter("@DeliverDate",SqlDbType.DateTime),
					new SqlParameter("@HouseState",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@HouseID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.HouseCode;
			parameters[1].Value = pEntity.HouseName;
			parameters[2].Value = pEntity.HouseImgURL;
			parameters[3].Value = pEntity.Coordinate;
			parameters[4].Value = pEntity.Hotline;
			parameters[5].Value = pEntity.HouseAddr;
			parameters[6].Value = pEntity.LowestPrice;
			parameters[7].Value = pEntity.SaleHoseNum;
			parameters[8].Value = pEntity.HouseOpenDate;
			parameters[9].Value = pEntity.DeliverDate;
			parameters[10].Value = pEntity.HouseState;
			parameters[11].Value = pEntity.CustomerID;
			parameters[12].Value = pEntity.LastUpdateBy;
			parameters[13].Value = pEntity.LastUpdateTime;
			parameters[14].Value = pEntity.HouseID;

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
        public void Update(WXHouseBuildEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(WXHouseBuildEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(WXHouseBuildEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(WXHouseBuildEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.HouseID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.HouseID, pTran);           
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
            sql.AppendLine("update [WXHouseBuild] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where HouseID=@HouseID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@HouseID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(WXHouseBuildEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.HouseID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.HouseID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(WXHouseBuildEntity[] pEntities)
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
            sql.AppendLine("update [WXHouseBuild] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where HouseID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public WXHouseBuildEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [WXHouseBuild] where isdelete=0 ");
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
            List<WXHouseBuildEntity> list = new List<WXHouseBuildEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseBuildEntity m;
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
        public PagedQueryResult<WXHouseBuildEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [HouseID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [WXHouseBuild] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [WXHouseBuild] where isdelete=0 ");
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
            PagedQueryResult<WXHouseBuildEntity> result = new PagedQueryResult<WXHouseBuildEntity>();
            List<WXHouseBuildEntity> list = new List<WXHouseBuildEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    WXHouseBuildEntity m;
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
        public WXHouseBuildEntity[] QueryByEntity(WXHouseBuildEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<WXHouseBuildEntity> PagedQueryByEntity(WXHouseBuildEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(WXHouseBuildEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.HouseID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HouseID", Value = pQueryEntity.HouseID });
            if (pQueryEntity.HouseCode!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HouseCode", Value = pQueryEntity.HouseCode });
            if (pQueryEntity.HouseName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HouseName", Value = pQueryEntity.HouseName });
            if (pQueryEntity.HouseImgURL!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HouseImgURL", Value = pQueryEntity.HouseImgURL });
            if (pQueryEntity.Coordinate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Coordinate", Value = pQueryEntity.Coordinate });
            if (pQueryEntity.Hotline!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "Hotline", Value = pQueryEntity.Hotline });
            if (pQueryEntity.HouseAddr!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HouseAddr", Value = pQueryEntity.HouseAddr });
            if (pQueryEntity.LowestPrice!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LowestPrice", Value = pQueryEntity.LowestPrice });
            if (pQueryEntity.SaleHoseNum!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "SaleHoseNum", Value = pQueryEntity.SaleHoseNum });
            if (pQueryEntity.HouseOpenDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HouseOpenDate", Value = pQueryEntity.HouseOpenDate });
            if (pQueryEntity.DeliverDate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "DeliverDate", Value = pQueryEntity.DeliverDate });
            if (pQueryEntity.HouseState!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "HouseState", Value = pQueryEntity.HouseState });
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
        protected void Load(SqlDataReader pReader, out WXHouseBuildEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new WXHouseBuildEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["HouseID"] != DBNull.Value)
			{
				pInstance.HouseID =  (Guid)pReader["HouseID"];
			}
			if (pReader["HouseCode"] != DBNull.Value)
			{
				pInstance.HouseCode =  Convert.ToString(pReader["HouseCode"]);
			}
			if (pReader["HouseName"] != DBNull.Value)
			{
				pInstance.HouseName =  Convert.ToString(pReader["HouseName"]);
			}
			if (pReader["HouseImgURL"] != DBNull.Value)
			{
				pInstance.HouseImgURL =  Convert.ToString(pReader["HouseImgURL"]);
			}
			if (pReader["Coordinate"] != DBNull.Value)
			{
				pInstance.Coordinate =  Convert.ToString(pReader["Coordinate"]);
			}
			if (pReader["Hotline"] != DBNull.Value)
			{
				pInstance.Hotline =  Convert.ToString(pReader["Hotline"]);
			}
			if (pReader["HouseAddr"] != DBNull.Value)
			{
				pInstance.HouseAddr =  Convert.ToString(pReader["HouseAddr"]);
			}
			if (pReader["LowestPrice"] != DBNull.Value)
			{
				pInstance.LowestPrice =  Convert.ToDecimal(pReader["LowestPrice"]);
			}
			if (pReader["SaleHoseNum"] != DBNull.Value)
			{
				pInstance.SaleHoseNum =   Convert.ToInt32(pReader["SaleHoseNum"]);
			}
			if (pReader["HouseOpenDate"] != DBNull.Value)
			{
				pInstance.HouseOpenDate =  Convert.ToDateTime(pReader["HouseOpenDate"]);
			}
			if (pReader["DeliverDate"] != DBNull.Value)
			{
				pInstance.DeliverDate =  Convert.ToDateTime(pReader["DeliverDate"]);
			}
			if (pReader["HouseState"] != DBNull.Value)
			{
				pInstance.HouseState =   Convert.ToInt32(pReader["HouseState"]);
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
