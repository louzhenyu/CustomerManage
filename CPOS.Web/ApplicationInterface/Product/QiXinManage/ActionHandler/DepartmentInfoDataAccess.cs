using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
using System.Data;

namespace JIT.CPOS.Web.ApplicationInterface.Product.QiXinManage.ActionHandler
{
    public class DepartmentInfoDataAccess
    {
        private readonly LoggingSessionInfo _loggingSessionInfo;
        public DepartmentInfoDataAccess(LoggingSessionInfo loggingSessionInfo)
        {
            _loggingSessionInfo = loggingSessionInfo;
        }

        /// <summary>
        /// 获取部门全部直接子部门
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <returns></returns>
        public List<DepartmentInfo> GetDeptDirectSubDept(string pUnitID)
        {
            List<DepartmentInfo> listDepartment = new List<DepartmentInfo>();
            TUnitBLL unitBll = new TUnitBLL(_loggingSessionInfo);
            DataTable dTbl = unitBll.GetDirectSubDept(pUnitID);
            if (dTbl != null)
                listDepartment = DataTableToObject.ConvertToList<DepartmentInfo>(dTbl);
            return listDepartment;
        }

        /// <summary>
        /// 获取部门直接个人成员
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <returns></returns>
        public List<PersonListItemInfo> GetDirectPersMembers(string pUnitID)
        {
            List<PersonListItemInfo> listPersMembers = new List<PersonListItemInfo>();
            UserDeptJobMappingBLL mappingBll = new UserDeptJobMappingBLL(_loggingSessionInfo);
            DataTable dTbl = mappingBll.GetDirectPersMembers(pUnitID);
            if (dTbl != null)
                listPersMembers = DataTableToObject.ConvertToList<PersonListItemInfo>(dTbl);
            return listPersMembers;
        }

        /// <summary>
        /// 获取部门直接成员
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <returns></returns>
        public List<DepartmentTotalMember> GetDirectMembers(string pUnitID)
        {
            List<DepartmentInfo> department = GetDeptDirectSubDept(pUnitID);
            List<DepartmentTotalMember> listMembers = new List<DepartmentTotalMember>();
            DepartmentTotalMember member = null;
            DataTable dTbl = null;
            UserDeptJobMappingBLL mappingBll = new UserDeptJobMappingBLL(_loggingSessionInfo);
            foreach (var item in department)
            {
                member = new DepartmentTotalMember();
                member.UnitID = item.UnitID;
                member.UnitName = item.UnitName;
                dTbl = mappingBll.GetDirectPersMembers(item.UnitID);
                if (dTbl != null)
                    member.DeptDirectPersMemberList = DataTableToObject.ConvertToList<PersonListItemInfo>(dTbl);
                listMembers.Add(member);
            }
            return listMembers;
        }
        /// <summary>
        /// 递归取子级成员
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <returns></returns>
        public List<DepartmentTotalMember> GetSubMembers(string pUnitID)
        {
            UserDeptJobMappingBLL mappingBll = new UserDeptJobMappingBLL(_loggingSessionInfo);
            List<DepartmentInfo> department = GetDeptDirectSubDept(pUnitID);
            List<DepartmentTotalMember> listMembers = new List<DepartmentTotalMember>();
            DepartmentTotalMember member = null;
            DataTable dTbl = null;
            foreach (var item in department)
            {
                member = new DepartmentTotalMember();
                member.UnitID = item.UnitID;
                member.UnitName = item.UnitName;
                dTbl = mappingBll.GetDirectPersMembers(item.UnitID);
                if (dTbl != null)
                    member.DeptDirectPersMemberList = DataTableToObject.ConvertToList<PersonListItemInfo>(dTbl);
                member.SubDepartmentList = GetSubMembers(item.UnitID);
                listMembers.Add(member);
            }
            return listMembers;
        }

        /// <summary>
        /// 获取部门及其各级子部门的所有成员
        /// </summary>
        /// <param name="pUnitID"></param>
        /// <returns></returns>
        public DepartmentTotalMember GetDeptAllMembers(string pUnitID, string pUnitName)
        {
            DataTable parentDTbl = null;
            UserDeptJobMappingBLL mappingBll = new UserDeptJobMappingBLL(_loggingSessionInfo);
            //父
            DepartmentTotalMember parentMember = new DepartmentTotalMember();
            parentMember.UnitID = pUnitID;
            parentMember.UnitName = pUnitName;
            parentDTbl = mappingBll.GetDirectPersMembers(pUnitID);
            if (parentDTbl != null)
                parentMember.DeptDirectPersMemberList = DataTableToObject.ConvertToList<PersonListItemInfo>(parentDTbl);
            //子
            parentMember.SubDepartmentList = GetSubMembers(pUnitID);
            return parentMember;
        }
    }
}