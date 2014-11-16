using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;


namespace JIT.CPOS.BS.BLL
{
    public class VIPDefindModuleBLL : DefindModuleCRUD
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public VIPDefindModuleBLL(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {

        }
        #endregion

        #region 获取会员数据部份
        #region 会员管理不分页数据
        /// <summary>
        /// 不分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <returns>会员数据</returns>
        public DataTable GetVIPData(List<DefindControlEntity> pSearch)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetVIPGridFildSQL()); //获取字SQL
            sql.AppendLine("main.VIPID ");
            sql.AppendLine("from VIP main");
            sql.Append(GetVIPLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetVIPSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            return base.GetData(sql.ToString());
        }
        #endregion
        #region 检查重复数据
        public string GetRepeatRowCount(List<DefindControlEntity> pSearch, string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(GetTempSql());//需要生成的临时表
            sql.AppendLine("select ");
            sql.AppendLine("Count(1)  RepeatRowCount");
            sql.AppendLine("from VIP main");
            sql.Append(GetVIPLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetVIPSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
            if (!string.IsNullOrEmpty(pKeyValue))
            {
                sql.AppendLine(string.Format(" and main.VIPID!='{0}'", pKeyValue));
            }
            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(GetDropTempSql()); //需要删除的临时表
            DataTable dt = base.GetData(sql.ToString());
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
        #region 会员管理分页数据
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetVIPPageData(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(GetTempSql());//需要生成的临时表
            sql.AppendLine("select ");
            sql.Append(GetVIPGridFildSQL()); //获取字SQL
            sql.AppendLine("ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.VIPID into #outTemp");
            sql.AppendLine("from VIP main");
            sql.Append(GetVIPLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetVIPSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
            //  if(base._pUserInfo.)
            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            sql.Append(GetDropTempSql()); //需要删除的临时表
            return base.GetPageData(sql.ToString());

        }
        #endregion
        #region 会员管理编辑页面获取的数据
        /// <summary>
        /// 获取编辑页面的值
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetVIPEditData(string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(GetTempSql());//需要生成的临时表
            sql.AppendLine("select ");
            sql.Append(GetVIPEditFildSQL()); //获取字SQL
            sql.AppendLine("main.VIPID ");
            sql.AppendLine("from VIP main");
            sql.Append(GetVIPLeftEditViewJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.AppendLine(string.Format("Where main.IsDelete=0 and  main.ClientID='{0}' and CONVERT(varchar(100),main.VIPID) in (select value from dbo.fnSplitStr('{1}',','))", base._pUserInfo.ClientID, pKeyValue));
            sql.Append(GetDropTempSql()); //需要删除的临时表
            DataTable db = base.GetData(sql.ToString());
            List<DefindControlEntity> l = base.GetEditControls();
            for (var i = 0; i < l.Count; i++)
            {
                if (db.Rows.Count > 0)
                {
                    if (db.Rows[0][l[i].ControlName] != DBNull.Value)
                        l[i].ControlValue = db.Rows[0][l[i].ControlName].ToString();

                }

            }

            return l;

        }
        #endregion
        #endregion
        #region 拼SQL
        #region 查询需要的字段
        #region 会员列表公共查询的字段
        /// <summary>
        /// 返回会员需要特殊处理的字段SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetVIPGridFildSQL()
        {
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
        #region 会员管理编辑页获取数据字段SQL
        /// <summary>
        /// 编辑页面获取数据SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetVIPEditFildSQL()
        {

            var pColumnDefind = base.GetGridColumns();
            StringBuilder fieldSQL = new StringBuilder();
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
        #region 会员管理及列表公共左联接的语句
        /// <summary>
        /// 返回会员需要联接的SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetVIPLeftGridJoinSQL()
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
                        //fieldSQL.AppendLine(string.Format("T_{0}.{0} {0}", ColumnName));
                        leftJoinSql.AppendLine(string.Format("left join #{0} T_{0} on  T_{0}.VIPID=main.VIPID", ColumnName));
                        break;

                }

            }
            return leftJoinSql;

        }
        #endregion
        #region 用于会员管理编辑页面取数据左联接句
        /// <summary>
        /// 编辑页的连接Join
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetVIPLeftEditViewJoinSQL()
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
                        leftJoinSql.AppendLine(string.Format("left join #{0} T_{0} on  T_{0}.VIPID=main.VIPID", ColumnName));
                        break;


                }

            }
            return leftJoinSql;

        }
        #endregion
        #endregion
        #region 条件语句 包括inner join 和 where 后条件语句
        #region 用于会员查询一些公共的条件语句
        /// <summary>
        /// 返回会员要处理的条件语句
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStroeGridSearchSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder searchSQL = base.GetPubGridSearchSQL(pSearch);
            //if (_pUserInfo.ClientDistributorID != null && _pUserInfo.ClientDistributorID != "0")
            //{
            //    searchSQL.AppendLine(string.Format("and main.ClientDistributorID='{0}'", _pUserInfo.ClientDistributorID));
            //}
            return searchSQL;
        }
        #endregion
        #region 用于会员查询时，递归查询的条件语句，这些数据一般生成表于主表inner join
        /// <summary>
        /// 返回会员特殊查询连接SQL
        /// </summary>
        /// <param name="pSearch"></param>
        /// <returns></returns>
        public StringBuilder GetVIPSearchJoinSQL(List<DefindControlEntity> pSearch)
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
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join (select distinct VIPID from dbo.fnGetVIPByUserID('{0}',1)) fn_{1} on   CONVERT(nvarchar(100),main.VIPID) = CONVERT(nvarchar(100),fn_{1}.VIPID)", ControlValue, ControlName));
                        break;
                    case 25: //点选经销商
                        HierarchyJoinSQL.AppendLine(string.Format(" inner join dbo.fnSplitStr('{0}',',') fn_{1} on   CONVERT(nvarchar(100),main.{1}) = fn_{1}.value", ControlValue, ControlName));
                        break;

                }

            }
            return HierarchyJoinSQL;

        }
        #endregion
        #endregion
        #region 需要生成的临时表
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
                        string CorrelationValue = Convert.ToString(pColumnDefind[i].CorrelationValue);
                        tempSql.AppendLine(string.Format(@" select 
                                                            t.VIPID,
                                                            Value= stuff((
                                                                             select ','+ CONVERT(varchar(50), b.ClientUserID) 
                                                                        from ClientVIPUserMapping a
                                                                        inner join ClientUser b on a.ClientUserID=b.ClientUserID 
                                                                        where a.VIPID=t.VIPID and b.ClientPositionID={0} and a.IsDelete=0 for xml path('')),1,1,''),
                                                            Text=stuff(( select ','+ CONVERT(varchar(50), b.Name) 
                                                                        from ClientVIPUserMapping a
                                                                        inner join ClientUser b on a.ClientUserID=b.ClientUserID 
                                                                        where a.VIPID=t.VIPID  and a.IsDelete=0  and b.ClientPositionID={0}  for xml path('')),1,1,'')
                                                          into #{1}
                                                          from ClientVIPUserMapping t
                                                          inner join ClientUser t1 on t.ClientUserID=t1.ClientUserID
                                                          where T.ClientID='{2}' and t.IsDelete=0 and t1.ClientPositionID={0}
                                                          group by T.VIPID", CorrelationValue, ColumnName, _pUserInfo.ClientID));
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
        /// 添加会员
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Create(List<DefindControlEntity> pEditValue)
        {
            var plm = pEditValue.ToArray().Where(i => i.ControlType != 23).ToList();
            StringBuilder sql = base.GetCreateSql(plm, "VIPID");
//            if (!string.IsNullOrEmpty(_pUserInfo.ClientDistributorID) && _pUserInfo.ClientDistributorID != "0")
//                sql.AppendLine(string.Format("update store set ClientDistributorID='{0}' where VIPID=@GUID",
//                    _pUserInfo.ClientDistributorID));
//            for (int i = 0; i < pEditValue.Count; i++)
//            {
//                switch (pEditValue[i].ControlType)
//                {
//                    case 23:
//                        if (!string.IsNullOrEmpty(pEditValue[i].ControlValue))
//                            sql.AppendLine(string.Format(@"
//                                                insert into ClientVIPUserMapping( ClientUserID,VIPID,ClientID,CreateBy, CreateTime, LastUpdateBy, LastUpdateTime)
//                                                select  value, @GUID,{0},{1}, GETDATE(),{1}, GETDATE()
//                                                from dbo.fnSplitStr('{2}',',')", _pUserInfo.ClientID, _pUserInfo.UserID, pEditValue[i].ControlValue));
//                        break;

//                }
//            }
            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Update(List<DefindControlEntity> pEditValue, string pKeyValue)
        {
            var plm = pEditValue.ToArray().Where(i => i.ControlType != 23).ToList();
            var pall = base.GetEditControls();
            var p = pall.ToArray().Where(i => i.ControlType != 23).ToList();
            StringBuilder sql = base.GetUpdateSql(p, plm, "VIPID", pKeyValue);

//            //删除人员与会员的映射关系
//            for (int i = 0; i < pall.Count; i++)
//            {
//                switch (pall[i].ControlType)
//                {
//                    case 23:
//                        string CorrelationValue = pall[i].CorrelationValue;
//                        sql.AppendLine(string.Format(@" update t
//                                                              set t.IsDelete=1,t.LastUpdateTime=getdate(),LastUpdateBy='{3}'
//                                                              from ClientVIPUserMapping t
//                                                              inner join ClientUser t1 on t1.ClientUserID=t.ClientUserID 
//                                                              where t.VIPID='{0}' and t1.ClientID='{1}' and t.IsDelete=0 and t1.ClientPositionID={2}
//                                                                                                                    ", pKeyValue, _pUserInfo.ClientID, CorrelationValue, _pUserInfo.UserID));
//                        break;
//                }

//            }
//            //添加人员与会员的映射关系
//            for (int i = 0; i < pEditValue.Count; i++)
//            {
//                switch (pEditValue[i].ControlType)
//                {
//                    case 23:
//                        string CorrelationValue = pEditValue[i].CorrelationValue;
//                        sql.AppendLine(string.Format(@"
//                                                insert into ClientVIPUserMapping( ClientUserID,VIPID,ClientID,CreateBy, CreateTime, LastUpdateBy, LastUpdateTime)
//                                                select  value, '{3}',{0},{1}, GETDATE(),{1}, GETDATE()
//                                                from dbo.fnSplitStr('{2}',',')", _pUserInfo.ClientID, _pUserInfo.UserID, pEditValue[i].ControlValue, pKeyValue));
//                        break;

//                }
//            }
//            if (!string.IsNullOrEmpty(_pUserInfo.ClientDistributorID) && _pUserInfo.ClientDistributorID != "0")
//                sql.AppendLine(string.Format("update store set ClientDistributorID='{0}' where VIPID='{1}'", _pUserInfo.ClientDistributorID, pKeyValue));
            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Delete(string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(base.GetDeleteSql("VIPID", pKeyValue));
//            //删除会员与人的关系
//            sql.AppendLine(string.Format(@"update ClientVIPUserMapping
//                                             set IsDelete=1 ,LastUpdateTime=getdate(),LastUpdateBy={1}
//                                            where CONVERT(nvarchar(100),VIPID) in (select value from dbo.fnSplitStr('{0}',','))", pKeyValue, _pUserInfo.UserID));

            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        #endregion
    }


}

