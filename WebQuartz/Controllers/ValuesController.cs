using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quartz;

namespace WebQuartz.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;
        public ValuesController(ISchedulerFactory schedulerFactory)
        {
            this._schedulerFactory = schedulerFactory;
        }

        [HttpGet]
        public async Task<string[]> GetER()
        {
            //1、通过调度工厂获得调度器

            return await Task.FromResult(new string[] { "value12", "value23" });
        }


        [HttpPost]
        public async Task<string[]> GetERDDDD()
        {
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            //2、开启调度器
            await _scheduler.Start();
            //3、创建一个触发器 无参数
            var trigger = TriggerBuilder.Create()
                            .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
                            .Build();

            //传递参数 在Trigger中添加参数值 
            var trigger3 = TriggerBuilder.Create()
                       .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//间隔2秒 一直执行
                       .UsingJobData("key1", 321)  //通过在Trigger中添加参数值
                       .UsingJobData("key2", "123")
                       .WithIdentity("trigger2", "group1")
                       .Build();

            //SimpleTrigger:能是实现简单业务，如每隔几分钟，几小时触发执行，并限制执行次数
            var trigger4 = TriggerBuilder.Create()
                       .WithSimpleSchedule(x => x.WithIntervalInSeconds(2) //间隔2秒
                       .WithRepeatCount(5))//指定触发器将重复的时间-触发总数将为该数字+1  执行6次       
                       .UsingJobData("key1", 321)
                       .WithIdentity("trigger", "group")
                       .Build();
            //常用表达式例子
            //0 0 2 1 * ? *表示在每月的1日的凌晨2点调整任
            //0 15 10 ? *MON - FRI   表示周一到周五每天上午10:15执行作业
            //0 15 10 ? 6L 2002 - 2006   表示2002 - 2006年的每个月的最后一个星期五上午10: 15执行作
            //0 0 10,14,16 * * ? 每天上午10点，下午2点，4点
            //0 0 / 30 9 - 17 * * ? 朝九晚五工作时间内每半小时
            //0 0 12 ? *WED    表示每个星期三中午12点
            //0 0 12 * * ? 每天中午12点触发
            //0 15 10 ? **每天上午10 : 15触发
            //0 15 10 * * ? 每天上午10 : 15触发
            //0 15 10 * * ? *每天上午10 : 15触发
            //0 15 10 * * ? 2005    2005年的每天上午10: 15触发
            //0 * 14 * * ? 在每天下午2点到下午2 : 59期间的每1分钟触发
            //0 0/5 14 * * ? 在每天下午2点到下午2 : 55期间的每5分钟触发
            //0 0/5 14,18 * * ? 在每天下午2点到2 : 55期间和下午6点到6: 55期间的每5分钟触发
            //0 0-5 14 * * ? 在每天下午2点到下午2 : 05期间的每1分钟触发
            //0 10,44 14 ? 3 WED    每年三月的星期三的下午2: 10和2: 44触发
            //0 15 10 ? *MON - FRI    周一至周五的上午10:15触发
            //0 15 10 15 * ? 每月15日上午10 : 15触发
            //0 15 10 L * ? 每月最后一日的上午10 : 15触发
            //0 15 10 ? *6L    每月的最后一个星期五上午10:15触发
            //0 15 10 ? *6L 2002 - 2005   2002年至2005年的每月的最后一个星期五上午10: 15触发
            //0 15 10 ? *6#3   每月的第三个星期五上午10:15触发
            //0 15 10 * * ? *        每天上午10: 15触发
            //0 0-5 14 * * ?         每天下午2点到下午2: 05期间的每1分钟触发
            //0 0/2 * * * ?  每2分钟触发
            //CronTrigger:Cron表达式包含7个字段，秒 分 时 月内日期 月 周内日期 年(可选)。
            var trigger5 = TriggerBuilder.Create()
                       .WithCronSchedule("0 0/2 * * * ?")// 每2分钟触发
                       .UsingJobData("key1", 321)
                       .UsingJobData("key2", "trigger-key2")
                       .WithIdentity("trigger4", "group14")
                       .Build();

            //4、创建任务
            var jobDetail = JobBuilder.Create<MyJob>()
                           .WithIdentity("job", "group")
                           .Build();
            //传递参数 在Job中添加参数值 2
            IJobDetail job = JobBuilder.Create<MyJob>()
                                .UsingJobData("key1", 123)//通过Job添加参数值
                                .UsingJobData("key2", "123")
                                .WithIdentity("job1", "group1")
                                .Build();


            //5、调度器    调度器就是将任务和触发器绑定，让触发器触发的时候去执行任务。
            await _scheduler.ScheduleJob(jobDetail, trigger5);

            return await Task.FromResult(new string[] { "value1", "value2" });
        }
    }
}
