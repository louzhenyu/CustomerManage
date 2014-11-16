using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Exchange
{
    /// <summary>
    /// 查询某个对象的列表的接口
    /// </summary>
    public interface IObjectQuery
    {
        /// <summary>
        /// 查询出的记录总数
        /// </summary>
        int RecordCount
        { get; set; }
    }

    /// <summary>
    /// 查询某个对象的列表的类(含总数和某页的对象列表)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SelectObjectResultsInfo<T> where T : IObjectQuery
    {
        /// <summary>
        /// 查询出的记录总数
        /// </summary>
        public int DataCount
        {
            get
            {
                if (Data != null && Data.Count > 0)
                {
                    return (Data[0] as IObjectQuery).RecordCount;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 查询出的某页上的数据列表
        /// </summary>
        public IList<T> Data
        { get; set; }
    }
}
