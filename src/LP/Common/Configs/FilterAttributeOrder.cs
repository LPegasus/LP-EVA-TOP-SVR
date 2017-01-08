using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Common.Attributes;

namespace LP.Common.Configs
{
    public class FilterAttributeOrder
    {
        public static int GetOrder(Type t)
        {
            switch (t.Name)
            {
                case "HttpHeaderAttribute":
                    return 0;
                default:
                    return 99999;
            }
        }
    }
}
