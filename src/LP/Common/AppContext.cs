using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace LP.Common
{
    public class AppContext
    {
        private static IApplicationBuilder _app = null;
        public static IConfigurationRoot Configuration { get; set; }
        public static IApplicationBuilder GetApp()
        {
            return _app;
        }
        public static void SetApp(IApplicationBuilder app)
        {
            _app = app;
        }

    }
}
