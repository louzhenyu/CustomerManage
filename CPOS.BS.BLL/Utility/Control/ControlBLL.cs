/*
 * Author		:tiansheng.zhu
 * EMail		:tiansheng.zhu@jitmarketing.cn
 * Company		:JIT
 * Create On	:03/01/2013 14:57:00 PM
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

using JIT.CPOS.BS.DAL;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;

using JIT.Utility.Reflection;

namespace JIT.CPOS.BS.BLL.Control
{
    /// <summary>
    /// ControlBL   
    /// </summary>
    public class ControlBLL
    {

        protected LoggingSessionInfo CurrentUserInfo;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public ControlBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
        }
        #endregion
        #region GetOptionsByClientID
        /// <summary>
        /// 选项Option 单选数据集
        /// </summary>
        /// <param name="OptionType">选项的类型，必须参数</param>
        /// <returns>ControlOptionsEntity[]</returns>
        public ControlOptionsEntity[] GetOptionsByClientID(string OptionType, string customer_id)
        {

            DataSet ds = new ControlDAO(CurrentUserInfo).GetOptionsByClientID(OptionType, customer_id);
            return DataLoader.LoadFrom<ControlOptionsEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetLEventsType
        /// <summary>
        /// 获取EventType数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetLEventsType()
        {
            return new ControlDAO(CurrentUserInfo).GetLEventsType();
          
        }
        #endregion

        #region GetMobileModule
        /// <summary>
        /// 获取MobileModule数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetMobileModule()
        {
            return new ControlDAO(CurrentUserInfo).GetMobileModule();

        }
        #endregion

        /*
        #region GetTreeByParentID
        /// <summary>
        /// 查询是否是Tree的形式
        /// </summary>
        /// <param name="pTableName">传入数据表</param>
        /// <param name="pParentID">传入ParentID</param>
        /// <returns>bool</returns>
        public bool GetTreeByParentID(string pTableName, string pParentID)
        {
            int result = new ControlDAO(CurrentUserInfo).GetTreeByParentID(pTableName, pParentID);
            return result > 0;
        }
        #endregion

        #region GetChannelByClientID
        /// <summary>
        /// 渠道查询，单选多选数据集
        /// </summary>
        /// <returns>ControlChannelEntity[]</returns>
        public ControlChannelEntity[] GetChannelByClientID(string pParentID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetChannelByClientID(pParentID);
            return DataLoader.LoadFrom<ControlChannelEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetChainByClientID
        /// <summary>
        /// 连锁查询，单选多选数据集
        /// </summary>
        /// <returns>ControlChainEntity[]</returns>
        public ControlChainEntity[] GetChainByClientID(string pParentID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetChainByClientID(pParentID);
            return DataLoader.LoadFrom<ControlChainEntity>(ds.Tables[0]);
        }
        #endregion

     

        #region GetClientPositionByClientID
        /// <summary>
        /// 职位查询，单选多选数据集
        /// </summary>
        /// <returns>ControlClientPositionEntity[]</returns>
        public ControlClientPositionEntity[] GetClientPositionByClientID(string pParentID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetClientPositionByClientID(pParentID);
            return DataLoader.LoadFrom<ControlClientPositionEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetCategoryByClientID
        /// <summary>
        /// 品类查询，单选多选数据集
        /// </summary>
        /// <returns>ControlCategoryEntity[]</returns>
        public ControlCategoryEntity[] GetCategoryByClientID(string pParentID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetCategoryByClientID(pParentID);
            return DataLoader.LoadFrom<ControlCategoryEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetBrandByClientID
        /// <summary>
        /// 品牌查询，单选多选数据集
        /// </summary>
        /// <returns>ControlBrandEntity[]</returns>
        public ControlBrandEntity[] GetBrandByClientID(string pParentID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetBrandByClientID(pParentID);
            return DataLoader.LoadFrom<ControlBrandEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetClientStructureByClientID
        /// <summary>
        /// 组织结构查询，单选多选数据集
        /// </summary>
        /// <returns>ControlClientStructureEntity[]</returns>
        public ControlClientStructureEntity[] GetClientStructureByClientID(string pParentID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetClientStructureByClientID(pParentID);
            return DataLoader.LoadFrom<ControlClientStructureEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetHierarchyByClientID
        /// <summary>
        /// 层系 第一级查询，单选数据集
        /// </summary>
        /// <returns>ControlHierarchyEntity[]</returns>
        public ControlHierarchyEntity[] GetHierarchyByClientID(int pHierarchyType)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetHierarchyByClientID(pHierarchyType);
            return DataLoader.LoadFrom<ControlHierarchyEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetHierarchyLevelByClientID
        /// <summary>
        /// 层系等级 第二级查询，单选数据集
        /// </summary>
        /// <returns>ControlHierarchyLevelEntity[]</returns>
        public ControlHierarchyLevelEntity[] GetHierarchyLevelByClientID(string pHierarchyID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetHierarchyLevelByClientID(pHierarchyID);
            return DataLoader.LoadFrom<ControlHierarchyLevelEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetHierarchyItemByClientID
        /// <summary>
        /// 层系项数据 第三级查询，单选，多选数据集
        /// </summary>
        /// <returns>ControlHierarchyItemEntity[]</returns>
        public ControlHierarchyItemEntity[] GetHierarchyItemByClientID(string pHierarchyID, string pParentID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetHierarchyItemByClientID(pHierarchyID, pParentID);
            return DataLoader.LoadFrom<ControlHierarchyItemEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetProvince
        /// <summary>
        /// 查询省的数据
        /// </summary>
        /// <returns>ControlProvinceEntity[]</returns>
        public ControlProvinceEntity[] GetProvince()
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetProvince();
            return DataLoader.LoadFrom<ControlProvinceEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetCityByProvinceID
        /// <summary>
        /// 查询市数据，根据省的ID
        /// </summary>
        /// <param name="pProvinceID">参数省的ID 1,2,3 不能为空</param>
        /// <returns>ControlCityEntity[]</returns>
        public ControlCityEntity[] GetCityByProvinceID(string pProvinceID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetCityByProvinceID(pProvinceID);
            return DataLoader.LoadFrom<ControlCityEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetCityByCityID
        /// <summary>
        /// 查询市数据，根据市的ID
        /// </summary>
        /// <param name="pProvinceID">参数市的ID 1 不能为空</param>
        /// <returns>ControlCityEntity[]</returns>
        public ControlCityEntity[] GetCityByCityID(string pCityID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetCityByCityID(pCityID);
            return DataLoader.LoadFrom<ControlCityEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetDistrictByCityID
        /// <summary>
        /// 查询县数据，根据市的ID
        /// </summary>
        /// <param name="pCitryID">参数市的ID 1,2,3 不能为空</param>
        /// <returns>ControlDistrictEntity[]</returns>
        public ControlDistrictEntity[] GetDistrictByCityID(string pCityID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetDistrictByCityID(pCityID);
            return DataLoader.LoadFrom<ControlDistrictEntity>(ds.Tables[0]);
        }
        #endregion

        #region GetDistrictByDistrictID
        /// <summary>
        /// 查询县数据，根据县的ID
        /// </summary>
        /// <param name="pCitryID">参数县的ID 1不能为空</param>
        /// <returns>ControlDistrictEntity[]</returns>
        public ControlDistrictEntity[] GetDistrictByDistrictID(string pDistrictID)
        {
            DataSet ds = new ControlDAO(CurrentUserInfo).GetDistrictByDistrictID(pDistrictID);
            return DataLoader.LoadFrom<ControlDistrictEntity>(ds.Tables[0]);
        }
        #endregion
         * 
         * */
    }
}
