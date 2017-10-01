using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace Events
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var strategy = new FixedInterval("fixed", 10, TimeSpan.FromSeconds(3));
            var strategies = new List<RetryStrategy> { strategy };
            var manager = new RetryManager(strategies, "fixed");
            RetryManager.SetDefault(manager);
        }

        private static ConnectionMultiplexer _redisCache;
        public static ConnectionMultiplexer RedisCache
        {
            get
            {
                if(_redisCache == null || !_redisCache.IsConnected)
                {
                    _redisCache = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["RedisCacheConnection"].ConnectionString);
                }
                return _redisCache;
            }
        }
    }
}
