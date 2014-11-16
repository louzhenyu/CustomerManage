using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.CPOS.BS.Entity;


namespace JIT.CPOS.BS.BLL
{
    public  class StoreDefindModuleBLL : DefindModuleCRUD
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public StoreDefindModuleBLL(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        { 

        }
        #endregion
        #region 获取终端数据部份
        #region 终端管理不分页数据
        /// <summary>
        /// 不分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <returns>终端数据</returns>
        public DataTable GetStoreData( List<DefindControlEntity> pSearch)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(GetTempSql());//需要生成的临时表
            sql.AppendLine("select ");
            sql.Append(GetStoreGridFildSQL()); //获取字SQL
            sql.AppendLine("main.StoreID ");
            sql.AppendLine("from Store main");
            sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetStoreSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}' ", _pUserInfo.ClientID));
            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(" order by main.CreateTime desc  ");
            sql.Append(GetDropTempSql()); //需要删除的临时表
             return GetData(sql.ToString());
        }
        #endregion
        #region 检查重复数据
        public string GetRepeatRowCount(List<DefindControlEntity> pSearch, string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
           // sql.Append(GetTempSql(pKeyValue));//需要生成的临时表
            sql.AppendLine("select ");
            sql.AppendLine("Count(1)  RepeatRowCount");
            sql.AppendLine("from Store main");
           // sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
       
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
           // sql.Append(GetPubRepeatSearchSQL(pSearch)); //获取条件联接SQL
            if (!string.IsNullOrEmpty(pKeyValue))
            {
                sql.AppendLine(string.Format(" and main.StoreID!='{0}'", pKeyValue));
            }
            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
          //  sql.Append(GetDropTempSql()); //需要删除的临时表
            DataTable dt= base.GetData(sql.ToString());
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }
         
        }

        #endregion
        #region 终端管理分页数据
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetStorePageData(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(GetTempSql());//需要生成的临时表
            sql.AppendLine("select ");
            sql.Append(GetStoreGridFildSQL()); //获取字SQL
            sql.AppendLine("ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.StoreID into #outTemp");
            sql.AppendLine("from Store main");
            sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetStoreSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
          //  if(base._pUserInfo.)
            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            sql.Append(GetDropTempSql()); //需要删除的临时表
            return base.GetPageData(sql.ToString());
        
        }
        #endregion
        #region 终端管理编辑页面获取的数据
        /// <summary>
        /// 获取编辑页面的值
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetStoreEditData(string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(GetTempSql(pKeyValue));//需要生成的临时表
            sql.AppendLine("select ");
            sql.Append(GetStoreEditFildSQL()); //获取字SQL
            sql.AppendLine("main.StoreID ");
            sql.AppendLine("from Store main");
            sql.Append(GetStoreLeftEditViewJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.AppendLine(string.Format("Where main.IsDelete=0 and  main.ClientID='{0}' and CONVERT(varchar(100),main.StoreID) in (select value from dbo.fnSplitStr('{1}',','))", base._pUserInfo.ClientID, pKeyValue));
            sql.Append(GetDropTempSql()); //需要删除的临时表
            DataTable db= base.GetData(sql.ToString());
            List<DefindControlEntity> l = base.GetEditControls();
            for (var i = 0; i < l.Count; i++)
            {
                if (db.Rows.Count > 0)
                {
                    if (db.Rows[0][l[i].ControlName]!=DBNull.Value)
                         l[i].ControlValue = db.Rows[0][l[i].ControlName].ToString();
                
                }
            
            }

             return l;
        
        }
        #endregion
        #endregion
        #region 拼SQL
        #region 查询需要的字段
        #region 终端列表公共查询的字段
        /// <summary>
        /// 返回终端需要特殊处理的字段SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStoreGridFildSQL() {
            var pColumnDefind = base.GetGridColumns();
              StringBuilder fieldSQL = base.GetPubGridFildSQL();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 17: //部门
                        fieldSQL.AppendLine(string.Format("T_{0}.StructureName {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 20: //渠道
                        fieldSQL.AppendLine(string.Format("T_{0}.ChannelName {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 21: //连锁
                        fieldSQL.AppendLine(string.Format("T_{0}.ChainName {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 23: //职位点选用户
                        fieldSQL.AppendLine(string.Format("T_{0}.Text {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 25: //点选经销商
                        fieldSQL.AppendLine(string.Format("T_{0}.Distributor {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;

                }

            }

            return fieldSQL;
        }
        #endregion
        #region 终端管理编辑页获取数据字段SQL
        /// <summary>
        /// 编辑页面获取数据SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStoreEditFildSQL() 
        {

            var pColumnDefind = base.GetGridColumns();
            StringBuilder fieldSQL =new  StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                  
                    case 23: //职位点选用户
                        fieldSQL.AppendLine(string.Format("T_{0}.Value {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 25: //职位点选用户
                        fieldSQL.AppendLine(string.Format("T_{0}.DistributorID {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    default:
                         fieldSQL.AppendLine(string.Format("main.{0} {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;

                }

            }
            return fieldSQL;
        }
        #endregion
        #endregion
        #region 左连接语句 left join
        #region 终端管理及列表公共左联接的语句
        /// <summary>
        /// 返回终端需要联接的SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStoreLeftGridJoinSQL()
        {

            StringBuilder leftJoinSql = base.GetPubLeftGridJoinSQL();
             var pColumnDefind = base.GetGridColumns();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                     case 17: //部门
                        leftJoinSql.AppendLine(string.Format("left join ClientStructure T_{0} on  T_{0}.ClientStructureID=main.ClientStructureID", ColumnName));
                         break;
                    case 20: //渠道
                        leftJoinSql.AppendLine(string.Format("left join Channel T_{0} on  T_{0}.ChannelID=main.ChannelID", ColumnName));
                         break;
                    case 21: //连锁
                        leftJoinSql.AppendLine(string.Format("left join Chain T_{0} on  T_{0}.ChainID=main.ChainID", ColumnName));
                        break;
                    case 23: //职位点选用户
                        leftJoinSql.AppendLine(string.Format("left join #{0} T_{0} on  T_{0}.StoreID=main.StoreID", ColumnName));
                        break;
                    case 25: //点选经销商
                        leftJoinSql.AppendLine(string.Format("left join ClientStoreDistributorMapping mapping_{0} on  mapping_{0}.StoreID=main.StoreID and mapping_{0}.IsDelete=0 ", ColumnName));
                        leftJoinSql.AppendLine(string.Format("left join Distributor T_{0} on  T_{0}.DistributorID=mapping_{0}.DistributorID ", ColumnName));
                         break;

                }

            }
            return leftJoinSql;
        
        }
        #endregion
        #region 用于终端管理编辑页面取数据左联接句
        /// <summary>
        /// 编辑页的连接Join
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStoreLeftEditViewJoinSQL()
        {

            StringBuilder leftJoinSql = new StringBuilder();
            var pColumnDefind = base.GetGridColumns();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                   
                    case 23: //职位点选用户
                        leftJoinSql.AppendLine(string.Format("left join #{0} T_{0} on  T_{0}.StoreID=main.StoreID", ColumnName));
                        break;
                    case 25: //点选经销商
                        leftJoinSql.AppendLine(string.Format("left join ClientStoreDistributorMapping T_{0} on  T_{0}.StoreID=main.StoreID and T_{0}.IsDelete=0 ", ColumnName));
                      //leftJoinSql.AppendLine(string.Format("left join Distributor T_{0} on  T_{0}.DistributorID=mapping_{0}.DistributorID ", ColumnName));
                        break;
                  

                }

            }
            return leftJoinSql;

        }
        #endregion
        #endregion
        #region 条件语句 包括inner join 和 where 后条件语句
        #region 用于终端查询一些公共的条件语句
        /// <summary>
        /// 返回终端要处理的条件语句
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStroeGridSearchSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder searchSQL = base.GetPubGridSearchSQL(pSearch);
            searchSQL.AppendLine(string.Format("and main.ClientDistributorID='{0}'", _pUserInfo.ClientDistributorID));
            return searchSQL;
        }
        #endregion
        #region 用于终端查询时，递归查询的条件语句，这些数据一般生成表于主表inner join 
        /// <summary>
        /// 返回终端特殊查询连接SQL
        /// </summary>
        /// <param name="pSearch"></param>
        /// <returns></returns>
        public StringBuilder GetStoreSearchJoinSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder HierarchyJoinSQL = base.GetPubSearchJoinSQL(pSearch);

            for (int i = 0; i < pSearch.Count; i++)
            {
                string ControlName = pSearch[i].ControlName;
                string ControlValue = pSearch[i].ControlValue;
                switch (pSearch[i].ControlType)
                {
                    case 17: //部门
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join dbo.fnGetStructure('{0}',1) fn_{1} on  CONVERT(nvarchar(100),main.{1}) = fn_{1}.StructureID", ControlValue, ControlName));
                        break;
                    case 20: //渠道
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join dbo.fnGetChannel('{0}',1) fn_{1} on  CONVERT(nvarchar(100),main.{1}) = fn_{1}.ChannelID", ControlValue, ControlName));
                        break;
                    case 21: //连锁
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join dbo.fnGetChain('{0}',1) fn_{1} on   CONVERT(nvarchar(100),main.{1}) = fn_{1}.ChainID", ControlValue, ControlName));
                        break;
                    case 23: //职位点选用户
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join (select distinct StoreID from dbo.fnGetStoreByUserID('{0}',1)) fn_{1} on   CONVERT(nvarchar(100),main.StoreID) = CONVERT(nvarchar(100),fn_{1}.StoreID)", ControlValue, ControlName));
                        break;
                    case 25: //点选经销商
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join dbo.fnSplitStr('{0}',',') fn_{1} on   mapping_{1}.DistributorID = fn_{1}.value", ControlValue, ControlName));
                        break;

                }

            }
            //TODO:Visiting加上权限
            //if (_pUserInfo.StructureLevel!=1)
            //     HierarchyJoinSQL.AppendLine(string.Format("inner join dbo.fnGetStoreByUserID({0},1) qx_store on CONVERT(nvarchar(100),main.StoreID)=qx_store.StoreID ",_pUserInfo.UserID));
            return HierarchyJoinSQL;
        
        }
        #endregion
        #endregion
        #region 需要生成的临时表
        /// <summary>
        /// 编辑的时候用，主要是MAPPING的数据，需要,分割
        /// </summary>
        /// <param name="pKeyValue"></param>
        /// <returns></returns>
        private StringBuilder GetTempSql(string pKeyValue)
        {
            var pColumnDefind = base.GetGridColumns();
            StringBuilder tempSql = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);

                switch (cType)
                {
                    case 23: //职位点选用户
                        string CorrelationValue = Convert.ToString(pColumnDefind[i].CorrelationValue);
                        tempSql.AppendLine(string.Format(
@" select 
t.StoreID,
Value= stuff((
                select ','+ CONVERT(varchar(50), b.ClientUserID) 
        from ClientStoreUserMapping a
        inner join ClientUser b on a.ClientUserID=b.ClientUserID 
        where a.StoreID=t.StoreID and b.ClientPositionID={0} and a.IsDelete=0 for xml path('')),1,1,'')
into #{1}
from ClientStoreUserMapping t
inner join ClientUser t1 on t.ClientUserID=t1.ClientUserID
where T.ClientID='{2}' and t.IsDelete=0 and t1.ClientPositionID={0} and T.ClientDistributorID={3} and T.StoreID='{4}'
group by T.StoreID", CorrelationValue, ColumnName, _pUserInfo.ClientID, _pUserInfo.ClientDistributorID, pKeyValue));
                        break;


                }

            }
            return tempSql;
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetTempSql()
        {
              var pColumnDefind = base.GetGridColumns();
            StringBuilder tempSql = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
 
                switch (cType)
                {
                    case 23: //职位点选用户
                       string CorrelationValue =Convert.ToString(pColumnDefind[i].CorrelationValue);
                        tempSql.AppendLine(string.Format(
@" select 
t.StoreID,
Text=stuff(( select ','+ CONVERT(varchar(50), b.Name) 
        from ClientStoreUserMapping a
        inner join ClientUser b on a.ClientUserID=b.ClientUserID 
        where a.StoreID=t.StoreID  and a.IsDelete=0  and b.ClientPositionID={0}  for xml path('')),1,1,'')
into #{1}
from ClientStoreUserMapping t
inner join ClientUser t1 on t.ClientUserID=t1.ClientUserID
where T.ClientID='{2}' and t.IsDelete=0 and t1.ClientPositionID={0} and T.ClientDistributorID={3}
group by T.StoreID", CorrelationValue, ColumnName, _pUserInfo.ClientID,_pUserInfo.ClientDistributorID));
                        break;
                  

                }

            }
             return tempSql;
        }
        #endregion 
        #region 需要删除的临时表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetDropTempSql()
        {
            var pColumnDefind = base.GetGridColumns();
            StringBuilder tempSql = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 23: //职位点选用户
                        tempSql.AppendLine(string.Format("Drop table #{0}", ColumnName));
                        break;

                }

            }
            return tempSql;
        }
        #endregion
        #endregion
        #region 增删改操作
        /// <summary>
        /// 添加终端
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Create(List<DefindControlEntity> pEditValue)
        {
            var plm = pEditValue.ToArray().Where(i => i.ControlType != 23 && i.ControlType != 25).ToList();
            StringBuilder sql = base.GetCreateSql(plm,"StoreID");
            sql.AppendLine(string.Format("update store set ClientDistributorID='{0}' where StoreID=@GUID", _pUserInfo.ClientDistributorID));
           for(int i = 0; i < pEditValue.Count; i++)
           {
               switch (pEditValue[i].ControlType) 
               {
                   case 23: //用户与终端的关联
                       if (!string.IsNullOrEmpty( pEditValue[i].ControlValue))
                       sql.AppendLine(string.Format(@"
                                                insert into ClientStoreUserMapping( ClientUserID,StoreID,ClientID,CreateBy, CreateTime, LastUpdateBy, LastUpdateTime,ClientDistributorID)
                                                select  value, @GUID,{0},{1}, GETDATE(),{1}, GETDATE(),{3}
                                                from dbo.fnSplitStr('{2}',',')", _pUserInfo.ClientID,  _pUserInfo.UserID, pEditValue[i].ControlValue,_pUserInfo.ClientDistributorID));
                       break;
                   case 17: //部门与终端的关联
                        sql.AppendLine("insert into ClientStructureStoreMapping(StoreID,ClientStructureID,ClientID,ClientDistributorID,CreateBy)");
                        sql.AppendLine(string.Format("select @GUID,'{0}',{1},{2},{3}", pEditValue[i].ControlValue, _pUserInfo.ClientID, _pUserInfo.ClientDistributorID,_pUserInfo.UserID));
                       break;
                   case 25: //终端与经销商的关联
                       sql.AppendLine(string.Format("insert into ClientStoreDistributorMapping(StoreID,DistributorID,ClientID,ClientDistributorID,CreateBy"));
                       sql.AppendLine(string.Format("select @GUID,'{0}',{1},{2},{3}", pEditValue[i].ControlValue, _pUserInfo.ClientID, _pUserInfo.ClientDistributorID, _pUserInfo.UserID));
                       break;
                   
               }
           }
            bool res=base.ICRUDable(sql.ToString());
            return res;
        }
        /// <summary>
        /// 修改终端
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Update(List<DefindControlEntity> pEditValue,string pKeyValue)
        {
            var plm = pEditValue.ToArray().Where(i => i.ControlType != 23 && i.ControlType != 25).ToList();
            var pall = base.GetEditControls();
            var p = pall.ToArray().Where(i => i.ControlType != 23 && i.ControlType != 25).ToList();
            StringBuilder sql = base.GetUpdateSql(p,plm, "StoreID", pKeyValue);
            
            //删除人员与终端的映射关系
            for (int i = 0; i < pall.Count; i++)
                {
                    switch (pall[i].ControlType)
                    {
                        case 23:
                            string CorrelationValue = pall[i].CorrelationValue;
                            sql.AppendLine(string.Format(@" update t
                                                              set t.IsDelete=1,t.LastUpdateTime=getdate(),LastUpdateBy='{3}'
                                                              from ClientStoreUserMapping t
                                                              inner join ClientUser t1 on t1.ClientUserID=t.ClientUserID 
                                                              where t.StoreID='{0}' and t1.ClientID='{1}' and t.IsDelete=0 and t1.ClientPositionID={2}
                                                                                                                    ", pKeyValue, _pUserInfo.ClientID, CorrelationValue, _pUserInfo.UserID));
                            break;
                        case 25: //终端与经销商的关联
                            sql.AppendLine(string.Format("Update ClientStoreDistributorMapping set IsDelete=1,LastUpdateBy={0},LastUpdateTime=getdate() where StoreID='{1}' and IsDelete=0", _pUserInfo.UserID, pKeyValue));
                           break;
                    }
            
                }
            //添加人员与终端的映射关系
                for (int i = 0; i < pEditValue.Count; i++)
                {
                    switch (pEditValue[i].ControlType)
                    {
                        case 23://人与终端的关系
                            string CorrelationValue = pEditValue[i].CorrelationValue;
                             sql.AppendLine(string.Format(@"
                                                insert into ClientStoreUserMapping( ClientUserID,StoreID,ClientID,CreateBy, CreateTime, LastUpdateBy, LastUpdateTime,ClientDistributorID)
                                                select  value, '{3}',{0},{1}, GETDATE(),{1}, GETDATE(),{4}
                                                from dbo.fnSplitStr('{2}',',')", _pUserInfo.ClientID, _pUserInfo.UserID, pEditValue[i].ControlValue, pKeyValue,_pUserInfo.ClientDistributorID));
                            break;
                        case 17://部门与终端的关系
                            sql.AppendLine(string.Format("Update ClientStructureStoreMapping set IsDelete=1,LastUpdateBy={0},LastUpdateTime=getdate() where StoreID='{1}' and IsDelete=0", _pUserInfo.UserID, pKeyValue));
                            sql.AppendLine("insert into ClientStructureStoreMapping(StoreID,ClientStructureID,ClientID,ClientDistributorID,CreateBy,CreateTime)");
                            sql.AppendLine(string.Format("select '{0}','{1}',{2},{3},{4},GetDate()", pKeyValue, pEditValue[i].ControlValue, _pUserInfo.ClientID, _pUserInfo.ClientDistributorID, _pUserInfo.UserID));
                            break;
                        case 25: //终端与经销商的关联
                            sql.AppendLine(string.Format("Update ClientStoreDistributorMapping set IsDelete=1,LastUpdateBy={0},LastUpdateTime=getdate() where StoreID='{1}' and IsDelete=0", _pUserInfo.UserID, pKeyValue));
                            sql.AppendLine(string.Format("insert into ClientStoreDistributorMapping(StoreID,DistributorID,ClientID,ClientDistributorID,CreateBy)"));
                            sql.AppendLine(string.Format("select '{0}','{1}',{2},{3},{4}",pKeyValue, pEditValue[i].ControlValue, _pUserInfo.ClientID, _pUserInfo.ClientDistributorID, _pUserInfo.UserID));
                            break;

                    }
                }

            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        /// <summary>
        /// 删除终端
        /// </summary>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Delete(string pKeyValue, out string resStr)
        {
            //#region 删除限制判断
            //if (new DataOperateBLL(_pUserInfo).StoreDeleteCheck(Guid.Parse(pKeyValue), out resStr) != 0)
            //{
            //    return false;
            //}
            //#endregion

            StringBuilder sql=new StringBuilder();
            sql.AppendLine(base.GetDeleteSql("StoreID", pKeyValue));
            //删除终端与人的关系
            sql.AppendLine(string.Format(@"
            update ClientStoreUserMapping
            set IsDelete=1 ,LastUpdateTime=getdate(),LastUpdateBy={1}
            where CONVERT(nvarchar(100),StoreID) in (select value from dbo.fnSplitStr('{0}',','))", pKeyValue,_pUserInfo.UserID));
            //删除终端与经销商的关系
            sql.AppendLine(string.Format("Update ClientStoreDistributorMapping set IsDelete=1,LastUpdateBy={0},LastUpdateTime=getdate() where StoreID='{1}' and IsDelete=0", _pUserInfo.UserID, pKeyValue));
            //删除终端与部门的关系
            sql.AppendLine(string.Format("Update ClientStructureStoreMapping set IsDelete=1,LastUpdateBy={0},LastUpdateTime=getdate() where StoreID='{1}' and IsDelete=0", _pUserInfo.UserID, pKeyValue));
            bool res = base.ICRUDable(sql.ToString());
            resStr = "";
            return res;
        }
        #endregion
    }

    
}

