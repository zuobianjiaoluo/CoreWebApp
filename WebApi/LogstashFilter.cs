using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace WebApi
{
    /// <summary>
    /// 记录日志用过滤器
    /// </summary>
    public class LogstashFilter : IActionFilter, IResultFilter
    {
        /// <summary>
        /// Action 调用前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            StringBuilder str = new StringBuilder();
            foreach (var arg in context.ActionArguments) //ActionArguments 获取调用操作时要传递的参数。键是参数名
            {
                str.Append($"{arg.Key} : { JsonConvert.SerializeObject(arg.Value)}");
            }



            var provider = new ServiceCollection()
                .AddLogging(x => x.SetMinimumLevel(0))
                .BuildServiceProvider();

            var factory = provider.GetService<ILoggerFactory>();
            factory.AddConsole();
            factory.AddDebug();

            var logger = provider.GetService<ILoggerFactory>()?.CreateLogger<Program>();
            //context.ActionDescriptor.DisplayName +
            logger.LogInformation("请求："+str.ToString());
        }

        /// <summary>
        /// Action 方法调用后，Result 方法调用前执行
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do nothing
        }

        /// <summary>
        /// Result 方法调用前（View 呈现前）执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            // do nothing
        }

        /// <summary>
        /// Result 方法调用后执行
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ObjectResult)
            {
                //context.ActionDescriptor.DisplayName +
                var result = "响应："+ JsonConvert.SerializeObject(((ObjectResult)context.Result).Value);

                var provider = new ServiceCollection()
                .AddLogging(x => x.SetMinimumLevel(0))
                .BuildServiceProvider();

                var factory = provider.GetService<ILoggerFactory>();
                factory.AddConsole();
                factory.AddDebug();

                var logger = provider.GetService<ILoggerFactory>()?.CreateLogger<Program>();
                logger.LogInformation(result);
            }
        }
    }
   
}
