using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.Text;

namespace WebApp
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
            ParameterLogEntity logEntity = new ParameterLogEntity()
            {
                Signature = context.ActionDescriptor.DisplayName
            };
            StringBuilder str = new StringBuilder();
            foreach (var arg in context.ActionArguments) //ActionArguments 获取调用操作时要传递的参数。键是参数名
            {
                str.Append($"{arg.Key} : { JsonConvert.SerializeObject(arg.Value)}");
            }

            Logger.Info(str.ToString());
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
                var result = JsonConvert.SerializeObject(((ObjectResult)context.Result).Value);

                Logger.Info(result);
            }
        }
    }
    
    public class ParameterLogEntity
    {
        public string Signature { get; set; }
        public string[] Args { get; set; }
    }

    public class ResultLogEntity
    {
        public string Result { get; set; }
    }
}
