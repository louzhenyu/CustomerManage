/*
 * Author		:陆荣平
 * EMail		:lurp@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/26 14:08:53
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
using JIT.CPOS.BS.DataAccess.Base;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 表VipCliearDAO的数据访问对象 
    /// </summary>
    public class VipCliearDAO : BaseCPOSDAO
    {
        public VipCliearDAO(LoggingSessionInfo pUserInfo)
            : base(pUserInfo)
        {
        }

        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="pClear"></param>
        /// <param name="pUpdateVipID"></param>
        /// <param name="pDuplicateGroup"></param>
        /// <param name="pTableList"></param>
        public void UpdateDuplicateClear(List<VipDuplicateClearEntity> pClear, string pUpdateVipID, string pDuplicateGroup, List<string> pTableList)
        {
            StringBuilder Sql = new StringBuilder();
            if (pClear.Count > 0)
            {
                //修改VIP数据
                Sql.AppendLine("update a  set ");
                for (int i = 0; i < pClear.Count; i++)
                {
                    Sql.AppendLine(string.Format(" a.{0}=(select top 1 {0} from Vip where VIPID={1})", pClear[i].VIPFieldName, pClear[i].VipID));
                    Sql.Append(",");

                }
                Sql.AppendLine(string.Format("a.DuplicateGroup='{0}'", pDuplicateGroup));
                Sql.AppendLine(string.Format("from Vip a where VipID={0}", pUpdateVipID));
                //修改从表数据
                for (int i = 0; i < pTableList.Count; i++)
                {
                    for (int j = 0; j < pClear.Count; j++)
                    {
                        Sql.AppendLine(string.Format("Update a set a.VIPID='{0}' from  {1} a where a.VipID='{2}'", pUpdateVipID, pTableList[i], pClear[j].VipID));
                    }
                }
                //删除需要改的数据
                for (int i = 0; i < pClear.Count; i++)
                {
                    ///删除被清洗过的数据
                    Sql.AppendLine(string.Format("update a set a.isDelete=1 from Vip a where a.VipID='{0}' and a.VipID!='{1}' and a.isDelete=0", pClear[i].VipID, pUpdateVipID));
                    ///更新清洗表
                    Sql.AppendLine(string.Format("Update a set a.isClean=1,ClearBy='{1}',ClearTime=getdate()  from VIPClearList a where a.VIPClearListID='{1}' and isClean=0", pClear[i].VIPClearListID, this.CurrentUserInfo.UserID));
                }


            }


        }
        /// <summary>
        /// 不合并处理
        /// </summary>
        /// <param name="pClear"></param>
        public void UpdateNoClear(List<VipDuplicateClearEntity> pClear)
        {
            StringBuilder Sql = new StringBuilder();
            for (int i = 0; i < pClear.Count; i++)
            {
                Sql.AppendLine(string.Format("Update a set a.isClean=1,ClearBy='{1}',ClearTime=getdate()  from VIPClearList a where a.VIPClearListID='{1}' and isClean=0", pClear[i].VIPClearListID, this.CurrentUserInfo.UserID));
                Sql.AppendLine(string.Format("update a set a.DuplicateGroup='{0}' from Vip a where a.VipID='{1}'  and a.isDelete=0", pClear[i].DuplicateGroup, pClear[i].VipID));
            }


        }

        #region GetVipCliearList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pStartDate"></param>
        /// <param name="pEndDate"></param>
        /// <param name="pPageSize"></param>
        /// <param name="pPageIndex"></param>
        /// <param name="pRowCount"></param>
        /// <returns></returns>
        public PagedQueryResult<VipClearEntity> GetVipCliearList(Dictionary<string, string> pParems, int pPageSize, int pPageIndex, out int pRowCount)
        {
            StringBuilder Sql = new StringBuilder();
            StringBuilder whereSql = new StringBuilder();

            if (pParems.ContainsKey("pStartDate"))
            {
                whereSql.AppendFormat(" and datediff(day,a.CreateTime,'{0}') <=0", Convert.ToDateTime(pParems["pStartDate"]).ToString("yyyy-MM-dd"));
            }
            if (pParems.ContainsKey("pEndDate"))
            {
                whereSql.AppendFormat(" and datediff(day,a.CreateTime,'{0}') >=0", Convert.ToDateTime(pParems["pEndDate"]).ToString("yyyy-MM-dd"));
            }

            Sql.AppendLine(@"
            select 
	            COUNT(1) RowsCount
            from VIPClearInfo a
            where a.isDelete=0 ");
            Sql.Append(whereSql);
            Sql.AppendLine(string.Format(@"      
            select 
                *
            from 
            (
                select 
                    ROW_NUMBER() over( order by T.Createtime) No,VIPClearID,CreateTime,InvalidNum,DuplicateNum,DrawbackNum
                from 
                (
                    select 
                        a.VIPClearID,
                        a.Createtime,
                        COUNT(distinct case when ClearRules=1 then  b.VIPID end ) InvalidNum,
                        COUNT(distinct case when ClearRules=2 then  b.VIPID end ) DuplicateNum,
                        COUNT(distinct case when ClearRules=3 then  b.VIPID end ) DrawbackNum
                    from VIPClearInfo a
                    left join VIPClearList b on a.VIPClearID=b.VIPClearID and b.IsDelete=0
                    where a.IsDelete=0  {0}
                    group by a.VIPClearID,a.Createtime
                ) T
            ) T_No
            WHERE  No between {1} and {2}", whereSql.ToString(), pPageSize, pPageIndex));

            DataSet dt = this.SQLHelper.ExecuteDataset(Sql.ToString());
            pRowCount = Convert.ToInt32(dt.Tables[0].Rows[0][0]);

            //返回值
            PagedQueryResult<VipClearEntity> pEntity = new PagedQueryResult<VipClearEntity>();
            pEntity.RowCount = pRowCount;
            if (dt.Tables[1] != null && dt.Tables[1].Rows.Count > 0)
            {
                pEntity.Entities = DataLoader.LoadFrom<VipClearEntity>(dt.Tables[1]);
            }
            return pEntity;
        }
        #endregion
    }
}
