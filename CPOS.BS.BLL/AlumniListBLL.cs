using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    public class AlumniListBLL
    {
        private AlumniListDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public AlumniListBLL(LoggingSessionInfo pUserInfo)
        {
            this._currentDAO = new AlumniListDAO(pUserInfo);
        }
        #endregion

        #region 获取校友信息列表
        /// <summary>
        /// 校友信息列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="alumniList">查询条件实体</param>
        /// <returns></returns>
        public DataSet GetAlumniListByCondition(string userID, AlumniListEntity alumniList)
        {
            return _currentDAO.GetAlumniListByCondition(userID, alumniList, false);
        }
        #endregion

        #region 我收藏的校友列表
        /// <summary>
        /// 我收藏的校友查询列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="alumniList">查询条件实体</param>
        /// <returns></returns>
        public DataSet GetCollectionAlumniLisByCond(string userID, AlumniListEntity alumniList)
        {
            return _currentDAO.GetAlumniListByCondition(userID, alumniList, true);
        } 
        #endregion
    }
}
