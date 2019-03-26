using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHost
{
    /// <summary>
    /// 简单的定时任务执行
    /// </summary>
    public class TimedExecutService : BackgroundService
    {
        public ILogger _logger;
        public TimedExecutService(ILogger<TimedExecutService> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken">传播应取消操作的通知</param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：启动");

                while (!stoppingToken.IsCancellationRequested) //获取是否为此令牌请求取消;如果此令牌请求已被取消，则为true；否则，为false。
                {
                    //创建一个可取消的任务，该任务在时间延迟之后完成
                    await Task.Delay(5000, stoppingToken); //启动后5秒执行一次 (用于测试)
                    _logger.LogInformation(DateTime.Now.ToString() + " 执行逻辑");
                }

                _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：停止");
            }
            catch (Exception ex)
            {
                if (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：异常" + ex.Message + ex.StackTrace);
                }
                else
                {
                    _logger.LogInformation(DateTime.Now.ToString() + "BackgroundService：停止");
                }
            }
        }
    }
}
