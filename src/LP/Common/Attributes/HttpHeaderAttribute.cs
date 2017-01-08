using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace LP.Common.Attributes
{
    public class HttpHeaderAttribute : ActionFilterAttribute {
        protected List<KeyValuePair<string, StringValues>> _headers;
        public HttpHeaderAttribute() { _headers = new List<KeyValuePair<string, StringValues>>(); }
        public HttpHeaderAttribute(params string[] headers)
        {
            _headers = headers.Select(header => {
                var strList = header.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                return new KeyValuePair<string, StringValues>(strList[0], strList[1]);
            }).ToList();
            Order = Configs.FilterAttributeOrder.GetOrder(GetType());
        }

        public HttpHeaderAttribute(params KeyValuePair<string, StringValues>[] headers)
        {
            _headers = headers.ToList();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var req = context.HttpContext.Request;
            var reqHeaders = context.HttpContext.Request.Headers;
            if (_headers.Count() > 0)
            {
                // 判断是否跨域
                if (reqHeaders.Any(d => d.Key.ToLower() == "access-control-request-method") && reqHeaders.Any(d => d.Key.ToLower() == "access-control-request-headers"))
                {
                    var acao = _headers.FirstOrDefault(d => d.Key == "Access-Control-Allow-Origin");
                    if (acao.Key != null)
                    {
                        _headers.Add(new KeyValuePair<string, StringValues>("Access-Control-Allow-Methods", new StringValues("POST,GET,OPTIONS")));
                        _headers.Add(new KeyValuePair<string, StringValues>("Access-Control-Allow-Credentials", "true"));

                        if (acao.Value == "*" && _headers.Remove(acao))
                        {
                            _headers.Add(new KeyValuePair<string, StringValues>("Access-Control-Allow-Origin", req.Host.Value));
                        }
                    }

                }
                foreach (var header in _headers)
                {
                    context.HttpContext.Response.Headers.Append(header);
                }
            }
            base.OnActionExecuting(context);
        }

        protected ActionFilterAttribute Append(string key, params string[] value)
        {
            _headers.Add(new KeyValuePair<string, StringValues>(key, new StringValues(value)));
            return this;
        }

        protected ActionFilterAttribute CORS(params string[] urls)
        {
            _headers.Add(new KeyValuePair<string, StringValues>("Access-Control-Allow-Origin", new StringValues(urls)));
            return this;
        }

        protected ActionFilterAttribute NoKeepAlive()
        {
            _headers.Add(new KeyValuePair<string, StringValues>("Connection", "Close"));
            return this;
        }
    }

    public class CORSAttribute : HttpHeaderAttribute {
        public CORSAttribute(params string[] urls)
        {
            if (urls.Length == 0)
            {
                CORS("*");
            }
            else
            {
                CORS(urls);
            }
        }
    }

    public class NoKeepAlive : HttpHeaderAttribute {
        public NoKeepAlive()
        {
            NoKeepAlive();
        }
    }
}
