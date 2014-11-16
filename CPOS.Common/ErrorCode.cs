using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace JIT.CPOS.Common
{
    #region ErrorCode
    public enum ErrorCode
    {
        Normal,

        /// <summary>
        /// 未知错误
        /// </summary>
        A000,

        /// <summary>
        /// 请求次数过多
        /// </summary>
        A001,

        /// <summary>
        /// 后台数据格式错误
        /// </summary>
        A002,

        /// <summary>
        /// 包含非法字符
        /// </summary>
        A003,

        /// <summary>
        /// 长度不合法
        /// </summary>
        A004,

        /// <summary>
        /// 令牌不匹配或过期
        /// </summary>
        A005,

        /// <summary>
        /// 不存在
        /// </summary>
        A006,

        /// <summary>
        /// 获取后台数据失败
        /// </summary>
        A007,

        /// <summary>
        /// 数据格式错误
        /// </summary>
        A008,

        /// <summary>
        /// 不匹配
        /// </summary>
        A009,

        /// <summary>
        /// 已存在
        /// </summary>
        A010,

        /// <summary>
        /// 接口已关闭
        /// </summary>
        A011,

        /// <summary>
        /// 连接数据通道已关闭
        /// </summary>
        A012,

        /// <summary>
        /// 已发布
        /// </summary>
        A013,

        /// <summary>
        /// 已停用
        /// </summary>
        A014,

        /// <summary>
        /// 操作失败
        /// </summary>
        A015,

        /// <summary>
        /// 不能为空
        /// </summary>
        A016,

        /// <summary>
        /// 超出数值范围
        /// </summary>
        A017,

        /// <summary>
        /// 保存数据至后台失败
        /// </summary>
        A018,

        /// <summary>
        /// 数据不合法
        /// </summary>
        A019,

        /// <summary>
        /// 
        /// </summary>
        A020,

        /// <summary>
        /// 
        /// </summary>
        A021,

        /// <summary>
        /// 
        /// </summary>
        A022,

        /// <summary>
        /// 
        /// </summary>
        A023,

        /// <summary>
        /// 
        /// </summary>
        A024,

        /// <summary>
        /// 
        /// </summary>
        A025,

        /// <summary>
        /// 
        /// </summary>
        A026,

        /// <summary>
        /// 
        /// </summary>
        A027,

        /// <summary>
        /// 
        /// </summary>
        A028,

        /// <summary>
        /// 
        /// </summary>
        A029,

        /// <summary>
        /// 
        /// </summary>
        A030
    }
    #endregion
}
