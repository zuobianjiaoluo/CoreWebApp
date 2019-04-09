一、介绍
　　Quartz.Net是根据Java的Quartz用C#改写而来，最新的版本是3.0.6，源码在https://github.com/quartznet/quartznet。主要作用是做一些周期性的工作，或者定时工作。比如每天凌晨2点对前一天的数据统计。

二、简单的案例
　　以WebApi项目举例，用VS脚手架功能新建WebApi项目。

public void ConfigureServices(IServiceCollection services)
{
     services.AddMvc();
     services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();//注册ISchedulerFactory的实例。
 }
复制代码
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
        public async Task<string[]> Get()
        {
　　　　　　　//1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
　　　　　　　//2、开启调度器
            await _scheduler.Start();
　　　　　　　//3、创建一个触发器
            var trigger = TriggerBuilder.Create()
                            .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
                            .Build();
　　　　　　  //4、创建任务
            var jobDetail = JobBuilder.Create<MyJob>()
                            .WithIdentity("job", "group")
                            .Build();
　　　　　　　//5、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);
            return await Task.FromResult(new string[] { "value1", "value2" });
        }
   }
复制代码
复制代码
public class MyJob : IJob//创建IJob的实现类，并实现Excute方法。
    {
        public Task Execute(IJobExecutionContext context)
        {
　　　　　　return Task.Run(() =>
            　　{
                　　using (StreamWriter sw = new StreamWriter(@"C:\Users\Administrator\Desktop\error.log", true, Encoding.UTF8))
                　　{
                    　　sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                　　}
            });
        }
    }
复制代码
输出的结果：

2018-08-03 00-03-19
2018-08-03 00-03-20
2018-08-03 00-03-22
2018-08-03 00-03-24
2018-08-03 00-03-26

上面这种执行的Job没有参数，当需要参数可以通过下面两种方法传递参数：

1、在Trigger中添加参数值

 var trigger3 = TriggerBuilder.Create()
                        .WithSimpleSchedule(x =>x.WithIntervalInSeconds(2).RepeatForever())//间隔2秒 一直执行
                        .UsingJobData("key1", 321)  //通过在Trigger中添加参数值
                        .UsingJobData("key2", "123")
                        .WithIdentity("trigger2", "group1")
                        .Build();
2、在Job中添加参数值

 IJobDetail job = JobBuilder.Create<MyJob>()
                                .UsingJobData("key1", 123)//通过Job添加参数值
                                .UsingJobData("key2", "123")
                                .WithIdentity("job1", "group1")
                                .Build();
通过下面方法在Job中获取参数值

复制代码
public class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;//获取Job中的参数

            var triggerData = context.Trigger.JobDataMap;//获取Trigger中的参数

            var data = context.MergedJobDataMap;//获取Job和Trigger中合并的参数

            var value1= jobData.GetInt("key1");
            var value2= jobData.GetString("key2");

            var dateString = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");return Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Administrator\Desktop\error.log", true, Encoding.UTF8))
                {
                    sw.WriteLine(dateString);
                }
            });
        }
    }
复制代码
当Job中的参数和Trigger中的参数名称一样时，用 context.MergedJobDataMap获取参数时，Trigger中的值会覆盖Job中的值。

3、上面那种情况只能适应那种，参数值不变的情况。假如有这种情况，这次的参数值是上一次执行后计算的值，就不能使用上面方法了。如 每两秒实现累加一操作，现在初始值是0，如果按照上面那种获取值的操作，一直都是0+1，返回值一直都是1。为了满足这个情况，只需要加一个特性[PersistJobDataAfterExecution]。

复制代码
    [PersistJobDataAfterExecution]//更新JobDetail的JobDataMap的存储副本，以便下一次执行这个任务接收更新的值而不是原始存储的值
    public class MyJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;
            var triggerData = context.Trigger.JobDataMap;
            var data = context.MergedJobDataMap;

            var value1 = jobData.GetInt("key1");
            var value2 = jobData.GetString("key2");
            var value3 = data.GetString("key2");

            var dateString = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            Random random = new Random();

            jobData["key1"] = random.Next(1, 20);//这里面给key赋值，下次再进来执行的时候，获取的值为更新的值，而不是原始值
            jobData["key2"] = dateString;

            return Task.Run(() =>
            {
                using (StreamWriter sw = new StreamWriter(@"C:\Users\Administrator\Desktop\error.log", true, Encoding.UTF8))
                {
                    sw.WriteLine($"{dateString} value1:{value1} value2:{value2}");
                }
                //context.Scheduler.DeleteJob(context.JobDetail.Key);
                //context.Scheduler.Shutdown();
            });
        }
    }
复制代码
 三、Quartz.Net组成
　　Quartz主要有三部分组成任务(Job)、触发器(Trigger)和调度器(Schedule)。

　　3.1 任务
　　　　Job就是执行的作业，Job需要继承IJob接口，实现Execute方法。Job中执行的参数从Execute方法的参数中获取。

　　3.2 触发器
 　　　　触发器常用的有两种：SimpleTrigger触发器和CronTrigger触发器。

　　　　SimpleTrigger:能是实现简单业务，如每隔几分钟，几小时触发执行，并限制执行次数。

var trigger = TriggerBuilder.Create()
                       .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).WithRepeatCount(5))//间隔2秒 执行6次
                       .UsingJobData("key1", 321)
                       .WithIdentity("trigger", "group")
                       .Build();
　　　　CronTrigger:Cron表达式包含7个字段，秒 分 时 月内日期 月 周内日期 年(可选)。





　　举例：

 var trigger = TriggerBuilder.Create()
                       .WithCronSchedule("0 0 0 1 1 ?")// 每年元旦1月1日 0 点触发
                       .UsingJobData("key1", 321)
                       .UsingJobData("key2", "trigger-key2")
                       .WithIdentity("trigger4", "group14")
                       .Build();
"0 15 10 * * ? *"         每天上午10:15触发  

"0 0-5 14 * * ?"          每天下午2点到下午2:05期间的每1分钟触发  

　　3.3 调度器
　　　　调度器就是将任务和触发器绑定，让触发器触发的时候去执行任务。


