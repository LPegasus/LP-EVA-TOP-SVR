using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using LP.Common;
using Microsoft.AspNetCore.Mvc;

namespace LP.Helpers
{
    public class AjaxHelper
    {
        public string Msg { get; set; }
        public object Data { get; set; }
        public Enums.AjaxStatus Status { get; set; }

        public static ContentResult Json(string msg, object data, Enums.AjaxStatus status)
        {

            return new ContentResult
            {
                Content = JsonStr(msg, data, status),
                ContentType = "application/json",
                StatusCode = 200
            };
        }

        public static ContentResult Success(string msg = "", object data = null)
        {
            return Json(msg, data, Enums.AjaxStatus.SUCCESS);
        }

        public static ContentResult Fail(string msg = "", object data = null)
        {
            return Json(msg, data, Enums.AjaxStatus.FAIL);
        }

        public static string JsonStr(string msg = "", object data = null, Enums.AjaxStatus status = Enums.AjaxStatus.SUCCESS)
        {
            return JsonConvert.SerializeObject(
                 new AjaxHelper { Msg = msg, Data = data, Status = status });
        }
    }
}
