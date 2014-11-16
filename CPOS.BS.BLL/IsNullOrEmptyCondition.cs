using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL
{
    class IsNullOrEqaulCondition : ComplexCondition
    {
        public IsNullOrEqaulCondition(LoggingSessionInfo CurrentUserInfo, string fieldName)
        {
            this.Left = new EqualsCondition() { FieldName = fieldName, Value = CurrentUserInfo.ClientID };
            this.Operator = LogicalOperators.Or;
            this.Right = new IsNullCondition() { FieldName = fieldName, IsNull = true };
        }
    }
}
