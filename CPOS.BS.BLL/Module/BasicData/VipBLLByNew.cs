/*
 * Author		:陆荣平
 * EMail		:lurp@jitmarketing.cn
 * Company		:JIT
 * Create On	:2013/12/10 13:45:18
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
using System.Text;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;

using JIT.CPOS.BS.Entity;
namespace JIT.CPOS.BS.BLL.Module.BasicData
{
    /// <summary>
    /// VipBLL 
    /// </summary>
    public class VipBLLByNew : DefindModuleCRUD
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pUserInfo">当前用户信息实体</param>
        /// <param name="pTableName">模块名称</param>
        public VipBLLByNew(LoggingSessionInfo pUserInfo, string pTableName)
            : base(pUserInfo, pTableName) { }
        #endregion

        #region GetRepeatRowCount
        public string GetRepeatRowCount(List<DefindControlEntity> pSearch, string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            // sql.Append(GetTempSql(pKeyValue));//需要生成的临时表
            sql.AppendLine("select ");
            sql.AppendLine("Count(1)  RepeatRowCount");
            sql.AppendLine("from Vip main");
            // sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");

            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));
            // sql.Append(GetPubRepeatSearchSQL(pSearch)); //获取条件联接SQL
            if (!string.IsNullOrEmpty(pKeyValue))
            {
                sql.AppendLine(string.Format(" and main.VIPID!='{0}'", pKeyValue));
            }
            sql.Append(GetPubRepeatSearchSQL(pSearch)); //获取条件
            //  sql.Append(GetDropTempSql()); //需要删除的临时表
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

        #region GetPageData
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetPageData(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string correlationValue)
        {
            int cnt = GetVipUnitCnt();
            StringBuilder sql = new StringBuilder();
            if (cnt > 0)
            {
                sql.AppendLine(string.Format(@"
                declare @unit_id nvarchar(max)
                set @unit_id=(
                select
                    unit_id + ','
                from T_User_Role
                where user_id='{0}' and status = 1
                FOR XML PATH(''))   

                set @unit_id=(
                select
                    unit_id+','
                from dbo.vw_unit_level a
                inner join dbo.fnSplitStr(@unit_id,',') b on a.path_unit_id like '%'+b.value+'%'
                where a.customer_id='{1}'
                FOR XML PATH('')) ", _pUserInfo.UserID, _pUserInfo.ClientID));
            }
            sql.AppendLine("select ");
            sql.Append(GetStoreGridFildSQL()); //获取字SQL
            sql.AppendLine("ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.VIPID into #outTemp");
            sql.AppendLine("from Vip main");

            //new line
            if (correlationValue.Length > 1)    //Fix a bug of correlationValue only has "&"
            {
                switch (int.Parse(correlationValue.Split('&')[0]))
                {
                    case 1://会员奖励
                        break;
                    case 2://导购员奖励
                    case 3://门店奖励
                        sql.AppendLine(" inner join VipUnitMapping vum on main.VIPID=vum.VIPID and main.isdelete=vum.isdelete ");
                        break;
                }
            }
            //new line 角色的关联
            if (!string.IsNullOrEmpty(_pTableName) && _pTableName.ToLower() != "vip")
            {
                sql.AppendLine(@" 
                inner join viprolemapping vrm on main.vipid=vrm.vipid and main.clientid=vrm.clientid and main.isdelete=vrm.isdelete 
                inner join t_role role on vrm.roleid=role.role_id and vrm.clientid=role.customer_id and role.status=1
                ");
            }

            sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetStoreSearchJoinSQL(pSearch)); //获取条件联接SQL
            //会籍店链接
            if (cnt > 0)
            {
                sql.AppendLine("");
                sql.AppendLine(string.Format(@"
                left join (
	                select
	                    VipId
	                from dbo.fnSplitStr(@unit_id,',') a
	                inner join VipUnitMapping b on a.value=b.UnitId and b.isDelete=0  
	                group by UnitId,VIPID
                )fn_VipUnitM_New on   main.VipId =fn_VipUnitM_New.VipId"));
            }
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", base._pUserInfo.ClientID));

            //new line
            if (correlationValue.Length > 1)    //Fix a bug of correlationValue only has "&"
            {
                switch (int.Parse(correlationValue.Split('&')[0]))
                {
                    case 1://会员奖励
                        sql.AppendLine(string.Format(" and HigherVipID='{0}' ", correlationValue.Split('&')[1]));
                        break;
                    case 2://导购员奖励
                        sql.AppendLine(string.Format(" and vum.userid='{0}' ", correlationValue.Split('&')[1]));
                        break;
                    case 3://门店奖励
                        sql.AppendLine(string.Format(" and vum.UnitId='{0}' ", correlationValue.Split('&')[1]));
                        break;
                }
            }
            //new line 角色的关联
            if (!string.IsNullOrEmpty(_pTableName) && _pTableName.ToLower() != "vip")
            {
                sql.AppendLine(string.Format(" and role.table_name='{0}' ", _pTableName));
            }

            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(base.GetPubPageSQL(pPageSize, pPageIndex));
            return base.GetPageData(sql.ToString());

        }
        #endregion

        #region GetEditData
        /// <summary>
        /// 获取编辑页面的值
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditData(string pKeyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetStoreEditFildSQL()); //获取字SQL
            sql.AppendLine("main.VIPID ");
            sql.AppendLine("from Vip main");
            sql.Append(GetStoreLeftEditViewJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.AppendLine(string.Format("Where main.IsDelete=0 and  main.ClientID='{0}' and CONVERT(varchar(100),main.VIPID) in (select value from dbo.fnSplitStr('{1}',','))", base._pUserInfo.ClientID, pKeyValue));
            DataTable db = base.GetData(sql.ToString());
            List<DefindControlEntity> l = base.GetEditControls();
            for (var i = 0; i < l.Count; i++)
            {
                if (db.Rows.Count > 0)
                {
                    if (db.Rows[0][l[i].ControlName] != DBNull.Value)
                    {
                        l[i].ControlValue = db.Rows[0][l[i].ControlName].ToString();
                    }
                }

            }
            return l;
        }
        #endregion

        #region GetStoreGridFildSQL
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
                    case 202: 
                        fieldSQL.AppendLine(string.Format("T_{0}.VipSourceName {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 203: 
                        fieldSQL.AppendLine(string.Format("T_{0}.Text {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 204: 
                        fieldSQL.AppendLine(string.Format("T_{0}.Text {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 205: //新版会员卡
                        fieldSQL.AppendLine(string.Format("Unit.Unit_Name {0}", ColumnName));
                        fieldSQL.Append(",");
                        break;
                }

            }

            return fieldSQL;
        }
        #endregion

        #region GetStoreEditFildSQL
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
                    case 203: //会籍店
                        fieldSQL.AppendLine(string.Format("T_{0}.Value + '|' +T_{0}.Text {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 204: //点选经销商
                        fieldSQL.AppendLine(string.Format("T_{0}.Value {0} ", ColumnName));
                        fieldSQL.Append(",");
                        break;
                    case 30: //地图
                        fieldSQL.AppendLine(string.Format("main.Longitude+','+main.Latitude {0} ", ColumnName));
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

        #region GetStoreLeftGridJoinSQL
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
                    case 205: //新版会员卡
                        leftJoinSql.AppendLine(string.Format("left join T_Unit  Unit on  Unit.Unit_Id = main.CouponInfo", ColumnName));
                        break;


                }

            }
            return leftJoinSql;

        }
        #endregion

        #region GetStoreLeftEditViewJoinSQL
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

        #region GetStoreSearchJoinSQL
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

        #region GetStroeGridSearchSQL
        /// <summary>
        /// 返回会员要处理的条件语句
        /// </summary>
        /// <returns></returns>
        public StringBuilder GetStroeGridSearchSQL(List<DefindControlEntity> pSearch)
        {
            StringBuilder searchSQL = base.GetPubGridSearchSQL(pSearch);
            //  searchSQL.AppendLine(string.Format("and main.ClientDistributorID='{0}'", _pUserInfo.ClientDistributorID));
            return searchSQL;
        }
        #endregion

        #region Create
        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Create(List<DefindControlEntity> pEditValue)
        {
            var plm = pEditValue.ToArray().Where(i => i.ControlType != 203).ToList();
            StringBuilder sql = base.GetCreateSql(plm, "VIPID", "VIP");
            if (!string.IsNullOrEmpty(_pTableName) && _pTableName.ToLower() != "vip")
            {
                sql.AppendFormat(@" 
                declare @roleid nvarchar(400)=(SELECT TOP 1 role_id FROM dbo.T_Role WHERE customer_id='{0}' AND table_name='{1}' AND status=1)", _pUserInfo.ClientID, _pTableName);

                sql.AppendFormat(@" 
                INSERT INTO dbo.VIPRoleMapping
                        ( 
                          VIPID ,
                          RoleID ,
                          ClientID ,
                          CreateBy
                        )
                VALUES  ( 
                          @GUID , -- VIPID - nvarchar(50)
                          @roleid , -- RoleID - nvarchar(50)
                          '{0}' , -- ClientID - nvarchar(50)
                          '{1}'
                        )
                ", _pUserInfo.ClientID, _pUserInfo.UserID);
            }
            for (int i = 0; i < pEditValue.Count; i++)
            {
                switch (pEditValue[i].ControlType)
                {
                    case 203: //会员与门店的关联
                        if (!string.IsNullOrEmpty(pEditValue[i].ControlValue))
                            sql.AppendLine(string.Format(@"
                            insert into VipUnitMapping(VipUnitID,UnitId,VIPID,CreateBy, CreateTime, LastUpdateBy, LastUpdateTime,IsDelete)
                            select  NEWID(),value, @GUID,'{0}', GETDATE(),'{0}', GETDATE(),0
                            from dbo.fnSplitStr('{1}',',')", _pUserInfo.UserID, pEditValue[i].ControlValue));
                        break;
                }
            }
            bool res = base.ICRUDable(sql.ToString());
            return true;
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改会员
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Update(List<DefindControlEntity> pEditValue, string pKeyValue)
        {
            var plm = pEditValue.ToArray().Where(i => i.ControlType != 203 && i.ControlType != 204).ToList();
            var pall = base.GetEditControls();
            var p = pall.ToArray().Where(i => i.ControlType != 203 && i.ControlType != 204).ToList();
            StringBuilder sql = base.GetUpdateSql(p, plm, "VIPID", pKeyValue, "VIP");

            //删除会员与门店的映射关系
            for (int i = 0; i < pall.Count; i++)
            {
                switch (pall[i].ControlType)
                {
                    case 203:
                        string CorrelationValue = pall[i].CorrelationValue;
                        sql.AppendLine(string.Format(@" 
                        update t
                        set t.IsDelete=1,t.LastUpdateTime=getdate(),LastUpdateBy='{3}'
                        from VipUnitMapping t
                        inner join Vip t1 on t1.VIPID=t.VIPID 
                        where t.VIPID='{0}' and t1.ClientID='{1}' and t.IsDelete=0", pKeyValue, _pUserInfo.ClientID, CorrelationValue, _pUserInfo.UserID));
                        break;
                }

            }
            //添加会员与门店的映射关系
            for (int i = 0; i < pEditValue.Count; i++)
            {
                switch (pEditValue[i].ControlType)
                {
                    case 203://会员与门店的关系
                        string CorrelationValue = pEditValue[i].CorrelationValue;
                        sql.AppendLine(string.Format(@"
                        insert into VipUnitMapping(VipUnitID,UnitId,VIPID,CreateTime,LastUpdateBy,LastUpdateTime)
                        select  NEWID(),value,'{2}',GETDATE(),'{0}',GETDATE()
                        from dbo.fnSplitStr('{1}',',')", _pUserInfo.UserID, pEditValue[i].ControlValue, pKeyValue));
                        break;
                }
            }

            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        #endregion

        #region Delete
        public bool Delete(string pKeyValue)
        {

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(base.GetDeleteSql("VIPID", pKeyValue, "VIP"));

            //删除人员与角色的关系
            sql.AppendLine(string.Format("Update VIPRoleMapping set IsDelete=1,LastUpdateBy='{0}',LastUpdateTime=getdate() where VIPID='{1}' and IsDelete=0", _pUserInfo.UserID, pKeyValue));
            bool res = base.ICRUDable(sql.ToString());
            return res;
        }
        #endregion

        #region GetStoreData
        /// <summary>
        /// 不分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <returns>终端数据</returns>
        public DataTable GetStoreData(List<DefindControlEntity> pSearch)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(GetStoreGridFildSQL()); //获取字SQL
            sql.AppendLine("main.VIPID ");
            sql.AppendLine("from Vip main");

            //new line 角色的关联
            if (!string.IsNullOrEmpty(_pTableName) && _pTableName.ToLower() != "vip")
            {
                sql.AppendLine(@" 
                inner join viprolemapping vrm on main.vipid=vrm.vipid and main.clientid=vrm.clientid and main.isdelete=vrm.isdelete 
                inner join t_role role on vrm.roleid=role.role_id and vrm.clientid=role.customer_id and role.status=1
                ");
            }

            sql.Append(GetStoreLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine("");
            sql.Append(GetStoreSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}' ", base._pUserInfo.ClientID));

            //new line 角色的关联
            if (!string.IsNullOrEmpty(_pTableName) && _pTableName.ToLower() != "vip")
            {
                sql.AppendLine(string.Format(" and role.table_name='{0}' ", _pTableName));
            }

            sql.Append(GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(" order by main.CreateTime desc  ");
            return base.GetData(sql.ToString());
        }
        #endregion

        #region 获取客户是否配置了会籍店
        public int GetVipUnitCnt()
        {
            return this._currentDAO.GetVipUnitCnt();
        }
        #endregion

    }
}
