using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LP.Common
{
    public static class Enums
    {
        /// <summary>
        /// 性别
        /// </summary>
        public enum Gender
        {
            [Description("男")]
            M = 0,
            [Description("女")]
            F = 1,
            [Description("未知")]
            NA = 2
        }

        public enum AjaxStatus {
            [Description("成功")]
            SUCCESS,
            [Description("失败")]
            FAIL,
            [Description("超时")]
            TIMEOUT
        }

    }
}
