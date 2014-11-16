using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.DataAccess
{
    /// <summary>
    /// 操作Bill的状态
    /// </summary>
    public enum BillOperateStateService
    {
        /// <summary>
        /// 当前角色下不允许新建表单
        /// </summary>
        NotAllowCreate,
        /// <summary>
        /// 当前角色下不允许修改表单
        /// </summary>
        NotAllowModify,
        /// <summary>
        /// 当前角色下不允许批准表单
        /// </summary>
        NotAllowApprove,
        /// <summary>
        /// 当前角色下不允许退回表单
        /// </summary>
        NotAllowReject,
        /// <summary>
        /// 当前角色下不允许删除表单
        /// </summary>
        NotAllowCancel,
        /// <summary>
        /// 该种表单未设置起始状态
        /// </summary>
        NotSetBeginStatus,
        /// <summary>
        /// 该种表单未设置删除时的状态
        /// </summary>
        NotSetDeleteStatus,
        /// <summary>
        /// 该种表单未设置创建动作
        /// </summary>
        NotSetCreateAction,
        /// <summary>
        /// 该种表单未设置修改动作
        /// </summary>
        NotSetModifyAction,
        /// <summary>
        /// 该种表单未设置批准动作
        /// </summary>
        NotSetApproveAction,
        /// <summary>
        /// 该种表单未设置退回动作
        /// </summary>
        NotSetRejectAction,
        /// <summary>
        /// 该种表单未设置删除动作
        /// </summary>
        NotSetCancelAction,
        /// <summary>
        /// 超出允许的金额范围
        /// </summary>
        OutOfMoneyScope,
        /// <summary>
        /// 创建成功
        /// </summary>
        CreateSuccessful,
        /// <summary>
        /// 修改成功
        /// </summary>
        ModifySuccessful,
        /// <summary>
        /// 批准成功
        /// </summary>
        ApproveSuccessful,
        /// <summary>
        /// 退回成功
        /// </summary>
        RejectSuccessful,
        /// <summary>
        /// 删除成功
        /// </summary>
        CancelSuccessful,
        /// <summary>
        /// 状态不存在
        /// </summary>
        NotExistStatus,
        /// <summary>
        /// 表单种类不存在
        /// </summary>
        NotExistKind,
        /// <summary>
        /// 表单不存在
        /// </summary>
        NotExist,
        /// <summary>
        /// 单位信息不存在
        /// </summary>
        UnitInfoNotExist,
        /// <summary>
        /// 操作失败
        /// </summary>
        OperateFail,
        /// <summary>
        /// 超出系统设置时间
        /// </summary>
        OutDateTime
    }

    /// <summary>
    /// 检查角色与表单的操作的关系的结果
    /// </summary>
    public enum BillActionRoleCheckState
    { 
        /// <summary>
        /// 未找到表单操作
        /// </summary>
        NotExistAction,

        /// <summary>
        /// 角色已经与表单的新建操作建立了关系
        /// </summary>
        ExistCreate,

        /// <summary>
        /// 角色已经与表单的修改操作在某前置状态下建立了关系
        /// </summary>
        ExistModify,

        /// <summary>
        /// 角色已经与表单的审核操作在此前置状态下建立了关系
        /// </summary>
        ExistApprove,

        /// <summary>
        /// 角色已经与表单的退回操作在此前置状态下建立了关系
        /// </summary>
        ExistReject,

        /// <summary>
        /// 角色已经与表单的删除操作在此前置状态下建立了关系
        /// </summary>
        ExistCancel,

        /// <summary>
        /// 成功
        /// </summary>
        Successful
    }

    /// <summary>
    /// 检查表单状态的结果
    /// </summary>
    public enum BillStatusCheckState
    {
        /// <summary>
        /// 状态已经存在
        /// </summary>
        ExistBillStatus,

        /// <summary>
        /// 已存在开始标志的状态
        /// </summary>
        ExistBegin,

        /// <summary>
        /// 已存在结束标志的状态
        /// </summary>
        ExistEnd,

        /// <summary>
        /// 已存在删除标志的状态
        /// </summary>
        ExistDelete,

        /// <summary>
        /// 已存在自定义标志的状态
        /// </summary>
        ExistCustom,

        /// <summary>
        /// 成功
        /// </summary>
        Successful
    }

    /// <summary>
    /// 检查表单操作的结果
    /// </summary>
    public enum BillActionCheckState
    {
        /// <summary>
        /// 编码已经存在
        /// </summary>
        ExistCode,

        /// <summary>
        /// 已存在开始操作
        /// </summary>
        ExistCreateAction,

        /// <summary>
        /// 已存在修改操作
        /// </summary>
        ExistModifyAction,

        /// <summary>
        /// 已存在审核操作
        /// </summary>
        ExistApproveAction,

        /// <summary>
        /// 已存在退回操作
        /// </summary>
        ExistRejectAction,

        /// <summary>
        /// 已存在删除操作
        /// </summary>
        ExistCancelAction,

        /// <summary>
        /// 成功
        /// </summary>
        Successful
    }
}
