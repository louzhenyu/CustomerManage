/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/8 11:16:03
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
    /// ��MobileBussinessDefined�����ݷ�����     
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MobileBussinessDefinedDAO : Base.BaseCPOSDAO, ICRUDable<MobileBussinessDefinedEntity>, IQueryable<MobileBussinessDefinedEntity>
    {
        #region ���캯��
        /// <summary>
        /// ���캯�� 
        /// </summary>
        public MobileBussinessDefinedDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }
        #endregion

        #region ICRUDable ��Ա
        /// <summary>
        /// ����һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create(MobileBussinessDefinedEntity pEntity)
        {
            this.Create(pEntity, null);
        }

        /// <summary>
        /// �������ڴ���һ����ʵ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Create(MobileBussinessDefinedEntity pEntity, IDbTransaction pTran)
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
            strSql.Append("insert into [MobileBussinessDefined](");
            strSql.Append("[TableName],[MobilePageBlockID],[ColumnDesc],[ColumnDescEn],[ColumnName],[LinkageItem],[CorrelationValue],[ExampleValue],[ControlType],[AuthType],[MinLength],[MaxLength],[MinSelected],[MaxSelected],[IsMustDo],[EditOrder],[ViewOrder],[TypeID],[CustomerID],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[AttributeTypeID],[IsTemplate],[MobileBussinessDefinedID])");
            strSql.Append(" values (");
            strSql.Append("@TableName,@MobilePageBlockID,@ColumnDesc,@ColumnDescEn,@ColumnName,@LinkageItem,@CorrelationValue,@ExampleValue,@ControlType,@AuthType,@MinLength,@MaxLength,@MinSelected,@MaxSelected,@IsMustDo,@EditOrder,@ViewOrder,@TypeID,@CustomerID,@CreateBy,@CreateTime,@LastUpdateBy,@LastUpdateTime,@IsDelete,@AttributeTypeID,@IsTemplate,@MobileBussinessDefinedID)");            

			Guid? pkGuid;
			if (pEntity.MobileBussinessDefinedID == null)
				pkGuid = Guid.NewGuid();
			else
				pkGuid = pEntity.MobileBussinessDefinedID;

            SqlParameter[] parameters = 
            {
					new SqlParameter("@TableName",SqlDbType.NVarChar),
					new SqlParameter("@MobilePageBlockID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ColumnDesc",SqlDbType.NVarChar),
					new SqlParameter("@ColumnDescEn",SqlDbType.NVarChar),
					new SqlParameter("@ColumnName",SqlDbType.NVarChar),
					new SqlParameter("@LinkageItem",SqlDbType.NVarChar),
					new SqlParameter("@CorrelationValue",SqlDbType.NVarChar),
					new SqlParameter("@ExampleValue",SqlDbType.NVarChar),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@AuthType",SqlDbType.Int),
					new SqlParameter("@MinLength",SqlDbType.Int),
					new SqlParameter("@MaxLength",SqlDbType.Int),
					new SqlParameter("@MinSelected",SqlDbType.Int),
					new SqlParameter("@MaxSelected",SqlDbType.Int),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@EditOrder",SqlDbType.Int),
					new SqlParameter("@ViewOrder",SqlDbType.Int),
					new SqlParameter("@TypeID",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@CreateBy",SqlDbType.NVarChar),
					new SqlParameter("@CreateTime",SqlDbType.DateTime),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@IsDelete",SqlDbType.Int),
					new SqlParameter("@AttributeTypeID",SqlDbType.Int),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@MobileBussinessDefinedID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TableName;
			parameters[1].Value = pEntity.MobilePageBlockID;
			parameters[2].Value = pEntity.ColumnDesc;
			parameters[3].Value = pEntity.ColumnDescEn;
			parameters[4].Value = pEntity.ColumnName;
			parameters[5].Value = pEntity.LinkageItem;
			parameters[6].Value = pEntity.CorrelationValue;
			parameters[7].Value = pEntity.ExampleValue;
			parameters[8].Value = pEntity.ControlType;
			parameters[9].Value = pEntity.AuthType;
			parameters[10].Value = pEntity.MinLength;
			parameters[11].Value = pEntity.MaxLength;
			parameters[12].Value = pEntity.MinSelected;
			parameters[13].Value = pEntity.MaxSelected;
			parameters[14].Value = pEntity.IsMustDo;
			parameters[15].Value = pEntity.EditOrder;
			parameters[16].Value = pEntity.ViewOrder;
			parameters[17].Value = pEntity.TypeID;
			parameters[18].Value = pEntity.CustomerID;
			parameters[19].Value = pEntity.CreateBy;
			parameters[20].Value = pEntity.CreateTime;
			parameters[21].Value = pEntity.LastUpdateBy;
			parameters[22].Value = pEntity.LastUpdateTime;
			parameters[23].Value = pEntity.IsDelete;
			parameters[24].Value = pEntity.AttributeTypeID;
			parameters[25].Value = pEntity.IsTemplate;
			parameters[26].Value = pkGuid;

            //ִ�в��������д
            int result;
            if (pTran != null)
               result= this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, strSql.ToString(), parameters);
            else
               result= this.SQLHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters); 
            pEntity.MobileBussinessDefinedID = pkGuid;
        }

        /// <summary>
        /// ���ݱ�ʶ����ȡʵ��
        /// </summary>
        /// <param name="pID">��ʶ����ֵ</param>
        public MobileBussinessDefinedEntity GetByID(object pID)
        {
            //�������
            if (pID == null)
                return null;
            string id = pID.ToString();
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileBussinessDefined] where MobileBussinessDefinedID='{0}' and IsDelete=0 ", id.ToString());
            //��ȡ����
            MobileBussinessDefinedEntity m = null;
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
        public MobileBussinessDefinedEntity[] GetAll()
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileBussinessDefined] where isdelete=0");
            //��ȡ����
            List<MobileBussinessDefinedEntity> list = new List<MobileBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileBussinessDefinedEntity m;
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
        public void Update(MobileBussinessDefinedEntity pEntity , IDbTransaction pTran)
        {
            Update(pEntity,true,pTran);
        }
        public void Update(MobileBussinessDefinedEntity pEntity , bool pIsUpdateNullField, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MobileBussinessDefinedID==null)
            {
                throw new ArgumentException("ִ�и���ʱ,ʵ�����������ֵ����Ϊnull.");
            }
             //��ʼ���̶��ֶ�
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.LastUpdateBy = CurrentUserInfo.UserID;

            //��֯������SQL
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [MobileBussinessDefined] set ");
            if (pIsUpdateNullField || pEntity.TableName!=null)
                strSql.Append( "[TableName]=@TableName,");
            if (pIsUpdateNullField || pEntity.MobilePageBlockID!=null)
                strSql.Append( "[MobilePageBlockID]=@MobilePageBlockID,");
            if (pIsUpdateNullField || pEntity.ColumnDesc!=null)
                strSql.Append( "[ColumnDesc]=@ColumnDesc,");
            if (pIsUpdateNullField || pEntity.ColumnDescEn!=null)
                strSql.Append( "[ColumnDescEn]=@ColumnDescEn,");
            if (pIsUpdateNullField || pEntity.ColumnName!=null)
                strSql.Append( "[ColumnName]=@ColumnName,");
            if (pIsUpdateNullField || pEntity.LinkageItem!=null)
                strSql.Append( "[LinkageItem]=@LinkageItem,");
            if (pIsUpdateNullField || pEntity.CorrelationValue!=null)
                strSql.Append( "[CorrelationValue]=@CorrelationValue,");
            if (pIsUpdateNullField || pEntity.ExampleValue!=null)
                strSql.Append( "[ExampleValue]=@ExampleValue,");
            if (pIsUpdateNullField || pEntity.ControlType!=null)
                strSql.Append( "[ControlType]=@ControlType,");
            if (pIsUpdateNullField || pEntity.AuthType!=null)
                strSql.Append( "[AuthType]=@AuthType,");
            if (pIsUpdateNullField || pEntity.MinLength!=null)
                strSql.Append( "[MinLength]=@MinLength,");
            if (pIsUpdateNullField || pEntity.MaxLength!=null)
                strSql.Append( "[MaxLength]=@MaxLength,");
            if (pIsUpdateNullField || pEntity.MinSelected!=null)
                strSql.Append( "[MinSelected]=@MinSelected,");
            if (pIsUpdateNullField || pEntity.MaxSelected!=null)
                strSql.Append( "[MaxSelected]=@MaxSelected,");
            if (pIsUpdateNullField || pEntity.IsMustDo!=null)
                strSql.Append( "[IsMustDo]=@IsMustDo,");
            if (pIsUpdateNullField || pEntity.EditOrder!=null)
                strSql.Append( "[EditOrder]=@EditOrder,");
            if (pIsUpdateNullField || pEntity.ViewOrder!=null)
                strSql.Append( "[ViewOrder]=@ViewOrder,");
            if (pIsUpdateNullField || pEntity.TypeID!=null)
                strSql.Append( "[TypeID]=@TypeID,");
            if (pIsUpdateNullField || pEntity.CustomerID!=null)
                strSql.Append( "[CustomerID]=@CustomerID,");
            if (pIsUpdateNullField || pEntity.LastUpdateBy!=null)
                strSql.Append( "[LastUpdateBy]=@LastUpdateBy,");
            if (pIsUpdateNullField || pEntity.LastUpdateTime!=null)
                strSql.Append( "[LastUpdateTime]=@LastUpdateTime,");
            if (pIsUpdateNullField || pEntity.AttributeTypeID!=null)
                strSql.Append( "[AttributeTypeID]=@AttributeTypeID,");
            if (pIsUpdateNullField || pEntity.IsTemplate!=null)
                strSql.Append( "[IsTemplate]=@IsTemplate");
            if (strSql.ToString().EndsWith(","))
                strSql.Remove(strSql.Length - 1, 1);
            strSql.Append(" where MobileBussinessDefinedID=@MobileBussinessDefinedID ");
            SqlParameter[] parameters = 
            {
					new SqlParameter("@TableName",SqlDbType.NVarChar),
					new SqlParameter("@MobilePageBlockID",SqlDbType.UniqueIdentifier),
					new SqlParameter("@ColumnDesc",SqlDbType.NVarChar),
					new SqlParameter("@ColumnDescEn",SqlDbType.NVarChar),
					new SqlParameter("@ColumnName",SqlDbType.NVarChar),
					new SqlParameter("@LinkageItem",SqlDbType.NVarChar),
					new SqlParameter("@CorrelationValue",SqlDbType.NVarChar),
					new SqlParameter("@ExampleValue",SqlDbType.NVarChar),
					new SqlParameter("@ControlType",SqlDbType.Int),
					new SqlParameter("@AuthType",SqlDbType.Int),
					new SqlParameter("@MinLength",SqlDbType.Int),
					new SqlParameter("@MaxLength",SqlDbType.Int),
					new SqlParameter("@MinSelected",SqlDbType.Int),
					new SqlParameter("@MaxSelected",SqlDbType.Int),
					new SqlParameter("@IsMustDo",SqlDbType.Int),
					new SqlParameter("@EditOrder",SqlDbType.Int),
					new SqlParameter("@ViewOrder",SqlDbType.Int),
					new SqlParameter("@TypeID",SqlDbType.Int),
					new SqlParameter("@CustomerID",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateBy",SqlDbType.NVarChar),
					new SqlParameter("@LastUpdateTime",SqlDbType.DateTime),
					new SqlParameter("@AttributeTypeID",SqlDbType.Int),
					new SqlParameter("@IsTemplate",SqlDbType.Int),
					new SqlParameter("@MobileBussinessDefinedID",SqlDbType.UniqueIdentifier)
            };
			parameters[0].Value = pEntity.TableName;
			parameters[1].Value = pEntity.MobilePageBlockID;
			parameters[2].Value = pEntity.ColumnDesc;
			parameters[3].Value = pEntity.ColumnDescEn;
			parameters[4].Value = pEntity.ColumnName;
			parameters[5].Value = pEntity.LinkageItem;
			parameters[6].Value = pEntity.CorrelationValue;
			parameters[7].Value = pEntity.ExampleValue;
			parameters[8].Value = pEntity.ControlType;
			parameters[9].Value = pEntity.AuthType;
			parameters[10].Value = pEntity.MinLength;
			parameters[11].Value = pEntity.MaxLength;
			parameters[12].Value = pEntity.MinSelected;
			parameters[13].Value = pEntity.MaxSelected;
			parameters[14].Value = pEntity.IsMustDo;
			parameters[15].Value = pEntity.EditOrder;
			parameters[16].Value = pEntity.ViewOrder;
			parameters[17].Value = pEntity.TypeID;
			parameters[18].Value = pEntity.CustomerID;
			parameters[19].Value = pEntity.LastUpdateBy;
			parameters[20].Value = pEntity.LastUpdateTime;
			parameters[21].Value = pEntity.AttributeTypeID;
			parameters[22].Value = pEntity.IsTemplate;
			parameters[23].Value = pEntity.MobileBussinessDefinedID;

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
        public void Update(MobileBussinessDefinedEntity pEntity )
        {
            Update(pEntity ,true);
        }
        public void Update(MobileBussinessDefinedEntity pEntity ,bool pIsUpdateNullField )
        {
            this.Update(pEntity, pIsUpdateNullField, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity"></param>
        public void Delete(MobileBussinessDefinedEntity pEntity)
        {
            this.Delete(pEntity, null);
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        /// <param name="pTran">����ʵ��,��Ϊnull,���Ϊnull,��ʹ������������</param>
        public void Delete(MobileBussinessDefinedEntity pEntity, IDbTransaction pTran)
        {
            //����У��
            if (pEntity == null)
                throw new ArgumentNullException("pEntity");
            if (pEntity.MobileBussinessDefinedID==null)
            {
                throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
            }
            //ִ�� 
            this.Delete(pEntity.MobileBussinessDefinedID, pTran);           
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
            sql.AppendLine("update [MobileBussinessDefined] set LastUpdateTime=@LastUpdateTime,LastUpdateBy=@LastUpdateBy,IsDelete=1 where MobileBussinessDefinedID=@MobileBussinessDefinedID;");
            SqlParameter[] parameters = new SqlParameter[] 
            { 
                new SqlParameter{ParameterName="@LastUpdateTime",SqlDbType=SqlDbType.DateTime,Value=DateTime.Now},
                new SqlParameter{ParameterName="@LastUpdateBy",SqlDbType=SqlDbType.VarChar,Value=Convert.ToString(CurrentUserInfo.UserID)},
                new SqlParameter{ParameterName="@MobileBussinessDefinedID",SqlDbType=SqlDbType.UniqueIdentifier,Value=pID}
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
        public void Delete(MobileBussinessDefinedEntity[] pEntities, IDbTransaction pTran)
        {
            //��������ֵ
            object[] entityIDs = new object[pEntities.Length];
            for (int i = 0; i < pEntities.Length; i++)
            {
                var item = pEntities[i];
                //����У��
                if (item == null)
                    throw new ArgumentNullException("pEntity");
                if (item.MobileBussinessDefinedID==null)
                {
                    throw new ArgumentException("ִ��ɾ��ʱ,ʵ�����������ֵ����Ϊnull.");
                }
                entityIDs[i] = item.MobileBussinessDefinedID;
            }
            Delete(entityIDs, pTran);
        }

        /// <summary>
        /// ����ɾ��
        /// </summary>
        /// <param name="pEntities">ʵ��ʵ������</param>
        public void Delete(MobileBussinessDefinedEntity[] pEntities)
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
            sql.AppendLine("update [MobileBussinessDefined] set LastUpdateTime='"+DateTime.Now.ToString()+"',LastUpdateBy='"+CurrentUserInfo.UserID+"',IsDelete=1 where MobileBussinessDefinedID in (" + primaryKeys.ToString().Substring(0, primaryKeys.ToString().Length - 1) + ");");
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
        public MobileBussinessDefinedEntity[] Query(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [MobileBussinessDefined] where isdelete=0 ");
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
            List<MobileBussinessDefinedEntity> list = new List<MobileBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileBussinessDefinedEntity m;
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
        public PagedQueryResult<MobileBussinessDefinedEntity> PagedQuery(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
                pagedSql.AppendFormat(" [MobileBussinessDefinedID] desc"); //Ĭ��Ϊ����ֵ����
            }
            pagedSql.AppendFormat(") as ___rn,* from [MobileBussinessDefined] where isdelete=0 ");
            //�ܼ�¼��SQL
            totalCountSql.AppendFormat("select count(1) from [MobileBussinessDefined] where isdelete=0 ");
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
            PagedQueryResult<MobileBussinessDefinedEntity> result = new PagedQueryResult<MobileBussinessDefinedEntity>();
            List<MobileBussinessDefinedEntity> list = new List<MobileBussinessDefinedEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(pagedSql.ToString()))
            {
                while (rdr.Read())
                {
                    MobileBussinessDefinedEntity m;
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
        public MobileBussinessDefinedEntity[] QueryByEntity(MobileBussinessDefinedEntity pQueryEntity, OrderBy[] pOrderBys)
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
        public PagedQueryResult<MobileBussinessDefinedEntity> PagedQueryByEntity(MobileBussinessDefinedEntity pQueryEntity, OrderBy[] pOrderBys, int pPageSize, int pCurrentPageIndex)
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
        protected IWhereCondition[] GetWhereConditionByEntity(MobileBussinessDefinedEntity pQueryEntity)
        { 
            //��ȡ�ǿ���������
            List<EqualsCondition> lstWhereCondition = new List<EqualsCondition>();
            if (pQueryEntity.MobileBussinessDefinedID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MobileBussinessDefinedID", Value = pQueryEntity.MobileBussinessDefinedID });
            if (pQueryEntity.TableName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TableName", Value = pQueryEntity.TableName });
            if (pQueryEntity.MobilePageBlockID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MobilePageBlockID", Value = pQueryEntity.MobilePageBlockID });
            if (pQueryEntity.ColumnDesc!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnDesc", Value = pQueryEntity.ColumnDesc });
            if (pQueryEntity.ColumnDescEn!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnDescEn", Value = pQueryEntity.ColumnDescEn });
            if (pQueryEntity.ColumnName!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ColumnName", Value = pQueryEntity.ColumnName });
            if (pQueryEntity.LinkageItem!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "LinkageItem", Value = pQueryEntity.LinkageItem });
            if (pQueryEntity.CorrelationValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "CorrelationValue", Value = pQueryEntity.CorrelationValue });
            if (pQueryEntity.ExampleValue!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ExampleValue", Value = pQueryEntity.ExampleValue });
            if (pQueryEntity.ControlType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ControlType", Value = pQueryEntity.ControlType });
            if (pQueryEntity.AuthType!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AuthType", Value = pQueryEntity.AuthType });
            if (pQueryEntity.MinLength!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinLength", Value = pQueryEntity.MinLength });
            if (pQueryEntity.MaxLength!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxLength", Value = pQueryEntity.MaxLength });
            if (pQueryEntity.MinSelected!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MinSelected", Value = pQueryEntity.MinSelected });
            if (pQueryEntity.MaxSelected!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "MaxSelected", Value = pQueryEntity.MaxSelected });
            if (pQueryEntity.IsMustDo!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsMustDo", Value = pQueryEntity.IsMustDo });
            if (pQueryEntity.EditOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "EditOrder", Value = pQueryEntity.EditOrder });
            if (pQueryEntity.ViewOrder!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "ViewOrder", Value = pQueryEntity.ViewOrder });
            if (pQueryEntity.TypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "TypeID", Value = pQueryEntity.TypeID });
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
            if (pQueryEntity.AttributeTypeID!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "AttributeTypeID", Value = pQueryEntity.AttributeTypeID });
            if (pQueryEntity.IsTemplate!=null)
                lstWhereCondition.Add(new EqualsCondition() { FieldName = "IsTemplate", Value = pQueryEntity.IsTemplate });

            return lstWhereCondition.ToArray();
        }
        /// <summary>
        /// װ��ʵ��
        /// </summary>
        /// <param name="pReader">��ǰֻ����</param>
        /// <param name="pInstance">ʵ��ʵ��</param>
        protected void Load(SqlDataReader pReader, out MobileBussinessDefinedEntity pInstance)
        {
            //�����е����ݴ�SqlDataReader�ж�ȡ��Entity��
            pInstance = new MobileBussinessDefinedEntity();
            pInstance.PersistenceHandle = new PersistenceHandle();
            pInstance.PersistenceHandle.Load();

			if (pReader["MobileBussinessDefinedID"] != DBNull.Value)
			{
				pInstance.MobileBussinessDefinedID =  (Guid)pReader["MobileBussinessDefinedID"];
			}
			if (pReader["TableName"] != DBNull.Value)
			{
				pInstance.TableName =  Convert.ToString(pReader["TableName"]);
			}
			if (pReader["MobilePageBlockID"] != DBNull.Value)
			{
				pInstance.MobilePageBlockID =  (Guid)pReader["MobilePageBlockID"];
			}
			if (pReader["ColumnDesc"] != DBNull.Value)
			{
				pInstance.ColumnDesc =  Convert.ToString(pReader["ColumnDesc"]);
			}
			if (pReader["ColumnDescEn"] != DBNull.Value)
			{
				pInstance.ColumnDescEn =  Convert.ToString(pReader["ColumnDescEn"]);
			}
			if (pReader["ColumnName"] != DBNull.Value)
			{
				pInstance.ColumnName =  Convert.ToString(pReader["ColumnName"]);
			}
			if (pReader["LinkageItem"] != DBNull.Value)
			{
				pInstance.LinkageItem =  Convert.ToString(pReader["LinkageItem"]);
			}
			if (pReader["CorrelationValue"] != DBNull.Value)
			{
				pInstance.CorrelationValue =  Convert.ToString(pReader["CorrelationValue"]);
			}
			if (pReader["ExampleValue"] != DBNull.Value)
			{
				pInstance.ExampleValue =  Convert.ToString(pReader["ExampleValue"]);
			}
			if (pReader["ControlType"] != DBNull.Value)
			{
				pInstance.ControlType =   Convert.ToInt32(pReader["ControlType"]);
			}
			if (pReader["AuthType"] != DBNull.Value)
			{
				pInstance.AuthType =   Convert.ToInt32(pReader["AuthType"]);
			}
			if (pReader["MinLength"] != DBNull.Value)
			{
				pInstance.MinLength =   Convert.ToInt32(pReader["MinLength"]);
			}
			if (pReader["MaxLength"] != DBNull.Value)
			{
				pInstance.MaxLength =   Convert.ToInt32(pReader["MaxLength"]);
			}
			if (pReader["MinSelected"] != DBNull.Value)
			{
				pInstance.MinSelected =   Convert.ToInt32(pReader["MinSelected"]);
			}
			if (pReader["MaxSelected"] != DBNull.Value)
			{
				pInstance.MaxSelected =   Convert.ToInt32(pReader["MaxSelected"]);
			}
			if (pReader["IsMustDo"] != DBNull.Value)
			{
				pInstance.IsMustDo =   Convert.ToInt32(pReader["IsMustDo"]);
			}
			if (pReader["EditOrder"] != DBNull.Value)
			{
				pInstance.EditOrder =   Convert.ToInt32(pReader["EditOrder"]);
			}
			if (pReader["ViewOrder"] != DBNull.Value)
			{
				pInstance.ViewOrder =   Convert.ToInt32(pReader["ViewOrder"]);
			}
			if (pReader["TypeID"] != DBNull.Value)
			{
				pInstance.TypeID =   Convert.ToInt32(pReader["TypeID"]);
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
			if (pReader["AttributeTypeID"] != DBNull.Value)
			{
				pInstance.AttributeTypeID =   Convert.ToInt32(pReader["AttributeTypeID"]);
			}
			if (pReader["IsTemplate"] != DBNull.Value)
			{
				pInstance.IsTemplate =   Convert.ToInt32(pReader["IsTemplate"]);
			}

        }
        #endregion
    }
}
