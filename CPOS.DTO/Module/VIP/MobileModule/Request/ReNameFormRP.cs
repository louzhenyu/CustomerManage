using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.MobileModule.Request
{
    public class ReNameFormRP : IAPIRequestParameter
    {
        public string MobileModuleID { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public void Validate()
        {
           // throw new System.NotImplementedException();
        }
    }
}
