using System;
using System.Text;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;

using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL.Module.BasicData;

namespace JIT.CPOS.BS.BLL.Vip
{
    public class VIPClearBLLByMB : VipBLLByNew
    {
        public VIPClearBLLByMB(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        { 

        }
         
        public DataSet GetGroupData(int pVIPClearID,int pPageSize,int pPageInex)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(
                            @"
                            declare @VIPClearID int ={0}
                            declare @PageSize int={1}
                            declare @PageIndex int={2}
                            ", pVIPClearID, pPageSize, pPageInex);
            sql.AppendLine(@"  select 
                                 *  into #DuplicateGrouptemp
                                from 
                                (
                                    select 
                                         DuplicateGroup,ROW_NUMBER() over( order by DuplicateGroup asc ) NO
                                    from 
                                    (
                                        select 
                                            distinct DuplicateGroup
                                        from VIPClearList
                                        where VIPClearID=@VIPClearID and ClearRules=2 and IsClean=0
                                    ) s
                                )
                                a
                               
                                select COUNT(1) RowsCount from #DuplicateGrouptemp
                                select * from #DuplicateGrouptemp  where No >=@PageIndex*@PageSize+1 and No<=(@PageIndex+1)*@PageSize
                            ");

                sql.AppendLine("select ");
                sql.Append(GetStoreGridFildSQL()); //获取字SQL
                sql.AppendLine("main.VIPID ,Dgp.DuplicateGroup");
                sql.AppendLine("from Vip main");
                sql.AppendLine(@" inner join (
                                    select 
	                                    a.VIPID,b.DuplicateGroup
                                    from VIPClearList a
                                    inner join #DuplicateGrouptemp b on a.DuplicateGroup=b.DuplicateGroup and No >=@PageIndex*@PageSize+1 and No<=(@PageIndex+1)*@PageSize
                                    where a.VIPClearID=@VIPClearID and a.IsClean=0 ) Dgp on Dgp.VIPID=main.VipID");
                sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
                sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
                sql.AppendLine("Drop table #DuplicateGrouptemp");
                return base.GetDataByDataSet(sql.ToString());
        }

    }
}
