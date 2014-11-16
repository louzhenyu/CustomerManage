using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using JIT.Utility;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;
using JIT.CPOS.BS.DataAccess;
using JIT.Utility.DataAccess;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// CEIBSBLL 
    /// </summary>
    public partial class EMBAUnionBLL
    {
        public LoggingSessionInfo CurrentUserInfo;
        public EMBAUnionDAO _currentDAO;
        public string _pTableName;

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public EMBAUnionBLL(LoggingSessionInfo pUserInfo, string pTableName)
        {
            _pTableName = pTableName;
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new EMBAUnionDAO(pUserInfo, pTableName);
        }
        #endregion

        #region GetList
        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex)
        {
            return this._currentDAO.GetList(pSearch, pPageSize, pPageIndex);
        }
        #endregion

        #region GetVipDetail
        public DataSet GetVipDetail(string id)
        {
            return this._currentDAO.GetVipDetail(id);
        }
        #endregion

        #region GetModuleColumn
        public DataSet GetModuleColumn()
        {
            return this._currentDAO.GetModuleColumn();
        }
        #endregion

        #region GetStatus
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipStatus()
        {
            return this._currentDAO.GetVipStatus();
        }
        #endregion

        #region VipApprove
        /// <summary>
        ///审核
        /// </summary>
        /// <param name="VipId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int VipApprove(string VipId,string Status)
        {
            return this._currentDAO.VipApprove(VipId,Status);
        }
        #endregion 

        #region NoVipApprove
        public int NoVipApprove(string VipId, string Remarke, string Status)
        {
            return this._currentDAO.NoVipApprove(VipId, Remarke,Status);
        }
        #endregion
       
        #region
        public DataSet GetVipStatusNum(List<DefindControlEntity> pSearch)
        {
            return this._currentDAO.GetVipStatusNum(pSearch); 

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
        public PageResultEntity GetPageData(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(this._currentDAO.GetVIPGridFildSQL()); //获取字SQL
            sql.AppendLine("ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.VIPID,t_status.OptionText Status,main.Col14,main.Col15 into #outTemp");
            sql.AppendLine("from VIP main");
            sql.Append(this._currentDAO.GetVIPLeftGridJoinSQL()); //获取联接SQL
            sql.AppendLine(string.Format("left join options t_status on t_status.OptionName='VipStatus' and isnull( t_status.ClientID,'{0}')='{0}' and t_status.OptionValue=main.status", CurrentUserInfo.ClientID));
            sql.AppendLine("");
            sql.Append(this._currentDAO.GetVIPSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", CurrentUserInfo.ClientID));
            sql.Append(this._currentDAO.GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(this._currentDAO.GetPubPageSQL(pPageSize, pPageIndex));
            return new VIPDefindModuleBLL(CurrentUserInfo, "VIP").GetPageData(sql.ToString());
        }
        #endregion

        #region 获取定义
        public List<GridColumnModelEntity> GetGridDataModels()
        {
            return this._currentDAO.GetGridDataModels();
        }

        public List<GridColumnEntity> GetGridColumns()
        {
            return this._currentDAO.GetGridColumns();
        }
        #endregion

        #region GetEditControls
        /// <summary>
        /// 获取编辑页面的控件 
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditControls()
        {
            return this._currentDAO.GetEditControls();
        }
        #endregion

        #region GetEditData
        /// <summary>
        /// 获取编辑页面的值
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditData(string pKeyValue)
        {
            return this._currentDAO.GetEditData(pKeyValue);
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 获取活动人员列表
        /// </summary>
        /// <param name="pParems"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<VipViewEntity> GetUserList(Dictionary<string, string> pParems, int pageIndex, int pageSize)
        {
            return _currentDAO.GetUserList(pParems, pageIndex, pageSize);
        }
        #endregion

        #region ExportUserList
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet ExportUserList(Dictionary<string, string> pParems)
        {
            return this._currentDAO.ExportUserList(pParems);
        }
        #endregion

        #region DeleteEventVipMapping
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID"></param>
        public void DeleteEventVipMapping(string pID)
        {
            this._currentDAO.DeleteEventVipMapping(pID);
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改终端
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Update(List<DefindControlEntity> pEditValue, string pKeyValue)
        {
            return this._currentDAO.Update(pEditValue, pKeyValue);
        }
        #endregion

        
    }
}
