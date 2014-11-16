/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/15 13:37:22
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
    /// ��LNewsMicroMapping�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class LNewsMicroMappingDAO : Base.BaseCPOSDAO, ICRUDable<LNewsMicroMappingEntity>, IQueryable<LNewsMicroMappingEntity>
    {
        #region ��ȡ΢����Ѷ�����б�
        /// <summary>
        /// ��ȡ΢����Ѷ�����б�
        /// </summary>
        /// <param name="microNumberId">����ID</param>
        /// <param name="microTypeId">���ID</param>
        /// <param name="sortField">�����ֶ�</param>
        /// <param name="sortOrder">����ʽ��0 ���� 1 ����</param>
        /// <param name="pageIndex">��ǰҳ</param>
        /// <param name="pageSize">ҳ��С</param>
        /// <returns></returns>
        public DataSet GetNewsMappList(string microNumberId, string microTypeId, string sortField, int sortOrder, int pageIndex, int pageSize)
        {
            string sort = "DESC";
            if (sortOrder != 0)
            {
                sort = "ASC";
            }
            if (string.IsNullOrEmpty(sortField))
            {
                sortField = "CreateTime";
            }
            //Instance Obj
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("select MappingId,NewsId,PublishTime,NewsTitle,MicroTypeName,BrowseCount,PraiseCount,CollCount,Sequence from (");
            sbSQL.AppendFormat("select ROW_NUMBER()over(order by N.{0} {1}) rowNum,MappingId,N.NewsId,PublishTime,NewsTitle,MicroTypeName,BrowseCount,PraiseCount,CollCount,NMM.Sequence from LNews N ", sortField, sort);
            sbSQL.Append("inner join LNewsMicroMapping NMM on NMM.IsDelete=0 and NMM.CustomerId=N.CustomerId and NMM.NewsId=N.NewsId ");
            sbSQL.Append("inner join EclubMicroType NT on NT.IsDelete=0 and NT.CustomerId=NMM.CustomerId and NT.MicroTypeID=NMM.MicroTypeId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and NT.CustomerId='{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and NMM.MicroNumberId='{0}' and NMM.MicroTypeId='{1}'", microNumberId, microTypeId);
            sbSQL.Append(") as Res ");
            sbSQL.AppendFormat("where rowNum between {0} and {1} ;", pageIndex * pageSize + 1, (pageIndex + 1) * pageSize);
            sbSQL.Append("select COUNT(N.NewsId) from LNews N ");
            sbSQL.Append("inner join LNewsMicroMapping NMM on NMM.IsDelete=0 and NMM.CustomerId=N.CustomerId and NMM.NewsId=N.NewsId ");
            sbSQL.Append("inner join EclubMicroType NT on NT.IsDelete=0 and NT.CustomerId=NMM.CustomerId and NT.MicroTypeID=NMM.MicroTypeId ");
            sbSQL.AppendFormat("where N.IsDelete = 0 and NT.CustomerId='{0}' ", CurrentUserInfo.ClientID);
            sbSQL.AppendFormat("and NMM.MicroNumberId='{0}' and NMM.MicroTypeId='{1}' ;", microNumberId, microTypeId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        #endregion

        #region ������Ѷ΢�������б�
        /// <summary>
        /// ������Ѷ΢�������б�
        /// </summary>
        /// <param name="numberId">����ID</param>
        /// <param name="typeId">���ID</param>
        /// <param name="newsIds">��ѶID</param>
        /// <returns>��Ӱ�������</returns>
        public int SetNewsMap(string numberId, string typeId, string[] newsIds)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            if (newsIds == null || newsIds.Length <= 0)
            {
                return 0;
            }
            //����ִ��
            foreach (var newsId in newsIds)
            {
                sbSQL.AppendFormat("IF NOT EXISTS(SELECT TOP 1 1 FROM LNewsMicroMapping where IsDelete=0 and CustomerId='{0}' and MicroNumberId='{1}' and MicroTypeId='{2}' and NewsId='{3}') ", CurrentUserInfo.ClientID, numberId, typeId, newsId);
                sbSQL.Append("INSERT INTO LNewsMicroMapping(NewsId,MicroNumberId,MicroTypeId,CustomerId,CreateBy,LastUpdateBy) ");
                sbSQL.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{4}');", newsId, numberId, typeId, CurrentUserInfo.ClientID, CurrentUserInfo.UserID);
            }
            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }

        /// <summary>
        /// ���þɵ����Ź�������
        /// �ϵĹ���ֱ��ɾ��
        /// </summary>
        public int SetOldNewsMap(string newsId, string oldNumberId, string oldTypeId, SqlTransaction trans)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();

            //����ִ��
            sbSQL.AppendFormat("delete from LNewsMicroMapping where NewsId = '{0}' and MicroNumberId = '{1}' and MicroTypeId = '{2}'", newsId, oldNumberId, oldTypeId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL.ToString());
        }

        /// <summary>
        /// ������Ѷ΢�������б��д桾û��ֱ�Ӳ��롿
        /// </summary>
        /// <param name="microMapEn">����ʵ��</param>
        /// <param name="trans">�������</param>
        /// <returns>��Ӱ�������</returns>
        public int SetNewsMap(LNewsMicroMappingEntity microMapEn, SqlTransaction trans)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            if (microMapEn == null)
            {
                return 0;
            }
            //����ִ��
            sbSQL.AppendFormat("IF NOT EXISTS(SELECT TOP 1 1 FROM LNewsMicroMapping where IsDelete=0 and CustomerId='{0}' and MicroNumberId='{1}' and MicroTypeId='{2}' and NewsId='{3}' )", CurrentUserInfo.ClientID, microMapEn.MicroNumberId, microMapEn.MicroTypeId, microMapEn.NewsId);
            sbSQL.Append("BEGIN ");
            sbSQL.Append("INSERT INTO	LNewsMicroMapping(NewsId,MicroNumberId,MicroTypeId,CustomerId,CreateBy,LastUpdateBy) ");
            sbSQL.AppendFormat("VALUES('{0}','{1}','{2}','{3}','{4}','{4}') ;", microMapEn.NewsId, microMapEn.MicroNumberId, microMapEn.MicroTypeId, microMapEn.CustomerId, CurrentUserInfo.ClientID, CurrentUserInfo.UserID);
            sbSQL.Append("END ");

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL.ToString());
        }
        #endregion

        #region ���š����ͳ�ƣ���ϵӳ���
        /// <summary>
        /// ���š����ͳ��
        /// </summary>
        /// <param name="numberId">����Id</param>
        /// <param name="typeId">���Id</param>
        /// <returns>������Ӱ�������</returns>
        public int MicroNumberTypeCollect(string numberId, string typeId)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendFormat("update LNumberTypeMapping set LastUpdateBy = '{0}',LastUpdateTime=GETDATE(),TypeCount = (", CurrentUserInfo.UserID);
            sbSQL.Append("select COUNT(*) from LNewsMicroMapping ");
            sbSQL.AppendFormat("where IsDelete=0 and CustomerId='{2}'and MicroNumberId='{0}' and MicroTypeId='{1}' ", numberId, typeId, CurrentUserInfo.ClientID);
            sbSQL.AppendFormat(") where IsDelete=0 and CustomerId='{2}' and NumberId='{0}' and TypeId='{1}' ;", numberId, typeId, CurrentUserInfo.ClientID);

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(sbSQL.ToString());
        }
        #endregion

        #region ɾ����Ѷ΢�������б�
        /// <summary>
        /// ɾ����Ѷ΢�������б�
        /// </summary>
        /// <param name="newsId">���ű�־Id</param>
        /// <param name="trans">�������</param>
        /// <returns>������Ӱ��ĺ���</returns>
        public int DelNewsMap(string newsId, SqlTransaction trans)
        {
            //Build SQL Text
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.Append("DELETE FROM LNewsMicroMapping ");
            sbSQL.AppendFormat("where NewsId='{0}' ;", newsId);

            //Execute SQL Script
            return this.SQLHelper.ExecuteNonQuery(trans, CommandType.Text, sbSQL.ToString());
        }

        /// <summary>
        /// ��������IDɾ������������
        /// </summary>
        public int DelNewsMap(string[] mappingId)
        {
            string ids = mappingId.ToJoinString(',', '\'');
            return this.SQLHelper.ExecuteNonQuery(CommandType.Text, string.Format("delete from LNewsMicroMapping where MappingId in (" + ids + ")"));
        }
        #endregion
    }
}
