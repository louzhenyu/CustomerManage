/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/18 16:43:12
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
    /// ��LNumberTypeMapping�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LNumberTypeMappingDAO : Base.BaseCPOSDAO, ICRUDable<LNumberTypeMappingEntity>, IQueryable<LNumberTypeMappingEntity>
    {
        /// <summary>
        /// ��ȡ�����Ϣ
        /// </summary>
        /// <param name="numberId">����ID</param>
        /// <returns>return DataSet ���ݼ�</returns>
        public DataSet GetTypeInfoList(string numberId)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            //TypeCount����NewsMicro�������� by yehua
            sbSQL.AppendFormat("select MappingId, NumberId, TypeId,MicroTypeName TypeName,(select COUNT(1) from LNewsMicroMapping LM where LM.CustomerId='{0}' and LM.MicroNumberId = '{1}' and LM.MicroTypeId = T.MicroTypeID) AS TypeCount,M.CreateTime from dbo.LNumberTypeMapping M ", CurrentUserInfo.ClientID, numberId);
            sbSQL.Append("inner join EclubMicroType T on T.IsDelete=0 and T.CustomerId=M.CustomerId and T.MicroTypeID=M.TypeId ");
            sbSQL.AppendFormat("where T.IsDelete = 0 and T.CustomerId = '{0}' and  NumberId = '{1}';", CurrentUserInfo.ClientID, numberId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
        /// <summary>
        /// �������������������б�
        /// </summary>
        /// <param name="numberId">����ID</param>
        /// <param name="typeIds">����ID�б�</param>
        public void SetNumberTypeMapping(string numberId, string[] typeIds)
        {
            using (var trans = this.SQLHelper.CreateTransaction())
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    //ѭ�����ݼ�
                    for (int i = 0; i < typeIds.Length; i++)
                    {
                        /*����ʵ�崴�� mod by yehua
                        //��������������
                        LNumberTypeMappingEntity map = new LNumberTypeMappingEntity() { MappingId = Guid.NewGuid(), NumberId = numberId, TypeId = typeIds[i], CustomerId = CurrentUserInfo.ClientID, TypeCount = 0 };

                        //����������Ϣ
                        Create(map, trans);
                        */

                        //��ʼ���̶��ֶ�
                        string userId = CurrentUserInfo.UserID;
                        string mappingId = Guid.NewGuid().ToString();
                        string cusId = CurrentUserInfo.ClientID;

                        //����Ƿ��Ѿ����� add by yehua
                        strSql.AppendFormat("if not exists (select top 1 1 from LNumberTypeMapping where NumberId = '{0}' and TypeId = '{1}' and CustomerId = '{2}') ", numberId, typeIds[i], cusId);
                        strSql.Append("insert into [LNumberTypeMapping](");
                        strSql.Append("[NumberId],[TypeId],[TypeCount],[Style],[Sequence],[CustomerId],[CreateBy],[CreateTime],[LastUpdateBy],[LastUpdateTime],[IsDelete],[MappingId]) ");
                        strSql.Append("select ");
                        strSql.AppendFormat("'{0}','{1}',0,Style,Sequence,'{2}','{3}',GETDATE(),'{3}',GETDATE(),0,'{4}' ", numberId, typeIds[i], cusId, userId, mappingId);
                        strSql.AppendFormat("from EclubMicroType where MicroTypeID = '{0}';", typeIds[i]);
                    }

                    this.SQLHelper.ExecuteNonQuery((SqlTransaction)trans, CommandType.Text, strSql.ToString());

                    //Commit
                    trans.Commit();
                }
                catch
                {
                    //Rollback
                    trans.Rollback();
                    throw;
                }

                //ɾ���Ѿ�ȡ������������
                DeleteMapping(CurrentUserInfo.ClientID, numberId, typeIds);
            }
        }


        /// <summary>
        /// ɾ��ȡ������������ add by yehua
        /// </summary>
        public void DeleteMapping(string customerId, string numberId, string[] typeIds)
        {
            string ids = typeIds.ToJoinString(',', '\'');
            string sql = string.Format("delete from LNumberTypeMapping where CustomerId = '{0}' and TypeId not in ({1}) and NumberId = '{2}'", customerId, ids, numberId);
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// ��ȡ������������Ҫ��Ϣ
        /// </summary>
        /// <param name="numberId">�ڿ�Id</param>
        /// <returns></returns>
        public DataSet GetNumberTypeSum(string numberId)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            //TypeCount����NewsMicro�������� by yehua
            sbSQL.AppendFormat("select MicroTypeName TypeName,TypeId,(select COUNT(1) from LNewsMicroMapping LM where LM.CustomerId='{0}' and LM.MicroNumberId = '{1}' and LM.MicroTypeId = MT.MicroTypeID) AS TypeCount from EclubMicroType MT ", CurrentUserInfo.ClientID, numberId);
            sbSQL.Append("inner join LNumberTypeMapping Map on Map.IsDelete=0 and Map.CustomerId=MT.CustomerId and Map.TypeId = MT.MicroTypeID ");
            sbSQL.AppendFormat("where MT.IsDelete=0 and MT.CustomerId='{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and NumberId = '{0}' ", numberId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
    }
}
