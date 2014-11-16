using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    public class VipClear_InvalidBLL : DefindModuleCRUD
    {
        private LoggingSessionInfo CurrentUserInfo;
        private new VipCliearDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public VipClear_InvalidBLL(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName)
        {
            CurrentUserInfo = pUserInfo;
            this._currentDAO = new VipCliearDAO(pUserInfo);
        }
        #endregion

        #region GetVIPClearList
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetVIPClearList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string pCorrelationValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetStoreGridFildSQL()); //获取字SQL
            sql.AppendLine("ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.VIPID into #outTemp");
            sql.AppendLine("from Vip main");
            sql.AppendFormat(@"
            inner join VIPClearList vc on vc.VIPID=main.VIPID and vc.IsDelete=0 and vc.VIPClearID={0} and vc.ClearRules={1}", pCorrelationValue.Split('|')[0].ToString(), pCorrelationValue.Split('|')[1].ToString());
            sql.AppendLine("");
            sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetStoreSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            return base.GetPageData(sql.ToString());
        }
        #endregion

        #region 拼SQL
        #region 查询需要的字段
        #region 会员列表公共查询的字段
        /// <summary>
        /// 返回会员需要特殊处理的字段SQL
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStoreGridFildSQL()
        {
            var pColumnDefind = base.GetGridColumns();
            StringBuilder fieldSQL = base.GetPubGridFildSQL();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ColumnControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].DataIndex);
                switch (cType)
                {
                    case 201: //来源
                        fieldSQL.AppendLine(string.Format("T_{0}.VipCardGradeName {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 202: //点选经销商
                        fieldSQL.AppendLine(string.Format("T_{0}.VipSourceName {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 203: //职位点选用户
                        fieldSQL.AppendLine(string.Format("T_{0}.Text {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 204: //点选经销商
                        fieldSQL.AppendLine(string.Format("T_{0}.Text {0} ", ColumnName));
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
        public StringBuilder GetStoreEditFildSQL()
        {

            var pColumnDefind = base.GetEditControls();
            StringBuilder fieldSQL = new StringBuilder();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].ControlName);
                switch (cType)
                {
                    case 203: //职位点选用户
                        fieldSQL.AppendLine(string.Format("T_{0}.Value {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 204: //点选经销商
                        fieldSQL.AppendLine(string.Format("T_{0}.Value {0} ", ColumnName));
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
                    case 201: //等级
                        leftJoinSql.AppendLine(string.Format("left join SysVipCardGrade  T_{0} on  T_{0}.VipCardGradeID=main.VipLevel", ColumnName));
                        break;
                    case 202: //会员来源
                        leftJoinSql.AppendLine(string.Format("left join SysVipSource T_{0} on  T_{0}.VipSourceId=main.VipSourceId", ColumnName));
                        break;
                    case 203: //会藉店
                        leftJoinSql.AppendLine(string.Format(@"left join( select 
                                                                            VIPID,
                                                                            Text=stuff(
                                                                                        ( 
                                                                                            select ','+ CONVERT(varchar(50), b.unit_name) 
                                                                                            from VipUnitMapping a
                                                                                            inner join T_Unit b on a.UnitId =b.unit_id 
                                                                                            where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                                         ),1,1,''
                                                                                    ),
                                                                            Value=stuff(
                                                                                        ( 
			                                                                            select ','+ CONVERT(varchar(50), b.unit_id) 
			                                                                                from VipUnitMapping a
			                                                                                inner join T_Unit b on  a.UnitId =b.unit_id 
			                                                                                where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                                         ),1,1,''
                                                                                    )
                                                                            from VipUnitMapping T
                                                                            inner join T_Unit T1 on T.UnitId =T1.unit_id 
                                                                            Where T.IsDelete=0
                                                                            group by VIPID) T_{0} on  T_{0}.VIPID=main.VIPID", ColumnName));
                        break;
                    case 204: //标签
                        leftJoinSql.AppendLine(string.Format(@"left join( 
                                                                select 
                                                                    VIPID,
                                                                    Text=stuff(
                                                                                ( 
                                                                            select ','+ CONVERT(varchar(50), b.TagsName) 
                                                                            from VipTagsMapping a
                                                                            inner join Tags b on a.TagsId =b.TagsId 
                                                                            where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                                 ),1,1,''
                                                                            ),
                                                                    Value=stuff(
                                                                                ( 
				                                                                    select ','+ CONVERT(varchar(50), b.TagsId) 
				                                                                    from VipTagsMapping a
				                                                                    inner join Tags b on a.TagsId =b.TagsId 
				                                                                    where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                                 ),1,1,''
                                                                            )
                                                                from VipTagsMapping  T
                                                            Where T.IsDelete=0
                                                            group by VIPID
                                                            ) T_{0} on  T_{0}.VIPID=main.VIPID", ColumnName));
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
        public StringBuilder GetStoreLeftEditViewJoinSQL()
        {

            StringBuilder leftJoinSql = new StringBuilder();
            var pColumnDefind = base.GetEditControls();
            for (int i = 0; i < pColumnDefind.Count; i++)
            {
                int cType = Convert.ToInt32(pColumnDefind[i].ControlType);
                string ColumnName = Convert.ToString(pColumnDefind[i].ControlName);
                switch (cType)
                {

                    case 203: //会藉店
                        leftJoinSql.AppendLine(string.Format(@"left join( select 
                                                                            VIPID,
                                                                            Text=stuff(
                                                                                        ( 
                                                                                            select ','+ CONVERT(varchar(50), b.unit_name) 
                                                                                            from VipUnitMapping a
                                                                                            inner join T_Unit b on a.UnitId =b.unit_id 
                                                                                            where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                                         ),1,1,''
                                                                                    ),
                                                                            Value=stuff(
                                                                                        ( 
			                                                                            select ','+ CONVERT(varchar(50), b.unit_id) 
			                                                                                from VipUnitMapping a
			                                                                                inner join T_Unit b on  a.UnitId =b.unit_id 
			                                                                                where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                                         ),1,1,''
                                                                                    )
                                                                            from VipUnitMapping T
                                                                            inner join T_Unit T1 on T.UnitId =T1.unit_id 
                                                                            Where T.IsDelete=0
                                                                            group by VIPID) T_{0} on  T_{0}.VIPID=main.VIPID", ColumnName));
                        break;
                    case 204: //标签
                        leftJoinSql.AppendLine(string.Format(@"left join( 
                                                            select 
                                                            VIPID,
                                                            Text=stuff(
                                                                        ( 
                                                                    select ','+ CONVERT(varchar(50), b.TagsName) 
                                                                    from VipTagsMapping a
                                                                    inner join Tags b on a.TagsId =b.TagsId 
                                                                    where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                         ),1,1,''
                                                                    ),
                                                            Value=stuff(
                                                                        ( 
				                                                            select ','+ CONVERT(varchar(50), b.TagsId) 
				                                                            from VipTagsMapping a
				                                                            inner join Tags b on a.TagsId =b.TagsId 
				                                                            where a.VIPID=t.VIPID  and a.IsDelete=0 for xml path('')
                                                                         ),1,1,''
                                                                    )
                                                            from VipTagsMapping  T
                                                           Where T.IsDelete=0
                                                            group by VIPID
                                                            ) T_{0} on  T_{0}.VIPID=main.VIPID", ColumnName));
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
            return searchSQL;
        }
        #endregion

        #region 用于会员查询时，递归查询的条件语句，这些数据一般生成表于主表inner join
        /// <summary>
        /// 返回会员特殊查询连接SQL
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
                    case 201: //等级
                        HierarchyJoinSQL.AppendLine(string.Format("inner join dbo.fnSplitStr('{0}',',') fn_{1} on  fn_{1}.Value = main.VipLevel", ControlValue, ControlName));
                        break;
                    case 202: //会员来源
                        HierarchyJoinSQL.AppendLine(string.Format("inner join dbo.fnSplitStr('{0}',',') fn_{1} on  fn_{1}.Value = main.VipSourceId", ControlValue, ControlName));
                        break;
                    case 203: //会藉店与会员映射
                        HierarchyJoinSQL.AppendLine(string.Format(@"
                                                        inner join (
                                                                        select 
                                                                             distinct b.VipId 
                                                                        from dbo.fnSplitStr('{0}',',') a
                                                                        inner join VipUnitMapping b on a.value=b.UnitId and b.isDelete=0 
                                                                        inner join T_Unit c on b.UnitId =c.unit_id 
                                                                    ) fn_{1} on   main.VipId =fn_{1}.VipId", ControlValue, ControlName));
                        break;
                    case 204: //会员与标签
                        HierarchyJoinSQL.AppendLine(string.Format(@"
                                                        inner join (
                                                                        select 
                                                                                distinct b.VipId 
                                                                        from dbo.fnSplitStr('{0}',',') a
                                                                        inner join VipTagsMapping b on a.value=b.TagsId and b.isDelete=0 
                                                                    ) fn_{1} on   main.VipId =fn_{1}.VipId", ControlValue, ControlName));
                        break;
                }

            }
            return HierarchyJoinSQL;

        }
        #endregion
        #endregion


        #endregion
    }
}
